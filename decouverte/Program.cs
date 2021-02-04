using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace LectureImage
{
    class Program
    {
        
        static void Main(string[] args)
        {

            
            byte[] myfile = File.ReadAllBytes("../images/bellpeper.bmp");
            //myfile est un vecteur composé d'octets représentant les métadonnées et les données de l'image
           
            //Métadonnées du fichier

            Console.WriteLine("\n Header \n");
            // for (int i = 0; i < 14; i++)
            // {   
            //     Console.Write(myfile[i] + " ");
            // }
            Console.WriteLine("utilisation du fichier: ");
            Console.WriteLine(myfile[0]);
            Console.WriteLine(myfile[1]);
            char[] osid = new char[2]{(char)(myfile[0]),(char)(myfile[1])};
            Console.Write("which equals: ");
            string osidstring = string.Join("",osid);
            Console.WriteLine(osidstring);
            Console.WriteLine("taille du fichier en bmp: ");
            Console.WriteLine(myfile[2]);
            Console.WriteLine(myfile[3]);
            Console.WriteLine(myfile[4]);
            Console.WriteLine(myfile[5]);
            int size = myfile[5]<<24 | myfile[4] << 16 | myfile[3] <<8 | myfile[2];
            Console.Write("which equals: ");
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
            Console.WriteLine("offset: ");
            Console.WriteLine(myfile[10]);
            Console.WriteLine(myfile[11]);
            Console.WriteLine(myfile[12]);
            Console.WriteLine(myfile[13]);
            int offset = myfile[13]<<24 | myfile[12] << 16 | myfile[11] <<8 | myfile[10];
            Console.Write("which equals: ");
            Console.WriteLine(offset);
            //Métadonnées de l'image
            Console.ReadLine();
            Console.WriteLine("\n DIB HEADER :\n");
            int dibsize = 0;
            switch (osidstring)
            {
                case "BM":
                    Console.WriteLine(" Windows 3.1x, 95, NT, ... etc.\n" );
                    break;
                case "BA":
                    Console.WriteLine(" OS/2 struct bitmap array")
                    break;
                case "CI":
                    Console.WriteLine(" OS/2 struct color icon")
                    break;
                case "CP":
                    Console.WriteLine(" OS/2 const color pointer");
                    break;
                case "IC":
                    Console.WriteLine(" OS/2 struct icon");
                    break;
                case "PT":
                    Console.WriteLine(" OS/2 pointer")
                    break;
                default:
                    Console.WriteLine(" osid not yet supported");
                    break;
            }
            for (int i = 14; i< offset; i++)
            {
                Console.Write(myfile[i] + " ");
            }    
            Console.ReadLine();
            //L'image elle-même
            Console.WriteLine("\n IMAGE \n");
            for (int i = offset; i < size; i = i + 60)
            {
                for (int j = i; j < i + 60; j++)
                {
                    Console.Write(myfile[j] + " ");

                }
                Console.WriteLine();
            }

            File.WriteAllBytes("./Images/Sortie.bmp", myfile);
  
            Console.ReadLine();
        }
    }
}
