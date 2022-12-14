namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант для построения ускорителей звездолета.
    /// </summary>
    public class AcceleratorsConstants
    {
        /// <summary>
        /// Создает экземпляр класса констант для построения ускорителей.
        /// </summary>
        /// <param name="difference">Разница между шириной крыльев и
        /// длиной корпуса в мм.</param>
        public AcceleratorsConstants(double difference)
        {
            CurrentPlane = 
                new Point3D(98.760456, 54.834961, -600 - difference);
            AirIntakePlane = 
                new Point3D(112.6678295, 104.4724056, -570 - difference);
            TurbinePlane = 
                new Point3D(121.2172883, 104.4724056, -800 - difference);
        }

        /// <summary>
        /// Координата, отвечающая за текущее расположение плоскости
        /// после выдавливания.Начальная координата - центр передней
        /// перпендикулярной плоскости правого верхнего крыла.
        /// </summary>
        public Point3D CurrentPlane { get; set; }

        /// <summary>
        /// Координата передней плоскости воздухозаборника звездолета.
        /// </summary>
        public Point3D AirIntakePlane { get; }

        /// <summary>
        /// Координата передней плоскости турбины.
        /// </summary>
        public Point3D TurbinePlane { get; }

        /// <summary>
        /// Массив точек для построения отрезков вершин основания
        /// ускорителя звездолета. В ходе программы координаты отразятся.
        /// </summary>
        public Point2D[,,] AcceleratorsBaseSegments { get; } =
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

        /// <summary>
        /// Массив окружностей, используемых для построения воздухозаборников.
        /// Центр координат окружностей не меняется, меняется только радиус.
        /// </summary>
        public Circle[,] AirIntakeCircles { get; } =
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

        /// <summary>
        /// Массив окружностей, используемых для построения турбин ускорителей.
        /// Центр координат окружностей не меняется, меняется только радиус.
        /// </summary>
        public Circle[,] TurbineCircles { get; set; } =
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

        /// <summary>
        /// Массив дуг, используемых для построения основного выреза
        /// в воздухозаборнике.
        /// </summary>
        public Arc[,] AirIntakeBaseCuttingArcs { get; } =
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

        /// <summary>
        /// Массив точек, используемых для построения отрезков основного
        /// выреза в воздухозаборнике.
        /// </summary>
        public Point2D[,,] AirIntakeBaseCuttingSegments { get; } =
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

        /// <summary>
        /// Массив дуг, используемых для построения нижнего выреза в воздухозаборнике.
        /// </summary>
        public Arc[,] AirIntakeLowerCuttingArcs { get; } =
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

        /// <summary>
        /// Массив точек, используемых для построения отрезков нижнего выреза в воздухозаборнике.
        /// </summary>
        public Point2D[,,] AirIntakeLowerCuttingSegments { get; } =
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

        /// <summary>
        /// Массив дуг, используемых для построения среднего выреза в воздухозаборнике.
        /// </summary>
        public Arc[,] AirIntakeMiddleCuttingArcs { get; } =
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

        /// <summary>
        /// Массив точек, используемых для построения отрезков
        /// среднего выреза в воздухозаборнике.
        /// </summary>
        public Point2D[,,] AirIntakeMiddleCuttingSegments { get; } =
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

        /// <summary>
        /// Массив дуг, используемых для построения малого выреза в воздухозаборнике.
        /// </summary>
        public Arc[,] AirIntakeSmallCuttingArcs { get; } =
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

        /// <summary>
        /// Массив точек, используемых для построения отрезков
        /// малого выреза в воздухозаборнике.
        /// </summary>
        public Point2D[,,] AirIntakeSmallCuttingSegments { get; } =
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

        /// <summary>
        /// Массив дуг, используемых для построения перегородки в воздухозаборнике.
        /// </summary>
        public Arc[,] AirIntakePartitionArcs { get; } =
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

        /// <summary>
        /// Массив точек, используемых для построения отрезков
        /// перегородки в воздухозаборнике.
        /// </summary>
        public Point2D[,,] AirIntakePartitionSegments { get; } =
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

        /// <summary>
        /// Массив точек, используемых для построения отрезков
        /// рисунка для правых нижнего и верхнего сопла.
        /// В ходе программы координаты отразятся.
        /// </summary>
        public Point2D[,,] NozzleDrawingSegments { get; } =
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

        /// <summary>
        /// Массив кругов, используемых для построения отрезков рисунка
        /// для правых нижнего и верхнего сопла.
        /// В ходе программы координаты отразятся.
        /// </summary>
        public Circle[,] NozzleDrawingCircles { get; } =
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
    }
}