using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCTool.Inspection;

namespace PLCTool.Forms
{
    public partial class FormInspectionLog : BaseForm
    {
        public FormInspectionLog()
        {
            InitializeComponent();
        }

        InspectionLog.InspectionLogLine[] LogLines = new InspectionLog.InspectionLogLine[] { };
        List<InspectionAction> InspectionActions = new List<InspectionAction>();

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                InspectionLog.InspectionLogLine[] lines;
                List<InspectionAction> list;
                try
                {
                    lines = InspectionLog.LoadLogLines(openFileDialog1.FileName);
                    list = InspectionLog.LoadInspectionActionLog(openFileDialog1.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                LogLines = lines;
                InspectionActions = list;
                Text = $"查看验布日志 {openFileDialog1.SafeFileName}";
                dgvDetect.DataSource = null;
                BindTree();
            }
        }

        private void BindTree()
        {
            treeView1.Enabled = false;
            treeView1.Nodes.Clear();
            for (int i = 0; i < InspectionActions.Count; i++)
            {
                InspectionAction inspectionaction = InspectionActions[i];
                TreeNode inspectionNode = new TreeNode();
                inspectionNode.Tag = inspectionaction;
                string text = inspectionaction.StartSoftTime.Value.ToString("HH:mm");
                if (inspectionaction.ExitSoftTime == null)
                {
                    text += " (未正常退出软件)";
                }
                else
                {
                    text += $" - {inspectionaction.ExitSoftTime.Value:HH:mm}";
                }
                inspectionNode.Text = text;
                for (int j = 0; j < inspectionaction.FabricActions.Count; j++)
                {
                    FabricAction fabricaction = inspectionaction.FabricActions[j];
                    TreeNode fabricNode = new TreeNode();
                    fabricNode.Tag = fabricaction;
                    fabricNode.Text = $"第{j + 1}匹({fabricaction.ID})";
                    inspectionNode.Nodes.Add(fabricNode);
                }
                treeView1.Nodes.Add(inspectionNode);
            }
            treeView1.Enabled = true;
        }

        private void BindDataGridView()
        {
            TreeNode node = treeView1.SelectedNode;
            if (node != null && node.Tag is FabricAction)
            {
                DataTable dt1 = InspectionStatistics.GetFabricDetectTable((FabricAction)node.Tag);
                dgvDetect.AutoGenerateColumns = true;
                dgvDetect.DataSource = dt1;
                DataTable dt2 = InspectionStatistics.GetFabricSplitTable((FabricAction)node.Tag);
                dgvSplit.AutoGenerateColumns = true;
                dgvSplit.DataSource = dt2;
                DataTable dt3 = InspectionStatistics.GetFabricSaveImageTable((FabricAction)node.Tag);
                dgvSaveImage.AutoGenerateColumns = true;
                dgvSaveImage.DataSource = dt3;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Tag is FabricAction)
            {
                FabricAction fabricaction = (FabricAction)e.Node.Tag;
                txtCheckID.Text = fabricaction.ID;
                txtShareQuote.Text = fabricaction.ShareQuote.ToString();
                txtSpeed.Text = fabricaction.Speed.ToString();
                BindDataGridView();
                Statistics();
            }
            dgvCustom.DataSource = null;
            txtCustomMax.Text = "";
            txtCustomMin.Text = "";
            txtCustomAvg.Text = "";
        }

        private void Statistics()
        {
            TreeNode node = treeView1.SelectedNode;
            if (node != null && node.Tag is FabricAction)
            {
                InspectionStatisticsOptions option = InspectionStatistics.Statistics((FabricAction)node.Tag);
                txtAvgDetectTimeUsage.Text = option.AvgDetectTimeUsage.ToString("f3");
                txtAvgDetectRate.Text = option.AvgDetectRate.ToString("f3");
                txtMaxDetectTimeUsage.Text = option.MaxDetectTimeUsage.ToString("f3");
                txtMinDetectTimeUsage.Text = option.MinDetectTimeUsage.ToString("f3");
                txtMaxDetectQueue.Text = option.MaxDetectQueue.ToString();
                txtTicketTimes.Text = option.TicketTimes.ToString();
                txtTicketFailedTimes.Text = option.TicketFailedTimes.ToString();
                txtTicketFailedRate.Text = (option.TicketFailedRate * 100).ToString() + "%";
                txtAvgSplitTimeUsage.Text = option.AvgSplitTimeUsage.ToString("f3");
                txtMaxSplitTimeUsage.Text = option.MaxSplitTimeUsage.ToString("f3");
                txtMinSplitTimeUsage.Text = option.MinSplitTimeUsage.ToString();
                txtMaxSplitQueue.Text = option.MaxSplitQueue.ToString();
                txtMaxSaveImageQueue.Text = option.MaxSaveImageQueue.ToString();
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null||!(node.Tag is FabricAction))
            {
                MessageBox.Show("请先在目录中选一个检验单");
                return;
            }
            FabricAction fabricaction = (FabricAction)node.Tag;
            DateTime time1 = fabricaction.StartTime.Value;
            DateTime time2 = fabricaction.StopTime == null ? DateTime.MaxValue : fabricaction.StopTime.Value;
            string keyword = txtKeyword.Text;
            DataTable dt = InspectionStatistics.GetCustomTable(LogLines, time1, time2, keyword);
            dgvCustom.AutoGenerateColumns = true;
            dgvCustom.DataSource = dt;
            SetDgvCustomColumnWidth();
            CustomStatisticsOptions option = InspectionStatistics.StatisticsCustom(LogLines, time1, time2, keyword);
            txtCustomMax.Text = option.Max.ToString("f3");
            txtCustomMin.Text = option.Min.ToString("f3");
            txtCustomAvg.Text = option.Avg.ToString("f3");
        }

        private void SetDgvCustomColumnWidth()
        {
            for (int i = 0; i < dgvCustom.Columns.Count - 1; i++)
            {
                dgvCustom.Columns[i].Width = 80;
            }
            dgvCustom.Columns[dgvCustom.Columns.Count - 1].Width = 360;
        }
    }
}
