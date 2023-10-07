using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Inspection
{
    /// <summary>
    /// 验布软件事件，每一次事件启动一次软件
    /// 包含多次验布事件和通讯异常记录
    /// </summary>
    public struct InspectionAction
    {
        /// <summary>
        /// 启动软件时间
        /// </summary>
        public DateTime? StartSoftTime;
        /// <summary>
        /// 退出软件时间
        /// </summary>
        public DateTime? ExitSoftTime;
        /// <summary>
        /// 验布记录
        /// </summary>
        public List<FabricAction> FabricActions;
        /// <summary>
        /// 通讯错误记录
        /// </summary>
        public List<OverTimeAction> OverTimeActions;
    }

    /// <summary>
    /// 验布事件，每一次事件检验一卷布
    /// 包含检验单信息和多次拆图事件、保存图片事件、疵点诊断事件、贴标事件
    /// </summary>
    public struct FabricAction
    {
        /// <summary>
        /// 检验单ID
        /// </summary>
        public string ID;
        /// <summary>
        /// 单号
        /// </summary>
        public string CheckID;
        /// <summary>
        /// 拆图份数
        /// </summary>
        public int ShareQuote;
        /// <summary>
        /// 速度
        /// </summary>
        public double Speed;
        /// <summary>
        /// 开始检验时间
        /// </summary>
        public DateTime? StartTime;
        /// <summary>
        /// 结束检验时间
        /// </summary>
        public DateTime? StopTime;
        /// <summary>
        /// 拆图记录
        /// </summary>
        public List<SplitAction> SplitActions;
        /// <summary>
        /// 保存图片记录
        /// </summary>
        public List<SaveImageAction> SaveImageActions;
        /// <summary>
        /// 诊断和贴标记录
        /// </summary>
        public List<DetectAction> DetectActions;
    }

    /// <summary>
    /// 拆图事件
    /// </summary>
    public struct SplitAction
    {
        /// <summary>
        /// 图片坐标
        /// </summary>
        public double ImagePosition;
        /// <summary>
        /// 开始拆图时间
        /// </summary>
        public DateTime SplitTime;
        /// <summary>
        /// 拆图耗时
        /// </summary>
        public int SplitTimeUsage;
        /// <summary>
        /// 待拆图队列长度
        /// </summary>
        public int WaitedSplitCount;
    }

    /// <summary>
    /// 保存图片事件
    /// </summary>
    public struct SaveImageAction
    {
        /// <summary>
        /// 图片坐标
        /// </summary>
        public double ImagePosition;
        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveImageTime;
        /// <summary>
        /// 保存图片耗时
        /// </summary>
        public int SaveImageTimeUsage;
        /// <summary>
        /// 待保存队列长度
        /// </summary>
        public int WaitedSaveImageCount;
    }

    /// <summary>
    /// 疵点诊断事件
    /// </summary>
    public struct DetectAction
    {
        /// <summary>
        /// 图片坐标
        /// </summary>
        public double ImagePosition;
        /// <summary>
        /// 开始诊断时间
        /// </summary>
        public DateTime DetectTime;
        /// <summary>
        /// 诊断耗时(ms)
        /// </summary>
        public int DetectTimeUsage;
        /// <summary>
        /// 诊断速度(米/s)
        /// </summary>
        public double DetectSpeed => DetectTimeUsage == 0 ? 0 : Math.Round(125f / DetectTimeUsage, 4);
        /// <summary>
        /// 待诊断队列长度
        /// </summary>
        public int WaitedDetectCount;
        /// <summary>
        /// 停机坐标
        /// </summary>
        public double StopPosition;
        /// <summary>
        /// 停机时间
        /// </summary>
        public DateTime StopTime;
        /// <summary>
        /// 疵点个数
        /// </summary>
        public int DefectCount;
        /// <summary>
        /// 贴标事件
        /// </summary>
        public List<TicketAction> TicketActions;
    }

    /// <summary>
    /// 贴标事件
    /// </summary>
    public struct TicketAction
    {
        /// <summary>
        /// 开始贴标时间
        /// </summary>
        public DateTime StartTime;
        /// <summary>
        /// 完成贴标时间
        /// </summary>
        public DateTime FinishTime;
        /// <summary>
        /// X坐标
        /// </summary>
        public double X;
        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y;
        /// <summary>
        /// 是否失败
        /// </summary>
        public bool IsFailed;
    }
    
    /// <summary>
    /// 通讯超时事件
    /// </summary>
    public class OverTimeAction
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time;
        /// <summary>
        /// 超时类型
        /// </summary>
        public int ErrorType;
        /// <summary>
        /// 通讯周期
        /// </summary>
        public int TcpInterval;
        /// <summary>
        /// 实际耗时
        /// </summary>
        public float TimeUsage;
    }
}
