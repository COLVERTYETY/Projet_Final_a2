using System;
using System.IO;

namespace complet
{
    public class MyImage
    {
        public pixel[,] data;

        public int height{
            get{return data.GetLength(1);}
        }
        public int width{
            get{return data.GetLength(0);}
        }
        public MyImage(byte[] arr){
            if(arr[0]!='B' && arr[1] != 'M'){
                throw new Exception("ce fichier ne commence pas par BM");
            }
            int start = Convertir_Endian_To_Int(arr, 10, 4);
            int numberofbitperpxl = Convertir_Endian_To_Int(arr, 28, 2);
            if(numberofbitperpxl!=24){
                throw new Exception("seul les fichiers 24 bits sont supporté");
            }
            int imagesize = Convertir_Endian_To_Int(arr, 34, 4);
            int _width = Convertir_Endian_To_Int(arr, 18, 4);
            int multof4width = 4*(((_width*(numberofbitperpxl/8))+3)/4);
            data = new pixel[_width,( imagesize )/ multof4width];
            for( int i = start;i<(start+imagesize);i+=multof4width){
                for(int j=i;j<i+(width*numberofbitperpxl/8);j+=numberofbitperpxl/8){
                    data[(j-i)/(numberofbitperpxl/8),(i-start)/multof4width] = new pixel(arr[j+2],arr[j+1],arr[j]);
                }
            }
        }
        public MyImage(int _width, int _height){  //creates a blank image
            data = new pixel[_width,_height];
        }
        public MyImage(pixel[,] content){
            data = content;
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
        public void fill(pixel p){
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    data[j,i] = p;
                }
            }
        }
        public void dispwithcolor(){
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    Console.ForegroundColor = colormachine.closestmatch(data[j,i]);
                    Console.Write( Convert.ToString((int)data[j,i].avg).PadLeft(4));
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        public void  From_Image_To_File(string pathName){
            // ! palettes not taken in account 
            const int headers = 54;
            int multof4width = 4*(((width*(3))+3)/4);
            int imagesize = multof4width * height + 2;
            int totalsize = headers + imagesize;  
            byte[] file= new byte[totalsize];
            // BM
            file[0] = (byte)'B';
            file[1] = (byte)'M';
            // tottalsize
            Convertir_Int_To_Endian((UInt32) totalsize, 4,file,2);
            // name of the creating app
            Convertir_Int_To_Endian((UInt32) 42069, 4,file,6);
            // offset
            Convertir_Int_To_Endian((UInt32) headers,4,file,10);
            // DIB HEADER
            // sizeof this header
            Convertir_Int_To_Endian((UInt32) 40, 4,file,14);
            // bitmap width
            Convertir_Int_To_Endian((UInt32)width, 4,file,18);
            // bitmap height
            Convertir_Int_To_Endian((UInt32) height, 4,file,22);
            // les color palnes (must be 1)
            Convertir_Int_To_Endian((UInt32)1, 2,file, 26);
            // bits perpxl
            Convertir_Int_To_Endian((UInt32) 24, 2,file,28);
            // compression
            Convertir_Int_To_Endian((UInt32)0, 4,file,30);
            // image size 
            Convertir_Int_To_Endian((UInt32) imagesize * 3, 4,file,34);
            // horizontal resolution
            Convertir_Int_To_Endian((UInt32) 1000, 4,file,38);
            // vertical resolution
            Convertir_Int_To_Endian((UInt32) 1000, 4,file,42);
            //color palette 0 par default
            Convertir_Int_To_Endian(0, 4,file,46);
            // n color importante 
            Convertir_Int_To_Endian(0, 4,file,50);
            // now we unroll the image
            byte[] temp = new byte[3];  
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
        public MyImage greyscale(){
            MyImage result = new MyImage(width, height);
            for(int i=0;i<result.height;i++){
                for(int j=0;j<result.width;j++){
                    result.data[j,i] = new pixel(data[j,i].avg,data[j,i].avg,data[j,i].avg); 
                }
            }
            return result;
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
        public bool inboundaries(Point a){
            return ((int)a.x>0) && ((int)a.x<width) && ((int)a.y>0) && ((int)a.y<height); 
        }
        public MyImage rotate(double degrees) {
            double theta=degrees*Math.PI/180.0;
            //calculating coordonates of corners in new image
            Point ocenter = new Point(this.width/2, this.height/2);
            Point[] points = new Point[4];
            points[0] = new Point(0, 0) - ocenter;
            points[1] = new Point (this.width, 0) - ocenter;
            points[2] = new Point(0, this.height) - ocenter;
            points[3] = new Point(this.width, this.height) - ocenter;

            //polar coordinates of corners + rotation
            for(int i=0;i<points.Length;i++){
                points[i] = Point.PolToCart(points[i].R,points[i].Theta+theta);
            }
            double ymax=0;
            double ymin=99999;
            double xmax=0;
            double xmin=99999;
            foreach( Point i in points){
                if(i.y>ymax){
                    ymax = i.y;
                }
                else if( ymin>i.y){
                    ymin = i.y;
                }
                if(i.x>xmax){
                    xmax = i.x;
                }
                else if( xmin>i.x){
                    xmin = i.x;
                }
            }
            // create blank image
            MyImage result = new MyImage((int)(xmax-xmin),(int)(ymax-ymin));
            // iterate threw new image and find coord of each pixel
            Point temp;
            pixel filler = new pixel(190,190,190);
            Point center = new Point(result.width/2,result.height/2);
            for(int i=0;i<result.height;i++){
                for(int j=0;j<result.width;j++){
                    // get a point
                    temp = new Point(j,i) - center;
                    // find where he was
                    temp = Point.PolToCart(temp.R, temp.Theta - theta) + ocenter;                  // if this poiçnt is valid
                    if(inboundaries(temp)){
                        result.data[j,i] = data[(int)temp.x,(int)temp.y];
                    }
                    else{
                        result.data[j,i] = filler;//new pixel((byte)((j*10)%255),(byte)((i*10)%255),(byte)((j*10)%255));
                    }
                }
            }
            return result;
        }
        
        public bool IsKernel()
        {
            if (this.height != this.width || this.height % 2 == 0) return false;
            foreach (pixel p in this.data) if (p.Nbits < 1) return false;
            return true;
        }

        public MyImage Convo (MyImage kernel)
        {
            MyImage res = this;
            if (kernel.IsKernel())
            {
                res = new MyImage(this.width - kernel.height-1, this.height - kernel.height-1);
                for (int i = kernel.height/2; i < res.height + kernel.height/2; i++) {
                    for (int j = kernel.height/2; j < res.width + kernel.height/2; j++) {
                        double[] temp = {0, 0, 0};
                        for (int x = -kernel.height/2; x < kernel.height/2; x++) {
                            for (int y = -kernel.height/2; y < kernel.height/2; y++) {
                                temp[0] += (double)(this.data[j+x, i+y].R*kernel.data[x+kernel.height/2, y+kernel.height/2].R);
                                temp[1] += (double)(this.data[j+x, i+y].G*kernel.data[x+kernel.height/2, y+kernel.height/2].R);
                                temp[2] += (double)(this.data[j+x, i+y].B*kernel.data[x+kernel.height/2, y+kernel.height/2].R);
                            }
                        }
                        temp[0] /= kernel.height * kernel.width;
                        temp[1] /= kernel.height * kernel.width;
                        temp[2] /= kernel.height * kernel.width;
                        res.data[j-kernel.height/2, i-kernel.height/2] = new pixel(temp);
                    }
                }
            }
            return res;
        }
        public MyImage Convo (MyImage kernel, int y0,int y1)
        {
            MyImage res = this;
            if (kernel.IsKernel())
            {
                int starty=0;
                int endy=0;
                if(y0 > kernel.height/2){
                    starty = y0;
                }else{
                    starty = kernel.height/2;
                }
                if(y1 > this.height - kernel.height/2){
                    endy = this.height - kernel.height/2;
                }else{
                    endy = y1;
                }
                res = new MyImage(this.width,y1-y0);
                res.fill(new pixel(0,0,0));
                //iterate threw the original image
                for(int i=starty;i<endy;i++){
                    for(int j=kernel.width/2;j<this.width-kernel.width/2;j++)
                    {
                        pixel temp = new pixel(0,0,0);
                        //iterate threw the kernel
                        for(int y=0;y<kernel.height;y++){
                            for(int x=0;x<kernel.width;x++){
                                //calcultae new pixel values
                                temp +=this.data[j+y-kernel.height/2 ,i+x-kernel.width/2]*kernel.data[x,y].avg;
                            }
                        }
                        //put this new value in the resulting image
                        res.data[j,i-starty] = temp;
                    }
                }
                
            }
            return res;
        }
        public void flattenkernel(){
            pixel temp= new pixel(0,0,0);
            //calculate sum
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    temp+= this.data[j,i];
                }
            }
            //smoothen
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    this.data[j,i]/=temp;
                }
            }
            
        }
        public MyImage hsvShift(pixel shift){
            MyImage res = new MyImage(data);
            for(int i=0;i<res.height;i++){
                for(int j=0;j<res.width;j++){
                    res.data[j,i].hsv = data[j,i].hsv + shift;
                }
            }
            return res;
        }
        static int closestIndex(pixel p, pixel[] totest){
            int res = 0;
            double smallest = 99999999;
            pixel dist;
            for(int i=0;i<totest.Length;i++){
                dist = p-totest[i];
                if(dist<smallest){
                    smallest = dist.Norm;
                    res = i;
                }
            }
            return res;
        }
        public MyImage fromclosest(pixel[] values){
            MyImage res = new MyImage(width,height);
            for(int i=0;i<height;i++){
                    for(int j=0;j<width;j++){
                        res.data[j,i] = values[closestIndex(data[j,i],values)];
                    }
                }
            return res;
        }
        public pixel[] Kmeans(int k, int depth){
            int temp = depth;
            loading loader = new loading(Console.CursorTop);
            loader.header = "kmeans: "+Convert.ToString(k)+" |";
            loader.fit();
            //initialisation des points de refs
            pixel[] res = new pixel[k];
            pixel[] newres = new pixel[k];
            int[] resnumber = new int[k];
            Random rnd = new Random();
            for(int i=0;i<res.Length;i++){
                res[i] = data[rnd.Next(width),rnd.Next(height)];
                newres[i] = new pixel(0,0,0);
                resnumber[i] = 0;
            }
            int stock = 0;
            //tant que encore le droit de calculer
            while (depth>0){
                depth--;
                loader.step(((double)temp-(double)depth)/(double)temp);
                //affecter a chaque point la valeur la plus proche
                for(int i=0;i<height;i++){
                    for(int j=0;j<width;j++){
                        stock = closestIndex(data[j,i],res);
                        newres[stock] += data[j,i];
                        resnumber[stock] +=1;
                    }
                }
                //faie moyennes de centres et reinitialiser les centres des clusters
                for(int i=0;i<newres.Length;i++){
                    res[i] = newres[i]/(double)resnumber[i];
                    newres[i] = new pixel(0,0,0);
                    resnumber[i] = 0;
                }
            }
            return res;
        }
        static byte[] Convertir_Int_To_Endian( UInt32 input, int nbits){
            byte[] res = new byte[4];
            for(int i=0;i<nbits;i++){
                res[i]=(byte)(input*Math.Pow(256,-1*i));
            }
            return res;
        }
        static void Convertir_Int_To_Endian( UInt32 input, int nbits, byte[] destination, int offset){
            for(int i=0;i<nbits;i++){
                destination[i+offset]=(byte)(input*Math.Pow(256,-1*i));
            }
        }
        static int Convertir_Endian_To_Int(byte[] arr, int pos, int nbits){
            int res=0;
            for(int i=0;i<nbits;i++){
                res+=arr[i+pos]*(int)Math.Pow(256,i);
            }
            return res;
        }
    }
}