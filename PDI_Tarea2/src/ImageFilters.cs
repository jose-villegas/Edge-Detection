using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Tarea2
{
    public static class ImageFilters
    {
        private static double[,] kernel1;
        private static double[,] kernel2;
        private static bool grayScaleMode;
        private static int KernelMiddlePoint;
        private static int numkernels;
        private static Bitmap bitmap;
        private static byte[] rgbValuesOrig;

        public static void LoadPersonalKernel()
        {
            kernel1 = new double[,]
            {
                { -1, -1, -1 },
                { -1, 9, -1 },
                { -1, -1, -1 }
            };
            numkernels = 1;
            KernelMiddlePoint = 1;
        }

        #region [DefaultKernels]

        public static void LoadMeanKernel()
        {
            kernel1 = new double[,]
            {
                { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 },
                { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 },
                { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 }
            };
            numkernels = 1;
            KernelMiddlePoint = 1;
        }

        public static void LoadSobelKernels_3x3()
        {
            kernel1 = new double[,]
            {
                { 1, 0, -1 },
                { 2, 0, -2 },
                { 1, 0, -1 }
            };
            kernel2 = new double[,]
            {
                { 1, 2, 1 },
                { 0, 0, 0 },
                { -1, -2, -1}
            };
            numkernels = 2;
            KernelMiddlePoint = 1;
        }

        public static void LoadSobelKernels_5x5()
        {
            kernel1 = new double[,]
            {
                { 2, 1, 0, -1, -2 },
                { 3, 2, 0, -2, -3 },
                { 4, 3, 0, -3, -4 },
                { 3, 2, 0, -2, -3 },
                { 2, 1, 0, -1, -2 }
            };
            kernel2 = new double[,]
            {
                { 2, 3, 4, 3, 2 },
                { 1, 2, 3, 2, 1 },
                { 0, 0, 0, 0, 0 },
                { -1, -2, -3, -2, -1},
                { -2, -3, -4, -3, -2}
            };
            numkernels = 2;
            KernelMiddlePoint = 2;
        }

        public static void LoadSobelKernels_7x7()
        {
            kernel1 = new double[,]
            {
                { 3, 2, 1, 0, -1, -2, -3},
                { 4, 3, 2, 0, -2, -3, -4},
                { 5, 4, 3, 0, -3, -4, -5},
                { 6, 5, 4, 0, -4, -5, -6},
                { 5, 4, 3, 0, -3, -4, -5},
                { 4, 3, 2, 0, -2, -3, -4},
                { 3, 2, 1, 0, -1, -2, -3}
            };
            kernel2 = new double[,]
            {
                { 3, 4, 5, 6, 5, 4, 3 },
                { 2, 3, 4, 5, 4, 3, 2 },
                { 1, 2, 3, 4, 3, 2, 1 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { -1, -2, -3, -4, -3, -2, -1 },
                { -2, -3, -4, -5, -4, -3, -2 },
                { -3, -4, -5, -6, -5, -4, -3 }
            };
            numkernels = 2;
            KernelMiddlePoint = 3;
        }

        public static void LoadSobelKernels_9x9()
        {
            kernel1 = new double[,]
            {
                { 4, 3, 2, 1, 0, -1, -2, -3, -4 },
                { 5, 4, 3, 2, 0, -2, -3, -4, -5 },
                { 6, 5, 4, 3, 0, -3, -4, -5, -6 },
                { 7, 6, 5, 4, 0, -4, -5, -6, -7 },
                { 8, 7, 6, 5, 0, -5, -6, -7, -8 },
                { 7, 6, 5, 4, 0, -4, -5, -6, -7 },
                { 6, 5, 4, 3, 0, -3, -4, -5, -6 },
                { 5, 4, 3, 2, 0, -2, -3, -4, -5 },
                { 4, 3, 2, 1, 0, -1, -2, -3, -4 }
            };
            kernel2 = new double[,]
            {
                { 4, 5, 6, 7, 8, 7, 6, 5, 4 },
                { 3, 4, 5, 6, 7, 6, 5, 4, 3 },
                { 2, 3, 4, 5, 6, 5, 4, 3, 2 },
                { 1, 2, 3, 4, 5, 4, 3, 2, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { -1, -2, -3, -4, -5, -4, -3, -2, -1 },
                { -2, -3, -4, -5, -6, -5, -4, -3, -2 },
                { -3, -4, -5, -6, -7, -6, -5, -4, -3 },
                { -4, -5, -6, -7, -8, -7, -6, -5, -4 }
            };
            numkernels = 2;
            KernelMiddlePoint = 4;
        }

        public static void LoadRobertsCrossKernels_3x3()
        {
            kernel1 = new double[,]
            {
                { 0, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, -1 }
            };
            kernel2 = new double[,]
            {
                { 0, 0, 0 },
                { 0, 1, 0 },
                { -1, 0, 0 }
            };
            numkernels = 2;
            KernelMiddlePoint = 1;
        }

        public static void LoadPrewittKernels_3x3()
        {
            kernel1 = new double[,]
            {
                { -1, 0, 1 },
                { -1, 0, 1 },
                { -1, 0, 1 }
            };
            kernel2 = new double[,]
            {
                { 1, 1, 1 },
                { 0, 0, 0 },
                { -1, -1, -1}
            };
            numkernels = 2;
            KernelMiddlePoint = 1;
        }

        public static void LoadPrewittKernels_5x5()
        {
            kernel1 = new double[,]
            {
                { 2, 1, 0, -1, -2 },
                { 2, 1, 0, -1, -2 },
                { 2, 1, 0, -1, -2 },
                { 2, 1, 0, -1, -2 },
                { 2, 1, 0, -1, -2 },
            };
            kernel2 = new double[,]
            {
                { 2, 2, 2, 2, 2 },
                { 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0 },
                { -1, -1, -1, -1, -1},
                { -2, -2, -2, -2, -2}
            };
            numkernels = 2;
            KernelMiddlePoint = 2;
        }

        public static void LoadPrewittKernels_7x7()
        {
            kernel1 = new double[,]
            {
                { 3, 2, 1, 0, -1, -2, -3 },
                { 3, 2, 1, 0, -1, -2, -3 },
                { 3, 2, 1, 0, -1, -2, -3 },
                { 3, 2, 1, 0, -1, -2, -3 },
                { 3, 2, 1, 0, -1, -2, -3 },
                { 3, 2, 1, 0, -1, -2, -3 },
                { 3, 2, 1, 0, -1, -2, -3 },
            };
            kernel2 = new double[,]
            {
                { 3, 3, 3, 3, 3, 3, 3 },
                { 2, 2, 2, 2, 2, 2, 2 },
                { 1, 1, 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { -1, -1, -1, -1, -1, -1, -1},
                { -2, -2, -2, -2, -2, -2, -2},
                { -3, -3, -3, -3, -3, -3, -3}
            };
            numkernels = 2;
            KernelMiddlePoint = 3;
        }

        public static void LoadLaplacianOfGaussian_3x3()
        {
            kernel1 = new double[,]
            {
                { 0, -1, 0 },
                { -1, 4, -1 },
                { 0, -1, 0 }
            };
            kernel2 = new double[,]
            {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 },
            };
            numkernels = 1;
            KernelMiddlePoint = 1;
        }

        public static void LoadLaplacianOfGaussian_5x5()
        {
            kernel1 = new double[,]
            {
                { 0, 0, -1, 0, 0 },
                { 0, -1, -2, -1, 0 },
                { -1, -2, 16, -2, -1 },
                { 0, -1, -2, -1, 0 },
                { 0, 0, -1, 0, 0 },
            };
            numkernels = 1;
            KernelMiddlePoint = 2;
        }

        public static void LoadLaplacianOfGaussian_7x7()
        {
            kernel1 = new double[,]
            {
                { 0, 0, 0, -1, 0, 0, 0 },
                { 0, 0, -1, -2, -1, 0, 0 },
                { 0, -1, -2, -3, -2, -1, 0 },
                { -1, -2, -3, 40, -3, -2, -1 },
                { 0, -1, -2, -3, -2, -1, 0 },
                { 0, 0, -1, -2, -1, 0, 0 },
                { 0, 0, 0, -1, 0, 0, 0 },
            };
            numkernels = 1;
            KernelMiddlePoint = 3;
        }

        public static void LoadLaplacianOfGaussian_9x9()
        {
            kernel1 = new double[,]
            {
                { 0, -1, -1, -2, -2, -2, -1, -1, 0 },
                { -1, -2, -4, -5, -5, -5, -4, -2, -1 },
                { -1, -4, -5, -3, 0, -3, -5, -4, -1 },
                { -2, -5, -3, 12, 24, 12, -3, -5, -2 },
                { -2, -5, 0, 24, 40, 24, 0, -5, -2 },
                { -2, -5, -3, 12, 24, 12, -3, -5, -2 },
                { -1, -4, -5, -3, 0, -3, -5, -4, -1 },
                { -1, -2, -4, -5, -5, -5, -4, -2, -1 },
                { 0, -1, -1, -2, -2, -2, -1, -1, 0 },
            };
            numkernels = 1;
            KernelMiddlePoint = 4;
        }
        #endregion

        public static void EdgeDetectionMode()
        {
            bitmap = Colors.ToGrayScale(bitmap);
            rgbValuesOrig = Cache.GetByteDataFromBitmap(bitmap);
            grayScaleMode = true;
        }

        public static void LoadCurrentBitmap()
        {
            bitmap = Cache.GetCurrentBitmap();
            rgbValuesOrig = Cache.GetByteDataFromBitmap(bitmap);
            grayScaleMode = false;
        }

        public static Bitmap ApplyLaplacianOfGaussianFilter(int size)
        {
            if (size == 3)
            {
                LoadLaplacianOfGaussian_3x3();
            }

            else if (size == 5)
            {
                LoadLaplacianOfGaussian_5x5();
            }

            else if (size == 7)
            {
                LoadLaplacianOfGaussian_7x7();
            }

            else if (size == 9)
            {
                LoadLaplacianOfGaussian_9x9();
            }

            else
            {
                return null;
            }

            LoadedMatrixBitmapConvolution();
            return bitmap;
        }

        public static Bitmap ApplySobelFilter(int size)
        {
            if (size == 3)
            {
                LoadSobelKernels_3x3();
            }

            else if (size == 5)
            {
                LoadSobelKernels_5x5();
            }

            else if (size == 7)
            {
                LoadSobelKernels_7x7();
            }

            else if (size == 9)
            {
                LoadSobelKernels_9x9();
            }

            else
            {
                return null;
            }

            LoadedMatrixBitmapConvolution();
            return bitmap;
        }

        public static Bitmap ApplyRobertsFilter(int size)
        {
            if (size == 3)
            {
                LoadRobertsCrossKernels_3x3();
            }

            else
            {
                return null;
            }

            LoadedMatrixBitmapConvolution();
            return bitmap;
        }

        public static Bitmap ApplyPrewittFilter(int size)
        {
            if (size == 3)
            {
                LoadPrewittKernels_3x3();
            }

            else if (size == 5)
            {
                LoadPrewittKernels_5x5();
            }

            else if (size == 7)
            {
                LoadPrewittKernels_7x7();
            }

            else
            {
                return null;
            }

            LoadedMatrixBitmapConvolution();
            return bitmap;
        }

        public static Bitmap ApplyMeanFilter()
        {
            LoadMeanKernel();
            LoadedMatrixBitmapConvolution();
            return bitmap;
        }

        public static Bitmap ApplyMedianFilter()
        {
            if (bitmap != null)
            {
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                try
                {
                    int height = bitmap.Height;
                    int width = bitmap.Width;
                    int stride = bmData.Stride;
                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                    int totalLength = Math.Abs(stride) * bitmap.Height;
                    IntPtr ptr = bmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    byte[] rgbValues = new byte[totalLength];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, totalLength);

                    for (int y = 1; y < height - 1; y++)
                    {
                        for (int x = 1; x < width - 1; x++)
                        {
                            ApplyMedianPixelArea(rgbValuesOrig, rgbValues, x, y, width, bytesPerPixel);
                        }
                    }

                    // Copy the RGB values back to the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, totalLength);
                }

                finally
                {
                    bitmap.UnlockBits(bmData);
                }

                return bitmap;
            }

            return null;
        }

        public static Bitmap ApplyPersonalKernel()
        {
            LoadPersonalKernel();
            LoadedMatrixBitmapConvolution();
            return bitmap;
        }

        public static void LoadedMatrixBitmapConvolution()
        {
            if (bitmap != null)
            {
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                try
                {
                    int height = bitmap.Height;
                    int width = bitmap.Width;
                    int stride = bmData.Stride;
                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                    int totalLength = Math.Abs(stride) * bitmap.Height;
                    IntPtr ptr = bmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    byte[] rgbValues = new byte[totalLength];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, totalLength);

                    for (int y = KernelMiddlePoint; y < height - KernelMiddlePoint; y++)
                    {
                        for (int x = KernelMiddlePoint; x < width - KernelMiddlePoint; x++)
                        {
                            if (grayScaleMode)
                            {
                                ApplyMatrixToPixel(rgbValuesOrig, rgbValues, x, y, width, bytesPerPixel);
                            }

                            else
                            {
                                ApplyMatrixToPixel_AllChannels(rgbValuesOrig, rgbValues, x, y, width, bytesPerPixel);
                            }
                        }
                    }

                    // Copy the RGB values back to the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, totalLength);
                }

                finally
                {
                    bitmap.UnlockBits(bmData);
                }
            }
        }

        private static void ApplyMatrixToPixel(byte[] src, byte[] dst, int x, int y, int width, int bytesPerPixel)
        {
            double finalX = 0, finalY = 0; ;
            int pos = 0;

            for (int i = 0; i < kernel1.GetLength(0); i++)
            {
                for (int j = 0; j < kernel1.GetLength(1); j++)
                {
                    int I = y + i - KernelMiddlePoint;
                    int J = x + j - KernelMiddlePoint;
                    pos = (I * width + J) * bytesPerPixel;
                    finalX += kernel1[i, j] * src[pos];

                    if (numkernels > 1)
                    {
                        finalY += kernel2[i, j] * src[pos];
                    }
                }
            }

            pos = ((y * width) + x) * bytesPerPixel;

            if (numkernels == 2)
            {
                dst[pos] = dst[pos + 1] = dst[pos + 2] = (byte)Colors.Clamp(Math.Sqrt(finalX * finalX + finalY * finalY), 0, 255.0);
            }

            else if (numkernels == 1)
            {
                dst[pos] = dst[pos + 1] = dst[pos + 2] = (byte)Colors.Clamp(Math.Abs(finalX), 0, 255.0);
            }
        }

        private static void ApplyMatrixToPixel_AllChannels(byte[] src, byte[] dst, int x, int y, int width, int bytesPerPixel)
        {
            double final1R, final1G, final1B;
            double final2R, final2G, final2B;
            final1R = final1G = final1B = final2R = final2G = final2B = 0;
            int pos = 0;

            for (int i = 0; i < kernel1.GetLength(0); i++)
            {
                for (int j = 0; j < kernel1.GetLength(1); j++)
                {
                    int I = y + i - KernelMiddlePoint;
                    int J = x + j - KernelMiddlePoint;
                    pos = (I * width + J) * bytesPerPixel;
                    final1R += kernel1[i, j] * src[pos];
                    final1G += kernel1[i, j] * src[pos + 1];
                    final1B += kernel1[i, j] * src[pos + 2];

                    if (numkernels > 1)
                    {
                        final2R += kernel2[i, j] * src[pos];
                        final2G += kernel2[i, j] * src[pos + 1];
                        final2B += kernel2[i, j] * src[pos + 2];
                    }
                }
            }

            pos = ((y * width) + x) * bytesPerPixel;

            if (numkernels == 2)
            {
                dst[pos] = (byte)Colors.Clamp(Math.Sqrt(final1R * final1R + final2R * final2R), 0, 255.0);
                dst[pos + 1] = (byte)Colors.Clamp(Math.Sqrt(final1G * final1G + final2G * final2G), 0, 255.0);
                dst[pos + 2] = (byte)Colors.Clamp(Math.Sqrt(final1B * final1B + final2B * final2B), 0, 255.0);
            }

            else if (numkernels == 1)
            {
                dst[pos] = (byte)Colors.Clamp(Math.Abs(final1R), 0, 255.0);
                dst[pos + 1] = (byte)Colors.Clamp(Math.Abs(final1G), 0, 255.0);
                dst[pos + 2] = (byte)Colors.Clamp(Math.Abs(final1B), 0, 255.0);
            }
        }

        private static void ApplyMedianPixelArea(byte[] src, byte[] dst, int x, int y, int width, int bytesPerPixel)
        {
            int indexR, indexG, indexB;
            List<byte> sortedListR = new List<byte>();
            List<byte> sortedListG = new List<byte>();
            List<byte> sortedListB = new List<byte>();
            int pos = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int I = y + i - 1;
                    int J = x + j - 1;
                    pos = (I * width + J) * bytesPerPixel;
                    indexR = sortedListR.BinarySearch(src[pos]);
                    indexG = sortedListG.BinarySearch(src[pos + 1]);
                    indexB = sortedListB.BinarySearch(src[pos + 1]);

                    if (indexR < 0)
                    {
                        indexR = ~indexR;
                    }

                    if (indexG < 0)
                    {
                        indexG = ~indexG;
                    }

                    if (indexB < 0)
                    {
                        indexB = ~indexB;
                    }

                    sortedListR.Insert(indexR, src[pos]);
                    sortedListG.Insert(indexG, src[pos + 1]);
                    sortedListB.Insert(indexB, src[pos + 2]);
                }
            }

            pos = ((y * width) + x) * bytesPerPixel;
            dst[pos] = sortedListR.ElementAt(4);
            dst[pos + 1] = sortedListG.ElementAt(4);
            dst[pos + 2] = sortedListB.ElementAt(4);
        }


    }
}
