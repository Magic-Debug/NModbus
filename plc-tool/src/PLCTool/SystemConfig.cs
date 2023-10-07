using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Specialized;

namespace PLCTool
{
    public class SystemConfig
    {
        private static string ConfigFile;
        private static Dictionary<string, string> AppSettings;
        static SystemConfig()
        {
            AppSettings = new Dictionary<string, string>();
            var DefaultSettings = ConfigurationManager.AppSettings;
            foreach (string key in DefaultSettings)
            {
                AppSettings.Add(key, DefaultSettings[key]);
            }
            ConfigFile = "PLCTool.exe.config";
        }
        public static void LoadSettings(string configfile)
        {
            if (!File.Exists(configfile))
            {
                throw new FileNotFoundException(configfile);
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(configfile);
            XmlNodeList nodelist = doc.SelectNodes(@"//appSettings//add");
            foreach (XmlNode node in nodelist)
            {
                string key = node.Attributes["key"].Value;
                string value = node.Attributes["value"].Value;
                if (AppSettings.ContainsKey(key))
                {
                    AppSettings[key] = value;
                }
            }
            ConfigFile = configfile;

        }
        public static string GetConfigValues(string key)
        {
            return AppSettings[key];
        }
        public static void SaveConfigValue(string key, string value)
        {
            if (!File.Exists(ConfigFile))
            {
                File.Copy("PLCTool.exe.config", ConfigFile);
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFile);
            SetXmlVlue(doc, key, value);
            doc.Save(ConfigFile);
            LoadSettings(ConfigFile);
        }
        public static void SaveConfigValues(List<KeyValuePair<string, string>> KeyValuePairs)
        {
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(ConfigFile))
            {
                File.Copy("PLCTool.exe.config", ConfigFile);
            }
            doc.Load(ConfigFile);
            foreach (var k in KeyValuePairs)
            {
                SetXmlVlue(doc, k.Key, k.Value);
            }
            doc.Save(ConfigFile);
            LoadSettings(ConfigFile);
        }
        private static void SetXmlVlue(XmlDocument doc, string key, string value)
        {
            XmlNode settingsnode = doc.SelectSingleNode(@"//appSettings");
            XmlNode node = settingsnode.SelectSingleNode($"add[@key='{key}']");
            if (node == null)
            {
                XmlElement newnode = doc.CreateElement("add");
                newnode.SetAttribute("key", key);
                newnode.SetAttribute("value", value);
                settingsnode.AppendChild(newnode);
            }
            else
            {
                node.Attributes["value"].Value = value;
            }
        }
        //删除设置
        public static void ClearSetting()
        {
            if (File.Exists(ConfigFile))
            {
                File.Delete(ConfigFile);
            }
        }
    }
}
