using System;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.IO;
using PLCTool.UC;
using MainFrom;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PLCTool.Forms
{
    public partial class FormIOStatus : BaseForm
    {
        public FormIOStatus()
        {
            InitializeComponent();
        }

        static ILogger<FormIOStatus>? Logger => Program.Services.GetService<ILogger<FormIOStatus>>();

        private int machinetype;

        private string GetDefaultSettingFileName(int machinetype)
        {
            string subFilePath = (machinetype == 1 ? "Servo" : "Invertor") + $".{SystemConfig.GetConfigValues("Language")}.xml";
            return Path.Combine(Common.GetInstance().ConfigDirectory, subFilePath);
        }

        /// <summary>
        /// 加载传感器
        /// </summary>
        private void InitSensors(string configFilePath)
        {
            if (File.Exists(configFilePath))
            {
                LoadSensorsFromConfigFile(configFilePath);
            }
            else//以默认配置实例化
            {
                LoadSensorsAsDefault();
            }
        }

        private void LoadSensorsFromConfigFile(string configFilePath)
        {
            try
            {
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel2.Controls.Clear();

                XmlDocument doc = new XmlDocument();
                doc.Load(configFilePath);
                XmlElement xmlRoot = (XmlElement)doc.SelectSingleNode("//setting");

                List<UC_Sensor> sensors;
                TableLayoutPanel tableLayoutPanel;
                int nowRow = 0, nowCol = 0;
                string nowGroupName, lastGroupName = "";
                Bitmap bitTrueImage, bitFalseImage = Properties.Resources.LightOff;
                foreach (XmlElement xmlGroup in xmlRoot.ChildNodes)
                {
                    bitTrueImage = xmlGroup.Name.IndexOf("I") >= 0 ? Properties.Resources.LightRed : Properties.Resources.LightGreen;
                    sensors = GetSensorsByGroup(xmlGroup, bitTrueImage, bitFalseImage);
                    tableLayoutPanel = xmlGroup.Name.IndexOf("PLC1") >= 0 ? tableLayoutPanel1 : tableLayoutPanel2;
                    nowGroupName = xmlGroup.Name.Substring(0, 4);
                    if (nowGroupName != lastGroupName)
                    {
                        nowRow = 0;
                        nowCol = 0;
                        lastGroupName = nowGroupName;
                    }
                    else
                    {
                        nowRow = 0;
                        nowCol++;
                    }

                    AddSensors(tableLayoutPanel, sensors, ref nowRow, ref nowCol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSensorsAsDefault()
        {
            ComponentResourceManager resource = new ComponentResourceManager(machinetype == 0 ? typeof(PLC.InvertorIO) : typeof(PLC.ServoIO));

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel2.Controls.Clear();
            int nowRow = 0, nowCol = 0;
            List<UC_Sensor> sensors;
            //PLC1_I
            sensors = GetSensorsByGroup(resource, "PLC1I", 5, 75, Properties.Resources.LightRed, Properties.Resources.LightOff);
            AddSensors(tableLayoutPanel1, sensors, ref nowRow, ref nowCol);
            //PLC1_Q
            nowRow = 0;
            nowCol++;
            sensors = GetSensorsByGroup(resource, "PLC1Q", 4, 79, Properties.Resources.LightGreen, Properties.Resources.LightOff);
            AddSensors(tableLayoutPanel1, sensors, ref nowRow, ref nowCol);
            //PLC2_I
            nowRow = 0; nowCol = 0;
            sensors = GetSensorsByGroup(resource, "PLC2I", 3, 78, Properties.Resources.LightRed, Properties.Resources.LightOff, false);
            AddSensors(tableLayoutPanel2, sensors, ref nowRow, ref nowCol);
            //PLC2_Q
            nowRow = 0;
            nowCol++;
            sensors = GetSensorsByGroup(resource, "PLC2Q", 2, 81, Properties.Resources.LightGreen, Properties.Resources.LightOff);
            AddSensors(tableLayoutPanel2, sensors, ref nowRow, ref nowCol);
        }

        private void SaveConfig(string savePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(declaration);
                XmlElement xmlRoot = doc.CreateElement("setting");
                doc.AppendChild(xmlRoot);

                string chilNodeName;
                UC_Sensor tempSonser;
                XmlElement xmlGroup, xmlRegister, xmlSensor;
                List<KeyValuePair<string, string>> childNodeAttributes = new List<KeyValuePair<string, string>>();
                Control[] sensors = new Control[tableLayoutPanel1.Controls.Count + tableLayoutPanel1.Controls.Count];
                tableLayoutPanel1.Controls.CopyTo(sensors, 0);
                tableLayoutPanel2.Controls.CopyTo(sensors, tableLayoutPanel1.Controls.Count);
                foreach (Control ctlTemp in sensors)
                {
                    tempSonser = (UC_Sensor)ctlTemp;
                    if (tempSonser == null)
                        continue;

                    //获取分组节点
                    chilNodeName = tempSonser.GroupName.Substring(0, 4) + "-" + tempSonser.GroupName.Substring(4, 1);
                    childNodeAttributes.Clear();
                    xmlGroup = GetOrAddSpecialChildNode(doc, xmlRoot, chilNodeName, childNodeAttributes, true);
                    //获取寄存器节点并添加到分组节点
                    chilNodeName = "RegByte";
                    childNodeAttributes.Clear();
                    childNodeAttributes.Add(new KeyValuePair<string, string>("RegByteAddress", tempSonser.RegisterByteAddress.ToString()));
                    xmlRegister = GetOrAddSpecialChildNode(doc, xmlGroup, chilNodeName, childNodeAttributes, true);
                    //获取传感器节点
                    chilNodeName = "RegBit";
                    childNodeAttributes.Clear();
                    childNodeAttributes.Add(new KeyValuePair<string, string>("bit", tempSonser.RegisterBit.ToString()));
                    childNodeAttributes.Add(new KeyValuePair<string, string>("name", tempSonser.SensorName));
                    xmlSensor = GetOrAddSpecialChildNode(doc, xmlRegister, chilNodeName, childNodeAttributes, true);

                    //刷新分组节点的元素数量属性
                    xmlGroup.SetAttribute("count", xmlGroup.ChildNodes.Count.ToString());
                }

                doc.Save(savePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 添加该组传感器到界面
        /// </summary>
        /// <param name="tlpParent"></param>
        /// <param name="groupSensors"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startColIndex"></param>
        private void AddSensors(TableLayoutPanel tlpParent, List<UC_Sensor> groupSensors, ref int startRowIndex, ref int startColIndex)
        {
            foreach (UC_Sensor sensor in groupSensors)
            {
                tlpParent.Controls.Add(sensor, startColIndex, startRowIndex);
                startRowIndex++;
                if (startRowIndex >= tlpParent.RowCount)
                {
                    startRowIndex = 0;
                    startColIndex++;
                }
                Logger.LogInformation(sensor.ToString());
            }
        }

        private List<UC_Sensor> GetSensorsByGroup(ComponentResourceManager resource, string groupName, byte sensorsNum, byte startRegisterAddress, Bitmap bitTrueImage, Bitmap bitFalseImage, bool addressIsInc = true)
        {
            List<UC_Sensor> sensors = new List<UC_Sensor>();

            UC_Sensor sensor;
            byte registerAddress = startRegisterAddress;
            for (byte i = 0; i < sensorsNum; i++)
            {
                for (byte j = 0; j < 8; j++)
                {
                    sensor = new UC_Sensor(registerAddress, j);
                    sensor.SensorName = resource.GetString($"{groupName}{i}_{j}");
                    sensor.GroupName = groupName + i;
                    sensor.RegisterBit = (byte)(i % 2 * 8 + j);//每个传感器一个字节
                    sensor.TrueStatusImage = bitTrueImage;
                    sensor.FalseStatusImage = bitFalseImage;
                    sensors.Add(sensor);

                    //添加单击事件
                    if (Common.GetInstance().IsSuperAdmin)
                    {
                        sensor.DoubleClick += new EventHandler(Sensor_DoubleClick);
                    }
                }

                //每2个传感器，寄存器地址增/减1
                if (i % 2 == 1)
                {
                    registerAddress = (byte)(registerAddress + (addressIsInc ? 1 : -1));
                }
            }
            return sensors;
        }

        private List<UC_Sensor> GetSensorsByGroup(XmlElement xmlGroup, Bitmap bitTrueImage, Bitmap bitFalseImage)
        {
            List<UC_Sensor> sensors = new List<UC_Sensor>();

            try
            {
                UC_Sensor sensor;
                string groupNodeName, groupName, registerByteAddressStr, sensorName, sensorBitStr;
                byte registerByteAddress, registerAddress = 0, registerBit;

                //分组名称
                groupNodeName = xmlGroup.Name;
                groupName = groupNodeName.Replace("-", "");

                //寄存器节点
                for (int i = 0; i < xmlGroup.ChildNodes.Count; i++)
                {
                    registerByteAddressStr = xmlGroup.ChildNodes[i].Attributes["RegByteAddress"].Value;
                    if (byte.TryParse(registerByteAddressStr, out registerByteAddress))
                        registerAddress = (byte)(registerByteAddress / 2);
                    //传感器节点                
                    foreach (XmlElement xmlSensor in xmlGroup.ChildNodes[i].ChildNodes)
                    {
                        sensorName = xmlSensor.Attributes["name"].Value;
                        sensorBitStr = xmlSensor.Attributes["bit"].Value;
                        byte.TryParse(sensorBitStr, out registerBit);

                        sensor = new UC_Sensor(registerAddress, registerBit);
                        sensor.SensorName = sensorName;
                        sensor.GroupName = groupName + i;
                        sensor.TrueStatusImage = bitTrueImage;
                        sensor.FalseStatusImage = bitFalseImage;
                        sensors.Add(sensor);

                        //添加单击事件
                        if (Common.GetInstance().IsSuperAdmin)
                        {
                            sensor.DoubleClick += new EventHandler(Sensor_DoubleClick);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sensors;
        }

        private XmlElement GetOrAddSpecialChildNode(XmlDocument doc, XmlElement xmlParent, string childName, List<KeyValuePair<string, string>> childAttributes = null, bool notExistsThenAdd = false)
        {
            XmlElement xmlSpecialChild = null;
            bool attributeValuesIsEqual;
            foreach (XmlElement xmlChild in xmlParent.ChildNodes)
            {
                if (xmlChild.Name == childName)//找到该名称子节点
                {
                    //检查属性是否相等
                    attributeValuesIsEqual = true;
                    if (childAttributes != null)
                    {
                        foreach (KeyValuePair<string, string> kvpAttribute in childAttributes)
                        {
                            if (xmlChild.Attributes[kvpAttribute.Key].Value != kvpAttribute.Value)
                            {
                                attributeValuesIsEqual = false;
                                break;
                            }
                        }
                    }

                    //完全相等
                    if (attributeValuesIsEqual)
                    {
                        xmlSpecialChild = xmlChild;
                        break;
                    }
                }
            }

            //未找到,新建     
            if (xmlSpecialChild == null && notExistsThenAdd)
            {
                xmlSpecialChild = doc.CreateElement(childName);
                if (childAttributes != null)//设置属性
                {
                    foreach (KeyValuePair<string, string> kvpAttribute in childAttributes)
                    {
                        xmlSpecialChild.SetAttribute(kvpAttribute.Key, kvpAttribute.Value);
                    }
                }
                xmlParent.AppendChild(xmlSpecialChild);
            }

            return xmlSpecialChild;
        }

        private void Sensor_DoubleClick(object sender, EventArgs e)
        {
            UC_Sensor nowSensor = (UC_Sensor)sender;
            FormEditIOName form = new FormEditIOName();
            form.IOName = nowSensor.SensorName;
            form.Text += $" {nowSensor.GroupSubName}";
            if (form.ShowDialog() == DialogResult.OK)
            {
                nowSensor.SensorName = form.IOName;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                InitSensors(openFileDialog1.FileName);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                SaveConfig(saveFileDialog1.FileName);
                MessageBox.Show(ExportSuccessMsg);
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            InitSensors("");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig(GetDefaultSettingFileName(machinetype));
            MessageBox.Show(SaveSuccessMsg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ushort[] PLCData = Common.GetInstance().modbusValues;
            UC_Sensor ucTemp;
            //刷新tableLayoutPanel1
            foreach (Control ctlTemp in tableLayoutPanel1.Controls)
            {
                if (ctlTemp is UC_Sensor)
                {
                    ucTemp = (UC_Sensor)ctlTemp;
                    ucTemp.Status = (PLCData[ucTemp.RegisterAddress] >> ucTemp.RegisterBit & 1) == 1;
                }
            }
            //刷新tableLayoutPanel2
            foreach (Control ctlTemp in tableLayoutPanel2.Controls)
            {
                if (ctlTemp is UC_Sensor)
                {
                    ucTemp = (UC_Sensor)ctlTemp;
                    ucTemp.Status = (PLCData[ucTemp.RegisterAddress] >> ucTemp.RegisterBit & 1) == 1;
                }
            }
        }

        private void FormIOStatus_Load(object sender, EventArgs e)
        {
            machinetype = Convert.ToInt32(SystemConfig.GetConfigValues("MachineType"));
            string defaultConfigFilePath = GetDefaultSettingFileName(machinetype);
            InitSensors(defaultConfigFilePath);

            if (Common.GetInstance().IsSuperAdmin)
            {
                Height += 40;
                btnImport.Visible = true;
                btnExport.Visible = true;
                btnDefault.Visible = true;
                btnSave.Visible = true;
            }
            timer1.Start();
        }

        #region 多语言
        string ExportSuccessMsg => LanguageResource.FormIOStatus_ExportSuccessMsg;
        string SaveSuccessMsg => LanguageResource.FormIOStatus_SaveSuccessMsg;
        #endregion
    }
}
