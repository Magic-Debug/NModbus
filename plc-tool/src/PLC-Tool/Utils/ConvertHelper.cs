using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCommon.Utils
{
    public static class ConvertHelper
    {
        public static int ToUInt32(this ushort[] value, int startIndex = 0)
        {
            if (value == null || value.Length < 2 || startIndex < 0 || (value.Length - 1 < startIndex + 1))
            {
                throw new ArgumentOutOfRangeException("参数错误！");
            }

            //var val = (uint)(value[startIndex] << 16) + value[startIndex + 1];
            var val = (value[startIndex] << 16) | value[startIndex + 1];
            return val;
        }

        public static float ToFloat(this ushort[] value, int startIndex = 0)
        {
            if (value == null || value.Length < 2 || startIndex < 0 || (value.Length - 1 < startIndex + 1))
            {
                throw new ArgumentOutOfRangeException("参数错误！");
            }

            var bytes = new byte[]
            {
                (byte) ((value[startIndex] >> 8) & 0xff),
                (byte) ((value[startIndex] >> 0) & 0xff),
                (byte) ((value[startIndex + 1] >> 8) & 0xff),
                (byte) ((value[startIndex + 1] >> 0) & 0xff),
            };

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        public static ushort[] ToByteArray(this float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return new[]
            {
                (ushort) ((bytes[0] << 8 | bytes[1]) & 0xFFFF),
                (ushort) ((bytes[2] << 8 | bytes[3]) & 0xFFFF),
            };
        }

        public static ushort[] ToUShorts(this int value)
        {
            var bytes = BitConverter.GetBytes(value);
            var ushorts = new ushort[bytes.Length / 2];
            Buffer.BlockCopy(bytes, 0, ushorts, 0, bytes.Length);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(ushorts);
            }

            return ushorts;
        }

        /// <summary>
        /// 将浮点数转换成short数组
        /// </summary>
        /// <param name="input">要转换的浮点数</param>
        /// <param name="count">数组长度（默认为2）</param>
        /// <param name="needSwapHighLow">是否需要交换高地位</param>
        /// <returns>short数组</returns>
        public static ushort[] FloatToUshorts(float input, ushort count = 2, bool needSwapHighLow = true)
        {
            var bytes = BitConverter.GetBytes(input);
            if (needSwapHighLow)
            {
                bytes = bytes.Reverse().ToArray();
            }
            var result = ConvertHelper.DecodeWords(bytes, 0, count);
            return result;
        }

        /// <summary>
        /// 将short数组转换成浮点数
        /// </summary>
        /// <param name="high">高字</param>
        /// <param name="low">低字</param>
        /// <param name="needSwapHighLow">是否需要交换高地位</param>
        /// <returns>short数组</returns>
        public static float ToFloat(ushort high, ushort low, bool needSwapHighLow = true)
        {
            var bytes = ConvertHelper.EncodeWords(new[] { high, low });
            if (needSwapHighLow)
            {
                bytes = bytes.Reverse().ToArray();
            }
            var result = BitConverter.ToSingle(bytes, 0);
            return result;
        }

        public static ushort CRC16(byte[] bytes, int offset, int count)
        {
            ushort crc = 0xFFFF;
            for (int pos = 0; pos < count; pos++)
            {
                crc ^= (ushort)bytes[pos + offset];
                for (int i = 8; i > 0; i--)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                        crc >>= 1;
                }
            }
            return crc;
        }

        public static byte EncodeBool(bool value)
        {
            return (byte)(value ? 0xFF : 0x00);
        }

        public static bool DecodeBool(byte value)
        {
            return (value != 0x00);
        }

        public static byte[] EncodeBools(bool[] bools)
        {
            var count = BytesForBools(bools.Length);
            var bytes = new byte[count];
            //for (var i = 0; i < count; i++) {
            //	bytes[i] = 0;
            //}
            for (var i = 0; i < bools.Length; i++)
            {
                var v = bools[i];
                if (v)
                {
                    var bi = i / 8;
                    bytes[bi] |= (byte)(1 << (i % 8));
                }
            }
            return bytes;
        }

        public static byte[] EncodeWords(ushort[] words)
        {
            var count = 2 * words.Length;
            var bytes = new byte[count];
            //for (var i = 0; i < count; i++) {
            //	bytes[i] = 0;
            //}
            for (var i = 0; i < words.Length; i++)
            {
                bytes[2 * i + 0] = (byte)((words[i] >> 8) & 0xff);
                bytes[2 * i + 1] = (byte)((words[i] >> 0) & 0xff);
            }
            return bytes;
        }

        public static bool[] DecodeBools(byte[] packet, int offset, ushort count)
        {
            var bools = new bool[count];
            var bytes = BytesForBools(count);
            for (var i = 0; i < bytes; i++)
            {
                var bits = count >= 8 ? 8 : count % 8;
                var b = packet[offset + i];
                ByteToBools(b, bools, bools.Length - count, bits);
                count -= (ushort)bits;
            }
            return bools;
        }

        public static ushort[] DecodeWords(byte[] packet, int offset, ushort count)
        {
            var results = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                results[i] = (ushort)(packet[offset + 2 * i] << 8 | packet[offset + 2 * i + 1]);
            }
            return results;
        }

        private static void ByteToBools(byte b, bool[] bools, int offset, int count)
        {
            for (int i = 0; i < count; i++)
                bools[offset + i] = ((b >> i) & 0x01) == 1;
        }

        public static byte BytesForWords(int count)
        {
            return (byte)(2 * count);
        }

        public static byte BytesForBools(int count)
        {
            return (byte)(count == 0 ? 0 : (count - 1) / 8 + 1);
        }

        public static byte High(int value)
        {
            return (byte)((value >> 8) & 0xff);
        }

        public static byte Low(int value)
        {
            return (byte)((value >> 0) & 0xff);
        }

        public static ushort GetUShort(byte bh, byte bl)
        {
            return (ushort)(
                ((bh << 8) & 0xFF00)
                | (bl & 0xff)
            );
        }

        public static ushort GetUShort(byte[] bytes, int offset)
        {
            return (ushort)(
                ((bytes[offset + 0] << 8) & 0xFF00)
                | (bytes[offset + 1] & 0xff)
            );
        }

        public static ushort GetUShortLittleEndian(byte[] bytes, int offset)
        {
            return (ushort)(
                ((bytes[offset + 1] << 8) & 0xFF00)
                | (bytes[offset + 0] & 0xff)
            );
        }

        public static void Copy(byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
        {
            for (var i = 0; i < count; i++)
                dst[dstOffset + i] = src[srcOffset + i];
        }

        public static bool[] Clone(bool[] values)
        {
            var clone = new bool[values.Length];
            for (var i = 0; i < values.Length; i++)
                clone[i] = values[i];
            return clone;
        }

        public static ushort[] Clone(ushort[] values)
        {
            var clone = new ushort[values.Length];
            for (var i = 0; i < values.Length; i++)
                clone[i] = values[i];
            return clone;
        }
    }
}
