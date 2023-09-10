using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageRenderingApp
{
    public class ImageRenderer
    {
        private Bitmap bitmap;
        private ImageSource imageSource;
        private int WindowSizeValue;

        public ImageRenderer(int windowSize)
        {
            WindowSizeValue = windowSize;
            bitmap = new Bitmap(WindowSizeValue, WindowSizeValue);
        }

        private System.Drawing.Color GetGreenGradientColor(int value)
        {
            int greenValue = value; // Hier wird der Wert direkt als Grünkomponente verwendet
            if (greenValue <= 0)
            {
                greenValue = 1;
            }
            else if (greenValue > 255)
            {
                greenValue = 255;
            }

            return System.Drawing.Color.FromArgb(0, greenValue, 0);
        }

        public ImageSource RenderImage()
        {
            for (int y = 0; y < WindowSizeValue; y++)
            {
                for (int x = 0; x < WindowSizeValue; x++)
                {
                    double formula = 10 - Math.Cos(x) / Math.Sin(y);
                    double actualY = y - WindowSizeValue / 2;
                    double computedY = formula + WindowSizeValue / 2;

                    int distance = (int)Math.Abs(actualY - computedY);
                    System.Drawing.Color pixelColor = GetGreenGradientColor(distance);

                    bitmap.SetPixel(x, y, pixelColor);
                }
            }

            imageSource = ConvertBitmapToImageSource(bitmap);
            return imageSource;
        }

        public void SaveImage()
        {
            bitmap.Save(@"D:\Tempotary\OutPut.png", ImageFormat.Png); // Änderung des Speicherorts
        }

        public void LoadImage()
        {
            bitmap = new Bitmap(@"D:\Tempotary\OutPut.png"); // Änderung des Speicherorts
            imageSource = ConvertBitmapToImageSource(bitmap);
        }

        public ImageSource GetLoadedImageSource()
        {
            return imageSource;
        }

        private BitmapImage ConvertBitmapToImageSource(Bitmap bitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
