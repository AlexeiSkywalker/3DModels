using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphic3D
{
    [Serializable]
    class TexturedCube : Model
    {
        Image texture;

        public TexturedCube(double size, Image texture)
        {
            this.texture = texture;
            double l = size / 2;
            vertices = new List<double[]>();
            vertices.Add(new double[] { -l, -l, -l, 1 });
            vertices.Add(new double[] { -l, l, -l, 1 });
            vertices.Add(new double[] { l, l, -l, 1 });
            vertices.Add(new double[] { l, -l, -l, 1 });
            vertices.Add(new double[] { -l, -l, l, 1 });
            vertices.Add(new double[] { -l, l, l, 1 });
            vertices.Add(new double[] { l, l, l, 1 });
            vertices.Add(new double[] { l, -l, l, 1 });


            indices = new List<int[]>();
            indices.Add(new int[] { 3, 2, 1, 0 });
            indices.Add(new int[] { 0, 1, 5, 4 });
            indices.Add(new int[] { 1, 2, 6, 5 });
            indices.Add(new int[] { 2, 3, 7, 6 });
            indices.Add(new int[] { 3, 0, 4, 7 });
            indices.Add(new int[] { 4, 5, 6, 7 });
        }

        public override void Draw(Graphics graphics, double[,] projection)
        {
            // 0, 3, 4 - лицевые грани
            //base.Draw(graphics, projection);
            for (int i = 0; i < indices.Count; ++i) // сначала рисуем нелицевые грани
            {
                if (i == 0 || i == 3 || i == 4)
                    continue;
                var f1 = Multiply(vertices[indices[i][0]], projection);
                Point pt1 = new Point((int)f1[0] + 300, (int)f1[1] + 300);
                var f2 = Multiply(vertices[indices[i][1]], projection);
                Point pt2 = new Point((int)f2[0] + 300, (int)f2[1] + 300);
                var f3 = Multiply(vertices[indices[i][3]], projection);
                Point pt3 = new Point((int)f3[0] + 300, (int)f3[1] + 300);
                Point[] rec = { pt1, pt3, pt2 };
                graphics.DrawImage(texture, rec);
            }

            for (int i = 0; i <= 4; ++i) // затем лицевые грани
            {
                if (i == 1 || i == 2)
                    continue;
                var f1 = Multiply(vertices[indices[i][0]], projection);
                Point pt1 = new Point((int)f1[0] + 300, (int)f1[1] + 300);
                var f2 = Multiply(vertices[indices[i][1]], projection);
                Point pt2 = new Point((int)f2[0] + 300, (int)f2[1] + 300);
                var f3 = Multiply(vertices[indices[i][3]], projection);
                Point pt3 = new Point((int)f3[0] + 300, (int)f3[1] + 300);
                Point[] rec = { pt1, pt3, pt2 };
                graphics.DrawImage(texture, rec);
            }
        }
    }
}
