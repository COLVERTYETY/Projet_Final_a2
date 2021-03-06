﻿using System;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using complet;

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
            test = ImageViewer;
        }

        static private Image test;
        static private MyImage imageToMyImage;

        static public MyImage ImageToMyImage{ get { return imageToMyImage; } set {
                value.From_Image_To_File("result.bmp");
                imageToMyImage = value;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/result.bmp");
                bitmap.EndInit();
                test.Source = bitmap;
                Console.WriteLine("succesfuly saved an image");
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void OpenImage(object sender, RoutedEventArgs e)
        {
            string filename = null;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = dlg.FileName;
            }
            try
            { ImageToMyImage = new MyImage(filename); }
            catch(Exception) { }
        }

        private void Save(object sender, RoutedEventArgs e)
        {

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