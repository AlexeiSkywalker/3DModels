using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphic3D
{
    /// <summary>
    /// Содержит методы, возвращающие матрицы афинных преобразований и проекций
    /// </summary>
    public static class Transformations
    {
        public static double[,] AxonometricProjection()
        {
            return new double[4, 4] {
                    { 0.86, 0.25, 0, 0 },
                    { 0, 0.86, 0, 0 },
                    { 0.5, -0.43, 0, 0 },
                    { 0, 0, 0, 1 }
                };
        }
        public static double[,] AxonometricProjection1()
        {
            return new double[4, 4] {
                    { 0.7, 0.49, 0, 0 },
                    { 0, 0.7, 0, 0 },
                    { 0.7, -0.49, 0, 0 },
                    { 0, 0, 0, 1 }
                };
        }

        public static double[,] Scale(double fx, double fy, double fz)
        {
            return new double[,] {
                    { fx, 0, 0, 0 },
                    { 0, fy, 0, 0 },
                    { 0, 0, fz, 0 },
                    { 0, 0, 0, 1 }
                };
        }

        public static double[,] Translate(double dx, double dy, double dz)
        {
            return new double[,]
                {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { dx, dy, dz, 1 },
                };
        }

        public static double[,] RotateX(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new double[,]
                {
                    { 1, 0, 0, 0 },
                    { 0, cos, sin, 0 },
                    { 0, -sin, cos, 0 },
                    { 0, 0, 0, 1 }
                };
        }

        public static double[,] RotateY(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new double[,]
                {
                    { cos, 0, -sin, 0 },
                    { 0, 1, 0, 0 },
                    { sin, 0, cos, 0 },
                    { 0, 0, 0, 1 }
                };
        }

        public static double[,] RotateZ(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new double[,]
                {
                    { cos, sin, 0, 0 },
                    { -sin, cos, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                };
        }

    }
}
