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
            string name = "bellpeper.bmp";
            string total = path+name;
            MyImage image = new MyImage(File.ReadAllBytes(total));
            MyImage kernel = new MyImage(3, 3);
            kernel.data = new pixel[3, 3];
            kernel.data[0, 0] = new pixel(0);
            kernel.data[0, 1] = new pixel(1);
            kernel.data[0, 2] = new pixel(0);
            kernel.data[1, 0] = new pixel(1);
            kernel.data[1, 1] = new pixel(-4);
            kernel.data[1, 2] = new pixel(1);
            kernel.data[2, 0] = new pixel(0);
            kernel.data[2, 1] = new pixel(1);
            kernel.data[2, 2] = new pixel(0);
            image.Convo(kernel).From_Image_To_File($"{path}convo.bmp");
            image.hsvShift(new pixel(0,1,0)).From_Image_To_File(path+"test.bmp");
        }
    }
}
