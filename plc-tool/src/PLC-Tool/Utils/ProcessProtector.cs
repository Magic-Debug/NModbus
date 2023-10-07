using System.Runtime.InteropServices;
using System.Threading;

namespace FrameworkCommon.Utils
{
    /// <summary>
    /// 进程保护者。保护进程不被关掉，一关掉电脑就会重启。
    /// 使用时需注意在Application.SessionEnding事件发生时取消保护，否则会蓝屏
    /// </summary>
    public static class ProcessProtector
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void RtlSetProcessIsCritical(uint v1, uint v2, uint v3);

        /// <summary>
        /// Flag for maintaining the state of protection.
        /// </summary>
        private static volatile bool isProtected = false;

        /// <summary>
        /// For synchronizing our current state.
        /// </summary>
        private static readonly ReaderWriterLockSlim isProtectedLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Gets whether or not the host process is currently protected.
        /// </summary>
        public static bool IsProtected
        {
            get
            {
                try
                {
                    isProtectedLock.EnterReadLock();
                    return isProtected;
                }
                finally
                {
                    isProtectedLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// If not alreay protected, will make the host process a system-critical process so it
        /// cannot be terminated without causing a shutdown of the entire system.
        /// </summary>
        public static void Protect()
        {
            try
            {
                isProtectedLock.EnterWriteLock();
                if (!isProtected)
                {
                    System.Diagnostics.Process.EnterDebugMode();
                    RtlSetProcessIsCritical(1, 0, 0);
                    isProtected = true;
                }
            }
            finally
            {
                isProtectedLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// If already protected, will remove protection from the host process, so that it will no
        /// longer be a system-critical process and thus will be able to shut down safely.
        /// </summary>
        public static void Unprotect()
        {
            try
            {
                isProtectedLock.EnterWriteLock();
                if (isProtected)
                {
                    RtlSetProcessIsCritical(0, 0, 0);
                    isProtected = false;
                }
            }
            finally
            {
                isProtectedLock.ExitWriteLock();
            }
        }
    }
}