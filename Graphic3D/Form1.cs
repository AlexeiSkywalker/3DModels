using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Graphic3D
{
    public partial class Form1 : Form
    {

        Model current;
        
        public Form1()
        {
            InitializeComponent();
            current = new Cube(150);
            Redraw();         
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
                current.ApplyTransformation(Transformations.Scale((double)numericUpDown1.Value,
                                            (double)numericUpDown2.Value, (double)numericUpDown3.Value));
            Redraw();
        }

        private void Redraw()
        {
            pictureBox1.Image = new Bitmap(600, 600);
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DrawAxises();
                current.Draw(graphics, Transformations.AxonometricProjection1());
            
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {

            current.ApplyTransformation(Transformations.RotateX(ToRadians((double)numericUpDown4.Value)));
            current.ApplyTransformation(Transformations.RotateY(ToRadians((double)numericUpDown5.Value)));
            current.ApplyTransformation(Transformations.RotateZ(ToRadians((double)numericUpDown6.Value)));

            Redraw();
        }

        private void translateButton_Click(object sender, EventArgs e)
        {
                current.ApplyTransformation(Transformations.Translate((double)numericUpDown7.Value,
                                            (double)numericUpDown8.Value, (double)numericUpDown9.Value));
            Redraw();
        }

        private double ToRadians(double angle)
        {
            return angle / 180 * Math.PI;
        }

        private void DrawAxises()
        {
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Point center = new Point(300, 300);
            graphics.DrawLine(Pens.Blue, center, new Point(300, 0));
            graphics.DrawLine(Pens.Red, center, new Point(599, 510));
            graphics.DrawLine(Pens.Green, center, new Point(0, 510));
        }

        private void Serialize(Model serializableObject, string fileName)
        {
            if (serializableObject == null)
                return;
            Stream stream = File.Open(fileName, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, serializableObject);
            stream.Close();
        }

        private Model Deserialize(string fileName)
        {
            Model res;
            Stream stream = File.Open(fileName, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            res = (Model)bformatter.Deserialize(stream);
            stream.Close();
            return res;
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Open Model File",
                Filter = "Object File|*.obj;"
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            current = Deserialize(dialog.FileName);
            Redraw();

        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                FileName = "model.obj",
                Title = "Save Model",
                Filter = "Object File|*.obj;"
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            Serialize(current, dialog.FileName);
            MessageBox.Show("Файл сохранён");
        }

        private void drawBbutton_Click(object sender, EventArgs e)
        {
            if (simpleCubeRadioButton.Checked)
                current = new Cube(150);
            else if (texturedCubeRadioButton.Checked)
                current = new TexturedCube(150, Image.FromFile("../../brickwork-texture.jpg"));
            Redraw();
        }
    }
}
