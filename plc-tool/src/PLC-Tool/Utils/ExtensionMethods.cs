using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace FrameworkCommon.Utils
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// 获取枚举类型的描述信息
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>描述信息</returns>
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        /// <summary>
        /// 获取程序集的配置文件
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>配置</returns>
        public static Configuration GetConfiguration(this Assembly assembly)
        {
            var map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = assembly.Location + ".config";
            return ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }
    }
}