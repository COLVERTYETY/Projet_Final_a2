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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using complet;

namespace Projet_Final_a2_wpf
{
    /// <summary>
    /// Logique d'interaction pour MiroirControl.xaml
    /// </summary>
    public partial class MiroirControl : UserControl
    {
        public MiroirControl()
        {
            InitializeComponent();
        }

        private void Horizontal_Click(object sender, RoutedEventArgs e)
        {
            VerticalCheck.IsChecked = !HorizontalCheck.IsChecked;
        }

        private void Vertical_Click(object sender, RoutedEventArgs e)
        {
            HorizontalCheck.IsChecked = !VerticalCheck.IsChecked;
        }

        private void Validate(object sender, RoutedEventArgs e)
        {
            if (HorizontalCheck.IsChecked != false || VerticalCheck.IsChecked != false)
            {
                bool vertical = VerticalCheck.IsChecked ?? false;
                MyImage tempImage = MainWindow.ImageToMyImage;
                tempImage = tempImage.mirror(vertical);
                MainWindow.ImageToMyImage = tempImage;
            }
        }

        private void Preview(object sender, RoutedEventArgs e)
        {
            if (HorizontalCheck.IsChecked != false || VerticalCheck.IsChecked != false)
            {
                bool vertical = VerticalCheck.IsChecked ?? false;
                MyImage tempImage = MainWindow.ImageToMyImage;
                if (tempImage.width > 200 || tempImage.height > 200)
                {
                    if (tempImage.height >= tempImage.width) { tempImage = tempImage.rescale((int)((double)((double)tempImage.width / (double)tempImage.height) * 200), 200); }
                    else { tempImage = tempImage.rescale(200, (int)(double)(((double)tempImage.height / (double)tempImage.width) * 200)); }
                }
                tempImage = tempImage.mirror(vertical);
                tempImage.From_Image_To_File("preview.bmp");
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/preview.bmp");
                bitmap.EndInit();
                previewMirror.Source = bitmap;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
