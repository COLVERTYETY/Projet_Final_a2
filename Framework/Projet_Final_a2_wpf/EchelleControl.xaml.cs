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
                    Warning.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                    Warning.Height = 0;
                    //Console.WriteLine($"tout va bien | {newSize[0]} {newSize[1]}");
                }
                else
                {
                    Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
                    Warning.Height = Double.NaN;
                    //Console.WriteLine($"c'est pas des entiers positifs | {newSize[0]} {newSize[1]}");
                }
            }
            catch(Exception)
            {
                Warning.Margin = new System.Windows.Thickness(2.5, 2.5, 2.5, 2.5);
                Warning.Height = Double.NaN;
                //Console.WriteLine($"c'est pas des entiers | {newSize[0]} {newSize[1]}");
            }
        }
    }
}
