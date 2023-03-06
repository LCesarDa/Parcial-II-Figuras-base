using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_II_Figuras_base
{
    public class Vertex
    {
        private float w, x, y, z;

        public static readonly Vertex Empty;

        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                if (x == 0f)
                {
                    if (y == 0f)
                        return z == 0f;
                }

                return false;
            }
        }
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
        public Vertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1;
        }

        public Vertex()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
            this.w = 1;
        }
    }
}
