using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Tarea2
{
    public static class Rotate
    {
        public static Bitmap FreeRotationNearestNeighbor(Bitmap src, double angle)
        {
            if (src != null)
            {
                Bitmap dst = new Bitmap(src.Width, src.Height);
                BitmapData bmSrcData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, src.PixelFormat);
                BitmapData bmDstData = dst.LockBits(new Rectangle(0, 0, dst.Width, dst.Height), ImageLockMode.ReadWrite, src.PixelFormat);

                try
                {
                    int bytesPerPixel = Bitmap.GetPixelFormatSize(src.PixelFormat) / 8;
                    int srcStride = bmSrcData.Stride;
                    int dstStride = bmDstData.Stride;
                    IntPtr srcPtr = bmSrcData.Scan0;
                    IntPtr dstPtr = bmDstData.Scan0;
                    int srcBytes = Math.Abs(srcStride) * src.Height;
                    int dstBytes = Math.Abs(dstStride) * dst.Height;
                    byte[] srcData = new byte[srcBytes];
                    byte[] dstData = new byte[dstBytes];
                    double srcXRadius = (double)(src.Width - 1) / 2;
                    double srcYRadius = (double)(src.Height - 1) / 2;
                    double angleRad = -angle * Math.PI / 180;
                    double angleCos = Math.Cos(angleRad);
                    double angleSin = Math.Sin(angleRad);
                    int dstOffset = dstStride - dst.Width * bytesPerPixel;
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcData, 0, srcBytes);
                    // Centro de la Imagen
                    double cx, cy;
                    // Pixeles de origen
                    int ox, oy;
                    // Posiciones originales
                    int dstPos = 0;
                    int srcPos = 0;
                    cy = -srcYRadius;

                    for (int y = 0; y < src.Height; y++)
                    {
                        cx = -srcXRadius;

                        for (int x = 0; x < src.Width; x++, dstPos += bytesPerPixel)
                        {
                            // Coordenadas del punto mas cercano
                            ox = (int)(angleCos * cx + angleSin * cy + srcXRadius);
                            oy = (int)(-angleSin * cx + angleCos * cy + srcYRadius);

                            // Validamos que las coordenadas esten dentro del rango valido
                            if ((ox < 0) || (oy < 0) || (ox >= src.Width) || (oy >= src.Height))
                            {
                                // Colocamos pixeles de relleno si no lo estan
                                for (int i = 0; i < bytesPerPixel; i++)
                                {
                                    dstData[dstPos + i] = 0;
                                }
                            }

                            else
                            {
                                // Si lo estan colocamos ese pixel en la data de destino
                                srcPos = oy * srcStride + ox * bytesPerPixel;

                                for (int i = 0; i < bytesPerPixel; i++)
                                {
                                    dstData[dstPos + i] = srcData[srcPos + i];
                                }
                            }

                            cx++;
                        }

                        cy++;
                        dstPos += dstOffset;
                    }

                    System.Runtime.InteropServices.Marshal.Copy(dstData, 0, dstPtr, dstBytes);
                }

                finally
                {
                    dst.UnlockBits(bmDstData);
                    src.UnlockBits(bmSrcData);
                }

                return dst;
            }

            return null;
        }
    }
}
