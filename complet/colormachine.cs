using System.Collections.Generic;
using System;

namespace complet
{
    public class colormachine
    {
        private static Dictionary<pixel, ConsoleColor > thecolordict= new Dictionary<pixel, ConsoleColor>(){
            {new pixel(0,0,0), ConsoleColor.Black},
            {new pixel(0,55,218),ConsoleColor.DarkBlue},
            {new pixel(58,15,221),ConsoleColor.Cyan},
            {new pixel(19,150,14),ConsoleColor.DarkGreen},
            {new pixel(136,23,152),ConsoleColor.DarkMagenta},
            {new pixel(197,15,31),ConsoleColor.DarkRed},
            {new pixel(255,255,255),ConsoleColor.White},
            {new pixel(193,156,0),ConsoleColor.DarkYellow},
            {new pixel(118,118,118),ConsoleColor.Gray},
            {new pixel(59,120,255),ConsoleColor.Blue},
            {new pixel(22,180,12),ConsoleColor.Green},
            {new pixel(231,72,86),ConsoleColor.Red},
            {new pixel(180,0,158),ConsoleColor.Magenta}
        };
        public colormachine(){

        }
        public static ConsoleColor closestmatch(pixel p){
            ConsoleColor res = ConsoleColor.Black;
            double smallest = 99999999;
            pixel dist;
            foreach( pixel i in thecolordict.Keys){
                dist = p-i;
                if(dist<smallest){
                    smallest = dist.Norm;
                    res = thecolordict[i];
                }
            }
            return res;
        }
    }
}