using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCommon.Utils
{
    public class RegeditHelper
    {
        public static string ReadRegedit(string KeyName)
        {
            string readStr = "";
            try
            {
                RegistryKey rsg = null;
                rsg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft", true);
                if (rsg.GetValue(KeyName) != null)
                {
                    readStr = rsg.GetValue(KeyName).ToString();                                                            //读取值
                }
                else
                {
                    readStr = "该键不存在！";
                }
                rsg.Close();
            }
            catch (Exception ex)
            {
                readStr = ex.Message;
            }
            return readStr;
        }

        public static void WriteRegedit(string KeyName,string KeyValue)
        {
            try
            {
                RegistryKey rsg = null;
                if (Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft").SubKeyCount <= 0)
                {
                    Registry.CurrentUser.DeleteSubKey("SOFTWARE\\Microsoft");
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft");
                }
                rsg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft", true);
                rsg.SetValue(KeyName, KeyValue);
                rsg.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
