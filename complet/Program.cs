using System;
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
            string name = "xp.bmp";
            string total = path+name;
            MyImage image = new MyImage(File.ReadAllBytes(total));
            int width = (Console.WindowWidth/4);
            int height = Console.WindowHeight-1;
            image.fromclosest(image.Kmeans(4,100)).From_Image_To_File(path+"test.bmp");
        }
    }
}
