using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphic3D
{
    [Serializable]
    class Plot : Model
    {
        public static double[,] multiply(double[,] m1, double[,] m2)
        {
            double[,] data = new double[4, 4];
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    data[i, j] = 0;
                    for (int k = 0; k < 4; ++k)
                        data[i, j] += m1[i, k] * m2[k, j];
                }
            return data;
        }

        public Plot(
            double x0, double x1, double dx, double z0, double z1, double dz,
            Func<double, double, double> function, double AngleX = Math.PI / 4,
            double AngleY = Math.PI / 2, double AngleZ = Math.PI / 4)
        {
            int nx = (int)((x1 - x0) / dx);
            int nz = (int)((z1 - z0) / dz);
            vertices = new List<double[]>(nx * nz);
            indices = new List<int[]>((nx - 1) * (nz - 1));
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < nz; ++j)
                {
                    var x = x0 + dx * i;
                    var z = z0 + dz * j;
                    vertices.Add(new double[] { x, function(x, z), z, 0 });
                }

            ApplyTransformation(multiply(Transformations.RotateX(-AngleX), multiply(Transformations.RotateY(0), Transformations.RotateZ(0))));
            ApplyTransformation(multiply(Transformations.RotateX(0), multiply(Transformations.RotateY(0), Transformations.RotateZ(-AngleZ))));
            ApplyTransformation(multiply(Transformations.RotateX(0), multiply(Transformations.RotateY(-AngleY), Transformations.RotateZ(0))));


            List<int[]> res_indices = new List<int[]>();

            for (int i = 0; i < nx - 1; ++i)
            {
                double y_max = double.MinValue;
                double y_min = double.MaxValue;
                for (int j = 0; j < i; ++j)
                {
                    var temp = new int[4] {
                        (nz - (2 + j)) * nz + (i - j - 1),
                        (nz - (1 + j)) * nz + (i - j - 1),
                        (nz - (1 + j)) * nz + (i - j ),
                        (nz - (2 + j)) * nz + (i - j)
                    };
                    if ((vertices[temp[0]][1] > y_max || vertices[temp[2]][1] > y_max) ||
                        (vertices[temp[1]][1] > y_max || vertices[temp[3]][1] > y_max))
                    {
                        res_indices.Add(temp);
                        y_max = Math.Max(Math.Max(vertices[temp[0]][1], vertices[temp[1]][1]), Math.Max(vertices[temp[2]][1], vertices[temp[3]][1]));
                    }

                    if ((vertices[temp[0]][1] < y_min || vertices[temp[2]][1] < y_min) ||
                        (vertices[temp[1]][1] < y_min || vertices[temp[3]][1] < y_min))
                    {
                        res_indices.Add(temp);
                        y_min = Math.Min(Math.Min(vertices[temp[0]][1], vertices[temp[1]][1]), Math.Min(vertices[temp[2]][1], vertices[temp[3]][1]));
                    }
                }
            }


            for (int i = 0; i < nx - 1; ++i)
            {
                double y_max = double.MinValue;
                double y_min = double.MaxValue;
                for (int j = i; j >= 0; --j)
                {
                    var temp = new int[4] {
                        (j + 1) * nx - (i + 1) + (j - 1),
                        (j + 2) * nx - (i + 1) + (j - 1),
                        (j + 2) * nx - i + (j - 1),
                        (j + 1) * nx - i + (j - 1)
                    };
                    if ((vertices[temp[0]][1] >= y_max || vertices[temp[2]][1] >= y_max) ||
                        (vertices[temp[1]][1] >= y_max || vertices[temp[3]][1] >= y_max))
                    {
                        res_indices.Add(temp);
                        y_max = Math.Max(Math.Max(vertices[temp[0]][1], vertices[temp[1]][1]), Math.Max(vertices[temp[2]][1], vertices[temp[3]][1]));
                    }

                    if ((vertices[temp[0]][1] <= y_min || vertices[temp[2]][1] <= y_min) ||
                        (vertices[temp[1]][1] <= y_min || vertices[temp[3]][1] <= y_min))
                    {
                        res_indices.Add(temp);
                        y_min = Math.Min(Math.Min(vertices[temp[0]][1], vertices[temp[1]][1]),  Math.Min(vertices[temp[2]][1],  vertices[temp[3]][1]));
                    }
                }
            }
            indices = res_indices;

            ApplyTransformation(multiply(Transformations.RotateX(0), multiply(Transformations.RotateY(AngleY), Transformations.RotateZ(0))));
            ApplyTransformation(multiply(Transformations.RotateX(0), multiply(Transformations.RotateY(0), Transformations.RotateZ(AngleZ))));
            ApplyTransformation(multiply(Transformations.RotateX(AngleX), multiply(Transformations.RotateY(0), Transformations.RotateZ(0))));
        }
    }
}