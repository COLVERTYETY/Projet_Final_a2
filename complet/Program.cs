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
            // string name = "rainbowrect.bmp";
            // string total = path+name;
            // MyImage image = new MyImage(File.ReadAllBytes(total));
            // pixel shifter = new pixel(1,0,0);
            // int width = (Console.WindowWidth/4);
            // int height = Console.WindowHeight-1;
            // while (true){
            //     image = image.hsvShift(shifter);
            //     Console.SetCursorPosition(0,0);
            //     image.rescale(width,height).dispwithcolor();
            // }
            string name = "1.bmp";
            string total = path+name;
            MyImage image = new MyImage(total);
            int width = (Console.WindowWidth/4);
            int height = Console.WindowHeight-1;
            //image.fromclosest(image.Kmeans(4,100)).From_Image_To_File(path+"test.bmp");
            double[,] content = new double[,]{
                {1,1,1},
                {1,1,1},
                {1,1,1}
                }; 
            MyImage kernel = new MyImage(content);
            kernel.flattenkernel();
            Console.WriteLine("about to start.....");
            //kernel.From_Image_To_File($"{path}convo.bmp");
            threadMachine a = new threadMachine(image.rescale(10000,10000));
            //a.Nthreads = 1;
            a.optimiseThreadCount();
            Stopwatch chrono = new Stopwatch();
            chrono.Start();
            a.convo(kernel).From_Image_To_File($"{path}convo.bmp");
            //new MyImage(File.ReadAllBytes(path+"sharp.bmp")).rotate(200).From_Image_To_File($"{path}convo.bmp");
            chrono.Stop();
            Console.WriteLine(chrono.Elapsed);
        }
    }
}
