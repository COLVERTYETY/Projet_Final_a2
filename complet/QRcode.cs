using System;
using System.Collections;
namespace complet
{
    public class QRcode
    {
        private QRcode(){}
        static int mask = 0b111011111000100;
        static int maxbits1 = 152;
        static int char1L = 25;
        static int char2L = 47;
        static int alphanumericmode = 2;
        static int[,] m1 = new int[21,21]{
            {  1,  1,  1,  1,  1,  1,  1,  0,  1,177,176, 99, 98,97,96,51,50,49,48, 3, 2},
            {  1,  0,  0,  0,  0,  0,  1,  0,  1,175,174,101,100,95,94,53,52,47,46, 5, 4},
            {  1,  0,  1,  1,  1,  0,  1,  0,  1,173,172,103,102,93,92,55,54,45,44, 7, 6},
            {  1,  0,  1,  1,  1,  0,  1,  0,  0,171,170,105,104,91,90,57,56,43,42, 9, 8},
            {  1,  0,  1,  1,  1,  0,  1,  0,  1,169,168,107,106,89,88,59,58,41,40,11,10},
            {  1,  0,  0,  0,  0,  0,  1,  0,  1,167,166,109,108,87,86,61,60,39,38,13,12},
            {  1,  1,  1,  1,  1,  1,  1,  0,  1,165,164,111,110,85,84,63,62,37,36,15,14},
            {  0,  0,  0,  0,  0,  0,  0,  0,  1,163,162,113,112,83,82,65,64,35,34,17,16},
            {209,208,195,194,193,192,  1,179,178,161,160,115,114,81,80,67,66,33,32,19,18},
            {207,206,197,196,191,190,  0,181,180,159,158,117,116,79,78,69,68,31,30,21,20},
            {205,204,199,198,189,188,  1,183,182,157,156,119,118,77,76,71,70,29,28,23,22},
            {203,202,201,200,187,186,  0,185,184,155,154,121,120,75,74,73,72,27,26,25,24},
            {  1,  1,  1,  0,  1,  1,  1,  1,  1,153,152,123,122, 0, 1, 0, 0, 0, 1, 0, 0},
            {  0,  0,  0,  0,  0,  0,  0,  0,  1,151,150,125,124, 0, 0, 0, 0, 0, 0, 0, 0},
            {  1,  1,  1,  1,  1,  1,  1,  0,  1,  0,  1,  0,  1, 0, 1, 1, 1, 1, 1, 1, 1},    
            {  1,  0,  0,  0,  0,  0,  1,  0,  0,149,148,127,126, 0, 1, 0, 0, 0, 0, 0, 1},
            {  1,  0,  1,  1,  1,  0,  1,  0,  0,147,146,129,128, 0, 1, 0, 1, 1, 1, 0, 1},
            {  1,  0,  1,  1,  1,  0,  1,  0,  0,145,144,131,130, 0, 1, 0, 1, 1, 1, 0, 1},
            {  1,  0,  1,  1,  1,  0,  1,  0,  1,143,142,133,132, 0, 1, 0, 1, 1, 1, 0, 1},
            {  1,  0,  0,  0,  0,  0,  1,  0,  0,141,140,135,134, 0, 1, 0, 0, 0, 0, 0, 1},
            {  1,  1,  1,  1,  1,  1,  1,  0,  0,139,138,137,136, 0, 1, 1, 1, 1, 1, 1, 1}
        };
        static public string pad152(string content){
            string res = content;
            if(res.Length<152){
                if(res.Length-4<152){
                    res+="0000";
                }else{
                    res.PadRight(152,'0');
                }
            }
            return res;
        }
    
        static public string padto8(string content){
            if(content.Length%8==0){
                return content;
            }
            int val = 8*(int)(((content.Length+5)/8));
            return content.PadRight(val,'0');
        }
        static public string binToString(int bin, int totallength){
            string res="";
            while(bin!=0){
                if((bin & 1) == 1){
                    res+="1";
                }else{
                    res+="0";
                }
                bin = bin >>1;
            }
            string temp="";
            for(int i=0;i<res.Length;i++){
                temp+= res[res.Length-1-i];
            }
            res = temp.PadLeft(totallength,'0');
            return res;
        }
        static public string binToString(byte[] bin){
            string res ="";
            foreach(byte b in bin){
                res+=Convert.ToString(b,2).PadLeft(8,'0');
            }
            return res;
        }
        static public int[] encodeText(string text){
            text=text.ToUpper();
            int[] res=new int[(text.Length+1)/2];
            int val=0;
            for(int i=0;i<text.Length;i++){
                if(char.IsLetter(text[i])){
                    val = (int)text[i]-55;
                }else if (char.IsDigit(text[i])){
                    val = (int)text[i]-48;
                }else{
                    switch(text[i]){
                        case ' ':
                            val = 36;
                            break;
                        case '$':
                            val = 37;
                            break;
                        case '%':
                            val = 38;
                            break;
                        case '*':
                            val = 39;
                            break;
                        case '+':
                            val = 40;
                            break;
                        case '-':
                            val = 41;
                            break;
                        case '.':
                            val = 42;
                            break;
                        case '/':
                            val = 43;
                            break;
                        case ':':
                            val = 44;
                            break;
                        default:
                            throw new Exception("charactere non supporté");
                    }
                }
                if (i==text.Length-1 && (i+1)%2==1){
                    res[i/2]+=(int)Math.Pow(45,0)*val;
                }else{
                    res[i/2]+=(int)Math.Pow(45,(i+1)%2)*val;
                }
                
            }
            Console.WriteLine(string.Join(" ",res));
            return res;
        }
        static public string padwith236and17(string content){
            int i=0;
            while(content.Length<152){
                if(i%2==0){
                    content+=binToString(236,8);
                }else{
                    content+=binToString(17,8);
                }
                i++;
            }
            return content;
        } 
        static public byte[] stringToBytes(string text){
            byte[] res= new byte[text.Length/8];
            byte byteval=0b0;
            for(int i=0;i<text.Length;i+=8){
                for(int c=0;c<8;c++){
                    byteval = (byte) (byteval <<1);
                    byteval = (byte)(byteval | ((int)text[i+c]-48));
                }
                res[i/8] = byteval;
                byteval=0;
            }
            
            Console.WriteLine();
            return res;
        }
        static public byte[] bytesfromstring(string text){
            byte[] res =new byte[text.Length/8];
            for(int i=0;i<text.Length/8;i++){
                res[i] = Convert.ToByte(text.Substring(8*i,8),2);
            }
            return res;
        }
        static public int[,] filler(int[,] tempalte, string content){
            int[,] res = new int[tempalte.GetLength(0),tempalte.GetLength(1)];
            for(int i=0;i<tempalte.GetLength(0);i++){
                for(int j=0;j<tempalte.GetLength(1);j++){
                    if(tempalte[i,j]<2){
                        res[i,j] = tempalte[i,j];
                    }else{
                        res[i,j] =Math.Abs((int)content[tempalte[i,j]-2]-48 - (i+j+1)%2);
                    }
                }
            }
            return res;
        }
        static public string Mode1L(string text){
            string data = "";
            // encoding mode
            data+= binToString(alphanumericmode,4);
            // number of characters
            data+= binToString(text.Length,9);
            // encoded etxt
            int[] binoftext = encodeText(text);
            for(int i=0;i<binoftext.Length-1;i++){
                data+=binToString(binoftext[i],11);
            }
            if(text.Length%2==1){
                data+=binToString(binoftext[binoftext.Length-1],6);
            }else{
                data+=binToString(binoftext[binoftext.Length-1],11);
            }
            // terminaison
            data = pad152(data);
            // pad 8
            data = padto8(data);
            //fill with 236 17
            data = padwith236and17(data);
            // encode with reedsalomon
            byte[]temp = bytesfromstring(data);
            //byte[] temp = stringToBytes(data);
            Console.WriteLine(string.Join(' ',temp));
            byte[] ecc = ReedSolomonAlgorithm.Encode(temp, 7, ErrorCorrectionCodeType.QRCode);
            Console.WriteLine(string.Join(' ',ecc));
            data+= binToString(ecc);
            //fill all
            string comp = "0010000001011011000010110111100011010001011100101101110001001101010000110100000011101100000100011110110000010001111011000001000111101100000100011110110011010001111011111100010011001111010011101100001101101101";
            Stack eror = new Stack();
            for(int i=0;i<comp.Length;i++){
                if(data[i]!=comp[i]){
                    Console.BackgroundColor = ConsoleColor.Red;
                    eror.Push(i);
                }
                Console.Write(data[i]);
                Console.ResetColor();
            }
            Console.WriteLine();
            while(eror.Count>0){
                Console.WriteLine(eror.Pop());
            }
            MyImage k = new MyImage(filler(m1,data));
            k.From_Image_To_File("test.bmp");
            return data;
        }

    }
    
}