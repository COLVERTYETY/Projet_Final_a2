using System;

namespace decouverte
{
    public struct pixel
    {
        private int r, g, b;
        public int R{
            get{return r;}
            set{r = value;}
        }
        public int G{
            get{return g;}
            set{g = value;}
        }
        public int B{
            get{return b;}
            set{b = value;}
        }
        public pixel(int _r, int _g, int _b){
            r = _r;
            g = _g;
            b = _b;
        }
    }
}