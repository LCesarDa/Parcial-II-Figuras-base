using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_II_Figuras_base
{
    public class Util
    {
        public static float angle, cos, sin, z;
        public static int d = 3;
        public static float[,] RotX, RotY, RotZ, ProjP;
        public static float[,] ProjO = new float[,]
        {
            {1, 0, 0 },
            {0, 1, 0 },
        };
        public static float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                angle = (float)(value * (System.Math.PI / 180));
                cos = (float)Math.Cos(angle);
                sin = (float)Math.Sin(angle);
                RotX = new float[,]
                {
                  {1,0,0},
                  {0,cos,-sin},
                  {0,sin,cos},
                };
                RotY = new float[,]
                {
                  {cos,0,sin},
                  {0,1,0},
                  {-sin,0,cos},
                };
                RotZ = new float[,]
                {
                  {cos,-sin,0},
                  {sin,cos,0},
                  {0,0,1},
                };
            }

        }

        public static float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
                ProjP = new float[,]
                {
                    {1/(d-z),0,0},
                    {0,1/(d-z),0},
                };
            }
        }
    }
}
