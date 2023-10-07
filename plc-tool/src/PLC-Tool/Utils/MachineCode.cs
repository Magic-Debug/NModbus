using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrameworkCommon.Utils
{
    public class MachineCode
    {
        [DllImport("kernel32.dll")]
        private static extern int GetVolumeInformation(
                string lpRootPathName,
                string lpVolumeNameBuffer,
                int nVolumeNameSize,
                ref int lpVolumeSerialNumber,
                int lpMaximumComponentLength,
                int lpFileSystemFlags,
                string lpFileSystemNameBuffer,
                int nFileSystemNameSize
                );

        static MachineCode machineCode;

        public static string GetMachineCodeString()
        {
            string machineCodeString = string.Empty;
            if (machineCode == null)
            {
                machineCode = new MachineCode();
            }
            
            machineCodeString = "PC." + machineCode.GetCpuInfo() + "." +
                                machineCode.GetHDVal() + "." + "LthS";
                            //machineCode.GetHDid() + "." + "LthS";
                            //machineCode.GetMoAddress();
            return machineCodeString;
        }

        ///   <summary> 
        ///   获取cpu序列号     
        ///   </summary> 
        ///   <returns> string </returns> 
        public string GetCpuInfo()
        {
            string cpuInfo = "";
            try
            {
                using (ManagementClass cimobject = new ManagementClass("Win32_Processor"))
                {
                    ManagementObjectCollection moc = cimobject.GetInstances();

                    foreach (ManagementObject mo in moc)
                    {
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cpuInfo.ToString();
        }

        ///   <summary> 
        ///   获取硬盘ID     
        ///   </summary> 
        ///   <returns> string </returns> 
        public string GetHDid()
        {
            string HDid = "";
            try
            {
                using (ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive"))
                {
                    ManagementObjectCollection moc1 = cimobject1.GetInstances();
                    foreach (ManagementObject mo in moc1)
                    {
                        HDid = mo.Properties["Model"].Value.ToString();
                        mo.Dispose();
                        //break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return HDid.ToString();
        }

        ///   <summary> 
        ///   获取网卡硬件地址 
        ///   </summary> 
        ///   <returns> string </returns> 
        public string GetMoAddress()
        {
            string MoAddress = "";
            try
            {
                using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
                {
                    ManagementObjectCollection moc2 = mc.GetInstances();
                    foreach (ManagementObject mo in moc2)
                    {
                        if ((bool)mo["IPEnabled"] == true)
                            MoAddress = mo["MacAddress"].ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MoAddress.ToString();
        }
        
        ///   <summary> 
        ///   获取主板序列号
        ///   </summary> 
        ///   <returns> string </returns> 
        public string GetBaseBoard()
        {
            string BaseBoard = "";
            try
            {
                using (ManagementClass mc = new ManagementClass("Win32_BaseBoard"))
                {
                    ManagementObjectCollection moc2 = mc.GetInstances();
                    foreach (ManagementObject mo in moc2)
                    {
                        BaseBoard = mo.Properties["SerialNumber"].Value.ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BaseBoard;
        }

        ///   <summary> 
        ///   获取BIOS序列号
        ///   </summary> 
        ///   <returns> string </returns> 
        public string GetBios()
        {
            string Bios = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_BIOS");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    Bios = mo.Properties["SerialNumber"].Value.ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Bios;
        }

        /// <summary>
        /// 获取系统盘的硬盘序列号
        /// </summary>
        /// <returns></returns>
        public string GetHDVal()
        {
            ManagementClass cimObject = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc = cimObject.GetInstances();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (ManagementObject mo in moc)
            {
                string tag = mo.Properties["Tag"].Value.ToString().ToLower().Trim();
                string hdId = (string)mo.Properties["SerialNumber"].Value ?? string.Empty;
                hdId = hdId.Trim();
                dict.Add(tag, hdId);
            }
            cimObject = new ManagementClass("Win32_OperatingSystem");
            moc = cimObject.GetInstances();
            string currentSysRunDisk = string.Empty;
            foreach (ManagementObject mo in moc)
            {
                if (mo.Properties["Name"].Value.ToString().Contains("C:"))
                {
                    currentSysRunDisk = Regex.Match(mo.Properties["Name"].Value.ToString().ToLower(), @"harddisk\d+").Value;
                    break;
                }
            }
            var results = dict.Where(x => Regex.IsMatch(x.Key, @"physicaldrive" + Regex.Match(currentSysRunDisk, @"\d+$").Value));
            if (results.Any())
            {
                return results.ElementAt(0).Value;
            }
            return "";
        }
    }
}
