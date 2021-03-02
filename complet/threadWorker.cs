using System;
using System.Threading;
namespace complet
{
    public class threadWorker
    {
        public int x=0;
        public int y=0;
        public int height=1;
        public MyImage result;
        public MyImage source;
        public MyImage kernel;
        public MyImage output;
        public bool finished = false;
        public double[] param;
        public threadWorker(MyImage _source){
            source = _source;
        }
        public void convo(){
            finished = false;
            result =  source.convo(kernel,y,y+height);
            //output.blit(result,x,y);
            finished = true;
        }
        public void Mandelbrot(){
            finished = false;
            result =  source.Mandelbrot(param[0],param[1],param[2],param[3]);
            Console.Write("start blit  ");
            Console.Write(y);
            Console.Write(" ");
            Console.WriteLine(param[3]-param[1]);
            output.blit(result,x,y);
            finished = true;
        }
        public override string  ToString(){
            string temp="";
            temp+=Convert.ToString(x);
            temp+=" ";
            temp+=Convert.ToString(y);
            temp+=" ";
            temp+=Convert.ToString(height);
            temp+=" ";
            temp+=Convert.ToString(result==null);
            return temp;
        }
    }
}