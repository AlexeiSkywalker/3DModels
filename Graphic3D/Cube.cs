using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphic3D
{
    class Cube : Model
    {
        public Cube(double size)
        {
            double s = size / 2;
            vertices = new List<double[]>();
            vertices.Add(new double[] { -s, -s, -s, 1 });
            vertices.Add(new double[] { -s, s, -s, 1 });
            vertices.Add(new double[] { s, s, -s, 1 });
            vertices.Add(new double[] { s, -s, -s, 1 });
            vertices.Add(new double[] { -s, -s, s, 1 });
            vertices.Add(new double[] { -s, s, s, 1 });
            vertices.Add(new double[] { s, s, s, 1 });
            vertices.Add(new double[] { s, -s, s, 1 });


            indices = new List<int[]>();
            indices.Add(new int[] { 3, 2, 1, 0 });
            indices.Add(new int[] { 0, 1, 5, 4 });
            indices.Add(new int[] { 1, 2, 6, 5 });
            indices.Add(new int[] { 2, 3, 7, 6 });
            indices.Add(new int[] { 3, 0, 4, 7 });
            indices.Add(new int[] { 4, 5, 6, 7 });
        }

        
    }
}
