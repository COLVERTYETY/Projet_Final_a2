using complet;
using System;
using System.Collections.Generic;
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
            int angleDegres = 0;
            try
            {
                angleDegres = Convert.ToInt32(Angle.Text);
                angleDegres = angleDegres % 360;
                Warning.Height = 0;
                Warning.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                Console.WriteLine("got angle");
            }
            catch (Exception)
            {
                Warning.Height = Double.NaN;
                Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
                Console.WriteLine("didn't get angle");
            }
            try
            {
                MyImage tempImage = new MyImage("result.bmp");
                tempImage.rotate(angleDegres).From_Image_To_File("temp.bmp");
                /*BitmapImage temp = new BitmapImage();
                temp.BeginInit();
                temp.UriSource = new Uri("temp.bmp", UriKind.Relative);
                temp.EndInit();*/
                previewRotation.Source = new BitmapImage(new Uri("pack://application:,,,/Projet_Final_a2_wpf;component/temp.bmp"));
                Console.WriteLine("created preview");
            }
            catch (Exception)
            {
                Console.WriteLine("didn't create preview");
            }
        }
    }
}
