using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using TsComm;
using FrameworkCommon;
using System.Threading;

namespace PLCTool.Forms
{
    public partial class FormColourDifferenceTest : BaseForm
    {
        Instrument instrument = new Instrument();
        private StandardRecord standard = null;
        private TrialRecord trial = null;
        private StandardIlluminant standardIlluminant = StandardIlluminant.D65;
        private StandardObserver standardObserver = StandardObserver.CIE1964;
        private ScMode scMode = ScMode.SCI;
        private ColorSpace colorSpace = ColorSpace.CIELAB;
        static readonly Dictionary<ColorSpace, string[]> ColorSpaceItems = new Dictionary<ColorSpace, string[]>
        {
            {ColorSpace.CIELAB, new []{"L*", "a*", "b*" } },
            {ColorSpace.CIEXYZ, new []{"X", "Y", "Z" } },
            {ColorSpace.CIEYxy, new []{"Y", "x", "y" } },
            {ColorSpace.CIELCH, new []{"L*", "C*", "h°" } },
            {ColorSpace.CIELUV, new []{"L*", "u*", "v*" } },
            {ColorSpace.HUNTERLAB, new []{"L", "a", "b" } },
            {ColorSpace.BETA_XY, new []{"β", "x", "y" } },
            {ColorSpace.DIN_LAB99, new []{"L99", "a99", "b99" } },
            {ColorSpace.SRGB, new []{"R", "G", "B" } },
        };
        public MainFrom.TcpCommunication communication = MainFrom.TcpCommunication.GetInstance();
        private Common common = Common.GetInstance();
        public FormColourDifferenceTest()
        {
            InitializeComponent();
            instrument.MeasurementResultReceived += OnMeasurementResultReceived;
            toolTip1.SetToolTip(label3, "VW18=1");
            toolTip1.SetToolTip(txtPositionX, "VW18=1");
            toolTip1.SetToolTip(lblFinishTicket, "VW18=1");
            toolTip1.SetToolTip(btnReturnZero, "VW18=1");
            toolTip1.SetToolTip(btnMeasureSample, "VW18=1");
        }
        private void OnMeasurementResultReceived(Record record)
        {
            if (record is StandardRecord std)
            {
                standard = std;
            }
            else if (record is TrialRecord t)
            {
                trial = t;
            }
            BeginInvoke(new Action(updateData));
        }
        private void updateData()
        {
            if (standard == null && trial == null)
            {
                return;
            }
            double[] stdValues = null;
            double[] stdSpectralData = null;
            double[] trialValues = null;
            double[] trialSpectralData = null;           
            int index = 0;
            int indexSample = 0;
            if (dgvStandard.Rows.Count == 0)
            {
                index = dgvStandard.Rows.Add();
            }

            if (standard != null)
            {
                stdSpectralData = (scMode == ScMode.SCI ? standard.Sci : standard.Sce)?.SpectralData;
                if (stdSpectralData != null)
                {
                    stdValues = Instrument.GetColorDataFromSpectralData(colorSpace, stdSpectralData,
                        standardIlluminant, standardObserver);
                }
            }

            if (trial != null)
            {
                trialSpectralData = (scMode == ScMode.SCI ? trial.Sci : trial.Sce)?.SpectralData;
                if (trialSpectralData != null)
                {
                    trialValues = Instrument.GetColorDataFromSpectralData(colorSpace, trialSpectralData,
                        standardIlluminant, standardObserver);
                }
                indexSample = dgvSample.Rows.Add();
            }

            
            string name = "--";
            string standardVue = "--";
            string sampleVue = "--";
            string difference = "--";
            for (int i = 0; i < ColorSpaceItems[colorSpace].Length; i++)
            {
                var label = ColorSpaceItems[colorSpace][i];
                name = label;
                var format = new[] { "x", "y", "β" }.Contains(label) ? "F4" : "F2";
                standardVue = stdValues == null ? "--" : stdValues[i].ToString(format);
                sampleVue = trialValues == null ? "--" : trialValues[i].ToString(format);
                if (stdValues != null && trialValues != null)
                {
                    difference = (trialValues[i] - stdValues[i]).ToString(format);
                }
                if (trial == null)
                {
                    dgvStandard.Rows[index].Cells[i].Value = standardVue;
                }
                else
                {
                    dgvSample.Rows[indexSample].Cells[0].Value = indexSample + 1;
                    switch (i)
                    {
                        case 0:
                            dgvSample.Rows[indexSample].Cells[1].Value = sampleVue;
                            dgvSample.Rows[indexSample].Cells[4].Value = difference;
                            break;
                        case 1:
                            dgvSample.Rows[indexSample].Cells[2].Value = sampleVue;
                            dgvSample.Rows[indexSample].Cells[5].Value = difference;
                            break;
                        case 2:
                            dgvSample.Rows[indexSample].Cells[3].Value = sampleVue;
                            dgvSample.Rows[indexSample].Cells[6].Value = difference;

                            try
                            {
                                double e = Math.Pow(double.Parse(dgvSample.Rows[indexSample].Cells[4].Value.ToString()), 2.0) + Math.Pow(double.Parse(dgvSample.Rows[indexSample].Cells[5].Value.ToString()), 2.0) + Math.Pow(double.Parse(dgvSample.Rows[indexSample].Cells[6].Value.ToString()), 2.0);
                                e = Math.Sqrt(e);
                                dgvSample.Rows[indexSample].Cells[8].Value = Math.Round(e, 2);
                            }
                            catch
                            { }
                            break;
                    }
                }
            }
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var dlg = new FormColourDifferenceDlg();
            if (dlg.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }
            txtName.Text = dlg.SelectedSlaveMac;
            txtPort.Text = dlg.SelectedPortName;
            if (!timer1.Enabled)
            {
                timer1.Start();
            }
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvStandard.Rows.Clear();
            dgvSample.Rows.Clear();
            standard = null;
            trial = null;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtName.Text) && !instrument.IsOpen)
            {
                if (Connect(txtPort.Text, txtName.Text))
                {
                    lbConnectState.Text = "已连接";
                    lbConnectState.ForeColor = Color.Green;
                }
                else
                {
                    lbConnectState.Text = "未连接";
                    lbConnectState.ForeColor = Color.Red;
                }
            }
        }
        
        /// <summary>
        /// 连接色差仪
        /// </summary>
        /// <returns></returns>
        private bool Connect(string portName, string slaveMac)
        {
            if (!instrument.IsOpen)
            {
                ErrorCode error = instrument.OpenByBleDongle(portName, slaveMac);
                if (error != ErrorCode.NoError)
                {
                    MessageBox.Show(_(error));
                    return false;
                }
            }
            return true;
        }
        private void btnMeasureStandard_Click(object sender, EventArgs e)
        {
            dgvStandard.Rows.Clear();
            MeasureStandard();
        }

        private void btnMeasureSample_Click(object sender, EventArgs e)
        {
            Collection();
            //MeasureSample();
        }
        /// <summary>
        /// 测量标样
        /// </summary>
        private void MeasureStandard()
        {
            StandardRecord record;
            var error = instrument.MeasureStandard(out record);
            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error));                
                return;
            }

            standard = record;
            updateData();
        }
        /// <summary>
        /// 测量试样
        /// </summary>
        private void MeasureSample()
        {
            TrialRecord record;
            var error = instrument.MeasureTrial(out record);
            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error));
                return;
            }

            trial = record;
            updateData();
        }
        string _(Enum enumItem)
        {
            ComponentResourceManager rm = new ComponentResourceManager(typeof(LanguageResource));
            return rm.GetString("FormColourDifference_" + enumItem.ToString());
        }

        private void FormColourDifferenceTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            if (instrument.IsOpen)
            {                
                instrument.Close();                
            }
        }

        private void txtPositionX_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timerSetValue.Enabled = false;
                try
                {
                    float value = Convert.ToSingle(((TextBox)sender).Text.Trim());                    
                    communication.WriteRegister<float>(MainFrom.ModbusRegs.ColourDifferencePositionX, value);                    
                    if (!communication.IsCanRunning)
                    {
                        communication.ReadAllRegister();
                    }
                    ((TextBox)sender).Text = value.ToString("f3");
                    ((TextBox)sender).ForeColor = Color.Lime;
                }
                catch { }
                timerSetValue.Enabled = true;
            }
            else
            {
                ((TextBox)sender).ForeColor = Color.Black;
            }
        }

        private void txtPositionX_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).ForeColor = Color.Black;
        }

        private void btnReturnZero_Click(object sender, EventArgs e)
        {
            try
            {
                //ModbusStatus states = common.modbusStatus;
                //states.ColourDifference_buzzer = true;
                //communication.WriteRegister<ushort>(MainFrom.ModbusRegs.ColourDifference, states.ColourDifference);
                //Thread.Sleep(50);
                //states.ColourDifference_buzzer = false;
                //communication.WriteRegister<ushort>(MainFrom.ModbusRegs.ColourDifference, states.ColourDifference);
                //Thread.Sleep(50);
                //LogHelper.Default.Debug("色差仪归零");
            }
            catch { }
        }
        private void Collection()
        {
            timerSetValue.Enabled = false;
            ModbusStatus states = common.modbusStatus;
            states.ColourDifference_red = true;
            communication.WriteRegister<ushort>(MainFrom.ModbusRegs.WeijinCount, 1);
            if (!communication.IsCanRunning)
            {
                communication.ReadAllRegister();
            }
            timerSetValue.Enabled = true;
            LogHelper.Default.Debug("通知采集");
        }
        private void timerSetValue_Tick(object sender, EventArgs e)
        {
            if (communication.IsConnected)
            {
                common.modbusStatus.SetValues(common.modbusValues);
                ModbusStatus states = common.modbusStatus;
                states.ColourDifference_yellow = true;
                if (states.ColourDifferenceReady == 1)
                {                    
                    rdoFinish.Checked = true;
                    timerSetValue.Enabled = false;
                    MeasureSample();
                }
                else
                {
                    rdoNotFinish.Checked = false;
                }
            }
        }
    }
}
