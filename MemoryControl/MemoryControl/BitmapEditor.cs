using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MemoryControl
{
    class BitmapEditor : IDisposable
    {
        private readonly Bitmap _bitmap;
        private readonly BitmapData _bmpData;

        public BitmapEditor(Bitmap bitmap)
        {
            _bitmap = bitmap;
            var rect = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
            _bmpData = _bitmap.LockBits(rect, ImageLockMode.ReadWrite, _bitmap.PixelFormat);
        }

        private bool CheckCoordinates(int x, int y)
        {
            return x >= _bmpData.Width || y >= _bmpData.Height;
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            if (CheckCoordinates(x, y))
            {
                throw new ArgumentException("not valid coordinates");
            }

            var scan = _bmpData.Scan0 + y*_bmpData.Stride + x*3;
            var rgbValues = new byte[3];

            Marshal.Copy(scan, rgbValues, 0, 3);

            rgbValues[0] = r;
            rgbValues[1] = g;
            rgbValues[2] = b;

            Marshal.Copy(rgbValues, 0, scan, 3);
        }

        public void Dispose()
        {
            _bitmap.UnlockBits(_bmpData);
            _bitmap.Save("d:/tmp.bmp");
            _bitmap.Dispose();
        }

        public static void Main(string[] args)
        {
            var bitmap = (Bitmap) Image.FromFile("d:/sis.bmp");
            using (var bitmapEditor = new BitmapEditor(bitmap))
            {
                var timer = new Timer();
                using (timer.Start())
                {
                    for (var i = 0; i < 206; i++)
                    {
                        for (var j = 0; j < 137; j++)
                        {
                            var r = (byte) new Random().Next(0, 85);
                            var g = (byte) new Random().Next(85, 170);
                            var b = (byte) new Random().Next(170, 255);
                            bitmapEditor.SetPixel(i, j, r, g, b);
                        }
                    }
                }
                Console.WriteLine((double) timer.ElapsedMilliseconds/1000);

                var timer1 = new Timer();
                var bitmap1 = (Bitmap) Image.FromFile("d:/sis.bmp");
                using (timer1.Start())
                {
                    for (var i = 0; i < 206; i++)
                    {
                        for (var j = 0; j < 137; j++)
                        {
                            var r = (byte) new Random().Next(85, 170);
                            var g = (byte) new Random().Next(0, 170);
                            var b = (byte) new Random().Next(170, 255);
                            var color = Color.FromArgb(r, g, b);
                            bitmap1.SetPixel(i, j, color);
                        }
                    }
                }
                Console.WriteLine((double) timer1.ElapsedMilliseconds/1000);
                Console.ReadLine();
            }
        }
    }
}