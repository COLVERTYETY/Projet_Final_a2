using System;
using System.IO;
namespace decouverte
{
    public class  MyImage
    {
        public triplet[,] data;
        public int height{
            get{return data.GetLength(1);}
        }
        public int width{
            get{return data.GetLength(0);}
        }
        public  MyImage(byte[] arr, int start, int width, int numberofbitperpxl){
            int multof4width = 4*(((width*(numberofbitperpxl/8))+2)/4);
            data = new triplet[width,(arr.Length - start )/ multof4width];
            for( int i = start;i<arr.Length;i+=multof4width){
                for(int j=i;j<i+(width*numberofbitperpxl/8);j+=numberofbitperpxl/8){
                    data[(j-i)/(numberofbitperpxl/8),(i-start)/multof4width] = new pixel(arr[j+2],arr[j+1],arr[j]);
                }
            }
        }
        public MyImage(int _width, int _height){  //creates a blank image
            data = new triplet[_width,_height];
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
        public void  From_Image_To_File(string pathName){
            // ! palettes not taken in account 
            const int headers = 54;
            int multof4width = 4*(((width*(3))+2)/4);
            int imagesize = multof4width * height + 2;
            int totalsize = headers + imagesize;  // TODO  aclculate size
            byte[] file= new byte[totalsize];
            // BM
            file[0] = (byte)'B';
            file[1] = (byte)'M';
            // tottalsize
            byte[] temp = new byte[4];
            temp = Convertir_Int_To_Endian((UInt32) totalsize, 4);
            file[2] = temp[0];
            file[3] = temp[1];
            file[4] = temp[2];
            file[5] = temp[3];
            // name of the creating app
            temp = Convertir_Int_To_Endian((UInt32) 42069, 4);
            file[6] = temp[0];
            file[7] = temp[1];
            file[8] = temp[2];
            file[9] = temp[3];
            // offset
            temp = Convertir_Int_To_Endian((UInt32) headers,4);
            file[10] = temp[0];
            file[11] = temp[1];
            file[12] = temp[2];
            file[13] = temp[3];
            // DIB HEADER
            // sizeof this header
            temp = Convertir_Int_To_Endian((UInt32) 40, 4);
            file[14] = temp[0];
            file[15] = temp[1];
            file[16] = temp[2];
            file[17] = temp[3];
            // bitmap width
            temp = Convertir_Int_To_Endian((UInt32)width, 4);
            file[18] = temp[0];
            file[19] = temp[1];
            file[20] = temp[2];
            file[21] = temp[3];
            // bitmap height
            temp = Convertir_Int_To_Endian((UInt32) height, 4);
            file[22] = temp[0];
            file[23] = temp[1];
            file[24] = temp[2];
            file[25] = temp[3];
            // les color palnes (must be 1)
            temp = Convertir_Int_To_Endian((UInt32)1, 2);
            file[26] = temp[0];
            file[27] = temp[1];
            // bits perpxl
            temp = Convertir_Int_To_Endian((UInt32) 24, 2);
            file[28] = temp[0];
            file[29] = temp[1];
            // compression
            temp = Convertir_Int_To_Endian((UInt32)0, 4);
            file[30] = temp[0];
            file[31] = temp[1];
            file[32] = temp[2];
            file[33] = temp[3];
            // image size 
            temp = Convertir_Int_To_Endian((UInt32) imagesize * 3, 4);
            file[34] = temp[0];
            file[35] = temp[1];
            file[36] = temp[2];
            file[37] = temp[3];
            // horizontal resolution
            temp = Convertir_Int_To_Endian((UInt32) 1000, 4);
            file[38] = temp[0];
            file[39] = temp[1];
            file[40] = temp[2];
            file[41] = temp[3];
            // vertical resolution
            temp = Convertir_Int_To_Endian((UInt32) 1000, 4);
            file[42] = temp[0];
            file[43] = temp[1];
            file[44] = temp[2];
            file[45] = temp[3];
            //color palette 0 par default
            temp = Convertir_Int_To_Endian(0, 4);
            file[46] = temp[0];
            file[47] = temp[0];
            file[48] = temp[0];
            file[49] = temp[0];
            // n color importante 
            file[50] = temp[0];
            file[51] = temp[0];
            file[52] = temp[0];
            file[53] = temp[0];
            // now we unroll the image
            for(int j=0;j<height;j++){
                for(int i=0;i<multof4width;i+=3){
                    if(i/3<width){
                        temp[0] = (byte)data[i/3,j].R;
                        temp[1] = (byte)data[i/3,j].G;
                        temp[2] = (byte)data[i/3,j].B;
                    }else{
                        temp[0] = 0b0;
                        temp[1] = 0b0;
                        temp[2] = 0b0;
                    }
                    file[headers+i+j*multof4width] = temp[2];
                    file[headers+i+j*multof4width+1] = temp[1];
                    file[headers+i+j*multof4width+2] = temp[0];
                }
            }
            File.WriteAllBytes(pathName, file);
        }
        static byte[] Convertir_Int_To_Endian( UInt32 input, int nbits){
            byte[] res = new byte[4];
            for(int i=0;i<nbits;i++){
                res[i]=(byte)(input*Math.Pow(256,-1*i));
            }
            return res;
        }
        static int Convertir_Endian_To_Int(byte[] arr, int pos, int nbits){
            int res=0;
            for(int i=0;i<nbits;i++){
                res+=arr[i+pos]*(int)Math.Pow(256,i);
            }
            return res;
        }
        public MyImage rescale(int newwidth, int newheight){
            MyImage result = new MyImage(newwidth, newheight);
            for(int i=0;i<result.height;i++){
                for(int j=0;j<result.width;j++){
                    result.data[j,i] = data[(int)(((double)j/(double)newwidth)*width),(int)(((double)i/(double)newheight)*height)]; 
                }
            }
            return result;
        }
    }
}