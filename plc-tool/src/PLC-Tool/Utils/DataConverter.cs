using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FrameworkCommon.Utils
{
    public static class DataConverter
    {
        /// <summary>
        /// 默认的矩阵宽高比：1440 / 900
        /// </summary>
        public static double DefaultAspectRatio = 1440 / 900d;

        public static T[] ToArray<T>(this T[,] datas)
        {
            var length = datas.Length;
            var col = datas.GetLength(1);
            var result = new T[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = datas[i / col, i % col];
            }

            return result;
        }

        public static T[,] SwapPositon<T>(this T[,] datas)
        {
            int x = datas.GetLength(0);
            int y = datas.GetLength(1);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Swap(ref datas[i, j], ref datas[i, y - j - 1]);
                }
            }

            return datas;
        }

        public static void Swap<T>(ref T X, ref T y)
        {
            T t = X;
            X = y;
            y = t;
        }

        /// <summary>
        /// 将一维数组数据转化为二维数组（如行数为零，默认按照1440 × 900的分辨率排列。如超出长度，末尾填充默认值）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas">一维数据</param>
        /// <param name="width">行数</param>
        /// <param name="isArrangeHorizontally">True:优先按照水平方向排列, False:优先按照竖直方向排列</param>
        /// <returns>T[].</returns>
        /// <exception cref="System.ArgumentNullException">datas;数据源不能为空！</exception>
        public static T[,] To2DMatrix<T>(this T[] datas, int width = 0, bool isArrangeHorizontally = true) where T : struct
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            if (width == 0)
            {
                // R / C = W / H
                // C * R = Length
                var r = Math.Sqrt(datas.Length * DefaultAspectRatio);
                width = Convert.ToInt32(r);
            }

            int height = Convert.ToInt32(Math.Ceiling(datas.Length / (double)width));
            var matrix = new T[width, height];

            if (isArrangeHorizontally)
            {
                //优先按照水平方向排列：先→后↓
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        int index = i * height + j;
                        matrix[i, j] = datas[index];
                    }
                }
            }
            else
            {
                //优先按照竖直方向排列：先↓后→
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int index = i * width + j;
                        matrix[j, i] = datas[index];
                    }
                }
            }

            return matrix;
        }

        /// <summary>
        /// 将一维数组数据转化为二维数组（如行数为零，默认按照1440 × 900的分辨率排列。如超出长度，末尾填充默认值）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas">一维数据</param>
        /// <param name="width">行数</param>
        /// <param name="height">列数</param>
        /// <returns>T[].</returns>
        /// <exception cref="System.ArgumentNullException">datas;数据源不能为空！</exception>
        public static T[,] To2DMatrix<T>(this T[] datas, int width, int height) where T : struct
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            if (datas.Length < width * height)
            {
                throw new ArgumentOutOfRangeException("行列数超出范围！");
            }

            var matrix = new T[width, height];

            //优先按照竖直方向排列：先↓后→
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = i * width + j;
                    matrix[j, i] = datas[index];
                }
            }

            return matrix;
        }

        /// <summary>
        /// 将一维数组数据转化为二维数组
        /// </summary>
        /// <param name="datas">一维数据</param>
        /// <param name="width">行数</param>
        /// <param name="height">列数</param>
        public static byte[,] To2DMatrix(this byte[] datas, int width, int height)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            if (datas.Length < width * height)
            {
                throw new ArgumentOutOfRangeException("行列数超出范围！");
            }

            var matrix = new byte[width, height];
            Buffer.BlockCopy(datas, 0, matrix, 0, matrix.Length);
            return matrix;
        }

        /// <summary>
        /// 将一维数组数据转化为二维数组
        /// </summary>
        /// <param name="datas">一维数据</param>
        /// <param name="width">行数</param>
        /// <param name="height">列数</param>
        public static int[,] To2DMatrix(this int[] datas, int width, int height)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            if (datas.Length < width * height)
            {
                throw new ArgumentOutOfRangeException("行列数超出范围！");
            }

            var matrix = new int[width, height];
            Buffer.BlockCopy(datas, 0, matrix, 0, matrix.Length * 4);
            return matrix;
        }

        /// <summary>
        /// 转化为Double类型的数组
        /// </summary>
        /// <param name="datas">数据源</param>
        public static double[] ToDouble(this int[] datas)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            var doubles = new double[datas.Length];
            Array.Copy(datas, doubles, datas.Length);
            return doubles;
        }

        /// <summary>
        /// 转化为Double类型的二维数组
        /// </summary>
        /// <param name="datas">数据源</param>
        public static double[,] ToDoubleMatrix(this int[,] datas)
        {
            int x = datas.GetLength(0);
            int y = datas.GetLength(1);

            var doubleMatrix = new double[x, y];
            Array.Copy(datas, doubleMatrix, datas.Length);
            return doubleMatrix;
        }

        /// <summary>
        /// 转化为Double类型的二维数组
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="datas">数据源</param>
        /// <param name="isArrangeHorizontally">True:优先按照水平方向排列, False:优先按照竖直方向排列</param>
        public static double[,] ToDoubleMatrix(this int[] datas, int row = 0, bool isArrangeHorizontally = true)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            return datas.ToDouble().To2DMatrix(row, isArrangeHorizontally);
        }

        /// <summary>
        /// 将数据转化为三维空间点矩阵
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="datas">数据</param>
        public static Point3D[,] ToPoint3DMatrix(this double[] datas, int row = 0)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            if (row == 0)
            {
                // R / C = W / H
                // C * R = Length
                var r = Math.Sqrt(datas.Length * DefaultAspectRatio);
                row = Convert.ToInt32(r);
            }

            int col = Convert.ToInt32(Math.Ceiling(datas.Length / (double)row));
            var point3Ds = new Point3D[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    point3Ds[i, j] = new Point3D(i, j, datas[i * col + j]);
                }
            }

            return point3Ds;
        }

        /// <summary>
        /// 将数据转化为三维空间点矩阵
        /// </summary>
        /// <param name="datas">数据</param>
        public static Point3D[,] ToPoint3DMatrix(this double[,] datas)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            int x = datas.GetLength(0);
            int y = datas.GetLength(1);

            var dataPoints = new Point3D[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var point = new Point3D(i, j, datas[i, j]);
                    dataPoints[i, j] = point;
                }
            }

            return dataPoints;
        }

        /// <summary>
        /// 将数据转化为三维空间点矩阵
        /// </summary>
        /// <param name="datas">数据</param>
        /// <param name="row">行数</param>
        public static Point3D[,] ToPoint3DMatrix(this int[] datas, int row = 0)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            if (row == 0)
            {
                // R / C = W / H
                // C * R = Length
                var r = Math.Sqrt(datas.Length * DefaultAspectRatio);
                row = Convert.ToInt32(r);
            }

            int col = Convert.ToInt32(Math.Ceiling(datas.Length / (double)row));
            var point3Ds = new Point3D[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    point3Ds[i, j] = new Point3D(i, j, datas[i * col + j]);
                }
            }

            return point3Ds;
        }

        /// <summary>
        /// 将数据转化为三维空间点矩阵
        /// </summary>
        /// <param name="datas">数据</param>
        public static Point3D[,] ToPoint3DMatrix(this int[,] datas)
        {
            if (datas == null)
            {
                throw new ArgumentNullException("datas", "数据源不能为空！");
            }

            int x = datas.GetLength(0);
            int y = datas.GetLength(1);

            var dataPoints = new Point3D[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var point = new Point3D(i, j, datas[i, j]);
                    dataPoints[i, j] = point;
                }
            }

            return dataPoints;
        }
    }
}
