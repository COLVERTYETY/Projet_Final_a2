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
            double angleRadians = 0;
            int angleDegres = 0;
            try
            {
                angleDegres = Convert.ToInt32(Angle.Text);
                angleDegres = angleDegres % 360;
                Warning.Height = 0;
                Warning.Margin = new System.Windows.Thickness(0, 0, 0, 0);
            }
            catch (Exception)
            {
                Warning.Height = Double.NaN;
                Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
            }
            angleRadians = angleDegres * Math.PI / 180;
            Console.WriteLine($"{angleDegres} | {angleRadians}");
        }
    }
}
