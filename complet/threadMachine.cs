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
        public static double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
        public MyImage convo(MyImage kernel){
            MyImage res = new MyImage(source.width, source.height);
            thethreads = new Thread[Nthreads];
            theWorkers = new threadWorker[Nthreads];
            //initilasie teh threads then map
            Console.WriteLine("begin init threads");
            for(int i=0;i<Nthreads;i++){
                threadWorker temp = new threadWorker(source);
                temp.x = kernel.width/2;
                temp.height = (int)(((double)1/(double)Nthreads)*(double)source.height)+(kernel.height/2);
                temp.y = (int)(((double)i/((double)Nthreads))*(double)source.height);
                temp.kernel = new MyImage(kernel.data);
                theWorkers[i] = temp;
                theWorkers[i].output = res;
                thethreads[i] = new Thread(new ThreadStart(temp.convo));
                thethreads[i].Priority = ThreadPriority.Highest;
            }
            // map
            Console.WriteLine("starting the threads");
            for(int i=0;i<Nthreads;i++){
                thethreads[i].Start();
            }
            // join / wait for all the threads to finish and reduce as we go allong
            for(int i=0;i<Nthreads;i++){
                thethreads[i].Join();
                Console.WriteLine("Joined a thread !!");
                res.blit(theWorkers[i].result,theWorkers[i].x,theWorkers[i].y);
                Console.WriteLine("blitfinished");
            }
            return res;
        }
        public MyImage Mandelbrot(double x0,double y0,double x1,double y1){
            MyImage res = new MyImage(source.width, source.height);
            thethreads = new Thread[Nthreads];
            theWorkers = new threadWorker[Nthreads];
            //initilasie teh threads then map
            Console.WriteLine("begin init threads");
            for(int i=0;i<Nthreads;i++){
                threadWorker temp = new threadWorker(source);
                temp.source = new MyImage(source.width, (int)((double)source.height/(double)Nthreads));
                temp.x = 0;
                temp.y = (int)(((double)i/((double)Nthreads))*(double)source.height);
                temp.output = res;
                temp.param = new double[]{x0,Map(i,0,Nthreads,y0,y1),x1,Map(i+1,0,Nthreads,y0,y1)};
                theWorkers[i] = temp;
                thethreads[i] = new Thread(new ThreadStart(temp.Mandelbrot));
                thethreads[i].Priority = ThreadPriority.Highest;
            }
            // map
            Console.WriteLine("starting the threads");
            for(int i=0;i<Nthreads;i++){
                thethreads[i].Start();
            }
            // join / wait for all the threads to finish and reduce as we go allong
            for(int i=0;i<Nthreads;i++){
                thethreads[i].Join();
                Console.WriteLine("Joined a thread !!");
                //res.blit(theWorkers[i].result,theWorkers[i].x,theWorkers[i].y);
                //Console.WriteLine("blitfinished");
            }
            return res;
        }
    }
}