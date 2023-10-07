using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLCTool.UC
{
    public partial class UC_Meter : UserControl
    {
        public UC_Meter()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]        
        public string Unit
        {
            get
            {
                return lblUnit.Text;
            }
            set
            {
                lblUnit.Text = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public double MinValue
        {
            get
            {
                return _minvalue;
            }
            set
            {
                _minvalue = value;
            }
        }
        private double _minvalue;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public double MaxValue
        {
            get
            {
                return _maxvalue;
            }
            set
            {
                _maxvalue = value;
            }
        }
        private double _maxvalue;

        //[Browsable(true)]
        //[EditorBrowsable(EditorBrowsableState.Always)]
        //public double AlarmValue
        //{
        //    get
        //    {
        //        return _alarmvalue;
        //    }
        //    set
        //    {
        //        _alarmvalue = value;
        //        SetUI();
        //    }
        //}
        //private double _alarmvalue = double.PositiveInfinity;

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                SetUI();
            }
        }
        private double _value;

        private void SetUI()
        {
            //if (_value > _alarmvalue)
            //{
            //    progressBar1.ForeColor = Color.Red;
            //}
            //else
            //{
            //    progressBar1.ForeColor = Color.Lime;
            //}
            double progress = (_value - _minvalue) / (_maxvalue - _minvalue);
            if (progress < 0 || double.IsNaN(progress))
            {
                progressBar1.Value = 0;
            }
            else if (progress > 1)
            {
                progressBar1.Value = 100;
            }
            else
            {
                progressBar1.Value = (int)Math.Round(progress * 100);
            }
            lblValue.Text = _value.ToString("f3");
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //}
    }
}
