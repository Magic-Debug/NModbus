using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCTool.Properties;

namespace PLCTool.UC
{
    public partial class UC_ForwardBackControl : UserControl
    {
        public ForwardBackControlState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                SetState();
            }
        }
        public event EventHandler ForwardClick;
        public event EventHandler BackClick;
        public event EventHandler StopClick;

        private ForwardBackControlState _state = ForwardBackControlState.Stop;
        public UC_ForwardBackControl()
        {
            InitializeComponent();
        }
        private void SetState()
        {
            switch(_state)
            {
                case ForwardBackControlState.Back:
                    pictureBox1.Image = Resources.arrowL;
                    pictureBox2.Image = Resources.stop2;
                    pictureBox3.Image = Resources.arrowR3;
                    break;
                case ForwardBackControlState.Forward:
                    pictureBox1.Image = Resources.arrowL3;
                    pictureBox2.Image = Resources.stop2;
                    pictureBox3.Image = Resources.arrowR;
                    break;
                default:
                    pictureBox1.Image = Resources.arrowL3;
                    pictureBox2.Image = Resources.stop;
                    pictureBox3.Image = Resources.arrowR3;
                    break;
            }
        }

        [Serializable]
        public enum ForwardBackControlState
        {
            Back,
            Stop,
            Forward
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            BackClick?.Invoke(this, EventArgs.Empty);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StopClick?.Invoke(this, EventArgs.Empty);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ForwardClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
