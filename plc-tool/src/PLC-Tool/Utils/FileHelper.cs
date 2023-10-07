using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCommon.Utils
{
    public class FileHelper
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        /// <summary>
        /// 检查文件是否被占用
        /// </summary>
        /// <param name="fileName"></param>
        public static bool IsUsed(string fileName)
        {
            IntPtr vHandle = _lopen(fileName, OF_READWRITE | OF_SHARE_DENY_NONE);
            if(vHandle == HFILE_ERROR)
            {
                return true;
            }

            CloseHandle(vHandle);
            return false;
        }
    }
}
