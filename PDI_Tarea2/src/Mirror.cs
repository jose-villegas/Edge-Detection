using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Tarea2
{
    public static class Mirror
    {
        public static Bitmap MirrorVertical(Bitmap src)
        {
            if (src != null)
            {
                Bitmap dst = new Bitmap(src.Width, src.Height);
                BitmapData srcBmData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, src.PixelFormat);
                BitmapData dstBmData = dst.LockBits(new Rectangle(0, 0, dst.Width, dst.Height), ImageLockMode.ReadWrite, src.PixelFormat);

                try
                {
                    int height = src.Height;
                    int width = src.Width;
                    int stride = srcBmData.Stride;
                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(src.PixelFormat) / 8;
                    int offset = stride - width * bytesPerPixel;
                    int totalLength = Math.Abs(stride) * src.Height;
                    IntPtr srcPtr = srcBmData.Scan0;
                    IntPtr dstPtr = dstBmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    byte[] srcData = new byte[totalLength];
                    byte[] dstData = new byte[totalLength];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcData, 0, totalLength);
                    // Posicion Inicial
                    int beginPos = 0;
                    // Posicion Final
                    int endPos = (src.Height - 1) * stride;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            for (int index = 0; index < bytesPerPixel; index++, beginPos++, endPos++)
                            {
                                dstData[beginPos] = srcData[endPos];
                            }
                        }

                        beginPos += offset;
                        endPos += offset - stride - stride;
                    }

                    // Copy the RGB values back to the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(dstData, 0, dstPtr, totalLength);
                }

                finally
                {
                    src.UnlockBits(srcBmData);
                    dst.UnlockBits(dstBmData);
                }

                return dst;
            }

            return null;
        }

        public static Bitmap MirrorHorizontal(Bitmap src)
        {
            if (src != null)
            {
                Bitmap dst = new Bitmap(src.Width, src.Height);
                BitmapData srcBmData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, src.PixelFormat);
                BitmapData dstBmData = dst.LockBits(new Rectangle(0, 0, dst.Width, dst.Height), ImageLockMode.ReadWrite, src.PixelFormat);

                try
                {
                    int height = src.Height;
                    int width = src.Width;
                    int stride = srcBmData.Stride;
                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(src.PixelFormat) / 8;
                    int offset = stride - width * bytesPerPixel;
                    int totalLength = Math.Abs(stride) * src.Height;
                    IntPtr srcPtr = srcBmData.Scan0;
                    IntPtr dstPtr = dstBmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    byte[] srcData = new byte[totalLength];
                    byte[] dstData = new byte[totalLength];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcData, 0, totalLength);
                    // Posicion Inicial
                    int beginPos = 0;
                    // Posicion Final
                    int endPos = (src.Width - 1) * bytesPerPixel;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++, endPos += -bytesPerPixel - bytesPerPixel )
                        {
                            for (int index = 0; index < bytesPerPixel; index++, beginPos++, endPos++)
                            {
                                dstData[beginPos] = srcData[endPos];
                            }
                        }

                        beginPos += offset;
                        endPos = beginPos + (src.Width - 1) * bytesPerPixel;
                    }

                    // Copy the RGB values back to the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(dstData, 0, dstPtr, totalLength);
                }

                finally
                {
                    src.UnlockBits(srcBmData);
                    dst.UnlockBits(dstBmData);
                }

                return dst;
            }

            return null;
        }
    }
}
