using System;    
using System.IO;
using Xunit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using complet;

namespace tests
{
    
    public class UnitTest1
    {
        static string path="./images/inputs/";
        static MyImage[] loadedimages;
        static string[] tobetested;
        static UnitTest1(){ //this constrictor is run at the beginning and is used to define environnement related variables
            if(IsLinux()){
                path = "../../../../images/inputs/";
                Console.WriteLine("detected LINUX as the os");
            }
            tobetested = alltheimages(path);
            List<MyImage> temp = new List<MyImage>();
            Console.WriteLine("");
            loading loader = new loading(Console.CursorTop);
            loader.header = "loading all images to memory |";
            loader.fit();
            for(double i=0;i<tobetested.Length;i++){
                temp.Add(new MyImage(File.ReadAllBytes(tobetested[(int)i])) );
                loader.half = tobetested[(int)i].Substring(tobetested[(int)i].LastIndexOf('/')).PadRight(10);
                loader.fit();
                loader.step((i+1)/((double)tobetested.Length));
            }
            loadedimages = temp.ToArray();
        }
        static string[] alltheimages(string _path){
            List<string> res = new List<string>();
            foreach (string file in Directory.EnumerateFiles(_path, "*.bmp"))
            {
                Console.WriteLine(file);
                res.Add(file);
            }
            return res.ToArray();
        }
        static bool IsLinux()
        {
            int p = (int) Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128);
        }

        [Fact]
        public void loadTest(){
            Assert.True(loadedimages.Length>0);
        }
        [Theory]
        [InlineData(5,5)]
        [InlineData(25,25)]
        [InlineData(100,100)]
        [InlineData(1000,1000)]
        [InlineData(5,25)]
        [InlineData(25,5)]
        [InlineData(25,500)]
        [InlineData(2500,500)]
        public void rescaleTest(int x, int y)
        {
            if(loadedimages.Length>0){
                MyImage image;
                Console.WriteLine("");
                loading loader = new loading(Console.CursorTop);
                loader.header = Convert.ToString(x)+" "+Convert.ToString(y)+" |";
                loader.fit();
                for(double i=0;i<loadedimages.Length;i++){
                    image = loadedimages[(int)i];
                    loader.half = tobetested[(int)i].Substring(tobetested[(int)i].LastIndexOf('/')).PadRight(10);
                    loader.fit();
                    loader.step((i+1)/((double)tobetested.Length));
                    image.rescale(x,y);
                }
                Assert.True(true);
            }else{
                Assert.True(false);
            }
            
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        [InlineData(300)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        [InlineData(-300)]
        [InlineData(0.11)]
        [InlineData(0.19)]
        [InlineData(3.1415)]
        [InlineData(-3.1415)]
        [InlineData(6.2830)]
        [InlineData(-6.2830)]
        public void rotateTest(double theta)
        {
            if(loadedimages.Length>0){
                MyImage image;
                Console.WriteLine("");
                loading loader = new loading(Console.CursorTop);
                loader.header = Convert.ToString(theta)+" |";
                loader.fit();
                for(double i=0;i<loadedimages.Length;i++){
                    image = loadedimages[(int)i];
                    loader.half = tobetested[(int)i].Substring(tobetested[(int)i].LastIndexOf('/')).PadRight(10);
                    loader.fit();
                    loader.step(i/((double)tobetested.Length-1));
                    image.rotate(theta);
                }
                Assert.True(true);
            }else{
                Assert.True(false);
            }
            
        }
        //[Fact]
        public void dispTest()
        {
            if(loadedimages.Length>0){
                int width = (Console.WindowWidth-2)/4;
                int height = (Console.WindowHeight-2);
                MyImage image;
                Console.WriteLine("");
                loading loader = new loading(Console.CursorTop);
                loader.fit();
                for(double i=0;i<loadedimages.Length;i++){
                    image = loadedimages[(int)i];
                    loader.half = tobetested[(int)i].Substring(tobetested[(int)i].LastIndexOf('/')).PadRight(10);
                    loader.fit();
                    loader.step(i/((double)tobetested.Length-1));
                    image.rescale(width,height).dispwithcolor();
                }
                Assert.True(true);
            }else{
                Assert.True(false);
            }
            
        }
    }
}
