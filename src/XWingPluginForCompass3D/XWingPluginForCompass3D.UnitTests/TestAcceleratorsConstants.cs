using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestAcceleratorsConstants
    {
        private const int Difference = 20;

        private readonly AcceleratorsConstants _constants =
            new AcceleratorsConstants(Difference);

        private readonly CheckingObjectEquality _check =
            new CheckingObjectEquality();

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

        [Test(Description = "Позитивный тест геттера AirIntakePlane")]
        public void TestAirIntakePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(112.6678295, 104.4724056, -570 - Difference);
            var actual = _constants.AirIntakePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TurbinePlane")]
        public void TestTurbinePlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(121.2172883, 104.4724056, -800 - Difference);
            var actual = _constants.TurbinePlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AcceleratorsBaseVertexes")]
        public void TestAcceleratorsBaseVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(-69.2385653, 55.7923285),
                        new Point2D(-54.3956889, 78.4931981) },
                    { new Point2D(-54.3956889, 78.4931981),
                        new Point2D(-39.1421342, 111.653099) },
                    { new Point2D(-39.1421342, 111.653099),
                        new Point2D(-208.9101823, 95.0731489) },
                    { new Point2D(-208.9101823, 95.0731489),
                        new Point2D(-216.3265224, 85.2054693) },
                    { new Point2D(-216.3265224, 85.2054693),
                        new Point2D(-69.2385653, 55.7923285) }
                },
                {
                    { new Point2D(-64.7392536, -54.4278464),
                        new Point2D(-49.4702548, -71.4931980) },
                    { new Point2D(-49.4702548, -71.4931980),
                        new Point2D(-34.2049246, -104.678698) },
                    { new Point2D(-34.2049246, -104.678698),
                        new Point2D(-211.8743904, -90.5910027) },
                    { new Point2D(-211.8743904, -90.5910027),
                        new Point2D(-216.6993372, -82.9936421) },
                    { new Point2D(-216.6993372, -82.9936421),
                        new Point2D(-64.7392536, -54.4278464) }
                }
            };
            var actual = _constants.AcceleratorsBaseVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeCircles")]
        public void TestAirIntakeCirclesGet_CorrectValue()
        {
            Circle[,] excepted =
            {
                {
                    new Circle(new Point2D(112.667829571579,
                        104.472405663619), 40),
                    new Circle(new Point2D(105.609456757216,
                        -99.016919293413), 40),
                    new Circle(new Point2D(-112.667829571579,
                        104.472405663619), 40),
                    new Circle(new Point2D(-105.609456757216,
                        -99.016919293413), 40)
                }
            };
            var actual = _constants.AirIntakeCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест сеттера TurbineCircles")]
        public void TestTurbineCirclesSet_CorrectValue()
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
            _constants.TurbineCircles = excepted;
            var actual = _constants.TurbineCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера TurbineCircles")]
        public void TestTurbineCirclesGet_CorrectValue()
        {
            Circle[,] excepted =
            {
                {
                    new Circle(new Point2D(109.6678295,
                        107.4724056), 33),
                    new Circle(new Point2D(102.6094567,
                        -102.0169192), 33),
                    new Circle(new Point2D(-109.6678295,
                        107.4724056), 33),
                    new Circle(new Point2D(-102.6094567,
                        -102.0169192), 33)
                }
            };
            var actual = _constants.TurbineCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeBaseCuttingArcs")]
        public void TestAirIntakeBaseCuttingArcsGet_CorrectValue()
        {
            Arc[,] excepted =
            {
                {
                    new Arc(new Point2D(112.6678295, 104.4724056), 35,
                    new Point2D(77.8054652, 101.3715100),
                    new Point2D(146.4883048, 113.4821477), -1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 35,
                    new Point2D(78.1995582, 98.39471944),
                    new Point2D(147.1361009, 110.5500918), 1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 15,
                    new Point2D(97.6732261, 104.8747323),
                    new Point2D(126.6205439, 109.9789255), -1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 15,
                    new Point2D(97.8957132, 101.8676829),
                    new Point2D(127.4399458, 107.0771283), 1)
                },
                {
                    new Arc(new Point2D(-112.6678295, 104.4724056), 35,
                    new Point2D(-146.4883048, 113.4821477),
                    new Point2D(-77.8054652, 101.3715100), -1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 35,
                    new Point2D(-147.1361009, 110.5500918),
                    new Point2D(-78.1995582, 98.39471944), 1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 15,
                    new Point2D(-126.6205439, 109.9789255),
                    new Point2D(-97.6732261, 104.8747323), -1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 15,
                    new Point2D(-127.4399458, 107.0771283),
                    new Point2D(-97.8957132, 101.8676829), 1)
                },
                {
                    new Arc(new Point2D(105.6094567, -99.0169192), 35,
                    new Point2D(71.1411854, -92.9392330),
                    new Point2D(140.0777281, -105.0946055), -1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 35,
                    new Point2D(70.7470924, -95.9160236),
                    new Point2D(139.4299320, -108.0266614), 1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 15,
                    new Point2D(90.8373404, -96.4121966),
                    new Point2D(120.3815730, -101.6216419), -1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 15,
                    new Point2D(90.6148532, -99.4192459),
                    new Point2D(119.5621711, -104.5234391), 1)
                },
                {
                    new Arc(new Point2D(-105.6094567, -99.0169192), 35,
                    new Point2D(-140.0777281, -105.0946055),
                    new Point2D(-71.1411854, -92.9392330), -1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 35,
                    new Point2D(-139.4299320, -108.0266614),
                    new Point2D(-70.7470924, -95.9160236), 1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 15,
                    new Point2D(-120.3815730, -101.6216419),
                    new Point2D(-90.8373404, -96.4121966), -1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 15,
                    new Point2D(-119.5621711, -104.5234391),
                    new Point2D(-90.6148532, -99.4192459), 1)
                }
            };
            var actual = _constants.AirIntakeBaseCuttingArcs;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeBaseCuttingSegments")]
        public void TestAirIntakeBaseCuttingSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(77.8054652, 101.3715100),
                        new Point2D(97.6732261, 104.8747323)},
                    { new Point2D(126.6205439, 109.9789255),
                        new Point2D(146.4883048, 113.4821477)},
                    { new Point2D(97.8957132, 101.8676829),
                        new Point2D(78.1995582, 98.3947194)},
                    { new Point2D(127.4399458, 107.0771283),
                        new Point2D(147.1361009, 110.5500918)}
                },
                {
                    { new Point2D(-146.4883048, 113.4821477),
                    new Point2D(-126.6205439, 109.9789255)},
                    { new Point2D(-147.1361009, 110.5500918),
                    new Point2D(-127.4399458, 107.0771283)},
                    { new Point2D(-97.6732261, 104.8747323),
                    new Point2D(-77.8054652, 101.3715100)},
                    { new Point2D(-97.8957132, 101.8676829),
                    new Point2D(-78.1995582, 98.3947194)}
                },
                {
                    { new Point2D(71.1411854, -92.9392330),
                    new Point2D(90.8373404, -96.4121966)},
                    { new Point2D(70.7470924, -95.9160236),
                    new Point2D(90.6148532, -99.4192459)},
                    { new Point2D(120.3815730, -101.6216419),
                    new Point2D(140.0777281, -105.0946055)},
                    { new Point2D(119.5621711, -104.5234391),
                    new Point2D(139.4299320, -108.0266614)}
                },
                {
                    { new Point2D(-140.0777281, -105.0946055),
                    new Point2D(-120.3815730, -101.6216419)},
                    { new Point2D(-90.8373404, -96.4121966),
                    new Point2D(-71.1411854, -92.9392330)},
                    { new Point2D(-139.4299320, -108.0266614),
                    new Point2D(-119.5621711, -104.5234391)},
                    { new Point2D(-90.6148532, -99.4192459),
                    new Point2D(-70.7470924, -95.9160236)}
                }
            };
            var actual = _constants.AirIntakeBaseCuttingSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeLowerCuttingArcs")]
        public void TestAirIntakeLowerCuttingArcsGet_CorrectValue()
        {
            Arc[,] excepted =
            {
                {
                    new Arc(new Point2D(112.6678295, 104.4724056), 40,
                        new Point2D(73.2755194, 97.5264785),
                        new Point2D(152.0601396, 111.4183327), 1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 35,
                        new Point2D(78.1995582, 98.3947194),
                        new Point2D(147.1361009, 110.5500918), 1)
                },
                {
                    new Arc(new Point2D(-112.6678295, 104.4724056), 40,
                        new Point2D(-152.0601396, 111.4183327),
                        new Point2D(-73.2755194, 97.5264785), 1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 35,
                        new Point2D(-147.1361009, 110.5500918),
                        new Point2D(-78.1995582, 98.3947194), 1)
                },
                {
                    new Arc(new Point2D(105.6094567, -99.0169192), 40,
                        new Point2D(66.2171466, -92.0709921),
                        new Point2D(145.0017668, -105.9628464), -1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 35,
                        new Point2D(71.1411854, -92.9392330),
                        new Point2D(140.0777281, -105.0946055), -1)
                },
                {
                    new Arc(new Point2D(-105.6094567, -99.0169192), 40,
                        new Point2D(-145.0017668, -105.9628464),
                        new Point2D(-66.2171466, -92.0709921), -1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 35,
                        new Point2D(-140.0777281, -105.0946055),
                        new Point2D(-71.1411854, -92.9392330), -1)
                }
            };
            var actual = _constants.AirIntakeLowerCuttingArcs;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeLowerCuttingSegments")]
        public void TestAirIntakeLowerCuttingSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(78.1995582, 98.3947194),
                        new Point2D(73.2755194, 97.5264785)},
                    { new Point2D(147.1361009, 110.5500918),
                        new Point2D(152.0601396, 111.4183327)}
                },
                {
                    { new Point2D(-147.1361009, 110.5500918),
                        new Point2D(-152.0601396, 111.4183327)},
                    { new Point2D(-78.1995582, 98.3947194),
                        new Point2D(-73.2755194, 97.5264785)}
                },
                {
                    { new Point2D(66.2171466, -92.0709921),
                        new Point2D(71.1411854, -92.9392330)},
                    { new Point2D(140.0777281, -105.0946055),
                        new Point2D(145.0017668, -105.9628464)}
                },
                {
                    { new Point2D(-140.0777281, -105.0946055),
                        new Point2D(-145.0017668, -105.9628464)},
                    { new Point2D(-71.1411854, -92.9392330),
                        new Point2D(-66.2171466, -92.0709921)}
                }
            };
            var actual = _constants.AirIntakeLowerCuttingSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeMiddleCuttingArcs")]
        public void TestAirIntakeMiddleCuttingArcsGet_CorrectValue()
        {
            Arc[,] excepted =
            {
                {
                    new Arc(new Point2D(112.6678295, 104.4724056), 10,
                        new Point2D(102.7524178, 105.7703308),
                        new Point2D(121.5413522, 109.0833269), -1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 10,
                        new Point2D(102.8197520, 102.7359238),
                        new Point2D(122.5159071, 106.2088874), 1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 15,
                        new Point2D(97.6732261, 104.8747323),
                        new Point2D(126.6205439, 109.9789255), -1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 15,
                        new Point2D(97.8957132, 101.8676829),
                        new Point2D(127.4399458, 107.0771283), 1)
                },
                {
                    new Arc(new Point2D(-112.6678295, 104.4724056), 10,
                        new Point2D(-121.5413522, 109.0833269),
                        new Point2D(-102.7524178, 105.7703308), -1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 10,
                        new Point2D(-122.5159071, 106.2088874),
                        new Point2D(-102.8197520, 102.7359238), 1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 15,
                        new Point2D(-126.6205439, 109.9789255),
                        new Point2D(-97.6732261, 104.8747323), -1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 15,
                        new Point2D(-127.4399458, 107.0771283),
                        new Point2D(-97.8957132, 101.8676829), 1)
                },
                {
                    new Arc(new Point2D(105.6094567, -99.0169192), 10,
                        new Point2D(95.7613792, -97.2804375),
                        new Point2D(115.4575342, -100.7534010), -1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 10,
                        new Point2D(95.6940450, -100.3148445),
                        new Point2D(114.4829794, -103.6278405), 1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 15,
                        new Point2D(90.8373404, -96.4121966),
                        new Point2D(120.3815730, -101.6216419), -1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 15,
                        new Point2D(90.6148532, -99.4192459),
                        new Point2D(119.5621711, -104.5234391), 1)
                },
                {
                    new Arc(new Point2D(-105.6094567, -99.0169192), 10,
                        new Point2D(-115.4575342, -100.7534010),
                        new Point2D(-95.7613792, -97.2804375), -1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 10,
                        new Point2D(-114.4829794, -103.6278405),
                        new Point2D(-95.6940450, -100.3148445), 1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 15,
                        new Point2D(-120.3815730, -101.6216419),
                        new Point2D(-90.8373404, -96.4121966), -1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 15,
                        new Point2D(-119.5621711, -104.5234391),
                        new Point2D(-90.6148532, -99.4192459), 1)
                }
            };
            var actual = _constants.AirIntakeMiddleCuttingArcs;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeMiddleCuttingSegments")]
        public void TestAirIntakeMiddleCuttingSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(102.7524178, 105.7703308),
                        new Point2D(97.6732261, 104.8747323)},
                    { new Point2D(121.5413522, 109.0833269),
                        new Point2D(126.6205439, 109.9789255)},
                    { new Point2D(127.4399458, 107.0771283),
                        new Point2D(122.5159071, 106.2088874)},
                    { new Point2D(102.8197520, 102.7359238),
                        new Point2D(97.8957132, 101.8676829)}
                },
                {
                    { new Point2D(-126.6205439, 109.9789255),
                        new Point2D(-121.5413522, 109.0833269)},
                    { new Point2D(-127.4399458, 107.0771283),
                        new Point2D(-122.5159071, 106.2088874)},
                    { new Point2D(-102.7524178, 105.7703308),
                        new Point2D(-97.6732261, 104.8747323)},
                    { new Point2D(-102.8197520, 102.7359238),
                        new Point2D(-97.8957132, 101.8676829)}
                },
                {
                    { new Point2D(90.8373404, -96.4121966),
                        new Point2D(95.7613792, -97.2804375)},
                    { new Point2D(90.6148532, -99.4192459),
                        new Point2D(95.6940450, -100.3148445)},
                    { new Point2D(115.4575342, -100.7534010),
                        new Point2D(120.3815730, -101.6216419)},
                    { new Point2D(114.4829794, -103.6278405),
                        new Point2D(119.5621711, -104.5234391)}
                },
                {
                    { new Point2D(-120.3815730, -101.6216419),
                        new Point2D(-115.4575342, -100.7534010)},
                    { new Point2D(-95.7613792, -97.2804375),
                        new Point2D(-90.8373404, -96.4121966)},
                    { new Point2D(-90.6148532, -99.4192459),
                        new Point2D(-95.6940450, -100.3148445)},
                    { new Point2D(-114.4829794, -103.6278405),
                        new Point2D(-119.5621711, -104.5234391)}
                }
            };
            var actual = _constants.AirIntakeMiddleCuttingSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeSmallCuttingArcs")]
        public void TestAirIntakeSmallCuttingArcsGet_CorrectValue()
        {
            Arc[,] excepted = 
            {
                {
                    new Arc(new Point2D(112.6678295, 104.4724056), 10,
                        new Point2D(102.7524178, 105.7703308),
                        new Point2D(121.5413522, 109.0833269), -1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 10,
                        new Point2D(102.8197520, 102.7359238),
                        new Point2D(122.5159071, 106.2088874), 1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 5,
                        new Point2D(108.2076540, 106.7322362),
                        new Point2D(116.0861160, 108.1214216), -1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 5,
                        new Point2D(107.7437908, 103.6041647),
                        new Point2D(117.5918683, 105.3406465), 1)
                },
                {
                    new Arc(new Point2D(-112.6678295, 104.4724056), 10,
                        new Point2D(-121.5413522, 109.0833269),
                        new Point2D(-102.7524178, 105.7703308), -1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 10,
                        new Point2D(-122.5159071, 106.2088874),
                        new Point2D(-102.8197520, 102.7359238), 1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 5,
                        new Point2D(-116.0861160, 108.1214216),
                        new Point2D(-108.2076540, 106.7322362), -1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 5,
                        new Point2D(-117.5918683, 105.3406465),
                        new Point2D(-107.7437908, 103.6041647), 1)
                },
                {
                    new Arc(new Point2D(105.6094567, -99.0169192), 10,
                        new Point2D(95.7613792, -97.2804375),
                        new Point2D(115.4575342, -100.7534010), -1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 10,
                        new Point2D(95.6940450, -100.3148445),
                        new Point2D(114.4829794, -103.6278405), 1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 5,
                        new Point2D(100.6854179, -98.1486784),
                        new Point2D(110.5334955, -99.8851601), -1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 5,
                        new Point2D(101.1492812, -101.2767498),
                        new Point2D(109.0277432, -102.6659352), 1)
                },
                {
                    new Arc(new Point2D(-105.6094567, -99.0169192), 10,
                        new Point2D(-115.4575342, -100.7534010),
                        new Point2D(-95.7613792, -97.2804375), -1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 10,
                        new Point2D(-114.4829794, -103.6278405),
                        new Point2D(-95.6940450, -100.3148445), 1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 5,
                        new Point2D(-110.5334955, -99.8851601),
                        new Point2D(-100.6854179, -98.1486784), -1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 5,
                        new Point2D(-109.0277432, -102.6659352),
                        new Point2D(-101.1492812, -101.2767498), 1)
                }
            };
            var actual = _constants.AirIntakeSmallCuttingArcs;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakeSmallCuttingSegments")]
        public void TestAirIntakeSmallCuttingSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(108.2076540, 106.7322362),
                        new Point2D(102.7524178, 105.7703308)},
                    { new Point2D(116.0861160, 108.1214216),
                        new Point2D(121.5413522, 109.0833269)},
                    { new Point2D(122.5159071, 106.2088874),
                        new Point2D(117.5918683, 105.3406465)},
                    { new Point2D(107.7437908, 103.6041647),
                        new Point2D(102.8197520, 102.7359238)}
                },
                {
                    { new Point2D(-116.0861160, 108.1214216),
                        new Point2D(-121.5413522, 109.0833269)},
                    { new Point2D(-108.2076540, 106.7322362),
                        new Point2D(-102.7524178, 105.7703308)},
                    { new Point2D(-117.5918683, 105.3406465),
                        new Point2D(-122.5159071, 106.2088874)},
                    { new Point2D(-107.7437908, 103.6041647),
                        new Point2D(-102.8197520, 102.7359238)}
                },
                {
                    { new Point2D(95.7613792, -97.2804375),
                        new Point2D(100.6854179, -98.1486784)},
                    { new Point2D(110.5334955, -99.8851601),
                        new Point2D(115.4575342, -100.7534010)},
                    { new Point2D(95.6940450, -100.3148445),
                        new Point2D(101.1492812, -101.2767498)},
                    { new Point2D(109.0277432, -102.6659352),
                        new Point2D(114.4829794, -103.6278405)}
                },
                {
                    { new Point2D(-115.4575342, -100.7534010),
                        new Point2D(-110.5334955, -99.8851601)},
                    { new Point2D(-100.6854179, -98.1486784),
                        new Point2D(-95.7613792, -97.2804375)},
                    { new Point2D(-95.6940450, -100.3148445),
                        new Point2D(-101.1492812, -101.2767498)},
                    { new Point2D(-109.0277432, -102.6659352),
                        new Point2D(-114.4829794, -103.6278405)}
                }
            };
            var actual = _constants.AirIntakeSmallCuttingSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakePartitionArcs")]
        public void TestAirIntakePartitionArcsGet_CorrectValue()
        {
            Arc[,] excepted =
            {
                {
                    new Arc(new Point2D(112.6678295, 104.4724056), 5,
                        new Point2D(110.8323229, 109.1233104),
                        new Point2D(112.8019384, 109.4706068), -1),

                    new Arc(new Point2D(112.6678295, 104.4724056), 35,
                        new Point2D(105.6078167, 138.7529572),
                        new Point2D(107.5774323, 139.1002536), -1)
                },
                {
                    new Arc(new Point2D(-112.6678295, 104.4724056), 5,
                        new Point2D(-112.8019384, 109.4706068),
                        new Point2D(-110.8323229, 109.1233104), -1),

                    new Arc(new Point2D(-112.6678295, 104.4724056), 35,
                        new Point2D(-107.5774323, 139.1002536),
                        new Point2D(-105.6078167, 138.7529572), -1)
                },
                {
                    new Arc(new Point2D(105.6094567, -99.0169192), 5,
                        new Point2D(103.7739501, -103.6678240),
                        new Point2D(105.7435656, -104.0151204), 1),

                    new Arc(new Point2D(105.6094567, -99.0169192), 35,
                        new Point2D(98.5494439, -133.2974709),
                        new Point2D(100.5190594, -133.6447672), 1)
                },
                {
                    new Arc(new Point2D(-105.6094567, -99.0169192), 5,
                        new Point2D(-105.7435656, -104.0151204),
                        new Point2D(-103.7739501, -103.6678240), 1),

                    new Arc(new Point2D(-105.6094567, -99.0169192), 35,
                        new Point2D(-100.5190594, -133.6447672),
                        new Point2D(-98.5494439, -133.2974709), 1)
                }
            };
            var actual = _constants.AirIntakePartitionArcs;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера AirIntakePartitionSegments")]
        public void TestAirIntakePartitionSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(105.6078167, 138.7529572),
                        new Point2D(110.8323229, 109.1233104)},
                    { new Point2D(107.5774323, 139.1002536),
                        new Point2D(112.8019384, 109.4706068)}
                },
                {
                    { new Point2D(-112.8019384, 109.4706068),
                        new Point2D(-107.5774323, 139.1002536)},
                    { new Point2D(-105.6078167, 138.7529572),
                        new Point2D(-110.8323229, 109.1233104)}
                },
                {
                    { new Point2D(103.7739501, -103.6678240),
                        new Point2D(98.5494439, -133.2974709)},
                    { new Point2D(100.5190594, -133.6447672),
                        new Point2D(105.7435656, -104.0151204)}
                },
                {
                    { new Point2D(-105.7435656, -104.0151204),
                        new Point2D(-100.5190594, -133.6447672)},
                    { new Point2D(-98.5494439, -133.2974709),
                        new Point2D(-103.7739501, -103.6678240)}
                }
            };
            var actual = _constants.AirIntakePartitionSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера NozzleDrawingSegments")]
        public void TestNozzleDrawingSegmentsGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(-109.6678295, 140.4724056),
                        new Point2D(-109.6678295, 122.4724056)},
                    { new Point2D(-133.0023533, 130.8069294),
                        new Point2D(-120.2744312, 118.0790073)},
                    { new Point2D(-142.6678295, 107.4724056),
                        new Point2D(-124.6678295, 107.4724056)},
                    { new Point2D(-133.0023533, 84.1378818),
                        new Point2D(-120.2744312, 96.8658039)},
                    { new Point2D(-109.6678295, 74.4724056),
                        new Point2D(-109.6678295, 92.4724056)},
                    { new Point2D(-86.3333057, 84.1378818),
                        new Point2D(-99.0612278, 96.8658039)},
                    { new Point2D(-76.6678295, 107.4724056),
                        new Point2D(-94.6678295, 107.4724056)},
                    { new Point2D(-86.3333057, 130.8069294),
                        new Point2D(-99.0612278, 118.0790073)}
                },
                {
                    { new Point2D(-79.2749329, -78.6823955),
                        new Point2D(-92.0028550, -91.4103175)},
                    { new Point2D(-102.6094567, -69.0169192),
                        new Point2D(-102.6094567, -87.0169192)},
                    { new Point2D(-125.9439805, -78.6823955),
                        new Point2D(-113.2160584, -91.4103175)},
                    { new Point2D(-135.6094567, -102.0169192),
                        new Point2D(-117.6094567, -102.0169192)},
                    { new Point2D(-125.9439805, -125.3514430),
                        new Point2D(-113.2160584, -112.6235210)},
                    { new Point2D(-102.6094567, -135.0169192),
                        new Point2D(-102.6094567, -117.0169192)},
                    { new Point2D(-79.2749329, -125.3514430),
                        new Point2D(-92.0028550, -112.6235210)},
                    { new Point2D(-69.6094567, -102.0169192),
                        new Point2D(-87.6094567, -102.0169192)}
                }
            };
            var actual = _constants.NozzleDrawingSegments;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера NozzleDrawingCircles")]
        public void TestNozzleDrawingCirclesGet_CorrectValue()
        {
            Circle[,] excepted =
            {
                {
                    new Circle(new Point2D(109.6678295,
                        107.4724056), 33),
                    new Circle(new Point2D(102.6094567,
                        -102.0169192), 33)
                },
                {
                    new Circle(new Point2D(109.6678295,
                        107.4724056), 15),
                    new Circle(new Point2D(102.6094567,
                        -102.0169192), 15),
                }
            };
            var actual = _constants.NozzleDrawingCircles;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }
        
    }
}
