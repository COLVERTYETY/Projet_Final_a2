namespace complet
{
    public class threadWorker
    {
        public int x=0;
        public int y=0;
        public MyImage result;
        public MyImage source;

        public threadWorker(MyImage _source){
            source = _source;
        }
        public void task(){}
    }
}