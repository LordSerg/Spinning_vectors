using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace spinning_vectors
{
    class d3
    {
        public static Vector Q1, Q2, Q3;//кватернионы поворота
        public static Vector E1, E2, E3;//базисы пространства
        public double X, Y, Z;//координаты для юзера
        protected double x, y, z;//координаты для пространства
        protected static double AngleOX = 0, AngleOY = 0, AngleOZ = 0;//углы поворота
        public d3()
        {

        }
        public d3(double coord_x, double coord_y, double coord_z)
        {
            X = coord_x;
            Y = coord_y;
            Z = coord_z;
            x = E1.C[0] * X + E1.C[1] * Y + E1.C[2] * Z;
            y = E2.C[0] * X + E2.C[1] * Y + E2.C[2] * Z;
            z = E3.C[0] * X + E3.C[1] * Y + E3.C[2] * Z;
        }
        public static void rotate(double angle1, double angle2, double angle3)
        {
            AngleOX += angle1 * Math.PI / 180;//вокруг OX
            AngleOY += angle2 * Math.PI / 180;//вокруг OY
            AngleOZ += angle3 * Math.PI / 180;//вокруг OZ
            //if (AngleOX > Math.PI * 2)
            //    AngleOX -= Math.PI * 2;
            //if (AngleOY > Math.PI * 2)
            //    AngleOY -= Math.PI * 2;
            //if (AngleOZ > Math.PI * 2)
            //    AngleOZ -= Math.PI * 2;
            //if (AngleOX < -Math.PI * 2)
            //    AngleOX += Math.PI * 2;
            //if (AngleOY < -Math.PI * 2)
            //    AngleOY += Math.PI * 2;
            //if (AngleOZ < -Math.PI * 2)
            //    AngleOZ += Math.PI * 2;
            reset();
            Vector e1 = E1;
            Vector e2 = E2;
            Vector e3 = E3;
            //вариант с кватернионным поворотом:
            //Q1 = new Vector(Math.Sin(AngleOX / 2) * E1[0], Math.Sin(AngleOX / 2) * E1[1], Math.Sin(AngleOX / 2) * E1[2], Math.Cos(AngleOX / 2));
            //Q2 = new Vector(Math.Sin(AngleOY / 2) * E1[0], Math.Sin(AngleOY / 2) * E1[1], Math.Sin(AngleOY / 2) * E1[2], Math.Cos(AngleOY / 2));
            //Q3 = new Vector(Math.Sin(AngleOZ / 2) * E1[0], Math.Sin(AngleOZ / 2) * E1[1], Math.Sin(AngleOZ / 2) * E1[2], Math.Cos(AngleOZ / 2));
            ////E1 = E1 / !Q1;
            ////E2 = E2 / !Q2;
            ////E3 = E3 / !Q3;
            //E1 = (Q3) / (Q2) / E1;
            //E2 = (Q3) / (Q1) / E2;
            //E3 = (Q2) / (Q1) / E3;
            //Q1 = new Vector(Math.Sin(AngleOX / 2) * E2[0], Math.Sin(AngleOX / 2) * E2[1], Math.Sin(AngleOX / 2) * E2[2], Math.Cos(AngleOX / 2));
            //Q2 = new Vector(Math.Sin(AngleOY / 2) * E2[0], Math.Sin(AngleOY / 2) * E2[1], Math.Sin(AngleOY / 2) * E2[2], Math.Cos(AngleOY / 2));
            //Q3 = new Vector(Math.Sin(AngleOZ / 2) * E2[0], Math.Sin(AngleOZ / 2) * E2[1], Math.Sin(AngleOZ / 2) * E2[2], Math.Cos(AngleOZ / 2));
            //E1 = (Q3) / (Q2) / E1;
            //E2 = (Q3) / (Q1) / E2;
            //E3 = (Q2) / (Q1) / E3;
            //Q1 = new Vector(Math.Sin(AngleOX / 2) * E3[0], Math.Sin(AngleOX / 2) * E3[1], Math.Sin(AngleOX / 2) * E3[2], Math.Cos(AngleOX / 2));
            //Q2 = new Vector(Math.Sin(AngleOY / 2) * E3[0], Math.Sin(AngleOY / 2) * E3[1], Math.Sin(AngleOY / 2) * E3[2], Math.Cos(AngleOY / 2));
            //Q3 = new Vector(Math.Sin(AngleOZ / 2) * E3[0], Math.Sin(AngleOZ / 2) * E3[1], Math.Sin(AngleOZ / 2) * E3[2], Math.Cos(AngleOZ / 2));
            //E1 = (Q3) / (Q2) / E1;
            //E2 = (Q3) / (Q1) / E2;
            //E3 = (Q2) / (Q1) / E3;
            //вариант с перемножением матриц:
            E1 = new Vector(Math.Cos(AngleOY) * Math.Cos(AngleOZ) * (e1.C[0] + e2.C[0] + e3.C[0]), Math.Cos(AngleOY) * Math.Sin(AngleOZ) * (e1.C[1] + e2.C[1] + e3.C[1]), Math.Sin(AngleOY) * (e1.C[2] + e2.C[2] + e3.C[2]));
            E2 = new Vector((-(Math.Sin(AngleOX) * Math.Sin(AngleOY) * Math.Cos(AngleOZ)) - (Math.Cos(AngleOX) * Math.Sin(AngleOZ))) * (e1.C[0] + e2.C[0] + e3.C[0]), (-(Math.Sin(AngleOX) * Math.Sin(AngleOY) * Math.Sin(AngleOZ)) + (Math.Cos(AngleOX) * Math.Cos(AngleOZ))) * (e1.C[1] + e2.C[1] + e3.C[1]), Math.Sin(AngleOX) * Math.Cos(AngleOY) * (e1.C[2] + e2.C[2] + e3.C[2]));
            E3 = new Vector((-(Math.Cos(AngleOX) * Math.Sin(AngleOY) * Math.Cos(AngleOZ)) + (Math.Sin(AngleOX) * Math.Sin(AngleOZ))) * (e1.C[0] + e2.C[0] + e3.C[0]), (-(Math.Cos(AngleOX) * Math.Sin(AngleOY) * Math.Sin(AngleOZ)) - (Math.Sin(AngleOX) * Math.Cos(AngleOZ))) * (e1.C[1] + e2.C[1] + e3.C[1]), Math.Cos(AngleOX) * Math.Cos(AngleOY) * (e1.C[2] + e2.C[2] + e3.C[2]));
            //вариант с поочередным поворотом с начала по OX, потом OY, потом OZ
            //E1 = new Vector((e1.C[0] + e2.C[0] + e3.C[0]), 0, 0);
            //E2 = new Vector(0, Math.Cos(AngleOX) * (e1.C[1] + e2.C[1] + e3.C[1]), Math.Sin(AngleOX) * (e1.C[2] + e2.C[2] + e3.C[2]));
            //E3 = new Vector(0, -Math.Sin(AngleOX) * (e1.C[1] + e2.C[1] + e3.C[1]), Math.Cos(AngleOX) * (e1.C[2] + e2.C[2] + e3.C[2]));

            //E1 = new Vector(Math.Cos(AngleOY) * (e1.C[0] + e2.C[0] + e3.C[0]), 0, Math.Sin(AngleOY) * (e1.C[2] + e2.C[2] + e3.C[2]));
            //E2 = new Vector(0, (e1.C[1] + e2.C[1] + e3.C[1]), 0);
            //E3 = new Vector(-Math.Sin(AngleOY) * (e1.C[0] + e2.C[0] + e3.C[0]), 0, Math.Cos(AngleOY) * (e1.C[2] + e2.C[2] + e3.C[2]));

            //E1 = new Vector(Math.Cos(AngleOZ) * (e1.C[0] + e2.C[0] + e3.C[0]), Math.Sin(AngleOZ) * (e1.C[1] + e2.C[1] + e3.C[1]), 0);
            //E2 = new Vector(-Math.Sin(AngleOZ) * (e1.C[0] + e2.C[0] + e3.C[0]), Math.Cos(AngleOZ) * (e1.C[1] + e2.C[1] + e3.C[1]), 0);
            //E3 = new Vector(0, 0, (e1.C[2] + e2.C[2] + e3.C[2]));

            //матрицы перехода:
            /*
             вокруг оси Х:
             1    0      0
             0  cos(a)  sin(a)
             0 -sin(a)  cos(a)

             вокруг оси Y:
             cos(b)  0  sin(b)
               0     1     0
            -sin(b)  0  cos(b)

             вокруг оси Z:
             cos(c)  sin(c)  0
            -sin(c)  cos(c)  0
               0      0      1
             
             перемножив эти матрицы получим:
             cos(b)cos(c)           cos(b)sin(c)            sin(b)

        -sin(a)sin(b)cos(c)     -sin(a)sin(b)sin(c)     sin(a)cos(b)
            -cos(a)sin(c)           +cos(a)cos(c)

        -cos(a)sin(b)cos(c)     -cos(a)sin(b)sin(c)     cos(a)cos(b)
            +sin(a)sin(c)           -sin(a)cos(c)
             */
        }
        public static void reset()
        {
            E1 = new Vector(1, 0, 0);
            E2 = new Vector(0, 1, 0);
            E3 = new Vector(0, 0, 1);
        }
        public static void restart()
        {
            E1 = new Vector(1, 0, 0);
            E2 = new Vector(0, 1, 0);
            E3 = new Vector(0, 0, 1);
            AngleOX = 0;
            AngleOY = 0;
            AngleOZ = 0;
        }
        public static void draw(d3 point, PictureBox p, Graphics g, Color col)
        {//отображение точки на пикчерБоксе
            Brushes br;
            point.x = point.X * E1.C[0] + point.Y * E1.C[1] + point.Z * E1.C[2];
            point.y = point.X * E2.C[0] + point.Y * E2.C[1] + point.Z * E2.C[2];
            g.FillEllipse(Brushes.White, (float)point.x + p.Width / 2, -(float)point.y + p.Height / 2, 2, 2);
        }
        public static void draw_line(d3 point1, d3 point2, PictureBox p, Graphics g,Color col)
        {//отображение прямой на пикчерБоксе
            point1.x = point1.X * E1.C[0] + point1.Y * E1.C[1] + point1.Z * E1.C[2];
            point1.y = point1.X * E2.C[0] + point1.Y * E2.C[1] + point1.Z * E2.C[2];
            point2.x = point2.X * E1.C[0] + point2.Y * E1.C[1] + point2.Z * E1.C[2];
            point2.y = point2.X * E2.C[0] + point2.Y * E2.C[1] + point2.Z * E2.C[2];
            Pen pn = new Pen(col);
            g.DrawLine(pn, (float)point1.x + p.Width / 2, -(float)point1.y + p.Height / 2, (float)point2.x + p.Width / 2, -(float)point2.y + p.Height / 2);
        }
        public static double gamma = 0, betta = 0, alpha = 0;
        public static void spin_by_slide(double delt_x, double delt_y)
        {//если провели по экрану, то зная, как провели можно найти на какие углы будет поворачиваться базис:
         //пусть delt_x - изменение по х на экране,
         //а delt_у - изменение по у на экране;
         //пусть (delt_x, delt_y, 0) - вектор v в базисе E1(x1,y1,z1),E2(x2,y2,z2),E3(x3,y3,z3)
         //тогда разложим вектор v по базису E1,E2,E3:
         //v = alpha*E1 + betta*E2 + gamma*E3; 
         //запишем в виде системы уравнений:
         //delt_x = alpha*x1 + betta*x2 + gamma*x3
         //delt_y = alpha*y1 + betta*y2 + gamma*y3
         //   0   = alpha*z1 + betta*z2 + gamma*z3
         //из системы по Гаусу находим неизвестные alpha, betta, gamma 
         //- которые и есть новыми угловыми скоростями вокруг соотв. векторов E1, E2, E3.

            gamma = 0;
            betta = 0;
            alpha = 0;
            //if (((E1[0] * E3[2] - E3[0] * E1[2]) * (E2[1] * E1[0] - E2[0] * E1[1]) - (E3[1] * E1[0] - E1[1] * E3[0]) * (E1[0] * E2[2] - E2[0] * E1[2])) != 0)
                gamma = ((-delt_x * E3[2] * (E2[1] * E1[0] - E2[0] * E1[1])) - (delt_y * E1[0] - delt_x * E1[1]) * (E1[0] * E2[2] - E2[0] * E1[2])) / ((E1[0] * E3[2] - E3[0] * E1[2]) * (E2[1] * E1[0] - E2[0] * E1[1]) - (E3[1] * E1[0] - E1[1] * E3[0]) * (E1[0] * E2[2] - E2[0] * E1[2]));
            //if ((E1[0] * E2[1] - E2[0] * E1[1]) != 0)
                betta = ((delt_y * E1[0] - delt_x * E1[1]) - gamma * (E1[0] * E3[1] - E3[0] * E1[1])) / (E1[0] * E2[1] - E2[0] * E1[1]);
            //if (E1[0] != 0)
                alpha = (delt_x - gamma * E3[0] - betta * E2[0]) / E1[0];
            if (Math.Sqrt(delt_x * delt_x + delt_y * delt_y) != 0)
            {
                alpha /= Math.Sqrt(delt_x * delt_x + delt_y * delt_y);
                gamma /= Math.Sqrt(delt_x * delt_x + delt_y * delt_y);
                betta /= Math.Sqrt(delt_x * delt_x + delt_y * delt_y);
            }
            else
            {
                gamma = 0;
                betta = 0;
                alpha = 0;
            }
        }
    }
}
