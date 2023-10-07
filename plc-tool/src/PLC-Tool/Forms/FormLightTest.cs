using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCTool.Lights;
using PLCTool.Lights.Wordop;
using PLCTool.Lights.ZhongZhi;
using PLCTool.Lights.ChenKe;
using PLCTool.Lights.KangShiDa;

namespace PLCTool.Forms
{
    public partial class FormLightTest : BaseForm
    {
        public FormLightTest()
        {
            InitializeComponent();
        }

        private Thread tdRefreshPortList;
        private LightBase lightNow;
        private LightSupplier Supplier;
        /// <summary>
        /// 当前选中操作的通道
        /// </summary>
        private string NowOperateChannel
        {
            get
            {
                return Supplier == LightSupplier.康视达 ? cmbKSDChannel.Text : nudChannel.Value.ToString();
            }
        }
        private bool StopRefresh = false;

        private void AfterSentData(byte[] sentData, Encoding encoding = null)
        {
            string displayStr = "";
            if (encoding == null)
            {
                foreach (byte btData in sentData)
                {
                    displayStr += btData.ToString("x2").ToUpper() + " ";
                }
            }
            else
                displayStr = encoding.GetString(sentData);

            AddLog($"发送数据：{displayStr}", DateTime.Now, Color.White);
        }

        private void AfterReceiveData(byte[] receiveData, Encoding encoding = null)
        {
            string displayStr = "";
            if (encoding == null)
            {
                foreach (byte btData in receiveData)
                {
                    displayStr += btData.ToString("x2").ToUpper() + " ";
                }
            }
            else
                displayStr = encoding.GetString(receiveData);

            AddLog($"接收数据：{displayStr}", DateTime.Now, Color.MediumAquamarine);
        }

        private void RefreshPortsList(object refeshParams)
        {
            object[] paramsArray = (object[])refeshParams;
            string deviceType = (string)paramsArray[0];
            int baudRate = (int)paramsArray[1];
            byte deviceIDOrAddressFrom = (byte)paramsArray[2];
            byte deviceIDOrAddressTo = (byte)paramsArray[3];
            int delayTimeForReceive = (int)paramsArray[4];
            bool onlyDisplayMayConnectCom = (bool)paramsArray[5];            

            StopRefresh = false;
            string[] portNames = SerialPort.GetPortNames();
            bool connectResult;
            int firstConnectComIndex = -1;
            int dealedNum = 0, totalNum = Math.Abs(deviceIDOrAddressTo - deviceIDOrAddressFrom + 1) * portNames.Length;
            for (int i = 0; i < portNames.Length; i++)
            {
                if (StopRefresh)
                    break;

                for (byte deviceIDOrAddress = deviceIDOrAddressFrom; deviceIDOrAddress <= deviceIDOrAddressTo; deviceIDOrAddress++)
                {
                    if (StopRefresh)
                        break;

                    //设备类型                
                    switch (Supplier)
                    {
                        case LightSupplier.WORDOP:
                            lightNow = new WordopLight(portNames[i], baudRate, 0x03, deviceIDOrAddress, delayTimeForReceive);
                            break;
                        case LightSupplier.辰科:
                            lightNow = new ChenKeLight(portNames[i], baudRate, deviceIDOrAddress, delayTimeForReceive);
                            break;
                        case LightSupplier.众智:
                            lightNow = new ZhongZhiLight(portNames[i], baudRate, deviceIDOrAddress, delayTimeForReceive);
                            break;
                        case LightSupplier.康视达:
                            lightNow = new KangShiDaLight(portNames[i], baudRate, deviceIDOrAddress, delayTimeForReceive);
                            break;
                    }
                    //连接                    
                    connectResult = lightNow.TestConnect();                    
                    dealedNum++;

                    //显示
                    this.Invoke(new Action(() =>
                    {
                        //第一次连接时，清空串口列表
                        if (i == 0 && deviceIDOrAddress == deviceIDOrAddressFrom)
                            lbxPorts.Items.Clear();

                        //扫描结果添加到串口列表
                        if (connectResult)
                        {
                            if (lbxPorts.Items.Contains(portNames[i]))
                                lbxPorts.Items.Remove(portNames[i]);
                            lbxPorts.Items.Add(portNames[i] + $"(ID:[{deviceIDOrAddress}]，已连接)");
                            if (firstConnectComIndex < 0) firstConnectComIndex = lbxPorts.Items.Count - 1;

                            //已连接上的后续不再尝试
                            dealedNum += deviceIDOrAddressTo - deviceIDOrAddress;
                        }
                        else if (!onlyDisplayMayConnectCom && !lbxPorts.Items.Contains(portNames[i]))
                            lbxPorts.Items.Add(portNames[i]);

                        //最后一次连接时，选中第一条可连接串口
                        if (i == portNames.Length - 1 && (connectResult || deviceIDOrAddress == deviceIDOrAddressTo))
                            lbxPorts.SelectedIndex = firstConnectComIndex;

                        //更新扫描进度
                        pbRefreshProgress.Value = dealedNum * 100 / totalNum;
                    }));

                    //已连接上的后续不再尝试
                    if (connectResult)                                           
                        break;                    
                }
            }
        }

        private void btnRefreshPortsList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxDeviceType.Text))
            {
                MessageBox.Show("请先选择设备类型！");
                return;
            }
            if (string.IsNullOrEmpty(cbxBaudRate.Text))
            {
                MessageBox.Show("请先选择波特率！");
                return;
            }
            int baudRate;
            if (!int.TryParse(cbxBaudRate.Text, out baudRate))
            {
                MessageBox.Show("波特率不是有效的数值！");
                return;
            }

            if (tdRefreshPortList != null && tdRefreshPortList.IsAlive)
            {
                MessageBox.Show("已有扫描任务在运行！");
                return;
            }

            tdRefreshPortList = new Thread(new ParameterizedThreadStart(RefreshPortsList));
            tdRefreshPortList.Start(new object[] { cbxDeviceType.Text, baudRate, (byte)nudDeviceIdOrAddressFrom.Value, (byte)nudDeviceIdOrAddressTo.Value, (int)nudDelayTimeForReceive.Value, chkOnlyDisplayMayConnectCom.Checked });
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前语言的按钮文本
                string openText, closeText;
                switch (Thread.CurrentThread.CurrentCulture.Name)
                {
                    case "zh-CN":
                        openText = "打开";
                        closeText = "关闭";
                        break;
                    case "zh-TW":
                        openText = "打開";
                        closeText = "關閉";
                        break;
                    case "en":
                        openText = "Open";
                        closeText = "Close";
                        break;
                    default:
                        openText = "打开";
                        closeText = "关闭";
                        break;
                }

                //打开或关闭串口
                if (btnOpen.Text == openText)
                {
                    byte deviceID = 0;
                    string portName;
                    if (lbxPorts.SelectedItem == null)
                    {
                        MessageBox.Show("请先选择串口！");
                        return;
                    }
                    else
                    {
                        portName = lbxPorts.SelectedItem.ToString();
                        //设备ID
                        int indexFrom = portName.IndexOf('[');
                        int indexEnd = portName.IndexOf(']');
                        if (indexEnd > indexFrom && indexFrom >= 0)
                        {
                            string strDeviceID = portName.Substring(indexFrom + 1, indexEnd - indexFrom - 1);
                            byte.TryParse(strDeviceID, out deviceID);
                        }
                        //串口名称
                        indexFrom = portName.IndexOf('(');
                        if (indexFrom > 0)
                        {
                            portName = portName.Substring(0, indexFrom);
                        }

                    }
                    if (string.IsNullOrEmpty(cbxBaudRate.Text))
                    {
                        MessageBox.Show("请先选择波特率！");
                        return;
                    }
                    int baudRate;
                    if (!int.TryParse(cbxBaudRate.Text, out baudRate))
                    {
                        MessageBox.Show("波特率不是有效的数值！");
                        return;
                    }

                    //设备类型                
                    switch (Supplier)
                    {
                        case LightSupplier.WORDOP:
                            lightNow = new WordopLight(portName, baudRate, 0x03, deviceID, (int)nudDelayTimeForReceive.Value);
                            break;
                        case LightSupplier.辰科:
                            lightNow = new ChenKeLight(portName, baudRate, deviceID, (int)nudDelayTimeForReceive.Value);
                            break;
                        case LightSupplier.众智:
                            lightNow = new ZhongZhiLight(portName, baudRate, deviceID, (int)nudDelayTimeForReceive.Value);
                            break;
                        case LightSupplier.康视达:
                            lightNow = new KangShiDaLight(portName, baudRate, deviceID, (int)nudDelayTimeForReceive.Value);
                            break;
                    }

                    //测试连接
                    if (lightNow.TestConnect(false))
                    {
                        AddLog($"{portName} 串口打开成功！", DateTime.Now, LogType.info);
                        lightNow.AfterSentDataEvent += AfterSentData;
                        lightNow.AfterReceiveDataEvent += AfterReceiveData;

                        if (lightNow is KangShiDaLight)//读取亮度信息 
                            cmbKSDChannel_SelectedIndexChanged(null, null);
                        else                                   
                            btnReadChannelInfo_Click(null, null);//读取通道信息                        
                        gbxPortsScan.Enabled = false;
                        gbxLightOperate.Enabled = true;
                        btnOpen.Text = closeText;
                        btnOpen.BaseColor = Color.Red;
                        cmbChannelAwalysStatus.SelectedIndex = 0;
                        pnlKangShiDa.Enabled = lightNow is KangShiDaLight;
                    }
                    else
                        AddLog($"{portName} 串口打开失败！", DateTime.Now, LogType.error);
                }
                else
                {
                    if (lightNow != null)
                    {
                        lightNow.AfterSentDataEvent -= AfterSentData;
                        lightNow.AfterReceiveDataEvent -= AfterReceiveData;
                        lightNow.ClosePort();
                    }
                    gbxPortsScan.Enabled = true;
                    gbxLightOperate.Enabled = false;
                    btnOpen.Text = openText;
                    btnOpen.BaseColor = Color.FromArgb(52, 152, 219);
                    AddLog($"串口关闭成功！", DateTime.Now, LogType.warn);
                }
            }
            catch (Exception ex)
            {
                this.btnOpen.Enabled = true;
                AddLog("串口操作出现错误：" + ex.Message, DateTime.Now, LogType.error);
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lbxLog.Items.Clear();
        }

        private void cmbChannelOperateWide_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strChannelOperateSpecial,strChannelOperateAll;
            switch (Thread.CurrentThread.CurrentCulture.Name)
            {
                case "zh-CN":
                    strChannelOperateSpecial = "读取指定通道信息";
                    strChannelOperateAll = "读取所有通道信息";
                    break;
                case "zh-TW":
                    strChannelOperateSpecial = "讀取指定通道信息";
                    strChannelOperateAll = "讀取所有通道信息";
                    break;
                case "en":
                    strChannelOperateSpecial = "Reads specified channel";
                    strChannelOperateAll = "Read all channel";
                    break;
                default:
                    strChannelOperateSpecial = "读取指定通道信息";
                    strChannelOperateAll = "读取所有通道信息";
                    break;
            }

            if (cmbChannelOperateWide.SelectedIndex == 0)
            {
                pnlChannel.Visible = true;
                btnReadChannelInfo.Text = strChannelOperateSpecial;                
            }
            else
            {
                pnlChannel.Visible = false;
                btnReadChannelInfo.Text = strChannelOperateAll;
            }
        }

        private void btnReadChannelInfo_Click(object sender, EventArgs e)
        {
            if (lightNow == null)
                return;

            if (lightNow is WordopLight)
            {
                byte[] receivePackerBytes = cmbChannelOperateWide.SelectedIndex == 0 ? lightNow.ReadAllChannel() : lightNow.ReadOneChannel(NowOperateChannel);
                if (receivePackerBytes == null)
                    return;

                Lights.Wordop.CommandType commandType = cmbChannelOperateWide.SelectedIndex == 0 ? Lights.Wordop.CommandType.AllChannelInfo_DeviceReback : Lights.Wordop.CommandType.OneChannelInfo_DeviceReback;
                Lights.Wordop.CommandBase commandBase = new Lights.Wordop.ReceivePackerBase(receivePackerBytes).Commands.FirstOrDefault(item => item.CommandCode == commandType);
                if (commandBase != null) tbSetBrightness.Value = commandBase.CommandParas[0];
            }            
        }

        private void tbSetBrightness_Scroll(object sender, EventArgs e)
        {
            if (lightNow == null)
                return;

            if (cmbChannelOperateWide.SelectedIndex == 0)
                lightNow.SetOneChannelBrightness(NowOperateChannel, (byte)tbSetBrightness.Value);

            else
                lightNow.SetAllChannelBrightness((byte)tbSetBrightness.Value);                    
        }

        private void tbSetBrightness_ValueChanged(object sender, EventArgs e)
        {
            lblBrightnessValue.Text = tbSetBrightness.Value.ToString();            
        }

        private void btnNewIDOrAddress_Click(object sender, EventArgs e)
        {
            if (lightNow == null)
                return;
            if (MessageBox.Show("确定要将当前设备ID或地址设置为：" + nudNewIDOrAddress.Value + "？", "设置设备ID或地址", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (lightNow.SetDeviceIDOrAddress((byte)nudNewIDOrAddress.Value))
            {
                lightNow.DeviceIDOrAddress = (byte)nudNewIDOrAddress.Value;
                AddLog($"设置设备新ID或地址[{nudNewIDOrAddress.Value}]成功！", DateTime.Now, LogType.warn);
            }
            else
                AddLog("设置设备新ID或地址失败！", DateTime.Now, LogType.error);
        }

        private void cbxDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(cbxDeviceType.Text, out Supplier);            
            pnlKangShiDa.Visible = Supplier == LightSupplier.康视达;
            pnlOperate.Visible = Supplier != LightSupplier.康视达;            
            pnlKangShiDa.Location = new Point(3, lblSetBrightness.Location.Y + lblSetBrightness.Height + 10);
            pnlOperate.Location = new Point(3, lblSetBrightness.Location.Y + lblSetBrightness.Height + 10);
            //指定通道操作可见性
            nudChannel.Visible = Supplier != LightSupplier.康视达;
            cmbKSDChannel.Visible = Supplier == LightSupplier.康视达;
            nudChannel.Location = new Point(lblChannel.Location.X + lblChannel.Width + 5, lblChannel.Location.Y + lblChannel.Height / 2 - nudChannel.Height / 2);
            cmbKSDChannel.Location = new Point(lblChannel.Location.X + lblChannel.Width + 5, lblChannel.Location.Y + lblChannel.Height / 2 - cmbKSDChannel.Height / 2);
        }

        private void cmbKSDChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lightNow == null)
                return;

            tbSetBrightness.Value = lightNow.ReadOneChannelBrightness(cmbKSDChannel.Text);
        }

        private void btnReadChannelAlwaysStatus_Click(object sender, EventArgs e)
        {
            lightNow?.ReadChannelLightAlwaysOnStatus();
        }

        private void btnSetChannelAlwaysStatus_Click(object sender, EventArgs e)
        {
            lightNow?.SetChannelLightAlwaysOnStatus(cmbChannelAwalysStatus.SelectedIndex == 0);
        }

        private void FormLightTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopRefresh = true;
            tdRefreshPortList?.Abort();
            tdRefreshPortList?.Join();
            lightNow?.ClosePort();
        }

        private void FormLightTest_Load(object sender, EventArgs e)
        {
            cbxDeviceType.Items.Clear();
            cbxDeviceType.DataSource = Enum.GetNames(typeof(LightSupplier));            
            cbxDeviceType.SelectedIndex = cbxDeviceType.Items.Count > 0 ? 0 : -1;
            cmbKSDChannel.SelectedIndex = 0;
            cmbChannelAwalysStatus.SelectedIndex = 0;
            cbxBaudRate.Text = "19200";
            cmbChannelOperateWide.SelectedIndex = 0;
            cmbChannelOperateWide_SelectedIndexChanged(null, null);            
        }

        #region 日志

        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            info,
            error,
            warn,
            normal
        }

        private struct LogData
        {
            internal LogData(LogType type, string value)
            {
                LogType = type;
                LogValue = value;
                ValueColor = null;
            }

            internal LogData(string value, Color valueColor)
            {
                LogType = LogType.info;
                LogValue = value;
                ValueColor = valueColor;
            }

            internal LogType LogType { get; set; }
            internal string LogValue { get; set; }

            internal Color? ValueColor { get; set; }
        }

        private delegate void AddLogDelegate(string log, DateTime dt, LogType type = LogType.info);
        private void AddLog(string log, DateTime dt, LogType type = LogType.info)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new AddLogDelegate(AddLog), new object[] { log, dt, type });
            }
            else
            {
                string tmpStr = ">" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + log;

                this.lbxLog.Items.Add(new LogData(type, tmpStr));
                lbxLog.SelectedIndex = lbxLog.Items.Count - 1;
                lbxLog.SelectedIndex = -1;
            }
        }

        private void AddLog(string log, DateTime dt, Color valueColor)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new AddLogDelegate(AddLog), new object[] { log, dt, valueColor });
            }
            else
            {
                string tmpStr = ">" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + log;

                lbxLog.Items.Add(new LogData(tmpStr, valueColor));
                lbxLog.SelectedIndex = lbxLog.Items.Count - 1;
                lbxLog.ClearSelected();
            }
        }

        private void lbxLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();//先调用基类实现            

            if (e.Index < 0)//窗体加载时不操作
                return;
            LogData data = (LogData)lbxLog.Items[e.Index];
            Brush bsColor;
            if (data.ValueColor != null)
                bsColor = new SolidBrush((Color)data.ValueColor);
            else
            {
                switch (data.LogType)
                {
                    case LogType.error:
                        bsColor = Brushes.Red;
                        break;
                    case LogType.warn:
                        bsColor = Brushes.Yellow;
                        break;
                    case LogType.info:
                        bsColor = Brushes.LimeGreen;
                        break;
                    default:
                        bsColor = Brushes.White;
                        break;
                }
            }

            e.Graphics.DrawString(data.LogValue, e.Font, bsColor, e.Bounds);
        }

        #endregion
    }
}
