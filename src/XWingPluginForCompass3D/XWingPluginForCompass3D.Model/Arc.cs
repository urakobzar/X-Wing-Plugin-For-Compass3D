using System;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс дуги.
    /// </summary>
    public class Arc: Circle, IEquatable<Arc>
    {
        /// <summary>
        /// Создает экземпляр класса для построения дуги.
        /// </summary>
        /// <param name="center">Центр окружности дуги.</param>
        /// <param name="radius">Радиус окружности дуги.</param>
        /// <param name="startPoint">Начальная точка дуги.</param>
        /// <param name="endPoint">Конечная точка дуги.</param>
        /// <param name="direction">Направление обхода.</param>
        public Arc (Point2D center, double radius, Point2D startPoint, Point2D endPoint,
            short direction): base(center, radius)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Direction = direction;
        }

        /// <summary>
        /// Начальная точка дуги.
        /// </summary>
        public Point2D StartPoint { set; get; }

        /// <summary>
        /// Конечная точка дуги.
        /// </summary>
        public Point2D EndPoint { set; get; }

        /// <summary>
        /// Направление дуги.
        /// </summary>
        public short Direction { set; get; }

        /// <summary>
        /// Проверка на равенство объектов класса.
        /// </summary>
        /// <param name="expected">Сравниваемый объект.</param>
        /// <returns>Возвращает true, если элементы равны,
        /// false - в обратном случае.</returns>
        public bool Equals(Arc expected)
        {
            return expected != null &&
                   Center.Equals(expected.Center) &&
                   StartPoint.Equals(expected.StartPoint) &&
                   EndPoint.Equals(expected.EndPoint) &&
                   Direction.Equals(expected.Direction) &&
                   Radius.Equals(expected.Radius);
        }
    }
}