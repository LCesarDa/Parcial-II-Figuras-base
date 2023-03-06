using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_II_Figuras_base
{
    public class Canvas
    {
        public Bitmap bitmap;
        public float Width, Height;
        public byte[] bits;
        Graphics g;
        int pixelFormatSize, stride;
        
        public Canvas(Size size)
        {
            Init(size.Width, size.Height);
        }

        public void Init(int width, int height)
        {
            PixelFormat format;
            GCHandle handle;
            IntPtr bitPtr;
            int padding;

            format = PixelFormat.Format32bppArgb;
            Width = width;
            Height = height;
            pixelFormatSize = Image.GetPixelFormatSize(format) / 8;
            stride = width * pixelFormatSize;
            padding = (stride % 4);
            stride += padding == 0 ? 0 : 4 - padding;
            bits = new byte[stride * height];
            handle = GCHandle.Alloc(bits, GCHandleType.Pinned);
            bitPtr = Marshal.UnsafeAddrOfPinnedArrayElement(bits, 0);
            bitmap = new Bitmap(width, height, stride, format, bitPtr);

            g = Graphics.FromImage(bitmap);
        }
        public List<float> Interpolate(int i0, int d0, int i1, int d1)
        {
            List<float> values = new List<float>();
            if (i0 == i1)
            {
                values.Add(d0);
                return values;
            }
            float a = ((float)d1 - (float)d0) / ((float)i1 - (float)i0);
            float d = d0;
            for (int i = i0; i <= i1; i++)
            {
                values.Add(d);
                d = d + a;
            }
            return values;
        }

        public List<float> Interpolate(float i0, float d0, float i1, float d1)
        {
            List<float> values = new List<float>();
            if (i0 == i1)
            {
                values.Add(d0);
                return values;
            }
            float a = ((float)d1 - (float)d0) / ((float)i1 - (float)i0);
            float d = d0;
            for (int i = (int)i0; i <= i1; i++)
            {
                values.Add(d);
                d = d + a;
            }
            return values;
        }

        public void DrawPixel(int x, int y, Color c)
        {
            int res = (int)((x * pixelFormatSize) + (y * stride));

            bits[res + 0] = c.B;
            bits[res + 1] = c.G;
            bits[res + 2] = c.R;
            bits[res + 3] = c.A;
        }

        public void FastClear()
        {
            unsafe
            {
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* bits = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        bits[x + 0] = 0;
                        bits[x + 1] = 0;
                        bits[x + 2] = 0;
                        bits[x + 3] = 0;
                    }
                });
                bitmap.UnlockBits(bitmapData);
            }
        }

        public void DrawLine(Point p0, Point p1, Color c)
        {
            if (Math.Abs(p1.X - p0.X) > Math.Abs(p1.Y - p0.Y))
            {
                if (p0.X > p1.X)
                {
                    Point p = p0;
                    p0 = p1;
                    p1 = p;
                }
                List<float> ys = Interpolate(p0.X, p0.Y, p1.X, p1.Y);
                for (int i = p0.X; i <= p1.X; i++)
                {
                    DrawPixel(i, (int)ys[i - p0.X], c);
                }
            }
            else
            {
                if (p0.Y > p1.Y)
                {
                    Point p = p0;
                    p0 = p1;
                    p1 = p;
                }
                List<float> xs = Interpolate(p0.Y, p0.X, p1.Y, p1.X);
                for (int i = p0.Y; i <= p1.Y; i++)
                {
                    DrawPixel((int)xs[i - p0.Y], i, c);
                }
            }
        }

        public void DrawWireFrameTriangle(PointF p0, PointF p1, PointF p2, Color color)
        {
            Point a = new Point(), b = new Point(), c = new Point();
            a.X = (int)p0.X;
            a.Y = (int)p0.Y;
            b.X = (int)p1.X;
            b.Y = (int)p1.Y;
            c.X = (int)p2.X;
            c.Y = (int)p2.Y;

            DrawLine(a, b, color);
            DrawLine(b, c, color);
            DrawLine(c, a, color);
        }

        public void DrawTriangle(PointF a, PointF b, PointF d, Color c, float h)
        {
            Point p0 = new Point(), p1 = new Point(), p2 = new Point();
            p0.X = (int)a.X;
            p0.Y = (int)a.Y;
            p1.X = (int)b.X;
            p1.Y = (int)b.Y;
            p2.X = (int)d.X;
            p2.Y = (int)d.Y;

            List<float> x_left;
            List<float> x_right;

            if (p1.Y < p0.Y)
            {
                Point p = p0;
                p0 = p1;
                p1 = p;
            }
            if (p2.Y < p0.Y)
            {
                Point p = p0;
                p0 = p2;
                p2 = p;
            }
            if (p2.Y < p1.Y)
            {
                Point p = p2;
                p2 = p1;
                p1 = p;
            }

            List<float> x01 = Interpolate(p0.Y, p0.X, p1.Y, p1.X);
            List<float> x12 = Interpolate(p1.Y, p1.X, p2.Y, p2.X);
            List<float> x02 = Interpolate(p0.Y, p0.X, p2.Y, p2.X);

            x01.RemoveAt(x01.Count - 1);
            List<float> x012 = new List<float>();
            x012.AddRange(x01);
            x012.AddRange(x12);

            int m = x02.Count / 2;
            if (x02[m] < x012[m])
            {
                x_left = x02;
                x_right = x012;
            }
            else
            {
                x_left = x012;
                x_right = x02;
            }

            for (int y = p0.Y; y < p2.Y; y++)
            {
                for (float x = x_left[y - p0.Y]; x < x_right[y - p0.Y]; x++)
                {
                    Color shaded_color = Color.FromArgb((int)(c.R * h), (int)(c.G * h), (int)(c.B * h));
                    DrawPixel((int)x, y, shaded_color);
                }
            }
        }

        public void DrawShadedTriangle(PointF a, PointF b, PointF d, Color c)
        {
            Point p0 = new Point(), p1 = new Point(), p2 = new Point();
            p0.X = (int)a.X;
            p0.Y = (int)a.Y;
            p1.X = (int)b.X;
            p1.Y = (int)b.Y;
            p2.X = (int)d.X;
            p2.Y = (int)d.Y;

            List<float> x_left;
            List<float> x_right;
            List<float> h_left;
            List<float> h_right;

            if (p1.Y < p0.Y)
            {
                Point p = p0;
                p0 = p1;
                p1 = p;
            }
            if (p2.Y < p0.Y)
            {
                Point p = p0;
                p0 = p2;
                p2 = p;
            }
            if (p2.Y < p1.Y)
            {
                Point p = p2;
                p2 = p1;
                p1 = p;
            }

            List<float> x01 = Interpolate(p0.Y, p0.X, p1.Y, p1.X);
            List<float> x12 = Interpolate(p1.Y, p1.X, p2.Y, p2.X);
            List<float> x02 = Interpolate(p0.Y, p0.X, p2.Y, p2.X);

            List<float> h01 = Interpolate(p0.Y, 1, p1.Y, 0);
            List<float> h12 = Interpolate(p1.Y, 0, p2.Y, 0);
            List<float> h02 = Interpolate(p0.Y, 1, p2.Y, 0);

            x01.RemoveAt(x01.Count - 1);
            List<float> x012 = new List<float>();
            x012.AddRange(x01);
            x012.AddRange(x12);

            h01.RemoveAt(h01.Count - 1);
            List<float> h012 = new List<float>();
            h012.AddRange(h01);
            h012.AddRange(h12);

            int m = x02.Count / 2;
            if (x02[m] < x012[m])
            {
                x_left = x02;
                x_right = x012;

                h_left = h02;
                h_right = h012;
            }
            else
            {
                x_left = x012;
                x_right = x02;

                h_left = h012;
                h_right = h02;
            }

            for (int y = p0.Y; y < p2.Y; y++)
            {
                float x_l = x_left[y - p0.Y];
                float x_r = x_right[y - p0.Y];
                List<float> h_segment = Interpolate(x_l, h_left[y - p0.Y], x_r, h_right[y - p0.Y]);
                for (float x = x_left[y - p0.Y]; x < x_right[y - p0.Y]; x++)
                {
                    Color shaded_color = Color.FromArgb((int)(c.R * h_segment[(int)(x - x_l)]), (int)(c.G * h_segment[(int)(x - x_l)]), (int)(c.B * h_segment[(int)(x - x_l)]));
                    DrawPixel((int)x, y, shaded_color);
                }
            }

        }

        public void DrawAxis(int w, int h)
        {
            Point wi = new Point(w / 2, h - h + 1);
            Point wf = new Point(w / 2, h - 1);
            Point hi = new Point(w - w + 1, h / 2);
            Point hf = new Point(w - 1, h / 2);
            DrawLine(wi, wf, Color.Yellow);
            DrawLine(hi, hf, Color.Yellow);
        }

        public void RenderLast (Scene scene, int t, bool f, Color c) 
        {
            for (int i = 0; i < scene.Figures[scene.Figures.Count-1].triangles.Count; i++)
            {
                Figure T = new Figure();
                T.triangles = scene.Figures[scene.Figures.Count - 1].triangles.OrderBy(x => x.cam).ToList();
                if (T.triangles[i].cam > 0)
                {
                    if (f) DrawTriangle(T.triangles[i].TriProjP(t)[0], T.triangles[i].TriProjP(t)[1], T.triangles[i].TriProjP(t)[2], c, T.triangles[i].h);
                    DrawWireFrameTriangle(T.triangles[i].TriProjP(t)[0], T.triangles[i].TriProjP(t)[1], T.triangles[i].TriProjP(t)[2], Color.Black);
                }
            }
        }
    }
}
