using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;

namespace PLCTool.Forms
{
    public partial class frmBatchTickPoints : BaseForm
    {
        public frmBatchTickPoints()
        {
            InitializeComponent();

            queBatchTicketPoints = new ConcurrentQueue<PointF>();
        }

        public frmBatchTickPoints(ConcurrentQueue<PointF> queBatchTicketPoints,bool isAreaTicket)
        {
            InitializeComponent();

            if (queBatchTicketPoints != null)
            {
                this.queBatchTicketPoints = queBatchTicketPoints;
                foreach (PointF tempPoint in queBatchTicketPoints)
                {
                    lbxTicketPoints.Items.Add(tempPoint.X + "," + tempPoint.Y);
                }
            }
            _isAreaTicket = isAreaTicket;
        }

        private bool Changed = false;
        public ConcurrentQueue<PointF> queBatchTicketPoints;
        private bool _isAreaTicket;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPointX.Text.Trim()))
            {
                MessageBox.Show("未输入贴标X值");
                txtNewPointX.Focus();
                return;
            }            
            float newPointX;
            if (!float.TryParse(txtNewPointX.Text.Trim(), out newPointX) || newPointX < 0)
            {
                MessageBox.Show("贴标X值必须为大于等于0的数值");
                txtNewPointX.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtNewPointY.Text.Trim()))
            {
                MessageBox.Show("未输入贴标Y值");
                txtNewPointY.Focus();
                return;
            }
            float newPointY;
            if (!float.TryParse(txtNewPointY.Text.Trim(), out newPointY) || newPointY <= 0)
            {
                MessageBox.Show("贴标Y值必须为大于0的数值");
                txtNewPointY.Focus();
                return;
            }
            if(_isAreaTicket && newPointY > 270)
            {
                MessageBox.Show("已开启区域贴标，贴标Y值不能大于270");
                txtNewPointY.Focus();
                return;
            }

            bool added = false;
            if (!_isAreaTicket)//不停机贴标，对添加的坐标进行排序
            {
                string[] pointParts, separetorStrs = { ",", " " };
                float tempX, tempY;
                for (int i = 0; i < lbxTicketPoints.Items.Count; i++)
                {
                    pointParts = lbxTicketPoints.Items[i].ToString().Split(separetorStrs, StringSplitOptions.RemoveEmptyEntries);
                    if (pointParts.Length == 2 && float.TryParse(pointParts[0], out tempX) && float.TryParse(pointParts[1], out tempY))
                    {
                        if (newPointY <= tempY)
                        {
                            if (newPointX != tempX || newPointY != tempY)
                                lbxTicketPoints.Items.Insert(i, newPointX + "," + newPointY);
                            added = true;
                            break;
                        }
                    }
                }
            }
            if(!added) lbxTicketPoints.Items.Add(newPointX + "," + newPointY);
            txtNewPointX.Text = txtNewPointY.Text = "";
            txtNewPointX.Focus();
            Changed = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lbxTicketPoints.SelectedItem != null)
            {
                lbxTicketPoints.Items.Remove(lbxTicketPoints.SelectedItem);
                Changed = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (queBatchTicketPoints != null)
            {
                if (Changed)
                {
                    PointF tempPoint;
                    while (queBatchTicketPoints.Count > 0)
                    {
                        queBatchTicketPoints.TryDequeue(out tempPoint);
                    }
                    string[] pointParts, separetorStrs = { ",", " " };
                    float X, Y;
                    for (int i = 0; i < lbxTicketPoints.Items.Count; i++)
                    {
                        pointParts = lbxTicketPoints.Items[i].ToString().Split(separetorStrs, StringSplitOptions.RemoveEmptyEntries);
                        if (pointParts.Length == 2 && float.TryParse(pointParts[0], out X) && float.TryParse(pointParts[1], out Y))
                            queBatchTicketPoints.Enqueue(new PointF(X, Y));
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
