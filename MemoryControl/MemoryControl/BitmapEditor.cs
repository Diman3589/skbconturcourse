using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MemoryControl
{
    class BitmapEditor : IDisposable
    {
        private readonly Bitmap _bitmap;
        private Rectangle _rect;
        private BitmapData _bmpData;

        public BitmapEditor(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            _rect = new Rectangle(x, y, _bitmap.Width - x, _bitmap.Height - y);
            _bmpData = _bitmap.LockBits(_rect, ImageLockMode.ReadWrite, _bitmap.PixelFormat);

            var ptr = _bmpData.Scan0;

            const int bytes = 4;
            var rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (var i = 2; i < rgbValues.Length; i += 3)
            {
                rgbValues[i] = r;
                rgbValues[i - 1] = g;
                rgbValues[i - 2] = b;
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);

            _bitmap.UnlockBits(_bmpData);
            _bitmap.Save("d:/tmp.bmp");

        }

        public void Dispose()
        {
            _bitmap.Dispose();
        }

        public static void Main(string[] args)
        {
            var bitmap = (Bitmap) Image.FromFile("d:/sis.bmp");
            using (var bitmapEditor = new BitmapEditor(bitmap))
            {
                //bitmapEditor.SetPixel(20, 10, 255, 0, 0);
                //bitmapEditor.SetPixel(30, 10, 255, 0, 255);
                bitmapEditor.SetPixel(40, 10, 255, 0, 0);
                bitmapEditor.SetPixel(50, 10, 0, 255, 0);
            }
        }
    }
}