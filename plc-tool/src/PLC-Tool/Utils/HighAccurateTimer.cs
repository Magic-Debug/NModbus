using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworkCommon.Utils
{
    /// <summary>
    /// 高精度计时器
    /// </summary>
    public class HighAccurateTimer : IDisposable
    {
        private Task timerThread;

        private long intervalTicks;
        private double intervalMilliseconds;             // interval in mimliseccond;
        private readonly long clockFrequency;            // result of QueryPerformanceFrequency()
        private volatile ManualResetEvent resetEvent = new ManualResetEvent(false);

        public HighAccurateTimer()
        {
            if (QueryPerformanceFrequency(out clockFrequency) == false)
            {
                throw new Win32Exception("QueryPerformanceFrequency() function is not supported");
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        public event EventHandler Tick;

        /// <summary>
        /// 触发周期
        /// </summary>
        public TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(intervalMilliseconds); }
            set
            {
                intervalMilliseconds = value.TotalMilliseconds;
                intervalTicks = (long)(intervalMilliseconds * clockFrequency / 1000);
            }
        }

        #region NativeMethods

        ///
        /// Pointer to a variable that receives the current performance-counter value, in counts.
        ///
        ///
        /// If the function succeeds, the return value is nonzero.
        ///
        [DllImport("Kernel32.dll", EntryPoint = "QueryPerformanceCounter")]
        private static extern bool GetCurrentTicks(out long lpPerformanceCount);

        ///
        /// Pointer to a variable that receives the current performance-counter frequency,
        /// in counts per second.
        /// If the installed hardware does not support a high-resolution performance counter,
        /// this parameter can be zero.
        ///
        ///
        /// If the installed hardware supports a high-resolution performance counter,
        /// the return value is nonzero.
        ///
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        #endregion

        /// <summary>
        /// 进程主程序
        /// </summary>
        private void ThreadProc()
        {
            long currentTicks;
            GetCurrentTicks(out currentTicks);
            var nextTriggerTimeTicks = currentTicks + intervalTicks;
            while (true)
            {
                resetEvent.WaitOne();

                while (currentTicks < nextTriggerTimeTicks)
                {
                    GetCurrentTicks(out currentTicks);
                }

                nextTriggerTimeTicks = currentTicks + intervalTicks;
                Tick?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Start()
        {
            if (timerThread == null)
            {
                timerThread = Task.Run(() => ThreadProc());
            }

            resetEvent.Set();
        }

        public void Stop()
        {
            resetEvent.Reset();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
