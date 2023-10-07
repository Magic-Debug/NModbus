namespace NModbus.Extensions.Functions
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    ///   This class provides some functions that can be used to read/write values of a set word size.
    /// </summary>
    public class RegisterFunctions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slaveAddress"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <param name="master"></param>
        /// <param name="wordSize"></param>
        /// <param name="endianConverter"></param>
        /// <param name="wordSwap"></param>
        /// <returns></returns>
        public static byte[][] ReadRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints, IModbusMaster master, uint wordSize, Func<byte[], byte[]> endianConverter, bool wordSwap = false)
        {
            int registerMultiplier = RegisterFunctions.GetRegisterMultiplier(wordSize);
            ushort registersToRead = (ushort)(numberOfPoints * registerMultiplier);
            ushort[] values = master.ReadHoldingRegisters(slaveAddress, startAddress, registersToRead);
            if (wordSwap) Array.Reverse(values);
            return ConvertRegistersToValues(values, registerMultiplier).Select(endianConverter).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slaveAddress"></param>
        /// <param name="startAddress"></param>
        /// <param name="data"></param>
        /// <param name="master"></param>
        /// <param name="wordSize"></param>
        /// <param name="endianConverter"></param>
        /// <param name="wordSwap"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void WriteRegistersFunc(byte slaveAddress, ushort startAddress, byte[][] data, IModbusMaster master, uint wordSize, Func<byte[], byte[]> endianConverter, bool wordSwap = false)
        {
            int wordByteArraySize = RegisterFunctions.GetRegisterMultiplier(wordSize) * 2;
            if (data.Any(e => e.Length != wordByteArraySize))
            {
                throw new ArgumentException("All data values must be of the correct word length.");
            }
            byte[][] dataCorrectEndian = data.Select(endianConverter).ToArray();
            ushort[] registerValues = RegisterFunctions.ConvertValuesToRegisters(dataCorrectEndian);
            if (wordSwap) Array.Reverse(registerValues);
            master.WriteMultipleRegisters(slaveAddress, startAddress, registerValues);
        }



        public static char[] ByteValueArraysToChars(byte[][] data, bool frontPadding = true, bool singleCharPerRegister = true)
        {
            if (singleCharPerRegister)
            {
                return frontPadding
                  ? data.Select(e => BitConverter.ToChar(e, e.Length - 2)).ToArray()
                  : data.Select(e => BitConverter.ToChar(e, 0)).ToArray();
            }
            byte[] flatData = data.SelectMany(e => e).ToArray();
            int count = flatData.Length / 2;
            char[] chars = new char[count];
            for (var index = 0; index < count; index++)
            {
                chars[index] = BitConverter.ToChar(flatData, index);
            }
            return chars;
        }

        public static short[] ByteValueArraysToShorts(byte[][] data, bool frontPadding = true)
        {
            return frontPadding
              ? data.Select(e => BitConverter.ToInt16(e, e.Length - 2)).ToArray()
              : data.Select(e => BitConverter.ToInt16(e, 0)).ToArray();
        }

        public static ushort[] ByteValueArraysToUShorts(byte[][] data, bool frontPadding = true)
        {
            return frontPadding
              ? data.Select(e => BitConverter.ToUInt16(e, e.Length - 2)).ToArray()
              : data.Select(e => BitConverter.ToUInt16(e, 0)).ToArray();
        }

        public static int[] ByteValueArraysToInts(byte[][] data, bool frontPadding = true)
        {
            return frontPadding
              ? data.Select(e => BitConverter.ToInt32(e, e.Length - 4)).ToArray()
              : data.Select(e => BitConverter.ToInt32(e, 0)).ToArray();
        }

        public static uint[] ByteValueArraysToUInts(byte[][] data, bool frontPadding = true)
        {
            return frontPadding
              ? data.Select(e => BitConverter.ToUInt32(e, e.Length - 4)).ToArray()
              : data.Select(e => BitConverter.ToUInt32(e, 0)).ToArray();
        }

        public static float[] ByteValueArraysToFloats(byte[][] data, bool frontPadding = true)
        {
            return frontPadding
              ? data.Select(e => BitConverter.ToSingle(e, e.Length - 4)).ToArray()
              : data.Select(e => BitConverter.ToSingle(e, 0)).ToArray();
        }


        public static byte[][] CharsToByteValueArrays(char[] data, uint wordSize, bool frontPadding = true, bool singleCharPerRegister = true)
        {
            var bytesPerWord = RegisterFunctions.GetRegisterMultiplier(wordSize) * 2;
            if (!singleCharPerRegister)
            {
                var remainder = data.Length % bytesPerWord;
                var registerBytes = remainder > 0
                  ? data.Length + (bytesPerWord - remainder)
                  : data.Length;
                var byteArray = new byte[registerBytes];
                for (var index = 0; index < byteArray.Length; index++)
                {
                    byteArray[index] = index < data.Length
                      ? Convert.ToByte(data[index])
                      : Convert.ToByte('\0'); //Unicode Null Charector
                }
                var byteValueArrays = new byte[byteArray.Length / bytesPerWord][];
                for (var index = 0; index < byteValueArrays.Length; index++)
                {
                    var offset = index * bytesPerWord;
                    byteValueArrays[index] = new ArraySegment<byte>(byteArray, offset, bytesPerWord).ToArray();
                }
                return byteValueArrays;
            }
            return (frontPadding)
              ? data.Select(e =>
              {
                  var bytes = new byte[bytesPerWord];
                  bytes[bytes.Length - 1] = Convert.ToByte(e);
                  return bytes;
              }).ToArray()
              : data.Select(e =>
              {
                  var bytes = new byte[bytesPerWord];
                  bytes[0] = Convert.ToByte(e);
                  return bytes;
              }).ToArray();
        }

        public static byte[][] ShortsToByteValueArrays(short[] data, uint wordSize, bool frontPadding = true)
          => data.Select(e => PadBytesToWordSize(wordSize, BitConverter.GetBytes(e), frontPadding)).ToArray();

        public static byte[][] UShortsToByteValueArrays(ushort[] data, uint wordSize, bool frontPadding = true)
          => data.Select(e => PadBytesToWordSize(wordSize, BitConverter.GetBytes(e), frontPadding)).ToArray();

        public static byte[][] IntToByteValueArrays(int[] data, uint wordSize, bool frontPadding = true)
          => data.Select(e => PadBytesToWordSize(wordSize, BitConverter.GetBytes(e), frontPadding)).ToArray();

        public static byte[][] UIntToByteValueArrays(uint[] data, uint wordSize, bool frontPadding = true)
        {
            return data.Select(e => PadBytesToWordSize(wordSize, BitConverter.GetBytes(e), frontPadding)).ToArray();
        }

        /// <summary>
        /// 将float数组转换成byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="wordSize"></param>
        /// <param name="frontPadding"></param>
        /// <returns></returns>
        public static byte[][] FloatToByteValueArrays(float[] data, uint wordSize, bool frontPadding = true)
        {
            byte[][] bytes = data.Select(e => PadBytesToWordSize(wordSize, BitConverter.GetBytes(e), frontPadding)).ToArray();
            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordSize"></param>
        /// <param name="source"></param>
        /// <param name="frontPadding"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static byte[] PadBytesToWordSize(uint wordSize, byte[] source, bool frontPadding)
        {
            int targetLength = GetRegisterMultiplier(wordSize) * 2;
            byte[] target = new byte[targetLength];
            if (source.Length > target.Length)
            {
                throw new ArgumentException("Source bytes can not greater than target");
            }
            int offset = frontPadding
              ? target.Length - source.Length
              : 0;
            Array.Copy(source, 0, target, offset, source.Length);
            return target;
        }

        private static ushort[] ConvertValuesToRegisters(byte[][] data)
        {
            byte[] flatData = data.SelectMany(e => e).ToArray();
            int count = flatData.Count() / 2;
            ushort[] registers = new ushort[count];
            for (int index = 0; index < count; index++)
            {
                registers[index] = BitConverter.ToUInt16(flatData, (index * 2));
            }
            return registers;
        }

        private static byte[][] ConvertRegistersToValues(ushort[] registers, int registerMultiplier) //TODO::Convert to function pass in everything it needs
        {
            if ((registers.Length % registerMultiplier) != 0)
            {
                throw new InvalidDataException("registers.Length is not a multiple of RegisterMultiplier");
            }
            int count = registers.Length / registerMultiplier;
            byte[][] values = new byte[count][];
            for (int index = 0; index < count; index++)
            {
                int offset = index * registerMultiplier;
                ArraySegment<ushort> segment = new ArraySegment<ushort>(registers, offset, registerMultiplier);
                byte[] bytes = segment.SelectMany(BitConverter.GetBytes).ToArray();
                values[index] = bytes;
            }
            return values;
        }

        private static int GetRegisterMultiplier(uint wordSize)
        {
            switch (wordSize)
            {
                case (16):
                    return 1;
                case (32):
                    return 2;
                case (64):
                    return 4;
                default: throw new ArgumentException("Word size mus be 16/32/64");
            }
        }
    }
}
