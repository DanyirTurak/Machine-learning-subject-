using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2.Model
{
    public enum Shape
    {
        Line,
        Circle,
        Rectangle,
        Pencil,
        Eraser,
        Triangle,
    }
    public enum DrawTool
    {
        Pen,
        Brush
    }

    public class DrawerState
    {
        public Pen pen = new Pen(Color.Red);
        public Bitmap bmp;
        Graphics g;
        GraphicsPath path;
        private PictureBox pictureBox1;
        
        public Point prevPoint; 

        public DrawTool DrawTool { get; set; }
        public Shape Shape { get; set; }

        public void FixPath()
        {
            if (path != null)
            {
                g.DrawPath(pen, path);
                path = null;
            }
        }

        public DrawerState(PictureBox pictureBox1)
        {
            this.pictureBox1 = pictureBox1;
            
            Load("");

            DrawTool = DrawTool.Pen;
            Shape = Shape.Pencil;

            pictureBox1.Paint += PictureBox1_Paint;
            //pictureBox1.MouseMove += PictureBox_MouseMove;
            //pictureBox1.MouseDown += PictureBox_MouseDown;
            //pictureBox1.MouseUp += PictureBox_MouseUp;
            

        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (path != null)
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        //private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        //{ 
        //    if (e.Button == MouseButtons.Left) 
        //    { 
        //        Draw(e.Location); 
        //    }                
        // }


        //private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        //{
        //    prevPoint = e.Location;
        //}


        //private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if(path != null)
        //    {
        //        g.DrawPath(pen, path);
        //        path = null;
        //    }
        //}

        public void Draw(Point currentPoint) 
        {
            switch (Shape)
            {
                case Shape.Line:
                    path = new GraphicsPath();
                    path.AddLine(prevPoint, currentPoint);
                    break;
                case Shape.Circle:
                    path = new GraphicsPath();
                    path.AddEllipse(prevPoint.X, prevPoint.Y, currentPoint.X - prevPoint.X, 
                                      currentPoint.Y - prevPoint.Y);

                    break;
                case Shape.Rectangle:
                    path = new GraphicsPath();
                    Rectangle rect = new Rectangle(prevPoint.X, prevPoint.Y, currentPoint.X - prevPoint.X,
                                                    currentPoint.Y - prevPoint.Y);
                    path.AddRectangle(rect);

                    break;
                case Shape.Pencil:
                    g.DrawLine(pen, prevPoint, currentPoint);
                    prevPoint = currentPoint;
                    break;
                case Shape.Eraser:
                    g.DrawLine(new Pen(Color.White,pen.Width), prevPoint, currentPoint);
                    break;
                case Shape.Triangle:
                    Point A= new Point(((currentPoint.X - prevPoint.X) / 2 + prevPoint.X), prevPoint.Y);
                    Point B= new Point(currentPoint.X, currentPoint.Y);
                    Point C= new Point(prevPoint.X, currentPoint.Y);
                    PointF[] arrtriangle = { A, B, C};
                    path = new GraphicsPath();
                    path.AddPolygon(arrtriangle);
                    break;
                
                default:
                    break;
            }

            pictureBox1.Refresh();
        }

        public void Save(string fileName)
        {
            bmp.Save(fileName);
        }

        public void Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            }
            else {
                bmp = new Bitmap(fileName);
            }

            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
        }
    }
}
