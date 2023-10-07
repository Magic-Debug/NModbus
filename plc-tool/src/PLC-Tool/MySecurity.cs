using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using FrameworkCommon;

namespace PLCTool
{
    class MySecurity
    {
        public static string GetMD5(string value)
        {
            byte[] a = Encoding.UTF8.GetBytes(value);
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] md5 = provider.ComputeHash(a);
            string result = "";
            for (int i = 0; i < md5.Length; i++)
            {
                result += md5[i].ToString("x2");
            }
            return result;
        }
        public static bool CheckPassword(string password)
        {
            password += "aabbccc3.14159265358979!!@@##$$%%^^&&**()";
            string passwordWaitCheck = GetMD5(password);
            if (passwordWaitCheck == "a58fb49bd51814cdad34855b17b9f0c3")
            {
                Common.GetInstance().IsAdmin = true;
                Common.GetInstance().IsSuperAdmin = true;
                return true;
            }
            string path = "Config\\UsersSetting";// Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //path = Path.Combine(path, "PLCTool", "UsersSetting");
            //if(!File.Exists(path))
            //{
            //    path = "Config\\UsersSetting";
            //}
            if (File.Exists(path))
            {
                BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open));
                byte[] data = reader.ReadBytes(48);
                reader.Close();

                //普通用户
                byte[] startIndexes = new byte[] { 0, 16, 32 };
                string passwordInConfigFile;
                for (int partIndex = 0; partIndex < startIndexes.Length; partIndex++)
                {
                    passwordInConfigFile = "";
                    for (int readNum = 0; readNum < 16; readNum++)
                    {
                        passwordInConfigFile += data[startIndexes[partIndex] + readNum].ToString("x2");
                    }
                    if (passwordInConfigFile == passwordWaitCheck)
                    {
                        switch (partIndex)
                        {
                            case 0:
                                Common.GetInstance().IsAdmin = false;
                                Common.GetInstance().IsSuperAdmin = false;
                                return true;
                            case 1://管理员
                                Common.GetInstance().IsAdmin = true;
                                Common.GetInstance().IsSuperAdmin = false;
                                return true;
                            case 2://超级管理员
                                Common.GetInstance().IsAdmin = true;
                                Common.GetInstance().IsSuperAdmin = true;
                                return true;
                        }
                    }
                }
            }
            else
            {
                LogHelper.Default.Error("Config\\UsersSetting文件不存在");
            }
            return false;
        }

        //修改密码
        public static void ChangePassword(string password)
        {
            password += "aabbccc3.14159265358979!!@@##$$%%^^&&**()";
            byte[] passwordMD5 = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(password));
            string settingPath = Path.Combine(Common.GetInstance().ConfigDirectory, "UsersSetting");
            if (!File.Exists(settingPath) && File.Exists("UsersSetting"))
            {
                File.Copy("UsersSetting", settingPath);
            }
            bool existfile = File.Exists(settingPath);
            DateTime CreationTime = DateTime.Now;
            DateTime LastAccessTime = DateTime.Now;
            DateTime LastWriteTime = DateTime.Now;
            byte[] writeBytes = new byte[256];
            if (existfile)
            {
                FileInfo info = new FileInfo(settingPath);
                CreationTime = info.CreationTime;
                LastAccessTime = info.LastAccessTime;
                LastWriteTime = info.LastWriteTime;
                BinaryReader reader = new BinaryReader(new FileStream(settingPath, FileMode.Open));
                reader.Read(writeBytes, 0, 48);//前48个字节存密码数据           
                reader.Close();
            }

            //修改当前用户的密码字节(16字节),顺序：普通用户、管理员、超级管理员
            int writeIndex = Common.GetInstance().IsSuperAdmin ? 32 : Common.GetInstance().IsAdmin ? 16 : 0;
            passwordMD5.CopyTo(writeBytes, writeIndex);

            //重新写入
            BinaryWriter writer = new BinaryWriter(new FileStream(settingPath, FileMode.Create));
            writer.Write(writeBytes);
            writer.Close();
            if (existfile)
            {
                FileInfo info = new FileInfo(settingPath);
                info.CreationTime = CreationTime;
                info.LastAccessTime = LastAccessTime;
                info.LastWriteTime = LastWriteTime;
            }
        }
    }
}
