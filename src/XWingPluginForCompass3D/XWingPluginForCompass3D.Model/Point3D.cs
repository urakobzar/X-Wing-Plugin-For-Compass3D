namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс точки в трёхмерном пространстве.
    /// </summary>
    public class Point3D: Point2D
    {
        /// <summary>
        /// Координата по оси Z.
        /// </summary>
        private double _z;

        /// <summary>
        /// Устанавливает и возвращает координату по оси Z.
        /// </summary>
        public double Z
        {
            set
            {
                _z = value;
            }
            get
            {
                return _z;
            }
        }

        /// <summary>
        /// Создаёт экземпляр класса для 3-D точки.
        /// </summary>
        /// <param name="x">Координату по оси X.</param>
        /// <param name="y">Координату по оси Y.</param>
        /// <param name="z">Координату по оси Z.</param>
        public Point3D(double x, double y, double z): base (x,y)
        {
            Z = z;
        }
    }
}
