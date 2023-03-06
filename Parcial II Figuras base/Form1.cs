using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TrackBar = System.Windows.Forms.TrackBar;
using TreeView = System.Windows.Forms.TreeView;

namespace Parcial_II_Figuras_base
{
    public partial class Form1 : Form
    {
        Canvas canvas;
        PictureBox pictureBox;
        TreeView list;
        Button rx, ry, rz, fill, cu, ci, co, sp, dode, dodes;
        NumericUpDown citri, cotri, sptri, r, g, b;
        TrackBar t;
        Figure f;
        Scene scene;
        public static Vertex Luz = new Vertex(-3, -3, 0);
        int w, h, a = 0, cucounter = 1, cicounter = 1, cocounter = 1, spcounter = 1, dodecounter = 1, dodescounter = 1;
        Boolean ex = true, ye = true, ze = true, pr = false;
        public Form1()
        {
            InitializeComponent();
            scene = new Scene();
            scene.Figures.Add(new Figure());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            w = Form1.ActiveForm.Width - 200;
            h = Form1.ActiveForm.Height - 150;
            canvas = new Canvas(new Size(w, h));
            pictureBox = new PictureBox
            {
                Image = canvas.bitmap,
                Size = new Size(w, h),
                Location = new Point(0, 50),
                BackColor = Color.Gray
            };
            ControlLoad();
            Projection op = new Projection(w, h);
            this.Controls.Add(pictureBox);
            this.Controls.Add(rx);
            this.Controls.Add(ry);
            this.Controls.Add(rz);
            this.Controls.Add(fill);
            this.Controls.Add(r);
            this.Controls.Add(g);
            this.Controls.Add(b);  
            this.Controls.Add(cu);
            this.Controls.Add(ci);
            this.Controls.Add(citri);
            this.Controls.Add(co);
            this.Controls.Add(cotri);
            this.Controls.Add(sp);
            this.Controls.Add(sptri);
            this.Controls.Add(dode);
            this.Controls.Add(dodes);
            this.Controls.Add(list);
            this.Controls.Add(t);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            a++;
            if (a == 360) a = 0;
            
            if (f != null)
            {
                f.t = t.Value;
                scene.Figures[scene.Figures.Count - 1] = f;
                if (ex) scene.Figures[scene.Figures.Count - 1] = scene.Figures[scene.Figures.Count - 1].RotateX(a);
                if (ye) scene.Figures[scene.Figures.Count - 1] = scene.Figures[scene.Figures.Count - 1].RotateY(a);
                if (ze) scene.Figures[scene.Figures.Count - 1] = scene.Figures[scene.Figures.Count - 1].RotateZ(a);
                canvas.FastClear();
                canvas.DrawAxis(w, h);
                canvas.RenderLast(scene, f.t, pr, Color.FromArgb((int)r.Value, (int)g.Value, (int)b.Value));
            }
            pictureBox.Refresh();
        }

        private void ControlLoad()
        {
            rx = new Button
            {
                Location = new Point(w + 25, 50),
                Text = "Stop X",
            };
            ry = new Button
            {
                Location = new Point(w + 25, 80),
                Text = "Stop Y",
            };
            rz = new Button
            {
                Location = new Point(w + 25, 110),
                Text = "Stop Z",
            };
            fill = new Button
            {
                Location = new Point(w + 25, 140),
                Text = "Fill",
            };
            r = new NumericUpDown
            {
                Location = new Point(w + 25, 170),
                Width = 50,
                Minimum = 0,
                Maximum = 255,
                Value = 0,
            };
            g = new NumericUpDown
            {
                Location = new Point(w + 25 + r.Width, 170),
                Width = 50,
                Minimum = 0,
                Maximum = 255,
                Value = 0,
            };
            b = new NumericUpDown
            {
                Location = new Point(w + 25 + r.Width + g.Width, 170),
                Width = 50,
                Minimum = 0,
                Maximum = 255,
                Value = 255,
            };
            cu = new Button
            {
                Location = new Point(w + 25, 625),
                Text = "Add Cube",
                Width = 135,
            };
            ci = new Button
            {
                Location = new Point(w + 25, 650),
                Text = "Add Cilinder"
            };
            citri = new NumericUpDown
            {
                Location = new Point(w + 30 + ci.Width, 650),
                Size = new Size(50, ci.Height),
                Minimum = 6,
                Maximum = 360,
            };
            co = new Button
            {
                Location = new Point(w + 25, 675),
                Text = "Add Cone"
            };
            cotri = new NumericUpDown
            {
                Location = new Point(w + 30 + ci.Width, 675),
                Size = new Size(50, ci.Height),
                Minimum = 6,
                Maximum = 360,
            };
            sp = new Button
            {
                Location = new Point(w + 25, 700),
                Text = "Add Sphere"
            };
            sptri = new NumericUpDown
            {
                Location = new Point(w + 30 + sp.Width, 700),
                Size = new Size(50, ci.Height),
                Minimum = 6,
                Maximum = 360,
                Increment = 2,
            };
            dode = new Button
            {
                Location = new Point(w + 25, 725),
                Text = "Add Dodechaedron",
                Width = 135,
            };
            dodes = new Button
            {
                Location = new Point(w + 25, 750),
                Text = "Add Dodechaedron Scrtach",
                Width = 135,
            };
            list = new TreeView
            {
                Location = new Point(w + 25, 210),
                Height = 400,
            };
            t = new TrackBar
            {
                Location = new Point(0, h + 50),
                Width = w,
                Maximum = 500,
                Minimum = 0,
                Value = 100,
            };
            list.AfterSelect += LIST_AfterSelect;
            rx.Click += RX_Click;
            ry.Click += RY_Click;
            rz.Click += RZ_Click;
            cu.Click += CU_Click;
            fill.Click += FILL_Click;
            ci.Click += CI_Click;
            co.Click += CO_Click;
            sp.Click += SP_Click;
            dode.Click += DODE_Click;
            dodes.Click += DODES_Click;
        }
        private void RX_Click(object sender, EventArgs e)
        {
            ex = !ex;
            if (ex) rx.Text = "Stop X";
            else rx.Text = "Run X";
        }
        private void RY_Click(object sender, EventArgs e)
        {
            ye = !ye;
            if (ye) ry.Text = "Stop Y";
            else ry.Text = "Run Y";
        }
        private void RZ_Click(object sender, EventArgs e)
        {
            ze = !ze;
            if (ze) rz.Text = "Stop Z";
            else rz.Text = "Run Z";
        }
        private void FILL_Click(object sender, EventArgs e)
        {
            pr = !pr;
        }
        private void CU_Click(object sender, EventArgs e)
        {

            t.Value = 100;
            scene.Figures.Add(new Figure());
            AddCube();
            f = scene.Figures[scene.Figures.Count-1];
            TreeNode node = new TreeNode("Cube" + (cucounter));
            cucounter++;
            node.Tag = f;
            list.Nodes.Add(node);
        }
        private void CI_Click(object sender, EventArgs e)
        {
            t.Value = 100;
            scene.Figures.Add(new Figure());
            AddCilinder((int)citri.Value);
            f = scene.Figures[scene.Figures.Count - 1];
            TreeNode node = new TreeNode("Cilinder " + cicounter + " t" + ((int)citri.Value));
            cicounter++;
            node.Tag = f;
            list.Nodes.Add(node);
        }
        private void CO_Click(object sender, EventArgs e)
        {
            t.Value = 100;
            scene.Figures.Add(new Figure());
            AddCone((int)cotri.Value);
            f = scene.Figures[scene.Figures.Count - 1];
            TreeNode node = new TreeNode("Cone" + cocounter + " t" + ((int)cotri.Value));
            cocounter++;
            node.Tag = f;
            list.Nodes.Add(node);
        }
        private void SP_Click(object sender, EventArgs e)
        {
            t.Value = 100;
            scene.Figures.Add(new Figure());
            AddSphere((int)sptri.Value);
            f = scene.Figures[scene.Figures.Count - 1];
            TreeNode node = new TreeNode("Sphere" + spcounter + " t" + ((int)sptri.Value));
            spcounter++;
            node.Tag = f;
            list.Nodes.Add(node);
        }
        private void DODE_Click(object sender, EventArgs e)
        {
            t.Value = 100;
            scene.Figures.Add(new Figure());
            AddDode();
            f = scene.Figures[scene.Figures.Count - 1];
            TreeNode node = new TreeNode("Dodecahedron" + dodecounter);
            dodecounter++;
            node.Tag = f;
            list.Nodes.Add(node);
        }
        private void DODES_Click(object sender, EventArgs e)
        {
            t.Value = 100;
            scene.Figures.Add(new Figure());
            AddDodeAureo();
            f = scene.Figures[scene.Figures.Count - 1];
            TreeNode node = new TreeNode("Dodecahedron Scratch" + dodescounter);
            dodecounter++;
            node.Tag = f;
            list.Nodes.Add(node);
        }
        private void LIST_AfterSelect(object sender, TreeViewEventArgs e)
        {
            f = (Figure)list.SelectedNode.Tag;
            t.Value = f.t;
        }
        private void AddCube()
        {
            Vertex A = new Vertex(1 ,1 ,1);
            Vertex B = new Vertex(-1,1,1);
            Vertex C = new Vertex(-1,-1,1);
            Vertex D = new Vertex(1,-1,1);
            Vertex E = new Vertex(1,1,-1);
            Vertex F = new Vertex(-1,1,-1);
            Vertex G = new Vertex(-1,-1,-1);
            Vertex H = new Vertex(1,-1,-1);
            scene.Figures.Add(new Figure());
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(A,B,C));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(A,C,D));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(E,A,D));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(E,D,H));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(F,E,H));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(F,H,G));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(B,F,G));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(B,G,C));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(E,F,B));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(E,B,A));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(C,G,H));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(C,H,D));
        }
        private void AddCilinder(int c)
        {
            List<Vertex> Base = new List<Vertex>();
            List<Vertex> Tapa = new List<Vertex>();
            for (int u = 0; u < c; u++)
            {
                float a = (float)((u * (360/c)) * (System.Math.PI / 180));
                Base.Add(new Vertex((float)Math.Cos(a),(float)Math.Sin(a), -1));
                Tapa.Add(new Vertex((float)Math.Cos(a), (float)Math.Sin(a), 1));
            }
            scene.Figures.Add(new Figure());
            for (int i = 0; i < Base.Count-1; i++)
            {
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Tapa[i+1], Tapa[i], Base[i]));
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Tapa[i+1], Base[i], Base[i+1]));
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Tapa[i], Tapa[i+1],new Vertex(0,0,1)));
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(new Vertex(0, 0, -1), Base[i + 1], Base[i]));
            }
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Tapa[0], Tapa[Tapa.Count-1], Base[Base.Count-1]));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Tapa[0], Base[Base.Count-1], Base[0]));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Tapa[Tapa.Count - 1], Tapa[0], new Vertex(0, 0, 1)));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(new Vertex(0, 0, -1), Base[0], Base[Base.Count - 1]));

        }
        private void AddCone(int c) 
        {
            List<Vertex> Base = new List<Vertex>();
            for (int u = 0; u < c; u++)
            {
                float a = (float)((u * (360/c)) * (System.Math.PI / 180));
                Base.Add(new Vertex((float)Math.Cos(a), (float)Math.Sin(a), -1));
            }
            scene.Figures.Add(new Figure());
            for (int i = 0; i < Base.Count - 1; i++)
            {
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Base[i], Base[i + 1], new Vertex(0, 0, 1)));
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(new Vertex(0, 0, -1), Base[i + 1], Base[i]));
            }
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Base[Base.Count - 1], Base[0], new Vertex(0, 0, 1)));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(new Vertex(0, 0, -1), Base[0], Base[Base.Count - 1]));
        }
        private void AddSphere(int c) 
        {
            Vertex[,] circ = new Vertex[c+1,c];
            Vertex top = new Vertex(0,0,1);
            Vertex bot = new Vertex(0,0,-1);
            for (int v = 1; v < c; v++)
            {
                float a = (float)((v * (180/(c+1)) * (System.Math.PI / 180)));
                for (int u = 0; u < c; u++)
                {
                    float b = (float)((u * (360/c)) * (System.Math.PI / 180));
                    circ[v, u] = new Vertex((float)(Math.Cos(b)*Math.Sin(a)), (float)(Math.Sin(b)*Math.Sin(a)), (float)(Math.Cos(a)));
                }
            }
            scene.Figures.Add(new Figure());
            for (int i = 0; i < c-1; i++)
            {
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[1,i], circ[1,i+1],top));
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[c-1, i], bot, circ[c - 1, i + 1]));
                for (int j = 1; j < (c/2); j++)
                {
                    scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[j, i], circ[j+1, i], circ[j+1, i+1]));
                    scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[j, i], circ[j + 1, i + 1], circ[j,i+1]));
                }
                for (int j = (c / 2); j < c - 1; j++)
                {
                    scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[j, i], circ[j + 1, i], circ[j + 1, i + 1]));
                    scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[j, i], circ[j + 1, i + 1], circ[j, i + 1]));
                }
            }
            for (int j = 1; j < (c / 2); j++)
            {
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[j, c - 1], circ[j + 1, c - 1], circ[j + 1, 0]));
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[j, c - 1], circ[j + 1, 0], circ[j, 0]));
            }
            for (int j = (c / 2); j < c - 1; j++)
            {
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[j, c - 1], circ[j + 1, c - 1], circ[j + 1, 0]));
                scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[j, c - 1], circ[j + 1, 0], circ[j, 0]));
            }
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[1, c - 1], circ[1, 0], top));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(circ[c - 1, c - 1], bot, circ[c - 1, 0]));
            
        }
        private void AddDodeAureo()
        {
            float phi = (float)(1 + Math.Sqrt(5)) / 2;
            
            Vertex A = new Vertex(phi,0,1/phi);
            Vertex B = new Vertex(-phi, 0, 1 / phi);
            Vertex C = new Vertex(-phi, 0, -1 / phi);
            Vertex D = new Vertex(phi, 0, -1 / phi);
            Vertex E = new Vertex(1 / phi, phi,0);
            Vertex F = new Vertex(1 / phi, -phi, 0);
            Vertex G = new Vertex(-1 / phi, -phi, 0);
            Vertex H = new Vertex(-1 / phi, phi, 0);
            Vertex I = new Vertex(0, 1 / phi, phi);
            Vertex J = new Vertex(0, 1 / phi, -phi);
            Vertex K = new Vertex(0, -1 / phi, -phi);
            Vertex L = new Vertex(0, -1 / phi, phi);
            Vertex M = new Vertex(1, 1, 1);
            Vertex N = new Vertex(-1, 1, 1);
            Vertex O = new Vertex(-1, -1, 1);
            Vertex P = new Vertex(1, -1, 1);
            Vertex Q = new Vertex(1, 1, -1);
            Vertex R = new Vertex(-1, 1, -1);
            Vertex S = new Vertex(-1, -1, -1);
            Vertex T = new Vertex(1, -1, -1);
            scene.Figures.Add(new Figure());
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(A, B, C));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(A, C, D));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(E, F, G));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(E, G, H));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(I, J, K));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(I, K, L));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(M, N, O));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(M, O, P));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Q, M, P));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Q, P, T));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(R, Q, T));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(R, T, S));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(N, R, S));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(N, S, O));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Q, R, N));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(Q, N, M));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(O, S, T));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(O, T, P));

        }
        private void AddDode()
        {
            float phi = (float)(1 + Math.Sqrt(5)) / 2;

            Vertex A = new Vertex(phi, 0, 1 / phi);
            Vertex B = new Vertex(-phi, 0, 1 / phi);
            Vertex C = new Vertex(-phi, 0, -1 / phi);
            Vertex D = new Vertex(phi, 0, -1 / phi);
            Vertex E = new Vertex(1 / phi, phi, 0);
            Vertex F = new Vertex(1 / phi, -phi, 0);
            Vertex G = new Vertex(-1 / phi, -phi, 0);
            Vertex H = new Vertex(-1 / phi, phi, 0);
            Vertex I = new Vertex(0, 1 / phi, phi);
            Vertex J = new Vertex(0, 1 / phi, -phi);
            Vertex K = new Vertex(0, -1 / phi, -phi);
            Vertex L = new Vertex(0, -1 / phi, phi);
            Vertex M = new Vertex(1, 1, 1);
            Vertex N = new Vertex(-1, 1, 1);
            Vertex O = new Vertex(-1, -1, 1);
            Vertex P = new Vertex(1, -1, 1);
            Vertex Q = new Vertex(1, 1, -1);
            Vertex R = new Vertex(-1, 1, -1);
            Vertex S = new Vertex(-1, -1, -1);
            Vertex T = new Vertex(1, -1, -1);
            scene.Figures.Add(new Figure());
            PenToTriR(M, I, L, P, A);
            PenToTriL(N, I, L, O, B);
            PenToTriR(E, H, N, I, M);
            PenToTriL(F, G, O, L, P);
            PenToTriR(A, P, F, T, D);
            PenToTriL(A, M, E, Q, D);
            PenToTriL(B, O, G, S, C);
            PenToTriR(B, N, H, R, C);
            PenToTriL(E, H, R, J, Q);
            PenToTriR(F, G, S, K, T);
            PenToTriL(Q, J, K, T, D);
            PenToTriR(R, J, K, S, C);

        }

        private void PenToTriR(Vertex a, Vertex b, Vertex c, Vertex d, Vertex e)
        {
            Vertex p = new Vertex();
            p.X = a.X + b.X + c.X + d.X + e.X;
            p.Y = a.Y + b.Y + c.Y + d.Y + e.Y;
            p.Z = a.Z + b.Z + c.Z + d.Z + e.Z;

            p.X = p.X / 5;
            p.Y = p.Y / 5;
            p.Z = p.Z / 5;

            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(a, b, p));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(b, c, p));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(c, d, p));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(d, e, p));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(e, a, p));
        }
        private void PenToTriL(Vertex a, Vertex b, Vertex c, Vertex d, Vertex e)
        {
            Vertex p = new Vertex();
            p.X = a.X + b.X + c.X + d.X + e.X;
            p.Y = a.Y + b.Y + c.Y + d.Y + e.Y;
            p.Z = a.Z + b.Z + c.Z + d.Z + e.Z;

            p.X = p.X / 5;
            p.Y = p.Y / 5;
            p.Z = p.Z / 5;

            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(b, a, p));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(c, b, p));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(d, c, p));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(e, d, p));
            scene.Figures[scene.Figures.Count - 1].Add(new Triangle(a, e, p));
        }

    }
}
