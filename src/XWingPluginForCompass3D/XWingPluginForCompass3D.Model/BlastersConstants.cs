namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант для построения бластерных орудий звездолёта.
    /// </summary>
    public class BlastersConstants
    {
        /// <summary>
        /// Параметризированный конструктор.
        /// </summary>
        /// <param name="difference">Разница между шириной крыльев и длиной корпуса в мм.</param>
        public BlastersConstants(double difference)
        {
            CurrentPlane = new Point3D(98.760456, 54.834961, -600 - difference);
            _sideRightTipPlane = new Point3D(-612.468507, -180, -200 - difference);
            _sideLeftTipPlane = new Point3D(612.468507, -180, -200 - difference);
            _frontBodyBlasterPlane = new Point3D(-641.968507, 185, -600 - difference);
            _rightAntennaSegments = new Point2D[,,]
            {
                {
                    { new Point2D(-210 - difference, 205), new Point2D(-210 - difference, 200)},
                    { new Point2D(-210 - difference, 160), new Point2D(-210 - difference, 155)}
                },
                {
                    { new Point2D(-210 - difference, -210), new Point2D(-210 - difference, -205)},
                    { new Point2D(-210 - difference, -165), new Point2D(-210 - difference, -160)}
                }
            };
            _rightAntennaTipArcs = new Arc[,]
            {
                {
                    new Arc(new Point2D(-210 - difference, 180), 25,
                        new Point2D(-210 - difference, 155), new Point2D(-210 - difference, 205), -1),

                    new Arc(new Point2D(-210 - difference, 180), 20,
                        new Point2D(-210 - difference, 160), new Point2D(-210 - difference, 200), -1)
                },
                {
                    new Arc(new Point2D(-210 - difference, -185), 25,
                        new Point2D(-210 - difference, -160), new Point2D(-210 - difference, -210), 1),

                    new Arc(new Point2D(-210 - difference, -185), 20,
                        new Point2D(-210 - difference, -165), new Point2D(-210 - difference, -205), 1)
                }
            };
            _leftAntennaSegments = new Point2D[,,]
            {
                {
                    { new Point2D(210 + difference, 205), new Point2D(210 + difference, 200)},
                    { new Point2D(210 + difference, 160), new Point2D(210 + difference, 155)}
                },
                {
                    { new Point2D(210 + difference, -210), new Point2D(210 + difference, -205)},
                    { new Point2D(210 + difference, -165), new Point2D(210 + difference, -160)}
                }
            };
            _leftAntennaTipArcs = new Arc[,]
            {
                {
                    new Arc(new Point2D(210 + difference, 180), 25,
                        new Point2D(210 + difference, 155), new Point2D(210 + difference, 205), 1),

                    new Arc(new Point2D(210 + difference, 180), 20,
                        new Point2D(210 + difference, 160), new Point2D(210 + difference, 200), 1)
                },
                {
                    new Arc(new Point2D(210 + difference, -185), 25,
                        new Point2D(210 + difference, -160), new Point2D(210 + difference, -210), -1),

                    new Arc(new Point2D(210 + difference, -185), 20,
                        new Point2D(210 + difference, -165), new Point2D(210 + difference, -205), -1)
                }
            };
        }

        /// <summary>
        /// Координата центра плоскости внутренней боковой стороны правого нижнего острия.
        /// </summary>
        private readonly Point3D _sideRightTipPlane;

        /// <summary>
        /// Координата центра плоскости внутренней боковой стороны левого нижнего острия.
        /// </summary>
        private readonly Point3D _sideLeftTipPlane;

        /// <summary>
        /// Координата плоскости передней грани основного тела бластерного оружия (правого верхнего)
        /// </summary>
        private readonly Point3D _frontBodyBlasterPlane;

        /// <summary>
        /// Координата, отвечающая за текущее расположение плоскости после выдавливания.
        /// Начальная координата - центр передней перпендикулярной плоскости правого верхнего крыла.
        /// </summary>
        private Point3D _currentPlane;

        /// <summary>
        /// Массив окружностей, используемых для построения всех цилиндров бластеров.
        /// Центр координат окружностей не меняется, меняется только радиус.
        /// </summary>
        private Circle[] _currentBlasterCircles =
        {
            new Circle(new Point2D(-617.468507, -180), 25),
            new Circle(new Point2D(617.468507, -180), 25),
            new Circle(new Point2D(-617.468507, 185), 25),
            new Circle(new Point2D(617.468507, 185), 25)
        };

        /// <summary>
        /// Массив точек, используемых для построения отрезков основания острия для правых нижнего
        /// и верхнего бластеров. В ходе программы, координаты отзеркалятся для левой половины.
        /// </summary>
        private readonly Point2D[,,] _tipsBaseSegments = new Point2D[,,]
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
        /// нижнего и верхнего бластеров. В ходе программы, координаты отзеркалятся 
        /// для левой половины.
        /// </summary>
        private readonly Arc[,] _tipsBaseArcs = new Arc[,]
        {
            {
                new Arc(new Point2D(-617.468507, -180), 5.0990195,
                    new Point2D(-612.468507, -181), new Point2D(-616.468507, -185), -1),

                new Arc(new Point2D(-617.468507, -180), 5.0990195,
                    new Point2D(-618.468507, -185), new Point2D(-622.468507, -181), -1),

                new Arc(new Point2D(-617.468507, -180), 5.0990195,
                    new Point2D(-622.468507, -179), new Point2D(-618.468507, -175), -1),

                new Arc(new Point2D(-617.468507, -180), 5.0990195,
                    new Point2D(-616.468507, -175), new Point2D(-612.468507, -179), -1)
            },
            {
                new Arc(new Point2D(-617.468507, 185), 5.0990195,
                    new Point2D(-612.468507, 186), new Point2D(-616.468507, 190), 1),

                new Arc(new Point2D(-617.468507, 185), 5.0990195,
                    new Point2D(-618.468507, 190), new Point2D(-622.468507, 186), 1),

                new Arc(new Point2D(-617.468507, 185), 5.0990195,
                    new Point2D(-622.468507, 184), new Point2D(-618.468507, 180), 1),

                new Arc(new Point2D(-617.468507, 185), 5.0990195,
                    new Point2D(-616.468507, 180), new Point2D(-612.468507, 184), 1)
            }
        };

        /// <summary>
        /// Массив точек, используемых для построения отрезков антенны для правых нижнего
        /// и верхнего бластеров.
        /// </summary>
        private Point2D[,,] _rightAntennaSegments;

        /// <summary>
        /// Массив дуг, используемых для построения антенны для правых нижнего и верхнего 
        /// бластеров.
        /// </summary>
        private Arc[,] _rightAntennaTipArcs;

        /// <summary>
        /// Массив точек, используемых для построения отрезков антенны для левых нижнего
        /// и верхнего бластеров.
        /// </summary>
        private Point2D[,,] _leftAntennaSegments;

        /// <summary>
        /// Массив дуг, используемых для построения антенны для левых нижнего и верхнего 
        /// бластеров.
        /// </summary>
        private Arc[,] _leftAntennaTipArcs;

        /// <summary>
        /// Массив точек, используемых для построения отрезков рисунка для правых нижего 
        /// и верхнего бластеров. В ходе программы, координаты отзеркалятся для левой половины.
        /// </summary>
        private readonly Point2D[,,] _blasterDrawingSegments = new Point2D[,,]
        {
            {
                { new Point2D(-634.439069, 201.970562), new Point2D(-628.075108, 195.606601)},
                { new Point2D(-617.468507, 209), new Point2D(-617.468507, 200)},
                { new Point2D(-600.497944, 201.970562), new Point2D(-606.861905, 195.606601)},
                { new Point2D(-593.468507, 185), new Point2D(-602.468507, 185)},
                { new Point2D(-600.497944, 168.029437), new Point2D(-606.861905, 174.393398)},
                { new Point2D(-617.468507, 161), new Point2D(-617.468507, 170)},
                { new Point2D(-634.439069, 168.029437), new Point2D(-628.075108, 174.393398)},
                { new Point2D(-641.468507, 185), new Point2D(-632.468507, 185)}
            },
            {
                { new Point2D(-634.439069, -163.029437), new Point2D(-628.075108, -169.393398)},
                { new Point2D(-617.468507, -156), new Point2D(-617.468507, -165)},
                { new Point2D(-600.497944, -163.029437), new Point2D(-606.861905, -169.393398)},
                { new Point2D(-593.468507, -180), new Point2D(-602.468507, -180)},
                { new Point2D(-600.497944, -196.970562), new Point2D(-606.861905, -190.606601)},
                { new Point2D(-617.468507, -204), new Point2D(-617.468507, -195)},
                { new Point2D(-634.439069, -196.970562), new Point2D(-628.075108, -190.606601)},
                { new Point2D(-641.468507, -180), new Point2D(-632.468507, -180)}
            }
        };

        /// <summary>
        /// Массив кругов, используемых для построения отрезков рисунка для правых нижего 
        /// и верхнего бластеров. В ходе программы, координаты отзеркалятся для левой половины.
        /// </summary>
        private readonly Circle[,] _blasterDrawingCircles = new Circle[,]
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

        /// <summary>
        /// Возвращает координату центра плоскости внутрнней стороны правого нижнего острия.
        /// </summary>
        public Point3D SideRightTipPlane
        {
            get
            {
                return _sideRightTipPlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости внутрнней стороны левого нижнего острия.
        /// </summary>
        public Point3D SideLeftTipPlane
        {
            get
            {
                return _sideLeftTipPlane;
            }
        }

        /// <summary>
        /// Возвращает координату плоскости передней грани основного тела бластера.
        /// </summary>
        public Point3D FrontBlasterBodyPlane
        {
            get
            {
                return _frontBodyBlasterPlane;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает текущую координату плоскости.
        /// </summary>
        public Point3D CurrentPlane
        {
            set
            {
                _currentPlane = value;
            }
            get
            {
                return _currentPlane;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает массив окружностей, используемых для построения
        /// всех цилиндров бластеров.
        /// </summary>
        public Circle[] CurrentBlasterCircles
        {
            set
            {
                _currentBlasterCircles = value;
            }
            get
            {
                return _currentBlasterCircles;
            }
        }

        /// <summary>
        /// Возвращает массив точек, используемых для построения отрезков основания острия.
        /// </summary>
        public Point2D[,,] TipsBaseSegments
        {
            get
            {
                return _tipsBaseSegments;
            }
        }

        /// <summary>
        /// Возвращает массив дуг, используемых для построения основания острия.
        /// </summary>
        public Arc[,] TipsBaseArcs
        {
            get
            {
                return _tipsBaseArcs;
            }
        }

        /// <summary>
        /// Возвращает массив точек, используемых для построения отрезков правых антенн.
        /// </summary>
        public Point2D[,,] RightAntennaSegments
        {
            get
            {
                return _rightAntennaSegments;
            }
        }

        /// <summary>
        /// Возвращает массив дуг, используемых для построения правых антенн.
        /// </summary>
        public Arc[,] RightAntennaArcs
        {
            get
            {
                return _rightAntennaTipArcs;
            }
        }

        /// <summary>
        /// Возвращает массив точек, используемых для построения отрезков левых антенн.
        /// </summary>
        public Point2D[,,] LeftAntennaSegments
        {
            get
            {
                return _leftAntennaSegments;
            }
        }

        /// <summary>
        /// Возвращает массив дуг, используемых для построения левых антенн.
        /// </summary>
        public Arc[,] LeftAntennaArcs
        {
            get
            {
                return _leftAntennaTipArcs;
            }
        }

        /// <summary>
        /// Возвращает массив точек, используемых для построения отрезков рисунка бластеров.
        /// </summary>
        public Point2D[,,] BlasterDrawingSegments
        {
            get
            {
                return _blasterDrawingSegments;
            }
        }

        /// <summary>
        /// Возвращает массив кругов, используемых для построения рисунка бластеров.
        /// </summary>
        public Circle[,] BlasterDrawingCircles
        {
            get
            {
                return _blasterDrawingCircles;
            }
        }
    }
}
