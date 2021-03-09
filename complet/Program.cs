using System;
using System.Collections.Generic;
using System.Collections;
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
            if(Directory.Exists(path)){
                string[] thefiles = Directory.GetFiles(path,"*.bmp");
                if(thefiles.Length>0){
                    Console.WriteLine("Les images suivantes ont été trouvé:");
                    foreach(string name in thefiles){
                        Console.WriteLine(name);
                    }
                    string outputs = Directory.GetCurrentDirectory()+"/images/resultats/";
                    Directory.CreateDirectory(outputs);
                    //resize
                    Console.WriteLine("chargement des images a la memoire");
                    List<MyImage> images = new List<MyImage>();
                    foreach(string name in thefiles){
                        images.Add(new MyImage(name));
                        string actualname = name.Substring(path.Length);
                        Console.WriteLine(actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nmandelbrot");
                    Console.ResetColor();
                    new MyImage(1000,1000).Mandelbrot(-2.5,-1,1,1).From_Image_To_File(outputs+"mandelbrot.bmp");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations de changement de taille");
                    Console.ResetColor();
                    Directory.CreateDirectory(outputs+"/rescale/");
                    for(int i=0;i<images.Count;i++){
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].rescale(200,200).From_Image_To_File(outputs+"/rescale/"+actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations de rotation");
                    Console.ResetColor();
                    Directory.CreateDirectory(outputs+"/rotate/");
                    for(int i=0;i<images.Count;i++){
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].rotate(20).From_Image_To_File(outputs+"/rotate/"+actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations de nuance de gris");
                    Console.ResetColor();
                    Directory.CreateDirectory(outputs+"/gris/");
                    for(int i=0;i<images.Count;i++){
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].greyscale().From_Image_To_File(outputs+"/gris/"+actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations noir et blanc");
                    Console.ResetColor();
                    Directory.CreateDirectory(outputs+"/netb/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].polarise(128).From_Image_To_File(outputs+"/netb/"+actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations hsvshift");
                    Console.ResetColor();
                    Directory.CreateDirectory(outputs+"/hsv/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].hsvShift(new pixel(30,0,0)).From_Image_To_File(outputs+"/hsv/"+actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations convolution flou 3*3");
                    Console.ResetColor();
                    double[,] k = new double[,]{
                        {1,1,1},
                        {1,1,1},
                        {1,1,1}
                    };
                    MyImage kernel = new MyImage(k);
                    kernel.flattenkernel();
                    Directory.CreateDirectory(outputs+"/flou/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].convo(kernel).From_Image_To_File(outputs+"/flou/"+actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations detection de bord 3*3");
                    Console.ResetColor();
                    k = new double[,]{
                        {-1,-1,-1},
                        {-1,8,-1},
                        {-1,-1,-1}
                    };
                    kernel = new MyImage(k);
                    Directory.CreateDirectory(outputs+"/bord/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].convo(kernel).polarise(0).From_Image_To_File(outputs+"/bord/"+actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations convolution sharpen 3*3");
                    Console.ResetColor();
                    k = new double[,]{
                        {-1,-1,-1},
                        {-1,9,-1},
                        {-1,-1,-1}
                    };
                    kernel = new MyImage(k);
                    Directory.CreateDirectory(outputs+"/sharp/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].convo(kernel).From_Image_To_File(outputs+"/sharp/"+actualname);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nopérations kmeans avec k=8");
                    Console.ResetColor();
                    Directory.CreateDirectory(outputs+"/kmeans/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].fromclosest(images[i].Kmeans(8,100)).From_Image_To_File(outputs+"/kmeans/"+actualname);
                    }
                    
                    Console.WriteLine("\nLe résultat des opérations vont être storé dans le fichier:");
                    Console.WriteLine(outputs);
                }else{
                Console.Write("\nAucune image en .bmp n'a été détécté dans le fichiers ");
                Console.WriteLine(Directory.GetCurrentDirectory()+"/images/");
                }
            }else{
                Console.WriteLine("\nLe fichier contenant les images de test n existe pas.");
                Console.WriteLine("veuillez creez le fichier suivant et y déposer des images a manipuler.");
                Console.WriteLine(Directory.GetCurrentDirectory()+"/images/");
            }
            
            
            //image.rescale(5000,5000).Mandelbrot(0.22,0.10,0.32,-0.10).From_Image_To_File($"{path}test.bmp");
        }
    }
}
