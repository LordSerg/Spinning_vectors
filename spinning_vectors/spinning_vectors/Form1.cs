using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
//эта форма написана без кнопок и лейблов.
//сдесь всего 4 элемента: 3 пикчербокса и 1 панель
namespace spinning_vectors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bp1, bp2, bp3;//отвечают за соответствующие пикчербоксы

        Graphics g1, g2, g3;
        List<Vector> vectors;
        Bitmap b_pattern;//отвечает за рисование узоров(picturebox1)
        Bitmap b_vectors;//отвечает за рисование векторов(picturebox1)
        Bitmap b_settings;//за настройки(picturebox2)
        Bitmap b_s1, b_s2, b_s3;//за кнопки(picturebox3)

        Graphics g_v, g_p, g_s, g_strt, g_set, g_vec;
        string[]b;//список текстовых названий и подписей

        int m_x, m_y, mx0, my0, mx1, my1;
        /*
if ((((y1 <= y) && (y < y2)) || ((y2 <= y) && (y < y1))) && (((y2 - y1) != 0) && (x > (((x2 - x1) * (y - y1)) / (y2 - y1) + x1))))
               k++;
    */
        private void Form1_Load(object sender, EventArgs e)
        {
            d3.reset();
            bp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            g1 = Graphics.FromImage(bp1);//комбо всех картинок на pictBox1
            bp2 = new Bitmap(pictureBox2.Width, pictureBox2.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            g2 = Graphics.FromImage(bp2);//комбо всех картинок на pictBox2
            bp3 = new Bitmap(pictureBox3.Width, pictureBox3.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            g3 = Graphics.FromImage(bp3);//комбо всех картинок на pictBox3
            b_pattern = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            b_vectors = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            b_settings = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            b_s1 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            b_s2 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            b_s3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            g_strt = CreateGraphics();
            g_strt = Graphics.FromImage(b_s1);//кнопка старта
            g_set = CreateGraphics();
            g_set = Graphics.FromImage(b_s2);//кнопка настроек
            g_vec = CreateGraphics();
            g_vec = Graphics.FromImage(b_s3);//кнопка списка векторов
            g_s = CreateGraphics();//еще неизвестный эл.
            g_s = Graphics.FromImage(b_settings);
            g_v = CreateGraphics();//тут хранится отображение векторов
            g_v = Graphics.FromImage(b_vectors);
            g_p = CreateGraphics();//здесь - узор, нарисованый векторами
            g_p = Graphics.FromImage(b_pattern);
            //--------------------обработка pictureBox1
            //рисуем узор
            g_v.Clear(Color.Transparent);
            g_p.Clear(Color.Transparent);
            g_v.DrawLine(Pens.White, 0, 0, pictureBox1.Width, pictureBox1.Height);
            g_p.DrawLine(Pens.Blue, 0,100, pictureBox1.Width, pictureBox1.Height);
            g1.CompositingMode = CompositingMode.SourceOver;
            g1.DrawImage(b_vectors, 0, 0);
            g1.DrawImage(b_pattern, 0, 0);
            pictureBox1.Image = bp1;
            //--------------------обработка pictureBox2
            //рисуем настройки:
            g_s.Clear(Color.Transparent);

            pictureBox2.Image = b_settings;
            //--------------------обработка pictureBox3
            //рисуем кнопки:
            b = new string[3];
            b[0] = "Start";
            b[1] = "Vectors";
            b[2] = "Settings";
            g_strt.Clear(Color.Transparent);
            g_set.Clear(Color.Transparent);
            g_vec.Clear(Color.Transparent);
            Font f = new Font(FontFamily.GenericSansSerif,10.0F, FontStyle.Bold);
            g_strt.DrawString(b[0], f, Brushes.White, 5, 20);
            g_set.DrawString(b[1], f, Brushes.White, (pictureBox3.Width / 3) + 5, 20);
            g_vec.DrawString(b[2], f, Brushes.White, (pictureBox3.Width * 2 / 3) + 5, 20);
            g3.CompositingMode = CompositingMode.SourceOver;
            g3.DrawImage(b_s1, 0, 0);
            g3.DrawImage(b_s2, 0, 0);
            g3.DrawImage(b_s3, 0, 0);
            pictureBox3.Image = bp3;

            //

            a = new d3(0, 0, 0);
            c = new d3(100, 0, 0);
            q = new d3(0, 100, 0);
            w = new d3(0, 0, 100);
            Random r = new Random();
            k = new d3[500];
            for (int i = 0; i < 500; i++)
                k[i] = new d3(r.Next(-200, 200), r.Next(-200, 200), r.Next(-200, 200));
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            m_x = Cursor.Position.X;
            m_y = Cursor.Position.Y;
            if(is_down)
            {
                d3.spin_by_slide(mx1-m_x,my1-m_y);
            }
            mx1 = Cursor.Position.X;
            my1 = Cursor.Position.Y;
        }
        bool is_down=false;
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mx0 = m_x;
            my0 = m_y;
            mx1 = m_x;
            my1 = m_y;
            is_down = true;
            Cursor.Hide();
        }
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            d3.spin_by_slide(mx0 - m_x, my0 - m_y);
            is_down = false;
            Cursor.Show();
            timer1.Enabled = true;
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void PictureBox3_Click(object sender, EventArgs e)
        {//кнопки
            Font f = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);
            g_strt.DrawString(b[0], f, Brushes.White, 5, 20);
            g_set.DrawString(b[1], f, Brushes.White, (pictureBox3.Width / 3) + 5, 20);
            g_vec.DrawString(b[2], f, Brushes.White, (pictureBox3.Width * 2 / 3) + 5, 20);
            pictureBox3.Refresh();
            if (Cursor.Position.X < pictureBox3.Width / 3)
            {//кнопка Старт/стоп
                g_strt.Clear(Color.Black);
                bool bu;
                if (b[0] == "Start")
                {
                    b[0] = "Stop";
                    bu = true;
                }
                else
                {
                    b[0] = "Start";
                    bu = false;
                }
                g_strt.DrawString(b[0], f, Brushes.Red, 5, 20);
                Start(bu);
            }
            else if (Cursor.Position.X < pictureBox3.Width * 2 / 3)
            {//кнопка "векторы"
                g_set.Clear(Color.Transparent);
                g_set.DrawString(b[1], f, Brushes.Red, (pictureBox3.Width / 3) + 5, 20);
                Vectors();
            }
            else
            {
                g_vec.Clear(Color.Transparent);
                g_vec.DrawString(b[2], f, Brushes.Red, (pictureBox3.Width * 2 / 3) + 5, 20);
                Settings();
            }
            pictureBox3.Image = null;
            
            g3.CompositingMode = CompositingMode.SourceOver;
            g3.DrawImage(b_s1, 0, 0);
            g3.DrawImage(b_s2, 0, 0);
            g3.DrawImage(b_s3, 0, 0);
            pictureBox3.Image = bp3;
            //g_strt.DrawString(b[0], f, Brushes.White, 5, 20);
            //g_set.DrawString(b[1], f, Brushes.White, (pictureBox3.Width / 3) + 5, 20);
            //g_vec.DrawString(b[2], f, Brushes.White, (pictureBox3.Width * 2 / 3) + 5, 20);
            //pictureBox3.Image = bp2;
        }
        d3 a, c, x, q, w;
        d3 []k;//массив точек, который необходимо отобразить
        double delt = 0.05;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            g_v.Clear(Color.Black);
            //g_p.Clear(Color.Transparent);
            d3.draw_line(a, c, pictureBox1, g_v,Color.Red);
            d3.draw_line(a, q, pictureBox1, g_v,Color.Green);
            d3.draw_line(a, w, pictureBox1, g_v,Color.Blue);//координатная ось
            for (int i = 0; i < 500; i++)
                d3.draw(k[i], pictureBox1, g_v, Color.White);
            //d3.draw_line(c, x, pictureBox1, g_p);
            g1.CompositingMode = CompositingMode.SourceOver;
            g1.DrawImage(b_vectors, 0, 0);
            //g1.DrawImage(b_pattern, 0, 0);
            pictureBox1.Image = bp1;
            //x = c;
            if (d3.alpha > 10)
                d3.alpha = 10;
            else if (d3.alpha < -10)
                d3.alpha = -10;
            else if (Math.Abs(d3.alpha) < delt)
                d3.alpha = 0;
            else if (d3.alpha > 0)
                d3.alpha -= delt;
            else if (d3.alpha < 0)
                d3.alpha += delt;

            if (d3.betta > 10)
                d3.betta = 10;
            else if (d3.betta < -10)
                d3.betta = -10;
            else if (Math.Abs(d3.betta) < delt)
                d3.betta = 0;
            else if (d3.betta > 0)
                d3.betta -= delt;
            else if (d3.betta < 0)
                d3.betta += delt;

            if (d3.gamma > 10)
                d3.gamma = 10;
            else if (d3.gamma < -10)
                d3.gamma = -10;
            else if (Math.Abs(d3.gamma) < delt)
                d3.gamma = 0;
            else if (d3.gamma > 0)
                d3.gamma -= delt;
            else if (d3.gamma < 0)
                d3.gamma += delt;
            if (d3.alpha != 0 || d3.betta != 0 || d3.gamma != 0)
                d3.rotate(d3.alpha, d3.betta, d3.gamma);
            else
                timer1.Enabled = false;
            
            g_s.Clear(Color.Black);
            Font f = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);
            g_s.DrawString(d3.alpha.ToString(), f, Brushes.White, 0, 0);
            g_s.DrawString(d3.betta.ToString(), f, Brushes.White, 0, pictureBox2.Height / 3);
            g_s.DrawString(d3.gamma.ToString(), f, Brushes.White, 0, pictureBox2.Height * 2 / 3);
            pictureBox2.Image = b_settings;
        }
        void Start(bool bu)
        {//запуск/остановка движения
            pictureBox2.Image = null;
            timer1.Enabled = bu;
            x = c;
            if (bu)
            {
                g_v.Clear(Color.Black);
                g_p.Clear(Color.Transparent);
            }
        }
        void Vectors()
        {
            //показываем список векторов
            d3.restart();


            pictureBox2.Image = bp2;
        }
        void Settings()
        {//показываем настройки
            pictureBox2.Image = bp2;

        }
    }
}
