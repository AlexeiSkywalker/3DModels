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
            currentModels.Add(new Cube(150));

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
            pictureBox1.Image = new Bitmap(800, 800);
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (var model in currentModels)
                model.Draw(graphics, Transformations.AxonometricProjection1());
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {
            foreach (var model in currentModels)
            {
                model.ApplyTransformation(Transformations.RotateX((double)numericUpDown4.Value));
                model.ApplyTransformation(Transformations.RotateY((double)numericUpDown5.Value));
                model.ApplyTransformation(Transformations.RotateZ((double)numericUpDown6.Value));
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
    }
}
