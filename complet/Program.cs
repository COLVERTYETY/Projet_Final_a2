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
            string path="../images/";
            if(IsLinux()){
                path = "../images/";
                Console.WriteLine("detected LINUX as the os");
            }
            string name = "sharp.bmp";
            string total = path+name;
            MyImage image = new MyImage(total);
            // int[] test = new int[]{9,14,26,69};
            // test=qrcode.encodeReedSalomon(test);
            // Console.WriteLine(String.Join(' ',test));
            // test[1] = 18;
            // test = qrcode.decodeReedSalomon(test);
            // Console.WriteLine(String.Join(' ',test));
            double[,] k = new double[,]{
                {-1,-1,-1},
                {-1,8,-1},
                {-1,-1,-1}
            };
            MyImage kernel = new MyImage(k);
            image.convo(kernel).polarise(0).From_Image_To_File($"{path}test.bmp");
            //image.rescale(5000,5000).Mandelbrot(0.22,0.10,0.32,-0.10).From_Image_To_File($"{path}test.bmp");
        }
    }
}
