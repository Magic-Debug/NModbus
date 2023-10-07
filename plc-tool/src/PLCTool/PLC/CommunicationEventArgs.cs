namespace PLCTool.PLC
{
    #region 事件定义
    public delegate void CommunicationEventHandler(CommunicationEventArgs e);

    public class CommunicationEventArgs : EventArgs
    {
        public CommunicationEventArgs(DateTime time, byte[] data)
        {
            Time = time;
            Data = data;
        }

        public DateTime Time { get; }
        public byte[] Data { get; }

        public override string ToString()
        {
            string text = Time.ToString();
            foreach (byte t in Data)
            {
                text += t.ToString();
            }
            return text;
        }
    }
    #endregion
}
