using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTool
{
    public partial class FormChangePassword : BaseForm
    {
        public FormChangePassword()
        {
            InitializeComponent();
        }
        private static ComponentResourceManager rm = new ComponentResourceManager(typeof(FormChangePassword));

        private void button1_Click(object sender, EventArgs e)
        {
            string oldpassword = txtOldPassword.Text;
            string newpassword1 = txtNewPassword1.Text;
            string newpassword2 = txtNewPassword2.Text;
            if (newpassword1 == "" || newpassword2 == "")
            {
                MessageBox.Show(NewPswEmptyMsg);//"新密码不能为空！"
                return;
            }
            else if (newpassword1 != newpassword2)
            {
                MessageBox.Show(PswNotMatchMsg);//"两次输入的密码不一致，请重新输入！"
                return;
            }
            else if (!MySecurity.CheckPassword(oldpassword))
            {
                MessageBox.Show(PswIncorrectMsg);//"原密码不正确！"
                return;
            }
            else
            {
                MySecurity.ChangePassword(newpassword1);
                MessageBox.Show(ChangePswSuccessMsg);//"密码修改成功，请牢记新密码！"
                Close();
            }
        }

        private void txt_KeyUP(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender.Equals(txtOldPassword))
                {
                    if(txtOldPassword.Text != "")
                    txtNewPassword1.Focus();
                }
                else if (sender.Equals(txtNewPassword1))
                {
                    if (txtNewPassword1.Text != "")
                        txtNewPassword2.Focus();
                }
                else
                    button1_Click(btnOK, EventArgs.Empty);
            }
        }

        #region 多语言
        private string ChangePswSuccessMsg => LanguageResource.FormChangePassword_ChangePswSuccessMsg;
        private string NewPswEmptyMsg => LanguageResource.FormChangePassword_NewPswEmptyMsg;
        private string PswIncorrectMsg => LanguageResource.FormChangePassword_PswIncorrectMsg;
        private string PswNotMatchMsg => LanguageResource.FormChangePassword_PswNotMatchMsg;
        #endregion
    }
}
