using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FrameworkCommon;

namespace PLCTool.Inspection
{
    public static class InspectionLog
    {
        //如有新的验布日志类型，在InspectionLogType添加枚举值，在构造方法添加正则表达式初始值。
        //注意新增验布日志关键字要与原有的不同，否则写在前面的类型优先匹配，不能得到预期。
        static InspectionLog()
        {
            MatchOptions = new MatchOption[]
            {
                new MatchOption (InspectionLogType.StartSoft,"启动程序"),
                new MatchOption (InspectionLogType.ExitSoft,"退出程序"),
                new MatchOption (InspectionLogType.StartInspection,"开始检验"),
                new MatchOption (InspectionLogType.StopInspection,"停止检验"),
                new MatchOption (InspectionLogType.SplitTimeUsageAndQueue,@"处理(耗时|时间)\d+,待处理队列长度\d+"),
                new MatchOption (InspectionLogType.SplitTimeUsage,"处理(耗时|时间)"),
                new MatchOption (InspectionLogType.SplitQueue,"待处理队列长度"),
                new MatchOption (InspectionLogType.SaveImageQueue,"待保存的图片队列长度"),
                new MatchOption (InspectionLogType.DetectTimeUsageAndQueue,@"诊断(耗时|时间)\d+,待诊断队列长度\d+"),
                new MatchOption (InspectionLogType.DetectTimeUsage,"诊断耗时"),
                new MatchOption (InspectionLogType.DetectQueue,"待诊断队列长度"),
                new MatchOption (InspectionLogType.NotDetect,"图片不检测"),
                new MatchOption (InspectionLogType.FilteDefect,"（筛选后）检测到缺陷"),
                new MatchOption (InspectionLogType.CommitDefect,"确认疵点"),
                new MatchOption (InspectionLogType.IgnoreDefect,"忽略疵点"),
                new MatchOption (InspectionLogType.CloseDefect,"疵点弹窗被手动关闭"),
                new MatchOption (InspectionLogType.MissDefect,"错过贴标"),
                new MatchOption (InspectionLogType.StartTicket,"开始贴标"),
                new MatchOption (InspectionLogType.FinishTicket,"贴标完成"),
                new MatchOption (InspectionLogType.FailedTicket,"贴标失败"),
                new MatchOption (InspectionLogType.SendTimeOver,"发送超时"),
                new MatchOption (InspectionLogType.ReceiveTimeOver,"接收超时"),
                new MatchOption (InspectionLogType.AnalysisTimeOver,"解析超时"),
                new MatchOption (InspectionLogType.SendCommand,@"发送命令，命令类型\d+")
            };
            LogLineFormat = new Regex(@"^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}(\.\d+)?\s(DEBUG|ERROR|INFO)\s.*");
            NumFormat = new Regex(@"-?\d+(\.\d+)?");
            TimeFormat = new Regex(@"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}(\.\d+)?");
            LogTypeFormat = new Regex(@"DEBUG|ERROR|INFO");
        }

        /// <summary>
        /// 日志文件编码方式，默认GB2312
        /// </summary>
        public static Encoding Encoding { get; set; } = Encoding.GetEncoding("GB2312");

        //一些正则表达式，用于解析日志，初始化时赋值
        private static Regex LogLineFormat;     //日志行
        private static Regex NumFormat;         //提取数字
        private static Regex TimeFormat;        //提取时间
        private static Regex LogTypeFormat;     //提取日志类型

        //日志行匹配选项列表，初始化时赋值
        private static MatchOption[] MatchOptions;

        /// <summary>
        /// 加载验布日志
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>验布软件事件</returns>
        public static List<InspectionAction> LoadInspectionActionLog(string filename)
        {
            try
            {
                return AnalysisAllInspectionLogLines(LoadLogLines(filename));
            }
            catch (Exception e)
            {
                LogHelper.Default.Error(e);
                throw new Exception("不是验布日志或者版本不兼容");
            }
        }
        /// <summary>
        /// 加载验布日志数据，用于自定义处理
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>日志数据</returns>
        public static InspectionLogLine[] LoadLogLines(string filename)
        {
            List<InspectionLogLine> list = new List<InspectionLogLine>();
            try
            {
                StreamReader reader = new StreamReader(filename, Encoding);
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    if (LogLineFormat.IsMatch(s))
                    {
                        try
                        {
                            InspectionLogLine line = AnalysisLine(s);
                            list.Add(line);
                        }
                        catch { }
                    }
                }
                reader.Close();
            }
            catch
            {
                LogHelper.Default.Info("加载日志错误");
            }
            return list.ToArray();
        }
        /// <summary>
        /// 按时间筛选日志内容，用于自定义处理
        /// </summary>
        /// <param name="loglines">日志数据</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public static InspectionLogLine[] SelectLogLines(InspectionLogLine[] loglines, DateTime starttime, DateTime endtime)
        {
            return loglines.Where(t => t.Time >= starttime && t.Time <= endtime).ToArray();
        }
        /// <summary>
        /// 按时间和关键字筛选日志内容，用于自定义处理
        /// </summary>
        /// <param name="loglines">日志数据</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public static InspectionLogLine[] SelectLogLines(InspectionLogLine[] loglines, DateTime starttime, DateTime endtime, string keyword)
        {
            return loglines.Where(t => t.Time >= starttime && t.Time <= endtime && t.LogMessage.Contains(keyword)).ToArray();
        }

        /*
         * 这些都是私有方法，用于解析验布事件，不需要深究
         */
        private static List<InspectionAction> AnalysisAllInspectionLogLines(InspectionLogLine[] loglines)
        {
            List<InspectionAction> inspections = new List<InspectionAction>();
            List<InspectionLogLine[]> InspectionActionLogLines = SplitLogLines(loglines, InspectionLogType.StartSoft);
            for (int i = 0; i < InspectionActionLogLines.Count; i++)
            {
                try
                {
                    InspectionAction inspection = AnalysisInspectionLogLines(InspectionActionLogLines[i]);
                    inspections.Add(inspection);
                }
                catch { }
            }
            return inspections;
        }
        private static InspectionAction AnalysisInspectionLogLines(InspectionLogLine[] loglines)
        {
            InspectionAction inspection = new InspectionAction();
            InspectionLogLine[][] ClassifyLogLines = ClassifyInspectionLogLines(loglines);
            inspection.StartSoftTime = ClassifyLogLines[0][0].Time;
            if (ClassifyLogLines[0].Length > 1)
            {
                inspection.ExitSoftTime = ClassifyLogLines[0][1].Time;
            }
            inspection.FabricActions = new List<FabricAction>();
            List<InspectionLogLine[]> FabricActionLogLines = SplitLogLines(ClassifyLogLines[1], InspectionLogType.StartInspection, InspectionLogType.StopInspection);
            for (int i = 0; i < FabricActionLogLines.Count; i++)
            {
                inspection.FabricActions.Add(AnalysisFabricActionLog(FabricActionLogLines[i]));
            }
            inspection.OverTimeActions = AnalysisOverTimeActionLogLines(ClassifyLogLines[2]);
            return inspection;
        }
        private static FabricAction AnalysisFabricActionLog(InspectionLogLine[] loglines)
        {
            FabricAction fabricaction = new FabricAction();
            InspectionLogLine[][] ClassifyLogLines = ClassifyFabricActionLogLine(loglines);
            fabricaction.StartTime = ClassifyLogLines[0][0].Time;
            fabricaction.ID = ClassifyLogLines[0][0].Values[0].ToString();
            fabricaction.ShareQuote = (int)ClassifyLogLines[0][0].Values[ClassifyLogLines[0][0].Values.Length - 2];
            fabricaction.Speed = ClassifyLogLines[0][0].Values[ClassifyLogLines[0][0].Values.Length - 1];
            if (ClassifyLogLines[0].Length > 1)
            {
                fabricaction.StopTime = ClassifyLogLines[0][1].Time;
            }
            List<SplitAction> SplitActions = AnalysisSplitActionLogLines(ClassifyLogLines[1]);
            List<SaveImageAction> SaveActions = AnalysisSaveActionLogLines(ClassifyLogLines[2]);
            List<DetectAction> DetectActions = AnalysisDetectActionLogLines(ClassifyLogLines[3]);
            List<DetectAction> TicketActions = AnalysisTicketActionLogLines(ClassifyLogLines[4]);
            fabricaction.SplitActions = SplitActions;
            fabricaction.SaveImageActions = SaveActions;
            fabricaction.DetectActions = MergeActions(DetectActions, TicketActions);
            return fabricaction;
        }
        private static List<SplitAction> AnalysisSplitActionLogLines(InspectionLogLine[] loglines)
        {
            List<SplitAction> list = new List<SplitAction>();
            InspectionLogType[] begintypes = new InspectionLogType[] { InspectionLogType.SplitTimeUsageAndQueue, InspectionLogType.SplitTimeUsage };
            List<InspectionLogLine[]> splitloglines = SplitLogLines(loglines, begintypes);
            foreach (InspectionLogLine[] lines in splitloglines)
            {
                SplitAction splitaction = new SplitAction();
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].InspectionLogType == InspectionLogType.SplitTimeUsageAndQueue)
                    {
                        splitaction.SplitTime = lines[i].Time;
                        splitaction.SplitTimeUsage = (int)lines[i].Values[0];
                        splitaction.WaitedSplitCount = (int)lines[i].Values[1];
                    }
                    else if (lines[i].InspectionLogType == InspectionLogType.SplitTimeUsage)
                    {
                        splitaction.SplitTime = lines[i].Time;
                        splitaction.SplitTimeUsage = (int)lines[i].Values[0];
                    }
                    else if (lines[i].InspectionLogType == InspectionLogType.SplitQueue)
                    {
                        splitaction.WaitedSplitCount = (int)lines[i].Values[0];
                    }
                }
                list.Add(splitaction);
            }
            return list;
        }
        private static List<SaveImageAction> AnalysisSaveActionLogLines(InspectionLogLine[] loglines)
        {
            List<SaveImageAction> list = new List<SaveImageAction>();
            for (int i = 0; i < loglines.Length; i++)
            {
                SaveImageAction saveaction = new SaveImageAction();
                saveaction.SaveImageTime = loglines[i].Time;
                saveaction.WaitedSaveImageCount = (int)loglines[i].Values[0];
                list.Add(saveaction);
            }
            return list;
        }
        private static List<DetectAction> AnalysisDetectActionLogLines(InspectionLogLine[] loglines)
        {
            List<DetectAction> list = new List<DetectAction>();
            InspectionLogType[] begintypes = new InspectionLogType[] { InspectionLogType.DetectTimeUsageAndQueue, InspectionLogType.DetectTimeUsage, InspectionLogType.NotDetect };
            List<InspectionLogLine[]> splitloglines = SplitLogLines(loglines, begintypes);
            foreach (InspectionLogLine[] lines in splitloglines)
            {
                DetectAction detection = new DetectAction();
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].InspectionLogType == InspectionLogType.DetectTimeUsageAndQueue)
                    {
                        detection.DetectTime = lines[i].Time;
                        detection.DetectTimeUsage = (int)lines[i].Values[0];
                        detection.WaitedDetectCount = (int)lines[i].Values[1];
                    }
                    else if (lines[i].InspectionLogType == InspectionLogType.DetectTimeUsage)
                    {
                        detection.DetectTime = lines[i].Time;
                        detection.DetectTimeUsage = (int)lines[i].Values[0];
                    }
                    else if (lines[i].InspectionLogType == InspectionLogType.DetectQueue)
                    {
                        detection.WaitedDetectCount = (int)lines[i].Values[0];
                    }
                    else if (lines[i].InspectionLogType == InspectionLogType.NotDetect)
                    {
                        detection.DetectTime = lines[i].Time;
                        detection.DetectTimeUsage = 0;
                        detection.ImagePosition = lines[i].Values[0];
                    }
                    else if (lines[i].InspectionLogType == InspectionLogType.FilteDefect)
                    {
                        detection.ImagePosition = lines[i].Values[0];
                    }
                }
                list.Add(detection);
            }
            return list;
        }
        private static List<DetectAction> AnalysisTicketActionLogLines(InspectionLogLine[] loglines)
        {
            List<DetectAction> list = new List<DetectAction>();
            DetectAction detectaction = new DetectAction();
            detectaction.TicketActions = new List<TicketAction>();
            TicketAction ticketaction = new TicketAction();
            bool flag = false;
            for (int i = 0; i < loglines.Length; i++)
            {
                InspectionLogType logtype = loglines[i].InspectionLogType;
                if (logtype == InspectionLogType.CommitDefect)
                {
                    if (flag)
                    {
                        list.Add(detectaction);
                    }
                    detectaction = new DetectAction();
                    detectaction.TicketActions = new List<TicketAction>();
                    detectaction.ImagePosition = loglines[i].Values[1];
                    detectaction.DefectCount = (int)loglines[i].Values[0];
                    flag = true;
                }
                else if (logtype == InspectionLogType.IgnoreDefect)
                {
                    if (flag)
                    {
                        list.Add(detectaction);
                    }
                    detectaction = new DetectAction();
                    detectaction.TicketActions = new List<TicketAction>();
                    detectaction.ImagePosition = loglines[i].Values[0];
                    detectaction.DefectCount = 0;
                    list.Add(detectaction);
                    flag = false;
                }
                else if (logtype == InspectionLogType.CloseDefect)
                {
                    if (flag)
                    {
                        list.Add(detectaction);
                    }
                    detectaction = new DetectAction();
                    detectaction.TicketActions = new List<TicketAction>();
                    detectaction.ImagePosition = loglines[i].Values[0];
                    detectaction.DefectCount = -1;
                    flag = true;
                }
                else if (logtype == InspectionLogType.StartTicket)
                {
                    ticketaction = new TicketAction();
                    ticketaction.StartTime = loglines[i].Time;
                    ticketaction.X = loglines[i].Values[0];
                    ticketaction.Y = loglines[i].Values[1];
                }
                else if (logtype == InspectionLogType.FinishTicket)
                {
                    ticketaction.FinishTime = loglines[i].Time;
                    detectaction.TicketActions.Add(ticketaction);
                }
                else if (logtype == InspectionLogType.FailedTicket)
                {
                    ticketaction.IsFailed = true;
                }
            }
            return list;
        }
        private static OverTimeAction AnalysisOverTimeActionLogLine(InspectionLogLine logline)
        {
            OverTimeAction action = new OverTimeAction();
            action.Time = logline.Time;
            switch (logline.InspectionLogType)
            {
                case InspectionLogType.SendTimeOver:
                    action.ErrorType = 1;
                    break;
                case InspectionLogType.ReceiveTimeOver:
                    action.ErrorType = 2;
                    break;
                case InspectionLogType.AnalysisTimeOver:
                    action.ErrorType = 3;
                    break;
            }
            action.TcpInterval = (int)logline.Values[0];
            action.TimeUsage = (float)logline.Values[1];
            return action;
        }
        private static List<OverTimeAction> AnalysisOverTimeActionLogLines(InspectionLogLine[] loglines)
        {
            List<OverTimeAction> list = new List<OverTimeAction>();
            for (int i = 0; i < loglines.Length; i++)
            {
                list.Add(AnalysisOverTimeActionLogLine(loglines[i]));
            }
            return list;
        }
        private static List<DetectAction> MergeActions(List<DetectAction> detectactions, List<DetectAction> ticketactions)
        {
            Dictionary<double, List<DetectAction>> imageDetectActions = new Dictionary<double, List<DetectAction>>();
            for (int i = 0; i < ticketactions.Count; i++)
            {
                if (imageDetectActions.ContainsKey(ticketactions[i].ImagePosition))
                {
                    imageDetectActions[ticketactions[i].ImagePosition].Add(ticketactions[i]);
                }
                else
                {
                    imageDetectActions.Add(ticketactions[i].ImagePosition, new List<DetectAction> { ticketactions[i] });
                }
            }
            for (int i = 0; i < detectactions.Count; i++)
            {
                DetectAction detectaction = detectactions[i];
                if (imageDetectActions.ContainsKey(detectaction.ImagePosition))
                {
                    DetectAction detectaction2 = imageDetectActions[detectaction.ImagePosition][0];
                    detectaction.TicketActions = detectaction2.TicketActions;
                    if (detectaction2.DefectCount == -1)
                    {
                        detectaction.DefectCount = detectaction2.TicketActions.Count;
                    }
                    else
                    {
                        detectaction.DefectCount = detectaction2.DefectCount;
                    }
                    imageDetectActions[detectaction.ImagePosition].RemoveAt(0);
                    detectactions[i] = detectaction;
                }
            }
            return detectactions;
        }        
        private static InspectionLogLine[][] ClassifyInspectionLogLines(InspectionLogLine[] loglines)
        {
            InspectionLogType[] SystemControlTypes = new InspectionLogType[]
            {
                InspectionLogType.StartSoft,
                InspectionLogType.ExitSoft
            };
            InspectionLogType[] FabricInspectionTypes = new InspectionLogType[]
            {
                InspectionLogType.StartInspection,
                InspectionLogType.StopInspection,
                InspectionLogType.SplitTimeUsageAndQueue,
                InspectionLogType.SplitTimeUsage,
                InspectionLogType.SplitQueue,
                InspectionLogType.NotSplit,
                InspectionLogType.SaveImageTimeUsageAndQueue,
                InspectionLogType.SaveImageTimeUsage,
                InspectionLogType.SaveImageQueue,
                InspectionLogType.DetectTimeUsageAndQueue,
                InspectionLogType.DetectTimeUsage,
                InspectionLogType.DetectQueue,
                InspectionLogType.NotDetect,
                InspectionLogType.FilteDefect,
                InspectionLogType.CommitDefect,
                InspectionLogType.IgnoreDefect,
                InspectionLogType.CloseDefect,
                InspectionLogType.MissDefect,
                InspectionLogType.StartTicket,
                InspectionLogType.FinishTicket,
                InspectionLogType.FailedTicket
            };
            InspectionLogType[] OverTimeTypes = new InspectionLogType[]
            {
                InspectionLogType.SendTimeOver,
                InspectionLogType.ReceiveTimeOver,
                InspectionLogType.AnalysisTimeOver
            };
            List<InspectionLogType[]> ClassifyTypes = new List<InspectionLogType[]>
            {
                SystemControlTypes,
                FabricInspectionTypes,
                OverTimeTypes
            };
            return ClassifyLogLines(loglines, ClassifyTypes).ToArray();
        }
        private static InspectionLogLine[][] ClassifyFabricActionLogLine(InspectionLogLine[] loglines)
        {
            InspectionLogType[] SystemControlType = new InspectionLogType[]
            {
                InspectionLogType.StartInspection,
                InspectionLogType.StopInspection
            };
            InspectionLogType[] SplitLogTypes = new InspectionLogType[]
            {
                InspectionLogType.SplitTimeUsageAndQueue,
                InspectionLogType.SplitTimeUsage,
                InspectionLogType.SplitQueue
            };
            InspectionLogType[] SaveTypes = new InspectionLogType[]
            {
                InspectionLogType.SaveImageTimeUsageAndQueue,
                InspectionLogType.SaveImageTimeUsage,
                InspectionLogType.SaveImageQueue
            };
            InspectionLogType[] DetectLogType = new InspectionLogType[]
            {
                InspectionLogType.DetectTimeUsageAndQueue,
                InspectionLogType.DetectTimeUsage,
                InspectionLogType.DetectQueue,
                InspectionLogType.NotDetect,
                InspectionLogType.FilteDefect
            };
            InspectionLogType[] TicketLogType = new InspectionLogType[]
            {
                 InspectionLogType.CommitDefect,
                 InspectionLogType.IgnoreDefect,
                 InspectionLogType.CloseDefect,
                 InspectionLogType.StartTicket,
                 InspectionLogType.FinishTicket,
                 InspectionLogType.FailedTicket
            };
            List<InspectionLogType[]> classifytypes = new List<InspectionLogType[]>
            {
                SystemControlType,
                SplitLogTypes,
                SaveTypes,
                DetectLogType,
                TicketLogType
            };
            return ClassifyLogLines(loglines, classifytypes).ToArray();
        }

        #region 基础解析方法
        // 解析一行日志的时间、类型、信息
        private static InspectionLogLine AnalysisLine(string line)
        {
            InspectionLogLine logline = new InspectionLogLine();
            string timestring = TimeFormat.Match(line).Value;
            line = line.Substring(timestring.Length + 1);
            logline.Time = DateTime.Parse(timestring);
            string logtypestring = LogTypeFormat.Match(line).Value;
            line = line.Substring(logtypestring.Length + 1);
            logline.LogType = logtypestring;
            logline.LogMessage = line;
            return logline;
        }
        // 拆分日志
        private static List<InspectionLogLine[]> SplitLogLines(InspectionLogLine[] loglines, InspectionLogType BeginType)
        {
            List<InspectionLogLine[]> list = new List<InspectionLogLine[]>();
            List<InspectionLogLine> list2 = new List<InspectionLogLine>();
            for (int i = 0; i < loglines.Length; i++)
            {
                if (loglines[i].InspectionLogType == BeginType)
                {
                    if (list2.Count > 0)
                    {
                        list.Add(list2.ToArray());
                        list2 = new List<InspectionLogLine>();
                    }
                }
                list2.Add(loglines[i]);
            }
            if (list2.Count > 0)
            {
                list.Add(list2.ToArray());
            }
            return list;
        }
        private static List<InspectionLogLine[]> SplitLogLines(InspectionLogLine[] loglines, InspectionLogType[] BeginTypes)
        {
            List<InspectionLogLine[]> list = new List<InspectionLogLine[]>();
            List<InspectionLogLine> list2 = new List<InspectionLogLine>();
            for (int i = 0; i < loglines.Length; i++)
            {
                if (BeginTypes.Contains(loglines[i].InspectionLogType))
                {
                    if (list2.Count > 0)
                    {
                        list.Add(list2.ToArray());
                    }
                    list2 = new List<InspectionLogLine>();
                }
                list2.Add(loglines[i]);
            }
            if (list2.Count > 0)
            {
                list.Add(list2.ToArray());
            }
            return list;
        }
        private static List<InspectionLogLine[]> SplitLogLines(InspectionLogLine[] loglines, InspectionLogType BeginType, InspectionLogType EndType)
        {
            List<InspectionLogLine[]> list = new List<InspectionLogLine[]>();
            List<InspectionLogLine> list2 = new List<InspectionLogLine>();
            bool isselect = false;
            for (int i = 0; i < loglines.Length; i++)
            {
                if (loglines[i].InspectionLogType == BeginType)
                {
                    if (isselect)
                    {
                        list.Add(list2.ToArray());
                    }
                    list2 = new List<InspectionLogLine>();
                    list2.Add(loglines[i]);
                    isselect = true;
                }
                else if (loglines[i].InspectionLogType == EndType)
                {
                    list2.Add(loglines[i]);
                    list.Add(list2.ToArray());
                    list2 = new List<InspectionLogLine>();
                    isselect = false;
                }
                else if (isselect)
                {
                    list2.Add(loglines[i]);
                }
            }
            if (isselect)
            {
                list.Add(list2.ToArray());
            }
            return list;
        }
        // 筛选日志内容
        private static InspectionLogLine[] SelectLogLines(InspectionLogLine[] loglines, InspectionLogType[] selecttypes)
        {
            List<InspectionLogLine> list = new List<InspectionLogLine>();
            for (int i = 0; i < loglines.Length; i++)
            {
                if (selecttypes.Contains(loglines[i].InspectionLogType))
                {
                    list.Add(loglines[i]);
                }
            }
            return list.ToArray();
        }
        // 日志类型分组
        private static List<InspectionLogLine[]> ClassifyLogLines(InspectionLogLine[] loglines, List<InspectionLogType[]> classifytypes)
        {
            List<InspectionLogLine[]> list = new List<InspectionLogLine[]>();
            for (int i = 0; i < classifytypes.Count; i++)
            {
                list.Add(SelectLogLines(loglines, classifytypes[i]));
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 验布日志行，每个实例代表一行日志记录
        /// </summary>
        public class InspectionLogLine
        {
            /// <summary>
            /// 日志时间
            /// </summary>
            public DateTime Time;
            /// <summary>
            /// 日志类型，分为 ERROR、INFO、DEBUG
            /// </summary>
            public string LogType;
            /// <summary>
            /// 日志信息
            /// </summary>
            public string LogMessage
            {
                get { return _logmessage; }
                set
                {
                    _logmessage = value;
                    InspectionLogType = MatchMessage(value);
                }
            }
            /// <summary>
            /// 日志数据，从日志中提取数字
            /// </summary>
            public double[] Values => SelectNumFromString(_logmessage);
            /// <summary>
            /// 验布信息类型
            /// </summary>
            internal InspectionLogType InspectionLogType { get; private set; }

            private string _logmessage;
            private static InspectionLogType MatchMessage(string msg)
            {
                for (int i = 0; i < MatchOptions.Length; i++)
                {
                    if (MatchOptions[i].Format.IsMatch(msg))
                    {
                        return MatchOptions[i].Type;
                    }
                }
                return InspectionLogType.Default;
            }
            private static double[] SelectNumFromString(string s)
            {
                MatchCollection matchs = NumFormat.Matches(s);
                List<double> list = new List<double>();
                foreach (Match k in matchs)
                {
                    list.Add(Convert.ToDouble(k.Value));
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// 日志行匹配选项，包含信息类型和格式
        /// </summary>
        private class MatchOption
        {
            /// <summary>
            /// 验布信息类型
            /// </summary>
            public InspectionLogType Type;
            /// <summary>
            /// 日志格式的正则表达式
            /// </summary>
            public Regex Format;
            public MatchOption(InspectionLogType type, string format)
            {
                Type = type;
                Format = new Regex(format);
            }
        }
        /// <summary>
        /// 验布信息类型
        /// </summary>
        internal enum InspectionLogType
        {
            Default,
            StartSoft,
            ExitSoft,
            StartInspection,
            StopInspection,
            SplitTimeUsageAndQueue,
            SplitTimeUsage,
            SplitQueue,
            NotSplit,
            SaveImageTimeUsageAndQueue,
            SaveImageTimeUsage,
            SaveImageQueue,
            DetectTimeUsageAndQueue,
            DetectTimeUsage,
            DetectQueue,
            NotDetect,
            FilteDefect,
            CommitDefect,
            IgnoreDefect,
            CloseDefect,
            MissDefect,
            StartTicket,
            FinishTicket,
            FailedTicket,
            SendTimeOver,
            ReceiveTimeOver,
            AnalysisTimeOver,
            SendCommand
        }
    }
}
