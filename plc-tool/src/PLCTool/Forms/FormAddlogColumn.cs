using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using MainFrom;

namespace PLCTool
{
    public partial class FormAddlogColumn : BaseForm
    {
        private int Index = -1;
        private static ComponentResourceManager rm = new ComponentResourceManager(typeof(FormAddlogColumn));
        private string SettingFilename;
        public FormAddlogColumn()
        {
            InitializeComponent();
        }

        public FormAddlogColumn(PLCRegister r,int index)
        {
            InitializeComponent();
            txtName.Text = r.Name;
            cbbDataType.SelectedIndex = (int)r.DataType - 1;
            txtAddress.Text = r.Address.ToString();
            chkIsShow.Checked = r.Visibel;
            Index = index;
        }

        public void FormAddlogColumn_Load(object sender, EventArgs e)
        {
            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            string filename = "Registers." + language + ".ini";
            SettingFilename = Path.Combine(Common.GetInstance().ConfigDirectory, filename);
            if (Index == -1)
            {
                Text = AddLogColumnTitle;
            }
            else
            {
                Text = EditLogColumnTitle;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckName())
            {
                MessageBox.Show(ExistNameMsg);//"名称已存在！"
                return;
            }
            if (cbbDataType.SelectedIndex == -1)
            {
                MessageBox.Show(SelectDataTypeMsg);//"请选择数据类型！"
                return;
            }
            int address = 0;
            if (cbbDataType.SelectedIndex == 6)
            {
                address = ModbusRegs.Alarm;
            }
            else if (cbbDataType.SelectedIndex == 7)
            {
                address = ModbusRegs.TicketAlarm;
            }
            else
            {
                string s_address = txtAddress.Text.Trim();
                try
                {
                    address = Convert.ToInt32(s_address);
                }
                catch
                {
                    MessageBox.Show(AddressIncorrectMsg);//"地址格式不正确"
                    return;
                }
            }
            PLCRegister register = new PLCRegister();
            register.Name = txtName.Text.Trim();
            register.DataType = (PLCDataType)(cbbDataType.SelectedIndex + 1);
            register.Address = address;
            register.Visibel = chkIsShow.Checked;
            if (Index == -1)
            {
                PLCLog.Registers.Add(register);
            }
            else
            {
                PLCLog.Registers[Index] = register;
            }
            PLCLog.SaveRegisters(SettingFilename);
            DialogResult = DialogResult.OK;
        }

        private bool CheckName()
        {
            string name = txtName.Text.Trim();
            for (int i = 0; i < PLCLog.Registers.Count; i++)
            {
                if (name == PLCLog.Registers[i].Name && i != Index)
                {
                    return false;
                }
            }
            return true;
        }

        #region 多语言
        private string AddLogColumnTitle => LanguageResource.FormAddLogColumn_AddLogColumnTitle;
        private string AddressIncorrectMsg => LanguageResource.FormAddLogColumn_AddressIncorrectMsg;
        private string EditLogColumnTitle => LanguageResource.FormAddLogColumn_EditLogColumnTitle;
        private string ExistNameMsg => LanguageResource.FormAddLogColumn_ExistNameMsg;
        private string SelectDataTypeMsg => LanguageResource.FormAddLogColumn_SelectDataTypeMsg;
        #endregion

        private void cbbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDataType.SelectedIndex == 6|| cbbDataType.SelectedIndex == 7)
            {
                txtAddress.Enabled = false;
            }
            else
            {
                txtAddress.Enabled = true;
            }
        }
    }
}
