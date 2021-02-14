using System;
namespace complet
{
    public class loading
    {
        public string header = " |";
        public string half = "| ";
        public string ender = "%.";
        public double length = 20;
        public string filler = "█";
        public string empty = "░";
        public int Y=1;

        public loading(){
        }
        public loading(int y){
            Y=y;
        }
        public void fit(){
            length=Console.WindowWidth-header.Length-ender.Length-half.Length-4;
            Y = Console.CursorTop;
        }
        public  void step(double val){
            Console.SetCursorPosition(0,Y);
            Console.Write(header);
            int j=0;
            for(int i=0;i<val*length;i++){
                Console.Write(filler);
                j++;
            }
            for(int i=j;i<(length);i++){
                Console.Write(empty);
            }
            Console.Write(half);
            Console.Write(Convert.ToString((int)(val*100)).PadLeft(3).PadRight(3));
            Console.Write(ender);
        }
        public  void step(int y,double val){
            Y=y;
            step(val);
        }
        

    }
}