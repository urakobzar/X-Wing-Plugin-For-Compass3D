using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XWingPluginForCompass3D.Model
{
    class XWingConstants
    {

        /// <summary>
        /// Количество вершин треугольника.
        /// </summary>
        private const int _triangleVerticesNumber = 3;

        /// <summary>
        /// Количество вершин четырёхугольника.
        /// </summary>
        private const int _quadrangleVerticesNumber = 4;

        /// <summary>
        /// Количество вершин шестиугольника.
        /// </summary>
        private const int _hexagonVerticesNumber = 6;

        /// <summary>
        /// Возвращает количество вершин треугольника.
        /// </summary>
        public int TriangleVerticesNumber
        {
            get
            {
                return _triangleVerticesNumber;
            }
        }

        /// <summary>
        /// Возвращает количество вершин четырёхугольника.
        /// </summary>
        public int QadrangleVerticesNumber
        {
            get
            {
                return _quadrangleVerticesNumber;
            }
        }

        /// <summary>
        /// Возвращает количество вершин шестиугольника.
        /// </summary>
        public int HexagonVerticesNumber
        {
            get
            {
                return _hexagonVerticesNumber;
            }
        }

    }
}