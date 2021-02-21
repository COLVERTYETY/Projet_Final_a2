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
        public bool finished = false;

        public threadWorker(MyImage _source){
            source = _source;
        }
        public void convo(){
            finished = false;
            result =  source.convo(kernel,y,y+height);
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