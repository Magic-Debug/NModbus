namespace NModbusApp
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void btnSerialAscii_Click(object sender, EventArgs e)
        {

        }

        private void btnTCP_Click(object sender, EventArgs e)
        {
            FrmTcp frmTcp = new FrmTcp();
            frmTcp.ShowDialog();
        }

        private void btnSerialRtu_Click(object sender, EventArgs e)
        {

        }

        private void btnSocketSerial_Click(object sender, EventArgs e)
        {

        }
    }
}