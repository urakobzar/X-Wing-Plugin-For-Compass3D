using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestBlastersConstants
    {
        private const int Difference = 20;

        private readonly BlastersConstants _constants =
            new BlastersConstants(Difference);

        private readonly CheckingObjectEquality _check =
            new CheckingObjectEquality();

        [Test(Description = "Позитивный тест геттера SideRightTipPlane")]
        public void TestSideRightTipPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(-612.468507, -180, -200 - Difference);
            var actual = _constants.SideRightTipPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера SideLeftTipPlane")]
        public void TestSideLeftTipPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(612.468507, -180, -200 - Difference);
            var actual = _constants.SideLeftTipPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера FrontBlasterBodyPlane")]
        public void TestFrontBlasterBodyPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(-641.968507, 185, -600 - Difference);
            var actual = _constants.FrontBlasterBodyPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест сеттера CurrentPlane")]
        public void TestCurrentPlaneSet_CorrectValue()
        {
            var excepted =
                new Point3D(100, 100, 100);
            _constants.CurrentPlane = excepted;
            var actual = _constants.CurrentPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера CurrentPlane")]
        public void TestCurrentPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(98.760456, 54.834961, -600 - Difference);
            var actual = _constants.CurrentPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест сеттера CurrentBlasterCircles")]
        public void TestCurrentBlasterCirclesSet_CorrectValue()
        {
            Circle[,] excepted =
            {
                {
                    new Circle(new Point2D(100, 100), 25),
                    new Circle(new Point2D(-100, 100), 25),
                    new Circle(new Point2D(-100, -100), 25),
                    new Circle(new Point2D(100, -100), 25)
                }
            };
            _constants.CurrentBlasterCircles = excepted;
            var actual = _constants.CurrentBlasterCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера CurrentBlasterCircles")]
        public void TestCurrentBlasterCirclesGet_CorrectValue()
        {
            Circle[,] excepted =
            {
                {
                    new Circle(new Point2D(-617.468507, -180), 25),
                    new Circle(new Point2D(617.468507, -180), 25),
                    new Circle(new Point2D(-617.468507, 185), 25),
                    new Circle(new Point2D(617.468507, 185), 25)
                }
            };
            var actual = _constants.CurrentBlasterCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipsBaseSegments")]
        public void TestTipsBaseSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.TipsBaseSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TipsBaseArcs")]
        public void TestTipsBaseArcsGet_CorrectValue()
        {
            Arc[,] excepted =
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
            };
            var actual = _constants.TipsBaseArcs;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера RightAntennaSegments")]
        public void TestRightAntennaSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.RightAntennaSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера RightAntennaArcs")]
        public void TestRightAntennaArcsGet_CorrectValue()
        {
            Arc[,] excepted =
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
            };
            var actual = _constants.RightAntennaArcs;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера LeftAntennaSegments")]
        public void TestLeftAntennaSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.LeftAntennaSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера LeftAntennaArcs")]
        public void TestLeftAntennaArcsGet_CorrectValue()
        {
            Arc[,] excepted =
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
            };
            var actual = _constants.LeftAntennaArcs;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BlasterDrawingSegments")]
        public void TestBlasterDrawingSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
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
            };
            var actual = _constants.BlasterDrawingSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BlasterDrawingCircles")]
        public void TestBlasterDrawingCirclesGet_CorrectValue()
        {
            Circle[,] excepted =
            {
                {
                    new Circle(new Point2D(-617.468507, -180), 24),
                    new Circle(new Point2D(-617.468507, 185), 24)
                },
                {
                    new Circle(new Point2D(-617.468507, -180), 15),
                    new Circle(new Point2D(-617.468507, 185), 15)
                }
            };
            var actual = _constants.BlasterDrawingCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }
    }
}