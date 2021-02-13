using System;
using System.IO;
using Xunit;
using complet;

namespace tests
{
    
    public class UnitTest1
    {
        static bool IsLinux()
        {
            int p = (int) Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128);
        }
        [Fact]
        public void Test1()
        {
            string path="./images/";
            if(IsLinux()){
                path = "../../../../images/";
                Console.WriteLine("detected LINUX as the os");
            }
            string name = "mediumtri.bmp";
            string total = path+name;
            MyImage image = new MyImage(File.ReadAllBytes(total));
            image.dispwithcolor();
            Assert.True(true);
        }
    }
}
