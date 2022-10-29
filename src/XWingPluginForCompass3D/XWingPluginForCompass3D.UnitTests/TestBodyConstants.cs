using System;
using System.Collections.Generic;
using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей констант для построения корпуса.
    /// </summary>
    [TestFixture]
    public class TestBodyConstants
    {
        /// <summary>
        /// Длина корпуса.
        /// </summary>
        private const int BodyLength = 300;

        /// <summary>
        /// Высота установок крыши корпуса.
        /// </summary>
        private const int CaseBodySetHeight = 10;

        /// <summary>
        /// Объект класса констант для построения корпуса.
        /// </summary>
        private static readonly BodyConstants Constants =
            new BodyConstants(BodyLength, CaseBodySetHeight);

        /// <summary>
        /// Объект класса для проверки равенства массивов созданных классов.
        /// </summary>
        private readonly CheckingObjectEquality _check =
            new CheckingObjectEquality();

        /// <summary>
        /// Перечисление полей типа Point3D класса констант корпуса.
        /// </summary>
        public enum Point3DTypes
        {
            UpperBasePlane,
            UpperFacePlane,
            LowerFacePlane,
            LowerSideBackPlane,
            BowBodyBackPlane,
            UpperBodyPartFacePlane,
            DeepBodyPartFacePlane,
            BaseDroidHeadPlane,
            BackBodyPlane,
            BackDeepPlane
        }

        /// <summary>
        /// Перечисление полей типа Point2D класса констант корпуса.
        /// </summary>
        public enum Point2DTypes
        {
            BaseSegments,
            UpperFaceSegments,
            LowerFaceSegments,
            BodyCutoutSegments,
            LowerBodySliceSegments,
            BowBodyFaceSegments,
            DeepUpperBodyFaceSegments,
            UpperBodyExtrudingRectanglesSegments,
            BackBodyDeepSegments,
            BackDrawingSegments
        }

        /// <summary>
        /// Перечисление полей типа Circle класса констант корпуса.
        /// </summary>
        public enum CircleTypes
        {
            UpperBodyExtrudingCircles,
            BackDrawingCircles
        }

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point3DTypes, Func<Point3D>> _point3DFunc
            = new Dictionary<Point3DTypes, Func<Point3D>>
            {
                [Point3DTypes.UpperBasePlane] = () => Constants.UpperBasePlane,
                [Point3DTypes.UpperFacePlane] = () => Constants.UpperFacePlane,
                [Point3DTypes.LowerFacePlane] = () => Constants.LowerFacePlane,
                [Point3DTypes.LowerSideBackPlane] = () => Constants.LowerSideBackPlane,
                [Point3DTypes.BowBodyBackPlane] = () => Constants.BowBodyBackPlane,
                [Point3DTypes.UpperBodyPartFacePlane] = () => Constants.UpperBodyPartFacePlane,
                [Point3DTypes.DeepBodyPartFacePlane] = () => Constants.DeepBodyPartFacePlane,
                [Point3DTypes.BaseDroidHeadPlane] = () => Constants.BaseDroidHeadPlane,
                [Point3DTypes.BackBodyPlane] = () => Constants.BackBodyPlane,
                [Point3DTypes.BackDeepPlane] = () => Constants.BackDeepPlane
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Func<Point2D[,,]>> _point2DFunc
            = new Dictionary<Point2DTypes, Func<Point2D[,,]>>
            {
                [Point2DTypes.BaseSegments] =
                    () => Constants.BaseSegments,
                [Point2DTypes.UpperFaceSegments] =
                    () => Constants.UpperFaceSegments,
                [Point2DTypes.LowerFaceSegments] =
                    () => Constants.LowerFaceSegments,
                [Point2DTypes.BodyCutoutSegments] =
                    () => Constants.BodyCutoutSegments,
                [Point2DTypes.LowerBodySliceSegments] =
                    () => Constants.LowerBodySliceSegments,
                [Point2DTypes.BowBodyFaceSegments] =
                    () => Constants.BowBodyFaceSegments,
                [Point2DTypes.DeepUpperBodyFaceSegments] =
                    () => Constants.DeepUpperBodyFaceSegments,
                [Point2DTypes.UpperBodyExtrudingRectanglesSegments] =
                        () => Constants.UpperBodyExtrudingRectanglesSegments,
                [Point2DTypes.BackBodyDeepSegments] =
                    () => Constants.BackBodyDeepSegments,
                [Point2DTypes.BackDrawingSegments] =
                    () => Constants.BackDrawingSegments
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Circle,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<CircleTypes, Func<Circle[,]>> _circleFunc
            = new Dictionary<CircleTypes, Func<Circle[,]>>
            {
                [CircleTypes.UpperBodyExtrudingCircles] =
                    () => Constants.UpperBodyExtrudingCircles,
                [CircleTypes.BackDrawingCircles] =
                    () => Constants.BackDrawingCircles
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Point2D[,,]> _point2Ds
            = new Dictionary<Point2DTypes, Point2D[,,]>
            {
                [Point2DTypes.BaseSegments] = new[, ,]
                {
                    {
                        { new Point2D(108.977589, -4.985001),
                            new Point2D(54.395689, 78.493198) },
                        { new Point2D(54.395689, 78.493198),
                            new Point2D(-54.395689, 78.493198) },
                        { new Point2D(-54.395689, 78.493198),
                            new Point2D(-108.977589, -4.985001) },
                        { new Point2D(-108.977589, -4.985001),
                            new Point2D(-49.470255, -71.493198) },
                        { new Point2D(-49.470255, -71.493198),
                            new Point2D(49.470255, -71.493198) },
                        { new Point2D(49.470255, -71.493198),
                            new Point2D(108.977589, -4.985001) }
                    }
                },
                [Point2DTypes.UpperFaceSegments] = new[, ,]
                {
                    {
                        { new Point2D(-54.395689, 600 + BodyLength),
                            new Point2D(54.395689, 600 + BodyLength) },
                        { new Point2D(54.395689, 600 + BodyLength),
                            new Point2D(54.395689, 600) },
                        { new Point2D(54.395689, 600),
                            new Point2D(-54.395689, 600) },
                        {new Point2D(-54.395689, 600),
                            new Point2D(-54.395689, 600 + BodyLength)}
                    }
                },
                [Point2DTypes.LowerFaceSegments] = new[, ,]
                {
                    {
                        { new Point2D(-49.470255, -600 - BodyLength),
                            new Point2D(49.470255, -600 - BodyLength) },
                        { new Point2D(49.470255, -600 - BodyLength),
                            new Point2D(49.470255, -600) },
                        { new Point2D(49.470255, -600),
                            new Point2D(-49.470255, -600) },
                        { new Point2D(-49.470255, -600),
                            new Point2D(-49.470255, -600 - BodyLength) }
                    }
                },
                [Point2DTypes.BodyCutoutSegments] = new[, ,]
                {
                    {
                        { new Point2D(-49.470255, -121.493198),
                            new Point2D(-49.470255, -71.493198) },
                        { new Point2D (-49.470255, -71.493198),
                            new Point2D(-26.470255, -121.493198) },
                        { new Point2D(-26.470255, -121.493198),
                            new Point2D(-49.470255, -121.493198) }
                    },
                    {
                        { new Point2D(-54.395689, 128.493198),
                            new Point2D(-54.395689, 78.493198) },
                        { new Point2D(-54.395689, 78.493198),
                            new Point2D(-31.395689, 128.493198) },
                        { new Point2D(-31.395689, 128.493198),
                            new Point2D(-54.395689, 128.493198) }
                    }
                },
                [Point2DTypes.LowerBodySliceSegments] = new[, ,]
                {
                    {
                        { new Point2D(600, 71.493198),
                            new Point2D(600, 121.493198) },
                        { new Point2D (600, 121.493198),
                            new Point2D(650, 121.493198)},
                        { new Point2D(650, 121.493198),
                            new Point2D(600, 71.493198) }
                    }
                },
                [Point2DTypes.BowBodyFaceSegments] = new[, ,]
                {
                    {
                        { new Point2D(54.395689, 25.901062),
                            new Point2D(26.115362, 75.697928) },
                        { new Point2D (26.115362, 75.697928),
                            new Point2D(-26, 75.901062150386) },
                        { new Point2D(-26, 75.901062150386),
                            new Point2D(-54.395689, 25.901062) },
                        { new Point2D (-54.395689, 25.901062),
                            new Point2D(54.395689, 25.901062) }
                    }
                },
                [Point2DTypes.DeepUpperBodyFaceSegments] = new[, ,]
                {
                    {
                        { new Point2D(-25, 607.6599144),
                            new Point2D(25, 607.6599144) },
                        { new Point2D (25, 607.6599144),
                            new Point2D(25, 892.6599144) },
                        { new Point2D(25, 892.6599144),
                            new Point2D(-25, 892.6599144) },
                        { new Point2D (-25, 892.6599144),
                            new Point2D(-25, 607.6599144) }
                    }
                },
                [Point2DTypes.UpperBodyExtrudingRectanglesSegments] = new[, ,]
                {
                    {
                        { new Point2D(-19.2033454, 663.7722065),
                            new Point2D(18.7966545, 663.7722065) },
                        { new Point2D(18.7966545, 663.7722065),
                            new Point2D(18.7966545, 721.7722065) },
                        { new Point2D(18.7966545, 721.7722065),
                            new Point2D(-19.2033454, 721.7722065) },
                        { new Point2D(-19.2033454, 721.7722065),
                            new Point2D(-19.2033454, 663.7722065) }
                    },
                    {
                        { new Point2D(-19.2033454, 858.5264552),
                            new Point2D(18.7966545, 858.5264552) },
                        { new Point2D (18.7966545, 858.5264552),
                            new Point2D(18.7966545, 878.5264552) },
                        { new Point2D(18.7966545, 878.5264552),
                            new Point2D(-19.2033454, 878.5264552) },
                        { new Point2D (-19.2033454, 878.5264552),
                            new Point2D(-19.2033454, 858.5264552) }
                    },
                    {
                        { new Point2D(-22.2033454, 784.7158326),
                            new Point2D(21.7966545, 784.7158326) },
                        { new Point2D(21.7966545, 784.7158326),
                            new Point2D(21.7966545, 842.7158326) },
                        { new Point2D(21.7966545, 842.7158326),
                            new Point2D(-22.2033454, 842.7158326) },
                        { new Point2D (-22.2033454, 842.7158326),
                            new Point2D(-22.2033454, 784.7158326) }
                    },
                    {
                        { new Point2D(-15.2033454, 669.2722065),
                            new Point2D(14.7966545, 669.2722065) },
                        { new Point2D(14.7966545, 669.2722065),
                            new Point2D(14.7966545, 716.2722065) },
                        { new Point2D(14.7966545, 716.2722065),
                            new Point2D(-15.2033454, 716.2722065) },
                        {new Point2D (-15.2033454, 716.2722065),
                            new Point2D(-15.2033454, 669.2722065)}
                    },
                    {
                        { new Point2D(-15.2033454, 861.5264552),
                            new Point2D(14.7966545, 861.5264552) },
                        { new Point2D(14.7966545, 861.5264552),
                            new Point2D(14.7966545, 875.5264552) },
                        { new Point2D(14.7966545, 875.5264552),
                            new Point2D(-15.2033454, 875.5264552) },
                        { new Point2D(-15.2033454, 875.5264552),
                            new Point2D(-15.2033454, 861.5264552) }
                    },
                    {
                        { new Point2D(-19.2033454, 788.2158326),
                            new Point2D(18.7966545, 788.2158326) },
                        { new Point2D(18.7966545, 788.2158326),
                            new Point2D(18.7966545, 839.2158326) },
                        { new Point2D(18.7966545, 839.2158326),
                            new Point2D(-19.2033454, 839.2158326) },
                        { new Point2D(-19.2033454, 839.2158326),
                            new Point2D(-19.2033454, 788.2158326) }
                    }
                },
                [Point2DTypes.BackBodyDeepSegments] = new[, ,]
                {
                    {
                        { new Point2D(-97, -4), new Point2D(-20, 123) },
                        { new Point2D(-20, 123), new Point2D(20, 123) },
                        { new Point2D(20, 123), new Point2D(97, -4) },
                        { new Point2D(97, -4), new Point2D(20, -100) },
                        { new Point2D(20, -100), new Point2D(-20, -100) },
                        { new Point2D(-20, -100), new Point2D(-97, -4) }
                    }
                },
                [Point2DTypes.BackDrawingSegments] = new[, ,]
                {
                    {
                        { new Point2D(-20, 40), new Point2D(-20, -100)},
                        { new Point2D(20, -100), new Point2D(20, 40)},
                        { new Point2D(20, 40), new Point2D(-20, 40)},
                        { new Point2D(0, 40), new Point2D(0, 123)},
                        { new Point2D(0, 75), new Point2D(49, 75)},
                        { new Point2D(0, 75), new Point2D(-49, 75)},
                        { new Point2D(-20, 0), new Point2D(-94, 0)},
                        { new Point2D(20, 0), new Point2D(94, 0)},
                        { new Point2D(-68, -40), new Point2D(-20, -40)},
                        { new Point2D(68, -40), new Point2D(20, -40)},
                        { new Point2D(-20, 25), new Point2D(-79, 25)},
                        { new Point2D(20, 25), new Point2D(79, 25)},
                        { new Point2D(0, -20), new Point2D(0, -100)}
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
                [CircleTypes.UpperBodyExtrudingCircles] = new[,]
                {
                    {
                        new Circle (new Point2D (-0.2033454, 632.6372698), 25),
                        new Circle (new Point2D (-0.2033454, 750.3556376), 22),
                        new Circle (new Point2D (-0.2033454, 706.4660939), 8),
                        new Circle (new Point2D (-0.2033454, 678.8481419), 8),
                        new Circle (new Point2D (-0.2033454, 750.3556376), 19)
                    }
                },
                [CircleTypes.BackDrawingCircles] = new[,]
                {
                    {
                        new Circle (new Point2D (0, 0), 20)
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
        [TestCase(0, 0, -600,
            Point3DTypes.UpperBasePlane)]
        [TestCase(0, 78.493198, -600 - (BodyLength / 2),
            Point3DTypes.UpperFacePlane)]
        [TestCase(0, -71.493198, -600 - (BodyLength / 2),
            Point3DTypes.LowerFacePlane)]
        [TestCase(-49.470255, -96.493198, -600 - (BodyLength / 2),
            Point3DTypes.LowerSideBackPlane)]
        [TestCase(0, 103.3980655, -597.8211064,
            Point3DTypes.BowBodyBackPlane)]
        [TestCase(0, 128.493198, -660,
            Point3DTypes.UpperBodyPartFacePlane)]
        [TestCase(0, 123.493198, -660,
            Point3DTypes.DeepBodyPartFacePlane)]
        [TestCase(-0.2033454, 123.493198 + CaseBodySetHeight, -632.6372698,
            Point3DTypes.BaseDroidHeadPlane)]
        [TestCase(0, 0, -600 - BodyLength,
            Point3DTypes.BackBodyPlane)]
        [TestCase(87.3, 11.5, -590 - BodyLength,
            Point3DTypes.BackDeepPlane)]
        public void TestPlaneGet_CorrectValue(double x, double y, double z,
            Point3DTypes type)
        {
            var expected = new Point3D(x, y, z);
            var actual = _point3DFunc[type]();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тест на возврат поля константы типа Point2D.
        /// </summary>
        /// <param name="type">Параметр поля типа Point2D.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение массива" +
                            "точек для построения отрезков.")]
        [TestCase(Point2DTypes.BaseSegments)]
        [TestCase(Point2DTypes.UpperFaceSegments)]
        [TestCase(Point2DTypes.LowerFaceSegments)]
        [TestCase(Point2DTypes.BodyCutoutSegments)]
        [TestCase(Point2DTypes.LowerBodySliceSegments)]
        [TestCase(Point2DTypes.BowBodyFaceSegments)]
        [TestCase(Point2DTypes.DeepUpperBodyFaceSegments)]
        [TestCase(Point2DTypes.UpperBodyExtrudingRectanglesSegments)]
        [TestCase(Point2DTypes.BackBodyDeepSegments)]
        [TestCase(Point2DTypes.BackDrawingSegments)]
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
        [TestCase(CircleTypes.UpperBodyExtrudingCircles)]
        [TestCase(CircleTypes.BackDrawingCircles)]
        public void TestCirclesGet_CorrectValue(CircleTypes type)
        {
            var expected = _circles[type];
            var actual = _circleFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }

        /// <summary>
        /// Тест сеттера поля DroidHeadArc.
        /// </summary>
        [Test(Description = "Позитивный тест геттера DroidHeadArc.")]
        public void TestDroidHeadArcGet_CorrectValue()
        {
            var excepted =
                new Arc(
                    new Point2D(0.2033454, 632.6372698), 25,
                    new Point2D(-0.2463156, 657.6332256),
                    new Point2D(-0.2463156, 607.6413140), 1);
            var actual = Constants.DroidHeadArc;
            Assert.IsTrue(actual.Equals(excepted));
        }
    }
}