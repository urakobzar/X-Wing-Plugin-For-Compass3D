using System;
using System.Collections.Generic;
using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей констант для построения носовой части.
    /// </summary>
    [TestFixture]
    public class TestBowBodyConstants
    {
        /// <summary>
        /// Длина острия носовой части.
        /// </summary>
        private const int BowLength = 50;

        /// <summary>
        /// Объект класса констант для построения носовой части.
        /// </summary>
        private static readonly BowBodyConstants Constants =
            new BowBodyConstants(BowLength);

        /// <summary>
        /// Объект класса для проверки равенства массивов созданных классов.
        /// </summary>
        private readonly CheckingObjectEquality _check = 
            new CheckingObjectEquality();

        /// <summary>
        /// Перечисление полей типа Point3D класса констант носовой части.
        /// </summary>
        public enum Point3DTypes
        {
            UpperFacePlane,
            CockpitFrontFacePlane,
            CockpitSideFacePlane,
            TipFrontBasePlane,
            TipUpperEdgePoint,
            TipLowerEdgePoint,
            TipFrontFilletEdgeCoordinates,
            TipSideFilletEdgeCoordinates
        }

        /// <summary>
        /// Перечисление полей типа Point2D класса констант носовой части.
        /// </summary>
        public enum Point2DTypes
        {
            UpperBaseSegments,
            UpperFaceSegments,
            CockpitCutoutSegments,
            CockpitSliceSegments,
            TipBowBodySegments
        }

        /// <summary>
        /// Перечисление полей типа double для фаски класса констант носовой части.
        /// </summary>
        public enum ChamferTypes
        {
            TipUpperChamferDistances,
            TipLowerChamferDistances
        }

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point3D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point3DTypes, Func<Point3D>> _point3DFunc
            = new Dictionary<Point3DTypes, Func<Point3D>>
            {
                [Point3DTypes.UpperFacePlane] = 
                    () => Constants.UpperFacePlane,
                [Point3DTypes.CockpitFrontFacePlane] = 
                    () => Constants.CockpitFrontFacePlane,
                [Point3DTypes.CockpitSideFacePlane] = 
                    () => Constants.CockpitSideFacePlane,
                [Point3DTypes.TipFrontBasePlane] = 
                    () => Constants.TipFrontBasePlane,
                [Point3DTypes.TipUpperEdgePoint] = 
                    () => Constants.TipUpperEdgePoint,
                [Point3DTypes.TipLowerEdgePoint] = 
                    () => Constants.TipLowerEdgePoint
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point3D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point3DTypes, Func<Point3D[]>> _point3DArrayFunc
            = new Dictionary<Point3DTypes, Func<Point3D[]>>
            {
                [Point3DTypes.TipFrontFilletEdgeCoordinates] =
                    () => Constants.TipFrontFilletEdgeCoordinates,
                [Point3DTypes.TipSideFilletEdgeCoordinates] =
                    () => Constants.TipSideFilletEdgeCoordinates
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Func<Point2D[,,]>> _point2DFunc
            = new Dictionary<Point2DTypes, Func<Point2D[,,]>>
            {
                [Point2DTypes.UpperBaseSegments] =
                    () => Constants.UpperBaseSegments,
                [Point2DTypes.UpperFaceSegments] =
                    () => Constants.UpperFaceSegments,
                [Point2DTypes.CockpitCutoutSegments] =
                    () => Constants.CockpitCutoutSegments,
                [Point2DTypes.CockpitSliceSegments] =
                    () => Constants.CockpitSliceSegments,
                [Point2DTypes.TipBowBodySegments] =
                    () => Constants.TipBowBodySegments
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа double,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<ChamferTypes, Func<double[]>> _chamferFunc
            = new Dictionary<ChamferTypes, Func<double[]>>
            {
                [ChamferTypes.TipUpperChamferDistances] =
                    () => Constants.TipUpperChamferDistances,
                [ChamferTypes.TipLowerChamferDistances] =
                    () => Constants.TipLowerChamferDistances
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<Point3DTypes, Point3D[]> _point3Ds
            = new Dictionary<Point3DTypes, Point3D[]>
            {
                [Point3DTypes.TipFrontFilletEdgeCoordinates] = new[]
                {
                    new Point3D(28.7200562, -7.2591033, 66.3134061),
                    new Point3D(-28.7554958, -7.1964225, 66.4892479),
                    new Point3D(27.6344000, 11.7433937, 67.9976029),
                    new Point3D(-0.1130515, 1.4464850, 90.7356546),
                    new Point3D(-28.2138190, 11.7433937, 67.9976029)
                },
                [Point3DTypes.TipSideFilletEdgeCoordinates] = new[]
                {
                    new Point3D(-38.4795221, 0.3415491, 41.1092127),
                    new Point3D(38.4303857, 0.2844938, 41.0929166)
                }
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Point2D[,,]> _point2Ds
            = new Dictionary<Point2DTypes, Point2D[,,]>
            {
                [Point2DTypes.UpperBaseSegments] = new[, ,]
                {
                    {
                        { new Point2D(-43, 0), new Point2D(-26, 26) },
                        { new Point2D(-26, 26), new Point2D(26, 26) },
                        { new Point2D(26, 26), new Point2D(43, 0) },
                        { new Point2D(43, 0), new Point2D(26, -19) },
                        { new Point2D(26, -19), new Point2D(-26, -19) },
                        { new Point2D(-26, -19), new Point2D(-43, 0) }
                    }
                },
                [Point2DTypes.UpperFaceSegments] = new[, ,]
                {
                    {
                        { new Point2D(54.3956890, 604.5579518),
                            new Point2D(-54.3956890, 604.5579518) },
                        { new Point2D(-54.3956890, 604.5579518),
                            new Point2D(-26, 2.2660493) },
                        { new Point2D(-26, 2.2660493),
                            new Point2D(26, 2.2660493) },
                        { new Point2D(26, 2.2660493),
                            new Point2D(54.3956890, 604.5579518) }
                    }
                },
                [Point2DTypes.CockpitCutoutSegments] = new[, ,]
                {
                    {
                        { new Point2D(-26, 75.9010621),
                            new Point2D(-54.3956890, 75.9010621) },
                        {new Point2D(-54.3956890, 75.9010621),
                            new Point2D(-54.3956890, 25.9010621)},
                        { new Point2D(-54.3956890, 25.9010621),
                            new Point2D(-26, 75.9010621) }
                    }
                },
                [Point2DTypes.CockpitSliceSegments] = new[, ,]
                {
                    {
                        { new Point2D(-1, 25.9010621),
                            new Point2D(-1, 75.9010621) },
                        { new Point2D(-1, 75.9010621),
                            new Point2D(-605.1116164, 75.9010621) },
                        { new Point2D(-605.1116164, 75.9010621),
                            new Point2D(-1, 25.9010621) }
                    }
                },
                [Point2DTypes.TipBowBodySegments] = new[, ,]
                {
                    {
                        { new Point2D(-43, 0), new Point2D(-26, 26) },
                        { new Point2D(-26, 26), new Point2D(25, 26) },
                        { new Point2D(25, 26), new Point2D(43, 0) },
                        { new Point2D(43, 0), new Point2D(26, -19) },
                        { new Point2D(26, -19), new Point2D(-26, -19) },
                        { new Point2D(-26, -19), new Point2D(-43, 0) }
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
        [TestCase(0, 52.2465990, -300,
            Point3DTypes.UpperFacePlane)]
        [TestCase(0, 50.9048674, 2.1788935,
            Point3DTypes.CockpitFrontFacePlane)]
        [TestCase(-39.9138876, 76.6265345, -291.8211064,
            Point3DTypes.CockpitSideFacePlane)]
        [TestCase(0, 0, BowLength,
            Point3DTypes.TipFrontBasePlane)]
        [TestCase(-0.4256897, 17.2511336, 100,
            Point3DTypes.TipUpperEdgePoint)]
        [TestCase(0, -10.2511336, 100,
            Point3DTypes.TipLowerEdgePoint)]
        public void TestPointGet_CorrectValue(double x, double y, double z,
            Point3DTypes type)
        {
            var expected = new Point3D(x, y, z);
            var actual = _point3DFunc[type]();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тест на возврат поля константы типа массива Point3D.
        /// </summary>
        /// <param name="type">Параметр поля типа Point3D.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение массива" +
                            "точек 3D пространства.")]
        [TestCase(Point3DTypes.TipFrontFilletEdgeCoordinates)]
        [TestCase(Point3DTypes.TipSideFilletEdgeCoordinates)]
        public void TestPointsGet_CorrectValue(Point3DTypes type)
        {
            var expected = _point3Ds[type];
            var actual = _point3DArrayFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }

        /// <summary>
        /// Тест на возврат поля константы типа Point2D.
        /// </summary>
        /// <param name="type">Параметр поля типа Point2D.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение массива" +
                            "точек для построения отрезков.")]
        [TestCase(Point2DTypes.UpperBaseSegments)]
        [TestCase(Point2DTypes.UpperFaceSegments)]
        [TestCase(Point2DTypes.CockpitCutoutSegments)]
        [TestCase(Point2DTypes.CockpitSliceSegments)]
        [TestCase(Point2DTypes.TipBowBodySegments)]
        public void TestSegmentsGet_CorrectValue(Point2DTypes type)
        {
            var expected = _point2Ds[type];
            var actual = _point2DFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }

        /// <summary>
        /// Тест на возврат поля константы типа double для фаски.
        /// </summary>
        /// <param name="distance1">Первое расстояние.</param>
        /// <param name="distance2">Второе расстояние.</param>
        /// <param name="type">Параметр поля типа фаски.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение массива" +
                            "расстояний для фаски.")]
        [TestCase(20, 54.9495483, ChamferTypes.TipUpperChamferDistances)]
        [TestCase(15, 55.9807621, ChamferTypes.TipLowerChamferDistances)]
        public void TestChamferDistancesGet_CorrectValue(double distance1,
            double distance2, ChamferTypes type)
        {
            double[] expected = { distance1, distance2 };
            var actual = _chamferFunc[type]();
            Assert.AreEqual(expected, actual);
        }
    }
}