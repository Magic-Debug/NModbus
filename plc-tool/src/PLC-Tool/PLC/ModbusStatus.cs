using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using MainFrom;

namespace PLCTool
{
    public class ModbusStatus
    {
        public ModbusStatus()
        {

        }

        public ModbusStatus(ushort[] values)
        {
            SetValues(values);
        }        

        public static float OnePlusLength = 0.05f;
        public int Encoder
        {
            get { return EncoderHight << 16 | EncoderLow; }
            set
            {
                EncoderHight = (ushort)(value >> 16 & 0xffff);
                EncoderLow = (ushort)(value & 0xffff);
            }
        }
        public ushort EncoderHight;
        public ushort EncoderLow;
        public float EncoderLength => Encoder * OnePlusLength * 0.001f;
        public ushort TensionerMode;
        public ushort MotorState;
        public ushort PickAxles;
        public ushort ReleaseAxles;
        public ushort ProtectiveFilm;
        public float DoubleLine;
        public ushort MasterMotorState;
        public ushort TicketAlarm;        
        
        #region MotorState
        public bool MotorState_fj_err
        {
            get { return GetBit(MotorState, 7); }
            set { SetBit(ref MotorState, 7, value); }
        }
        public bool MotorState_fj_run
        {
            get { return GetBit(MotorState, 6); }
            set { SetBit(ref MotorState, 6, value); }
        }
        public bool MotorState_bb_err
        {
            get { return GetBit(MotorState, 5); }
            set { SetBit(ref MotorState, 5, value); }
        }
        public bool MotorState_bb_run
        {
            get { return GetBit(MotorState, 4); }
            set { SetBit(ref MotorState, 4, value); }
        }
        public bool MotorState_zl_err
        {
            get { return GetBit(MotorState, 3); }
            set { SetBit(ref MotorState, 3, value); }
        }
        public bool MotorState_zl_run
        {
            get { return GetBit(MotorState, 2); }
            set { SetBit(ref MotorState, 2, value); }
        }
        public bool MotorState_qy_err
        {
            get { return GetBit(MotorState, 1); }
            set { SetBit(ref MotorState, 1, value); }
        }
        public bool MotorState_qy_run
        {
            get { return GetBit(MotorState, 0); }
            set { SetBit(ref MotorState, 0, value); }
        }
        public List<string> GetMotorStateAlarmList()
        {
            List<string> list = new List<string>();
            if (MotorState_fj_err)
            {
                list.Add(rm.GetString("MotorState_fj_err"));
            }
            if (MotorState_bb_err)
            {
                list.Add(rm.GetString("MotorState_bb_err"));
            }
            if (MotorState_zl_err)
            {
                list.Add(rm.GetString("MotorState_zl_err"));
            }
            if (MotorState_qy_err)
            {
                list.Add(rm.GetString("MotorState_qy_err"));
            }
            return list;
        }
        #endregion
        public ushort WorkState;
        public ushort DeviceReady;
        public ushort Alarm;
        #region Alarm        
        
        #endregion
        public ushort TicketFinish;
        public ushort DeviceRunning;
        public float RealTimeWeight;
        public float FinalWeight;
        public ushort SlaveMotorWorkMode;
        public float MasterMotorSpeed;
        public float SlaveMotorRatio;
        public ushort SetTensionerPosion;
        public float SwingMotorRatio;
        public float CurrentPositionX;
        public float CurrentPositionY;
        public float CurrentPositionZ;
        public ushort SetScram;
        public ushort ResetPLC;
        public ushort ResetEncoder;
        public ushort PLCStart_stop;
        public ushort Ticket;
        public float TicketPositionX;
        public ushort TicketAlarmCount;
        public float TicketPositionY;
        public ushort Buzzer;
        public ushort ForwardBackMode;
        public ushort ReadyForTicket;
        public ushort SetManualMode;
        public ushort ManualDebugPLC;
        #region ManualDebugPLC
        public bool ManualDebugPLC_jbdj
        {
            get { return GetBit(ManualDebugPLC, 10); }
            set { SetBit(ref ManualDebugPLC, 10, value); }
        }
        public bool ManualDebugPLC_zkf
        {
            get { return GetBit(ManualDebugPLC, 9); }
            set { SetBit(ref ManualDebugPLC, 9, value); }
        }
        public bool ManualDebugPLC_qg
        {
            get { return GetBit(ManualDebugPLC, 8); }
            set { SetBit(ref ManualDebugPLC, 8, value); }
        }
        public bool ManualDebugPLC_zlfbdj_off
        {
            get { return GetBit(ManualDebugPLC, 7); }
            set { SetBit(ref ManualDebugPLC, 7, value); }
        }
        public bool ManualDebugPLC_zlfbdj_on
        {
            get { return GetBit(ManualDebugPLC, 6); }
            set { SetBit(ref ManualDebugPLC, 6, value); }
        }
        public bool ManualDebugPLC_fjdj_fz
        {
            get { return GetBit(ManualDebugPLC, 5); }
            set { SetBit(ref ManualDebugPLC, 5, value); }
        }
        public bool ManualDebugPLC_fjdj_zz
        {
            get { return GetBit(ManualDebugPLC, 4); }
            set { SetBit(ref ManualDebugPLC, 4, value); }
        }
        public bool ManualDebugPLC_zldj_fz
        {
            get { return GetBit(ManualDebugPLC, 3); }
            set { SetBit(ref ManualDebugPLC, 3, value); }
        }
        public bool ManualDebugPLC_zldj_zz
        {
            get { return GetBit(ManualDebugPLC, 2); }
            set { SetBit(ref ManualDebugPLC, 2, value); }
        }
        public bool ManualDebugPLC_zdj_fz
        {
            get { return GetBit(ManualDebugPLC, 1); }
            set { SetBit(ref ManualDebugPLC, 1, value); }
        }
        public bool ManualDebugPLC_zdj_zz
        {
            get { return GetBit(ManualDebugPLC, 0); }
            set { SetBit(ref ManualDebugPLC, 0, value); }
        }
        #endregion
        public ushort ManualDebugTicket;
        #region ManualDebugTicket
        public bool ManualDebugTicket_Z_down
        {
            get { return GetBit(ManualDebugTicket, 7); }
            set { SetBit(ref ManualDebugTicket, 7, value); }
        }
        public bool ManualDebugTicket_Z_up
        {
            get { return GetBit(ManualDebugTicket, 6); }
            set { SetBit(ref ManualDebugTicket, 6, value); }
        }
        public bool ManualDebugTicket_Z_stop
        {
            get { return GetBit(ManualDebugTicket, 5); }
            set { SetBit(ref ManualDebugTicket, 5, value); }
        }
        public bool ManualDebugTicket_Z_return
        {
            get { return GetBit(ManualDebugTicket, 4); }
            set { SetBit(ref ManualDebugTicket, 4, value); }
        }
        public bool ManualDebugTicket_X_1
        {
            get { return GetBit(ManualDebugTicket, 3); }
            set { SetBit(ref ManualDebugTicket, 3, value); }
        }
        public bool ManualDebugTicket_X_2
        {
            get { return GetBit(ManualDebugTicket, 2); }
            set { SetBit(ref ManualDebugTicket, 2, value); }
        }
        public bool ManualDebugTicket_X_stop
        {
            get { return GetBit(ManualDebugTicket, 1); }
            set { SetBit(ref ManualDebugTicket, 1, value); }
        }
        public bool ManualDebugTicket_X_return
        {
            get { return GetBit(ManualDebugTicket, 0); }
            set { SetBit(ref ManualDebugTicket, 0, value); }
        }
        #endregion
        public ushort IndicatorLight;
        #region IndicatorLight
        public bool IndicatorLight_ticketReturnZero
        {
            get { return GetBit(IndicatorLight, 5); }
            set { SetBit(ref IndicatorLight, 5, value); }
        }
        public bool IndicatorLight_allReturnZero
        {
            get { return GetBit(IndicatorLight, 4); }
            set { SetBit(ref IndicatorLight, 4, value); }
        }
        public bool IndicatorLight_buzzer
        {
            get { return GetBit(IndicatorLight, 3); }
            set { SetBit(ref IndicatorLight, 3, value); }
        }
        public bool IndicatorLight_yellow
        {
            get { return GetBit(IndicatorLight, 2); }
            set { SetBit(ref IndicatorLight, 2, value); }
        }
        public bool IndictorLight_red
        {
            get { return GetBit(IndicatorLight, 1); }
            set { SetBit(ref IndicatorLight, 1, value); }
        }
        public bool IndicatorLight_green
        {
            get { return GetBit(IndicatorLight, 0); }
            set { SetBit(ref IndicatorLight, 0, value); }
        }
        #endregion
        public float TensionSpeed;
        public float WindingSpeed;
        public float TicketFallingHeight;
        public float MainSpeed;
        public ushort ShieldSetting;
        #region ShieldSetting
        /// <summary>
        /// 是否屏蔽设备上的启动按钮,1:屏蔽，0允许
        /// </summary>
        public bool ShieldSetting_sbqd
        {
            get { return GetBit(ShieldSetting, 0); }
            set { SetBit(ref ShieldSetting, 0, value); }
        }
        public bool ShieldSetting_wbjc
        {
            get { return GetBit(ShieldSetting, 5); }
            set { SetBit(ref ShieldSetting, 5, value); }
        }
        public bool ShieldSetting_sjsjwbjc
        {
            get { return GetBit(ShieldSetting, 6); }
            set { SetBit(ref ShieldSetting, 6, value); }
        }
        public bool ShieldSetting_glwc
        {
            get { return GetBit(ShieldSetting, 7); }
            set { SetBit(ref ShieldSetting, 7, value); }
        }
        #endregion
        public float WindingTensionSetValue;
        public float WindingTensionGetValue;
        public float InspectionTensionSetValue;
        public float InspectionTensionGetValue;
        public uint WeijinCount;
        public float WasteEncoderValue;//皮革机型：废料收卷编码器长度 cm
        public float ClothWidthGetValue;        
        public int AxlesErrorCode; //轴故障代码

        public float VD170;
        public float VD174;
        public float VD178;
        public float VD182;
        public float VD186;
        public float VD190;
        public float VD194;
        public float VD198;        
        public int VD202;
        public int VD206;
        public int VD210;
        public int VD214;
        public ushort ColourDifferenceReady;
        #region ColourDifference        
        public bool ColourDifference_buzzer
        {
            get { return GetBit(ColourDifferenceReady, 3); }
            set { SetBit(ref ColourDifferenceReady, 3, value); }
        }
        public bool ColourDifference_yellow
        {
            get { return GetBit(ColourDifferenceReady, 2); }
            set { SetBit(ref ColourDifferenceReady, 2, value); }
        }
        public bool ColourDifference_red
        {
            get { return GetBit(ColourDifferenceReady, 1); }
            set { SetBit(ref ColourDifferenceReady, 1, value); }
        }
        #endregion

        public float ThicknessDisplacement;
        public float ThicknessDisplacement2;


        private static ComponentResourceManager rm = new ComponentResourceManager(typeof(ModbusStatus));

        private static string[] _Alarms;
        /// <summary>
        /// 设备故障
        /// </summary>
        public static string[] Alarms
        {
            get
            {
                if (_Alarms == null)
                {
                    _Alarms = new string[16];
                    for (int i = 0; i < _Alarms.Length; i++)
                    {
                        _Alarms[i] = rm.GetString($"Alarm{i}");
                    }
                }
                return _Alarms;
            }
            set
            {
                _Alarms = value;
            }
        }
        
        public static string[] _TicketAlarms;
        /// <summary>
        /// 贴标故障
        /// </summary>
        public static string[] TicketAlarms
        {
            get
            {
                if (_TicketAlarms == null)
                {
                    _TicketAlarms = new string[6];
                    for (int i = 0; i < _TicketAlarms.Length; i++)
                    {
                        _TicketAlarms[i] = rm.GetString($"TicketAlarm{i}");
                    }
                }
                return _TicketAlarms;
            }
            set
            {
                _TicketAlarms = value;
            }
        }

        public static string[] _AxlesAlarms;
        //轴故障
        public static string[] AxlesAlarms
        {
            get
            {
                if (_AxlesAlarms == null)
                {
                    _AxlesAlarms = new string[36];
                    for (int i = 0; i < _AxlesAlarms.Length; i++)
                    {
                        _AxlesAlarms[i] = rm.GetString($"AxlesAlarm{i}");
                    }
                }
                return _AxlesAlarms;
            }
            set
            {
                _AxlesAlarms = value;
            }
        }

        public ushort[] ToShortValues()
        {
            ushort[] values = new ushort[ModbusRegs.Count];
            values[ModbusRegs.EncoderHigh] = EncoderHight;
            values[ModbusRegs.EncoderLow] = EncoderLow;
            values[ModbusRegs.TensionerMode] = TensionerMode;
            values[ModbusRegs.PickAxles] = PickAxles;
            values[ModbusRegs.ReleaseAxles] = ReleaseAxles;
            values[ModbusRegs.ProtectiveFilm] = ProtectiveFilm;
            values[ModbusRegs.MasterMotorState] = MasterMotorState;
            values[ModbusRegs.TicketAlarm] = TicketAlarm;
            values[ModbusRegs.MotorState] = MotorState;
            values[ModbusRegs.WorkState] = WorkState;
            values[ModbusRegs.DeviceReady] = DeviceReady;
            values[ModbusRegs.Alarm] = Alarm;
            values[ModbusRegs.TicketFinish] = TicketFinish;
            values[ModbusRegs.DeviceRunning] = DeviceRunning;
            Array.Copy(RealTimeWeight.ToByteArray(), 0, values, ModbusRegs.RealtimeWeight, 2);
            Array.Copy(FinalWeight.ToByteArray(), 0, values, ModbusRegs.FinalWeight, 2);
            values[ModbusRegs.SlaveMotorWorkMode] = SlaveMotorWorkMode;
            Array.Copy(MasterMotorSpeed.ToByteArray(), 0, values, ModbusRegs.MasterMotorSpeed, 2);
            Array.Copy(SlaveMotorRatio.ToByteArray(), 0, values, ModbusRegs.SlaveMotorRatio, 2);
            values[ModbusRegs.SetTensionerPosion] = SetTensionerPosion;
            Array.Copy(SwingMotorRatio.ToByteArray(), 0, values, ModbusRegs.SwingMotorRatio, 2);
            Array.Copy(CurrentPositionX.ToByteArray(), 0, values, ModbusRegs.CurrentPositionX, 2);
            Array.Copy(CurrentPositionY.ToByteArray(), 0, values, ModbusRegs.CurrentPositionY, 2);
            Array.Copy(CurrentPositionZ.ToByteArray(), 0, values, ModbusRegs.CurrentPositionZ, 2);
            values[ModbusRegs.SetScram] = SetScram;
            values[ModbusRegs.ResetPLC] = ResetPLC;
            values[ModbusRegs.ResetEncoder] = ResetEncoder;
            values[ModbusRegs.PLCStart_stop] = PLCStart_stop;
            values[ModbusRegs.Ticket] = Ticket;
            Array.Copy(TicketPositionX.ToByteArray(), 0, values, ModbusRegs.TicketPositionX, 2);
            values[ModbusRegs.TicketAlarmCount] = TicketAlarmCount;
            Array.Copy(TicketPositionY.ToByteArray(), 0, values, ModbusRegs.TicketPositionY, 2);
            values[ModbusRegs.Buzzer] = Buzzer;
            values[ModbusRegs.ForwardBackMode] = ForwardBackMode;
            values[ModbusRegs.ReadyForTicket] = ReadyForTicket;
            values[ModbusRegs.SetManualMode] = SetManualMode;
            values[ModbusRegs.ManualDebugPLC] = ManualDebugPLC;
            values[ModbusRegs.ManualDebugTicket] = ManualDebugTicket;
            values[ModbusRegs.IndicatorLight] = IndicatorLight;            
            Array.Copy(AxlesErrorCode.ToByteArray(), 0, values, ModbusRegs.AxlesErrorCode, 2);
            Array.Copy(TensionSpeed.ToByteArray(), 0, values, ModbusRegs.TensionSpeed, 2);
            Array.Copy(WindingSpeed.ToByteArray(), 0, values, ModbusRegs.WindingSpeed, 2);
            Array.Copy(TicketFallingHeight.ToByteArray(), 0, values, ModbusRegs.TicketFallingHeight, 2);
            Array.Copy(MainSpeed.ToByteArray(), 0, values, ModbusRegs.MainSpeed, 2);
            values[ModbusRegs.ShieldSetting] = ShieldSetting;
            Array.Copy(WindingTensionSetValue.ToByteArray(), 0, values, ModbusRegs.WindingTensionSetValue, 2);
            Array.Copy(WindingTensionGetValue.ToByteArray(), 0, values, ModbusRegs.WindingTensionGetValue, 2);
            Array.Copy(InspectionTensionSetValue.ToByteArray(), 0, values, ModbusRegs.InspectionTensionSetValue, 2);
            Array.Copy(InspectionTensionGetValue.ToByteArray(), 0, values, ModbusRegs.InspectionTensionGetValue, 2);
            Array.Copy(WeijinCount.ToByteAray(), 0, values, ModbusRegs.WeijinCount, 2);
            Array.Copy(WasteEncoderValue.ToByteArray(), 0, values, ModbusRegs.CurrentPositionX, 2);
            Array.Copy(ClothWidthGetValue.ToByteArray(), 0, values, ModbusRegs.ClothWidthGetValue, 2);
            Array.Copy(VD170.ToByteArray(), 0, values, 85, 2);
            Array.Copy(VD174.ToByteArray(), 0, values, 87, 2);
            Array.Copy(VD178.ToByteArray(), 0, values, 89, 2);
            Array.Copy(VD182.ToByteArray(), 0, values, 91, 2);
            Array.Copy(VD186.ToByteArray(), 0, values, 93, 2);
            Array.Copy(VD190.ToByteArray(), 0, values, 95, 2);
            Array.Copy(VD194.ToByteArray(), 0, values, 97, 2);
            Array.Copy(VD198.ToByteArray(), 0, values, 99, 2);        
            Array.Copy(VD202.ToByteArray(), 0, values, 101, 2);
            Array.Copy(VD206.ToByteArray(), 0, values, 103, 2);
            Array.Copy(VD210.ToByteArray(), 0, values, 105, 2);
            Array.Copy(VD214.ToByteArray(), 0, values, 107, 2);
            Array.Copy(ThicknessDisplacement.ToByteArray(), 0, values, ModbusRegs.ThicknessDisplacement, 2);
            Array.Copy(ThicknessDisplacement2.ToByteArray(), 0, values, ModbusRegs.ThicknessDisplacement2, 2);
            values[ModbusRegs.ColourDifferenceReady] = ColourDifferenceReady;
            return values;
        }

        public void SetValues(ushort[] values)
        {
            EncoderHight = values[ModbusRegs.EncoderHigh];
            EncoderLow = values[ModbusRegs.EncoderLow];
            TensionerMode = values[ModbusRegs.TensionerMode];
            PickAxles = values[ModbusRegs.PickAxles];
            ReleaseAxles = values[ModbusRegs.ReleaseAxles];
            ProtectiveFilm = values[ModbusRegs.ProtectiveFilm];
            DoubleLine = values.ToFloat(ModbusRegs.TensionerMode);
            MasterMotorState = values[ModbusRegs.MasterMotorState];
            TicketAlarm = values[ModbusRegs.TicketAlarm];
            MotorState = values[ModbusRegs.MotorState];
            WorkState = values[ModbusRegs.WorkState];
            DeviceReady = values[ModbusRegs.DeviceReady];
            Alarm = values[ModbusRegs.Alarm];
            TicketFinish = values[ModbusRegs.TicketFinish];
            DeviceRunning = values[ModbusRegs.DeviceRunning];
            RealTimeWeight = values.ToFloat(ModbusRegs.RealtimeWeight);
            FinalWeight = values.ToFloat(ModbusRegs.FinalWeight);
            SlaveMotorWorkMode = values[ModbusRegs.SlaveMotorWorkMode];
            MasterMotorSpeed = values.ToFloat(ModbusRegs.MasterMotorSpeed);
            SlaveMotorRatio = values.ToFloat(ModbusRegs.SlaveMotorRatio);
            SetTensionerPosion = values[ModbusRegs.SetTensionerPosion];
            SwingMotorRatio = values.ToFloat(ModbusRegs.SwingMotorRatio);
            CurrentPositionX = values.ToFloat(ModbusRegs.CurrentPositionX);
            CurrentPositionY = values.ToFloat(ModbusRegs.CurrentPositionY);
            CurrentPositionZ = values.ToFloat(ModbusRegs.CurrentPositionZ);
            SetScram = values[ModbusRegs.SetScram];
            ResetPLC = values[ModbusRegs.ResetPLC];
            ResetEncoder = values[ModbusRegs.ResetEncoder];
            PLCStart_stop = values[ModbusRegs.PLCStart_stop];
            Ticket = values[ModbusRegs.Ticket];
            TicketPositionX = values.ToFloat(ModbusRegs.TicketPositionX);
            TicketAlarmCount = values[ModbusRegs.TicketAlarmCount];
            TicketPositionY = values.ToFloat(ModbusRegs.TicketPositionY);
            Buzzer = values[ModbusRegs.Buzzer];
            ForwardBackMode = values[ModbusRegs.ForwardBackMode];
            ReadyForTicket = values[ModbusRegs.ReadyForTicket];
            SetManualMode = values[ModbusRegs.SetManualMode];
            ManualDebugPLC = values[ModbusRegs.ManualDebugPLC];
            ManualDebugTicket = values[ModbusRegs.ManualDebugTicket];
            IndicatorLight = values[ModbusRegs.IndicatorLight];
            TensionSpeed = values.ToFloat(ModbusRegs.TensionSpeed);
            WindingSpeed = values.ToFloat(ModbusRegs.WindingSpeed);
            TicketFallingHeight = values.ToFloat(ModbusRegs.TicketFallingHeight);
            MainSpeed = values.ToFloat(ModbusRegs.MainSpeed);
            ShieldSetting = values[ModbusRegs.ShieldSetting];
            WindingTensionSetValue = values.ToFloat(ModbusRegs.WindingTensionSetValue);
            WindingTensionGetValue = values.ToFloat(ModbusRegs.WindingTensionGetValue);
            InspectionTensionSetValue = values.ToFloat(ModbusRegs.InspectionTensionSetValue);
            InspectionTensionGetValue = values.ToFloat(ModbusRegs.InspectionTensionGetValue);
            WeijinCount = values.ToUInt32(ModbusRegs.WeijinCount);
            WasteEncoderValue = values.ToFloat(ModbusRegs.CurrentPositionX);
            ClothWidthGetValue = values.ToFloat(ModbusRegs.ClothWidthGetValue);
            AxlesErrorCode = values.ToInt32(ModbusRegs.AxlesErrorCode);
            OnePlusLength = values.ToFloat(ModbusRegs.OnePlusLength);
            VD170 = values.ToFloat(85);
            VD174 = values.ToFloat(87);
            VD178 = values.ToFloat(89);
            VD182 = values.ToFloat(91);
            VD186 = values.ToFloat(93);
            VD190 = values.ToFloat(95);
            VD194 = values.ToFloat(97);
            VD198 = values.ToFloat(99);
            VD202 = values.ToInt32(101);
            VD206 = values.ToInt32(103);
            VD210 = values.ToInt32(105);
            VD214 = values.ToInt32(107);
            ColourDifferenceReady = values[ModbusRegs.ColourDifferenceReady];
            ThicknessDisplacement = values.ToFloat(ModbusRegs.ThicknessDisplacement);
            ThicknessDisplacement2 = values.ToFloat(ModbusRegs.ThicknessDisplacement2);
        }

        public List<string> GetAllAlarmList()
        {
            List<string> list = new List<string>();
            list.AddRange(GetMotorStateAlarmList());
            list.AddRange(GetAlarmList());
            list.AddRange(GetTicketAlarmList());
            return list;
        }

        public List<string> GetAlarmList()
        {
            List<string> list = new List<string>();

            //轴报警明细
            int axlesNo = -1;
            ushort axlesDetailErrorCode = 0;
            if (AxlesErrorCode > 0)
            {
                byte[] axlesCodeBytes = BitConverter.GetBytes(AxlesErrorCode);
                axlesNo = BitConverter.ToUInt16(axlesCodeBytes, 2);//报警轴代码
                axlesDetailErrorCode = BitConverter.ToUInt16(axlesCodeBytes, 0);//错误明细代码                
            }

            BitArray baAlarms = new BitArray(new int[] { Alarm });
            for (int i = 0; i < 16; i++)
            {
                if (baAlarms[i])
                {
                    if (axlesNo > 0 && i - 7 == axlesNo
                        && axlesDetailErrorCode > 0)//当前轴有报警，后面追加报警明细                    
                    {
                        string alarmText = Alarms[i] + "," + ParseAxlesAlarmCode(axlesDetailErrorCode);
                        list.Add(alarmText);                        
                    }
                    else
                        list.Add(Alarms[i]);
                }
            }
            if (ShieldSetting_wbjc)
            {
                list.Add(rm.GetString("Alarm16"));
            }
            return list;
        }

        public List<string> GetTicketAlarmList()
        {
            List<string> list = new List<string>();

            BitArray baTicketAlarm = new BitArray(new int[] { TicketAlarm });

            for (int i = 0; i < 6; i++)
            {
                if (baTicketAlarm[i])
                {
                    list.Add(TicketAlarms[i]);
                }
            }

            return list;
        }

        private string ParseAxlesAlarmCode(ushort alarmCode)
        {
            if (alarmCode == 0)
                return string.Empty;

            if (alarmCode > 0 && alarmCode < AxlesAlarms.Length)
                return AxlesAlarms[alarmCode - 1];
            else
                return $"未知轴错误,错误代码：{alarmCode}";
        }

        #region 位操作
        private bool GetBit(ushort value,int offset)
        {
            return (value >> offset & 1) == 1;
        }
        private void SetBit(ref ushort value,int offset, bool bit)
        {
            value = (ushort)(bit ? value | 1u << offset : value & ~(1u << offset));
        }
        #endregion
    }
}
