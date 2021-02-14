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
            values = new double[1]{scalaire};
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
        
    }
}