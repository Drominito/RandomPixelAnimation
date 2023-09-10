//Loading scenn
//Gradient Screen Manual (10x10 pixel)
//Asyncron
// * was ein Thread ist
// * wie man auf Threads zugreift
// * Liste vom Threads und welche benutzt werden, wofür, und welche nicht

using System;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace RandomPixelAnimation
{
    public partial class MainWindow : Window
    {
        public int Width;
        public int Height;
        public int Counter = 0;
        public int Images = 1000;
        public string[] ImageFilePathArray;
        public Bitmap ImageBit;

        public MainWindow()
        {
            InitializeComponent();

            ImageFilePathArray = new string[Images];

            for (int i = 0; i < Images; i++)
            {
                ImageFilePathArray[i] = @"D:\Tempotary\Pixeln\NextPixels\Pixels{i}.png";
            }
        }

        private void UpdateImageSource(BitmapImage bitmapImage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Image_Front.Source = bitmapImage;
            });
        }

        public async Task CreateBitmap(int Index)
        {
            Width = 1000;
            Height = 1000;

            using (ImageBit = new Bitmap(Width, Height, PixelFormat.Format32bppArgb))
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        int r = new Random().Next(255);
                        int g = new Random().Next(255);
                        int b = new Random().Next(255);

                        Color CurentColor = Color.FromArgb(255, r, g, b);
                        ImageBit.SetPixel(x, y, CurentColor);
                    }
                }

                // Das Bitmap in Image_Front anzeigen
                BitmapImage bitmapImage = new BitmapImage(new Uri(ImageFilePathArray[Index]));
                //UpdateImageSource(bitmapImage);
                Image_Front.Source = bitmapImage;


                ImageBit.Save(ImageFilePathArray[Index]);
            }
        }

        public void ClearBitmap()
        {
            Width = 2;
            Height = 2;

            if (ImageBit != null)
            {
                ImageBit.Dispose(); // Vorheriges Bitmap freigeben
            }

            ImageBit = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            // Blaue Pixel setzen, um das Bild zu löschen
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    ImageBit.SetPixel(x, y, Color.Blue);
                }
            }

            // Das Bitmap in Image_Front anzeigen
            BitmapImage bitmapImage = new BitmapImage(new Uri(ImageFilePathArray[Counter]));
            UpdateImageSource(bitmapImage);
        }

        private /*async*/ void Button_Click(object sender, RoutedEventArgs e)
        {
            //await Task.Run(async () =>
            //{
                for (int i = 0; i < Images; i++)
                {
                   /*await*/ CreateBitmap(i);
                }
            //});
        }

        private void ClearButton(object sender, RoutedEventArgs e)
        {
            //ClearBitmap();
            //Counter++;
            ShowImages();
        }

        private void ShowImages()
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(ImageFilePathArray[Counter]));

            Image_Front.Source = bitmapImage;

            Counter++;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //ClearButton(sender, e);
            ShowImages();
        }
    }
}
