namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс дуги.
    /// </summary>
    public class Arc: Circle
    {
        /// <summary>
        /// Создаёт экземпляр класса для построения дуги.
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
        private Point2D _startPoint;

        /// <summary>
        /// Конечная точка дуги.
        /// </summary>
        private Point2D _endPoint;

        /// <summary>
        /// Направление дуги.
        /// </summary>
        private short _direction;

        /// <summary>
        /// Устанавливает и возвращает начальную точку дуги.
        /// </summary>
        public Point2D StartPoint
        {
            set
            {
                _startPoint = value;
            }
            get
            {
                return _startPoint;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает конечную точку дуги.
        /// </summary>
        public Point2D EndPoint
        {
            set
            {
                _endPoint = value;
            }
            get
            {
                return _endPoint;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает направление обхода дуги.
        /// </summary>
        public short Direction
        {
            set
            {
                _direction = value;
            }
            get
            {
                return _direction;
            }
        }
    }
}