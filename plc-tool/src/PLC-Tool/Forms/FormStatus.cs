using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainFrom;
using LightColor = PLCTool.UC.UC_Light.LightColor;

namespace PLCTool
{
    public partial class FormStatus : BaseForm
    {
        private ushort[] Data = new ushort[7];
        private LightTag[] Tags1 = new LightTag[]
        {
            new LightTag("编码器A相",0,LightColor.Red,"I0.0"),new LightTag("编码器B相",1,LightColor.Red,"I0.1"),
            new LightTag("X轴正限位",2,LightColor.Red,"I0.2"),new LightTag("X轴负限位",3,LightColor.Red,"I0.3"),
            new LightTag("X轴原点",4,LightColor.Red,"I0.4"),new LightTag("Z轴正限位",5,LightColor.Red,"I0.5"),
            new LightTag("Z轴负限位",6,LightColor.Red,"I0.6"),new LightTag("Z轴原点",7,LightColor.Red,"I0.7"),

            new LightTag("A轴正限位",8,LightColor.Red,"I1.0"),new LightTag("A轴负限位",9,LightColor.Red,"I1.1"),
            new LightTag("A轴原点",10,LightColor.Red,"I1.2"),new LightTag("紧急停止",11,LightColor.Red,"I1.3"),
            new LightTag("启动",12,LightColor.Red,"I1.4"),new LightTag("停止",13,LightColor.Red,"I1.5"),
            new LightTag("归零",14,LightColor.Red,"I1.6"),new LightTag("放卷处无布检测",15,LightColor.Red,"I1.7"),

            new LightTag("放卷处下限检测",16,LightColor.Red,"I2.0"),new LightTag("进布上料无布检测",17,LightColor.Red,"I2.1"),
            new LightTag("进布下限检测",18,LightColor.Red,"I2.2"),new LightTag("伸出气缸前限位",19,LightColor.Red,"I2.3"),
            new LightTag("伸出气缸后限位",20,LightColor.Red,"I2.4"),new LightTag("卷标到位检测",21,LightColor.Red,"I2.5"),
            new LightTag("复位",22,LightColor.Red,"I2.6"),new LightTag("",23,LightColor.Red,"I2.7"),

            new LightTag("验布站主动辊变频器报警",24,LightColor.Red,"I3.0"),new LightTag("验布站张力辊变频器报警",25,LightColor.Red,"I3.1"),
            new LightTag("进布站进布辊变频器报警",26,LightColor.Red,"I3.2"),new LightTag("进布站分丝辊变频器报警",27,LightColor.Red,"I3.3"),
            new LightTag("收卷站收卷辊变频器报警",28,LightColor.Red,"I3.4"),new LightTag("收卷站分丝辊变频器报警",29,LightColor.Red,"I3.5"),
            new LightTag("开卷站主动辊变频器报警",30,LightColor.Red,"I3.6"),new LightTag("摆布变频器报警",31,LightColor.Red,"I3.7"),

            new LightTag("放卷伺服正极限",48,LightColor.Red,"I6.0"),new LightTag("放卷伺服负极限",49,LightColor.Red,"I6.1"),
            new LightTag("放卷伺服原点",50,LightColor.Red,"I6.2"),new LightTag("收卷伺服正极限",51,LightColor.Red,"I6.3"),
            new LightTag("收卷伺服负极限",52,LightColor.Red,"I6.4"),new LightTag("收卷伺服原点",53,LightColor.Red,"I6.5"),
            new LightTag("启动",54,LightColor.Red,"I6.6"),new LightTag("停止",55,LightColor.Red,"I6.7"),

            new LightTag("收卷传感器1-1",56,LightColor.Red,"I7.0"),new LightTag("收卷传感器1-2",57,LightColor.Red,"I7.1"),
            new LightTag("放卷传感器1-1",58,LightColor.Red,"I7.2"),new LightTag("放卷传感器1-2",59,LightColor.Red,"I7.3"),
            new LightTag("",60,LightColor.Red,"I7.4"),new LightTag("",61,LightColor.Red,"I7.5"),
            new LightTag("",62,LightColor.Red,"I7.6"),new LightTag("",63,LightColor.Red,"I7.7")
        };
        private LightTag[] Tags2 = new LightTag[]
        {
            new LightTag("X轴脉冲",64,LightColor.Green,"Q0.0"),new LightTag("Z轴脉冲",65,LightColor.Green,"Q0.1"),
            new LightTag("X轴方向",66,LightColor.Green,"Q0.2"),new LightTag("A轴脉冲",67,LightColor.Green,"Q0.3"),
            new LightTag("X轴使能",68,LightColor.Green,"Q0.4"),new LightTag("Z轴使能",69,LightColor.Green,"Q0.5"),
            new LightTag("A轴使能",70,LightColor.Green,"Q0.6"),new LightTag("Z轴方向",71,LightColor.Green,"Q0.7"),

            new LightTag("A轴方向",72,LightColor.Green,"Q1.0"),new LightTag("三色灯-绿",73,LightColor.Green,"Q1.1"),
            new LightTag("三色灯-黄",74,LightColor.Green,"Q1.2"),new LightTag("三色灯-红",75,LightColor.Green,"Q1.3"),
            new LightTag("三色灯-蜂鸣器",76,LightColor.Green,"Q1.4"),new LightTag("分边直流电机1",77,LightColor.Green,"Q1.5"),
            new LightTag("分边直流电机2",78,LightColor.Green,"Q1.6"),new LightTag("摆布对中启停",79,LightColor.Green,"Q1.7"),

            new LightTag("急停输出-电机断电",80,LightColor.Green,"Q2.0"),new LightTag("真空电磁阀",81,LightColor.Green,"Q2.1"),
            new LightTag("气缸电磁阀",82,LightColor.Green,"Q2.2"),new LightTag("复位输出",83,LightColor.Green,"Q2.3"),
            new LightTag("纠偏器自动",84,LightColor.Green,"Q2.4"),new LightTag("纠偏器回中",85,LightColor.Green,"Q2.5"),
            new LightTag("",86,LightColor.Green,"Q2.6"),new LightTag("",87,LightColor.Green,"Q2.7"),

            new LightTag("",88,LightColor.Green,"Q3.0"),new LightTag("",89,LightColor.Green,"Q3.1"),
            new LightTag("",90,LightColor.Green,"Q3.2"),new LightTag("",91,LightColor.Green,"Q3.3"),
            new LightTag("",92,LightColor.Green,"Q3.4"),new LightTag("",93,LightColor.Green,"Q3.5"),
            new LightTag("",94,LightColor.Green,"Q3.6"),new LightTag("",95,LightColor.Green,"Q3.7"),

            new LightTag("放卷脉冲",96,LightColor.Green,"Q4.0"),new LightTag("收卷脉冲",97,LightColor.Green,"Q4.1"),
            new LightTag("放卷方向",98,LightColor.Green,"Q4.2"),new LightTag("",99,LightColor.Green,"Q4.3"),
            new LightTag("",100,LightColor.Green,"Q4.4"),new LightTag("",101,LightColor.Green,"Q4.5"),
            new LightTag("",102,LightColor.Green,"Q4.6"),new LightTag("放卷方向",103,LightColor.Green,"Q4.7"),

            new LightTag("",104,LightColor.Green,"Q5.0"),new LightTag("",105,LightColor.Green,"Q5.1"),
            new LightTag("",106,LightColor.Green,"Q5.2"),new LightTag("",107,LightColor.Green,"Q5.3"),
            new LightTag("",108,LightColor.Green,"Q5.4"),new LightTag("",109,LightColor.Green,"Q5.5"),
            new LightTag("",110,LightColor.Green,"Q5.6"),new LightTag("",111,LightColor.Green,"Q5.7")
        };

        public FormStatus()
        {
            InitializeComponent();
        }

        private bool IsPMD = false;
        private void ShowData()
        {
            if (IsPMD)
            {
                Random random = new Random();
                for (int i = 0; i < 7; i++)
                {
                    Data[i] = (ushort)(random.Next() & 0xffff);
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    Data[i] = Common.GetInstance().modbusValues[ModbusRegs.IOStatus + i];
                }
            }
            for (int i = 0; i < Tags1.Length; i++)
            {
                UC.UC_Light light = (UC.UC_Light)tableLayoutPanel1.Controls[i];
                light.Status = (Data[Tags1[i].Bit / 16] >> Tags1[i].Bit % 16 & 1) == 1;
            }
            for (int i = 0; i < Tags2.Length; i++)
            {
                UC.UC_Light light = (UC.UC_Light)tableLayoutPanel2.Controls[i];
                light.Status = (Data[Tags2[i].Bit / 16] >> Tags2[i].Bit % 16 & 1) == 1;
            }
        }
        private void FormmStatus_Load(object sender,EventArgs e)
        {
            cbbIColor.SelectedIndex = 0;
            cbbQColor.SelectedIndex = 3;
            chkRandom.Checked = false;
            for (int i = 0; i < Tags1.Length; i++)
            {
                UC.UC_Light light = new UC.UC_Light();
                light.Title = Tags1[i].Title;
                light.Color = Tags1[i].Color;
                light.LightLabel = Tags1[i].LightLabel;
                tableLayoutPanel1.Controls.Add(light, i / 16, i % 16);
            }
            for (int i = 0; i < Tags2.Length; i++)
            {
                UC.UC_Light light = new UC.UC_Light();
                light.Title = Tags2[i].Title;
                light.Color = Tags2[i].Color;
                light.LightLabel = Tags2[i].LightLabel;
                tableLayoutPanel2.Controls.Add(light, i / 16, i % 16);
            }
            timer1.Start();
            cbbIColor.SelectedIndexChanged += new EventHandler(cbbColor_SelectedIndexChanged);
            cbbQColor.SelectedIndexChanged += new EventHandler(cbbColor_SelectedIndexChanged);
            chkRandom.CheckedChanged += new EventHandler(chkRandom_CheckedChanged);
        }
        private struct LightTag
        {
            public string Title;
            public int Bit;
            public LightColor Color;
            public string LightLabel;
            public LightTag(string title, int bit, LightColor color, string lightLabel = "")
            {
                Title = title;
                Bit = bit;
                Color = color;
                LightLabel = lightLabel;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowData();
        }

        private void cbbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            LightColor color;
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    color = LightColor.Red;
                    break;
                case 1:
                    color = LightColor.Yello;
                    break;
                case 2:
                    color = LightColor.Blue;
                    break;
                default:
                    color = LightColor.Green;
                    break;
            }
            if (sender.Equals(cbbIColor))
                for (int i = 0; i < 48; i++)
                {
                    ((UC.UC_Light)tableLayoutPanel1.Controls[i]).Color = color;
                }
            else
                for (int i = 0; i < 48; i++)
                {
                    ((UC.UC_Light)tableLayoutPanel2.Controls[i]).Color = color;
                }
        }

        private void chkRandom_CheckedChanged(object sender, EventArgs e)
        {
            IsPMD = chkRandom.Checked;
        }
    }
}
