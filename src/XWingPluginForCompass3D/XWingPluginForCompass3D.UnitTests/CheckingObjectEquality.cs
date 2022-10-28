using XWingPluginForCompass3D.Model;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс проверки равенства объектов класса.
    /// </summary>
    public class CheckingObjectEquality
    {
        /// <summary>
        /// Сравнение трехмерных массивов класса Point2D.
        /// </summary>
        /// <param name="expected">Сравниваемый объект.</param>
        /// <param name="actual">Текущий объект.</param>
        /// <returns>Возвращает true, если элементы равны,
        /// false - в обратном случае.</returns>
        public bool CheckEqual(Point2D[,,] expected, Point2D[,,] actual)
        {
            for (var i = 0; i < expected.GetLength(0); i++)
            {
                for (var j = 0; j < expected.GetLength(1); j++)
                {
                    for (var k = 0; k < expected.GetLength(2); k++)
                    {
                        if (!actual[i, j, k].Equals(expected[i, j, k]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Сравнение одномерных массивов класса Point3D.
        /// </summary>
        /// <param name="expected">Сравниваемый объект.</param>
        /// <param name="actual">Текущий объект.</param>
        /// <returns>Возвращает true, если элементы равны,
        /// false - в обратном случае.</returns>
        public bool CheckEqual(Point3D[] expected, Point3D[] actual)
        {
            for (var i = 0; i < expected.GetLength(0); i++)
            {
                if (!actual[i].Equals(expected[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Сравнение двумерных массивов класса Circle.
        /// </summary>
        /// <param name="expected">Сравниваемый объект.</param>
        /// <param name="actual">Текущий объект.</param>
        /// <returns>Возвращает true, если элементы равны,
        /// false - в обратном случае.</returns>
        public bool CheckEqual(Circle[,] expected, Circle[,] actual)
        {
            for (var i = 0; i < expected.GetLength(0); i++)
            {
                for (var j = 0; j < expected.GetLength(1); j++)
                {
                    if (!actual[i, j].Equals(expected[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Сравнение двумерных массивов класса Arc.
        /// </summary>
        /// <param name="expected">Сравниваемый объект.</param>
        /// <param name="actual">Текущий объект.</param>
        /// <returns>Возвращает true, если элементы равны,
        /// false - в обратном случае.</returns>
        public bool CheckEqual(Arc[,] expected, Arc[,] actual)
        {
            for (var i = 0; i < expected.GetLength(0); i++)
            {
                for (var j = 0; j < expected.GetLength(1); j++)
                {
                    if (!actual[i, j].Equals(expected[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}