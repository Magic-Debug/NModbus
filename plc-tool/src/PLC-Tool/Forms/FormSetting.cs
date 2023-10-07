using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using MainFrom;

namespace PLCTool
{
    public partial class FormSetting : BaseForm
    {
        public FormSetting()
        {
            InitializeComponent();
        }

        private void textBoxFloat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                ((TextBox)sender).ForeColor = Color.Black;
                return;
            }

            try
            {
                float value = Convert.ToSingle(((TextBox)sender).Text.Trim());
                TcpCommunication.GetInstance().WriteRegister<float>(ModbusRegs.TensionerMode, value * 1000);

                ((TextBox)sender).Text = value.ToString();
                ((TextBox)sender).ForeColor = Color.Lime;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBoxFloat_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).ForeColor = Color.Black;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfig();
                MessageBox.Show("保存成功，请重新运行软件。");
                Environment.Exit(0);
            }
            catch(Exception ex)
            {
                FrameworkCommon.LogHelper.Default.Error(ex);
                MessageBox.Show("保存失败");
            }
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            LoadSetting();
            SetMenu();
        }

        private void LoadSetting()
        {
            comboBox1.SelectedIndex = Convert.ToInt32(SystemConfig.GetConfigValues("MachineType"));
            chkShowWeight.Checked = SystemConfig.GetConfigValues("ShowWeight") == "1";
            chkIsClothOutShelf.Checked = SystemConfig.GetConfigValues("IsClothOutShelf") == "1";
            chkLeather.Checked = SystemConfig.GetConfigValues("IsLeather") == "1";
            chkWeijin.Checked = SystemConfig.GetConfigValues("IsWeijin") == "1";
            txtIP.Text = SystemConfig.GetConfigValues("IP");
            txtPort1.Text = SystemConfig.GetConfigValues("Port");
            chkAutoLog.Checked = SystemConfig.GetConfigValues("AutoLog") == "1";
            chkCheckError.Checked = SystemConfig.GetConfigValues("CheckError") == "1";
            txtScanRate.Text = SystemConfig.GetConfigValues("ScanRate");
            txtOnePlusLength.Text = SystemConfig.GetConfigValues("OnePlusLength");
            txtMaxSpeed.Text = SystemConfig.GetConfigValues("MaxSpeed");
            txtDoubleLine.Text = (Common.GetInstance().modbusStatus.DoubleLine / 1000).ToString();
        }

        private void SetMenu()
        {
            if(Common.GetInstance().IsSuperAdmin)
            {
                txtIP.ReadOnly = false;
                txtPort1.ReadOnly = false;
                chkAutoLog.Enabled = true;
                chkCheckError.Enabled = true;
                txtScanRate.ReadOnly = false;
            }
            else
            {
                txtIP.ReadOnly = true;
                txtPort1.ReadOnly = true;
                chkAutoLog.Enabled = false;
                chkCheckError.Enabled = false;
                txtScanRate.ReadOnly = true;
            }
            label2.Visible = label3.Visible = txtOnePlusLength.Visible = false;
        }

        private void SaveConfig()
        {
            List<KeyValuePair<string, string>> KeyValuePairs = new List<KeyValuePair<string, string>>();
            KeyValuePairs.Add(new KeyValuePair<string, string>("MachineType", comboBox1.SelectedIndex.ToString()));
            KeyValuePairs.Add(new KeyValuePair<string, string>("ShowWeight", chkShowWeight.Checked ? "1" : "0"));
            KeyValuePairs.Add(new KeyValuePair<string, string>("IsClothOutShelf", chkIsClothOutShelf.Checked ? "1" : "0"));
            KeyValuePairs.Add(new KeyValuePair<string, string>("IsLeather", chkLeather.Checked ? "1" : "0"));
            KeyValuePairs.Add(new KeyValuePair<string, string>("IsWeijin", chkWeijin.Checked ? "1" : "0"));
            KeyValuePairs.Add(new KeyValuePair<string, string>("IP", txtIP.Text));
            KeyValuePairs.Add(new KeyValuePair<string, string>("Port", txtPort1.Text));
            KeyValuePairs.Add(new KeyValuePair<string, string>("AutoLog", chkAutoLog.Checked ? "1" : "0"));
            KeyValuePairs.Add(new KeyValuePair<string, string>("CheckError", chkCheckError.Checked ? "1" : "0"));
            KeyValuePairs.Add(new KeyValuePair<string, string>("ScanRate", txtScanRate.Text));
            KeyValuePairs.Add(new KeyValuePair<string, string>("OnePlusLength", txtOnePlusLength.Text));
            KeyValuePairs.Add(new KeyValuePair<string, string>("MaxSpeed", txtMaxSpeed.Text));
            SystemConfig.SaveConfigValues(KeyValuePairs);
        }
    }
}
