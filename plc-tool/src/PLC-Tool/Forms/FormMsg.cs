using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MainFrom;

namespace PLCTool
{
    public partial class FormMsg : BaseForm
    {
        #region 多语言
        private string CloseMonitorInfo => LanguageResource.FormMsg_CloseMonitorInfo;
        private string CloseMonitorMsg => LanguageResource.FormMsg_CloseMonitorMsg;
        private string FormatIncorrectMsg => LanguageResource.FormMsg_FormatIncorrectMsg;
        private string NotConnectedErrMsg => LanguageResource.FormMsg_NotConnectedErrMsg;
        private string OpenMonitorInfo => LanguageResource.FormMsg_OpenMonitorInfo;
        private string OpenMonitorMsg => LanguageResource.FormMsg_OpenMonitorMsg;
        private string ReceiveFormat => LanguageResource.FormMsg_ReceiveFormat;
        private string SendFormat => LanguageResource.FormMsg_SendFormat;
        private string StartCounterInfo => LanguageResource.FormMsg_StartCounterInfo;
        private string StartReadingMsg => LanguageResource.FormMsg_StartReadingMsg;
        private string StopCounterInfo => LanguageResource.FormMsg_StopCounterInfo;
        private string StopReadingMsg => LanguageResource.FormMsg_StopReadingMsg;
        private string StopReadingMsg2 => LanguageResource.FormMsg_StopReadingMsg2;
        #endregion               

        TcpCommunication communication = TcpCommunication.GetInstance();
        bool IsMonitor = true;
        bool AutoCount = true;
        bool checkerror;
        int SendCount = 0;
        int ReceiveCount = 0;
        int ErrorCount = 0;

        
        List<ushort> FilterRegisterAddress = new List<ushort>();       

        public FormMsg()
        {
            InitializeComponent();
        }
        
        private void FormMsg_Load(object sender, EventArgs e)
        {
            txtSendCommand.Visible = Common.GetInstance().IsSuperAdmin;
            btnSend.Visible = Common.GetInstance().IsSuperAdmin;
            chkSplitPackage.Visible = Common.GetInstance().IsSuperAdmin;
            checkerror = SystemConfig.GetConfigValues("CheckError") != "0";
            chkDisplaySent_CheckedChanged(null, null);
            chkDisplayReceived_CheckedChanged(null, null);            
            rtbMsg.Select(rtbMsg.TextLength, 0);
            rtbMsg.SelectionColor = Color.Red;
            rtbMsg.AppendText(OpenMonitorMsg);//已开启监听\n\n
        }

        private void SendedData(CommunicationEventArgs e)
        {
            if (!IsMonitor)
            {
                return;
            }

            //自动计数
            if (AutoCount)
            {
                SendCount++;
            }

            //显示数据
            FilterAndDisplayData(e, Color.Green, true);
        }
        private void ReceivedData(CommunicationEventArgs e)
        {
            if (!IsMonitor)
            {
                return;
            }

            List<byte[]> list = chkSplitPackage.Checked ? ConvertHelper.SplitData(e.Data) : new List<byte[]>() { e.Data };
            Color SelectionColor;
            foreach (byte[] k in list)
            {
                if (AutoCount)
                {
                    ReceiveCount++;
                }
                
                if (k.Length > 7 && k[7] < 0x80)
                {
                    SelectionColor = Color.Blue;
                }
                else
                {
                    SelectionColor = Color.Red;
                    if (AutoCount)
                    {
                        ErrorCount++;
                    }
                }

                FilterAndDisplayData(e, SelectionColor, false);
            }
        }
       
        private void FilterAndDisplayData(CommunicationEventArgs e, Color selectionColor, bool isSend)
        {
            if (InvokeRequired)
            {                
                this.Invoke(new Action(() => { FilterAndDisplayData(e, selectionColor, isSend); }));
                return;
            }
            //地址过滤
            if (chkDisplaySpecialAddress.Checked)
            {
                byte[] addressBytes = new byte[2];
                Array.Copy(e.Data, 8, addressBytes, 0, addressBytes.Length);
                addressBytes = addressBytes.Reverse();
                ushort tempAddress = BitConverter.ToUInt16(addressBytes, 0);
                if (!FilterRegisterAddress.Contains(tempAddress))
                    return;
            }

            rtbMsg.Select(rtbMsg.TextLength, 0);
            rtbMsg.SelectionColor = Color.Black;
            rtbMsg.AppendText(string.Format(isSend ? SendFormat : ReceiveFormat, e.Time.ToString("HH:mm:ss.fff"))); 
            rtbMsg.Select(rtbMsg.TextLength, 0);
            rtbMsg.SelectionColor = selectionColor;
            rtbMsg.AppendText(string.Format("{0}\n\n", e.Data.BytesToString()));

            //刷新计数
            txtSend.Text = SendCount.ToString();
            txtReceive.Text = ReceiveCount.ToString();
            txtError.Text = ErrorCount.ToString();
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbMsg.Clear();
        }

        private void FormMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            communication.Sended -= SendedData;
            communication.Received -= ReceivedData;            
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            IsMonitor = !IsMonitor;
            if (IsMonitor)
            {
                btnMoniter.Text = CloseMonitorInfo;
                rtbMsg.Select(rtbMsg.TextLength, 0);
                rtbMsg.SelectionColor = Color.Red;
                rtbMsg.AppendText(OpenMonitorMsg);
            }
            else
            {
                btnMoniter.Text = OpenMonitorInfo;
                rtbMsg.Select(rtbMsg.TextLength, 0);
                rtbMsg.SelectionColor = Color.Red;
                rtbMsg.AppendText(CloseMonitorMsg);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SendCount = 0;
            ReceiveCount = 0;
            ErrorCount = 0;

            txtSend.Text = SendCount.ToString();
            txtReceive.Text = ReceiveCount.ToString();
            txtError.Text = ErrorCount.ToString();
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            AutoCount = !AutoCount;
            btnCount.Text = AutoCount ? StopCounterInfo : StartCounterInfo;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            txtSendCommand.Text = GetConvertSendString(txtSendCommand.Text);
            if (txtSendCommand.Text == "")
            {
                return;
            }
            if (!communication.IsConnected)
            {
                rtbMsg.Select(rtbMsg.TextLength, 0);
                rtbMsg.SelectionColor = Color.Red;
                rtbMsg.AppendText(NotConnectedErrMsg);//"未连接，无法发送命令。\n\n"
            }
            string s = txtSendCommand.Text;
            if (s[0] == 'v' || s[0] == 'V')
            {
                if(communication.CheckCommand(s))
                {
                    byte[] data = communication.GetCommand(s);
                    communication.SendData(data);
                }
                else
                {
                    rtbMsg.Select(rtbMsg.TextLength, 0);
                    rtbMsg.SelectionColor = Color.Purple;
                    rtbMsg.AppendText(FormatIncorrectMsg);//"指令格式错误，未发送。\n\n"
                }
            }
            else
            {
                string[] a = s.Split(' ');
                byte[] data = new byte[a.Length];
                for (int i = 0; i < a.Length; i++)
                {
                    data[i] = Convert.ToByte(a[i], 16);
                }
                if ((data.Length < 6 || data.Length != data[5] + 6) && checkerror)
                {
                    rtbMsg.Select(rtbMsg.TextLength, 0);
                    rtbMsg.SelectionColor = Color.Purple;
                    rtbMsg.AppendText(FormatIncorrectMsg);//"指令格式错误，未发送。\n\n"
                }
                else
                {
                    communication.SendData(data);
                }
            }
        }

        private void txtSendCommand_TextChanged(object sender, EventArgs e)
        {
            if (txtSendCommand.SelectionStart == txtSendCommand.Text.Length)
            {
                txtSendCommand.Text = GetConvertSendString(txtSendCommand.Text);
                txtSendCommand.SelectionStart = txtSendCommand.Text.Length;
            }
        }

        private string GetConvertSendString(string s)
        {
            if (s.Length > 0 && (s[0] == 'v' || s[0] == 'V'))
                return s;
            Regex reg1 = new Regex(@"[^0-9a-fA-F]");
            Regex reg2 = new Regex(@"[0-9a-fA-F]{2}");
            s = reg1.Replace(s, "");
            s = reg2.Replace(s, (a) => a + " ");
            s = s.Trim();
            return s;
        }

        private void txtSendCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(btnSend, EventArgs.Empty);
            }
        }

        private void chkDisplaySent_CheckedChanged(object sender, EventArgs e)
        {
            if(chkDisplaySent.Checked)
                communication.Sended += SendedData;
            else
                communication.Sended -= SendedData;
        }

        private void chkDisplayReceived_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisplayReceived.Checked)
                communication.Received += ReceivedData;
            else
                communication.Received -= ReceivedData;
        }

        private void txtDisplaySpecialAddress_TextChanged(object sender, EventArgs e)
        {
            FilterRegisterAddress.Clear();

            string[] AddressStrs = txtDisplaySpecialAddress.Text.Split(new string[] { "0x", " ", ",", ";", "，", "；" }, StringSplitOptions.RemoveEmptyEntries);
            ushort RegisterAddress;
            foreach (string AddressStr in AddressStrs)
            {
                try
                {
                    RegisterAddress = Convert.ToUInt16(AddressStr, 16);
                    FilterRegisterAddress.Add(RegisterAddress);
                }
                catch
                {
                    continue;
                }                
            }
        }
    }
}
