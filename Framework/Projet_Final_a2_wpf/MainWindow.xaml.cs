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
using System.Windows.Forms;
using System.ComponentModel;


namespace Projet_Final_a2_wpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        OpenFileDialog dlg;
        public MainWindow()
        {
            InitializeComponent();
            dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.bmp)|*.bmp";
        }

        private void OpenImage(object sender, RoutedEventArgs e)
        {
            string filename = null;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;

            }
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filename);
            bitmap.EndInit();
            ImageViewer.Source = bitmap;
        }

        private void Resize(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("oui");
        }

        private void CallMirror(object sender, RoutedEventArgs e)
        {
            theUserControl.Content = new MiroirControl();
        }

        private void CallResize(object sender, RoutedEventArgs e)
        {
            theUserControl.Content = new EchelleControl();
        }

        private void CallRotate(object sender, RoutedEventArgs e)
        {
            theUserControl.Content = new RotationControl();
        }
    }
}


