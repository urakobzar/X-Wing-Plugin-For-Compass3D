using System;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс круга.
    /// </summary>
    public class Circle: IEquatable<Circle>
    {
        /// <summary>
        /// Возвращает точку центра круга.
        /// </summary>
        public Point2D Center { get; }

        /// <summary>
        /// Радиус круга.
        /// </summary>
        public double Radius { set; get; }

        /// <summary>
        /// Параметризованный конструктор.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Circle (Point2D center, double radius)
        {
            Radius = radius;
            Center = center;
        }

        public bool Equals(Circle expected)
        {
            return expected != null &&
                   Center.Equals(expected.Center) &&
                   (expected.Radius == Radius);
        }
    }
}