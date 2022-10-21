using System;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс точки в двухмерном пространстве.
    /// </summary>
    public class Point2D: IEquatable<Point2D>
    {
        /// <summary>
        /// Координата по оси X.
        /// </summary>
        public double X { set; get; }

        /// <summary>
        /// Координата по оси Y.
        /// </summary>
        public double Y { set; get; }

        /// <summary>
        /// Создает экземпляр класса для создания 2D-точки.
        /// </summary>
        /// <param name="x">Координату по оси X.</param>
        /// <param name="y">Координату по оси Y.</param>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point2D other)
        {
            throw new NotImplementedException();
        }
    }
}