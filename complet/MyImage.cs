using System;
using System.Text;
using System.IO;

namespace complet
{
    public class MyImage
    {
        public pixel[][] data;// why not [,] ? because jagged arrays have faster read spee when used properly

        public int height{
            get{return data.Length;}
        }
        public int width{
            get{return data[0].Length;}
        }
        public MyImage(string path){
            byte [] arr = File.ReadAllBytes(path);
            if(arr[0]!='B' && arr[1] != 'M'){
                throw new Exception("ce fichier ne commence pas par BM");
            }
            int start = Convertir_Endian_To_Int(arr, 10, 4);
            int numberofbitperpxl = Convertir_Endian_To_Int(arr, 28, 2);
            if(numberofbitperpxl!=24){
                throw new Exception("seul les fichiers 24 bits sont supportés");
            }
            int _width = Convertir_Endian_To_Int(arr, 18, 4);
            int multof4width = 4*(((_width*(numberofbitperpxl/8))+3)/4);
            int imagesize = Convertir_Endian_To_Int(arr, 22, 4) * multof4width;
            //initialise the contents of data as empty
            data = new pixel[( imagesize )/ multof4width][];
            for(int i=0;i<data.Length;i++){
                data[i] = new pixel[_width];
            }
            for( int i = start;i<(start+imagesize);i+=multof4width){
                for(int j=i;j<i+(width*numberofbitperpxl/8);j+=numberofbitperpxl/8){
                    data[(i-start)/multof4width][(j-i)/(numberofbitperpxl/8)] = new pixel(arr[j+2],arr[j+1],arr[j]);
                }
            }
        }
        public MyImage(int _width, int _height){  //creates a blank image
            data = new pixel[_height][];
            for(int i=0;i<_height;i++){
                data[i] = new pixel[_width];
            }
        }
        public MyImage(pixel[][] content){
            data = new pixel[content.Length][];
            int temp=0;
            if(content.Length>0){
                temp = content[0].Length;
            }
            for(int i=0;i<data.Length;i++){
                data[i] = new pixel[temp];
                for(int j=0;j<temp;j++){
                    data[i][j] = content[i][j];
                }
            }
        }
        public MyImage(double[,] content){
            data = new pixel[content.GetLength(0)][];
            for(int i=0;i<content.GetLength(0);i++){
                data[i] = new pixel[content.GetLength(1)];
                for(int j=0;j<content.GetLength(1);j++){
                    data[i][j] = new pixel(content[i,j]);
                }
            }
        }
        public MyImage(int[,] content){
            data = new pixel[content.GetLength(0)][];
            for(int i=0;i<content.GetLength(0);i++){
                data[i] = new pixel[content.GetLength(1)];
                for(int j=0;j<content.GetLength(1);j++){
                    data[i][j] = new pixel(255-255*content[i,j]);
                }
            }
        }
        public override string ToString()
        {
            StringBuilder temp = new StringBuilder(" ");
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    temp.Append(Convert.ToString((int)data[i][j].avg).PadLeft(4));
                }
                temp.Append("\n");
            }
            return Convert.ToString(temp);
        }
        public void fill(pixel p){
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    data[i][j] = p;
                }
            }
        }
        public void dispwithcolor(){
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    Console.ForegroundColor = colormachine.closestmatch(data[i][j]);
                    Console.Write( Convert.ToString((int)data[i][j].avg).PadLeft(4));
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
                    temp[0] = (byte)data[j][i/3].R;
                    temp[1] = (byte)data[j][i/3].G;
                    temp[2] = (byte)data[j][i/3].B;
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
        public static MyImage magnitudefuse(MyImage a, MyImage b){
            MyImage res = new MyImage(a.width,a.height);
            for(int i=0;i<res.height;i++){
                for(int j=0;j<res.width;j++){
                    pixel c = (a.data[i][j]*a.data[i][j])+(b.data[i][j]*b.data[i][j]);
                    res.data[i][j] = new pixel(Math.Sqrt(c.r),Math.Sqrt(c.g),Math.Sqrt(c.b));
                }
            }
            return res;
        }
        public MyImage greyscale(){
            MyImage result = new MyImage(width, height);
            for(int i=0;i<result.height;i++){
                for(int j=0;j<result.width;j++){
                    result.data[i][j] = new pixel(data[i][j].avg,data[i][j].avg,data[i][j].avg); 
                }
            }
            return result;
        }
        public MyImage rescale(int newwidth, int newheight){
            MyImage result = new MyImage(newwidth, newheight);
            for(int i=0;i<result.height;i++){
                for(int j=0;j<result.width;j++){
                    result.data[i][j] = data[(int)(((double)i/(double)newheight)*height)][(int)(((double)j/(double)newwidth)*width)]; 
                }
            }
            return result;
        }

        public MyImage mirror(bool vertical = true) {
            MyImage res = new MyImage(this.width, this.height);
            for (int i = 0 ; i < res.height ; i++) {
                for (int j = 0 ; j < res.width ; j++) {
                    res.data[i][j] = vertical ? this.data[i][this.width - 1 - j] : this.data[this.height -1 - i][j];
                }
            }
            return res;
        }

        public bool inboundaries(Point a){
            return ((int)a.x>0) && ((int)a.x<width) && ((int)a.y>0) && ((int)a.y<height); 
        }
        public bool inboundaries(double x, double y){
            return ((int)x>0) && ((int)x<width) && ((int)y>0) && ((int)y<height); 
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
                        result.data[i][j] = data[(int)temp.y][(int)temp.x];
                    }
                    else{
                        result.data[i][j] = filler;
                    }
                }
            }
            return result;
        }
        
        public bool IsKernel()
        {
            if (this.height != this.width || this.height % 2 == 0) return false;
            // foreach (pixel p in this.data) if (p.Nbits < 1) return false; //! this should be deleted but haven done it in respect for antoine tete
            return true;
        }

        public MyImage convo(MyImage kernel, int y0,int y1)
        {
            MyImage res = this;
            if (kernel.IsKernel())
            {
                //image stuff
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
                res = new MyImage(this.width-kernel.width,endy-starty);
                //iterate threw the original image
                pixel sum;
                for(int i=starty;i<endy;i++){
                    for(int j=0;j<res.width;j++){
                        sum = new pixel(0,0,0);
                        //iterate threw the kernel
                        for(int y=0;y<kernel.height;y++){
                            for(int x=0;x<kernel.width;x++){
                                //calcultae new pixel values
                                sum += data[i+y-kernel.height/2][j+x]*kernel.data[y][x];
                                //?Console.WriteLine(data[i+y-kernel.height/2][j+x]);
                            }
                        }
                        //put this new value in the resulting image
                        res.data[i-starty][j] = sum;
                    }
                }
                
            }
            return res;
        }
        public MyImage convo(MyImage kernel)
        {
            MyImage res = this;
            if (kernel.IsKernel())
            {
                //image stuff
                int starty=0;
                int endy=0;
                starty = kernel.height/2;
                endy = this.height - kernel.height/2;
                res = new MyImage(this.width-kernel.width,endy-starty);
                //iterate threw the original image
                pixel sum;
                for(int i=starty;i<endy;i++){
                    for(int j=0;j<res.width;j++){
                        sum = new pixel(0,0,0);
                        //iterate threw the kernel
                        for(int y=0;y<kernel.height;y++){
                            for(int x=0;x<kernel.width;x++){
                                //calcultae new pixel values
                                sum += data[i+y-kernel.height/2][j+x]*kernel.data[y][x];
                                //?Console.WriteLine(data[i+y-kernel.height/2][j+x]);
                            }
                        }
                        //put this new value in the resulting image
                        res.data[i-starty][j] = sum;
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
                    temp+= data[i][j];
                }
            }
            //smoothen
            pixel inverted = new pixel(1)/temp;
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    data[i][j]*=inverted;
                }
            }
            
        }
        public MyImage hsvShift(pixel shift){
            MyImage res = new MyImage(data);
            for(int i=0;i<res.height;i++){
                for(int j=0;j<res.width;j++){
                    res.data[i][j].hsv = data[i][j].hsv + shift;
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
                    res.data[i][j] = values[closestIndex(data[i][j],values)];
                }
            }
            return res;
        }
        public MyImage polarise(double val){
            pixel noir = new pixel(0);
            pixel blanc = new pixel(255);
            pixel refer = new pixel(val);
            MyImage res = new MyImage(width,height);
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    if(data[i][j]>refer){
                        res.data[i][j] = blanc;
                    }else{
                        res.data[i][j] = noir;
                    }
                }
            }
            return res;
        }
        public MyImage noiretblanc(){
            pixel[] arr = new pixel[]{new pixel(0,0,0),new pixel(255,255,255)};
            return this.fromclosest(arr);
        }
        public static double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
        public MyImage Mandelbrot(double X0,double Y0,double X1,double Y1){
            MyImage res = new MyImage(width,height);
            double x2 =0;
            double y2 =0;
            double x0 =0;
            double y0 =0;
            double x =0;
            double y =0;
            int it=0;
            const int maxit=1000;
            for(int i=0;i<height;i++){
                for(int j=0;j<width;j++){
                    x0 = Map(j,0,width,X0,X1);
                    y0 = Map(i,0,height,Y0,Y1);
                    x=0;
                    y=0;
                    x2=0;
                    y2=0;
                    it=0;
                    while((it<maxit)&&(x2+y2<=4)){
                        y = 2*x*y+y0;
                        x = x2 - y2 +x0;
                        x2 = x*x;
                        y2 = y*y;
                        it++;
                    }
                    res.data[i][j].hsv=new pixel(it*360/maxit,1,1);
                }
            }
            return res;
        }
        public void blit(MyImage other, int x,int y){
            int newx;
            int newy;
            for(int i=0;i<other.height;i++){
                for(int j=0;j<other.width;j++){
                    newx = j+x;
                    newy = i+y;
                    if(inboundaries(newx,newy)){
                        data[newy][newx] = other.data[i][j];
                    }
                }
            }
        }
        public void blit(MyImage other, int x,int y, pixel key){
            int newx;
            int newy;
            for(int i=0;i<other.height;i++){
                for(int j=0;j<other.width;j++){
                    newx = j+x;
                    newy = i+y;
                    if(inboundaries(newx,newy)){
                        if(other.data[i][j]!=key){
                            data[newy][newx] = other.data[i][j];
                        }
                        
                    }
                }
            }
        }

        public MyImage hidden(MyImage hidden) {
            MyImage res = new MyImage(this.data);
            if (this.height != hidden.height || this.width != hidden.width) {
                double factor = (Math.Abs(this.height - hidden.height) > Math.Abs(this.width - hidden.width)) ? (double)hidden.height / (double)this.height : (double)hidden.width / (double)this.width;
                res = res.rescale((int)(res.width * factor), (int)(res.height * factor));
            }
            for(int i = 0; i < res.height; i++) {
                for(int j = 0; j < res.width; j++) {
                    if (!(i < (res.height-hidden.height)/2 || i >= res.height-(res.height-hidden.height)/2 || j < (res.width-hidden.width)/2 || j >= res.width-(res.width-hidden.width)/2)) {
                        res.data[i][j].R = (byte)((res.data[i][j].R >> 4 << 4) + (hidden.data[i-(res.height-hidden.height)/2][j-(res.width-hidden.width)/2].R >> 4));
                        res.data[i][j].G = (byte)((res.data[i][j].G >> 4 << 4) + (hidden.data[i-(res.height-hidden.height)/2][j-(res.width-hidden.width)/2].G >> 4));
                        res.data[i][j].B = (byte)((res.data[i][j].B >> 4 << 4) + (hidden.data[i-(res.height-hidden.height)/2][j-(res.width-hidden.width)/2].B >> 4));
                    }
                    else {
                        res.data[i][j].R = (byte)((res.data[i][j].R >> 4 << 4));
                        res.data[i][j].G = (byte)((res.data[i][j].G >> 4 << 4));
                        res.data[i][j].B = (byte)((res.data[i][j].B >> 4 << 4));
                    }
                }
            }
            return res;
        }

        public MyImage findHidden() {
            MyImage res = new MyImage(this.width, this.height);
            for(int i = 0; i < this.height; i++) {
                for(int j = 0; j < this.width; j++) {
                    res.data[i][j].R = (byte)((this.data[i][j].R & 0x0F) << 4);
                    res.data[i][j].G = (byte)((this.data[i][j].G & 0x0F) << 4);
                    res.data[i][j].B = (byte)((this.data[i][j].B & 0x0F) << 4);
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
                res[i] = data[rnd.Next(height)][rnd.Next(width)];
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
                        stock = closestIndex(data[i][j],res);
                        newres[stock] += data[i][j];
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