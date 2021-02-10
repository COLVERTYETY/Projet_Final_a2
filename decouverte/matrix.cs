using System;
namespace decouverte
{
    public class matrix
    {
        private triplet[,] data;
        public int height{
            get{return data.GetLength(1);}
        }
        public int width{
            get{return data.GetLength(0);}
        }
        public matrix(byte[] arr, int start, int width, int numberofbitperpxl){
            int multof4width = 4*(((width*(numberofbitperpxl/8))+2)/4);
            data = new triplet[width,(arr.Length - start )/ multof4width];
            Console.Write(width);
            Console.Write(" , ");
            Console.WriteLine((arr.Length - start )/ multof4width);
            Console.WriteLine(arr.Length);
            for( int i = start;i<arr.Length;i+=multof4width){
                for(int j=i;j<i+(width*numberofbitperpxl/8);j+=numberofbitperpxl/8){
                    Console.Write((j-i)/(numberofbitperpxl/8));
                    Console.Write(" : ");
                    Console.Write((i-start)/multof4width);
                    Console.Write("  ---> ");
                    Console.Write(j);
                    Console.Write(" ");
                    Console.WriteLine(i);
                    data[(j-i)/(numberofbitperpxl/8),(i-start)/multof4width] = new pixel(arr[j],arr[j+1],arr[j+2]);
                }
            }
        }
        public override string ToString()
        {
            string temp = "";
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    temp+=Convert.ToString((int)data[j,i].avg).PadLeft(4);
                }
                temp+="\n";
            }
            return temp;
        }
        public void dispwithcolor(){
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    Console.ForegroundColor = colormachine.closestmatch((pixel)data[j,i]);
                    Console.Write( Convert.ToString((int)data[j,i].avg).PadLeft(4));
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}