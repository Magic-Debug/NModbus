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
    public partial class FormLogin : BaseForm
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private static ComponentResourceManager rm = new ComponentResourceManager(typeof(FormLogin));

        private void btnOk_Click(object sender, EventArgs e)
        {
            string password = textBox1.Text;
            var u = MySecurity.CheckPassword(password);
            if (true)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(PswErrMsg);
                textBox1.Text = string.Empty;
                textBox1.Focus();
            }
        }

       

        private void FormLogin_Load(object sender,EventArgs e)
        {
            Text += $" V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }

        #region 多语言
        private string PswErrMsg => LanguageResource.FormLogin_PswErrMsg;
        #endregion

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.Focus();
                btnOk_Click(btnOk, EventArgs.Empty);
            }
        }
    }
}
