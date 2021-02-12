using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace decouverte
{
    class Program
    {
        static byte[] inttole(int i){
            byte[] res= new byte[4];
            res[0] = (byte)i;
            res[1] = (byte)(((uint)i >> 8) & 0xFF);
            res[2] = (byte)(((uint)i >> 16) & 0xFF);
            res[3] = (byte)(((uint)i >> 24) & 0xFF);
            return res;
        }
        static int LE(byte[] arr, int pos, int nbits){
            int res = 0;
            for(int i = nbits;i>=0;i--){
                res|= ((((Int32)arr[pos+i]))<<i*8);
            }
            return res;
        }
        static byte[] el( UInt32 input, int nbits){
            byte[] res = new byte[4];
            for(int i=0;i<nbits;i++){
                res[i]=(byte)(input*Math.Pow(256,-1*i));
            }
            return res;
        }
        static int le(byte[] arr, int pos, int nbits){
            int res=0;
            for(int i=0;i<nbits;i++){
                res+=arr[i+pos]*(int)Math.Pow(256,i);
            }
            return res;
        }
        static void Main(string[] args)
        {
            byte[] myfile = File.ReadAllBytes("./images/bellpeper.bmp");
            Console.WriteLine("\n Header \n");
            Console.Write("utilisation du fichier: ");
            char[] osid = new char[2]{(char)(myfile[0]),(char)(myfile[1])};
            string osidstring = string.Join("",osid);
            Console.WriteLine(osidstring);
            Console.Write("taille du fichier: ");
            int size = le(myfile, 2, 4);
            Console.WriteLine(size);
            Console.WriteLine("identification du createur du fichier: ");
            Console.Write(myfile[6]);
            Console.WriteLine("  : "+ (char)(myfile[6]));
            Console.Write(myfile[7]);
            Console.WriteLine("  : "+ (char)(myfile[7]));
            Console.WriteLine("identification du createur du fichier -  part 2: ");
            Console.Write(myfile[8]);
            Console.WriteLine("  : "+ (char)(myfile[8]));
            Console.Write(myfile[9]);
            Console.WriteLine("  : "+ (char)(myfile[9]));
            Console.Write("offset: ");
            int offset = le(myfile, 10, 4);
            Console.WriteLine(offset);
            //Métadonnées de l'image
            Console.WriteLine("\n DIB HEADER :\n");
            int width = 0;
            int height = 0;
            switch (osidstring)
            {
                case "BM":
                    Console.WriteLine(" Windows 3.1x, 95, NT, ... etc.\n" );
                    int sizeofheader = le(myfile, 14, 4);
                    Console.Write("taille du header: ");
                    Console.WriteLine(sizeofheader);
                    width = le(myfile, 18, 4);
                    Console.Write("largeur de la bmp en pixels: ");
                    Console.WriteLine(width);
                    height = le(myfile, 22, 4);
                    Console.Write("hauteur de la bmp en pixels: ");
                    Console.WriteLine(height);
                    int numberofbitperpxl = le(myfile, 28, 2);
                    Console.Write("nombre de bit par pixel: ");
                    Console.WriteLine(numberofbitperpxl);
                    int compressionmethod = le(myfile, 30, 4);
                    Console.Write("method de copression: ");
                    Console.WriteLine(compressionmethod);
                    int imagesize = le(myfile, 34, 4);
                    Console.Write("taille de l'image: ");
                    Console.WriteLine(imagesize);
                    int horresolution = le(myfile, 38, 4);
                    Console.Write("resolution horrizontale: ");
                    Console.WriteLine(horresolution);
                    int verresolution = le(myfile, 42, 4);
                    Console.Write("resolution verticale: ");
                    Console.WriteLine(verresolution);
                    int ncolorsinpalette = le(myfile, 46, 4);
                    Console.Write("nombre de couleurs dans la palette: ");
                    Console.WriteLine(ncolorsinpalette);
                    int nimportantcolors = le(myfile, 50, 4);
                    Console.Write("nombre de couleurs importante: ");
                    Console.WriteLine(nimportantcolors);
                    Console.WriteLine("\n IMAGE \n");
                    MyImage image = new MyImage(myfile, offset, width, numberofbitperpxl);
                    // image.dispwithcolor();
                    // image.Mirror(true).dispwithcolor();
                    // image.Mirror(false).dispwithcolor();
                    // image.Mirror(true).Mirror(false).dispwithcolor();
                    // grey.From_Image_To_File("./images/test.bmp");
                    // image.rotate(3).rescale(25, 25).dispwithcolor();
                    image.rotate(-3).rescale(1500, 1500).From_Image_To_File("./images/peper1.bmp");
                    // image.rotate(3).rescale(250, 250).From_Image_To_File("./images/peper2.bmp");
                    break;
                case "BA":
                    Console.WriteLine(" OS/2 struct bitmap array");
                    break;
                case "CI":
                    Console.WriteLine(" OS/2 struct color icon");
                    break;
                case "CP":
                    Console.WriteLine(" OS/2 const color pointer");
                    break;
                case "IC":
                    Console.WriteLine(" OS/2 struct icon");
                    break;
                case "PT":
                    Console.WriteLine(" OS/2 pointer");
                    break;
                default:
                    Console.WriteLine(" osid not yet supported");
                    break;
            }   

        }
    }
}
