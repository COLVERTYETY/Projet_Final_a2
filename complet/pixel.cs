using System;

namespace complet
{
    public struct pixel
    {
        private double[] values;
        public int Nbits{
            get{return values.Length;}
        }
        public double[] Values{
            get{return values;}
            set{
                values = value; 
            }
        }
        public byte R{
            get{
                return (byte)values[0];
            }
            set{
                values[0] = value;
            }
        }
        public byte G{
            get{
                return (byte)values[1];
            }
            set{
                values[1] = value;
            }
        }
        public byte B{
            get{
                return (byte)values[2];
            }
            set{
                values[2] = value;
            }
        }
        public pixel hsv{
            get{
                double r_ = values[0]/255;
                double g_ = values[1]/255;
                double b_ = values[2]/255;
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
                double h = value.values[0];
                double s = value.values[1];
                double v = value.values[2];
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
                R = (byte)((r_+m)*255);
                G = (byte)((g_+m)*255);
                B = (byte)((b_+m)*255);
            }
        }   
        public double avg{
            get{
                double res=0;
                for(int i=0;i<values.Length;i++){
                    res += values[i];
                }
                return res/(double)values.Length;
            }
        }
        public double Norm{
            get{
                double res = 0;
                for(int i=0;i<values.Length;i++){
                    res += values[i]*values[i];
                }
                return Math.Sqrt(res);
            }
        }
        public double NormSquared{
            get{
                double res = 0;
                for(int i=0;i<values.Length;i++){
                    res += values[i]*values[i];
                }
                return res;
            }
        }
        public pixel(double scalaire){
            values = new double[3]{scalaire,scalaire,scalaire};
        }
        public pixel(double r, double g, double b){
            values = new double[3]{r,g,b};
        }
        public pixel(double[] arr){
            values = arr;
        }
        public pixel(double[] arr, int offset, int nbits){
            values = new double[nbits];
            for(int i=0;i<nbits;i++){
                values[i] = arr[offset+i];
            }
        }
        public override string ToString(){
            return string.Join(':',values);
        }
        public static pixel operator +(pixel a,pixel b){
            double[] temp = new double[a.Nbits];
            for(int i=0;i<temp.Length;i++){
                temp[i] = a.values[i]+b.values[i];
            }
            return new pixel(temp);
        }
        public static pixel operator -(pixel a,pixel b){
            double[] temp = new double[a.Nbits];
            for(int i=0;i<temp.Length;i++){
                temp[i] = a.values[i]-b.values[i];
            }
            return new pixel(temp);
        }
        public static pixel operator *(pixel a,pixel b){
            double[] temp = new double[a.Nbits];
            for(int i=0;i<temp.Length;i++){
                temp[i] = a.values[i]*b.values[i];
            }
            return new pixel(temp);
        }
        public static pixel operator /(pixel a,pixel b){
            double[] temp = new double[a.Nbits];
            for(int i=0;i<temp.Length;i++){
                temp[i] = a.values[i]/b.values[i];
            }
            return new pixel(temp);
        }
        public static pixel operator *(pixel a,double d){
            double[] temp = new double[a.Nbits];
            for(int i=0;i<temp.Length;i++){
                temp[i] = a.values[i]*d;
            }
            return new pixel(temp);
        }
        public static pixel operator /(pixel a,double d){
            double[] temp = new double[a.Nbits];
            for(int i=0;i<temp.Length;i++){
                temp[i] = a.values[i]/d;
            }
            return new pixel(temp);
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