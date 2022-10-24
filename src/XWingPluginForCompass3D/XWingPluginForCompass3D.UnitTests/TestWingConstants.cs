using System;
using System.Collections.Generic;
using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей констант для построения крыльев.
    /// </summary>
    [TestFixture]
    public class TestWingConstants
    {
        /// <summary>
        /// Длина корпуса.
        /// </summary>
        private const int BodyLength = 300;

        /// <summary>
        /// Ширина крыльев.
        /// </summary>
        private const int WingsWidth = 300;

        /// <summary>
        /// Объект класса констант для построения крыльев.
        /// </summary>
        private static readonly WingsConstants Constants =
            new WingsConstants(WingsWidth,BodyLength);

        /// <summary>
        /// Объект класса для проверки равенства массивов созданных классов.
        /// </summary>
        private readonly CheckingObjectEquality _check =
            new CheckingObjectEquality();

        /// <summary>
        /// Перечисление полей типа Point3D класса констант ускорителей.
        /// </summary>
        public enum Point3DTypes
        {
            BackBodyPlane,
            CuttingPlane
        }

        /// <summary>
        /// Перечисление полей типа Point2D класса констант ускорителей.
        /// </summary>
        public enum Point2DTypes
        {
            BaseVertexes,
            WingsCutVertexes
        }

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point3DTypes, Func<Point3D>> _point3DFunc
            = new Dictionary<Point3DTypes, Func<Point3D>>
            {
                [Point3DTypes.BackBodyPlane] = () => Constants.BackBodyPlane,
                [Point3DTypes.CuttingPlane] = () => Constants.CuttingPlane
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Func<Point2D[,,]>> _point2DFunc
            = new Dictionary<Point2DTypes, Func<Point2D[,,]>>
            {
                [Point2DTypes.BaseVertexes] = () => Constants.BaseVertexes,
                [Point2DTypes.WingsCutVertexes] = () => Constants.WingsCutVertexes
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Point2D[,,]> _point2Ds
            = new Dictionary<Point2DTypes, Point2D[,,]>
            {
                [Point2DTypes.BaseVertexes] = new[, ,]
                {
                    {
                        { new Point2D(69.173833, 55.779384),
                            new Point2D(78.023751, 42.034027) },
                        { new Point2D(78.023751, 42.034027),
                            new Point2D(632.086192, 150.642161) },
                        { new Point2D(632.086192, 150.642161),
                            new Point2D(639.549926, 169.837344) },
                        { new Point2D(639.549926, 169.837344),
                            new Point2D(69.173833, 55.779384) }
                    },
                    {
                        { new Point2D(77.853158, -39.308584),
                            new Point2D(64.171267, -54.321075) },
                        { new Point2D(64.171267, -54.321075),
                            new Point2D(646.963505, -163.875661) },
                        { new Point2D(646.963505, -163.875661),
                            new Point2D(640.123102, -145.081809) },
                        { new Point2D(640.123102, -145.081809),
                            new Point2D(77.853158, -39.308584) }
                    }
                },
                [Point2DTypes.WingsCutVertexes] = new[, ,]
                {
                    {
                        {
                            new Point2D(646.963505,
                                -600 - BodyLength + WingsWidth),
                            new Point2D(646.963505,
                                -666.419923 - BodyLength + WingsWidth)
                        },
                        {
                            new Point2D(646.963505,
                                -666.419923 - BodyLength + WingsWidth),
                            new Point2D(128.282347,
                                -600 - BodyLength + WingsWidth)
                        },
                        {
                            new Point2D(128.282347,
                                -600 - BodyLength + WingsWidth),
                            new Point2D(646.963505,
                                -600 - BodyLength + WingsWidth)
                        }
                    },

                    {
                        { new Point2D(646.963505, -600 - BodyLength),
                            new Point2D(646.963505, -550 - BodyLength) },
                        {new Point2D(646.963505, -550 - BodyLength),
                            new Point2D(128.282347, -600 - BodyLength)},
                        { new Point2D(128.282347, -600 - BodyLength),
                            new Point2D(646.963505, -600 - BodyLength) }
                    }
                }
            };

        /// <summary>
        /// Тест на возврат поля константы типа Point3D.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="z">Координата Z.</param>
        /// <param name="type">Параметр поля типа Point3D.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение" +
                            "точки 3D плоскости.")]
        [TestCase(0, 0, -600 - BodyLength,
            Point3DTypes.BackBodyPlane)]
        [TestCase(0, -121.493198, -600 - BodyLength,
            Point3DTypes.CuttingPlane)]
        public void TestPlaneGet_CorrectValue(double x, double y, double z,
            Point3DTypes type)
        {
            var expected = new Point3D(x, y, z);
            var actual = _point3DFunc[type]();
            Assert.IsTrue(actual.Equals(expected));
        }

        [Test(Description = "Позитивный тест геттеров на возвращение массива" +
                            "точек для построения отрезков.")]
        [TestCase(Point2DTypes.BaseVertexes)]
        [TestCase(Point2DTypes.WingsCutVertexes)]
        public void TestVertexesGet_CorrectValue(Point2DTypes type)
        {
            var expected = _point2Ds[type];
            var actual = _point2DFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }
    }
}