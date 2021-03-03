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
                    Console.WriteLine("mandelbrot");
                    new MyImage(1000,1000).Mandelbrot(-2.5,-1,1,1).From_Image_To_File(outputs+"mandelbrot.bmp");
                    Console.WriteLine("opérations de changement de taille");
                    Directory.CreateDirectory(outputs+"/rescale/");
                    for(int i=0;i<images.Count;i++){
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].rescale(200,200).From_Image_To_File(outputs+"/rescale/"+actualname);
                    }
                    Console.WriteLine("opérations de rotation");
                    Directory.CreateDirectory(outputs+"/rotate/");
                    for(int i=0;i<images.Count;i++){
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].rotate(20).From_Image_To_File(outputs+"/rotate/"+actualname);
                    }
                    Console.WriteLine("opérations de nuance de gris");
                    Directory.CreateDirectory(outputs+"/gris/");
                    for(int i=0;i<images.Count;i++){
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].greyscale().From_Image_To_File(outputs+"/gris/"+actualname);
                    }
                    Console.WriteLine("opérations noir et blanc");
                    Directory.CreateDirectory(outputs+"/netb/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].polarise(128).From_Image_To_File(outputs+"/netb/"+actualname);
                    }
                    Console.WriteLine("opérations hsvshift");
                    Directory.CreateDirectory(outputs+"/hsv/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].hsvShift(new pixel(30,0,0)).From_Image_To_File(outputs+"/hsv/"+actualname);
                    }
                    Console.WriteLine("opérations convolution flou 3*3");
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
                    Console.WriteLine("opérations detection de bord 3*3");
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
                    Console.WriteLine("opérations convolution sharpen 3*3");
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
                    Console.WriteLine("opérations kmeans avec k=8");
                    Directory.CreateDirectory(outputs+"/kmeans/");
                    for(int i=0;i<images.Count;i++){    
                        string actualname = thefiles[i].Substring(path.Length);
                        Console.WriteLine(actualname);
                        images[i].fromclosest(images[i].Kmeans(8,100)).From_Image_To_File(outputs+"/kmeans/"+actualname);
                    }
                    
                    Console.WriteLine("Le résultat des opérations vont être storé dans le fichier:");
                    Console.WriteLine(outputs);
                }else{
                Console.Write("Aucune image en .bmp n'a été détécté dans le fichiers ");
                Console.WriteLine(Directory.GetCurrentDirectory()+"/images/");
                }
            }else{
                Console.WriteLine("Le fichier contenant les images de test n existe pas.");
                Console.WriteLine("veuillez creez le fichier suivant et y déposer des images a manipuler.");
                Console.WriteLine(Directory.GetCurrentDirectory()+"/images/");
            }
            
            
            //image.rescale(5000,5000).Mandelbrot(0.22,0.10,0.32,-0.10).From_Image_To_File($"{path}test.bmp");
        }
    }
}
