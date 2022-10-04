namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант для построения основного корпуса звездолёта.
    /// </summary>
    public class BodyConstants
    {
        /// <summary>
        /// Создаёт экземпляр класса констант для построения корпуса.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса звездолёта.</param>
        public BodyConstants(double bodyLength)
        {
            _upperFacePlane 
                = new Point3D(0, 78.493198, -600 - (bodyLength / 2));
            _upperFaceVertexes = new Point2D[,]
            {
                {
                    new Point2D(-54.395689, 600 + bodyLength),
                    new Point2D(54.395689, 600 + bodyLength),
                    new Point2D(54.395689, 600),
                    new Point2D(-54.395689, 600)
                }                
            };
            _lowerFacePlane 
                = new Point3D(0, -71.493198, -600 - (bodyLength / 2));
            _lowerFaceVertexes = new Point2D[,]
            {
                {
                    new Point2D(-49.470255, -600-bodyLength),
                    new Point2D(49.470255, -600-bodyLength),
                    new Point2D(49.470255, -600),
                    new Point2D(-49.470255, -600)
                }                
            };
            _lowerBodyBackPlane 
                = new Point3D(0, -96.493198, -600 - bodyLength);
            _lowerBodySidePlane 
                = new Point3D(-49.470255, -96.493198, -600 - (bodyLength / 2));
            _upperBodyPartFacePlane 
                = new Point3D(0, 128.493198, -600 - (bodyLength / 2));
            _deepingBodyPartFacePlane 
                = new Point3D(0, 123.493198, -600 - (bodyLength / 2));
            _backBodyPlaneCoordinate 
                = new Point3D(0, 0, -600 - bodyLength);
            _backDeepingPlane 
                = new Point3D(87.3, 11.5, -590 - bodyLength);
        }

        /// <summary>
        /// Координата центра плоскости верхнего основания корпуса звездолёта.
        /// </summary>
        private readonly Point3D _upperBasePlane = 
            new Point3D(0, 0, -600);

        /// <summary>
        /// Координата центра плоскости верхней грани звездолёта.
        /// </summary>
        private readonly Point3D _upperFacePlane;

        /// <summary>
        /// Координата центра плоскости нижней грани звездолёта.
        /// </summary>
        private readonly Point3D _lowerFacePlane;

        /// <summary>
        /// Координата центра плоскости задней грани нижней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _lowerBodyBackPlane;

        /// <summary>
        /// Координата центра плоскости боковой грани нижней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _lowerBodySidePlane;

        /// <summary>
        /// Координата центра задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _bowBodyBackPlane = 
            new Point3D(0, 103.3980655, -597.8211064);

        /// <summary>
        /// Координата центра плоскости верхней выпуклой части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _upperBodyPartFacePlane;

        /// <summary>
        /// Координата центра плоскости углубления верхней выпуклой части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _deepingBodyPartFacePlane;

        /// <summary>
        /// Координата центра плоскости основания головы дроида звездолёта.
        /// </summary>
        private readonly Point3D _baseDroidHeadPlane =
            new Point3D(-0.2033454, 133.493198, -632.6372698);

        /// <summary>
        /// Координата центра плоскости задней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _backBodyPlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости углубления задней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _backDeepingPlane;

        /// <summary>
        /// Массив координат вершин верхнего основания корпуса звездолёта.
        /// </summary>
        private readonly Point2D[,] _baseVertexes =
        {
            {
                new Point2D(108.977589, -4.985001),
                new Point2D(54.395689, 78.493198),
                new Point2D(-54.395689, 78.493198),
                new Point2D(-108.977589, -4.985001),
                new Point2D(-49.470255, -71.493198),
                new Point2D(49.470255, -71.493198)
            }            
        };

        /// <summary>
        /// Массив координат вершин верхней грани корпуса звездолёта.
        /// </summary>
        private readonly Point2D[,] _upperFaceVertexes;

        /// <summary>
        /// Массив координат вершин нижней грани корпуса звездолёта.
        /// </summary>
        private readonly Point2D[,] _lowerFaceVertexes;

        /// <summary>
        /// Массив координат вершин выреза выдавленных частей корпуса звездолёта.
        /// В ходе программы координаты отзеркалятся.
        /// </summary>
        private readonly Point2D[,] _BodyCutoutVertexes =
        {
            {
                new Point2D (-49.470255, -121.493198),
                new Point2D (-49.470255, -71.493198),
                new Point2D (-26.470255, -121.493198)
            },
            {
                new Point2D(-54.395689, 128.493198),
                new Point2D(-54.395689, 78.493198),
                new Point2D(-31.395689, 128.493198)
            }
        };

        /// <summary>
        /// Массив координат вершин среза нижней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[,] _lowerBodySliceVertexes =
        {
            {
                new Point2D (600, 71.493198),
                new Point2D (600, 121.493198),
                new Point2D (650, 121.493198)
            }            
        };

        /// <summary>
        /// Массив координат вершин задней плоскости носовой части корпуса звездолёта.
        /// (Поле необходимо для того, чтобы убрать зазор между носом и корпусом)
        /// </summary>
        private readonly Point2D[,] _bowBodyFaceVertexes =
        {
            {
                new Point2D (54.395689, 25.901062),
                new Point2D (26.115362, 75.697928),
                new Point2D (-26, 75.901062150386),
                new Point2D (-54.395689, 25.901062)
            }            
        };

        /// <summary>
        /// Массив координат вершин углубления верхней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[,] _deepingUpperBodyFaceVertexes =
        {
            {
                new Point2D (-25, 607.659914486825),
                new Point2D (25, 607.659914486825),
                new Point2D (25, 892.659914486825),
                new Point2D (-25, 892.659914486825)
            }            
        };

        /// <summary>
        /// Массив окружностей для выдавливания в верхней части корпуса.
        /// </summary>
        private readonly Circle[] _upperBodyExtrudingCircles =
        {
            new Circle (new Point2D (-0.2033454, 632.6372698), 25),
            new Circle (new Point2D (-0.2033454, 750.3556376), 22),
            new Circle (new Point2D (-0.2033454, 706.4660939), 8),
            new Circle (new Point2D (-0.2033454, 678.8481419), 8),
            new Circle (new Point2D (-0.2033454, 750.3556376), 19)
        };

        /// <summary>
        /// Массив координат вершин выреза задней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[,] _backBodyDeepingVertexes =
        {
            {
                new Point2D (-97, -4),
                new Point2D (-20, 123),
                new Point2D (20, 123),
                new Point2D (97, -4),
                new Point2D (20, -100),
                new Point2D (-20, -100)
            }            
        };

        /// <summary>
        /// Массив прямоугольников для выдавливания в верхней части корпуса.
        /// </summary>
        private readonly Point2D[,] _upperBodyExtrudingRectangles = new Point2D[,]
        {
            {
                new Point2D (-19.2033454, 663.7722065),
                new Point2D (18.7966545, 663.7722065),
                new Point2D (18.7966545, 721.7722065),
                new Point2D (-19.2033454, 721.7722065)
            },
            {
                new Point2D (-19.2033454, 858.5264552),
                new Point2D (18.7966545, 858.5264552),
                new Point2D (18.7966545, 878.5264552),
                new Point2D (-19.2033454, 878.5264552)
            },
            {
                new Point2D (-22.2033454, 784.7158326),
                new Point2D (21.7966545, 784.7158326),
                new Point2D (21.7966545, 842.7158326),
                new Point2D (-22.2033454, 842.7158326)
            },
            {
                new Point2D (-15.2033454, 669.2722065),
                new Point2D (14.7966545, 669.2722065),
                new Point2D (14.7966545, 716.2722065),
                new Point2D (-15.2033454, 716.2722065)
            },
            {
                new Point2D (-15.2033454, 861.5264552),
                new Point2D (14.7966545, 861.5264552),
                new Point2D (14.7966545, 875.5264552),
                new Point2D (-15.2033454, 875.5264552)
            },
            {
                new Point2D (-19.2033454, 788.2158326),
                new Point2D (18.7966545, 788.2158326),
                new Point2D (18.7966545, 839.2158326),
                new Point2D (-19.2033454, 839.2158326)
            }
        };

        /// <summary>
        /// Дуга для построения головы дроида.
        /// </summary>
        private readonly Arc _droidHeadArc = new Arc(
            new Point2D(0.2033454, 632.6372698), 25,
            new Point2D(-0.2463156, 657.6332256), 
            new Point2D(-0.2463156, 607.6413140), 1);

        /// <summary>
        /// Массив окружностей для выдавливания в задней части корпуса.
        /// </summary>
        private readonly Circle[] _backDrawingCircles =
        {
            new Circle (new Point2D (0, 0), 20)
        };

        /// <summary>
        /// Массив точек, используемых для построения отрезков рисунка задней части корпуса.
        /// </summary>
        private readonly Point2D[,] _backDrawingSegments = new Point2D[,]
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
        };

        /// <summary>
        /// Возвращает координату центра плоскости верхнего основания корпуса звездолёта. 
        /// </summary>
        public Point3D UpperBasePlane
        {
            get
            {
                return _upperBasePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра верхней грани корпуса звездолёта. 
        /// </summary>
        public Point3D UpperFacePlane
        {
            get
            {
                return _upperFacePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра нижней грани корпуса звездолёта. 
        /// </summary>
        public Point3D LowerFacePlane
        {
            get
            {
                return _lowerFacePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра задней грани корпуса звездолёта. 
        /// </summary>
        public Point3D BodyBackPlane
        {
            get
            {
                return _lowerBodyBackPlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра боковой грани нижней части корпуса звездолёта. 
        /// </summary>
        public Point3D LowerSideBackPlane
        {
            get
            {
                return _lowerBodySidePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра задней плоскости носовой части корпуса звездолёта. 
        /// </summary>
        public Point3D BowBodyBackPlane
        {
            get
            {
                return _bowBodyBackPlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public Point3D UpperBodyPartFacePlane
        {
            get
            {
                return _upperBodyPartFacePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости углубления верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public Point3D DeepingBodyPartFacePlane
        {
            get
            {
                return _deepingBodyPartFacePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости углубления верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public Point3D BaseDroidHeadPlane
        {
            get
            {
                return _baseDroidHeadPlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости задней части корпуса звездолёта.
        /// </summary>
        public Point3D BackBodyPlaneCoordinate
        {
            get
            {
                return _backBodyPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости углубления задней части корпуса звездолёта.
        /// </summary>
        public Point3D BackDeepingPlane
        {
            get
            {
                return _backDeepingPlane;
            }
        }

        /// <summary>
        /// Возвращает массив координат верхнего основания корпуса звездолёта. 
        /// </summary>
        public Point2D[,] BaseVertexes
        {
            get
            {
                return _baseVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат верхней грани корпуса звездолёта. 
        /// </summary>
        public Point2D[,] UpperFaceVertexes
        {
            get
            {
                return _upperFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат нижней грани корпуса звездолёта. 
        /// </summary>
        public Point2D[,] LowerFaceVertexes
        {
            get
            {
                return _lowerFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин выреза нижней части корпуса звездолёта. 
        /// </summary>
        public Point2D[,] BodyCutoutVertexes
        {
            get
            {
                return _BodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин среза нижней части корпуса звездолёта. 
        /// </summary>
        public Point2D[,] LowerBodySliceVertexes
        {
            get
            {
                return _lowerBodySliceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        public Point2D[,] BowBodyFaceVertexes
        {
            get
            {
                return _bowBodyFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        public Point2D[,] DeepingUpperBodyFaceVertexes
        {
            get
            {
                return _deepingUpperBodyFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив окружностей для выдавливания в верхней части корпуса.
        /// </summary>
        public Circle[] UpperBodyExtrudingCircles
        {
            get
            {
                return _upperBodyExtrudingCircles;
            }
        }

        /// <summary>
        /// Возвращает массив прямоугольников для выдавливания в верхней части корпуса.
        /// </summary>
        public Point2D[,] UpperBodyExtrudingRectangles
        {
            get
            {
                return _upperBodyExtrudingRectangles;
            }
        }

        /// <summary>
        /// Возвращает массив координат углубления задней части корпуса.
        /// </summary>
        public Point2D[,] BackBodyDeepingVertexes
        {
            get
            {
                return _backBodyDeepingVertexes;
            }
        }

        /// <summary>
        /// Возвращает дугу для построения головы дроида.
        /// </summary>
        public Arc DroidHeadArc
        {
            get
            {
                return _droidHeadArc;
            }
        }

        /// <summary>
        /// Возвращает массив окружностей для выдавливания в задней части корпуса.
        /// </summary>
        public Circle[] BackDrawingCircles
        {
            get
            {
                return _backDrawingCircles;
            }
        }

        /// <summary>
        /// Возвращает массив точек, используемых для построения отрезков рисунка задней части корпуса.
        /// </summary>
        public Point2D[,] BackDrawingSegments
        {
            get
            {
                return _backDrawingSegments;
            }
        }
    }
}