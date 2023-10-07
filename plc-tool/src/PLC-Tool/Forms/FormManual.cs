using System;
using System.Drawing;
using System.Windows.Forms;
using MainFrom;
using PLCTool.Properties;

namespace PLCTool.Forms
{
    public partial class FormManual : BaseForm
    {
        public FormManual()
        {
            InitializeComponent();
        }

        private Color OnColor = Color.FromArgb(52, 152, 219);
        private Color OffColor = Color.FromArgb(255, 255, 255);

        private TcpCommunication communication = TcpCommunication.GetInstance();
        public bool ManualMode
        {
            get
            {
                return manualmode;
            }
            set
            {
                if (canset)
                {
                    manualmode = value;
                    pcbManual.Image = manualmode ? Resources.BtnOn2 : Resources.BtnOff2;
                }
            }
        }
        public bool JbdjState
        {
            get
            {
                return jbdjState;
            }
            set
            {
                if (canset)
                {
                    jbdjState = value;
                    pcbJbdj.Image = jbdjState ? Resources.BtnOn2 : Resources.BtnOff2;
                }
            }
        }
        public bool ZkfState
        {
            get
            {
                return zkfState;
            }
            set
            {
                if (canset)
                {
                    zkfState = value;
                    pcbZkf.Image = zkfState ? Resources.BtnOn2 : Resources.BtnOff2;
                }
            }
        }
        public bool QgState
        {
            get
            {
                return qgState;
            }
            set
            {
                if (canset)
                {
                    qgState = value;
                    pcbQg.Image = qgState ? Resources.BtnOn2 : Resources.BtnOff2;
                }
            }
        }
        public bool ZlfbdjState
        {
            get
            {
                return zlfbdjState;
            }
            set
            {
                if (canset)
                {
                    zlfbdjState = value;
                    pcbZlfbdj.Image = zlfbdjState ? Resources.BtnOn2 : Resources.BtnOff2;
                    if (zlfbdjState)
                    {
                        toolTip1.SetToolTip(lblZlfbdj, "V91.6");
                        toolTip1.SetToolTip(pcbZlfbdj, "V91.6");
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblZlfbdj, "V91.7");
                        toolTip1.SetToolTip(pcbZlfbdj, "V91.7");
                    }
                }
            }
        }
        public bool FjdjBack
        {
            get
            {
                return fjdjBack;
            }
            set
            {
                if (canset)
                {
                    fjdjBack = value;
                    SetFjdj();
                }
            }
        }
        public bool FjdjForward
        {
            get
            {
                return fjdjForward;
            }
            set
            {
                if (canset)
                {
                    fjdjForward = value;
                    SetFjdj();
                }
            }
        }
        public bool ZldjBack
        {
            get
            {
                return zldjBack;
            }
            set
            {
                if (canset)
                {
                    zldjBack = value;
                    SetZldj();
                }
            }
        }
        public bool ZldjForward
        {
            get
            {
                return zldjForward;
            }
            set
            {
                if (canset)
                {
                    zldjForward = value;
                    SetZldj();
                }
            }
        }
        public bool ZdjBack
        {
            get
            {
                return zdjBack;
            }
            set
            {
                if (canset)
                {
                    zdjBack = value;
                    SetZdj();
                }
            }
        }
        public bool ZdjForward
        {
            get
            {
                return zdjForword;
            }
            set
            {
                if (canset)
                {
                    zdjForword = value;
                    SetZdj();
                }
            }
        }
        public bool Z_Up
        {
            get
            {
                return z_up;
            }
            set
            {
                z_up = value;
                btnZUp.BaseColor = z_up ? OnColor : OffColor;
            }
        }
        public bool Z_Down
        {
            get
            {
                return z_down;
            }
            set
            {
                z_down = value;
                btnZDown.BaseColor = z_down ? OnColor : OffColor;
            }
        }
        public bool Z_Stop
        {
            get
            {
                return z_stop;
            }
            set
            {
                z_stop = value;
                btnZstop.BaseColor = z_stop ? OnColor : OffColor;
            }
        }
        public bool Z_Return
        {
            get
            {
                return z_return;
            }
            set
            {
                z_return = value;
                btnZReturn.BaseColor = z_return ? OnColor : OffColor;
            }
        }
        public bool X_Left
        {
            get
            {
                return x_left;
            }
            set
            {
                x_left = value;
                btnXLeft.BaseColor = x_left ? OnColor : OffColor;
            }
        }
        public bool X_Right
        {
            get
            {
                return x_right;
            }
            set
            {
                x_right = value;
                btnXRight.BaseColor = x_right ? OnColor : OffColor;
            }
        }
        public bool X_Stop
        {
            get
            {
                return x_stop;
            }
            set
            {
                x_stop = value;
                btnXStop.BaseColor = x_stop ? OnColor : OffColor;
            }
        }
        public bool X_Return
        {
            get
            {
                return x_return;
            }
            set
            {
                x_return = value;
                btnXReturn.BaseColor = x_return ? OnColor : OffColor;
            }
        }

        private bool manualmode;
        private bool jbdjState;
        private bool zkfState;
        private bool qgState;
        private bool zlfbdjState;
        private bool fjdjBack;
        private bool fjdjForward;
        private bool zldjBack;
        private bool zldjForward;
        private bool zdjBack;
        private bool zdjForword;
        private bool z_down;
        private bool z_up;
        private bool z_stop;
        private bool z_return;
        private bool x_left;
        private bool x_right;
        private bool x_stop;
        private bool x_return;
        private bool canset = true;

        private void SetFjdj()
        {
            if (fjdjBack)
            {
                pcbFjdjBack.Image = Resources.Back;
                pcbFjdjForward.Image = Resources.Forward2;
            }
            else if (fjdjForward)
            {
                pcbFjdjBack.Image = Resources.Back2;
                pcbFjdjForward.Image = Resources.Forward;
            }
            else
            {
                pcbFjdjBack.Image = Resources.Back2;
                pcbFjdjForward.Image = Resources.Forward2;
            }
        }
        private void SetZldj()
        {
            if (zldjBack)
            {
                pcbZldjBack.Image = Resources.Back;
                pcbZldjForward.Image = Resources.Forward2;
            }
            else if (zldjForward)
            {
                pcbZldjBack.Image = Resources.Back2;
                pcbZldjForward.Image = Resources.Forward;
            }
            else
            {
                pcbZldjBack.Image = Resources.Back2;
                pcbZldjForward.Image = Resources.Forward2;
            }
        }
        private void SetZdj()
        {
            if (zdjBack)
            {
                btnZdjBack.BaseColor = OnColor;
                btnZdjForward.BaseColor = OffColor;
            }
            else if (zdjForword)
            {
                btnZdjBack.BaseColor = OffColor;
                btnZdjForward.BaseColor = OnColor;
            }
            else
            {
                btnZdjBack.BaseColor = OffColor;
                btnZdjForward.BaseColor = OffColor;
            }
        }

        private void pcbZup_MouseDown(object sender, MouseEventArgs e)
        {
            canset = false;
            Z_Up = true;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void pcbZup_MouseUp(object sender, MouseEventArgs e)
        {
            canset = false;
            Z_Up = false;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void pcbZdown_MouseDown(object sender, MouseEventArgs e)
        {
            canset = false;
            Z_Down = true;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void pcbZdown_MouseUp(object sender, MouseEventArgs e)
        {
            canset = false;
            Z_Down = false;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void pcbXleft_MouseDown(object sender, MouseEventArgs e)
        {
            canset = false;
            X_Left = true;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void pcbXleft_MouseUp(object sender, MouseEventArgs e)
        {
            canset = false;
            X_Left = false;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void pcbXright_MouseDown(object sender, MouseEventArgs e)
        {
            canset = false;
            X_Right = true;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void pcbXright_MouseUp(object sender, MouseEventArgs e)
        {
            canset = false;
            X_Right = false;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void chkZstop_Click(object sender, EventArgs e)
        {
            canset = false;
            Z_Stop = !Z_Stop;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void chkZreturn_Click(object sender, EventArgs e)
        {
            canset = false;
            Z_Return = !Z_Return;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void chkXstop_Click(object sender, EventArgs e)
        {
            canset = false;
            X_Stop = !X_Stop;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void chkXreturn_Click(object sender, EventArgs e)
        {
            canset = false;
            X_Return = !X_Return;
            WriteModbusManualDebugTicket();
            canset = true;
        }

        private void pcbZkf_Click(object sender, EventArgs e)
        {
            canset = false;
            zkfState = !zkfState;
            pcbZkf.Image = zkfState ? Resources.BtnOn2 : Resources.BtnOff2;
            WriteModbusManualDebugPLC();
            canset = true;
        }
        private void pcbZlfbdj_Click(object sender, EventArgs e)
        {
            canset = false;
            zlfbdjState = !zlfbdjState;
            pcbZlfbdj.Image = zlfbdjState ? Resources.BtnOn2 : Resources.BtnOff2;
            if (zlfbdjState)
            {
                toolTip1.SetToolTip(lblZlfbdj, "V91.6");
                toolTip1.SetToolTip(pcbZlfbdj, "V91.6");
            }
            else
            {
                toolTip1.SetToolTip(lblZlfbdj, "V91.7");
                toolTip1.SetToolTip(pcbZlfbdj, "V91.7");
            }
            WriteModbusManualDebugPLC();
            canset = true;
        }
        private void pcbQg_Click(object sender, EventArgs e)
        {
            canset = false;
            qgState = !qgState;
            pcbQg.Image = qgState ? Resources.BtnOn2 : Resources.BtnOff2;
            WriteModbusManualDebugPLC();
            canset = true;
        }
        private void pcbJbdj_Click(object sender, EventArgs e)
        {
            canset = false;
            jbdjState = !jbdjState;
            pcbJbdj.Image = jbdjState ? Resources.BtnOn2 : Resources.BtnOff2;
            WriteModbusManualDebugPLC();
            canset = true;
        }
        private void WriteModbusManualDebugPLC()
        {
            ModbusStatus states = Common.GetInstance().modbusStatus;
            states.ManualDebugPLC_jbdj = jbdjState;
            states.ManualDebugPLC_zkf = zkfState;
            states.ManualDebugPLC_qg = qgState;
            states.ManualDebugPLC_zlfbdj_on = zlfbdjState;
            states.ManualDebugPLC_zlfbdj_off = !zlfbdjState;
            states.ManualDebugPLC_fjdj_fz = fjdjBack;
            states.ManualDebugPLC_fjdj_zz = fjdjForward;
            states.ManualDebugPLC_zldj_fz = zldjBack;
            states.ManualDebugPLC_zldj_zz = zldjForward;
            states.ManualDebugPLC_zdj_fz = zdjBack;
            states.ManualDebugPLC_zdj_zz = zdjForword;
            communication.WriteRegister<ushort>(ModbusRegs.ManualDebugPLC, states.ManualDebugPLC);
            if (!communication.IsCanRunning)
            {
                communication.ReadAllRegister();
            }
        }
        private void WriteModbusManualDebugTicket()
        {
            ModbusStatus states = Common.GetInstance().modbusStatus;
            states.ManualDebugTicket_Z_down = z_down;
            states.ManualDebugTicket_Z_up = z_up;
            states.ManualDebugTicket_Z_stop = z_stop;
            states.ManualDebugTicket_Z_return = z_return;
            states.ManualDebugTicket_X_1 = x_left;
            states.ManualDebugTicket_X_2 = x_right;
            states.ManualDebugTicket_X_stop = x_stop;
            states.ManualDebugTicket_X_return = x_return;
            communication.WriteRegister<ushort>(ModbusRegs.ManualDebugTicket, states.ManualDebugTicket);
            if (!communication.IsCanRunning)
            {
                communication.ReadAllRegister();
            }
        }

        private void pcbManual_Click(object sender, EventArgs e)
        {
            canset = false;
            communication.WriteRegister<ushort>(ModbusRegs.SetManualMode, (ushort)(ManualMode ? 0 : 1));
            if (!communication.IsCanRunning)
            {
                communication.ReadAllRegister();
            }
            canset = true;
        }

        private void uC_ForwardBackControl1_BackClick(object sender, EventArgs e)
        {
            canset = false;
            fjdjBack = true;
            fjdjForward = false;
            SetFjdj();
            WriteModbusManualDebugPLC();
            canset = true;
        }

        private void uC_ForwardBackControl1_ForwardClick(object sender, EventArgs e)
        {
            canset = false;
            fjdjBack = false;
            fjdjForward = true;
            SetFjdj();
            WriteModbusManualDebugPLC();
            canset = true;
        }

        private void uC_ForwardBackControl1_StopClick(object sender, EventArgs e)
        {
            canset = false;
            fjdjBack = false;
            fjdjForward = false;
            SetFjdj();
            WriteModbusManualDebugPLC();
            canset = true;
        }

        private void uC_ForwardBackControl2_BackClick(object sender, EventArgs e)
        {
            canset = false;
            zldjBack = true;
            zldjForward = false;
            SetZldj();
            WriteModbusManualDebugPLC();
            canset = true;
        }

        private void uC_ForwardBackControl2_ForwardClick(object sender, EventArgs e)
        {
            canset = false;
            zldjBack = false;
            zldjForward = true;
            SetZldj();
            canset = true;
        }

        private void uC_ForwardBackControl2_StopClick(object sender, EventArgs e)
        {
            canset = false;
            zldjBack = false;
            zldjForward = false;
            SetZldj();
            WriteModbusManualDebugPLC();
            canset = true;
        }

        private void uC_ForwardBackControl3_BackClick(object sender, EventArgs e)
        {
            canset = false;
            zdjBack = true;
            zdjForword = false;
            SetZdj();
            WriteModbusManualDebugPLC();
            canset = true;
        }

        private void uC_ForwardBackControl3_ForwardClick(object sender, EventArgs e)
        {
            canset = false;
            zdjBack = false;
            zdjForword = true;
            SetZdj();
            WriteModbusManualDebugPLC();
            canset = true;
        }

        private void uC_ForwardBackControl3_StopClick(object sender, EventArgs e)
        {
            canset = false;
            zdjBack = false;
            zdjForword = false;
            SetZdj();
            WriteModbusManualDebugPLC();
            canset = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ModbusStatus states = Common.GetInstance().modbusStatus;
            ManualMode = states.SetManualMode == 1;
            //ZlfbdjState = states.ManualDebugPLC_zlfbdj_on;
            JbdjState = states.ManualDebugPLC_jbdj;
            ZkfState = states.ManualDebugPLC_zkf;
            QgState = states.ManualDebugPLC_qg;

            FjdjBack = states.ManualDebugPLC_fjdj_fz;
            FjdjForward = states.ManualDebugPLC_fjdj_zz;
            ZldjBack = states.ManualDebugPLC_zldj_fz;
            ZldjForward = states.ManualDebugPLC_zldj_zz;
            ZdjBack = states.ManualDebugPLC_zdj_fz;
            ZdjForward = states.ManualDebugPLC_zdj_zz;

            Z_Stop = states.ManualDebugTicket_Z_stop;
            Z_Return = states.ManualDebugTicket_Z_return;
            X_Stop = states.ManualDebugTicket_X_stop;
            X_Return = states.ManualDebugTicket_X_return;
        }
    }
}
