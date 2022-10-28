using System;
using System.Collections.Generic;
using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей констант для построения ускорителей.
    /// </summary>
    [TestFixture]
    public class TestAcceleratorsConstants
    {
        /// <summary>
        /// Разница между шириной крыла и длиной корпуса.
        /// </summary>
        private const int Difference = 20;

        /// <summary>
        /// Объект класса констант для построения ускорителей.
        /// </summary>
        private static readonly AcceleratorsConstants Constants =
            new AcceleratorsConstants(Difference);

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
            CurrentPlane,
            AirIntakePlane,
            TurbinePlane
        }

        /// <summary>
        /// Перечисление полей типа Point2D класса констант ускорителей.
        /// </summary>
        public enum Point2DTypes
        {
            AcceleratorsBaseSegments,
            AirIntakeBaseCuttingSegments,
            AirIntakeLowerCuttingSegments,
            AirIntakeMiddleCuttingSegments,
            AirIntakeSmallCuttingSegments,
            AirIntakePartitionSegments,
            NozzleDrawingSegments
        }

        /// <summary>
        /// Перечисление полей типа Circle класса констант ускорителей.
        /// </summary>
        public enum  CircleTypes
        {
            AirIntakeCircles,
            TurbineCircles,
            NozzleDrawingCircles
        }

        /// <summary>
        /// Перечисление полей типа Arc класса констант ускорителей.
        /// </summary>
        public enum ArcTypes
        {
            AirIntakeBaseCuttingArcs,
            AirIntakeLowerCuttingArcs,
            AirIntakeMiddleCuttingArcs,
            AirIntakeSmallCuttingArcs,
            AirIntakePartitionArcs
        }

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point3D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point3DTypes, Func<Point3D>> _point3DFunc
            = new Dictionary<Point3DTypes, Func<Point3D>>
            {
                [Point3DTypes.CurrentPlane] = () => Constants.CurrentPlane,
                [Point3DTypes.AirIntakePlane] = () => Constants.AirIntakePlane,
                [Point3DTypes.TurbinePlane] = () => Constants.TurbinePlane,
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Func<Point2D[,,]>> _point2DFunc
            = new Dictionary<Point2DTypes, Func<Point2D[,,]>>
            {
                [Point2DTypes.AcceleratorsBaseSegments] = 
                    () => Constants.AcceleratorsBaseSegments,
                [Point2DTypes.AirIntakeBaseCuttingSegments] = 
                    () => Constants.AirIntakeBaseCuttingSegments,
                [Point2DTypes.AirIntakeLowerCuttingSegments] = 
                    () => Constants.AirIntakeLowerCuttingSegments,
                [Point2DTypes.AirIntakeMiddleCuttingSegments] = 
                    () => Constants.AirIntakeMiddleCuttingSegments,
                [Point2DTypes.AirIntakeSmallCuttingSegments] = 
                    () => Constants.AirIntakeSmallCuttingSegments,
                [Point2DTypes.AirIntakePartitionSegments] = 
                    () => Constants.AirIntakePartitionSegments,
                [Point2DTypes.NozzleDrawingSegments] = 
                    () => Constants.NozzleDrawingSegments
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Circle,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<CircleTypes, Func<Circle[,]>> _circleFunc
            = new Dictionary<CircleTypes, Func<Circle[,]>>
            {
                [CircleTypes.AirIntakeCircles] = 
                    () => Constants.AirIntakeCircles,
                [CircleTypes.TurbineCircles] = 
                    () => Constants.TurbineCircles,
                [CircleTypes.NozzleDrawingCircles] = 
                    () => Constants.NozzleDrawingCircles,
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Arc,
        /// значение - делегат на возвращение значения константы.
        /// </summary>
        private readonly Dictionary<ArcTypes, Func<Arc[,]>> _arcFunc
            = new Dictionary<ArcTypes, Func<Arc[,]>>
            {
                [ArcTypes.AirIntakeBaseCuttingArcs] = 
                    () => Constants.AirIntakeBaseCuttingArcs,
                [ArcTypes.AirIntakeLowerCuttingArcs] = 
                    () => Constants.AirIntakeLowerCuttingArcs,
                [ArcTypes.AirIntakeMiddleCuttingArcs] = 
                    () => Constants.AirIntakeMiddleCuttingArcs,
                [ArcTypes.AirIntakeSmallCuttingArcs] = 
                    () => Constants.AirIntakeSmallCuttingArcs,
                [ArcTypes.AirIntakePartitionArcs] = 
                    () => Constants.AirIntakePartitionArcs
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Point2D,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<Point2DTypes, Point2D[,,]> _point2Ds
            = new Dictionary<Point2DTypes, Point2D[,,]>
            {
                [Point2DTypes.AcceleratorsBaseSegments] = new[, ,]
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
                            new Point2D(-49.4702548, -71.4931980)
                        },
                        {
                            new Point2D(-49.4702548, -71.4931980),
                            new Point2D(-34.2049246, -104.678698)
                        },
                        {
                            new Point2D(-34.2049246, -104.678698),
                            new Point2D(-211.8743904, -90.5910027)
                        },
                        {
                            new Point2D(-211.8743904, -90.5910027),
                            new Point2D(-216.6993372, -82.9936421)
                        },
                        {
                            new Point2D(-216.6993372, -82.9936421),
                            new Point2D(-64.7392536, -54.4278464)
                        }
                    }
                },
                [Point2DTypes.AirIntakeBaseCuttingSegments] = new[, ,]
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
                },
                [Point2DTypes.AirIntakeLowerCuttingSegments] = new[, ,]
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
                },
                [Point2DTypes.AirIntakeMiddleCuttingSegments] = new[, ,]
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
                },
                [Point2DTypes.AirIntakeSmallCuttingSegments] = new[, ,]
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
                },
                [Point2DTypes.AirIntakePartitionSegments] = new[, ,]
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
                },
                [Point2DTypes.NozzleDrawingSegments] = new[, ,]
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
                }
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Circle,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<CircleTypes, Circle[,]> _circles
            = new Dictionary<CircleTypes, Circle[,]>
            {
                [CircleTypes.AirIntakeCircles] = new[,]
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
                },
                [CircleTypes.TurbineCircles] = new[,]
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
                },
                [CircleTypes.NozzleDrawingCircles] = new[,]
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
                }
            };

        /// <summary>
        /// Словарь, где ключ - перечисление типа Arc,
        /// значение - соответствующее значение параметра.
        /// </summary>
        private readonly Dictionary<ArcTypes, Arc[,]> _arcs
            = new Dictionary<ArcTypes, Arc[,]>
            {
                [ArcTypes.AirIntakeBaseCuttingArcs] = new[,]
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
                },
                [ArcTypes.AirIntakeLowerCuttingArcs] = new[,]
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
                },
                [ArcTypes.AirIntakeMiddleCuttingArcs] = new[,]
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
                },
                [ArcTypes.AirIntakeSmallCuttingArcs] = new[,]
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
                },
                [ArcTypes.AirIntakePartitionArcs] = new[,]
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
        [TestCase(112.6678295, 104.4724056, -570 - Difference,
            Point3DTypes.AirIntakePlane)]
        [TestCase(121.2172883, 104.4724056, -800 - Difference,
            Point3DTypes.TurbinePlane)]
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
        [Test(Description = "Позитивный тест сеттера/геттера CurrentPlane.")]
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
        [TestCase(Point2DTypes.AcceleratorsBaseSegments)]
        [TestCase(Point2DTypes.AirIntakeBaseCuttingSegments)]
        [TestCase(Point2DTypes.AirIntakeLowerCuttingSegments)]
        [TestCase(Point2DTypes.AirIntakeMiddleCuttingSegments)]
        [TestCase(Point2DTypes.AirIntakeSmallCuttingSegments)]
        [TestCase(Point2DTypes.AirIntakePartitionSegments)]
        [TestCase(Point2DTypes.NozzleDrawingSegments)]
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
        [TestCase(CircleTypes.AirIntakeCircles)]
        [TestCase(CircleTypes.NozzleDrawingCircles)]
        public void TestCirclesGet_CorrectValue(CircleTypes type)
        {
            var expected = _circles[type];
            var actual = _circleFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }

        /// <summary>
        /// Тест сеттера и геттера поля TurbineCircles.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера/геттера TurbineCircles.")]
        public void TestTurbineCirclesSet_CorrectValue()
        {
            const CircleTypes type = CircleTypes.TurbineCircles;
            Circle[,] excepted =
            {
                {
                    new Circle(new Point2D(100, 100), 25),
                    new Circle(new Point2D(-100, 100), 25),
                    new Circle(new Point2D(-100, -100), 25),
                    new Circle(new Point2D(100, -100), 25)
                }
            };
            Constants.TurbineCircles = excepted;
            var actual = _circleFunc[type]();
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        /// <summary>
        /// Тест на возврат поля константы типа Arc.
        /// </summary>
        /// <param name="type">Параметр поля типа Arc.</param>
        [Test(Description = "Позитивный тест геттеров на возвращение массива дуг.")]
        [TestCase(ArcTypes.AirIntakeBaseCuttingArcs)]
        [TestCase(ArcTypes.AirIntakeLowerCuttingArcs)]
        [TestCase(ArcTypes.AirIntakeMiddleCuttingArcs)]
        [TestCase(ArcTypes.AirIntakeSmallCuttingArcs)]
        [TestCase(ArcTypes.AirIntakePartitionArcs)]
        public void TestArcsGet_CorrectValue(ArcTypes type)
        {
            var expected = _arcs[type];
            var actual = _arcFunc[type]();
            Assert.IsTrue(_check.CheckEqual(expected, actual));
        }
    }
}