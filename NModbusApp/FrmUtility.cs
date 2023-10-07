namespace NModbusApp
{
    public partial class FrmUtility : Form
    {
        public FrmUtility()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            byte[]? dd = BitConverter.GetBytes(-132.64f);
        }
    }
}
