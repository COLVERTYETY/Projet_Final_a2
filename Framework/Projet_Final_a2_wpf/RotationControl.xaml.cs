using complet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet_Final_a2_wpf
{
    /// <summary>
    /// Logique d'interaction pour RotationControl.xaml
    /// </summary>
    public partial class RotationControl : UserControl
    {
        public RotationControl()
        {
            InitializeComponent();
        }

        private void Validate(object sender, RoutedEventArgs e)
        {
            double angleDegres = 0;
            try
            {
                MyImage tempImage = MainWindow.ImageToMyImage;
                angleDegres = Convert.ToDouble(Angle.Text);
                angleDegres = angleDegres % 360;
                Warning.Height = 0;
                Warning.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                tempImage = tempImage.rotate(angleDegres);
                MainWindow.ImageToMyImage = tempImage;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception) { }
        }

        private void Preview(object sender, RoutedEventArgs e)
        {
            try
            {
                double angleDegres = 0;
                MyImage tempImage = MainWindow.ImageToMyImage;
                if (tempImage.width > 200 || tempImage.height > 200)
                {
                    if (tempImage.height >= tempImage.width) { tempImage = tempImage.rescale((int)((double)((double)tempImage.width / (double)tempImage.height) * 200), 200); }
                    else { tempImage = tempImage.rescale(200, (int)(double)(((double)tempImage.height / (double)tempImage.width) * 200)); }
                }
                angleDegres = Convert.ToDouble(Angle.Text);
                angleDegres = angleDegres % 360;
                Warning.Height = 0;
                Warning.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                tempImage = tempImage.rotate(angleDegres);
                tempImage.From_Image_To_File("preview.bmp");
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/preview.bmp");
                bitmap.EndInit();
                previewRotation.Source = bitmap;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception)
            {
                Warning.Height = Double.NaN;
                Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
                Console.WriteLine("didn't work");
            }
        }
    }
}
