using System;

namespace decouverte
{
    public struct pixel : triplet
    {
        private byte r, g, b;
        public double R{
            get{return r;}
            set{r = (byte)value;}
        }
        public double G{
            get{return g;}
            set{g = (byte)value;}
        }
        public double B{
            get{return b;}
            set{b = (byte)value;}
        }
        public double H{
            get{
            return r;
            }
            set{b = (byte)value;}
        }
        public double S{
            get{return g;}
            set{b = (byte)value;}
        }
        public double V{
            get{return b;}
            set{b = (byte)value;}
        }
        public double avg{
            get{return ((double)(r+g+b))/3.0;}
        }
        public pixel(byte _r, byte _g, byte _b){
            r = _r;
            g = _g;
            b = _b;
        }
        public pixel(vect v){
            r = (byte)v.R;
            g = (byte)v.G;
            b = (byte)v.B;
        }
        public static vect operator +(pixel a, pixel c){
            return new vect(a.r+c.r,a.g+c.g,a.b+c.b);
        }
        public static vect operator -(pixel a, pixel c){
            return new vect(a.r-c.r,a.g-c.g,a.b-c.b);
        }
        public static vect operator *(pixel a, pixel c){
            return new vect(a.r*c.r,a.g*c.g,a.b*c.b);
        }
        public static vect operator /(pixel a, pixel c){
            return new vect(a.r/c.r,a.g/c.g,a.b/c.b);
        }
        
    }
}