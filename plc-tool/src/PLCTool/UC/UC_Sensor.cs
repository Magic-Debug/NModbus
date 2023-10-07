namespace PLCTool.UC
{
    public partial class UC_Sensor : UserControl
    {
        public UC_Sensor(byte registerAddress, byte registerBit)
        {
            InitializeComponent();
            this.RegisterAddress = registerAddress;
            this.RegisterBit = registerBit;
        }

        /// <summary>
        /// 传感器名称
        /// </summary>
        public string SensorName
        {
            get
            {
                return lblName.Text;
            }
            set
            {
                lblName.Text = value;
            }
        }

        private string _GroupName;
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName
        {
            get
            {
                return _GroupName;
            }
            set
            {
                _GroupName = value;
                lblGroupSubName.Text = value.Length > 1 ? value.Substring(value.Length - 2, 2) + "." + RegisterBit % 8 : "";
            }
        }

        /// <summary>
        /// 分组简称
        /// </summary>
        public string GroupSubName
        {
            get
            {
                return lblGroupSubName.Text;
            }
        }

        /// <summary>
        /// 所属寄存器地址
        /// </summary>
        public byte RegisterAddress { get; }

        /// <summary>
        /// 所属寄存器位
        /// </summary>
        public byte RegisterBit { get; set; }

        /// <summary>
        /// 寄存器字节地址
        /// </summary>
        public int RegisterByteAddress
        {
            get
            {
                return RegisterAddress * 2 + (RegisterBit >= 8 ? 1 : 0);
            }
        }

        /// <summary>
        /// True状态时的图像
        /// </summary>
        public Image TrueStatusImage;

        /// <summary>
        /// False状态时的图像
        /// </summary>
        public Image FalseStatusImage;

        /// <summary>
        /// 当前状态
        /// </summary>
        private bool _Status;
        public bool Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                pbxStatusImage.Image = value ? TrueStatusImage : FalseStatusImage;
                lblGroupSubName.Parent = pbxStatusImage;
            }
        }

        private void lbl_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }

        public override string ToString() => $"传感器名称={SensorName} 所属寄存器位={RegisterBit} 所属寄存器地址={RegisterAddress} 寄存器字节地址={RegisterByteAddress}";
    }
}
