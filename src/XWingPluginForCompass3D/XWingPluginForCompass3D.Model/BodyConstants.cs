using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант для построения основного корпуса звездолёта.
    /// </summary>
    public class BodyConstants
    {

        /// <summary>
        /// Конструктор класса констант для построения корпуса.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса звездолёта.</param>
        public BodyConstants(double bodyLength)
        {
            _upperFacePlaneCoordinate = new Point3D(0, 78.493198, -600 - (bodyLength / 2));

            _upperFaceVertexes = new Point2D[]
            {
                new Point2D(-54.395689, 600 + bodyLength),
                new Point2D(54.395689, 600 + bodyLength),
                new Point2D(54.395689, 600),
                new Point2D(-54.395689, 600)
            };

            _lowerFacePlaneCoordinate = new Point3D(0, -71.493198, -600 - (bodyLength / 2));

            _lowerFaceVertexes = new Point2D[]
            {
                new Point2D(-49.470255, -600-bodyLength),
                new Point2D(49.470255, -600-bodyLength),
                new Point2D(49.470255, -600),
                new Point2D(-49.470255, -600)
            };

            _lowerBodyBackPlaneCoordinate = new Point3D(0, -96.493198, -600 - bodyLength);

            _lowerBodySidePlaneCoordinate = new Point3D(-49.470255, -96.493198, -600 - (bodyLength / 2));

            _upperBodyPartFacePlaneCoordinate = new Point3D(0, 128.493198, -600 - (bodyLength / 2));

            _deepingBodyPartFacePlaneCoordinate = new Point3D(0, 123.493198, -600 - (bodyLength / 2));

            _backBodyPlaneCoordinate = new Point3D(0, 0, -600 - bodyLength);

            _backDeepingPlaneCoordinate = new Point3D(87.3, 11.5, -590 - bodyLength);
        }

        /// <summary>
        /// Координата центра плоскости верхнего основания корпуса звездолёта.
        /// </summary>
        private readonly Point3D _upperBasePlaneCoordinate =
            new Point3D(0, 0, -600);

        /// <summary>
        /// Координата центра плоскости верхней грани звездолёта.
        /// </summary>
        private readonly Point3D _upperFacePlaneCoordinate;


        /// <summary>
        /// Координата центра плоскости нижней грани звездолёта.
        /// </summary>
        private readonly Point3D _lowerFacePlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости передней грани верхней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _upperBodyFrontPlaneCoordinate =
            new Point3D(0, 103.493198, -600);

        /// <summary>
        /// Координата центра плоскости задней грани нижней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _lowerBodyBackPlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости боковой грани нижней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _lowerBodySidePlaneCoordinate;

        /// <summary>
        /// Координата центра задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _bowBodyBackPlaneCoordinate =
            new Point3D(0, 103.398065567848, -597.821106431309);

        /// <summary>
        /// Координата центра плоскости верхней выпуклой части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _upperBodyPartFacePlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости углубления верхней выпуклой части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _deepingBodyPartFacePlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости основания головы дроида звездолёта.
        /// </summary>
        private readonly Point3D _baseDroidHeadPlaneCoordinate =
            new Point3D(-0.203345439322, 133.493198, -632.637269856963);

        /// <summary>
        /// Координата центра плоскости задней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _backBodyPlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости задней части корпуса звездолёта.
        /// </summary>
        private readonly Point3D _backDeepingPlaneCoordinate;

        /// <summary>
        /// Массив координат вершин верхнего основания корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _baseVertexes =
        {
            new Point2D(108.977589, -4.985001),
            new Point2D(54.395689, 78.493198),
            new Point2D(-54.395689, 78.493198),
            new Point2D(-108.977589, -4.985001),
            new Point2D(-49.470255, -71.493198),
            new Point2D(49.470255, -71.493198)
        };

        /// <summary>
        /// Массив координат вершин верхней грани корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _upperFaceVertexes;

        /// <summary>
        /// Массив координат вершин нижней грани корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _lowerFaceVertexes;

        /// <summary>
        /// Массив координат вершин первого выреза верхней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _firstUpperBodyCutoutVertexes =
        {
            new Point2D(-54.395689, 128.493198),
            new Point2D(-54.395689, 78.493198),
            new Point2D(-31.395689, 128.493198)
        };

        /// <summary>
        /// Массив координат вершин второго выреза верхней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _secondUpperBodyCutoutVertexes =
        {
            new Point2D(54.395689, 128.493198),
            new Point2D(54.395689, 78.493198),
            new Point2D(31.395689, 128.493198)
        };

        /// <summary>
        /// Массив координат вершин первого выреза нижней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _firstLowerBodyCutoutVertexes =
        {
            new Point2D(-49.470255, -121.493198),
            new Point2D(-49.470255, -71.493198),
            new Point2D(-26.470255, -121.493198)
        };

        /// <summary>
        /// Массив координат вершин второго выреза нижней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _secondLowerBodyCutoutVertexes =
        {
            new Point2D(49.470255, -121.493198),
            new Point2D(49.470255, -71.493198),
            new Point2D(26.470255, -121.493198)
        };

        /// <summary>
        /// Массив координат вершин среза нижней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _lowerBodySliceVertexes =
        {
            new Point2D(600, 71.493198),
            new Point2D(600, 121.493198),
            new Point2D(650, 121.493198)
        };

        /// <summary>
        /// Массив координат вершин задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _bowBodyFaceVertexes =
        {
            new Point2D(54.395689, 25.901062),
            new Point2D(26.115362, 75.697928),
            new Point2D(-26, 75.901062150386),
            new Point2D(-54.395689, 25.901062)
        };

        /// <summary>
        /// Массив координат вершин углубления верхней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _deepingUpperBodyFaceVertexes =
        {
            new Point2D(-25, 607.659914486825),
            new Point2D(25, 607.659914486825),
            new Point2D(25, 892.659914486825),
            new Point2D(-25, 892.659914486825)
        };

        /// <summary>
        /// Массив окружностей для выдавливания в верхней части корпуса.
        /// Первым элементом в массиве является центр окружности по X,
        /// вторым элементом по Y, третий элемент - радиус
        /// </summary>
        private readonly Circle[] _upperBodyPartExtrudingCirclesParameters =
        {
            new Circle( new Point2D(-0.203345439322, 632.637269856963), 25),
            new Circle( new Point2D(-0.203345439322, 750.355637638365), 22),
            new Circle( new Point2D(-0.203345439322, 706.466093921629), 8),
            new Circle( new Point2D(-0.203345439322, 678.848141946151), 8),
            new Circle( new Point2D(-0.203345439322, 750.355637638365), 19)
        };

        /// <summary>
        /// Массив координат вершин выреза задней части корпуса звездолёта.
        /// </summary>
        private readonly Point2D[] _backBodyDeepingVertexes =
        {
            new Point2D(-97, -4),
            new Point2D(-20, 123),
            new Point2D(20, 123),
            new Point2D(97, -4),
            new Point2D(20, -100),
            new Point2D(-20, -100)
        };

        /// <summary>
        /// Массив прямоугольников для выдавливания в верхней части корпуса.
        /// Первым элементом в массиве является центр окружности по X,
        /// вторым элементом по Y.
        /// </summary>
        private readonly Point2D[,] _upperBodyPartExtrudingRectanglesCoordinates = new Point2D[,]
        {
            {
                new Point2D (-19.203345439322, 663.772206526744),
                new Point2D (18.796654560678, 663.772206526744),
                new Point2D (18.796654560678, 721.772206526744),
                new Point2D (-19.203345439322, 721.772206526744)
            },
            {
                new Point2D (-19.203345439322, 858.526455211801),
                new Point2D (18.796654560678, 858.526455211801),
                new Point2D(18.796654560678, 878.526455211801),
                new Point2D(-19.203345439322, 878.526455211801)
            },
            {
                new Point2D(-22.203345439322, 784.715832632264),
                new Point2D(21.796654560678, 784.715832632264),
                new Point2D(21.796654560678, 842.715832632264),
                new Point2D(-22.203345439322, 842.715832632264)
            },
            {
                new Point2D(-15.203345439322, 669.272206526744),
                new Point2D(14.796654560678, 669.272206526744),
                new Point2D(14.796654560678, 716.272206526744),
                new Point2D(-15.203345439322, 716.272206526744)
            },
            {
                new Point2D(-15.203345439322, 861.526455211801),
                new Point2D(14.796654560678, 861.526455211801),
                new Point2D(14.796654560678, 875.526455211801),
                new Point2D(-15.203345439322, 875.526455211801)
            },
            {
                new Point2D(-19.203345439322, 788.215832632264),
                new Point2D(18.796654560678, 788.215832632264),
                new Point2D(18.796654560678, 839.215832632264),
                new Point2D(-19.203345439322, 839.215832632264)
            }
        };

        /// <summary>
        /// Массив окружностей для выдавливания в задней части корпуса.
        /// </summary>
        private readonly Circle[] _backBodyPartExtrudingCirclesParameters =
        {
            new Circle(new Point2D(0,0), 20)
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
        public Point3D UpperBasePlaneCoordinate
        {
            get
            {
                return _upperBasePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра верхней грани корпуса звездолёта. 
        /// </summary>
        public Point3D UpperFacePlaneCoordinate
        {
            get
            {
                return _upperFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра нижней грани корпуса звездолёта. 
        /// </summary>
        public Point3D LowerFacePlaneCoordinate
        {
            get
            {
                return _lowerFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра передней грани верхней части корпуса звездолёта. 
        /// </summary>
        public Point3D UpperBodyFrontPlaneCoordinate
        {
            get
            {
                return _upperBodyFrontPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра задней грани нижней части корпуса звездолёта. 
        /// </summary>
        public Point3D LowerBodyBackPlaneCoordinate
        {
            get
            {
                return _lowerBodyBackPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра боковой грани нижней части корпуса звездолёта. 
        /// </summary>
        public Point3D LowerSideBackPlaneCoordinate
        {
            get
            {
                return _lowerBodySidePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра задней плоскости носовой части корпуса звездолёта. 
        /// </summary>
        public Point3D BowBodyBackPlaneCoordinate
        {
            get
            {
                return _bowBodyBackPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public Point3D UpperBodyPartFacePlaneCoordinate
        {
            get
            {
                return _upperBodyPartFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости углубления верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public Point3D DeepingBodyPartFacePlaneCoordinate
        {
            get
            {
                return _deepingBodyPartFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости углубления верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public Point3D BaseDroidHeadPlaneCoordinate
        {
            get
            {
                return _baseDroidHeadPlaneCoordinate;
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
        public Point3D BackDeepingPlaneCoordinate
        {
            get
            {
                return _backDeepingPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает массив координат верхнего основания корпуса звездолёта. 
        /// </summary>
        public Point2D[] BaseVertexes
        {
            get
            {
                return _baseVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат верхней грани корпуса звездолёта. 
        /// </summary>
        public Point2D[] UpperFaceVertexes
        {
            get
            {
                return _upperFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат нижней грани корпуса звездолёта. 
        /// </summary>
        public Point2D[] LowerFaceVertexes
        {
            get
            {
                return _lowerFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин первого выреза верхней части корпуса звездолёта. 
        /// </summary>
        public Point2D[] FirstUpperBodyCutoutVertexes
        {
            get
            {
                return _firstUpperBodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин второго выреза верхней части корпуса звездолёта. 
        /// </summary>
        public Point2D[] SecondUpperBodyCutoutVertexes
        {
            get
            {
                return _secondUpperBodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин первого выреза нижней части корпуса звездолёта. 
        /// </summary>
        public Point2D[] FirstLowerBodyCutoutVertexes
        {
            get
            {
                return _firstLowerBodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин второго выреза нижней части корпуса звездолёта. 
        /// </summary>
        public Point2D[] SecondLowerBodyCutoutVertexes
        {
            get
            {
                return _secondLowerBodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин среза нижней части корпуса звездолёта. 
        /// </summary>
        public Point2D[] LowerBodySliceVertexes
        {
            get
            {
                return _lowerBodySliceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        public Point2D[] BowBodyFaceVertexes
        {
            get
            {
                return _bowBodyFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        public Point2D[] DeepingUpperBodyFaceVertexes
        {
            get
            {
                return _deepingUpperBodyFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив окружностей для выдавливания в верхней части корпуса.
        /// </summary>
        public Circle[] UpperBodyPartExtrudingCirclesParameters
        {
            get
            {
                return _upperBodyPartExtrudingCirclesParameters;
            }
        }

        /// <summary>
        /// Возвращает массив прямоугольников для выдавливания в верхней части корпуса.
        /// </summary>
        public Point2D[,] UpperBodyPartExtrudingRectanglesCoordinates
        {
            get
            {
                return _upperBodyPartExtrudingRectanglesCoordinates;
            }
        }

        /// <summary>
        /// Возвращает массив координат углубления задней части корпуса.
        /// </summary>
        public Point2D[] BackBodyDeepingVertexes
        {
            get
            {
                return _backBodyDeepingVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив окружностей для выдавливания в задней части корпуса.
        /// </summary>
        public Circle[] BackBodyPartExtrudingCirclesParameters
        {
            get
            {
                return _backBodyPartExtrudingCirclesParameters;
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
