using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDI_Tarea2
{
    public static class Colors
    {

        public static Bitmap Negativo(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                Bitmap res = new Bitmap(bitmap.Width, bitmap.Height);
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                BitmapData dstBmData = res.LockBits(new Rectangle(0, 0, res.Width, res.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                try
                {
                    int height = bitmap.Height;
                    int width = bitmap.Width;
                    int stride = bmData.Stride;
                    int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                    int offset = stride - width * bytesPerPixel;
                    int totalLength = Math.Abs(stride) * bitmap.Height;
                    IntPtr ptr = bmData.Scan0;
                    IntPtr dstPtr = dstBmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    byte[] rgbValues = new byte[totalLength];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, totalLength);
                    int i = 0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++, i += bytesPerPixel)
                        {
                            rgbValues[i] = (byte)(255 - rgbValues[i]);
                            rgbValues[i + 1] = (byte)(255 - rgbValues[i + 1]);
                            rgbValues[i + 2] = (byte)(255 - rgbValues[i + 2]);
                        }

                        i += offset;
                    }

                    // Copiamos los valores de vuelta al bitmap
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, dstPtr, totalLength);
                }

                finally
                {
                    bitmap.UnlockBits(bmData);
                    res.UnlockBits(dstBmData);
                }

                return res;
            }

            return null;
        }

        public static Bitmap ToGrayScale(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                Bitmap res = new Bitmap(bitmap.Width, bitmap.Height);
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                BitmapData dstBmData = res.LockBits(new Rectangle(0, 0, res.Width, res.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                try
                {
                    int height = bitmap.Height;
                    int width = bitmap.Width;
                    int stride = bmData.Stride;
                    int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                    int offset = stride - width * bytesPerPixel;
                    int totalLength = Math.Abs(stride) * bitmap.Height;
                    IntPtr ptr = bmData.Scan0;
                    IntPtr dstPtr = dstBmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    byte[] rgbValues = new byte[totalLength];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, totalLength);
                    int i = 0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++, i += bytesPerPixel)
                        {
                            rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = (byte)(0.21f * rgbValues[i] + 0.71f * rgbValues[i + 1] + 0.07f * rgbValues[i + 1]);
                        }

                        i += offset;
                    }

                    // Copiamos los valores de vuelta al bitmap
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, dstPtr, totalLength);
                }

                finally
                {
                    bitmap.UnlockBits(bmData);
                    res.UnlockBits(dstBmData);
                }

                return res;
            }

            return null;
        }

        public static Bitmap BrigthnessAndContrast(Bitmap bitmap, int brightness, int constrast)
        {
            if (bitmap != null)
            {
                Bitmap res = new Bitmap(bitmap.Width, bitmap.Height);
                BitmapData srcBmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                BitmapData dstBmData = res.LockBits(new Rectangle(0, 0, res.Width, res.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                try
                {
                    int height = bitmap.Height;
                    int width = bitmap.Width;
                    int stride = srcBmData.Stride;
                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                    int offset = stride - width * bytesPerPixel;
                    int bytes = Math.Abs(stride) * bitmap.Height;
                    IntPtr srcPtr = srcBmData.Scan0;
                    IntPtr dstPtr = dstBmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    byte[] srcData = new byte[bytes];
                    byte[] dstData = new byte[bytes];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcData, 0, bytes);
                    double contrastFactor = (100.0 + constrast) / 100.0;
                    contrastFactor *= contrastFactor;
                    int i = 0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            for (int index = 0; index < bytesPerPixel; index++, i++)
                            {
                                dstData[i] = (byte)Colors.Clamp(srcData[i] + (brightness * 255 / 100), 0, 255);
                                dstData[i] = (byte)Colors.Clamp((((dstData[i] / 255.0 - 0.5) * contrastFactor) + 0.5) * 255.0, 0, 255);
                            }
                        }

                        i += offset;
                    }

                    // Copy the RGB values back to the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(dstData, 0, dstPtr, bytes);
                }

                finally
                {
                    bitmap.UnlockBits(srcBmData);
                    res.UnlockBits(dstBmData);
                }

                return res;
            }

            return null;
        }

        public static Bitmap OtsuThreshold(Bitmap bitmap, int threshold)
        {
            if (bitmap != null)
            {
                Bitmap dstBitmap = new Bitmap(bitmap.Width, bitmap.Height);
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                BitmapData dstBmData = dstBitmap.LockBits(new Rectangle(0, 0, dstBitmap.Width, dstBitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                try
                {
                    int height = bitmap.Height;
                    int width = bitmap.Width;
                    int stride = bmData.Stride;
                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                    int offset = stride - width * bytesPerPixel;
                    int bytes = Math.Abs(stride) * bitmap.Height;
                    IntPtr srcPtr = bmData.Scan0;
                    IntPtr dstPtr = dstBmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    byte[] srcData = new byte[bytes];
                    byte[] dstData = new byte[bytes];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcData, 0, bytes);
                    int i = 0;

                    // Otsu Threshold
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            for (int index = 0; index < bytesPerPixel; index++, i++)
                            {
                                dstData[i] = (byte)((srcData[i] >= (byte)threshold) ? 255 : 0);
                            }
                        }

                        i += offset;
                    }

                    // Copy the RGB values back to the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(dstData, 0, dstPtr, bytes);
                }

                finally
                {
                    bitmap.UnlockBits(bmData);
                    dstBitmap.UnlockBits(dstBmData);
                }

                return dstBitmap;
            }

            return null;
        }

        public static int[][] GetRGBHistogram(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                int[][] RGBHist = { new int[256], new int[256], new int[256] };
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                try
                {
                    int stride = bmData.Stride;
                    int bytesPerPixel = (bitmap.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4);
                    IntPtr ptr = bmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    int bytes = Math.Abs(stride) * bitmap.Height;
                    byte[] rgbValues = new byte[bytes];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                    // Invertimos los valores
                    for (int i = 0; i < rgbValues.Length; i += bytesPerPixel)
                    {
                        ++RGBHist[0][rgbValues[i]];
                        ++RGBHist[1][rgbValues[i + 1]];
                        ++RGBHist[2][rgbValues[i + 2]];
                    }

                    // Copy the RGB values back to the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                }

                finally
                {
                    bitmap.UnlockBits(bmData);
                }

                return RGBHist;
            }

            return null;
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min;
            }

            else if (val.CompareTo(max) > 0)
            {
                return max;
            }

            else { return val; }
        }

    }
}
