using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab14_g
{
    public partial class Form1 : Form
    {

        #region переменные

        // угол
        double angel_OXY;

        // угол
        double angel_res_OXY;

        // точка 0
        Point Point_0 = new Point(0, 0);

        // фигура
        List<Point3D> figure_3D = new List<Point3D>();

        // pen для проекции figure_3D
        Pen pen_figure_3D = new Pen(Color.Red);

        // для временного хранения при поворотах
        int tmp_XX;
        int tmp_YY;

        # endregion

        /// <summary>
        /// конструктор формы
        /// </summary>        
        public Form1()
        {
            InitializeComponent();

            // Двойная буф-я
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty).SetValue(pictureBox1, true, null);

            // зададим точку отсчета по середине
            Point_0.X = pictureBox1.Width / 2;
            Point_0.Y = pictureBox1.Height / 3;

            // установим углы
            angel_OXY = 1.0;
            angel_res_OXY = 0.5;
        }

        #region расчеты и отрисовка

        private Point convert_3D_in_2D_Point(Point3D val)
        {
            double res_x = -val._z * System.Math.Sin(angel_OXY) + val._x * System.Math.Cos(angel_OXY) + Point_0.X;
            double res_y = -(val._z * System.Math.Cos(angel_OXY) + val._x * System.Math.Sin(angel_OXY)) * System.Math.Sin(angel_res_OXY) + val._y * System.Math.Cos(angel_res_OXY) + Point_0.Y;

            return new Point((int)(res_x), (int)(res_y));
        }
        void draw(List<Point3D> val)
        {
            if (figure_3D.Count <= 0)
                return;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics grf = Graphics.FromImage(bmp);


            for (int i = 0; i < val.Count - 1; i++)
            {
                grf.DrawLine(pen_figure_3D, convert_3D_in_2D_Point(val[i]), convert_3D_in_2D_Point(val[i + 1]));
            }

            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
            GC.Collect();

        }

        private void cube()
        {
            if (figure_3D != null)
                figure_3D.Clear();

            figure_3D.Add(new Point3D(0, 0, 0));
            figure_3D.Add(new Point3D(300, 0, 0));
            figure_3D.Add(new Point3D(300, 0, 300));
            figure_3D.Add(new Point3D(0, 0, 300));
            figure_3D.Add(new Point3D(0, 500, 300));
            figure_3D.Add(new Point3D(0, 500, 0));
            figure_3D.Add(new Point3D(0, 0, 300));
            figure_3D.Add(new Point3D(0, 0, 0));
            figure_3D.Add(new Point3D(0, 500, 0));
            figure_3D.Add(new Point3D(300, 500, 0));
            figure_3D.Add(new Point3D(300, 500, 300));
            figure_3D.Add(new Point3D(300, 0, 300));
            figure_3D.Add(new Point3D(300, 0, 0));
            figure_3D.Add(new Point3D(300, 500, 0));
            figure_3D.Add(new Point3D(0, 500, 0));
            figure_3D.Add(new Point3D(0, 500, 300));
            figure_3D.Add(new Point3D(300, 500, 300));
            figure_3D.Add(new Point3D(300, 500, 300));
        }

        private void button_cub_Click(object sender, EventArgs e)
        {
            cube();
            draw(figure_3D);
        }


        private void Form_main_Resize(object sender, EventArgs e)
        {
            draw(figure_3D);
        }

        # endregion
    }
}
