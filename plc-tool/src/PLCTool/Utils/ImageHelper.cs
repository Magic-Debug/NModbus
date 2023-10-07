using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace FrameworkCommon.Utils
{
    public class D2<T>
    {
        private readonly T[] input;
        private readonly int rows;

        public D2(T[] input, int rows)
        {
            this.input = input;
            this.rows = rows;
        }

        public T this[int row, int col]
        {
            get { return input[col * this.rows + row]; }
            set { input[col * this.rows + row] = value; }
        }
    }



    public static class ImageHelper
    {
        private static readonly PixelFormat[] indexedPixelFormats =
        {
            PixelFormat.Undefined, PixelFormat.DontCare,
            System.Drawing.Imaging.PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed,
            PixelFormat.Format8bppIndexed
        };

        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            return indexedPixelFormats.Contains(imgPixelFormat);
        }

        // 获取像素值
        public static double[,] GetImagePixel(Bitmap img)
        {
            var huiduzhi = new double[img.Width, img.Height];

            var w = img.Width;
            var h = img.Height;

            for (var i = 0; i < w; i++)
            {
                for (var j = 0; j < h; j++)
                {
                    var color = img.GetPixel(i, j);
                    huiduzhi[i, j] = color.R * 0.299 + color.G * 0.587 + color.B * 0.114;

                    //Gray = (R * 38 + G * 75 + B * 15) >> 7
                    //huiduzhi[i, j] = (color.R * 38 + color.G * 75 + color.B * 15) >> 7;
                }
            }

            return huiduzhi;
        }

        public static byte[,] To2DBytes(this Bitmap bitmap)
        {
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            var length = bmpData.Stride * bmpData.Height;
            var bytes = new byte[length];
            Marshal.Copy(bmpData.Scan0, bytes, 0, length);
            bitmap.UnlockBits(bmpData);

            var bs = bytes.To2DMatrix(bitmap.Width, false);
            return bs;
        }

        public static WriteableBitmap ConvertFormat(this WriteableBitmap bitmap, System.Windows.Media.PixelFormat format)
        {
            if (bitmap.Format == format)
            {
                return bitmap;
            }

            var source = new FormatConvertedBitmap(bitmap, format, null, 0);
            return new WriteableBitmap(source);
        }

        public static BitmapImage ToBitmapImage(this byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;
            var image = new BitmapImage();
            try
            {
                var mem = new MemoryStream(byteArray);
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
                image.Freeze();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return image;
        }

        /// <summary>
        /// 将一个字节数组转换为8bit灰度位图
        /// </summary>
        /// <param name="rawValues">显示字节数组</param>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <returns>位图</returns>
        public static Bitmap ToGrayBitmap(this byte[] rawValues, int width, int height)
        {
            // 申请目标位图的变量，并将其内存区域锁定
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // 获取图像参数
            int stride = bmpData.Stride;  // 扫描线的宽度
            int offset = stride - width;  // 显示宽度与扫描线宽度的间隙
            IntPtr iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置
            int scanBytes = stride * height;   // 用stride宽度，表示这是内存区域的大小

            // 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组
            int posScan = 0, posReal = 0;   // 分别设置两个位置指针，指向源数组和目标数组
            byte[] pixelValues = new byte[scanBytes];  //为目标数组分配内存
            for (int x = 0; x < height; x++)
            {
                // 下面的循环节是模拟行扫描
                for (int y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = rawValues[posReal++];
                }
                posScan += offset;  //行扫描结束，要将目标位置指针移过那段“间隙”
            }

            // 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中
            Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);  // 解锁内存区域

            // 下面的代码是为了修改生成位图的索引表，从伪彩修改为灰度
            ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }

            bmp.Palette = tempPalette;

            return bmp;
        }

        public static int[] PointsToArray(this Point[] points)
        {
            int len = points.Length * 2 + 2;
            int[] result = new int[len];
            for (var i = 0; i < points.Length; i++)
            {
                result[i * 2] = points[i].X;
                result[i * 2 + 1] = points[i].Y;
            }

            result[len - 2] = points[0].X;
            result[len - 1] = points[0].Y;

            return result;
        }

        public static Bitmap CutImage(this Bitmap bitmap, Rectangle rectangle)
        {
            var cutImage = bitmap.Clone(rectangle, bitmap.PixelFormat);
            return cutImage;
        }

        //public static byte[] Cut(this Bitmap bitmap, Rectangle rectangle)
        //{
        //    var cutImage = bitmap.CutImage(rectangle);
        //    var bytes = cutImage.ToMemoryStreamBytes();
        //    return bytes;
        //}

        public static byte[] Cut(this Bitmap bitmap, Rectangle rectangle)
        {
            //以大小为剪切大小，像素格式为32位RGB创建一个位图对像
            Bitmap cutImage = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppRgb);
            //定义一个区域
            Rectangle rect = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            //要绘制到的位图
            var g = Graphics.FromImage(cutImage);
            //将bm内rg所指定的区域绘制到bm1
            g.DrawImage(bitmap, rect);

            var bytes = cutImage.ToMemoryStreamBytes();
            return bytes;
        }

        public static Bitmap KiCut(this Bitmap b, Rectangle rectangle)
        {
            return KiCut(b, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static Bitmap KiCut(this Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error("截图异常！", ex);
                return null;
            }
        }

        /// <summary>
        /// Convert a bitmap to a byte array
        /// </summary>
        /// <param name="bitmap">image to convert</param>
        /// <returns>image as bytes</returns>
        public static byte[] ToRawBytes(this Bitmap bitmap)
        {
            BitmapData bitmapData = null;

            try
            {
                var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int length = bitmapData.Stride * bitmapData.Height;
                var rawImage = new byte[length];
                Marshal.Copy(bitmapData.Scan0, rawImage, 0, length);
                return rawImage;
            }
            finally
            {
                if (bitmapData != null)
                {
                    bitmap.UnlockBits(bitmapData);
                }
            }
        }

        public static byte[] ToMemoryStreamBytes(this Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                System.Threading.Thread.Sleep(500);
                bitmap.Save(ms, ImageFormat.Bmp);
                var byteImage = ms.ToArray();
                return byteImage;
            }
        }

        public static void Draw(this WriteableBitmap bitmap, byte[] bytes)
        {
            var w = bitmap.PixelWidth;
            var h = bitmap.PixelHeight;
            var s = bitmap.BackBufferStride;
            bitmap.WritePixels(new Int32Rect(0, 0, w, h), bytes, s, 0);
        }

        public static void Draw(this WriteableBitmap bitmap, Bitmap image)
        {
            var w = Math.Min(bitmap.PixelWidth, image.Width);
            var h = Math.Min(bitmap.PixelHeight, image.Height);
            var rect = new Rectangle(0, 0, w, h);
            var bmpData = image.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);
            var stride = bitmap.BackBufferStride;
            var length = w * h;
            bitmap.WritePixels(new Int32Rect(0, 0, w, h), bmpData.Scan0, length, stride);
            image.UnlockBits(bmpData);
        }

        public static Image DrawPoints(this Image img, Point[] points, bool isCross = true, int penSize = 2, int interval = 10)
        {
            Graphics g;
            Image bmp = null;

            if (IsPixelFormatIndexed(img.PixelFormat))
            {
                bmp = new Bitmap(img);
                g = Graphics.FromImage(bmp);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(bmp, 0, 0);
                //g.DrawImage(img, 0, 0, img.Width, img.Height);
            }
            else
            {
                g = Graphics.FromImage(img);
            }

            if (isCross)
            {
                foreach (var p in points)
                {
                    g.DrawLine(new Pen(Color.Red, penSize), new Point(p.X, p.Y - interval), new Point(p.X, p.Y + interval));
                    g.DrawLine(new Pen(Color.Red, penSize), new Point(p.X - interval, p.Y), new Point(p.X + interval, p.Y));
                }
            }
            else
            {
                foreach (var p in points)
                {
                    g.DrawRectangle(new Pen(Color.Red, penSize), new Rectangle(p.X - interval, p.Y - interval, 2 * interval, 2 * interval));
                }
            }

            g.Dispose();

            return bmp ?? img;
        }

        //public static void DrawPoints(this WriteableBitmap img, Point[] points, bool isCross = true, int interval = 10)
        //{
        //    using (img.GetBitmapContext())
        //    {
        //        if (isCross)
        //        {
        //            foreach (var p in points)
        //            {
        //                img.DrawLine(p.X, p.Y - interval, p.X, p.Y + interval, Colors.Red);
        //                img.DrawLine(p.X - interval, p.Y, p.X + interval, p.Y, Colors.Red);
        //            }
        //        }
        //        else
        //        {
        //            foreach (var p in points)
        //            {
        //                //img.DrawRectangle(p.X - interval - 1, p.Y - interval - 1, p.X + interval + 1, p.Y + interval + 1, Colors.Red);
        //                img.DrawRectangle(p.X - interval, p.Y - interval, p.X + interval, p.Y + interval, Colors.Red);
        //                //img.DrawRectangle(p.X - interval + 1, p.Y - interval + 1, p.X + interval - 1, p.Y + interval - 1, Colors.Red);
        //            }
        //        }
        //    }
        //}

        public static void Mark(this byte[] bytes, Int32Rect rect, int width, int height, int stride, int ratio = 1, byte color = 0, bool isCross = true)
        {
            int x = rect.X;
            int y = rect.Y;
            int w = rect.Width;
            int h = rect.Height;

            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= width) x = width - 1;
            if (y >= height) y = height - 1;

            int xMax = Math.Min(width - 1, x + w);
            int yMax = Math.Min(height - 1, y + h);

            if (isCross)
            {
                var yc = Math.Min(y + h / 2, yMax);
                for (int i = x * ratio; i < xMax * ratio; i++)
                {
                    //横线
                    bytes[yc * stride + i] = color;
                }

                var xc = Math.Min(x + w / 2, xMax);
                for (int i = y; i < yMax; i++)
                {
                    //竖线
                    bytes[i * stride + xc * ratio] = color;
                    if (ratio > 1)
                    {
                        bytes[i * stride + xc * ratio + 1] = color;
                        bytes[i * stride + xc * ratio - 1] = color;
                    }
                }
            }
            else
            {
                for (int i = x * ratio; i < xMax * ratio; i++)
                {
                    //上
                    bytes[y * stride + i] = color;
                    //下
                    bytes[yMax * stride + i] = color;
                }

                for (int i = y; i < yMax; i++)
                {
                    //左
                    bytes[i * stride + x * ratio] = color;
                    //右
                    bytes[i * stride + xMax * ratio] = color;

                    if (ratio > 1)
                    {
                        bytes[i * stride + x * ratio + 1] = color;
                        bytes[i * stride + x * ratio - 1] = color;

                        bytes[i * stride + xMax * ratio + 1] = color;
                        bytes[i * stride + xMax * ratio - 1] = color;
                    }
                }
            }
        }

        //public static WriteableBitmap ToBitmap(this byte[] bytes, int width, int height)
        //{
        //    try
        //    {
        //        var bitmap = BitmapFactory.New(width, height);
        //        using (bitmap.GetBitmapContext())
        //        {
        //            bitmap.FromByteArray(bytes);
        //            return bitmap;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Global.Logger.Error(e);
        //        return null;
        //    }
        //}

        public static Bitmap BytesToBitmap(this byte[] bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                return new Bitmap(stream);
            }
            catch (ArgumentNullException ex)
            {
                WindowsSystemInfo wsi = WindowsSystemInfo.GetInstance();
                LogHelper.Default.Error($"内存状态 ：{wsi.SystemMemoryUsedDouble}/{wsi.PhysicalMemoryDouble} GB  ({wsi.MemoryLoad}%)");
                throw ex;
            }
            catch (ArgumentException ex)
            {
                WindowsSystemInfo wsi = WindowsSystemInfo.GetInstance();
                LogHelper.Default.Error($"内存状态 ：{wsi.SystemMemoryUsedDouble}/{wsi.PhysicalMemoryDouble} GB  ({wsi.MemoryLoad}%)");
                throw ex;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
        }


        /// <summary>
        /// Bitmap to BitmapSource
        /// </summary>
        /// <param name="bytes">Bitmap Bytes</param>
        /// <returns>BitmapSource</returns>
        public static WriteableBitmap CreateBitmapSource(this byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            using (var memoryStream = new MemoryStream(bytes))
            {
                // Make sure to create the bitmap in the UI thread
                if (InvokeRequired)
                {
                    return (WriteableBitmap)System.Windows.Application.Current.Dispatcher.Invoke(
                        new Func<Stream, WriteableBitmap>(CreateBitmapSourceFromBitmap),
                        memoryStream);
                }

                return CreateBitmapSourceFromBitmap(memoryStream);
            }
        }

        /// <summary>
        /// Bitmap to BitmapSource
        /// </summary>
        /// <param name="bitmap">Bitmap</param>
        /// <returns>BitmapSource</returns>
        public static WriteableBitmap CreateBitmapSource(this Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            using (var memoryStream = new MemoryStream())
            {
                // You need to specify the image format to fill the stream. 
                // I'm assuming it is PNG
                bitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Make sure to create the bitmap in the UI thread
                if (InvokeRequired)
                {
                    return (WriteableBitmap)System.Windows.Application.Current.Dispatcher.Invoke(
                        new Func<Stream, WriteableBitmap>(CreateBitmapSourceFromBitmap),
                        memoryStream);
                }

                return CreateBitmapSourceFromBitmap(memoryStream);
            }
        }

        private static bool InvokeRequired
        {
            get { return Dispatcher.CurrentDispatcher != System.Windows.Application.Current.Dispatcher; }
        }

        private static WriteableBitmap CreateBitmapSourceFromBitmap(Stream stream)
        {
            var bitmapDecoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            // This will disconnect the stream from the image completely...
            var writable = new WriteableBitmap(bitmapDecoder.Frames.Single());
            writable.Freeze();

            return writable;
        }


        public static Geometry ToGeometry(this IEnumerable<System.Windows.Point> points)
        {
            var pthFigure = new PathFigure();
            pthFigure.IsClosed = true;   //是否封闭
            pthFigure.IsFilled = false;   //是否填充
            pthFigure.StartPoint = points.First();
            var plineSeg = new PolyLineSegment(points, true);
            var myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(plineSeg);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);
            PathGeometry pthGeometry = new PathGeometry();
            pthGeometry.Figures = pthFigureCollection;
            //pthGeometry.Transform = new ScaleTransform(0.098, 0.159);
            return pthGeometry;
        }

        public static Bitmap ResizeImage(this Image imgToResize, int width, int height)
        {
            return new Bitmap(imgToResize, new Size(width, height));
        }

        public static Bitmap ResizeImage2(this Bitmap bmp, int width, int height)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
            Bitmap newBitmap = new Bitmap(width, height, bmpData.Stride, bmp.PixelFormat, bmpData.Scan0);
            bmp.UnlockBits(bmpData);
            return newBitmap;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <param name="format">The format of resized image</param>
        /// <returns>The resized image.</returns>
        public static Bitmap Resize(this Image image, int width, int height, PixelFormat format = PixelFormat.Format24bppRgb)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height, format);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        #region 绘制线条

        /// <summary>
        /// Bitfields used to partition the space into 9 regions
        /// </summary>
        private const byte INSIDE = 0; // 0000
        private const byte LEFT = 1;   // 0001
        private const byte RIGHT = 2;  // 0010
        private const byte BOTTOM = 4; // 0100
        private const byte TOP = 8;    // 1000

        /// <summary>
        /// Compute the bit code for a point (x, y) using the clip rectangle
        /// bounded diagonally by (xmin, ymin), and (xmax, ymax)
        /// ASSUME THAT xmax , xmin , ymax and ymin are global constants.
        /// </summary>
        /// <param name="extents">The extents.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        private static byte ComputeOutCode(Rect extents, double x, double y)
        {
            // initialized as being inside of clip window
            byte code = INSIDE;

            if (x < extents.Left)           // to the left of clip window
                code |= LEFT;
            else if (x > extents.Right)     // to the right of clip window
                code |= RIGHT;
            if (y > extents.Bottom)         // below the clip window
                code |= BOTTOM;
            else if (y < extents.Top)       // above the clip window
                code |= TOP;

            return code;
        }

        /// <summary>
        /// Cohen–Sutherland clipping algorithm clips a line from
        /// P0 = (x0, y0) to P1 = (x1, y1) against a rectangle with 
        /// diagonal from (xmin, ymin) to (xmax, ymax).
        /// </summary>
        /// <remarks>See http://en.wikipedia.org/wiki/Cohen%E2%80%93Sutherland_algorithm for details</remarks>
        /// <returns>a list of two points in the resulting clipped line, or zero</returns>
        internal static bool CohenSutherlandLineClip(Rect extents, ref double x0, ref double y0, ref double x1, ref double y1)
        {
            // compute outcodes for P0, P1, and whatever point lies outside the clip rectangle
            byte outcode0 = ComputeOutCode(extents, x0, y0);
            byte outcode1 = ComputeOutCode(extents, x1, y1);

            // No clipping if both points lie inside viewport
            if (outcode0 == INSIDE && outcode1 == INSIDE)
                return true;

            bool isValid = false;

            while (true)
            {
                // Bitwise OR is 0. Trivially accept and get out of loop
                if ((outcode0 | outcode1) == 0)
                {
                    isValid = true;
                    break;
                }
                // Bitwise AND is not 0. Trivially reject and get out of loop
                else if ((outcode0 & outcode1) != 0)
                {
                    break;
                }
                else
                {
                    // failed both tests, so calculate the line segment to clip
                    // from an outside point to an intersection with clip edge
                    double x, y;

                    // At least one endpoint is outside the clip rectangle; pick it.
                    byte outcodeOut = (outcode0 != 0) ? outcode0 : outcode1;

                    // Now find the intersection point;
                    // use formulas y = y0 + slope * (x - x0), x = x0 + (1 / slope) * (y - y0)
                    if ((outcodeOut & TOP) != 0)
                    {   // point is above the clip rectangle
                        x = x0 + (x1 - x0) * (extents.Top - y0) / (y1 - y0);
                        y = extents.Top;
                    }
                    else if ((outcodeOut & BOTTOM) != 0)
                    { // point is below the clip rectangle
                        x = x0 + (x1 - x0) * (extents.Bottom - y0) / (y1 - y0);
                        y = extents.Bottom;
                    }
                    else if ((outcodeOut & RIGHT) != 0)
                    {  // point is to the right of clip rectangle
                        y = y0 + (y1 - y0) * (extents.Right - x0) / (x1 - x0);
                        x = extents.Right;
                    }
                    else if ((outcodeOut & LEFT) != 0)
                    {   // point is to the left of clip rectangle
                        y = y0 + (y1 - y0) * (extents.Left - x0) / (x1 - x0);
                        x = extents.Left;
                    }
                    else
                    {
                        x = double.NaN;
                        y = double.NaN;
                    }

                    // Now we move outside point to intersection point to clip
                    // and get ready for next pass.
                    if (outcodeOut == outcode0)
                    {
                        x0 = x;
                        y0 = y;
                        outcode0 = ComputeOutCode(extents, x0, y0);
                    }
                    else
                    {
                        x1 = x;
                        y1 = y;
                        outcode1 = ComputeOutCode(extents, x1, y1);
                    }
                }
            }

            return isValid;
        }

        internal static bool CohenSutherlandLineClip(Rect extents, ref int xi0, ref int yi0, ref int xi1, ref int yi1)
        {
            double x0 = xi0;
            double y0 = yi0;
            double x1 = xi1;
            double y1 = yi1;

            var isValid = CohenSutherlandLineClip(extents, ref x0, ref y0, ref x1, ref y1);

            // Update the clipped line
            xi0 = (int)x0;
            yi0 = (int)y0;
            xi1 = (int)x1;
            yi1 = (int)y1;

            return isValid;
        }

        /// <summary>
        /// Draws a colored line by connecting two points using an optimized DDA. 
        /// Uses the pixels array and the width directly for best performance.
        /// </summary>
        /// <param name="bytes">The context containing the pixels as int RGBA value.</param>
        /// <param name="width">The width of one scanline in the pixels array.</param>
        /// <param name="height">The height of the bitmap.</param>
        /// <param name="x1">The x-coordinate of the start point.</param>
        /// <param name="y1">The y-coordinate of the start point.</param>
        /// <param name="x2">The x-coordinate of the end point.</param>
        /// <param name="y2">The y-coordinate of the end point.</param>
        /// <param name="color">The color for the line.</param>
        public static void DrawLine(this byte[] bytes, int width, int height, int x1, int y1, int x2, int y2, byte color)
        {
            //var pData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
            //var pixels = (int*)pData;
            var pixels = bytes;

            // Get clip coordinates
            int clipX1 = 0;
            int clipX2 = width;
            int clipY1 = 0;
            int clipY2 = height;

            // Perform cohen-sutherland clipping if either point is out of the viewport
            if (!CohenSutherlandLineClip(new Rect(0, 0, width, height), ref x1, ref y1, ref x2, ref y2)) return;

            //var pixels = context.Pixels;

            // Distance start and end point
            int dx = x2 - x1;
            int dy = y2 - y1;

            const int PRECISION_SHIFT = 8;

            // Determine slope (absolute value)
            int lenX, lenY;
            if (dy >= 0)
            {
                lenY = dy;
            }
            else
            {
                lenY = -dy;
            }

            if (dx >= 0)
            {
                lenX = dx;
            }
            else
            {
                lenX = -dx;
            }

            if (lenX > lenY)
            {   // x increases by +/- 1
                if (dx < 0)
                {
                    int t = x1;
                    x1 = x2;
                    x2 = t;
                    t = y1;
                    y1 = y2;
                    y2 = t;
                }

                // Init steps and start
                int incy = (dy << PRECISION_SHIFT) / dx;

                int y1s = y1 << PRECISION_SHIFT;
                int y2s = y2 << PRECISION_SHIFT;
                int hs = height << PRECISION_SHIFT;

                if (y1 < y2)
                {
                    if (y1 >= clipY2 || y2 < clipY1)
                    {
                        return;
                    }
                    if (y1s < 0)
                    {
                        if (incy == 0)
                        {
                            return;
                        }
                        int oldy1s = y1s;
                        // Find lowest y1s that is greater or equal than 0.
                        y1s = incy - 1 + ((y1s + 1) % incy);
                        x1 += (y1s - oldy1s) / incy;
                    }
                    if (y2s >= hs)
                    {
                        if (incy != 0)
                        {
                            // Find highest y2s that is less or equal than ws - 1.
                            // y2s = y1s + n * incy. Find n.
                            y2s = hs - 1 - (hs - 1 - y1s) % incy;
                            x2 = x1 + (y2s - y1s) / incy;
                        }
                    }
                }
                else
                {
                    if (y2 >= clipY2 || y1 < clipY1)
                    {
                        return;
                    }
                    if (y1s >= hs)
                    {
                        if (incy == 0)
                        {
                            return;
                        }
                        int oldy1s = y1s;
                        // Find highest y1s that is less or equal than ws - 1.
                        // y1s = oldy1s + n * incy. Find n.
                        y1s = hs - 1 + (incy - (hs - 1 - oldy1s) % incy);
                        x1 += (y1s - oldy1s) / incy;
                    }
                    if (y2s < 0)
                    {
                        if (incy != 0)
                        {
                            // Find lowest y2s that is greater or equal than 0.
                            // y2s = y1s + n * incy. Find n.
                            y2s = y1s % incy;
                            x2 = x1 + (y2s - y1s) / incy;
                        }
                    }
                }

                if (x1 < 0)
                {
                    y1s -= incy * x1;
                    x1 = 0;
                }
                if (x2 >= width)
                {
                    x2 = width - 1;
                }

                int ys = y1s;

                // Walk the line!
                int y = ys >> PRECISION_SHIFT;
                int previousY = y;
                int index = x1 + y * width;
                int k = incy < 0 ? 1 - width : 1 + width;
                for (int x = x1; x <= x2; ++x)
                {
                    pixels[index] = color;
                    ys += incy;
                    y = ys >> PRECISION_SHIFT;
                    if (y != previousY)
                    {
                        previousY = y;
                        index += k;
                    }
                    else
                    {
                        ++index;
                    }
                }
            }
            else
            {
                // Prevent division by zero
                if (lenY == 0)
                {
                    return;
                }
                if (dy < 0)
                {
                    int t = x1;
                    x1 = x2;
                    x2 = t;
                    t = y1;
                    y1 = y2;
                    y2 = t;
                }

                // Init steps and start
                int x1s = x1 << PRECISION_SHIFT;
                int x2s = x2 << PRECISION_SHIFT;
                int ws = width << PRECISION_SHIFT;

                int incx = (dx << PRECISION_SHIFT) / dy;

                if (x1 < x2)
                {
                    if (x1 >= clipX2 || x2 < clipX1)
                    {
                        return;
                    }
                    if (x1s < 0)
                    {
                        if (incx == 0)
                        {
                            return;
                        }
                        int oldx1s = x1s;
                        // Find lowest x1s that is greater or equal than 0.
                        x1s = incx - 1 + ((x1s + 1) % incx);
                        y1 += (x1s - oldx1s) / incx;
                    }
                    if (x2s >= ws)
                    {
                        if (incx != 0)
                        {
                            // Find highest x2s that is less or equal than ws - 1.
                            // x2s = x1s + n * incx. Find n.
                            x2s = ws - 1 - (ws - 1 - x1s) % incx;
                            y2 = y1 + (x2s - x1s) / incx;
                        }
                    }
                }
                else
                {
                    if (x2 >= clipX2 || x1 < clipX1)
                    {
                        return;
                    }
                    if (x1s >= ws)
                    {
                        if (incx == 0)
                        {
                            return;
                        }
                        int oldx1s = x1s;
                        // Find highest x1s that is less or equal than ws - 1.
                        // x1s = oldx1s + n * incx. Find n.
                        x1s = ws - 1 + (incx - (ws - 1 - oldx1s) % incx);
                        y1 += (x1s - oldx1s) / incx;
                    }
                    if (x2s < 0)
                    {
                        if (incx != 0)
                        {
                            // Find lowest x2s that is greater or equal than 0.
                            // x2s = x1s + n * incx. Find n.
                            x2s = x1s % incx;
                            y2 = y1 + (x2s - x1s) / incx;
                        }
                    }
                }

                if (y1 < 0)
                {
                    x1s -= incx * y1;
                    y1 = 0;
                }
                if (y2 >= height)
                {
                    y2 = height - 1;
                }

                int index = x1s;
                int indexBaseValue = y1 * width;

                // Walk the line!
                var inc = (width << PRECISION_SHIFT) + incx;
                for (int y = y1; y <= y2; ++y)
                {
                    pixels[indexBaseValue + (index >> PRECISION_SHIFT)] = color;
                    index += inc;
                }
            }
        }

        /// <summary>
        /// Draws a polyline anti-aliased. Add the first point also at the end of the array if the line should be closed.
        /// </summary>
        /// <param name="bytes">The WriteableBitmap.</param>
        /// <param name="height">image height</param>
        /// <param name="points">The points of the polyline in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, ..., xn, yn).</param>
        /// <param name="color">The color for the line.</param>
        /// <param name="width">image width</param>
        public static void DrawPolyline(this byte[] bytes, int width, int height, int[] points, byte color)
        {
            var w = width;
            var h = height;
            var x1 = points[0];
            var y1 = points[1];

            for (var i = 2; i < points.Length; i += 2)
            {
                var x2 = points[i];
                var y2 = points[i + 1];

                DrawLine(bytes, w, h, x1, y1, x2, y2, color);
                x1 = x2;
                y1 = y2;
            }
        }

        /// <summary>
        /// Draws a polyline anti-aliased. Add the first point also at the end of the array if the line should be closed.
        /// </summary>
        /// <param name="bytes">The WriteableBitmap.</param>
        /// <param name="height">image height</param>
        /// <param name="points">The points.</param>
        /// <param name="color">The color for the line.</param>
        /// <param name="width">image width</param>
        public static void DrawPolyline(this byte[] bytes, int width, int height, Point[] points, byte color)
        {
            var w = width;
            var h = height;
            var x1 = points[0].X;
            var y1 = points[0].Y;

            for (var i = 1; i < points.Length; i++)
            {
                var x2 = points[i].X;
                var y2 = points[i].Y;

                //DrawLine(bytes, w, h, x1, y1, x2, y2, color);

                DrawLine(bytes, w, h, x1 - 1, y1 - 1, x2 + 1, y2 + 1, color);
                DrawLine(bytes, w, h, x1 - 1, y1 - 1, x2 - 1, y2 - 1, color);
                DrawLine(bytes, w, h, x1, y1, x2, y2, color);
                DrawLine(bytes, w, h, x1 + 1, y1 + 1, x2 - 1, y2 - 1, color);
                DrawLine(bytes, w, h, x1 + 1, y1 + 1, x2 + 1, y2 + 1, color);


                x1 = x2;
                y1 = y2;
            }

            //最后首尾相连
            //DrawLine(bytes, w, h, points[0].X, points[0].Y, x1, y1, color);

            DrawLine(bytes, w, h, points[0].X - 1, points[0].Y - 1, x1 + 1, y1 + 1, color);
            DrawLine(bytes, w, h, points[0].X - 1, points[0].Y - 1, x1 - 1, y1 - 1, color);
            DrawLine(bytes, w, h, points[0].X, points[0].Y, x1, y1, color);
            DrawLine(bytes, w, h, points[0].X + 1, points[0].Y + 1, x1 - 1, y1 - 1, color);
            DrawLine(bytes, w, h, points[0].X + 1, points[0].Y + 1, x1 + 1, y1 + 1, color);
        }

        #endregion

        private static string Key = "LintSenseImageEncryption";

        public static bool SaveEncryptBitmap(Bitmap bmp, string savePath)
        {
            if (!string.IsNullOrEmpty(savePath))
            {
                if (bmp != null)
                {
                    try
                    {
                        //byte[] bytes = bmp0.ToMemoryStreamBytes();
                        //var ms = new MemoryStream(bytes);
                        //Bitmap bmp = Bitmap.FromStream(ms) as Bitmap;
                        Bitmap bitmap = DeepCopyBitmap(bmp);
                        int srcWidth = bitmap.Width;
                        int srcHeight = bitmap.Height;
                        int tWidth = srcWidth > 1000 ? 1000 : srcWidth;
                        int tHeight = srcWidth > 1000 ? (int)((1000f / srcWidth) * srcHeight) : srcHeight;
                        Color color = bitmap.GetPixel(srcWidth / 2, srcHeight / 2);
                        SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(155, 255 - color.R, 255 - color.G, 255 - color.B));
                        //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(120, 177, 171, 171));
                        Font crFont = new Font("微软雅黑", 10, System.Drawing.FontStyle.Bold);
                        byte[] CheckByte = new byte[3] { 0xAC, 0xFF, 0xAC };
                        Bitmap bmpThumbnail = new Bitmap(tWidth, tHeight);
                        using (Graphics g = Graphics.FromImage(bmpThumbnail))
                        {
                            string text = "LINTSENSE-灵图慧视版权所有";
                            int width = bmpThumbnail.Width, height = bmpThumbnail.Height;
                            SizeF crSize = new SizeF();
                            crSize = g.MeasureString(text, crFont);
                            g.DrawImage(bitmap, new Rectangle(0, 0, tWidth, tHeight), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                            int xcount = (int)(tWidth / crSize.Width - 2), ycount = (int)(tHeight / crSize.Height);
                            if (xcount <= 0)
                            {
                                xcount = 1;
                            }
                            if (ycount <= 0)
                            {
                                ycount = 1;
                            }
                            ycount = ycount > 1 ? ycount - 1 : ycount;
                            int xspacing = tWidth / xcount, yspacing = tHeight / ycount;
                            for (int i = 0; i < xcount * ycount; i++)
                            {
                                int r = i / xcount;
                                int top = (yspacing - (int)crSize.Height) / 2 + r * yspacing;
                                //if((r%2==0&& i % 2==0)|| (r % 2 != 0 && i % 2 == 0))
                                g.DrawString(text, crFont, semiTransBrush, new Rectangle((xspacing - (int)crSize.Width) / 2 + (i % xcount) * xspacing, top, xspacing, yspacing));
                            }
                        }
                        byte[] byteArrthumbnail = BitmaptOByte(bmpThumbnail, ImageFormat.Jpeg);

                        ImageFormat format = bmp.RawFormat as ImageFormat;
                        byte[] byteArrSrc = BitmaptOByte(bmp, format == null ? ImageFormat.Jpeg : format);
                        byte[] erpdata = EncryptHelper.Encrypt3DES(byteArrSrc, Key);
                        byte[] allData = byteArrthumbnail.Concat(CheckByte).Concat(erpdata).ToArray();
                        using (FileStream savefile = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            savefile.Write(allData, 0, allData.Count());
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                        throw ex;
                    }
                }
                return false;
            }
            else
            {
                throw new Exception("图像保存路径不得为空");
            }
        }

        public static Bitmap BytesToImage(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                using (stream = new MemoryStream(Bytes))
                {
                    using (Bitmap bmp = new Bitmap(stream))
                    {
                        //Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height, bmp.PixelFormat);
                        ////将第一个bmp拷贝到bmp2中
                        //using (Graphics draw = Graphics.FromImage(bmp2))
                        //{
                        //    draw.SmoothingMode = SmoothingMode.AntiAlias;
                        //    draw.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        //    draw.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
                        //}
                        Bitmap dstBitmap = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
                        return dstBitmap;//new Bitmap(bmp);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                LogHelper.Default.Error(ex);
                throw ex;
            }
            catch (ArgumentException ex)
            {
                LogHelper.Default.Error(ex);
                throw ex;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
        }

        public static bool SaveEncryptBitmap(byte[] bmp, string savePath)
        {
            if (!string.IsNullOrEmpty(savePath))
            {
                if (bmp != null)
                {
                    try
                    {
                        //byte[] bytes = bmp0.ToMemoryStreamBytes();
                        //var ms = new MemoryStream(bytes);
                        //Bitmap bmp = Bitmap.FromStream(ms) as Bitmap;
                        Bitmap bitmap = BytesToImage(bmp);
                        int srcWidth = bitmap.Width;
                        int srcHeight = bitmap.Height;
                        int tWidth = srcWidth > 1000 ? 1000 : srcWidth;
                        int tHeight = srcWidth > 1000 ? (int)((1000f / srcWidth) * srcHeight) : srcHeight;
                        Color color = bitmap.GetPixel(srcWidth / 2, srcHeight / 2);
                        SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(155, 255 - color.R, 255 - color.G, 255 - color.B));
                        //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(120, 177, 171, 171));
                        Font crFont = new Font("微软雅黑", 10, System.Drawing.FontStyle.Bold);
                        byte[] CheckByte = new byte[3] { 0xAC, 0xFF, 0xAC };
                        Bitmap bmpThumbnail = new Bitmap(tWidth, tHeight);
                        using (Graphics g = Graphics.FromImage(bmpThumbnail))
                        {
                            string text = "LINTSENSE-灵图慧视版权所有";
                            int width = bmpThumbnail.Width, height = bmpThumbnail.Height;
                            SizeF crSize = new SizeF();
                            crSize = g.MeasureString(text, crFont);
                            g.DrawImage(bitmap, new Rectangle(0, 0, tWidth, tHeight), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                            int xcount = (int)(tWidth / crSize.Width - 2), ycount = (int)(tHeight / crSize.Height);
                            if (xcount <= 0)
                            {
                                xcount = 1;
                            }
                            if (ycount <= 0)
                            {
                                ycount = 1;
                            }
                            ycount = ycount > 1 ? ycount - 1 : ycount;
                            int xspacing = tWidth / xcount, yspacing = tHeight / ycount;
                            for (int i = 0; i < xcount * ycount; i++)
                            {
                                int r = i / xcount;
                                int top = (yspacing - (int)crSize.Height) / 2 + r * yspacing;
                                //if((r%2==0&& i % 2==0)|| (r % 2 != 0 && i % 2 == 0))
                                g.DrawString(text, crFont, semiTransBrush, new Rectangle((xspacing - (int)crSize.Width) / 2 + (i % xcount) * xspacing, top, xspacing, yspacing));
                            }
                        }
                        byte[] byteArrthumbnail = BitmaptOByte(bmpThumbnail, ImageFormat.Jpeg);

                        //ImageFormat format =  as ImageFormat;GetImageFormat(bmp.RawFormat)
                        byte[] byteArrSrc = bmp;// BitmaptOByte(bitmap, ImageFormat.Jpeg);
                        byte[] erpdata = EncryptHelper.Encrypt3DES(byteArrSrc, Key);
                        byte[] allData = byteArrthumbnail.Concat(CheckByte).Concat(erpdata).ToArray();
                        using (FileStream savefile = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            savefile.Write(allData, 0, allData.Count());
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                        throw ex;
                    }
                }
                return false;
            }
            else
            {
                throw new Exception("图像保存路径不得为空");
            }
        }

        public static bool SaveEncryptBitmap(byte[][] bmpArray, string savePath)
        {
            if (!string.IsNullOrEmpty(savePath))
            {
                if (bmpArray != null || bmpArray.Count() != 2)
                {
                    try
                    {
                        //byte[] bytes = bmp0.ToMemoryStreamBytes();
                        //var ms = new MemoryStream(bytes);
                        //Bitmap bmp = Bitmap.FromStream(ms) as Bitmap;
                        Bitmap bitmapLeft = BytesToImage(bmpArray[0]);
                        Bitmap bitmapReight = BytesToImage(bmpArray[1]);
                        int srcWidth = bitmapLeft.Width + bitmapReight.Width;
                        int srcHeight = bitmapLeft.Height;

                        Bitmap bmp = new Bitmap(srcWidth, srcHeight);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.DrawImage(bitmapLeft, 0, 0);
                            g.DrawImage(bitmapReight, bitmapLeft.Width, 0);
                        }

                        int tWidth = srcWidth > 1000 ? 1000 : srcWidth;
                        int tHeight = srcWidth > 1000 ? (int)((1000f / srcWidth) * srcHeight) : srcHeight;
                        Color color = bitmapLeft.GetPixel(bitmapLeft.Width / 2, bitmapLeft.Height / 2);
                        SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(155, 255 - color.R, 255 - color.G, 255 - color.B));

                        //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(120, 177, 171, 171));
                        Font crFont = new Font("微软雅黑", 10, System.Drawing.FontStyle.Bold);
                        byte[] CheckByte = new byte[3] { 0xAC, 0xFF, 0xAC };
                        Bitmap bmpThumbnail = new Bitmap(tWidth, tHeight);
                        using (Graphics g = Graphics.FromImage(bmpThumbnail))
                        {
                            string text = "LINTSENSE-灵图慧视版权所有";
                            int width = bmpThumbnail.Width, height = bmpThumbnail.Height;
                            SizeF crSize = new SizeF();
                            crSize = g.MeasureString(text, crFont);
                            g.DrawImage(bitmapLeft, new Rectangle(0, 0, tWidth / 2, tHeight), new Rectangle(0, 0, bitmapLeft.Width, bitmapLeft.Height), GraphicsUnit.Pixel);
                            g.DrawImage(bitmapReight, new Rectangle(tWidth / 2, 0, tWidth / 2, tHeight), new Rectangle(0, 0, bitmapReight.Width, bitmapReight.Height), GraphicsUnit.Pixel);
                            int xcount = (int)(tWidth / crSize.Width - 2), ycount = (int)(tHeight / crSize.Height);
                            if (xcount <= 0)
                            {
                                xcount = 1;
                            }
                            if (ycount <= 0)
                            {
                                ycount = 1;
                            }
                            ycount = ycount > 1 ? ycount - 1 : ycount;
                            int xspacing = tWidth / xcount, yspacing = tHeight / ycount;
                            for (int i = 0; i < xcount * ycount; i++)
                            {
                                int r = i / xcount;
                                int top = (yspacing - (int)crSize.Height) / 2 + r * yspacing;
                                //if((r%2==0&& i % 2==0)|| (r % 2 != 0 && i % 2 == 0))
                                g.DrawString(text, crFont, semiTransBrush, new Rectangle((xspacing - (int)crSize.Width) / 2 + (i % xcount) * xspacing, top, xspacing, yspacing));
                            }
                        }
                        byte[] byteArrthumbnail = BitmaptOByte(bmpThumbnail, ImageFormat.Jpeg);

                        //ImageFormat format =  as ImageFormat;GetImageFormat(bmp.RawFormat)
                        byte[] byteArrSrc =  BitmaptOByte(bmp, ImageFormat.Jpeg);
                        byte[] erpdata = EncryptHelper.Encrypt3DES(byteArrSrc, Key);
                        byte[] allData = byteArrthumbnail.Concat(CheckByte).Concat(erpdata).ToArray();
                        using (FileStream savefile = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            savefile.Write(allData, 0, allData.Count());
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                        
                        LogHelper.Default.Error(ex);
                        throw ex;
                    }
                }
                return false;
            }
            else
            {
                throw new Exception("图像保存路径不得为空");
            }
        }

        public static byte[] BitmaptOByte(Bitmap bitmap, ImageFormat format)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    bitmap.Save(stream, GetImageFormat(format));
                    byte[] data = new byte[stream.Length];
                    data = stream.ToArray();
                    //stream.Seek(0, SeekOrigin.Begin);
                    //stream.Read(data, 0, Convert.ToInt32(stream.Length));
                    return data;
                }
                finally
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        private static ImageFormat GetImageFormat(ImageFormat format)
        {
            if (format.Guid == ImageFormat.Bmp.Guid || format.Guid == ImageFormat.Gif.Guid ||
                format.Guid == ImageFormat.Exif.Guid || format.Guid == ImageFormat.Jpeg.Guid ||
                format.Guid == ImageFormat.Png.Guid || format.Guid == ImageFormat.Tiff.Guid
                )
            {
                return format;
            }
            else
            {
                return ImageFormat.Jpeg;
            }
        }

        public static Bitmap DeepCopyBitmap1(Bitmap bitmap)

        {
            try
            {
                Bitmap dstBitmap = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, bitmap);
                    ms.Seek(0, SeekOrigin.Begin);
                    dstBitmap = (Bitmap)bf.Deserialize(ms);
                    ms.Close();
                }
                return dstBitmap;
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error(ex);
                //errMsg = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap DeepCopyBitmap(Bitmap bitmap)
        {
            //Stopwatch watch = Stopwatch.StartNew();
            try
            {
                //Image<Hsv,int> 
                if (bitmap == null || bitmap.PixelFormat == PixelFormat.DontCare)
                    return null;

                var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                Bitmap temp = new Bitmap(rect.Width, rect.Height, bitmap.PixelFormat);
                var palette = bitmap.Palette;
                if (palette.Entries.Length > 0)
                    temp.Palette = palette;
                var tempData = temp.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                var bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int length = bmpData.Stride * bmpData.Height;
                int tLength = tempData.Stride * tempData.Height;
                byte[] pixs = new byte[length];
                Marshal.Copy(bmpData.Scan0, pixs, 0, length);
                bitmap.UnlockBits(bmpData);
                Marshal.Copy(pixs, 0, tempData.Scan0, tLength);
                temp.UnlockBits(tempData);
                return temp;
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error(ex);
                return null;
            }
            finally
            {
            }
        }

        public static Bitmap DeepCopyBitmap2(Bitmap bitmap)
        {
            try
            {
                Bitmap bmp2 = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
                //将第一个bmp拷贝到bmp2中
                using (Graphics draw = Graphics.FromImage(bmp2))
                {
                    draw.DrawImage(bitmap, 0, 0);

                }
                return bmp2;
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error(ex);
                return null;
            }
            finally
            {
            }
        }
    }
}
