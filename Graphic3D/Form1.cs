using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphic3D
{
    public partial class Form1 : Form
    {
        List<Model> currentModels;
        
        public Form1()
        {
            InitializeComponent();
            currentModels = new List<Model>();
            currentModels.Add(new Cube(100));
            Redraw();         
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            foreach (var model in currentModels)
                model.ApplyTransformation(Transformations.Scale((double)numericUpDown1.Value,
                                            (double)numericUpDown2.Value, (double)numericUpDown3.Value));
            Redraw();
        }

        private void Redraw()
        {
            pictureBox1.Image = new Bitmap(600, 600);
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (var model in currentModels)
                model.Draw(graphics, Transformations.AxonometricProjection1());
            //DrawAxises();
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {
            foreach (var model in currentModels)
            {
                model.ApplyTransformation(Transformations.RotateX(ToRadians((double)numericUpDown4.Value)));
                model.ApplyTransformation(Transformations.RotateY(ToRadians((double)numericUpDown5.Value)));
                model.ApplyTransformation(Transformations.RotateZ(ToRadians((double)numericUpDown6.Value)));
            }
            Redraw();
        }

        private void translateButton_Click(object sender, EventArgs e)
        {
            foreach (var model in currentModels)
                model.ApplyTransformation(Transformations.Translate((double)numericUpDown7.Value,
                                            (double)numericUpDown8.Value, (double)numericUpDown9.Value));
            Redraw();
        }

        private double ToRadians(double angle)
        {
            return angle / 180 * Math.PI;
        }

        /*private void DrawAxises()
        {
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.DrawLine(Pens.Blue, new Point(300, 320), new Point(300, 0));
            graphics.DrawLine(Pens.Red, new Point(300, 320), new Point(599, 599));
            graphics.DrawLine(Pens.Green, new Point(300, 320), new Point(0, 599));
        }*/
    }
}
