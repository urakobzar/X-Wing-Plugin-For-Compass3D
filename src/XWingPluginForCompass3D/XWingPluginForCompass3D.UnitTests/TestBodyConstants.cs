using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestBodyConstants
    {
        private const int BodyLength = 300;

        private readonly BodyConstants _constants =
            new BodyConstants(BodyLength);

        private readonly CheckingObjectEquality _check =
            new CheckingObjectEquality();

        [Test(Description = "Позитивный тест геттера UpperBasePlane")]
        public void TestUpperBasePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 0, -600);
            var actual = _constants.UpperBasePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера UpperFacePlane")]
        public void TestUpperFacePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 78.493198, -600 - (BodyLength / 2));
            var actual = _constants.UpperFacePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера LowerFacePlane")]
        public void TestLowerFacePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, -71.493198, -600 - (BodyLength / 2));
            var actual = _constants.LowerFacePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера LowerSideBackPlane")]
        public void TestLowerSideBackPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(-49.470255, -96.493198, -600 - (BodyLength / 2));
            var actual = _constants.LowerSideBackPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BowBodyBackPlane")]
        public void TestBowBodyBackPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 103.3980655, -597.8211064);
            var actual = _constants.BowBodyBackPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера UpperBodyPartFacePlane")]
        public void TestUpperBodyPartFacePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 128.493198, -660);
            var actual = _constants.UpperBodyPartFacePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера DeepBodyPartFacePlane")]
        public void TestDeepBodyPartFacePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 123.493198, -660);
            var actual = _constants.DeepBodyPartFacePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BaseDroidHeadPlane")]
        public void TestBaseDroidHeadPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(-0.2033454, 133.493198, -632.6372698);
            var actual = _constants.BaseDroidHeadPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BackBodyPlane")]
        public void TestBackBodyPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 0, -600 - BodyLength);
            var actual = _constants.BackBodyPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BackDeepPlane")]
        public void TestBackDeepPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(87.3, 11.5, -590 - BodyLength);
            var actual = _constants.BackDeepPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BaseVertexes")]
        public void TestBaseVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.BaseVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера UpperFaceVertexes")]
        public void TestUpperFaceVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.UpperFaceVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера LowerFaceVertexes")]
        public void TestLowerFaceVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.LowerFaceVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BodyCutoutVertexes")]
        public void TestBodyCutoutVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.BodyCutoutVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера LowerBodySliceVertexes")]
        public void TestLowerBodySliceVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(600, 71.493198),
                        new Point2D(600, 121.493198) },
                    { new Point2D (600, 121.493198),
                        new Point2D(650, 121.493198)},
                    { new Point2D(650, 121.493198),
                        new Point2D(600, 71.493198) }
                }
            };
            var actual = _constants.LowerBodySliceVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BowBodyFaceVertexes")]
        public void TestBowBodyFaceVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.BowBodyFaceVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера DeepUpperBodyFaceVertexes")]
        public void TestDeepUpperBodyFaceVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.DeepUpperBodyFaceVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера UpperBodyExtrudingCircles")]
        public void TestUpperBodyExtrudingCirclesGet_CorrectValue()
        {
            Circle[,] excepted =
            {
                {
                    new Circle (new Point2D (-0.2033454, 632.6372698), 25),
                    new Circle (new Point2D (-0.2033454, 750.3556376), 22),
                    new Circle (new Point2D (-0.2033454, 706.4660939), 8),
                    new Circle (new Point2D (-0.2033454, 678.8481419), 8),
                    new Circle (new Point2D (-0.2033454, 750.3556376), 19)
                }
            };
            var actual = _constants.UpperBodyExtrudingCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера UpperBodyExtrudingRectangles")]
        public void TestUpperBodyExtrudingRectanglesGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
                };
            var actual = _constants.UpperBodyExtrudingRectangles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BackBodyDeepVertexes")]
        public void TestBackBodyDeepVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(-97, -4), new Point2D(-20, 123) },
                    { new Point2D(-20, 123), new Point2D(20, 123) },
                    { new Point2D(20, 123), new Point2D(97, -4) },
                    { new Point2D(97, -4), new Point2D(20, -100) },
                    { new Point2D(20, -100), new Point2D(-20, -100) },
                    { new Point2D(-20, -100), new Point2D(-97, -4) }
                }
            };
            var actual = _constants.BackBodyDeepVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера DroidHeadArc")]
        public void TestDroidHeadArcGet_CorrectValue()
        {
            var excepted =
                new Arc(
                    new Point2D(0.2033454, 632.6372698), 25,
                    new Point2D(-0.2463156, 657.6332256),
                    new Point2D(-0.2463156, 607.6413140), 1);
            var actual = _constants.DroidHeadArc;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BackDrawingCircles")]
        public void TestBackDrawingCirclesGet_CorrectValue()
        {
            Circle[,] excepted =
            {
                {
                    new Circle (new Point2D (0, 0), 20)
                }
            };
            var actual = _constants.BackDrawingCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BackDrawingSegments")]
        public void TestBackDrawingSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.BackDrawingSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }
    }
}