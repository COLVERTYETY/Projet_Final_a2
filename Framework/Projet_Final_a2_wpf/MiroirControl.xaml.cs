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
    }
}
