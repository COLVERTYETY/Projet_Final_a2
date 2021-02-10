using System;
namespace decouverte
{
    public struct vect : triplet 
    {
        private double r, g, b;
        public double R{
            get{return r;}
            set{r = value;}
        }
        public double G{
            get{return g;}
            set{g = value;}
        }
        public double B{
            get{return b;}
            set{b = value;}
        }
        public double norm{
            get{
                return Math.Sqrt(this.r*this.r + this.g*this.g + this.b*this.b);
            }
        }
        public double avg{
            get{return ((double)(r+g+b))/3.0;}
        }
        public vect( double _r, double _g, double _b){
            r = _r;
            g = _g;
            b = _b;
        }
        public vect( pixel a){
            r = (double)a.R;
            g = (double)a.G;
            b = (double)a.B;
        }
        public static bool operator <(vect vect, double c){
            return vect.norm<c;
        }
        public static bool operator >(vect vect, double c){
            return vect.norm>c;
        }
        public static bool operator ==(vect vect, double c){
            return vect.norm==c;
        }
        public static bool operator !=(vect vect, double c){
            return vect.norm!=c;
        }
        public static vect operator +(vect a, vect c){
            return new vect(a.r+c.r,a.g+c.g,a.b+c.b);
        }
        public static vect operator -(vect a, vect c){
            return new vect(a.r-c.r,a.g-c.g,a.b-c.b);
        }
        public static vect operator *(vect a, vect c){
            return new vect(a.r*c.r,a.g*c.g,a.b*c.b);
        }
        public static vect operator /(vect a, vect c){
            return new vect(a.r/c.r,a.g/c.g,a.b/c.b);
        }
    }
}