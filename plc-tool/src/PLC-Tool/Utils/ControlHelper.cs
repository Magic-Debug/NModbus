using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FrameworkCommon.Utils
{
    public static class ControlHelper
    {
        //public static T FindVisualChild<T>(this DependencyObject obj) where T : DependencyObject
        //{
        //    if (obj != null)
        //    {
        //        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        //        {
        //            DependencyObject child = VisualTreeHelper.GetChild(obj, i);
        //            if (child != null && child is T)
        //            {
        //                return (T)child;
        //            }
        //            T childItem = FindVisualChild<T>(child);
        //            if (childItem != null)
        //            {
        //                return childItem;
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public static Task<bool> TouchHold(this FrameworkElement element, TimeSpan duration)
        //{
        //    DispatcherTimer timer = new DispatcherTimer();
        //    TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
        //    timer.Interval = duration;

        //    MouseButtonEventHandler touchUpHandler = delegate
        //    {
        //        timer.Stop();
        //        if (task.Task.Status == TaskStatus.Running)
        //        {
        //            task.SetResult(false);
        //        }
        //    };

        //    element.PreviewMouseUp += touchUpHandler;

        //    timer.Tick += delegate
        //    {
        //        element.PreviewMouseUp -= touchUpHandler;
        //        timer.Stop();
        //        task.SetResult(true);
        //    };

        //    timer.Start();
        //    return task.Task;
        //}
    }
}
