using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTool.Inspection
{
    public static class InspectionStatistics
    {
        public static DataTable GetFabricSplitTable(FabricAction fabricaction)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("时间");
            dt.Columns.Add("处理图片耗时");
            dt.Columns.Add("待处理队列长度");
            for (int i = 0; i < fabricaction.SplitActions.Count; i++)
            {
                SplitAction splitaction = fabricaction.SplitActions[i];
                DataRow dr = dt.NewRow();
                dr["时间"] = splitaction.SplitTime.ToString("HH:mm:ss");
                dr["处理图片耗时"] = splitaction.SplitTimeUsage.ToString("f3");
                dr["待处理队列长度"] = splitaction.WaitedSplitCount.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable GetFabricSaveImageTable(FabricAction fabricaction)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("时间");
            dt.Columns.Add("待保存队列长度");
            for (int i = 0; i < fabricaction.SaveImageActions.Count; i++)
            {
                SaveImageAction saveimageaction = fabricaction.SaveImageActions[i];
                DataRow dr = dt.NewRow();
                dr["时间"] = saveimageaction.SaveImageTime.ToString("HH:mm:ss");
                dr["待保存队列长度"] = saveimageaction.WaitedSaveImageCount.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable GetFabricDetectTable(FabricAction fabricaction)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("时间");
            dt.Columns.Add("图片坐标");
            dt.Columns.Add("诊断耗时");
            dt.Columns.Add("待诊断队列长度");
            dt.Columns.Add("疵点数量");
            dt.Columns.Add("诊断速度");
            for (int i = 0; i < fabricaction.DetectActions.Count; i++)
            {
                DetectAction detectaction = fabricaction.DetectActions[i];
                DataRow dr = dt.NewRow();
                dr["时间"] = detectaction.DetectTime.ToString("HH:mm:ss");
                if (detectaction.ImagePosition > 0)
                {
                    dr["图片坐标"] = detectaction.ImagePosition.ToString("f3");
                }
                if (detectaction.DetectTimeUsage > 0)
                {
                    dr["诊断耗时"] = detectaction.DetectTimeUsage.ToString();
                    dr["诊断速度"] = detectaction.DetectSpeed;
                }
                dr["待诊断队列长度"] = detectaction.WaitedDetectCount.ToString();
                dr["疵点数量"] = detectaction.DefectCount.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable GetCustomTable(InspectionLog.InspectionLogLine[] loglines, DateTime time1, DateTime time2, string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("时间");
            dt.Columns.Add("数值");
            dt.Columns.Add("日志内容");
            InspectionLog.InspectionLogLine[] filterlines = InspectionLog.SelectLogLines(loglines, time1, time2, keyword);
            foreach(InspectionLog.InspectionLogLine line in filterlines)
            {
                DataRow dr = dt.NewRow();
                dr["时间"] = line.Time.ToString("HH:mm:ss");
                if(line.Values.Length>0)
                {
                    dr["数值"] = line.Values[0];
                }
                dr["日志内容"] = line.LogMessage;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static InspectionStatisticsOptions Statistics(FabricAction fabricaction)
        {
            InspectionStatisticsOptions result = new InspectionStatisticsOptions();
            result.SplitOption = StatisticsSplit(fabricaction.SplitActions);
            result.SaveImageOption = StatisticsSaveImage(fabricaction.SaveImageActions);
            result.DetectOption = StatisticsDetect(fabricaction.DetectActions);
            return result;
        }
        public static SplitStatisticsOptions StatisticsSplit(List<SplitAction> splitactions)
        {
            SplitStatisticsOptions result = new SplitStatisticsOptions();
            List<SplitAction> list = splitactions.Where(t => t.SplitTimeUsage > 0).ToList();
            if(list.Count>0)
            {
                result.ImageCount = list.Count;
                result.TotalTimeUsage = list.Sum(t => t.SplitTimeUsage);
                result.MaxTimeUsage = list.Max(t => t.SplitTimeUsage);
                result.MinTimeUsage = list.Min(t => t.SplitTimeUsage);
            }
            if(splitactions.Count>0)
            {
                result.MaxWaitedQueue = splitactions.Max(t => t.WaitedSplitCount);
            }
            return result;
        }
        public static SaveImageStatisticsOptions StatisticsSaveImage(List<SaveImageAction> saveimageactions)
        {
            SaveImageStatisticsOptions result = new SaveImageStatisticsOptions();
            if (saveimageactions.Count > 0)
            {
                result.ImageCount = saveimageactions.Count;
                result.TotalTimeUsage = saveimageactions.Sum(t => t.SaveImageTimeUsage);
                result.MaxTimeUsage = saveimageactions.Max(t => t.SaveImageTimeUsage);
                result.MinTimeUsage = saveimageactions.Min(t => t.SaveImageTimeUsage);
            }
            if(saveimageactions.Count>0)
            {
                result.MaxWaitedQueue = saveimageactions.Max(t => t.WaitedSaveImageCount);
            }
            return result;
        }
        public static DetectStatisticsOptions StatisticsDetect(List<DetectAction> detectactions)
        {
            DetectStatisticsOptions result = new DetectStatisticsOptions();
            List<DetectAction> list = detectactions.Where(t => t.DetectTimeUsage > 0).ToList();
            if (list.Count > 0)
            {
                result.ImageCount = list.Count;
                result.TotalTimeUsage = list.Sum(t => t.DetectTimeUsage);
                result.AvgDetectRate = Math.Round(list.Sum(t => t.DetectSpeed) / list.Count,4);
                result.MaxTimeUsage = list.Max(t => t.DetectTimeUsage);
                result.MinTimeUsage = list.Min(t => t.DetectTimeUsage);
                result.MaxWaitedtQueue = detectactions.Max(t => t.WaitedDetectCount);
            }
            list = detectactions.Where(t => t.DefectCount > 0).ToList();
            List<TicketAction> ticketactions = new List<TicketAction>();
            foreach(DetectAction detectaction in list)
            {
                ticketactions.AddRange(detectaction.TicketActions);
            }
            result.TicketOption = StatisticsTicket(ticketactions);
            return result;
        }
        public static TicketStatisticsOptions StatisticsTicket(List<TicketAction> ticketactions)
        {
            TicketStatisticsOptions result = new TicketStatisticsOptions();
            result.TicketTimes = ticketactions.Count;
            result.TicketFailedTimes = ticketactions.Count(t => t.IsFailed);
            return result;
        }
        public static CustomStatisticsOptions StatisticsCustom(InspectionLog.InspectionLogLine[] loglines)
        {
            CustomStatisticsOptions result = new CustomStatisticsOptions();
            List<InspectionLog.InspectionLogLine> list = loglines.Where(t => t.Values.Length > 0).ToList();
            result.AllCount = loglines.Length;
            if (list.Count > 0)
            {
                result.StatisticsCount = list.Count;
                result.Sum = list.Sum(t => t.Values[0]);
                result.Max = list.Max(t => t.Values[0]);
                result.Min = list.Min(t => t.Values[0]);
            }
            return result;
        }
        public static CustomStatisticsOptions StatisticsCustom(InspectionLog.InspectionLogLine[] loglines, DateTime time1, DateTime time2, string keyword)
        {
            InspectionLog.InspectionLogLine[] filterlines = InspectionLog.SelectLogLines(loglines, time1, time2, keyword);
            return StatisticsCustom(filterlines);
        }
    }

    public struct InspectionStatisticsOptions
    {
        public double MaxSplitTimeUsage
        {
            get { return SplitOption.MaxTimeUsage; }
            set { SplitOption.MaxTimeUsage = value; }
        }
        public double MinSplitTimeUsage
        {
            get { return SplitOption.MinTimeUsage; }
            set { SplitOption.MinTimeUsage = value; }
        }
        public double AvgSplitTimeUsage => SplitOption.AvgTimeUsage;
        public int MaxSplitQueue
        {
            get { return SplitOption.MaxWaitedQueue; }
            set { SplitOption.MaxWaitedQueue = value; }
        }
        public double MaxSaveImageTimeUsage
        {
            get { return SaveImageOption.MaxTimeUsage; }
            set { SaveImageOption.MaxTimeUsage = value; }
        }
        public double MinSaveImageTimeUsage
        {
            get { return SaveImageOption.MinTimeUsage; }
            set { SaveImageOption.MinTimeUsage = value; }
        }
        public double AvgSaveImageTimeUsage=> SaveImageOption.AvgTimeUsage;
        public int MaxSaveImageQueue
        {
            get { return SaveImageOption.MaxWaitedQueue; }
            set { SaveImageOption.MaxWaitedQueue = value; }
        }
        public int DetectImageCount
        {
            get { return DetectOption.ImageCount; }
            set { DetectOption.ImageCount = value; }
        }
        public double MaxDetectTimeUsage
        {
            get { return DetectOption.MaxTimeUsage; }
            set { DetectOption.MaxTimeUsage = value; }
        }
        public double MinDetectTimeUsage
        {
            get { return DetectOption.MinTimeUsage; }
            set { DetectOption.MinTimeUsage = value; }
        }
        public double AvgDetectTimeUsage => DetectOption.AvgTimeUsage;
        public double AvgDetectRate => DetectOption.AvgDetectRate;
        public int MaxDetectQueue
        {
            get { return DetectOption.MaxWaitedtQueue; }
            set { DetectOption.MaxWaitedtQueue = value; }
        }
        public int TicketTimes
        {
            get { return DetectOption.TicketTimes; }
            set { DetectOption.TicketTimes = value; }
        }
        public int TicketFailedTimes
        {
            get { return DetectOption.TicketFailedTimes; }
            set { DetectOption.TicketFailedTimes = value; }
        }
        public double TicketFailedRate => DetectOption.TicketFailedRate;

        public SplitStatisticsOptions SplitOption;
        public SaveImageStatisticsOptions SaveImageOption;
        public DetectStatisticsOptions DetectOption;
    }
    public struct SplitStatisticsOptions
    {
        public int ImageCount { get; set; }
        public double TotalTimeUsage { get; set; }
        public double MaxTimeUsage { get; set; }
        public double MinTimeUsage { get; set; }
        public double AvgTimeUsage => ImageCount == 0 ? 0 : TotalTimeUsage / ImageCount;
        public int MaxWaitedQueue { get; set; }
    }
    public struct SaveImageStatisticsOptions
    {
        public int ImageCount { get; set; }
        public double TotalTimeUsage { get; set; }
        public double MaxTimeUsage { get; set; }
        public double MinTimeUsage { get; set; }
        public double AvgTimeUsage => ImageCount == 0 ? 0 : TotalTimeUsage / ImageCount;
        public int MaxWaitedQueue { get; set; }
    }
    public struct DetectStatisticsOptions
    {
        public int ImageCount { get; set; }
        public double TotalTimeUsage { get; set; }
        public double MaxTimeUsage { get; set; }
        public double MinTimeUsage { get; set; }
        public double AvgTimeUsage => ImageCount == 0 ? 0 : TotalTimeUsage / ImageCount;
        public double AvgDetectRate { get; set; }
        public int MaxWaitedtQueue { get; set; }
        public int TicketTimes
        {
            get { return TicketOption.TicketTimes; }
            set { TicketOption.TicketFailedTimes = value; }
        }
        public int TicketFailedTimes
        {
            get { return TicketOption.TicketFailedTimes; }
            set { TicketOption.TicketFailedTimes = value; }
        }
        public double TicketFailedRate => TicketOption.TicketFailedRate;

        public TicketStatisticsOptions TicketOption;
    }
    public struct TicketStatisticsOptions
    {
        public int TicketTimes { get; set; }
        public int TicketFailedTimes { get; set; }
        public double TicketFailedRate => TicketFailedTimes == 0 ? 0 : (double)TicketFailedTimes / TicketTimes;
    }
    public struct CustomStatisticsOptions
    {
        public int AllCount { get; set; }
        public int StatisticsCount { get; set; }
        public double Sum { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public double Avg => StatisticsCount == 0 ? 0 : Sum / StatisticsCount;
    }
}
