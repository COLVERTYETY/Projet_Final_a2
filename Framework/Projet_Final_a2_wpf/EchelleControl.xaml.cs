using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.IO;
using complet;

namespace Projet_Final_a2_wpf
{
    /// <summary>
    /// Logique d'interaction pour EchelleControl.xaml
    /// </summary>
    public partial class EchelleControl : UserControl
    {
        public string MyHeight { get; set; }
        public string MyWidth { get; set; }

        public EchelleControl()
        {
            InitializeComponent();
        }

        private void Validate(object sender, RoutedEventArgs e)
        {
            int[] newSize = new int[2] { -1, -1 };
            try
            {
                newSize = new int[2] { Convert.ToInt32(Height.Text), Convert.ToInt32(Width.Text) };
                if (newSize[0] > 0 && newSize[1] > 0)
                {
                    //tout va bien
                    Warning.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                    Warning.Height = 0;
                    MyImage tempImage = MainWindow.ImageToMyImage;
                    tempImage = tempImage.rescale(newSize[1], newSize[0]);
                    MainWindow.ImageToMyImage = tempImage;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                {
                    //c'est pas des entiers positifs
                    Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
                    Warning.Height = Double.NaN;
                }
            }
            catch(Exception)
            {
                //c'est pas des entiers
                Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
                Warning.Height = Double.NaN;
            }
        }

        private void Preview(object sender, RoutedEventArgs e)
        {
            int[] newSize = new int[2] { -1, -1 };
            try
            {
                newSize = new int[2] { Convert.ToInt32(Height.Text), Convert.ToInt32(Width.Text) };
                if (newSize[0] > 0 && newSize[1] > 0)
                {
                    //tout va bien
                    Warning.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                    Warning.Height = 0;
                    MyImage tempImage = MainWindow.ImageToMyImage;
                    if (tempImage.width > 200 || tempImage.height > 200)
                    {
                        if (tempImage.height >= tempImage.width) { tempImage = tempImage.rescale((int)((double)((double)tempImage.width / (double)tempImage.height) * 200), 200); }
                        else { tempImage = tempImage.rescale(200, (int)(double)(((double)tempImage.height / (double)tempImage.width) * 200)); }
                    }
                    tempImage = tempImage.rescale(newSize[1], newSize[0]);
                    tempImage.From_Image_To_File("preview.bmp");
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/preview.bmp");
                    bitmap.EndInit();
                    previewRescale.Source = bitmap;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                {
                    //c'est pas des entiers positifs
                    Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
                    Warning.Height = Double.NaN;
                }
            }
            catch (Exception)
            {
                //c'est pas des entiers
                Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
                Warning.Height = Double.NaN;
            }
        }
    }
}
