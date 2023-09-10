//wenn bild über 99 ist dann soll er weitere bilder rendern, aber die function soll geändert werden

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;
using ImageRenderingApp;
using System.Diagnostics.Eventing.Reader;

namespace SimpelPixelImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Pfad = @"D:\Tempotary\Pixeln\90DegreesColorPallete";
        public string Name = @"PixelBild";
        public string DateiTyp = ".png";
        public int WindowPixelValue = 500;
        List<Bitmap> VideoBitmaps = new List<Bitmap>();
        Bitmap Bitmaps;
        public int FramesWanted = 500;
        private double Controller = 1;
        private double Controller2= 1;
        string[] DoneImageFileArray;
        private int MiniCounter;
        public Slider SliderFrontFeld;

        Color[,] DefaultPixelArray;
        //string InputImageFile = @"C:\Users\Dromi\OneDrive\Bilder\Main-Bilder\Urlaub\Türkei\20220804_120522.jpg";
        Bitmap SourceBitmapForImage;
        



        public int Index = 0;
        public bool AutoUpdate = false;

        public MainWindow()
        {
            InitializeComponent();

            SliderFrontFeld = SliderFront;
            SliderLabel.Content = SliderFront.Value;
            SliderFront.Maximum = FramesWanted;

            int Width = WindowPixelValue / 2, Height = WindowPixelValue / 2;

            Bitmap SourceBitmapForImage = new Bitmap(Width, Height);
            SavePixelsOnArray(SourceBitmapForImage);

            //SomeMethod();
            if (DoneImageFileArray == null)
            {
                GenerateImagePaths(FramesWanted);
            }

            CreatePngFiles();




        }
       

        async private void SomeMethod()
        {
            //await StartAsyncMethod();
        }

        async private Task StartAsyncMethod()
        {
            GenerateSound GS = new GenerateSound();

        }

        public void PrepareToStart()
        {
            GenerateImagePaths(FramesWanted);
            CreateBitmaps(FramesWanted);
        }
        

        public void StartingGenerateSingleFrame()
        {
            CreateBitmaps(1)     ;
            //GenerateImagePaths(ImagesWanted);

            GeneratePixels();
        }


        async private Task StartingGenerateAll()
        {
            await StartingGenerateAllAsync();
        }

        async public Task StartingGenerateAllAsync()
        {
            //GenerateImagePaths(ImagesWanted);

            CreateBitmaps(FramesWanted); // The Parameter is The Limit

            for (int i = 0; i < FramesWanted; i++)
            {
                //bool HavingCurrentImage = CurrentHavingImage();
                //if (HavingCurrentImage == true)
                //{
                //}
                GeneratePixels();
                SliderFront.Value++;
                Controller += 0.075;
                Index++;
            }
        }

        private bool CurrentHavingImage()
        {
           
            for (int i = 0; i < VideoBitmaps.Count; i++)
            {
                if (VideoBitmaps[i] == null); { return false; }
            }
            return true;
        }

        public void CreatePngFiles()
        {
            bool IsFilesExisting = false;

            //if (DoneImageFileArray == null)       // Ist schon oben definiert
            //{
            //    GenerateImagePaths(ImagesWanted);
            //}
            IsFilesExisting = CheckingIfFilesExists(DoneImageFileArray);


            if (IsFilesExisting)
            {
                // wähle ob du es überschrieben möchtest
                // stück für stück rendern
            }
            else
            {
                for (int i = 0; i < FramesWanted; i++)
                {
                    File.Create(DoneImageFileArray[i]);
                }
            }
        }

        private void GenerateImagePaths(int Limit)
        {
            DoneImageFileArray = new string[Limit];
            for (int i = 0; i < Limit; i++)
            {
                DoneImageFileArray[i] = Pfad + Name + $"0{i}" + DateiTyp;
            }
        }

        private bool CheckingIfFilesExists(string[] imageFileArray)
        {
            int i = 0;
            // Extra noch so überprüfen weil es könnte sein das manche Dateien da sind und manche sind, da unten, um genauer zu sein
            while (i < imageFileArray.Length)
            {
                // Grob gesagt ob die Dateien Existieren damit es schneller geht => Wenn Datei[0] existiert dann returne "true" wenn nicht dann durchläufe alle dateien vom den Array
                for (i = 0; i < imageFileArray.Length; i++) // Mit jeden einzelnen überprüfen ob sie existieren, wenn nicht dann break.
                {
                    if (File.Exists(imageFileArray[i]) == true && i >= imageFileArray.Length - 1)    
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void CreateBitmaps(int Limit)
        {
            for (int i = 0; i < Limit; i++)
            {
                Bitmaps = new Bitmap(WindowPixelValue, WindowPixelValue, PixelFormat.Format24bppRgb);
                VideoBitmaps.Add(Bitmaps);
            }
        }

       
        public void GeneratePixels()
        {
            using (VideoBitmaps[Index])
            {
                for (int y = 0; y < WindowPixelValue / 2; y++)
                {
                    for (int x = 0; x < WindowPixelValue / 2; x++)
                    {
                        if (x > 0 && y > 0)
                        {
                            VoidSpace_Logic(x, y);
                        }
                    }
                }



                VideoBitmaps[Index].Save(DoneImageFileArray[Index]);

                MiniCounter++;

                BitmapImage bitmapimage = new BitmapImage(new Uri(DoneImageFileArray[Index]));
                PixelImageFront.Source = bitmapimage;
            }
        }


        private void VoidSpace_Logic(int x, int y)
        {
            double centerX = WindowPixelValue / 2;   // Controller => x, if x geht higher you'll move to left side.
            double centerY = WindowPixelValue / 2;
            double radius = WindowPixelValue / 4;

            double distanceSquared = Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2);
            double scaledDistance = distanceSquared / (Math.Pow(radius, 2));
            double Iteration01 = Math.Sin(x * x);

            double Iteration02 = 1;

            double angle = Math.Atan2(y - centerY, x - centerX);

            double value = Math.Abs(Math.Cos(scaledDistance * Iteration01)) * Math.Abs(Math.Sin(angle)) * 255;

            int red = new Random().Next(255);
            int green = (int)(Math.Abs(Math.Sin(angle)) * (int)(Math.Abs(Math.Cos(angle)) * 255 * 2));
            int blue = new Random().Next(255); 

            if ((int)(value - Controller) <= 255 || (int)(value - Controller) >= 0)
            {
                red = (int)(value - Controller);
            }

            if ((int)(Math.Abs(Math.Cos(angle)) * 255) <= 255)
            {
                blue = (int)(Math.Abs(Math.Cos(angle)) * 255);
            }

            Color pixelColor = Color.FromArgb(255, 0, 0);

            if (distanceSquared < Math.Sin(radius) * 100)
            {
                VideoBitmaps[Index].SetPixel(x, y, Color.FromArgb(red, green, Math.Min(155, blue)));
            }
            //if (distanceSquared <= Math.Cos(Math.Pow(y, Iteration02)) || scaledDistance %Controller <= Math.Cos(y + Math.Pow(x, Iteration02) ))
            //{
            //    VideoBitmaps[Index].SetPixel(x, y, Color.FromArgb(0, 255, 0));

            //}
            //else
            //{
            //    //VideoBitmaps[Index].SetPixel(x, y, Color.FromArgb(255 - red, (red) / x, 255 - 255)); // Color Inverted by (Max - Previous Value)
            //    VideoBitmaps[Index].SetPixel(x, y, Color.FromArgb(0, 0, 0));

            //}
        }

        private void RotatingCircle(int x, int y)
        {
            double centerX = WindowPixelValue / 2;
            double centerY = WindowPixelValue / 2;
            double radius = WindowPixelValue / 8;
            double SlowerSpeed = 2;

            
            // Berechne die Entfernung des aktuellen Pixels zum Mittelpunkt
            double distanceToCenter = Math.Sqrt(Math.Pow(x - centerX + Math.Sin((Controller/ SlowerSpeed) ), 2) + Math.Pow(y - centerY + Math.Cos(Controller/ SlowerSpeed), 2));

            // Berechne den relativen Abstand vom Mittelpunkt (im Bereich von 0 bis 1)
            double distanceRatio = distanceToCenter / (Math.Sin(Controller/10) * (Controller));

            // Stelle sicher, dass der relative Abstand im Bereich von 0 bis 1 liegt
            //distanceRatio = Math.Min(1, distanceRatio);

            // Berechne die Helligkeit basierend auf dem Abstand (hier abgeschwächt, um Dunkelheit zu erzeugen)
            int brightness = (int)(255 * (1 - distanceRatio));

            //Color pixelColor = Color.FromArgb(brightness, brightness, brightness);

            //VideoBitmaps[Index].SetPixel(x, y, pixelColor);

            int DefaultARGB = DefaultPixelArray[x, y].ToArgb();
            if (distanceRatio / 10000 <= Math.Cos(x) && distanceRatio <= Math.Sin(y))
            {
                VideoBitmaps[Index].SetPixel(x, y, Color.FromArgb(DefaultARGB));
            }
            if (0.5 <= Math.Cos(x + Controller) || 0.5 <= Math.Sin(y + Controller))
            {
                //VideoBitmaps[Index].SetPixel(x, y, Color.FromArgb(0, 255, 0));

                //VideoBitmaps[Index].SetPixel(x /2, y / 2, Color.FromArgb(DefaultARGB/10));
            }
            else
            {

            }
            if (DefaultARGB > 0)
            {
            }
        }
        public void TransformingCircle(int x, int y)
        {
            double centerX = WindowPixelValue / 2;
            double centerY = WindowPixelValue / 2;
            double radius = WindowPixelValue / 4;

            double distanceToCenter = Math.Sqrt(Math.Pow(x - centerX /*/ (Math.Sin(Controller / 80))*/, 2) + Math.Pow(y - centerY /*/ (Math.Sin(Controller / 80))*/, 2));


            double MyCalc = Math.Pow(Math.Cos(radius - Controller / 50), 2) + Math.Pow(Math.Tan(radius - Controller / 10), 2);
            double SqrtMyCalc = Math.Sqrt(MyCalc);

            if (distanceToCenter <= radius / SqrtMyCalc)
            {
                VideoBitmaps[Index].SetPixel(x, y, Color.FromArgb(0, 255, 0));
            }
        }

        private void SavePixelsOnArray(Bitmap SourceBitmapForImage)
        {
            DefaultPixelArray = new Color[SourceBitmapForImage.Width, SourceBitmapForImage.Height];

            for (int y = 0; y < SourceBitmapForImage.Height; y++)
            {
                for (int x = 0; x < SourceBitmapForImage.Width; x++)
                {

                    DefaultPixelArray[x, y] = SourceBitmapForImage.GetPixel(x, y);
                }
            }
        }








        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            bool TempSwitch = false;
            switch (e.Key)
            {
                case Key.Left:
                {
                        TempSwitch = true;
                        if (TempSwitch)
                        {
                            AutoUpdate = false;
                        }
                        SliderFront.Value--;
                    Controller = SliderFront.Value;
                    Index-=1;
                        AutoUpdate = true;
                } break;


                case Key.Right:
                {

                        //bool HavingCurrentImage = CurrentHavingImage();
                        SliderFront.Value++;
                        Controller++;
                        Index++;


                }
                break;


                case Key.Up:
                {
                        Controller2++;
                } break;


                case Key.Down:
                {
                    Controller2--;
                }
                break;

                case Key.G:
                {
                        PrepareStartingGenerateAllWithAsync();

                } break;

                case Key.D:
                {

                   for (int i = 0; i < DoneImageFileArray.Length; i++)
                   {
                       File.Delete(DoneImageFileArray[i]);
                   }
                }
                break;
                case Key.Escape: { File.Delete(DoneImageFileArray[Index]); File.Create(DoneImageFileArray[Index]); StartingGenerateSingleFrame(); } break;      //BUGGED :(


                case Key.U:
                {
                    if (AutoUpdate == true)
                    {
                        AutoUpdate = false;
                    }
                    else
                    {
                        AutoUpdate = true;
                    }
                } 
                break;

                case Key.Space:
                {
                    UpdateImage();
                }
                break;


                case Key.Enter:
                {
                        //Sliderlabel.content = sliderfront.value;
                        StartingGenerateSingleFrame();
                        //SliderFront.Value = 0;
                }
                break;

                case Key.F1:
                {
                        PrepareToStart();
                }
                    break;
            }
        }

        async public Task PrepareStartingGenerateAllWithAsync()
        {
            await StartingGenerateAll();
        }

        private void SliderFront_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderLabel.Content = SliderFront.Value;
            SliderLabel2.Content = SliderFront.Value;


            if (SliderFront.Value < FramesWanted && AutoUpdate)
            {
                UpdateImage();
            }
        }

        private void UpdateImage()
        {
            BitmapImage bitmapImage = null;
            if (DoneImageFileArray != null)
            {
                try
                {
                    bitmapImage = new BitmapImage(new Uri(DoneImageFileArray[(int)SliderFront.Value]));

                    //GeneratePixels();

                }
                catch
                {
                    GeneratePixels();
                }
            }
            LoadingPlaceImage.Source = bitmapImage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            VideoBitmaps = null;
            BitmapImage bitmapimage = null;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = Width;
            Width = Height;
        }
    } 
}
