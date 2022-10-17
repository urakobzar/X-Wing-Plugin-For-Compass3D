using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestBowBodyConstants
    {
        private const int BowLength = 10;

        private readonly BowBodyConstants _constants =
            new BowBodyConstants(BowLength);

        private readonly CheckingObjectEquality _check = 
            new CheckingObjectEquality();

        [Test(Description = "Позитивный тест геттера UpperBaseVertexes")]
        public void TestUpperBaseVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted = 
            {
                {
                    { new Point2D(-43, 0), new Point2D(-26, 26) },
                    { new Point2D(-26, 26), new Point2D(26, 26) },
                    { new Point2D(26, 26), new Point2D(43, 0) },
                    { new Point2D(43, 0), new Point2D(26, -19) },
                    { new Point2D(26, -19), new Point2D(-26, -19) },
                    { new Point2D(-26, -19), new Point2D(-43, 0) }
                }
            };
            var actual = _constants.UpperBaseVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера UpperFaceVertexes")]
        public void TestUpperFaceVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.UpperFaceVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера CockpitCutoutVertexes")]
        public void TestCockpitCutoutVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(-26, 75.9010621),
                        new Point2D(-54.3956890, 75.9010621) },
                    {new Point2D(-54.3956890, 75.9010621),
                        new Point2D(-54.3956890, 25.9010621)},
                    { new Point2D(-54.3956890, 25.9010621),
                        new Point2D(-26, 75.9010621) }
                }
            };
            var actual = _constants.CockpitCutoutVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера CockpitSliceVertexes")]
        public void TestCockpitSliceVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(-1, 25.9010621),
                        new Point2D(-1, 75.9010621) },
                    { new Point2D(-1, 75.9010621),
                        new Point2D(-605.1116164, 75.9010621) },
                    { new Point2D(-605.1116164, 75.9010621),
                        new Point2D(-1, 25.9010621) }
                }
            };
            var actual = _constants.CockpitSliceVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipBowBodyVertexes")]
        public void TestTipBowBodyVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(-43, 0), new Point2D(-26, 26) },
                    { new Point2D(-26, 26), new Point2D(25, 26) },
                    { new Point2D(25, 26), new Point2D(43, 0) },
                    { new Point2D(43, 0), new Point2D(26, -19) },
                    { new Point2D(26, -19), new Point2D(-26, -19) },
                    { new Point2D(-26, -19), new Point2D(-43, 0) }
                }
            };
            var actual = _constants.TipBowBodyVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipFrontFilletEdgeCoordinates")]
        public void TestTipFrontFilletEdgeCoordinatesGet_CorrectValue()
        {
            Point3D[] excepted =
            {
                new Point3D(28.7200562, -7.2591033, 66.3134061),
                new Point3D(-28.7554958, -7.1964225, 66.4892479),
                new Point3D(27.6344000, 11.7433937, 67.9976029),
                new Point3D(-0.1130515, 1.4464850, 90.7356546),
                new Point3D(-28.2138190, 11.7433937, 67.9976029)
            };
            var actual = _constants.TipFrontFilletEdgeCoordinates;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipFrontFilletEdgeCoordinates")]
        public void TestTipSideFilletEdgeCoordinatesGet_CorrectValue()
        {
            Point3D[] excepted =
            {
                new Point3D(-38.4795221, 0.3415491, 41.1092127),
                new Point3D(38.4303857, 0.2844938, 41.0929166)
            };
            var actual = _constants.TipSideFilletEdgeCoordinates;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера UpperFacePlane")]
        public void TestUpperFacePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 52.2465990, -300);
            var actual = _constants.UpperFacePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера CockpitFrontFacePlane")]
        public void TestCockpitFrontFacePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 50.9048674, 2.1788935);
            var actual = _constants.CockpitFrontFacePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера CockpitSideFacePlane")]
        public void TestCockpitSideFacePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(-39.9138876, 76.6265345, -291.8211064);
            var actual = _constants.CockpitSideFacePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipFrontBasePlane")]
        public void TestTipFrontBasePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 0, BowLength);
            var actual = _constants.TipFrontBasePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipUpperEdge")]
        public void TestTipUpperEdgeGet_CorrectValue()
        {
            var excepted =
                new Point3D(-0.4256897, 17.2511336, 100);
            var actual = _constants.TipUpperEdge;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipLowerEdge")]
        public void TestTipLowerEdgeGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, -10.2511336, 100);
            var actual = _constants.TipLowerEdge;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipUpperChamferDistances")]
        public void TestTipUpperChamferDistancesGet_CorrectValue()
        {
            double[] excepted = { 20, 54.9495483 };
            var actual = _constants.TipUpperChamferDistances;
            Assert.AreEqual(excepted, actual);
        }

        [Test(Description = "Позитивный тест геттера TipLowerChamferDistances")]
        public void TestTipLowerChamferDistancesGet_CorrectValue()
        {
            double[] excepted = { 15, 55.9807621 };
            var actual = _constants.TipLowerChamferDistances;
            Assert.AreEqual(excepted, actual);
        }
    }
}