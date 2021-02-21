using System;
using System.Threading;
namespace complet
{
    public class threadMachine
    {
        private Thread[] thethreads;
        private threadWorker[] theWorkers;
        public MyImage source;
        public int Nthreads = 2;
        public threadMachine(MyImage _source){
            source = _source;
        }
        public threadMachine(MyImage _source, int nthreads){
            source = _source;
            Nthreads = nthreads;
        }
        public void optimiseThreadCount(){
            Nthreads = Environment.ProcessorCount -1;
        }
        public MyImage convo(MyImage kernel){
            MyImage res = new MyImage(source.width, source.height);
            thethreads = new Thread[Nthreads];
            theWorkers = new threadWorker[Nthreads];
            //initilasie teh threads then map
            Console.WriteLine("begin init the trheas");
            for(int i=0;i<Nthreads;i++){
                threadWorker temp = new threadWorker(new MyImage(source.data));
                temp.x = kernel.width/2;
                temp.height = (int)(((double)1/(double)Nthreads)*(double)source.height)+(kernel.height/2);
                temp.y = (int)(((double)i/((double)Nthreads))*(double)source.height);
                temp.kernel = new MyImage(kernel.data);
                theWorkers[i] = temp;
                thethreads[i] = new Thread(new ThreadStart(temp.convo));
                thethreads[i].Priority = ThreadPriority.Highest;
            }
            // map
            Console.WriteLine("starting the threads");
            for(int i=0;i<Nthreads;i++){
                thethreads[i].Start();
            }
            res.fill(new pixel(0,0,0));
            // join / wait for all the threads to finish and reduce as we go allong
            foreach(Thread t in thethreads){
                t.Join();
            }
            Console.WriteLine("joined !!");
            foreach(threadWorker t in theWorkers){
                res.blit(t.result,t.x,t.y);
            }
            return res;
        }
    }
}