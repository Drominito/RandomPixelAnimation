using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpelPixelImage
{
    //public class OtherFunctions
    //{
    //    public int WindowSizeValue;
    //    public object SliderFront;
    //    public Bitmap bitmap;
    //    public MainWindow window;

    //    public OtherFunctions()
    //    {
           
    //    }
    //    public static void MathFunction01(int x, int y)
    //    {
    //        int centerX = WindowSizeValue / 2;
    //        int centerY = WindowSizeValue / 2;
    //        int radius = WindowSizeValue / 4;

    //        double formula = Math.Cos(x * x) / Math.Sin(y * y) * SliderFront.Value * 25;

    //        Color Green = Color.FromArgb(0, 255, 0);
    //        Color Black = Color.FromArgb(0, 0, 0);

    //        if (x * x / formula + Controller <= radius) // Prüfe, ob der Unterschied im Bereich des Radius liegt
    //        {
    //            bitmap.SetPixel(x / (int)SliderFront.Value, y, Green);
    //        }
    //        else
    //        {
    //            bitmap.SetPixel(x, y, Black);
    //        }
    //    }


    //    private void ColorfulLines01(int x, int y)
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            if (x / y > i && i >= 0)
    //            {
    //                bitmap.SetPixel(x, y, Color.FromArgb((i * 20), 255 - i * 20, i * 20));
    //            }
    //        }
    //    }
    //    private void HorizontalNoiseMethod(int x, int y)
    //    {
    //        int
    //        centerX = WindowSizeValue / 2,
    //        centerY = WindowSizeValue / 2,
    //        radius = WindowSizeValue / 4;
    //        double MiddlePoint = Math.Pow((x - centerX * 0.75), 3) + Math.Pow((y - centerY * Math.Sin(y * x * y)), 2);

    //        if (MiddlePoint <= radius)
    //        {
    //            bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 0));
    //        }
    //    }

    //    private void MyPet(int x, int y)
    //    {
    //        int
    //        centerX = WindowSizeValue / 2,
    //        centerY = WindowSizeValue / 2,
    //        radius = WindowSizeValue / 4,
    //        MiddlePoint = (int)Math.Pow((x - centerX), 2) + (int)Math.Pow(y - centerX, 2);       // The AI named that "DistanceSquared"

    //        if (MiddlePoint <= radius * radius)
    //        {
    //            bitmap.SetPixel(x / y + (int)Math.Sqrt(x * y), y, Color.FromArgb(0, 255, 0));
    //        }
    //    }

    //    private void AIMethod(int x, int y)
    //    {
    //        int
    //        centerX = WindowSizeValue / 2,
    //        centerY = WindowSizeValue / 2,
    //        radius = WindowSizeValue / 4; // Setzen Sie den Radius des Kreises auf ein Viertel der Fenstergröße


    //        int distanceSquared = (x - centerX) * (x - centerX) + (y - centerY) * (y - centerY);

    //        if (distanceSquared < radius * radius)
    //        {
    //            // Der Punkt (x, y) liegt innerhalb des Kreises, setzen Sie die Farbe
    //            int intensity = 255 * (radius - (int)Math.Sqrt(distanceSquared)) / radius;
    //            bitmap.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
    //        }
    //    }
    //    public void Method1(int x, int y, int RGBColor)
    //    {
    //        RGBColor = x;

    //        if (x > 5 && y >= 5)
    //        {
    //            if (x * x * x * x / y * y % 2 == 0)
    //            {
    //                bitmap.SetPixel(x, y, Color.FromArgb(y / 5, x / 100, 255));
    //            }
    //        }

    //    }
    //    private void ColorfulLines02(int x, int y, int RColor, int GColor, int BColor)
    //    {
    //        if (x / y == 0)
    //        {
    //            bitmap.SetPixel(x, y / 1, Color.FromArgb(255, GColor / 8, BColor / 4));
    //            bitmap.SetPixel(x, y / 2, Color.FromArgb(255, 127, 0));
    //            bitmap.SetPixel(x, y / 3, Color.FromArgb(0, 127, 63));
    //            bitmap.SetPixel(x, y / 4, Color.FromArgb(0, 255, 127));
    //        }
    //    }
    //}
}
