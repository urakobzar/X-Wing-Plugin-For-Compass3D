using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс круга.
    /// </summary>
    public class Circle
    {
	    /// <summary>
        /// Радиус круга.
        /// </summary>
        private double _radius;

        /// <summary>
        /// Возвращает точку центра круга.
        /// </summary>
        public Point2D Center { get; }

        /// <summary>
        /// Устанавливает и возвращает радиус круга.
        /// </summary>
        public double Radius
        {
            set
            {
                _radius = value;
            }
            get
            {
                return _radius;
            }
        }

        /// <summary>
        /// Параметризированный конструктор.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Circle (Point2D center, double radius)
        {
            Radius = radius;
            Center = center;
        }
    }
}
