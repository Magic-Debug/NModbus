using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainFrom;

namespace PLCTool.PLC
{
    public class AutoSetting
    {
        private List<KeyValuePair<ushort[], ushort[]>> Settings = new List<KeyValuePair<ushort[], ushort[]>>();
        private static List<ItemOption> SettingKeyOptions { get; }
        private static List<ItemOption> SettingValueOptions { get; }

        static AutoSetting()
        {
            SettingKeyOptions = new List<ItemOption>
            {
                new ItemOption { RegAddress = ModbusRegs.MasterMotorSpeed, Length = 2 },
                new ItemOption { RegAddress = ModbusRegs.ForwardBackMode, Length = 1 },
                new ItemOption { RegAddress = ModbusRegs.TensionerMode, Length = 1 }
            };
            SettingValueOptions = new List<ItemOption>
            {
                new ItemOption { RegAddress = ModbusRegs.SlaveMotorRatio, Length = 2 },
                new ItemOption { RegAddress = ModbusRegs.SwingMotorRatio, Length = 2 },
                new ItemOption { RegAddress = ModbusRegs.TensionSpeed, Length = 2 },
                new ItemOption { RegAddress = ModbusRegs.WindingSpeed, Length = 2 }
            };
        }

        public void SaveSettings(string filename)
        {
            BinaryWriter writer = new BinaryWriter(new FileStream(filename, FileMode.Create));
            writer.Write(SettingKeyOptions.Count);
            foreach (ItemOption keyoption in SettingKeyOptions)
            {
                writer.Write(keyoption.RegAddress);
                writer.Write(keyoption.Length);
            }
            writer.Write(SettingValueOptions.Count);
            foreach (ItemOption valueoption in SettingValueOptions)
            {
                writer.Write(valueoption.RegAddress);
                writer.Write(valueoption.Length);
            }
            writer.Write(Settings.Count);
            foreach (KeyValuePair<ushort[], ushort[]> keyvalue in Settings)
            {
                foreach (ushort k in keyvalue.Key)
                {
                    writer.Write(k);
                }
                foreach (ushort v in keyvalue.Value)
                {
                    writer.Write(v);
                }
            }
            writer.Close();
        }
        public void LoadSettings(string filename)
        {
            Settings.Clear();
            BinaryReader reader = new BinaryReader(new FileStream(filename, FileMode.Open));
            try
            {
                List<ItemOption> keyoptions = new List<ItemOption>();
                List<ItemOption> valueoptions = new List<ItemOption>();
                int keyoptioncount = reader.ReadInt32();
                for (int i = 0; i < keyoptioncount; i++)
                {
                    ItemOption keyoption = new ItemOption();
                    keyoption.RegAddress = reader.ReadByte();
                    keyoption.Length = reader.ReadByte();
                    keyoptions.Add(keyoption);
                }
                int valueoptioncount = reader.ReadInt32();
                for (int i = 0; i < valueoptioncount; i++)
                {
                    ItemOption valueoption = new ItemOption();
                    valueoption.RegAddress = reader.ReadByte();
                    valueoption.Length = reader.ReadByte();
                    valueoptions.Add(valueoption);
                }
                if (!CheckVersion(keyoptions, valueoptions))
                {
                    return;
                }
                int keylength = keyoptions.Sum(x => x.Length);
                int valuelength = valueoptions.Sum(x => x.Length);
                int settingcount = reader.ReadInt32();
                for (int i = 0; i < settingcount; i++)
                {
                    ushort[] key = new ushort[keylength];
                    ushort[] value = new ushort[valuelength];
                    for (int j = 0; j < keylength; j++)
                    {
                        key[j] = reader.ReadUInt16();
                    }
                    for (int j = 0; j < valuelength; j++)
                    {
                        value[j] = reader.ReadUInt16();
                    }
                    Settings.Add(new KeyValuePair<ushort[], ushort[]>(key, value));
                }
            }
            catch
            {
                Settings.Clear();
            }
            finally
            {
                reader.Close();
            }
        }
        public bool ContainsRecommendSetting(ModbusStatus currentsetting)
        {
            ushort[] setting = currentsetting.ToShortValues();
            ushort[] key = GetKey(setting);
            return FindIndex(key) >= 0;
        }
        public void SetRecommendSetting(ModbusStatus currentsetting)
        {
            ushort[] setting = currentsetting.ToShortValues();
            ushort[] key = GetKey(setting);
            int index = FindIndex(key);
            ushort[] recommendsetting = Settings[index].Value;
            SetSetting(recommendsetting);
        }
        public void AddSetting(ModbusStatus currentsetting)
        {
            ushort[] setting = currentsetting.ToShortValues();
            ushort[] key = GetKey(setting);
            ushort[] value = GetValue(setting);
            AddSetting(key, value);
        }

        private int FindIndex(ushort[] key)
        {
            for (int i = 0; i < Settings.Count; i++)
            {
                KeyValuePair<ushort[], ushort[]> keyvalue = Settings[i];
                ushort[] settingkey = keyvalue.Key;
                if (IsMatchKey(key, settingkey))
                {
                    return i;
                }
            }
            return -1;
        }
        private bool IsMatchKey(ushort[] key1, ushort[] key2)
        {
            if (key1.Length != key2.Length)
            {
                return false;
            }
            for (int i = 0; i < key1.Length; i++)
            {
                if (key1[i] != key2[i])
                {
                    return false;
                }
            }
            return true;
        }
        private void SetSetting(ushort[] value)
        {
            TcpCommunication communication = TcpCommunication.GetInstance();
            int i = 0;
            foreach (ItemOption option in SettingValueOptions)
            {
                if (option.Length == 1)
                {
                    communication.WriteRegister<ushort>(option.RegAddress, value[i++]);
                }
                else if (option.Length == 2)
                {
                    float regvalue = value.ToFloat(i);
                    communication.WriteRegister<float>(option.RegAddress, regvalue);
                    i += 2;
                }
                else
                {
                    for (int j = 0; j < option.Length; j++)
                    {
                        communication.WriteRegister<ushort>((ushort)(option.RegAddress + j), value[i++]);
                    }
                }
            }
        }
        private void AddSetting(ushort[] key, ushort[] setting)
        {
            int index = FindIndex(key);
            if (index >= 0)
            {
                Settings[index] = new KeyValuePair<ushort[], ushort[]>(Settings[index].Key, setting);
            }
            else
            {
                Settings.Add(new KeyValuePair<ushort[], ushort[]>(key, setting));
            }
        }
        private ushort[] GetKey(ushort[] status)
        {
            List<ushort> list = new List<ushort>();
            foreach (ItemOption option in SettingKeyOptions)
            {
                for (int i = 0; i < option.Length; i++)
                {
                    list.Add(status[option.RegAddress + i]);
                }
            }
            return list.ToArray();
        }
        private ushort[] GetValue(ushort[] status)
        {
            List<ushort> list = new List<ushort>();
            foreach (ItemOption option in SettingValueOptions)
            {
                for (int i = 0; i < option.Length; i++)
                {
                    list.Add(status[option.RegAddress + i]);
                }
            }
            return list.ToArray();
        }
        private bool CheckVersion(List<ItemOption> keyoptions, List<ItemOption> valueoptions)
        {
            if (keyoptions.Count != SettingKeyOptions.Count || valueoptions.Count != SettingValueOptions.Count)
            {
                return false;
            }
            for (int i = 0; i < keyoptions.Count; i++)
            {
                ItemOption item1 = keyoptions[i];
                ItemOption item2 = SettingKeyOptions[i];
                if (item1.RegAddress != item2.RegAddress || item1.Length != item2.Length)
                {
                    return false;
                }
            }
            for (int i = 0; i < valueoptions.Count; i++)
            {
                ItemOption item1 = valueoptions[i];
                ItemOption item2 = SettingValueOptions[i];
                if (item1.RegAddress != item2.RegAddress || item1.Length != item2.Length)
                {
                    return false;
                }
            }
            return true;
        }
        private class ItemOption
        {
            public byte RegAddress { get; set; }
            public byte Length { get; set; }
        }
    }
}
