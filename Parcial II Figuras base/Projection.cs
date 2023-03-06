using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_II_Figuras_base
{
    public class Projection
    {
        static int w, h;

        public int W
        {
            get
            {
                return w;
            }
            set
            {
                w = value;
            }
        }

        public int H
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
            }
        }
        public Projection(int w, int h)
        {
            W = w;
            H = h;
        }
        public static PointF Scale(PointF p, int S)
        {
            p.X = p.X * S;
            p.Y = p.Y * S;
            return p;
        }
        public static PointF TranslateToO(PointF p)
        {
            p.X = p.X + w / 2;
            p.Y = p.Y + h / 2;
            return p;
        }

        public static PointF ProjectionO(Vertex p, int t)
        {
            PointF pp = new PointF();
            float[,] np = new float[2, 1];
            float[,] mat = new float[3, 1]
            {
                {p.X},
                {p.Y},
                {p.Z},
             };
            for (int i = 0; i < 2; i++)
            {
                float sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum += Util.ProjO[i, j] * mat[j, 0];
                }
                np[i, 0] = sum;
            }
            pp.X = np[0, 0];
            pp.Y = np[1, 0];
            pp = Scale(pp, t);
            pp = TranslateToO(pp);
            return pp;
        }
        public static PointF ProjectionP(Vertex p, int t)
        {
            Util.Z = p.Z;
            PointF pp = new PointF();
            float[,] np = new float[2, 1];
            float[,] mat = new float[3, 1]
            {
                {p.X},
                {p.Y},
                {p.Z},
             };
            for (int i = 0; i < 2; i++)
            {
                float sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum += Util.ProjP[i, j] * mat[j, 0];
                }
                np[i, 0] = sum;
            }
            pp.X = np[0, 0];
            pp.Y = np[1, 0];
            pp = Scale(pp, t);
            pp = TranslateToO(pp);
            return pp;
        }
    }
}
