using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_II_Figuras_base
{
    public class Figure
    {
        public List<Triangle> triangles;
        public Vertex Centroid;
        public int t;
        

        public Figure()
        {
            triangles = new List<Triangle>();
        }

        public void Add(Triangle tri)
        {
            Centroid = new Vertex();
            triangles.Add(tri);


            for (int p = 0; p < triangles.Count; p++)
            {
                Centroid.X += triangles[p].Centroid.X;
                Centroid.Y += triangles[p].Centroid.Y;
                Centroid.Z += triangles[p].Centroid.Z;
            }

            Centroid.X /= triangles.Count;
            Centroid.Y /= triangles.Count;
            Centroid.Z /= triangles.Count;
        }

        public Figure RotateX(float angle)
        {
            Figure figure = new Figure();
            Triangle tri = new Triangle();
            for (int i = 0; i < triangles.Count; i++)
            {
                tri = triangles[i].TRotateX(angle);
                figure.Add(tri);
            }
            return figure;
        }

        public Figure RotateY(float angle)
        {
            Figure figure = new Figure();
            Triangle tri = new Triangle();
            for (int i = 0; i < triangles.Count; i++)
            {
                tri = triangles[i].TRotateY(angle);
                figure.Add(tri);
            }
            return figure;
        }

        public Figure RotateZ(float angle)
        {
            Figure figure = new Figure();
            Triangle tri = new Triangle();
            for (int i = 0; i < triangles.Count; i++)
            {
                tri = triangles[i].TRotateZ(angle);
                figure.Add(tri);
            }
            return figure;
        }

        public void UpdateAttributes()
        {
            Centroid = new Vertex();

            for (int p = 0; p < triangles.Count; p++)
            {
                Centroid.X += triangles[p].Centroid.X;
                Centroid.Y += triangles[p].Centroid.Y;
                Centroid.Z += triangles[p].Centroid.Z;
            }

            Centroid.X /= triangles.Count;
            Centroid.Y /= triangles.Count;
            Centroid.Z /= triangles.Count;
        }

    }
}
