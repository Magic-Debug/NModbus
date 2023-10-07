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

namespace PLCTool.Forms
{
    public partial class FormColourDifferenceDlg : BaseForm
    {
        public FormColourDifferenceDlg()
        {
            InitializeComponent();

            foreach (var portName in SerialPort.GetPortNames())
            {
                comboBoxPortNames.Items.Add(portName);
            }
        }

        public string SelectedPortName { get; private set; }

        public string SelectedSlaveMac { get; private set; }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            var portName = comboBoxPortNames.SelectedItem as string;
            if (portName == null)
            {
                MessageBox.Show(PleaseSelectAPortName);
                return;
            }

            listView1.Items.Clear();

            var error = Instrument.ScanBleSlaves(portName, out var slaves );
            if (error == ErrorCode.NoError && slaves != null)
            {
                foreach (var slave in slaves)
                {
                    var item = new ListViewItem {Text = slave.name};
                    item.SubItems.Add(slave.mac);
                    item.SubItems.Add(slave.signal_intensity.ToString());
                    listView1.Items.Add(item);
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var item = listView1.SelectedItems[0];
                SelectedPortName = comboBoxPortNames.SelectedItem as string;
                SelectedSlaveMac = item.SubItems[1].Text;

                DialogResult = DialogResult.OK;
                return;
            }
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #region 多语言
        private string PleaseSelectAPortName => LanguageResource.FormColourDifference_PleaseSelectAPortName;
        #endregion
    }
}
