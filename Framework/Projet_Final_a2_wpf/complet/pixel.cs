using System;

namespace complet
{
    public struct pixel
    {
        public double r,g,b;
        public byte R{
            get{
                return (byte)r;
            }
            set{
                r = value;
            }
        }
        public byte G{
            get{
                return (byte)g;
            }
            set{
                g = value;
            }
        }
        public byte B{
            get{
                return (byte)b;
            }
            set{
                b = value;
            }
        }
        public pixel hsv{
            get{
                double r_ = r/255;
                double g_ = g/255;
                double b_ = b/255;
                double cmax = max(r_,max(g_,b_));
                double cmin = min(r_,min(g_,b_));
                double delta = cmax-cmin;
                //calculate hue
                double Hue=0;
                if(delta == 0){
                    Hue = 0;
                }else {
                    if(cmax == r_){
                        Hue = 60*(((g_-b_)/delta)%6);
                    }else if (cmax == g_){
                        Hue = 60*(((b_-r_)/delta)+2);
                    }else if (cmax == b_){
                        Hue = 60*(((r_-g_)/delta)+4);
                    }
                }
                //calculkate saturation
                double s=0;
                if(cmax!=0){
                    s = delta/cmax;
                }
                //calculate value
                double v = cmax;
                return new pixel(Hue,s,v);
            }
            set{
                double h = value.r;
                double s = value.g;
                double v = value.b;
                double c = v*s;
                double x = c*(1-Math.Abs(((h/60)%2) - 1));
                double m = v-c;
                h = h%360;
                double r_=0;
                double g_=0;
                double b_=0;
                switch (((int)(h-h%60))/(int)60){
                    case 0:
                        r_ = c;
                        g_ = x;
                        b_ = 0;
                    break;
                    case 1:
                        r_ = x;
                        g_ = c;
                        b_ = 0;
                    break;
                    case 2:
                        r_ = 0;
                        g_ = c;
                        b_ = x;
                    break;
                    case 3:
                        r_ = 0;
                        g_ = x;
                        b_ = c;
                    break;
                    case 4:
                        r_ = x;
                        g_ = 0;
                        b_ = c;
                    break;
                    case 5:
                        r_ = c;
                        g_ = 0;
                        b_ = x;
                    break;
                }
                r = ((r_+m)*255)%255;
                g = ((g_+m)*255)%255;
                b = ((b_+m)*255)%255;
            }
        }   
        public double avg{
            get{
                return (r+g+b)/3.0;
            }
        }
        public double Norm{
            get{
                return Math.Sqrt((r*r)+(g*g)+(b*b));
            }
        }
        public double NormSquared{
            get{
                return (r*r)+(g*g)+(b*b);
            }
        }
        public pixel(double scalaire){
            r = scalaire;
            g = scalaire;
            b = scalaire;
        }
        public pixel(double _r, double _g, double _b){
            r = _r;
            g = _g;
            b = _b;
        }
        public pixel(double[] arr){
            r = arr[0];
            g = arr[1];
            b = arr[2];
        }
        public pixel(double[] arr, int offset){
            r = arr[offset];
            g = arr[offset+1];
            b = arr[offset+2];
        }
        public override string ToString(){
            return Convert.ToString(r)+":"+Convert.ToString(g)+":"+Convert.ToString(b);
        }
        public static pixel operator +(pixel a,pixel b){
            return new pixel(a.r+b.r,a.g+b.g,a.b+b.b);
        }
        public static pixel operator -(pixel a,pixel b){
            return new pixel(a.r-b.r,a.g-b.g,a.b-b.b);
        }
        public static pixel operator *(pixel a,pixel b){
            return new pixel(a.r*b.r,a.g*b.g,a.b*b.b);
        }
        public static pixel operator /(pixel a,pixel b){
            return new pixel(a.r/b.r,a.g/b.g,a.b/b.b);
        }
        public static bool operator ==(pixel a, pixel b){
            return a.r==b.r && a.g==b.g && a.b==b.b;
        }
        public static bool operator >(pixel a, pixel b){
            return a.r>b.r && a.g>b.g && a.b>b.b;
        }
        public static bool operator <(pixel a, pixel b){
            return a.r<b.r && a.g<b.g && a.b<b.b;
        }
        public static bool operator !=(pixel a, pixel b){
            return a.r!=b.r && a.g!=b.g && a.b!=b.b;
        }
        public static pixel operator *(pixel a,double d){
            return new pixel(a.r*d,a.g*d,a.b*d);
        }
        public static pixel operator /(pixel a,double d){
            return new pixel(a.r/d,a.g/d,a.b/d);
        }
        public static bool operator >(pixel a,double b){
            return a.NormSquared>b*b;
        }
        public static bool operator <(pixel a,double b){
            return a.NormSquared<b*b;
        }
        public static bool operator ==(pixel a,double b){
            return a.NormSquared==b*b;
        }
        public static bool operator !=(pixel a,double b){
            return a.NormSquared!=b*b;
        }
        static double max(double a, double b){
            if(a>b){
                return a;
            }else{
                return b;
            }
        }
        static double min(double a, double b){
            if(a>b){
                return b;
            }else{
                return a;
            }
        }
    }
}