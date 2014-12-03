using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Tarea2
{

    public static class Cache
    {
        private static Form1 owner;
        private static Bitmap original;
        private static List<Bitmap> history = new List<Bitmap>();
        private static Stack<Bitmap> redoHistory = new Stack<Bitmap>();
        private static int historySize;

        public static void Start(Bitmap bitmap)
        {
            while (redoHistory.Count > 0)
            {
                redoHistory.Pop().Dispose();
            }

            while (history.Count > 0)
            {
                history.First().Dispose();
                history.RemoveAt(0);
            }

            StoreOriginalBitmap(bitmap);
        }

        public static void SetOwner(Form1 form, int size)
        {
            owner = form;
            historySize = size;
        }

        private static void StoreOriginalBitmap(Bitmap bitmap)
        {
            original = (Bitmap)bitmap.Clone();
        }

        public static byte[] GetOriginalBitmapData()
        {
            return GetByteDataFromBitmap(original);
        }

        public static void RestoreToOriginalBitmap()
        {
            if (original != null)
            {
                owner.setPictureBoxBitmap(original);
                history.Clear();
                redoHistory.Clear();
                owner.SetUndo(false);
                owner.SetRedo(false);
            }
        }

        public static Bitmap GetCurrentBitmap()
        {
            if (owner.getCurrentBitmap() != null)
            {
                return (Bitmap)owner.getCurrentBitmap().Clone();
            }

            else
            {
                return null;
            }
        }

        public static void SetMainformPictureBox(Bitmap bitmap)
        {
            owner.setPictureBoxBitmap(bitmap);
        }

        public static byte[] GetByteDataFromBitmap(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                byte[] rgbValues;

                try
                {
                    int stride = bmData.Stride;
                    IntPtr ptr = bmData.Scan0;
                    // Declaramos un arreglo para guardar toda la data.
                    int bytes = Math.Abs(stride) * bitmap.Height;
                    rgbValues = new byte[bytes];
                    // Copiamos los valores RGB en el arreglo.
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                }

                finally
                {
                    bitmap.UnlockBits(bmData);
                }

                return rgbValues;
            }

            return null;
        }

        public static void StoreCurrentBitmapData()
        {
            if (history.Count == historySize)
            {
                history.First().Dispose();
                history.RemoveAt(0);
            }

            if (owner.getCurrentBitmap() != null)
            {
                history.Add((Bitmap)owner.getCurrentBitmap().Clone());
                owner.SetUndo(true);
            }

            if (redoHistory.Count > 0)
            {
                while (redoHistory.Count > 0)
                {
                    redoHistory.Pop().Dispose();
                }

                owner.SetRedo(false);
            }
        }

        public static void ToPreviousBitmapData()
        {
            if (history.Count > 0)
            {
                owner.setPictureBoxBitmap(history.Last());
            }

            else
            {
                owner.setPictureBoxBitmap(original);
            }
        }

        public static void UndoBitmap()
        {
            if (history.Count > 0)
            {
                redoHistory.Push(history.Last());
                history.RemoveAt(history.Count - 1);
                owner.SetRedo(true);

                if (history.Count > 0)
                {
                    owner.setPictureBoxBitmap(history.Last());
                }

                else
                {
                    owner.setPictureBoxBitmap(original);
                    owner.SetUndo(false);
                }
            }
        }

        public static void RedoBitmap()
        {
            if (redoHistory.Count > 0)
            {
                history.Add(redoHistory.Pop());
                owner.setPictureBoxBitmap(history.Last());
                owner.SetUndo(true);

                if (redoHistory.Count == 0)
                {
                    owner.SetRedo(false);
                }
            }
        }
    }
}
