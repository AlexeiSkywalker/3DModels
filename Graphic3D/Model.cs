using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphic3D
{
    // Абстрактный класс модели. Все реальные фигуры (куб, тетрайдер и т. п.) должны наследоваться от этого класса
    public abstract class Model
    {
        public List<double[]> vertices { get; set; } // Вершины фигуры
        public List<int[]> indices { get; set; } // Индексы вершин

        /// <summary>
        /// Применяет афинное преобразование
        /// </summary>
        /// <param name="transformation">Матрица преобразования</param>
        public virtual void ApplyTransformation(double[,] transformation)
        {
            for (int i = 0; i < vertices.Count; ++i)
                vertices[i] = Multiply(vertices[i], transformation);

        }

        /// <summary>
        /// Рисует объект
        /// </summary>
        /// <param name="graphics">Объект Graphics для рисования</param>
        /// <param name="projection">Матрица проекции</param>
        public virtual void Draw(Graphics graphics, double[,] projection)
        {
            for (int i = 0; i < indices.Count; ++i)
                for (int j = 0; j < indices[i].Length; ++j)
                {
                    var f1 = Multiply(vertices[indices[i][j]], projection);
                    Point pt1 = new Point((int)f1[0] + 400, (int)f1[1] + 400);
                    var f2 = Multiply(vertices[indices[i][(j + 1) % indices[i].Length]], projection);
                    Point pt2 = new Point((int)f2[0] + 400, (int)f2[1] + 400);
                    graphics.DrawLine(Pens.White, pt1, pt2);
                }
        }

        protected double[] Multiply(double[] vertex, double[,] matrix)
        {
            double[] result = new double[] {0, 0, 0, 0 };
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                    result[i] += vertex[j] * matrix[j, i];
            return result;
        }
    }
}
