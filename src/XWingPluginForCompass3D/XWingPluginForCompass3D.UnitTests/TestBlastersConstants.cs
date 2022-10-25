using System;
using System.Collections.Generic;
using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей констант для построения бластеров.
    /// </summary>
    [TestFixture]
    public class TestBlastersConstants
    {
        /// <summary>
        /// Разница между шириной крыла и длиной корпуса.
        /// </summary>
        private const int Difference = 20;

        /// <summary>
        /// Объект класса констант для построения бластеров.
        /// </summary>
        private static readonly BlastersConstants Constants =
            new BlastersConstants(Difference);

        /// <summary>
        /// Объект класса для проверки равенства массивов созданных классов.
        /// </summary>
        private readonly CheckingObjectEquality _check =
            new CheckingObjectEquality();

        /// <summary>
        /// Перечисление полей типа Point3D класса констант бластеров.
        /// </summary>
        public enum Point3DTypes
        {
            SideRightTipPlane,
            SideLeftTipPlane,
            FrontBlasterBodyPlane,
            CurrentPlane
        }

        /// <summary>
        /// Перечисление полей типа Point2D класса констант бластеров.
        /// </summary>
        public enum Point2DTypes
        {
            TipsBaseSegments,
            RightAntennaSegments,
            LeftAntennaSegments,
            BlasterDrawingSegments
        }

        /// <summary>
        /// Перечисление полей типа Circle класса констант бластеров.
        /// </summary>
        public enum CircleTypes
        {
            CurrentBlasterCircles,
            BlasterDrawingCircles
        }

        /// <summary>
        /// Перечисление полей типа Arc класса констант бластеров.
        /// </summary>
        public enum ArcTypes
        {
            TipsBaseArcs,
            RightAntennaArcs,
            LeftAntennaArcs
        }

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point3D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point3DTypes, Func<Point3D>> _point3DFunc
            = new Dictionary<Point3DTypes, Func<Point3D>>
            {
                [Point3DTypes.SideRightTipPlane] = () => Constants.SideRightTipPlane,
                [Point3DTypes.SideLeftTipPlane] = () => Constants.SideLeftTipPlane,
                [Point3DTypes.FrontBlasterBodyPlane] = () => Constants.FrontBlasterBodyPlane,
                [Point3DTypes.CurrentPlane] = () => Constants.CurrentPlane
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Func<Point2D[,,]>> _point2DFunc
            = new Dictionary<Point2DTypes, Func<Point2D[,,]>>
            {
                [Point2DTypes.TipsBaseSegments] =
                    () => Constants.TipsBaseSegments,
                [Point2DTypes.RightAntennaSegments] =
                    () => Constants.RightAntennaSegments,
                [Point2DTypes.LeftAntennaSegments] =
                    () => Constants.LeftAntennaSegments,
                [Point2DTypes.BlasterDrawingSegments] =
                    () => Constants.BlasterDrawingSegments
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Circle,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<CircleTypes, Func<Circle[,]>> _circleFunc
            = new Dictionary<CircleTypes, Func<Circle[,]>>
            {
                [CircleTypes.CurrentBlasterCircles] = 
                    () => Constants.CurrentBlasterCircles,
                [CircleTypes.BlasterDrawingCircles] = 
                    () => Constants.BlasterDrawingCircles
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Arc,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<ArcTypes, Func<Arc[,]>> _arcFunc
            = new Dictionary<ArcTypes, Func<Arc[,]>>
            {
                [ArcTypes.TipsBaseArcs] = () => Constants.TipsBaseArcs,
                [ArcTypes.RightAntennaArcs] = () => Constants.RightAntennaArcs,
                [ArcTypes.LeftAntennaArcs] = () => Constants.LeftAntennaArcs
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Point2D[,,]> _point2Ds
            = new Dictionary<Point2DTypes, Point2D[,,]>
            {
                [Point2DTypes.TipsBaseSegments] = new[, ,]
                {
                    {
                        { new Point2D(-622.468507, -179),
                            new Point2D(-622.468507, -181)},
                        { new Point2D(-612.468507, -179),
                            new Point2D(-612.468507, -181)},
                        { new Point2D(-616.468507, -175),
                            new Point2D(-618.468507, -175)},
                        { new Point2D(-616.468507, -185),
                            new Point2D(-618.468507, -185)}
                    },
                    {
                        { new Point2D(-622.468507, 184),
                            new Point2D(-622.468507, 186)},
                        { new Point2D(-612.468507, 184),
                            new Point2D(-612.468507, 186)},
                        { new Point2D(-616.468507, 180),
                            new Point2D(-618.468507, 180)},
                        { new Point2D(-616.468507, 190),
                            new Point2D(-618.468507, 190)}
                    }
                },
                [Point2DTypes.RightAntennaSegments] = new[, ,]
                {
                    {
                        { new Point2D(-210 - Difference, 205),
                            new Point2D(-210 - Difference, 200)},
                        { new Point2D(-210 - Difference, 160),
                            new Point2D(-210 - Difference, 155)}
                    },
                    {
                        { new Point2D(-210 - Difference, -210),
                            new Point2D(-210 - Difference, -205)},
                        { new Point2D(-210 - Difference, -165),
                            new Point2D(-210 - Difference, -160)}
                    }
                },
                [Point2DTypes.LeftAntennaSegments] = new[, ,]
                {
                    {
                        { new Point2D(210 + Difference, 205),
                            new Point2D(210 + Difference, 200)},
                        { new Point2D(210 + Difference, 160),
                            new Point2D(210 + Difference, 155)}
                    },
                    {
                        { new Point2D(210 + Difference, -210),
                            new Point2D(210 + Difference, -205)},
                        { new Point2D(210 + Difference, -165),
                            new Point2D(210 + Difference, -160)}
                    }
                },
                [Point2DTypes.BlasterDrawingSegments] = new[, ,]
                {
                    {
                        { new Point2D(-634.439069, 201.970562),
                            new Point2D(-628.075108, 195.606601)},
                        { new Point2D(-617.468507, 209),
                            new Point2D(-617.468507, 200)},
                        { new Point2D(-600.497944, 201.970562),
                            new Point2D(-606.861905, 195.606601)},
                        { new Point2D(-593.468507, 185),
                            new Point2D(-602.468507, 185)},
                        { new Point2D(-600.497944, 168.029437),
                            new Point2D(-606.861905, 174.393398)},
                        { new Point2D(-617.468507, 161),
                            new Point2D(-617.468507, 170)},
                        { new Point2D(-634.439069, 168.029437),
                            new Point2D(-628.075108, 174.393398)},
                        { new Point2D(-641.468507, 185),
                            new Point2D(-632.468507, 185)}
                    },
                    {
                        { new Point2D(-634.439069, -163.029437),
                            new Point2D(-628.075108, -169.393398)},
                        { new Point2D(-617.468507, -156),
                            new Point2D(-617.468507, -165)},
                        { new Point2D(-600.497944, -163.029437),
                            new Point2D(-606.861905, -169.393398)},
                        { new Point2D(-593.468507, -180),
                            new Point2D(-602.468507, -180)},
                        { new Point2D(-600.497944, -196.970562),
                            new Point2D(-606.861905, -190.606601)},
                        { new Point2D(-617.468507, -204),
                            new Point2D(-617.468507, -195)},
                        { new Point2D(-634.439069, -196.970562),
                            new Point2D(-628.075108, -190.606601)},
                        { new Point2D(-641.468507, -180),
                            new Point2D(-632.468507, -180)}
                    }
                }
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Circle,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<CircleTypes, Circle[,]> _circles
            = new Dictionary<CircleTypes, Circle[,]>
            {
                [CircleTypes.CurrentBlasterCircles] = new[,]
                {
                    {
                        new Circle(new Point2D(-617.468507, -180), 25),
                        new Circle(new Point2D(617.468507, -180), 25),
                        new Circle(new Point2D(-617.468507, 185), 25),
                        new Circle(new Point2D(617.468507, 185), 25)
                    }
                },
                [CircleTypes.BlasterDrawingCircles] = new[,]
                {
                    {
                        new Circle(new Point2D(-617.468507, -180), 24),
                        new Circle(new Point2D(-617.468507, 185), 24)
                    },
                    {
                        new Circle(new Point2D(-617.468507, -180), 15),
                        new Circle(new Point2D(-617.468507, 185), 15)
                    }
                }
            };
        
        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<ArcTypes, Arc[,]> _arcs
            = new Dictionary<ArcTypes, Arc[,]>
            {
                [ArcTypes.TipsBaseArcs] = new[,]
                {
                    {
                        new Arc(new Point2D(-617.468507, -180), 5.0990195,
                            new Point2D(-612.468507, -181),
                            new Point2D(-616.468507, -185), -1),

                        new Arc(new Point2D(-617.468507, -180), 5.0990195,
                            new Point2D(-618.468507, -185),
                            new Point2D(-622.468507, -181), -1),

                        new Arc(new Point2D(-617.468507, -180), 5.0990195,
                            new Point2D(-622.468507, -179),
                            new Point2D(-618.468507, -175), -1),

                        new Arc(new Point2D(-617.468507, -180), 5.0990195,
                            new Point2D(-616.468507, -175),
                            new Point2D(-612.468507, -179), -1)
                    },
                    {
                        new Arc(new Point2D(-617.468507, 185), 5.0990195,
                            new Point2D(-612.468507, 186),
                            new Point2D(-616.468507, 190), 1),

                        new Arc(new Point2D(-617.468507, 185), 5.0990195,
                            new Point2D(-618.468507, 190),
                            new Point2D(-622.468507, 186), 1),

                        new Arc(new Point2D(-617.468507, 185), 5.0990195,
                            new Point2D(-622.468507, 184),
                            new Point2D(-618.468507, 180), 1),

                        new Arc(new Point2D(-617.468507, 185), 5.0990195,
                            new Point2D(-616.468507, 180),
                            new Point2D(-612.468507, 184), 1)
                    }
                },
                [ArcTypes.RightAntennaArcs] = new[,]
                {
                    {
                        new Arc(new Point2D(-210 - Difference, 180), 25,
                            new Point2D(-210 - Difference, 155),
                            new Point2D(-210 - Difference, 205), -1),

                        new Arc(new Point2D(-210 - Difference, 180), 20,
                            new Point2D(-210 - Difference, 160),
                            new Point2D(-210 - Difference, 200), -1)
                    },
                    {
                        new Arc(new Point2D(-210 - Difference, -185), 25,
                            new Point2D(-210 - Difference, -160),
                            new Point2D(-210 - Difference, -210), 1),

                        new Arc(new Point2D(-210 - Difference, -185), 20,
                            new Point2D(-210 - Difference, -165),
                            new Point2D(-210 - Difference, -205), 1)
                    }
                },
                [ArcTypes.LeftAntennaArcs] = new[,]
                {
                    {
                        new Arc(new Point2D(210 + Difference, 180), 25,
                            new Point2D(210 + Difference, 155),
                            new Point2D(210 + Difference, 205), 1),

                        new Arc(new Point2D(210 + Difference, 180), 20,
                            new Point2D(210 + Difference, 160),
                            new Point2D(210 + Difference, 200), 1)
                    },
                    {
                        new Arc(new Point2D(210 + Difference, -185), 25,
                            new Point2D(210 + Difference, -160),
                            new Point2D(210 + Difference, -210), -1),

                        new Arc(new Point2D(210 + Difference, -185), 20,
                            new Point2D(210 + Difference, -165),
                            new Point2D(210 + Difference, -205), -1)
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
                            "точки 3D пространства.")]
        [TestCase(-612.468507, -180, -200 - Difference,
            Point3DTypes.SideRightTipPlane)]
        [TestCase(612.468507, -180, -200 - Difference,
            Point3DTypes.SideLeftTipPlane)]
        [TestCase(-641.968507, 185, -600 - Difference,
            Point3DTypes.FrontBlasterBodyPlane)]
        public void TestPlaneGet_CorrectValue(double x, double y, double z,
            Point3DTypes type)
        {
            var expected = new Point3D(x, y, z);
            var actual = _point3DFunc[type]();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тест сеттера и геттера поля CurrentPlane.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера CurrentPlane")]
        public void TestCurrentPlaneSet_CorrectValue()
        {
            const Point3DTypes type = Point3DTypes.CurrentPlane;
            var expected = new Point3D(100, 100, 100);
            Constants.CurrentPlane = expected;
            var actual = _point3DFunc[type]();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тест на возврат поля константы типа Point2D.
        /// </summary>
        /// <param name="type">Параметр поля типа Point2D.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение массива" +
                            "точек для построения отрезков.")]
        [TestCase(Point2DTypes.TipsBaseSegments)]
        [TestCase(Point2DTypes.RightAntennaSegments)]
        [TestCase(Point2DTypes.LeftAntennaSegments)]
        [TestCase(Point2DTypes.BlasterDrawingSegments)]
        public void TestSegmentsGet_CorrectValue(Point2DTypes type)
        {
            var expected = _point2Ds[type];
            var actual = _point2DFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }

        /// <summary>
        /// Тест на возврат поля константы типа Circle.
        /// </summary>
        /// <param name="type">Параметр поля типа Circle.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение массива" +
                            "кругов.")]
        [TestCase(CircleTypes.CurrentBlasterCircles)]
        [TestCase(CircleTypes.BlasterDrawingCircles)]
        public void TestCirclesGet_CorrectValue(CircleTypes type)
        {
            var expected = _circles[type];
            var actual = _circleFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }

        /// <summary>
        /// Тест сеттера поля CurrentBlasterCircles.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера CurrentBlasterCircles")]
        public void TestCurrentBlasterCirclesSet_CorrectValue()
        {
            const CircleTypes type = CircleTypes.CurrentBlasterCircles;
            Circle[,] excepted =
            {
                {
                    new Circle(new Point2D(100, 100), 25),
                    new Circle(new Point2D(-100, 100), 25),
                    new Circle(new Point2D(-100, -100), 25),
                    new Circle(new Point2D(100, -100), 25)
                }
            };
            Constants.CurrentBlasterCircles = excepted;
            var actual = _circleFunc[type]();
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        /// <summary>
        /// Тест на возврат поля константы типа Arc.
        /// </summary>
        /// <param name="type">Параметр поля типа Arc.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение массива дуг.")]
        [TestCase(ArcTypes.TipsBaseArcs)]
        [TestCase(ArcTypes.RightAntennaArcs)]
        [TestCase(ArcTypes.LeftAntennaArcs)]
        public void TestArcsGet_CorrectValue(ArcTypes type)
        {
            var expected = _arcs[type];
            var actual = _arcFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }
    }
}