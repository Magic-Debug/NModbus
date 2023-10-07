using System.ComponentModel;
using System.IO;
using System.Collections.Concurrent;
using MainFrom;
using FrameworkCommon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PLCTool.Forms
{

    /// <summary>
    /// [Modbus主站]主动发出指令
    /// </summary>
    public partial class FrmDashboard : BaseNModbusForm
    {
        ILogger<FrmDashboard> Logger { get; }

        public FrmDashboard(IConfiguration configuration, ILogger<FrmDashboard> logger, IOptions<PlcOptions> plcOptions)
        {
            InitializeComponent();
            Logger = logger;
        }

        #region 属性

        /// <summary>
        /// 
        /// </summary>


        private string lengthUnit = "m";
        private string speedUnit = "m/s";
        private float maxSpeed;
        private bool isconnected = false;

        //批量贴标坐标列表
        private ConcurrentQueue<PointF> queBatchTicketPoints => new ConcurrentQueue<PointF>();


        //是否正在编辑批量贴标坐标
        private bool IsEditBatchPoints = false;
        //上一坐标是否已尝试过贴标
        private bool LastPointHastTryTicket = true;

        //从键盘接收到的命令（此功能隐藏，只为方便开发者调试）
        private string debugcmd { get; set; }

        //打开监控的隐藏指令
        private string openmonitorcmd = "mon";
        private string opensettingscmd = "set";
        private string openclosereadcmd = "stt";

        #endregion

        #region 多语言
        private string ConnectedInfo => "已连接";
        private string NotConnectedInfo => LanguageResource.FormMain_NotConnectedInfo;
        private string XinyuanLabel23 => LanguageResource.FormMain_XinyuanLabel23;
        private string FineTensionAdjustmentRange_Add => LanguageResource.FormMain_FineTensionAdjustmentRange_Add;
        private string ClothOutSpeed => LanguageResource.FormMain_ClothOutSpeed;
        private string LeatherLabel22 => LanguageResource.FormMain_LeatherLabel22;
        #endregion

        #region 事件
        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSetValue_Tick(object sender, EventArgs e)
        {
            if (1 > 0)
            {
                toolStripStatusLabel1.Text = ConnectedInfo;
                toolStripStatusLabel1.ForeColor = Color.Green;
                toolStripStatusLabel2.Text = Host;
                toolStripStatusLabel2.ForeColor = Color.Green;
                SetControlText();
                if (!isconnected)
                {
                    ModbusStatus status = new ModbusStatus(modbusValues);

                }
            }
            else
            {
                toolStripStatusLabel1.Text = NotConnectedInfo;
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel2.Text = "";
            }
        }

        /// <summary>
        /// 长度单位换算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLength_Click(object sender, EventArgs e)
        {
            if (lengthUnit == "m")
            {
                lengthUnit = "yd";
                lblength.Text = "yd";
                btnLength.Text = (modbusStatus.EncoderLength / 0.9144).ToString("f2") + "  ";
            }
            else
            {
                lengthUnit = "m";
                lblength.Text = "m";
                btnLength.Text = (modbusStatus.EncoderLength).ToString("f2") + "  ";
            }
        }
        /// <summary>
        /// 速度单位换算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpeed_Click(object sender, EventArgs e)
        {
            if (speedUnit == "m/s")
            {
                speedUnit = "m/min";
                btnSpeed.Text = (modbusStatus.MainSpeed * 60).ToString("f2") + "   ";
            }
            else if (speedUnit == "m/min")
            {
                speedUnit = "km/h";
                btnSpeed.Text = (modbusStatus.MainSpeed * 3.6).ToString("f2") + "   ";
            }
            else if (speedUnit == "km/h")
            {
                speedUnit = "yd/s";
                btnSpeed.Text = (modbusStatus.MainSpeed / 0.9144).ToString("f2") + "   ";
            }
            else if (speedUnit == "yd/s")
            {
                speedUnit = "yd/min";
                btnSpeed.Text = (modbusStatus.MainSpeed * 60 / 0.9144).ToString("f2") + "   ";
            }
            else if (speedUnit == "yd/min")
            {
                speedUnit = "kyd/h";
                btnSpeed.Text = (modbusStatus.MainSpeed * 3.6 / 0.9144).ToString("f2") + "   ";
            }
            else
            {
                speedUnit = "m/s";
                btnSpeed.Text = modbusStatus.MainSpeed.ToString("f2") + "   ";
            }
            lbspeed.Text = speedUnit;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            Master.WriteSingleRegister(1, ModbusRegs.PLCStart_stop, 1);
            ushort[]? data = Master.ReadHoldingRegisters(1, ModbusRegs.PLCStart_stop, 1);
            Logger.LogInformation("启动设备");
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            Master.WriteSingleRegister(1, ModbusRegs.PLCStart_stop, 0);
            Logger.LogInformation("停止设备");
        }
        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                Master.WriteSingleRegister(1, ModbusRegs.ResetPLC, 1);
                await Task.Delay(50);
                Master.WriteSingleRegister(1, ModbusRegs.ResetPLC, 0);
                Logger.LogInformation("复位设备");
                ushort[]? data = Master.ReadHoldingRegisters(1, ModbusRegs.ResetPLC, 1);
            });
        }
        /// <summary>
        /// 归零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnZero_Click(object sender, EventArgs e)
        {
            ModbusStatus states = modbusStatus;
            states.IndicatorLight_allReturnZero = true;
            Master.WriteSingleRegister(1, ModbusRegs.IndicatorLight, states.IndicatorLight);
            Thread.Sleep(50);
            states.IndicatorLight_allReturnZero = false;
            Master.WriteSingleRegister(1, ModbusRegs.IndicatorLight, states.IndicatorLight);
            Thread.Sleep(50);
            Logger.LogInformation("设备归零");
            ushort[]? data = Master.ReadHoldingRegisters(1, ModbusRegs.IndicatorLight, 1);
        }
        /// <summary>
        /// 贴标机归零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTicketReturnZero_Click(object sender, EventArgs e)
        {
            ModbusStatus states = modbusStatus;
            states.IndicatorLight_ticketReturnZero = true;
            Master.WriteSingleRegister(1, ModbusRegs.IndicatorLight, states.IndicatorLight);
            Thread.Sleep(50);
            states.IndicatorLight_ticketReturnZero = false;
            Master.WriteSingleRegister(1, ModbusRegs.IndicatorLight, states.IndicatorLight);
            Thread.Sleep(50);
            Logger.LogInformation("贴标机归零");
        }
        /// <summary>
        /// 急停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmergencyStop_Click(object sender, EventArgs e)
        {
            Master.WriteSingleRegister(1, ModbusRegs.SetScram, 1);
            ushort[]? data = Master.ReadHoldingRegisters(1, ModbusRegs.SetScram, 1);
            ModbusEnhanced.WriteFloatHoldingRegisters(1, ModbusRegs.WindingSpeed, new float[1] { 4 * (-0.5f) });
            float[] ff = ModbusEnhanced.ReadFloatHoldingRegisters(1, ModbusRegs.WindingSpeed, 4);
            modbusValues[ModbusRegs.WindingSpeed] = (ushort)Environment.CurrentManagedThreadId;
            Logger.LogInformation("设备急停");
        }
        /// <summary>
        /// 编码器清零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEncoder_Click(object sender, EventArgs e)
        {
            Master.WriteSingleRegister(1, ModbusRegs.ResetEncoder, 1);
            ushort[]? data = Master.ReadHoldingRegisters(1, ModbusRegs.ResetEncoder, 18);
            Logger.LogInformation("编码器清零");
        }

        /// <summary>
        /// 释放键时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFloat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float value = Convert.ToSingle(((TextBox)sender).Text.Trim());
                if ((byte)((TextBox)sender).Tag == ModbusRegs.MasterMotorSpeed && value > maxSpeed)
                {
                    value = maxSpeed;
                }
                byte registerAddress = (byte)((TextBox)sender).Tag;
                ModbusEnhanced.WriteFloatHoldingRegisters(1, registerAddress, new float[1] { value });

                if ((byte)((TextBox)sender).Tag == ModbusRegs.MasterMotorSpeed)
                {
                    ModbusStatus status = new ModbusStatus(modbusValues);
                    status.MasterMotorSpeed = value;
                    btnLoad.Enabled = true;
                    if (chkAutoLoad.Checked)
                    {
                        Logger.LogError("当前设置不存在");
                    }
                    else if (SystemConfig.GetConfigValues("MachineType") == "1")
                    {
                        ModbusEnhanced.WriteFloatHoldingRegisters(1, ModbusRegs.WindingSpeed, new float[1] { value * (-0.5f) });
                    }
                }
                float[] ff = ModbusEnhanced.ReadFloatHoldingRegisters(1, registerAddress, 4);
                ((TextBox)sender).Text = ff[0].ToString("f3");
                ((TextBox)sender).ForeColor = Color.Lime;
            }
            else
            {
                ((TextBox)sender).ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// 当控件不再是窗体的活动控件时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFloat_Leave(object sender, EventArgs e) => ((TextBox)sender).ForeColor = Color.Red;

        /// <summary>
        /// 单选框点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoVWControl_Click(object sender, EventArgs e)
        {
            object[] tagContent = (object[])((Control)sender).Tag;
            byte registerAddress = (byte)tagContent[0];
            ushort configValue = Convert.ToUInt16(tagContent[1]);

            //发送命令
            Master.WriteSingleRegister(1, registerAddress, configValue);

            //状态保存到本地配置
            modbusValues[registerAddress] = configValue;
            ModbusStatus status = new ModbusStatus(modbusValues);
            btnLoad.Enabled = true;
            if (chkAutoLoad.Checked)
            {
                LogHelper.Default.Error("当前设置不存在");
            }
            else
            {
                btnLoad.Enabled = false;
            }
        }

        /// <summary>
        /// 自动刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbAutoRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Visible = true;
        }
        /// <summary>
        /// 刷新状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新周期，释放键时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScanRate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int value = Convert.ToInt32(txtScanRate.Text.Trim());
                if (value > 0)
                {
                    txtScanRate.ForeColor = Color.Lime;
                }
            }
            else
            {
                txtScanRate.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// 刷新周期，当控件不再是窗体的活动控件时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScanRate_Leave(object sender, EventArgs e)
        {
            txtScanRate.Text = "10";
            txtScanRate.ForeColor = Color.Black;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnLoad.Enabled = true;
        }
        /// <summary>
        /// 加载设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LogHelper.Default.Error("当前设置不存在");
        }
        /// <summary>
        /// 修改速度时自动加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoLoad_CheckedChanged(object sender, EventArgs e)
        {
            // SystemConfig.SaveConfigValue("AutoLoadSpeedSetting", chkAutoLoad.Checked ? "1" : "0");
        }

        /// <summary>
        /// 通知贴标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTicket_Click(object sender, EventArgs e)
        {
            Master.WriteSingleRegister(1, ModbusRegs.Ticket, 1);
            Logger.LogInformation("通知贴标");
        }
        /// <summary>
        /// 批量贴标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBatchTicket_Click(object sender, EventArgs e)
        {
            //停机
            btnStop_Click(null, null);

            IsEditBatchPoints = true;
            frmBatchTickPoints fbt = new frmBatchTickPoints(queBatchTicketPoints, chkAreaTicket.Checked);
            if (fbt.ShowDialog() == DialogResult.OK)
            {
                pcbAutoRefresh_Click(pcbAutoRefresh, e);//开启自动刷新
                pcbAutoRefresh.Enabled = false;//禁止关闭自动刷新
            }
            IsEditBatchPoints = false;
        }
        /// <summary>
        /// 模拟裁剪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCut_Click(object sender, EventArgs e)
        {
            Master.WriteSingleRegister(1, ModbusRegs.Cut, 1);
        }

        /// <summary>
        /// 蜂鸣器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbBuzzer_Click(object sender, EventArgs e)
        {
            ModbusStatus states = modbusStatus;
            if (states.Buzzer == 1)
            {
                Master.WriteSingleRegister(1, ModbusRegs.Buzzer, 0);
            }
            else
            {
                Master.WriteSingleRegister(1, ModbusRegs.Buzzer, 1);
            }
        }
        /// <summary>
        /// 屏蔽收卷/上布架无布检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbShieldSetting_wbjc_Click(object sender, EventArgs e)
        {
            ModbusStatus states = modbusStatus;
            states.ShieldSetting_wbjc = !states.ShieldSetting_wbjc;
            Master.WriteSingleRegister(1, ModbusRegs.ShieldSetting, states.ShieldSetting);
        }
        /// <summary>
        /// 空跑测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbShieldSetting_sbqd_Click(object sender, EventArgs e)
        {
            ModbusStatus states = modbusStatus;
            states.ShieldSetting_sbqd = !states.ShieldSetting_sbqd;
            Master.WriteSingleRegister(1, ModbusRegs.ShieldSetting, states.ShieldSetting);
        }
        /// <summary>
        /// 屏蔽设备启动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbShieldSetting_sjsjwbjc_Click(object sender, EventArgs e)
        {
            ModbusStatus states = modbusStatus;
            states.ShieldSetting_sjsjwbjc = !states.ShieldSetting_sjsjwbjc;
            Master.WriteSingleRegister(1, ModbusRegs.ShieldSetting, states.ShieldSetting);
        }

        /// <summary>
        /// 编码器脉冲长度，释放键时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOnePlusLength_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float value = Convert.ToSingle(txtOnePlusLength.Text.Trim());
                ModbusStatus.OnePlusLength = value;
                ModbusEnhanced.WriteFloatHoldingRegisters(1, ModbusRegs.OnePlusLength, new float[1] { ModbusStatus.OnePlusLength });
                float[]? data = ModbusEnhanced.ReadFloatHoldingRegisters(1, ModbusRegs.OnePlusLength, 2);
                txtOnePlusLength.ForeColor = Color.Lime;
            }
            else
            {
                txtOnePlusLength.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// 编码器脉冲长度，当控件不再是窗体的活动控件时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOnePlusLength_Leave(object sender, EventArgs e)
        {
            txtOnePlusLength.Text = SystemConfig.GetConfigValues("OnePlusLength");
            txtOnePlusLength.ForeColor = Color.Black;
        }

        /// <summary>
        /// 通讯监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuMonitor_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FormMsg)
                {
                    form.Activate();
                    return;
                }
            }
            FormMsg formmsg = new FormMsg();
            if (this.Location.X + this.Width + formmsg.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - formmsg.Width, Location.Y);
            }
            else if (Location.X - formmsg.Width / 2 < 0)
            {
                this.Location = new Point(0, Location.Y);
            }
            else
            {
                this.Location = new Point(Location.X - formmsg.Width / 2, Location.Y);
            }

            formmsg.Location = new Point(Location.X + Size.Width, Location.Y);
            formmsg.Height = Size.Height;
            formmsg.Show();
        }
        /// <summary>
        /// 查看PLC日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuLog_Click(object sender, EventArgs e)
        {
            ShowForm<FormLog>();
        }
        /// <summary>
        /// 查看验布日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tiViewDetectLog_Click(object sender, EventArgs e)
        {
            ShowForm<FormInspectionLog>();
        }
        /// <summary>
        /// 查看幅宽数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuWidthData_Click(object sender, EventArgs e)
        {
            ShowForm<FormWidthData>();
        }
        /// <summary>
        /// 传感器状态显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuIOState_Click(object sender, EventArgs e)
        {
            ShowForm<FormIOStatus>();
        }
        /// <summary>
        /// 设备光源测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLightTest_Click(object sender, EventArgs e)
        {
            ShowForm<FormLightTest>();
        }
        /// <summary>
        /// 设备色差仪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuColourDifference_Click(object sender, EventArgs e)
        {
            ShowForm<FormColourDifference>();
        }
        /// <summary>
        /// 厚度测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuThickness_Click(object sender, EventArgs e)
        {
            ShowForm<FormThickness>();
        }
        /// <summary>
        /// 手动功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuManual_Click(object sender, EventArgs e)
        {
            ShowForm<FormManual>();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuModifyPassword_Click(object sender, EventArgs e)
        {
            FormChangePassword form = new FormChangePassword();
            form.ShowDialog();
        }
        /// <summary>
        /// 简体中文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tiLanguage_Zh_CN_Click(object sender, EventArgs e)
        {
            SetMenu();
        }
        /// <summary>
        /// 繁體中文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tiLanguage_Zh_TW_Click(object sender, EventArgs e)
        {
            SetMenu();
        }
        /// <summary>
        /// English
        /// </summary>
        /// <param name="senderB"></param>
        /// <param name="e"></param>
        private void tiLanguage_en_Click(object sender, EventArgs e)
        {
            SetMenu();
        }
        /// <summary>
        /// 开发者选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeveloperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm<FormSetting>();
        }

        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAbout_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 单例非模态窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void ShowForm<T>() where T : Form, new()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is T)
                {
                    form.Activate();
                    return;
                }
            }
            T tForm = new T();
            tForm.Show();
        }
        /// <summary>
        /// 设置菜单，管理员和超级管理员可见
        /// </summary>
        private void SetMenu()
        {
            if (1 > 0)
            {
                menuLog.Visible = true;
                menuMonitor.Visible = true;
                menuManual.Visible = true;
                tiViewDetectLog.Visible = true;
                txtSlaveMotorRatio.ReadOnly = false;
                txtSwingMotorRatio.ReadOnly = false;
                txtTensionSpeed.ReadOnly = false;
                txtWindingSpeed.ReadOnly = false;
                txtTickedFallingHeight.ReadOnly = false;
                pcbBuzzer.Enabled = true;
                pcbShieldSetting_sjsjwbjc.Enabled = true;
                lblVD170.Visible = true;
                lblVD174.Visible = true;
                lblVD178.Visible = true;
                lblVD182.Visible = true;
                lblVD186.Visible = true;
                lblVD190.Visible = true;
                lblVD194.Visible = true;
                lblVD198.Visible = true;
                txtVD170.Visible = true;
                txtVD174.Visible = true;
                txtVD178.Visible = true;
                txtVD182.Visible = true;
                txtVD186.Visible = true;
                txtVD190.Visible = true;
                txtVD194.Visible = true;
                txtVD198.Visible = true;
                lblOnePlusLength.Visible = true;
                txtOnePlusLength.Visible = true;
                tsmiLightTest.Visible = true;
                menuColourDifference.Visible = true;

                DeveloperToolStripMenuItem.Visible = true;
                txtFinalWeight.ReadOnly = true;
                txtVD170.ReadOnly = true;
                txtVD174.ReadOnly = true;
                txtVD178.ReadOnly = true;
                txtVD182.ReadOnly = true;
                txtVD186.ReadOnly = true;
                txtVD190.ReadOnly = true;
                txtVD194.ReadOnly = true;
                txtVD198.ReadOnly = true;
                txtOnePlusLength.ReadOnly = false;// !IsSuperAdmin;
                lblAutoRefresh.Visible = true;
                pcbAutoRefresh.Visible = true;
                btnRefresh.Visible = true;
                lblScanRate.Visible = true;
                txtScanRate.Visible = true;
                btnBatchTicket.Visible = true;
                lblShieldSetting_wbjc.Visible = true;
                pcbShieldSetting_wbjc.Visible = true;
                lblShieldSetting_sbqd.Visible = true;
                pcbShieldSetting_sbqd.Visible = true;

                toolTip1.SetToolTip(btnStart, "VW68=1");
                toolTip1.SetToolTip(btnStop, "VW68=0");
                toolTip1.SetToolTip(btnReset, "VW64=1");
                toolTip1.SetToolTip(btnReturnZero, "V95.4=1");
                toolTip1.SetToolTip(btnTicketReturnZero, "V95.5=1");
                toolTip1.SetToolTip(btnEmergencyStop, "VW62=1");
                toolTip1.SetToolTip(btnEncoder, "VW66=1");
                toolTip1.SetToolTip(lblEncoder, "VD0");
                toolTip1.SetToolTip(txtEncoder, "VD0");
                toolTip1.SetToolTip(lblCurrentY2, "VD58");
                toolTip1.SetToolTip(txtCurrentY2, "VD58");
                toolTip1.SetToolTip(lblMainSpeed, "VD36");
                toolTip1.SetToolTip(txtMainSpeed, "VD36");
                toolTip1.SetToolTip(lblEncoderSpeed, "VD112");
                toolTip1.SetToolTip(txtEncoderSpeed, "VD112");
                toolTip1.SetToolTip(lblWindingTensionGetValue, "VD132");
                toolTip1.SetToolTip(txtWindingTensionGetValue, "VD132");
                toolTip1.SetToolTip(lblInspectionTensionGetValue, "VD124");
                toolTip1.SetToolTip(txtInspectionTensionGetValue, "VD124");
                toolTip1.SetToolTip(lblRealWeight, "VD22");
                toolTip1.SetToolTip(txtRealWeight, "VD22");
                toolTip1.SetToolTip(lblFinalWeight, "VD26");
                toolTip1.SetToolTip(txtFinalWeight, "VD26");
                toolTip1.SetToolTip(lblWeijinCount, "VD136");
                toolTip1.SetToolTip(txtWeijinCount, "VD136");
                //toolTip1.SetToolTip(lblWasteLentgh, "VD136");
                //toolTip1.SetToolTip(txtWasteLentgh, "VD136");
                toolTip1.SetToolTip(lblMasterMotorSpeed, "VD36");
                toolTip1.SetToolTip(txtMasterMotorSpeed, "VD36");
                toolTip1.SetToolTip(lblWindingTensionSetValue, "VD128");
                toolTip1.SetToolTip(txtWindingTensionSetValue, "VD128");
                toolTip1.SetToolTip(lblInspectionTensionSetValue, "VD120");
                toolTip1.SetToolTip(txtInspectionTensionSetValue, "VD120");
                toolTip1.SetToolTip(lblForwardBackMode, "VW82");
                toolTip1.SetToolTip(rdoBackMode, "VW82=0");
                toolTip1.SetToolTip(rdoForwardMode, "VW82=1");
                toolTip1.SetToolTip(lblPickAxles, "VW32");
                toolTip1.SetToolTip(rbnPickAxlesA, "VW32=0");
                toolTip1.SetToolTip(rbnPickAxlesB, "VW32=1");
                toolTip1.SetToolTip(lblReleaseAxles, "VW84");
                toolTip1.SetToolTip(rbnReleaseAxlesA, "VW84=0");
                toolTip1.SetToolTip(rbnReleaseAxlesB, "VW84=1");
                toolTip1.SetToolTip(lblProtectiveFilm, "VW98");
                toolTip1.SetToolTip(rbnProtectiveFilm_Close, "VW98=0");
                toolTip1.SetToolTip(rbnProtectiveFilm_Open, "VW98=1");
                toolTip1.SetToolTip(lblBSOrCloth, "VW34");
                toolTip1.SetToolTip(rbnBeiSi, "VW34=0");
                toolTip1.SetToolTip(rbnCloth, "VW34=1");
                toolTip1.SetToolTip(lblTensionMode, "VW4");
                toolTip1.SetToolTip(rdoBasicCloth, "VW4=0");
                toolTip1.SetToolTip(rdoTensionCloth, "VW4=1");
                toolTip1.SetToolTip(lblCurrentX, "VD50");
                toolTip1.SetToolTip(txtCurrentX, "VD50");
                toolTip1.SetToolTip(lblCurrentY, "VD58");
                toolTip1.SetToolTip(txtCurrentY, "VD58");
                toolTip1.SetToolTip(lblCurrentZ, "VD54");
                toolTip1.SetToolTip(txtCurrentZ, "VD54");
                toolTip1.SetToolTip(lblTicketY, "VD76");
                toolTip1.SetToolTip(txtTicketY, "VD76");
                toolTip1.SetToolTip(lblTicketX, "VD72");
                toolTip1.SetToolTip(txtTicketX, "VD72");
                toolTip1.SetToolTip(lblFinishTicket, "VW18");
                toolTip1.SetToolTip(rdoNotFinish, "VW18=0");
                toolTip1.SetToolTip(rdoFinish, "VW18=1");
                toolTip1.SetToolTip(btnTicket, "VW70=1");
                toolTip1.SetToolTip(btnCut, "VW76=1");
                toolTip1.SetToolTip(lblSlaveMotorRatio, "VD40");
                toolTip1.SetToolTip(txtSlaveMotorRatio, "VD40");
                toolTip1.SetToolTip(lblSwingMotorRatio, "VD46");
                toolTip1.SetToolTip(txtSwingMotorRatio, "VD46");
                toolTip1.SetToolTip(lblTensionSpeed, "VD100");
                toolTip1.SetToolTip(txtTensionSpeed, "VD100");
                toolTip1.SetToolTip(lblWindingSpeed, "VD104");
                toolTip1.SetToolTip(txtWindingSpeed, "VD104");
                toolTip1.SetToolTip(lblTickedFallingHeight, "VD108");
                toolTip1.SetToolTip(txtTickedFallingHeight, "VD108");
                toolTip1.SetToolTip(lblBuzzer, "VW80");
                toolTip1.SetToolTip(pcbBuzzer, "VW80");
                toolTip1.SetToolTip(lblShieldSetting_sbqd, "V117.0");
                toolTip1.SetToolTip(pcbShieldSetting_sbqd, "V117.0");
                toolTip1.SetToolTip(lblShieldSetting_wbjc, "V117.5");
                toolTip1.SetToolTip(pcbShieldSetting_wbjc, "V117.5");
                toolTip1.SetToolTip(lblShieldSetting_sjsjwbjc, "V117.6");
                toolTip1.SetToolTip(pcbShieldSetting_sjsjwbjc, "V117.6");
                toolTip1.SetToolTip(lblVD170, "VD170");
                toolTip1.SetToolTip(txtVD170, "VD170");
                toolTip1.SetToolTip(lblVD174, "VD174");
                toolTip1.SetToolTip(txtVD174, "VD174");
                toolTip1.SetToolTip(lblVD178, "VD178");
                toolTip1.SetToolTip(txtVD178, "VD178");
                toolTip1.SetToolTip(lblVD182, "VD182");
                toolTip1.SetToolTip(txtVD182, "VD182");
                toolTip1.SetToolTip(lblVD186, "VD186");
                toolTip1.SetToolTip(txtVD186, "VD186");
                toolTip1.SetToolTip(lblVD190, "VD190");
                toolTip1.SetToolTip(txtVD190, "VD190");
                toolTip1.SetToolTip(lblVD194, "VD194");
                toolTip1.SetToolTip(txtVD194, "VD194");
                toolTip1.SetToolTip(lblVD198, "VD198");
                toolTip1.SetToolTip(txtVD198, "VD198");
                toolTip1.SetToolTip(lblOnePlusLength, "VD144");
                toolTip1.SetToolTip(txtOnePlusLength, "VD144");
                toolTip1.SetToolTip(txtClothWidth, "VD140");
            }
            else
            {
                menuLog.Visible = false;
                menuMonitor.Visible = false;
                menuManual.Visible = false;
                tiViewDetectLog.Visible = false;
                DeveloperToolStripMenuItem.Visible = false;
                txtSlaveMotorRatio.ReadOnly = true;
                txtSwingMotorRatio.ReadOnly = true;
                txtTensionSpeed.ReadOnly = true;
                txtWindingSpeed.ReadOnly = true;
                txtTickedFallingHeight.ReadOnly = true;
                lblVD170.Visible = false;
                lblVD174.Visible = false;
                lblVD178.Visible = false;
                lblVD182.Visible = false;
                lblVD186.Visible = false;
                lblVD190.Visible = false;
                lblVD194.Visible = false;
                lblVD198.Visible = false;
                txtVD170.Visible = false;
                txtVD174.Visible = false;
                txtVD178.Visible = false;
                txtVD182.Visible = false;
                txtVD186.Visible = false;
                txtVD190.Visible = false;
                txtVD194.Visible = false;
                txtVD198.Visible = false;
                txtVD170.ReadOnly = true;
                txtVD174.ReadOnly = true;
                txtVD178.ReadOnly = true;
                txtVD182.ReadOnly = true;
                txtVD186.ReadOnly = true;
                txtVD190.ReadOnly = true;
                txtVD194.ReadOnly = true;
                txtVD198.ReadOnly = true;
                pcbBuzzer.Enabled = false;
                lblShieldSetting_wbjc.Visible = false;
                pcbShieldSetting_wbjc.Visible = false;
                lblShieldSetting_sbqd.Visible = false;
                pcbShieldSetting_sbqd.Visible = false;
                pcbShieldSetting_sjsjwbjc.Enabled = false;
                txtFinalWeight.ReadOnly = true;
                lblAutoRefresh.Visible = false;
                pcbAutoRefresh.Visible = false;
                btnRefresh.Visible = false;
                lblOnePlusLength.Visible = false;
                txtOnePlusLength.Visible = false;
                lblScanRate.Visible = false;
                txtScanRate.Visible = false;
                tsmiLightTest.Visible = false;
                menuColourDifference.Visible = false;
            }

            #region
            txtFinalWeight.Tag = ModbusRegs.FinalWeight;
            txtMasterMotorSpeed.Tag = ModbusRegs.MasterMotorSpeed;
            txtWindingTensionSetValue.Tag = ModbusRegs.WindingTensionSetValue;
            rdoForwardMode.Tag = new object[] { ModbusRegs.ForwardBackMode, 0 };
            rdoBackMode.Tag = new object[] { ModbusRegs.ForwardBackMode, 1 };
            rdoBasicCloth.Tag = new object[] { ModbusRegs.TensionerMode, 0 };
            rdoTensionCloth.Tag = new object[] { ModbusRegs.TensionerMode, 1 };
            rbnPickAxlesA.Tag = new object[] { ModbusRegs.PickAxles, 0 };
            rbnPickAxlesB.Tag = new object[] { ModbusRegs.PickAxles, 1 };
            rbnReleaseAxlesA.Tag = new object[] { ModbusRegs.ReleaseAxles, 0 };
            rbnReleaseAxlesB.Tag = new object[] { ModbusRegs.ReleaseAxles, 1 };
            rbnProtectiveFilm_Close.Tag = new object[] { ModbusRegs.ProtectiveFilm, 0 };
            rbnProtectiveFilm_Open.Tag = new object[] { ModbusRegs.ProtectiveFilm, 1 };
            rbnBeiSi.Tag = new object[] { ModbusRegs.SlaveMotorWorkMode, 0 };
            rbnCloth.Tag = new object[] { ModbusRegs.SlaveMotorWorkMode, 1 };
            txtInspectionTensionSetValue.Tag = ModbusRegs.InspectionTensionSetValue;
            txtTicketY.Tag = ModbusRegs.TicketPositionY;
            txtTicketX.Tag = ModbusRegs.TicketPositionX;
            txtSlaveMotorRatio.Tag = ModbusRegs.SlaveMotorRatio;
            txtSwingMotorRatio.Tag = ModbusRegs.SwingMotorRatio;
            txtTensionSpeed.Tag = ModbusRegs.TensionSpeed;
            txtWindingSpeed.Tag = ModbusRegs.WindingSpeed;
            txtTickedFallingHeight.Tag = ModbusRegs.TicketFallingHeight;
            txtClothWidth.Tag = ModbusRegs.ClothWidthGetValue;
            txtVD170.Tag = (byte)85;
            txtVD174.Tag = (byte)87;
            txtVD178.Tag = (byte)89;
            txtVD182.Tag = (byte)91;
            txtVD186.Tag = (byte)93;
            txtVD190.Tag = (byte)95;
            txtVD194.Tag = (byte)97;
            txtVD198.Tag = (byte)99;
            txtOnePlusLength.Text = SystemConfig.GetConfigValues("OnePlusLength");
            txtScanRate.Text = SystemConfig.GetConfigValues("ScanRate");
            chkAutoLoad.Checked = SystemConfig.GetConfigValues("AutoLoadSpeedSetting") == "1";
            #endregion

            SetNonStandardUI();
        }

        /// <summary>
        /// 非标机型界面设置
        /// </summary>
        private void SetNonStandardUI()
        {
            lblRealWeight.Visible = true;
            txtRealWeight.Visible = true;
            lblFinalWeight.Visible = true;
            txtFinalWeight.Visible = true;
            lblWeijinCount.Location = new Point(lblWeijinCount.Location.X, lblWeijinCount.Location.Y - 74);
            txtWeijinCount.Location = new Point(txtWeijinCount.Location.X, txtWeijinCount.Location.Y - 74);

            lblVD170.Text = ClothOutSpeed;
            lblVD170.Text = FineTensionAdjustmentRange_Add;

            lblWindingSpeed.Text = LeatherLabel22;
            lblTensionMode.Visible = true;
            pnlTensionMode.Visible = true;
            lblPickAxles.Visible = true;
            pnlPickAxles.Visible = true;
            lblReleaseAxles.Visible = true;
            pnlReleaseAxles.Visible = true;
            lblProtectiveFilm.Visible = true;
            pnlProtectiveFilm.Visible = true;
            lblBSOrCloth.Visible = true;
            pnlBSOrCloth.Visible = true;
            lblWeijinCount.Visible = true;
            txtWeijinCount.Visible = true;
            btnCut.Visible = true;
            lbWasteLentgh.Visible = true;
            txtWasteLentgh.Visible = true;

            //设置控件对齐
            if (SystemConfig.GetConfigValues("Language") != "en")
            {
                SetControlRightAlign(lblSlaveMotorRatio, new Control[] { lblSwingMotorRatio, lblTensionSpeed, lblWindingSpeed, lblTickedFallingHeight });
                SetControlLeftAlign(txtSlaveMotorRatio, new Control[] { txtSwingMotorRatio, txtTensionSpeed, txtWindingSpeed, txtTickedFallingHeight });
                SetControlRightAlign(lblOnePlusLength, new Control[] { lblVD182, lblVD178, lblVD174, lblVD170 });
            }
            else
            {
                SetControlLeftAlign(lblSlaveMotorRatio, new Control[] { lblSwingMotorRatio, lblTensionSpeed, lblWindingSpeed, lblTickedFallingHeight });
                SetControlLeftAlign(txtSlaveMotorRatio, new Control[] { txtSwingMotorRatio, txtTensionSpeed, txtWindingSpeed, txtTickedFallingHeight });
                SetControlRightAlign(lblVD182, new Control[] { lblVD178, lblVD174, lblVD170 });
                SetControlRightAlign(lblVD198, new Control[] { lblVD194, lblVD190, lblVD186 });
            }
        }

        /// <summary>
        /// 读取PLC数据，显示在UI控件上
        /// </summary>
        private void SetControlText()
        {
            modbusStatus.SetValues(modbusValues);
            ModbusStatus states = modbusStatus;

            //机器状态
            if (lengthUnit == "m")
            {
                btnLength.Text = states.EncoderLength.ToString("f2") + "  ";
            }
            else
            {
                btnLength.Text = (states.EncoderLength / 0.9144).ToString("f2") + "  ";
            }
            if (speedUnit == "m/s")
            {
                btnSpeed.Text = states.MainSpeed.ToString("f2") + "   ";
            }
            else if (speedUnit == "m/min")
            {
                btnSpeed.Text = (states.MainSpeed * 60).ToString("f2") + "   ";
            }
            else if (speedUnit == "km/h")
            {
                btnSpeed.Text = (states.MainSpeed * 3.6).ToString("f2") + "   ";
            }
            else if (speedUnit == "yd/s")
            {
                btnSpeed.Text = (states.MainSpeed / 0.9144).ToString("f2") + "   ";
            }
            else if (speedUnit == "yd/min")
            {
                btnSpeed.Text = (states.MainSpeed * 60 / 0.9144).ToString("f2") + "   ";
            }
            else
            {
                btnSpeed.Text = (states.MainSpeed * 3.6 / 0.9144).ToString("f2") + "   ";
            }

            if (states.DeviceRunning == 1)
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            else
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }

            List<string> errorlist = new List<string>(states.GetAllAlarmList());
            uC_Alarm21.SetErrors(errorlist);

            //实时状态
            txtEncoder.Text = states.Encoder.ToString();
            txtCurrentY2.Text = states.CurrentPositionY.ToString("f3");
            txtMainSpeed.Text = states.MasterMotorSpeed.ToString("f3");
            txtEncoderSpeed.Text = states.MainSpeed.ToString("f3");
            txtWindingTensionGetValue.Text = states.WindingTensionGetValue.ToString("f3");
            txtInspectionTensionGetValue.Text = states.InspectionTensionGetValue.ToString("f3");
            txtRealWeight.Text = states.RealTimeWeight.ToString("f3");
            if (!txtFinalWeight.Focused)
            {
                txtFinalWeight.Text = states.FinalWeight.ToString("f3");
            }
            txtWeijinCount.Text = states.WeijinCount.ToString();
            txtWasteLentgh.Text = states.WasteEncoderValue.ToString();//没有编码器系数

            //普通参数设置
            if (!txtMasterMotorSpeed.Focused)
            {
                txtMasterMotorSpeed.Text = states.MasterMotorSpeed.ToString("f3");
            }
            if (!txtWindingTensionSetValue.Focused)
            {
                txtWindingTensionSetValue.Text = states.WindingTensionSetValue.ToString("f3");
            }
            if (!txtInspectionTensionSetValue.Focused)
            {
                txtInspectionTensionSetValue.Text = states.InspectionTensionSetValue.ToString("f3");
            }
            if (states.ForwardBackMode == 0)
            {
                rdoForwardMode.Checked = true;
            }
            else
            {
                rdoBackMode.Checked = true;
            }
            if (states.TensionerMode == 0)
            {
                rdoBasicCloth.Checked = true;
            }
            else
            {
                rdoTensionCloth.Checked = true;
            }
            //收卷轴
            if (states.PickAxles == 0)
            {
                rbnPickAxlesA.Checked = true;
            }
            else
            {
                rbnPickAxlesB.Checked = true;
            }
            //放卷轴
            if (states.ReleaseAxles == 0)
            {
                rbnReleaseAxlesA.Checked = true;
            }
            else
            {
                rbnReleaseAxlesB.Checked = true;
            }
            //保护膜
            if (states.ProtectiveFilm == 0)
            {
                rbnProtectiveFilm_Close.Checked = true;
            }
            else
            {
                rbnProtectiveFilm_Open.Checked = true;
            }
            //贝斯或布
            if (states.SlaveMotorWorkMode == 0)
            {
                rbnBeiSi.Checked = true;
            }
            else
            {
                rbnCloth.Checked = true;
            }
            txtClothWidth.Text = states.ClothWidthGetValue.ToString("f3");
            //贴标调试
            txtCurrentX.Text = states.CurrentPositionX.ToString("f3");
            txtCurrentY.Text = states.CurrentPositionY.ToString("f3");
            txtCurrentZ.Text = states.CurrentPositionZ.ToString("f3");
            if (!txtTicketY.Focused)
            {
                txtTicketY.Text = states.TicketPositionY.ToString("f3");
            }
            if (!txtTicketX.Focused)
            {
                txtTicketX.Text = states.TicketPositionX.ToString("f3");
            }
            if (states.TicketFinish == 1)
            {
                rdoFinish.Checked = true;
                btnTicket.Enabled = true;
                //有批量贴标任务，下发贴标坐标                
                if (queBatchTicketPoints.Count > 0 && !IsEditBatchPoints && LastPointHastTryTicket)
                {
                    btnTicket.Enabled = false;//有批量贴标任务则禁止手动单个贴标
                    PointF ticketPoint;
                    if (queBatchTicketPoints.TryDequeue(out ticketPoint))
                    {
                        txtTicketX.Text = ticketPoint.X.ToString();
                        textBoxFloat_KeyUp(txtTicketX, new KeyEventArgs(Keys.Enter));
                        if (chkAreaTicket.Checked)//区域贴标
                        {
                            ModbusEnhanced.WriteFloatHoldingRegisters(1, ModbusRegs.AreaTickeY, new float[1] { ticketPoint.Y });
                        }
                        else//不停机贴标
                        {
                            txtTicketY.Text = ticketPoint.Y.ToString();
                            textBoxFloat_KeyUp(txtTicketY, new KeyEventArgs(Keys.Enter));
                        }
                        btnTicket_Click(null, null);//通知贴标
                        LastPointHastTryTicket = false;
                    }
                }
                pcbAutoRefresh.Enabled = queBatchTicketPoints.Count == 0;//自动刷新
            }
            else
            {
                rdoNotFinish.Checked = true;
                btnTicket.Enabled = false;
                LastPointHastTryTicket = true;
            }

            //系统参数
            if (!txtSlaveMotorRatio.Focused)
            {
                txtSlaveMotorRatio.Text = states.SlaveMotorRatio.ToString("f3");
            }
            if (!txtSwingMotorRatio.Focused)
            {
                txtSwingMotorRatio.Text = states.SwingMotorRatio.ToString("f3");
            }
            if (!txtTensionSpeed.Focused)
            {
                txtTensionSpeed.Text = states.TensionSpeed.ToString("f3");
            }
            if (!txtWindingSpeed.Focused)
            {
                txtWindingSpeed.Text = states.WindingSpeed.ToString("f3");
            }
            if (!txtTickedFallingHeight.Focused)
            {
                txtTickedFallingHeight.Text = states.TicketFallingHeight.ToString("f3");
            }
            if (!txtVD170.Focused)
            {
                txtVD170.Text = states.VD170.ToString("f3");
            }
            if (!txtVD174.Focused)
            {
                txtVD174.Text = states.VD174.ToString("f3");
            }
            if (!txtVD178.Focused)
            {
                txtVD178.Text = states.VD178.ToString("f3");
            }
            if (!txtVD182.Focused)
            {
                txtVD182.Text = states.VD182.ToString("f3");
            }
            if (!txtVD186.Focused)
            {
                txtVD186.Text = states.VD186.ToString("f3");
            }
            if (!txtVD190.Focused)
            {
                txtVD190.Text = states.VD190.ToString("f3");
            }
            if (!txtVD194.Focused)
            {
                txtVD194.Text = states.VD194.ToString("f3");
            }
            if (!txtVD198.Focused)
            {
                txtVD198.Text = states.VD198.ToString("f3");
            }
            if (!txtOnePlusLength.Focused)
            {
                txtOnePlusLength.Text = ModbusStatus.OnePlusLength.ToString("f3");
            }
            //底部亮灯颜色
            if (states.IndicatorLight_green)
            {
                statusLight.BackColor = Color.Lime;
            }
            else if (states.IndictorLight_red)
            {
                statusLight.BackColor = Color.Red;
            }
            else if (states.IndicatorLight_yellow)
            {
                statusLight.BackColor = Color.Yellow;
            }
            else
            {
                statusLight.BackColor = Color.Gray;
            }
        }
        /// <summary>
        /// 设置给定控件的X坐标按模板控件的X坐标右对齐
        /// </summary>
        /// <param name="ctlTemplate"></param>
        /// <param name="ctlForSet"></param>
        private void SetControlRightAlign(Control ctlTemplate, Control ctlForSet)
        {
            ctlForSet.Location = new Point(ctlTemplate.Location.X + ctlTemplate.Width - ctlForSet.Width, ctlForSet.Location.Y);
        }
        /// <summary>
        /// 设置给定列表中控件的X坐标按模板控件的X坐标右对齐
        /// </summary>
        /// <param name="ctlTemplate"></param>
        /// <param name="ctlForSets"></param>
        private void SetControlRightAlign(Control ctlTemplate, Control[] ctlForSets)
        {
            foreach (Control ctlForSet in ctlForSets)
            {
                SetControlRightAlign(ctlTemplate, ctlForSet);
            }
        }
        /// <summary>
        /// 设置给定控件的X坐标按模板控件的X坐标左对齐
        /// </summary>
        /// <param name="ctlTemplate"></param>
        /// <param name="ctlForSet"></param>
        private void SetControlLeftAlign(Control ctlTemplate, Control ctlForSet) => ctlForSet.Location = new Point(ctlTemplate.Location.X, ctlForSet.Location.Y);
        /// <summary>
        /// 设置给定列表中控件的X坐标按模板控件的X坐标左对齐
        /// </summary>
        /// <param name="ctlTemplate"></param>
        /// <param name="ctlForSets"></param>
        private void SetControlLeftAlign(Control ctlTemplate, Control[] ctlForSets)
        {
            foreach (Control ctlForSet in ctlForSets)
            {
                SetControlLeftAlign(ctlTemplate, ctlForSet);
            }
        }
        /// <summary>
        /// 递归加载资源
        /// </summary>
        /// <param name="ctrl"></param>
        private void LoadChildControlResource(Control ctrl)
        {
            ComponentResourceManager rm = new ComponentResourceManager(typeof(FormMain));
            foreach (Control child in ctrl.Controls)
            {
                rm.ApplyResources(child, child.Name);
                if (!(child is UserControl))
                {
                    LoadChildControlResource(child);
                }
            }
        }
        /// <summary>
        /// 重新加载菜单源
        /// </summary>
        /// <param name="menu"></param>
        private void ReloadMenuResource(MenuStrip menu)
        {
            ComponentResourceManager rm = new ComponentResourceManager(typeof(FormMain));
            rm.ApplyResources(menu, menu.Name);
            foreach (ToolStripMenuItem item in menu.Items)
            {
                LoadChildMenuResource(item);
            }
        }
        /// <summary>
        /// 递归加载菜单源
        /// </summary>
        /// <param name="menu"></param>
        private void LoadChildMenuResource(ToolStripMenuItem menu)
        {
            ComponentResourceManager rm = new ComponentResourceManager(typeof(FormMain));
            rm.ApplyResources(menu, menu.Name);
            foreach (ToolStripMenuItem item in menu.DropDownItems)
            {
                LoadChildMenuResource(item);
            }
        }
        #endregion

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text += $"AngkorW专用-PLC-Dashboard调试版本-{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
            titleControl1.MaximizeBox = MaximizeBox;
            titleControl1.MinimizeBox = MinimizeBox;
            SetMenu();
            base.Init();
            string language = Thread.CurrentThread.CurrentUICulture.Name;
            string filename = "Registers." + language + ".ini";
            string settingfilename = Path.Combine("/", filename);
            if (!File.Exists(settingfilename))
            {
                filename = "Registers.ini";
                settingfilename = Path.Combine("/", filename);
            }
            if (File.Exists(settingfilename))
            {
                PLCLog.LoadRegisters(settingfilename);
            }
            maxSpeed = Convert.ToSingle("0.538");
            btnSave.Visible = true;
            btnLoad.Visible = true;
            chkAutoLoad.Visible = true;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            debugcmd += (char)e.KeyCode;
            if (debugcmd.Length >= 3 && debugcmd.Substring(debugcmd.Length - 3).ToLower() == openmonitorcmd)
            {
                debugcmd = "";
                ShowForm<FormMsg>();
            }
            else if (debugcmd.Length >= 3 && debugcmd.Substring(debugcmd.Length - 3).ToLower() == opensettingscmd)
            {
                debugcmd = "";
                if (1 > 0)
                {
                    ShowForm<FormSetting>();
                }
            }
            else if (debugcmd.Length >= 3 && debugcmd.Substring(debugcmd.Length - 3).ToLower() == openclosereadcmd)
            {
                debugcmd = "";
            }
        }

        private void textBoxInt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ushort value = Convert.ToUInt16(((TextBox)sender).Text.Trim());
                Master.WriteSingleRegister(1, (ushort)((TextBox)sender).Tag, value);
                ((TextBox)sender).Text = value.ToString();

                ((TextBox)sender).ForeColor = Color.Lime;
            }
            else
            {
                ((TextBox)sender).ForeColor = Color.Black;
            }
        }
    }
}
