using System;
using System.Diagnostics;
using System.IO;
using System.Threading;



namespace complet
{
    class Program{
        static bool IsLinux()
        {
            int p = (int) Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128);
        }
        static void Main(string[] args)
        {
            string path="./images/";
            if(IsLinux()){
                path = "../images/";
                Console.WriteLine("detected LINUX as the os");
            }
            string name = "1.bmp";
            string total = path+name;
            MyImage image = new MyImage(total);
            (new FileInfo("outputs")).Directory.Create();
            image.greyscale().From_Image_To_File($"{path}outputs/gris.bmp");
            image.noiretblanc().From_Image_To_File($"{path}outputs/noiretblanc.bmp");
            image.rotate(30).From_Image_To_File($"{path}outputs/rotation.bmp");
            image.rescale(1000,1000).From_Image_To_File($"{path}outputs/resize.bmp");
            //image.Miror().From_Image_To_File($"{path}output/resize.bmp");
            //image.rescale(5000,5000).Mandelbrot(0.22,0.10,0.32,-0.10).From_Image_To_File($"{path}test.bmp");
        }
    }
}
