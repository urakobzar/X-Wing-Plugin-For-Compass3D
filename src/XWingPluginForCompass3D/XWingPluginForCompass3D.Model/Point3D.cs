using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingPluginForCompass3D.Model
{
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
        /// Параметризированный конструктор класса.
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
