namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант для построения орудий звездолета.
    /// </summary>
    public class BlastersConstants
    {
        /// <summary>
        /// Создает экземпляр класса констант для построения орудий.
        /// </summary>
        /// <param name="difference">Разница между шириной крыльев и
        /// длиной корпуса в мм.</param>
        public BlastersConstants(double difference)
        {
            CurrentPlane = 
                new Point3D(98.760456, 54.834961, -600 - difference);
            SideRightTipPlane = 
                new Point3D(-612.468507, -180, -200 - difference);
            SideLeftTipPlane = 
                new Point3D(612.468507, -180, -200 - difference);
            FrontBlasterBodyPlane = 
                new Point3D(-641.968507, 185, -600 - difference);
            RightAntennaSegments = new[,,]
            {
                {
                    { new Point2D(-210 - difference, 205), 
                        new Point2D(-210 - difference, 200)},
                    { new Point2D(-210 - difference, 160), 
                        new Point2D(-210 - difference, 155)}
                },
                {
                    { new Point2D(-210 - difference, -210), 
                        new Point2D(-210 - difference, -205)},
                    { new Point2D(-210 - difference, -165), 
                        new Point2D(-210 - difference, -160)}
                }
            };
            RightAntennaArcs = new[,]
            {
                {
                    new Arc(new Point2D(-210 - difference, 180), 25,
                        new Point2D(-210 - difference, 155), 
                        new Point2D(-210 - difference, 205), -1),

                    new Arc(new Point2D(-210 - difference, 180), 20,
                        new Point2D(-210 - difference, 160), 
                        new Point2D(-210 - difference, 200), -1)
                },
                {
                    new Arc(new Point2D(-210 - difference, -185), 25,
                        new Point2D(-210 - difference, -160), 
                        new Point2D(-210 - difference, -210), 1),

                    new Arc(new Point2D(-210 - difference, -185), 20,
                        new Point2D(-210 - difference, -165), 
                        new Point2D(-210 - difference, -205), 1)
                }
            };
            LeftAntennaSegments = new[,,]
            {
                {
                    { new Point2D(210 + difference, 205), 
                        new Point2D(210 + difference, 200)},
                    { new Point2D(210 + difference, 160), 
                        new Point2D(210 + difference, 155)}
                },
                {
                    { new Point2D(210 + difference, -210), 
                        new Point2D(210 + difference, -205)},
                    { new Point2D(210 + difference, -165), 
                        new Point2D(210 + difference, -160)}
                }
            };
            LeftAntennaArcs = new[,]
            {
                {
                    new Arc(new Point2D(210 + difference, 180), 25,
                        new Point2D(210 + difference, 155), 
                        new Point2D(210 + difference, 205), 1),

                    new Arc(new Point2D(210 + difference, 180), 20,
                        new Point2D(210 + difference, 160), 
                        new Point2D(210 + difference, 200), 1)
                },
                {
                    new Arc(new Point2D(210 + difference, -185), 25,
                        new Point2D(210 + difference, -160), 
                        new Point2D(210 + difference, -210), -1),

                    new Arc(new Point2D(210 + difference, -185), 20,
                        new Point2D(210 + difference, -165), 
                        new Point2D(210 + difference, -205), -1)
                }
            };
        }

        /// <summary>
        /// Координата центра плоскости внутренней боковой стороны правого нижнего острия.
        /// </summary>
        public Point3D SideRightTipPlane { get; }

        /// <summary>
        /// Координата центра плоскости внутренней боковой стороны левого нижнего острия.
        /// </summary>
        public Point3D SideLeftTipPlane { get; }

        /// <summary>
        /// Координата плоскости передней грани основного тела оружия (правого верхнего)
        /// </summary>
        public Point3D FrontBlasterBodyPlane { get; }

        /// <summary>
        /// Координата, отвечающая за текущее расположение плоскости после выдавливания.
        /// Начальная координата - центр передней перпендикулярной плоскости правого верхнего крыла.
        /// </summary>
        public Point3D CurrentPlane { set; get; }

        /// <summary>
        /// Массив окружностей, используемых для построения всех цилиндров бластеров.
        /// Центр координат окружностей не меняется, меняется только радиус.
        /// </summary>
        public Circle[,] CurrentBlasterCircles { set; get; } =
        {
            {
                new Circle(new Point2D(-617.468507, -180), 25),
                new Circle(new Point2D(617.468507, -180), 25),
                new Circle(new Point2D(-617.468507, 185), 25),
                new Circle(new Point2D(617.468507, 185), 25)
            }
        };

        /// <summary>
        /// Массив точек, используемых для построения отрезков основания острия для правых нижнего
        /// и верхнего бластеров. В ходе программы, координаты отразятся для левой половины.
        /// </summary>
        public Point2D[,,] TipsBaseSegments { get; } =
        {
            {
                { new Point2D(-622.468507, -179), new Point2D(-622.468507, -181)},
                { new Point2D(-612.468507, -179), new Point2D(-612.468507, -181)},
                { new Point2D(-616.468507, -175), new Point2D(-618.468507, -175)},
                { new Point2D(-616.468507, -185), new Point2D(-618.468507, -185)}
            },
            {
                { new Point2D(-622.468507, 184), new Point2D(-622.468507, 186)},
                { new Point2D(-612.468507, 184), new Point2D(-612.468507, 186)},
                { new Point2D(-616.468507, 180), new Point2D(-618.468507, 180)},
                { new Point2D(-616.468507, 190), new Point2D(-618.468507, 190)}
            }
        };

        /// <summary>
        /// Массив дуг, используемых для построения скругления основания острия для правых 
        /// нижнего и верхнего бластеров. В ходе программы, координаты отразятся 
        /// для левой половины.
        /// </summary>
        public Arc[,] TipsBaseArcs { get; } =
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

        /// <summary>
        /// Массив точек, используемых для построения отрезков антенны для правых нижнего
        /// и верхнего бластеров.
        /// </summary>
        public Point2D[,,] RightAntennaSegments { get; }

        /// <summary>
        /// Массив дуг, используемых для построения антенны для правых нижнего и верхнего 
        /// бластеров.
        /// </summary>
        public Arc[,] RightAntennaArcs { get; }

        /// <summary>
        /// Массив точек, используемых для построения отрезков антенны для левых нижнего
        /// и верхнего бластеров.
        /// </summary>
        public Point2D[,,] LeftAntennaSegments { get; }

        /// <summary>
        /// Массив дуг, используемых для построения антенны для левых нижнего и верхнего 
        /// бластеров.
        /// </summary>
        public Arc[,] LeftAntennaArcs { get; }

        /// <summary>
        /// Массив точек, используемых для построения отрезков рисунка для правых нижнего 
        /// и верхнего бластеров. В ходе программы, координаты отразятся для левой половины.
        /// </summary>
        public Point2D[,,] BlasterDrawingSegments { get; } =
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

        /// <summary>
        /// Массив кругов, используемых для построения отрезков рисунка для правых нижнего 
        /// и верхнего бластеров. В ходе программы, координаты отразятся для левой половины.
        /// </summary>
        public Circle[,] BlasterDrawingCircles { get; } =
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
    }
}