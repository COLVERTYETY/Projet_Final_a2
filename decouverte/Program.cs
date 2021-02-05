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
        static int LE(byte[] arr, int pos, int nbits){
            int res = 0;
            for(int i = nbits;i>=0;i--){
                res|= (arr[pos+i]<<i*8);
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

            
            byte[] myfile = File.ReadAllBytes("../images/smolrick.bmp");
            //myfile est un vecteur composé d'octets représentant les métadonnées et les données de l'image
           
            //Métadonnées du fichier

            Console.WriteLine("\n Header \n");
            // for (int i = 0; i < 14; i++)
            // {   
            //     Console.Write(myfile[i] + " ");
            // }
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
                    Dictionary<pixel, ConsoleColor > thecolordict= new Dictionary<pixel, ConsoleColor>();
                    thecolordict.Add(new pixel(12,12,12),ConsoleColor.Black);
                    thecolordict.Add(new pixel(0,55,218),ConsoleColor.DarkBlue);
                    thecolordict.Add(new pixel(58,15,221),ConsoleColor.Cyan);
                    thecolordict.Add(new pixel(19,161,14),ConsoleColor.DarkGreen);
                    thecolordict.Add(new pixel(136,23,152),ConsoleColor.DarkMagenta);
                    thecolordict.Add(new pixel(197,15,31),ConsoleColor.DarkRed);
                    thecolordict.Add(new pixel(255,255,255),ConsoleColor.White);
                    thecolordict.Add(new pixel(193,156,0),ConsoleColor.DarkYellow);
                    thecolordict.Add(new pixel(118,118,118),ConsoleColor.Gray);
                    thecolordict.Add(new pixel(59,120,255),ConsoleColor.Blue);
                    //thecolordict.Add(new pixel(59,120,255),ConsoleColor.Blue);
                    int multof4width = 4*(((width*(numberofbitperpxl/8))+2)/4);
                    for( int i = offset;i<myfile.Length;i+=multof4width){
                        for(int j=i;j<i+(width*numberofbitperpxl/8);j+=numberofbitperpxl/8){
                            int avg = (myfile[j]+myfile[j+1]+myfile[j+2])/3;
                            ConsoleColor temp = ConsoleColor.Red;
                            double small=99999999;
                            foreach(pixel p in thecolordict.Keys){
                                double dist=((p.R-(int)myfile[j])*(p.R-(int)myfile[j])) +((p.G-(int)myfile[j+1])*(p.G-(int)myfile[j+1])) +((p.B-(int)myfile[j+2])*(p.B-(int)myfile[j+2]));
                                if( dist<small){
                                    small = dist;
                                    temp = thecolordict[p];
                                }
                            }
                            Console.BackgroundColor = temp;
                            Console.Write(Convert.ToString(avg).PadLeft(4));
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
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
