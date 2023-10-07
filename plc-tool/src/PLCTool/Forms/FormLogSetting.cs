using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace PLCTool
{
    public partial class FormLogSetting : BaseForm
    {
        public FormLogSetting()
        {
            InitializeComponent();
        }

        private static ComponentResourceManager rm = new ComponentResourceManager(typeof(FormLogSetting));
        private string SettingFilename;

        private void FormLogSetting_Load(object sender, EventArgs e)
        {
            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            string filename = "Registers." + language + ".ini";         
            SettingFilename = Path.Combine(Common.GetInstance().ConfigDirectory, filename);
            //编辑表格列的功能只对超级管理员显示
            //SetMenu();
            BindData();
        }

        private void BindData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = PLCLog.Registers;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            if (index > 0)
            {
                PLCRegister r = PLCLog.Registers[index];
                PLCLog.Registers.RemoveAt(index);
                PLCLog.Registers.Insert(index - 1, r);
                PLCLog.SaveRegisters(SettingFilename);
                BindData();
                dataGridView1.Rows[index - 1].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = index - 1;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            if(index<PLCLog.Registers.Count-1)
            {
                PLCRegister r = PLCLog.Registers[index];
                PLCLog.Registers.RemoveAt(index);
                PLCLog.Registers.Insert(index + 1, r);
                PLCLog.SaveRegisters(SettingFilename);
                BindData();
                dataGridView1.Rows[index + 1].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = index + 1;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAddlogColumn form = new FormAddlogColumn();
            if (form.ShowDialog() == DialogResult.OK)
            {
                BindData();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(NotSelectEditRowMsg);//"请选择需要编辑的行"
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            PLCRegister r = PLCLog.Registers[index];
            FormAddlogColumn form = new FormAddlogColumn(r, index);
            if (form.ShowDialog() == DialogResult.OK)
            {
                BindData();
                dataGridView1.Rows[index].Selected = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(NotSelectDeleteRowMsg);//"请选择需要删除的行"
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            PLCRegister r = PLCLog.Registers[index];
            if (MessageBox.Show(string.Format(DeleteRowMsg,r.Name) , DeleteColumn, MessageBoxButtons.YesNo) == DialogResult.Yes)//"确定要删除{r.Name}吗？", "删除列"
            {
                PLCLog.Registers.RemoveAt(index);
                BindData();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < PLCLog.Registers.Count && e.ColumnIndex == 0)
            {
                PLCRegister r = PLCLog.Registers[e.RowIndex];
                r.Visibel = !r.Visibel;
                PLCLog.Registers[e.RowIndex] = r;
                PLCLog.SaveRegisters(SettingFilename);
            }
        }

        private void SetMenu()
        {
            if(Common.GetInstance().IsSuperAdmin)
            {
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        #region 多语言
        private string DeleteColumn => LanguageResource.FormLogSetting_DeleteColumn;
        private string DeleteRowMsg => LanguageResource.FormLogSetting_DeleteRowMsg;
        private string NotSelectDeleteRowMsg => LanguageResource.FormLogSetting_NotSelectDeleteRowMsg;
        private string NotSelectEditRowMsg => LanguageResource.FormLogSetting_NotSelectEditRowMsg;
        #endregion
    }
}
