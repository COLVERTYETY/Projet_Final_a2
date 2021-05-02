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
        static int[,] m2 = new int[25,25]{
            {  1,  1,  1,  1,  1,  1,  1,000,  1,296,295,202,201,200,199,111,110,109,108, 67, 66, 65, 64,  3,  2},
            {  1,  0,  0,  0,  0,  0,  1,000,  1,294,293,204,203,198,197,113,112,107,106, 69, 68, 63, 62,  5,  4},
            {  1,  0,  1,  1,  1,  0,  1,000,  1,292,291,206,205,196,195,115,114,105,104, 71, 70, 61, 60,  7,  6},
            {  1,  0,  1,  1,  1,  0,  1,000,  0,290,289,208,207,194,193,117,116,103,102, 73, 72, 59, 58,  9,  8},
            {  1,  0,  1,  1,  1,  0,  1,000,  1,288,287,210,209,192,191,118,  1,  1,  1,  1,  1, 57, 56, 11, 10},
            {  1,  0,  0,  0,  0,  0,  1,000,  1,286,285,212,211,190,189,119,  1,  0,  0,  0,  1, 55, 54, 13, 12},
            {  1,  1,  1,  1,  1,  1,  1,000,  1,284,283,214,213,188,187,120,  1,  0,  1,  0,  1, 53, 52, 15, 14},
            {000,000,000,000,000,000,  0,000,  1,282,281,216,215,186,185,121,  1,  0,  0,  0,  1, 51, 50, 17, 16},
            {360,359,330,329,328,327,  1,298,297,280,279,218,217,184,183,122,  1,  1,  1,  1,  1, 49, 48, 19, 18},
            {358,357,332,331,326,325,  0,300,299,278,277,220,219,182,181,124,123,101,100, 75, 74, 47, 46, 21, 20},
            {356,355,334,333,324,323,  1,302,301,276,275,222,221,180,179,126,125, 99, 98, 77, 76, 45, 44, 23, 22},
            {354,353,336,335,322,321,  0,304,303,274,273,224,223,178,177,128,127, 97, 96, 79, 78, 43, 42, 25, 24},
            {352,351,338,337,320,319,  1,306,305,272,271,226,225,176,175,130,129, 95, 94, 81, 80, 41, 40, 27, 26},
            {350,349,340,339,318,317,  0,308,307,270,269,228,227,174,173,132,131, 93, 92, 83, 82, 39, 38, 29, 28},
            {348,347,342,341,316,315,  1,310,309,268,267,230,229,172,171,134,133, 91, 90, 85, 84, 37, 36, 31, 30},
            {346,345,344,343,314,313,  0,312,311,266,265,232,231,170,169,136,135, 89, 88, 87, 86, 35, 34, 33, 32},
            {  1,  1,  1,  0,  1,  1,  1,  1,  1,264,263,234,233,168,167,138,137,  1,  1,  0,  0,  0,  1,  0,  0},
            {  0,  0,  0,  0,  0,  0,  0,000,  1,262,261,236,235,166,165,140,139,  0,  0,  0,  0,  0,  0,  0,  0},
            {  1,  1,  1,  1,  1,  1,  1,000,  1,000,001,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  1,  1,  1,  1},
            {  1,  0,  0,  0,  0,  0,  1,000,  0,260,259,238,237,164,163,142,141,  0,  1,  0,  0,  0,  0,  0,  1},
            {  1,  0,  1,  1,  1,  0,  1,000,  0,258,257,240,239,162,161,144,143,  0,  1,  0,  1,  1,  1,  0,  1},
            {  1,  0,  1,  1,  1,  0,  1,000,  0,256,255,242,241,160,159,146,145,  0,  1,  0,  1,  1,  1,  0,  1},
            {  1,  0,  1,  1,  1,  0,  1,000,  1,254,253,244,243,158,157,148,147,  0,  1,  0,  1,  1,  1,  0,  1},
            {  1,  0,  0,  0,  0,  0,  1,000,  0,252,251,246,245,156,155,150,149,  0,  1,  0,  0,  0,  0,  0,  1},
            {  1,  1,  1,  1,  1,  1,  1,000,  0,250,249,248,247,154,153,152,151,  0,  1,  1,  1,  1,  1,  1,  1}
        };
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
        static public string pad(string content,int maxx){
            string res = content;
            if(res.Length<maxx){
                if(res.Length-4<maxx){
                    res+="0000";
                }else{
                    res.PadRight(maxx,'0');
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
        static public string stringfrombytearray(byte[] data){
            string res="";
            foreach(byte b in data){
                res+=Convert.ToChar(b);
            }
            return res;
        }
        static public int[] encodeText(string text){
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
            return res;
        }
        static public string padwith236and17(string content,int maxx){
            int i=0;
            while(content.Length<maxx){
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
                        if(tempalte[i,j]<content.Length){
                            res[i,j] = Math.Abs((int)content[tempalte[i,j]-2]-48 - (i+j+1)%2);
                        }else{
                            res[i,j] = (i+j+1)%2;
                        }
                    }
                }
            }
            return res;
        }
        static public int[,] tointmat(MyImage input){
            int[,] res= new int[input.width,input.height];
            for(int i=0;i<input.height;i++){
                for(int j=0;j<input.width;j++){
                    res[j,i] = (int)input.data[i][j].avg;
                }
            }
            return res;
        }
        static public string readTemplate(int[,] template, int [,] data){
            char[] res = new char[360];
            for(int i=0;i<template.GetLength(0);i++){
                for(int j=0;j<template.GetLength(1);j++){
                    if(template[i,j]>1){
                        res[template[i,j]-2] = (char)(Math.Abs( data[i,j]-48-(i+j+1)%2));
                    }
                }
            }
            string output="";
            foreach(char c in res){
                if(c=='1' || c=='0'){
                    output+=c;
                }
            }
            return output;
        }
        static public string decodeM1L(MyImage imageofqr){
            string output;
            int[,] data = tointmat(imageofqr);
            string raw = readTemplate(m1,data);
            byte[] bytes = bytesfromstring(raw);
            byte[] ecc = new byte[7];
            byte[] msg = new byte[bytes.Length-7];
            for(int i=0;i<bytes.Length;i++){
                if(i>=bytes.Length-7){
                    ecc[i-bytes.Length] = bytes[i];
                }
                else{
                    msg[i] = bytes[i];
                }
            }
            bytes = ReedSolomonAlgorithm.Decode(msg,ecc,ErrorCorrectionCodeType.QRCode);
            output = stringfrombytearray(bytes);
            return output;
        }
        static public MyImage Mode1L(string text){
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
            data = pad(data,152);
            // pad 8
            data = padto8(data);
            //fill with 236 17
            data = padwith236and17(data,152);
            // encode with reedsalomon
            byte[]temp = bytesfromstring(data);
            //byte[] temp = stringToBytes(data);
            byte[] ecc = ReedSolomonAlgorithm.Encode(temp, 7, ErrorCorrectionCodeType.QRCode);
            data+= binToString(ecc);
            //fill all
            return new MyImage(filler(m1,data));
        }
        static public MyImage Mode2L(string text){
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
            data = pad(data,34*8);
            // pad 8
            data = padto8(data);
            //fill with 236 17
            data = padwith236and17(data,34*8);
            // encode with reedsalomon
            byte[]temp = bytesfromstring(data);
            //byte[] temp = stringToBytes(data);
            byte[] ecc = ReedSolomonAlgorithm.Encode(temp, 10, ErrorCorrectionCodeType.QRCode);
            data+= binToString(ecc);
            //fill all
            return new MyImage(filler(m2,data));
        }

    }
    
}