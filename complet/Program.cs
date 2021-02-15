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
            image.hsvShift(new pixel(0,0.1,0)).From_Image_To_File(path+"test.bmp");
        }
    }
}
