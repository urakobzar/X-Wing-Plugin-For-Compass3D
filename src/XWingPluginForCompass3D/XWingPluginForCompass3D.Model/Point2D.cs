namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс точки в двухмерном пространстве.
    /// </summary>
    public class Point2D
    {
        /// <summary>
        /// Координата по оси X.
        /// </summary>
        private double _x;

        /// <summary>
        /// Координата по оси Y.
        /// </summary>
        private double _y;

        /// <summary>
        /// Устанавливает и возвращает координату по оси X.
        /// </summary>
        public double X
        {
            set
            {
                _x = value;
            }
            get
            {
                return _x;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает координату по оси Y.
        /// </summary>
        public double Y
        {
            set
            {
                _y = value;
            }
            get
            {
                return _y;
            }
        }

        /// <summary>
        /// Создаёт экземпляр класса для создания 2D-точки.
        /// </summary>
        /// <param name="x">Координату по оси X.</param>
        /// <param name="y">Координату по оси Y.</param>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
