﻿using System;
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
            MyImage kernel = new MyImage(5, 5);
            #region kernel
            kernel.data = new pixel[5, 5];
            kernel.data[0, 0] = new pixel(0);
            kernel.data[0, 1] = new pixel(0);
            kernel.data[0, 2] = new pixel(0);
            kernel.data[0, 3] = new pixel(0);
            kernel.data[0, 4] = new pixel(0);

            kernel.data[1, 0] = new pixel(0);
            kernel.data[1, 1] = new pixel(1);
            kernel.data[1, 2] = new pixel(1);
            kernel.data[1, 3] = new pixel(1);
            kernel.data[1, 4] = new pixel(0);

            kernel.data[2, 0] = new pixel(0);
            kernel.data[2, 1] = new pixel(1);
            kernel.data[2, 2] = new pixel(1);
            kernel.data[2, 3] = new pixel(1);
            kernel.data[2, 4] = new pixel(0);

            kernel.data[3, 0] = new pixel(0);
            kernel.data[3, 1] = new pixel(1);
            kernel.data[3, 2] = new pixel(1);
            kernel.data[3, 3] = new pixel(1);
            kernel.data[3, 4] = new pixel(0);

            kernel.data[4, 0] = new pixel(0);
            kernel.data[4, 1] = new pixel(0);
            kernel.data[4, 2] = new pixel(0);
            kernel.data[4, 3] = new pixel(0);
            kernel.data[4, 4] = new pixel(0);
            #endregion
            image.Convo(kernel).From_Image_To_File($"{path}convo.bmp");
        }
    }
}
