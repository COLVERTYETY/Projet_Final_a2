using System;
namespace complet
{
    public class qrcode
    {
        private int[] charCapacity = new int[] {25, 20, 16, 10}; // L M Q H
        public MyImage content;
        public qrcode(){

        }
        public static int[] encodeReedSalomon(int[] values){
            int[] res= new int[values.Length+2];
            for(int i=0;i<values.Length;i++){
                res[i] = values[i];
            }
            //calculate sum
            int sum=0;
            foreach(int i in values){
                sum+=i;
            }
            //calculate polynome
            int pol=0;
            for(int i=0;i<values.Length;i++){
                pol+=values[i]*(i+1);
            }
            //store correction values in arr
            res[values.Length] = sum;
            res[values.Length+1] = pol;
            return res;
        }
        public static int[] decodeReedSalomon(int[] values){
            int[] res= new int[values.Length-2];
            for(int i=0;i<res.Length;i++){
                res[i] = values[i];
            }
            //calculate sum
            int sum=0;
            foreach(int i in res){
                sum+=i;
            }
            //isthere an errr?
            if(sum!=values[values.Length-2]){
                //there is an error
                int Verreur = sum - values[values.Length-2];
                //calculate polynome
                int pol=0;
                for(int i=0;i<res.Length;i++){
                    pol+=res[i]*(i+1);
                }
                int polerr = pol - values[values.Length-1];
                int pos = (polerr / Verreur)-1;
                //now we correct
                res[pos] -=Verreur;
            }
            
            return res;
        }
    }
}