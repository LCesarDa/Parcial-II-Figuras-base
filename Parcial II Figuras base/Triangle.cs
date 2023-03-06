using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_II_Figuras_base
{
    public class Triangle
    {
        public Vertex[] Points;
        public Vertex Centroid, Normal;
        public float n, z, cam, h;

        public Triangle(Vertex a, Vertex b, Vertex c)
        {
            Centroid = new Vertex();
            Normal = new Vertex();
            Vertex N = new Vertex();
            Points = new Vertex[3];
            Points[0] = a;
            Points[1] = b;
            Points[2] = c;

            for (int p = 0; p < 3; p++)
            {
                Centroid.X += Points[p].X;
                Centroid.Y += Points[p].Y;
                Centroid.Z += Points[p].Z;
            }

            Centroid.X /= 3;
            Centroid.Y /= 3;
            Centroid.Z /= 3;

            Vertex p0 = new Vertex(), p1 = new Vertex();
            p0.X = Points[0].X - Points[2].X;
            p0.Y = Points[0].Y - Points[2].Y;
            p0.Z = Points[0].Z - Points[2].Z;
            p1.X = Points[1].X - Points[2].X;
            p1.Y = Points[1].Y - Points[2].Y;
            p1.Z = Points[1].Z - Points[2].Z;

            N.X = (p0.Y * p1.Z - p0.Z * p1.Y);
            N.Y = (p0.Z * p1.X - p0.X * p1.Z);
            N.Z = (p0.X * p1.Y - p0.Y * p1.X);
            n = (float)Math.Sqrt((N.X * N.X) + (N.Y * N.Y) + (N.Z * N.Z));
            Normal.X = N.X / n;
            Normal.Y = N.Y / n;
            Normal.Z = N.Z / n;

            cam = Normal.X * 0 + Normal.Y * 0 + Normal.Z * 10;
            float productoPunto = Normal.X * (Form1.Luz.X) + Normal.Y * Form1.Luz.Y + Normal.Z * Form1.Luz.Z;
            float longitudNormal = (float)Math.Sqrt(Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z);
            float longitudDireccion = (float)Math.Sqrt((Form1.Luz.X) * (Form1.Luz.X) + (Form1.Luz.Y) * (Form1.Luz.Y) + Form1.Luz.Z * Form1.Luz.Z);
            h = (productoPunto / (longitudNormal * longitudDireccion) + 1) / 2;

        }

        public Triangle()
        {
            Points = new Vertex[3];
        }

        public void Add(Vertex point, int v)
        {
            Centroid = new Vertex();
            Normal = new Vertex();
            Vertex N = new Vertex();
            Points[v - 1] = point;

            if (Points[0] != null && Points[1] != null && Points[2] != null)
            {
                for (int p = 0; p < 3; p++)
                {
                    Centroid.X += Points[p].X;
                    Centroid.Y += Points[p].Y;
                    Centroid.Z += Points[p].Z;
                }

                Centroid.X /= 3;
                Centroid.Y /= 3;
                Centroid.Z /= 3;

                Vertex p0 = new Vertex(), p1 = new Vertex();
                p0.X = Points[0].X - Points[2].X;
                p0.Y = Points[0].Y - Points[2].Y;
                p0.Z = Points[0].Z - Points[2].Z;
                p1.X = Points[1].X - Points[2].X;
                p1.Y = Points[1].Y - Points[2].Y;
                p1.Z = Points[1].Z - Points[2].Z;

                N.X = (p0.Y * p1.Z - p0.Z * p1.Y);
                N.Y = (p0.Z * p1.X - p0.X * p1.Z);
                N.Z = (p0.X * p1.Y - p0.Y * p1.X);
                n = (float)Math.Sqrt((N.X * N.X) + (N.Y * N.Y) + (N.Z * N.Z));
                Normal.X = N.X / n;
                Normal.Y = N.Y / n;
                Normal.Z = N.Z / n;

                cam = Normal.X * 0 + Normal.Y * 0 + Normal.Z * 10;
                float productoPunto = Normal.X * (Form1.Luz.X) + Normal.Y * Form1.Luz.Y + Normal.Z * Form1.Luz.Z;
                float longitudNormal = (float)Math.Sqrt(Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z);
                float longitudDireccion = (float)Math.Sqrt((Form1.Luz.X) * (Form1.Luz.X) + (Form1.Luz.Y) * (Form1.Luz.Y) + Form1.Luz.Z * Form1.Luz.Z);
                h = (productoPunto / (longitudNormal * longitudDireccion) + 1) / 2;


            }
        }

        

        public Triangle TRotateX(float angle)
        {
            Util.Angle = angle;
            Triangle tri = new Triangle();
            for (int k = 0; k < 3; k++)
            {
                float[,] nv = new float[3, 1];
                float[,] ver = new float[3, 1]
                {
                {this.Points[k].X},
                {this.Points[k].Y},
                {this.Points[k].Z},
                 };
                for (int i = 0; i < 3; i++)
                {
                    float sum = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        sum += Util.RotX[i, j] * ver[j, 0];
                    }
                    nv[i, 0] = sum;
                }
                tri.Add(new Vertex(nv[0, 0], nv[1, 0], nv[2, 0]), k + 1);
            }
            return tri;

        }

        public Triangle TRotateY(float angle)
        {
            Util.Angle = angle;
            Triangle tri = new Triangle();
            for (int k = 0; k < 3; k++)
            {
                float[,] nv = new float[3, 1];
                float[,] ver = new float[3, 1]
                {
                {this.Points[k].X},
                {this.Points[k].Y},
                {this.Points[k].Z},
                 };
                for (int i = 0; i < 3; i++)
                {
                    float sum = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        sum += Util.RotY[i, j] * ver[j, 0];
                    }
                    nv[i, 0] = sum;
                }
                tri.Add(new Vertex(nv[0, 0], nv[1, 0], nv[2, 0]), k + 1);
            }
            return tri;
        }

        public Triangle TRotateZ(float angle)
        {
            Util.Angle = angle;
            Triangle tri = new Triangle();
            for (int k = 0; k < 3; k++)
            {
                float[,] nv = new float[3, 1];
                float[,] ver = new float[3, 1]
                {
                {this.Points[k].X},
                {this.Points[k].Y},
                {this.Points[k].Z},
                 };
                for (int i = 0; i < 3; i++)
                {
                    float sum = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        sum += Util.RotZ[i, j] * ver[j, 0];
                    }
                    nv[i, 0] = sum;
                }
                tri.Add(new Vertex(nv[0, 0], nv[1, 0], nv[2, 0]), k + 1);
            }
            return tri;
        }

        public void UpdateAttributes()
        {
            Centroid = new Vertex();
            for (int p = 0; p < 3; p++)
            {
                Centroid.X += Points[p].X;
                Centroid.Y += Points[p].Y;
                Centroid.Z += Points[p].Z;
            }

            Centroid.X /= 3;
            Centroid.Y /= 3;
            Centroid.Z /= 3;
        }

        public PointF[] TriProjP(int t)
        {
            PointF[] tri = new PointF[3];
            for (int i = 0; i < 3; i++)
            {
                tri[i] = Projection.ProjectionP(Points[i], t);
            }
            return tri;
        }
        public PointF[] TriProjO(int t)
        {
            PointF[] tri = new PointF[3];
            for (int i = 0; i < 3; i++)
            {
                tri[i] = Projection.ProjectionO(Points[i], t);
            }
            return tri;
        }

    }
}
