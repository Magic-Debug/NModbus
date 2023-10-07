using System;
using System.Linq;
using NModbus.Unme.Common;

namespace NModbus.Data
{
    /// <summary>
    /// A simple implementation of the point source. All registers are 
    /// </summary>
    /// <typeparam name="TPoint"></typeparam>
    internal class DefaultPointSource<TPoint> : IPointSource<TPoint>
    {
        //Only create this if referenced.
        public TPoint[] Points { get; }

        private readonly object _syncRoot = new object();

        public DefaultPointSource()
        {
            Points = new TPoint[120];
        }

        public void Init()
        {

        }

        public TPoint[] ReadPoints(ushort startAddress, ushort numberOfPoints)
        {
            lock (_syncRoot)
            {
                return Points
                    .Slice(startAddress, numberOfPoints)
                    .ToArray();
            }
        }

        public void WritePoints(ushort startAddress, TPoint[] points)
        {
            lock (_syncRoot)
            {
                for (ushort index = 0; index < points.Length; index++)
                {
                    Points[startAddress + index] = points[index];
                }
            }
        }
    }
}