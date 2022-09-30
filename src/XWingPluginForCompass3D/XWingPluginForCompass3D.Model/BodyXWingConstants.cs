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
    public class BodyXWingConstants
    {
        /// <summary>
        /// Конструктор класса, где учитывается введенный размер корпуса.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса звездолёта.</param>
        public BodyXWingConstants(double bodyLength)
        {
            _upperFacePlaneCoordinate = new double[]
            { 0, 78.493198, -600 - (bodyLength/2)};


            _upperFaceVertexes = new double[,]
            {
                { -54.395689, 600 + bodyLength},
                { 54.395689, 600 + bodyLength},
                { 54.395689, 600 },
                { -54.395689, 600 }
            };

            _lowerFacePlaneCoordinate = new double[]
            { 0, -71.493198, -600 - (bodyLength/2)};

            _lowerFaceVertexes = new double[,]
            {
                { -49.470255, -600-bodyLength},
                { 49.470255, -600-bodyLength},
                { 49.470255, -600 },
                { -49.470255, -600 }
            };

            _lowerBodyBackPlaneCoordinate = new double[]
            { 0, -96.493198, -600 - bodyLength };

            _lowerBodySidePlaneCoordinate = new double[]
            {-49.470255, -96.493198, -600 - (bodyLength/2)};

            _upperBodyPartFacePlaneCoordinate = new double[]
            {0, 128.493198, -600 - (bodyLength/2)};

            _deepingBodyPartFacePlaneCoordinate = new double[]
            {0, 123.493198, -600 - (bodyLength/2)};

            _backBodyPlaneCoordinate = new double[]
            {0, 0, -600 - bodyLength};

            _backDeepingPlaneCoordinate = new double[]
            {87.3, 11.5, -590 - bodyLength};
        }


        /// <summary>
        /// Координата центра плоскости верхнего основания корпуса звездолёта.
        /// </summary>
        private readonly double[] _upperBasePlaneCoordinate =
            { 0, 0, -600 };

        /// <summary>
        /// Координата центра плоскости верхней грани звездолёта.
        /// </summary>
        private readonly double[] _upperFacePlaneCoordinate;


        /// <summary>
        /// Координата центра плоскости нижней грани звездолёта.
        /// </summary>
        private readonly double[] _lowerFacePlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости передней грани верхней части корпуса звездолёта.
        /// </summary>
        private readonly double[] _upperBodyFrontPlaneCoordinate =
            {0, 103.493198, -600 };

        /// <summary>
        /// Координата центра плоскости задней грани нижней части корпуса звездолёта.
        /// </summary>
        private readonly double[] _lowerBodyBackPlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости боковой грани нижней части корпуса звездолёта.
        /// </summary>
        private readonly double[] _lowerBodySidePlaneCoordinate;

        /// <summary>
        /// Координата центра задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        private readonly double[] _bowBodyBackPlaneCoordinate =
            {0, 103.398065567848, -597.821106431309};

        /// <summary>
        /// Координата центра плоскости верхней выпуклой части корпуса звездолёта.
        /// </summary>
        private readonly double[] _upperBodyPartFacePlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости углубления верхней выпуклой части корпуса звездолёта.
        /// </summary>
        private readonly double[] _deepingBodyPartFacePlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости основания головы дроида звездолёта.
        /// </summary>
        private readonly double[] _baseDroidHeadPlaneCoordinate =
        { -0.203345439322, 133.493198, -632.637269856963 };

        /// <summary>
        /// Координата центра плоскости задней части корпуса звездолёта.
        /// </summary>
        private readonly double[] _backBodyPlaneCoordinate;

        /// <summary>
        /// Координата центра плоскости задней части корпуса звездолёта.
        /// </summary>
        private readonly double[] _backDeepingPlaneCoordinate;

        /// <summary>
        /// Массив координат вершин верхнего основания корпуса звездолёта.
        /// </summary>
        private readonly double[,] _baseVertexes =
        {
            { 108.977589, -4.985001},
            {54.395689, 78.493198},
            {-54.395689, 78.493198},
            {-108.977589, -4.985001},
            {-49.470255, -71.493198},
            {49.470255, -71.493198}
        };

        /// <summary>
        /// Массив координат вершин верхней грани корпуса звездолёта.
        /// </summary>
        private readonly double[,] _upperFaceVertexes;

        /// <summary>
        /// Массив координат вершин нижней грани корпуса звездолёта.
        /// </summary>
        private readonly double[,] _lowerFaceVertexes;

        /// <summary>
        /// Массив координат вершин первого выреза верхней части корпуса звездолёта.
        /// </summary>
        private readonly double[,] _firstUpperBodyCutoutVertexes =
        {
            { -54.395689, 128.493198 },
            {-54.395689, 78.493198},
            {-31.395689, 128.493198}
        };

        /// <summary>
        /// Массив координат вершин второго выреза верхней части корпуса звездолёта.
        /// </summary>
        private readonly double[,] _secondUpperBodyCutoutVertexes =
        {
            {54.395689, 128.493198},
            {54.395689, 78.493198},
            {31.395689, 128.493198}
        };

        /// <summary>
        /// Массив координат вершин первого выреза нижней части корпуса звездолёта.
        /// </summary>
        private readonly double[,] _firstLowerBodyCutoutVertexes =
        {
            {-49.470255, -121.493198},
            {-49.470255, -71.493198},
            {-26.470255, -121.493198}
        };

        /// <summary>
        /// Массив координат вершин второго выреза нижней части корпуса звездолёта.
        /// </summary>
        private readonly double[,] _secondLowerBodyCutoutVertexes =
        {
            {49.470255, -121.493198},
            {49.470255, -71.493198},
            {26.470255, -121.493198}
        };

        /// <summary>
        /// Массив координат вершин среза нижней части корпуса звездолёта.
        /// </summary>
        private readonly double[,] _lowerBodySliceVertexes =
        {
            {600, 71.493198},
            {600, 121.493198},
            {650, 121.493198}
        };

        /// <summary>
        /// Массив координат вершин задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        private readonly double[,] _bowBodyFaceVertexes =
        {
            {54.395689, 25.901062},
            {26.115362, 75.697928},
            {-26, 75.901062150386},
            {-54.395689, 25.901062}
        };

        /// <summary>
        /// Массив координат вершин углубления верхней части корпуса звездолёта.
        /// </summary>
        private readonly double[,] _deepingUpperBodyFaceVertexes =
        {
            {-25, 607.659914486825 },
            {25, 607.659914486825 },
            {25, 892.659914486825 },
            {-25, 892.659914486825 }
        };

        /// <summary>
        /// Массив окружностей для выдавливания в верхней части корпуса.
        /// Первым элементом в массиве является центр окружности по X,
        /// вторым элементом по Y, третий элемент - радиус
        /// </summary>
        private readonly double[,] _upperBodyPartExtrudingCirclesParameters =
        {
            {-0.203345439322, 632.637269856963, 25},
            {-0.203345439322, 750.355637638365, 22},
            {-0.203345439322, 706.466093921629, 8},
            {-0.203345439322, 678.848141946151, 8},
            {-0.203345439322, 750.355637638365, 19}
        };

        /// <summary>
        /// Массив координат вершин выреза задней части корпуса звездолёта.
        /// </summary>
        private readonly double[,] _backBodyDeepingVertexes =
        {
            {-97, -4},
            {-20, 123},
            {20, 123},
            {97, -4},
            {20, -100},
            {-20, -100}
        };

        /// <summary>
        /// Массив прямоугольников для выдавливания в верхней части корпуса.
        /// Первым элементом в массиве является центр окружности по X,
        /// вторым элементом по Y.
        /// </summary>
        private readonly double[,,] _upperBodyPartExtrudingRectanglesCoordinates =
        {
            {
                {-19.203345439322, 663.772206526744},
                {18.796654560678, 663.772206526744},
                {18.796654560678, 721.772206526744},
                {-19.203345439322, 721.772206526744}
            },
            {
                {-19.203345439322, 858.526455211801},
                {18.796654560678, 858.526455211801},
                {18.796654560678, 878.526455211801},
                {-19.203345439322, 878.526455211801}
            },
            {
                {-22.203345439322, 784.715832632264},
                {21.796654560678, 784.715832632264},
                {21.796654560678, 842.715832632264},
                {-22.203345439322, 842.715832632264}
            },
            {
                {-15.203345439322, 669.272206526744},
                {14.796654560678, 669.272206526744},
                {14.796654560678, 716.272206526744},
                {-15.203345439322, 716.272206526744}
            },
            {
                {-15.203345439322, 861.526455211801},
                {14.796654560678, 861.526455211801},
                {14.796654560678, 875.526455211801},
                {-15.203345439322, 875.526455211801}
            },
            {
                {-19.203345439322, 788.215832632264},
                {18.796654560678, 788.215832632264},
                {18.796654560678, 839.215832632264},
                {-19.203345439322, 839.215832632264}
            }
        };

        /// <summary>
        /// Массив окружностей для выдавливания в задней части корпуса.
        /// </summary>
        private readonly double[,] _backBodyPartExtrudingCirclesParameters =
        {
            { 0,0,20 }
        };

        /// <summary>
        /// Возвращает координату центра плоскости верхнего основания корпуса звездолёта. 
        /// </summary>
        public double[] UpperBasePlaneCoordinate
        {
            get
            {
                return _upperBasePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра верхней грани корпуса звездолёта. 
        /// </summary>
        public double[] UpperFacePlaneCoordinate
        {
            get
            {
                return _upperFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра нижней грани корпуса звездолёта. 
        /// </summary>
        public double[] LowerFacePlaneCoordinate
        {
            get
            {
                return _lowerFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра передней грани верхней части корпуса звездолёта. 
        /// </summary>
        public double[] UpperBodyFrontPlaneCoordinate
        {
            get
            {
                return _upperBodyFrontPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра задней грани нижней части корпуса звездолёта. 
        /// </summary>
        public double[] LowerBodyBackPlaneCoordinate
        {
            get
            {
                return _lowerBodyBackPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра боковой грани нижней части корпуса звездолёта. 
        /// </summary>
        public double[] LowerSideBackPlaneCoordinate
        {
            get
            {
                return _lowerBodySidePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра задней плоскости носовой части корпуса звездолёта. 
        /// </summary>
        public double[] BowBodyBackPlaneCoordinate
        {
            get
            {
                return _bowBodyBackPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public double[] UpperBodyPartFacePlaneCoordinate
        {
            get
            {
                return _upperBodyPartFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости углубления верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public double[] DeepingBodyPartFacePlaneCoordinate
        {
            get
            {
                return _deepingBodyPartFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости углубления верхней выпуклой части корпуса звездолёта. 
        /// </summary>
        public double[] BaseDroidHeadPlaneCoordinate
        {
            get
            {
                return _baseDroidHeadPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости задней части корпуса звездолёта.
        /// </summary>
        public double[] BackBodyPlaneCoordinate
        {
            get
            {
                return _backBodyPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости углубления задней части корпуса звездолёта.
        /// </summary>
        public double[] BackDeepingPlaneCoordinate
        {
            get
            {
                return _backDeepingPlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает массив координат верхнего основания корпуса звездолёта. 
        /// </summary>
        public double[,] BaseVertexes
        {
            get
            {
                return _baseVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат верхней грани корпуса звездолёта. 
        /// </summary>
        public double[,] UpperFaceVertexes
        {
            get
            {
                return _upperFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат нижней грани корпуса звездолёта. 
        /// </summary>
        public double[,] LowerFaceVertexes
        {
            get
            {
                return _lowerFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин первого выреза верхней части корпуса звездолёта. 
        /// </summary>
        public double[,] FirstUpperBodyCutoutVertexes
        {
            get
            {
                return _firstUpperBodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин второго выреза верхней части корпуса звездолёта. 
        /// </summary>
        public double[,] SecondUpperBodyCutoutVertexes
        {
            get
            {
                return _secondUpperBodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин первого выреза нижней части корпуса звездолёта. 
        /// </summary>
        public double[,] FirstLowerBodyCutoutVertexes
        {
            get
            {
                return _firstLowerBodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин второго выреза нижней части корпуса звездолёта. 
        /// </summary>
        public double[,] SecondLowerBodyCutoutVertexes
        {
            get
            {
                return _secondLowerBodyCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин среза нижней части корпуса звездолёта. 
        /// </summary>
        public double[,] LowerBodySliceVertexes
        {
            get
            {
                return _lowerBodySliceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        public double[,] BowBodyFaceVertexes
        {
            get
            {
                return _bowBodyFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат задней плоскости носовой части корпуса звездолёта.
        /// </summary>
        public double[,] DeepingUpperBodyFaceVertexes
        {
            get
            {
                return _deepingUpperBodyFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив окружностей для выдавливания в верхней части корпуса.
        /// </summary>
        public double[,] UpperBodyPartExtrudingCirclesParameters
        {
            get
            {
                return _upperBodyPartExtrudingCirclesParameters;
            }
        }

        /// <summary>
        /// Возвращает массив прямоугольников для выдавливания в верхней части корпуса.
        /// </summary>
        public double[,,] UpperBodyPartExtrudingRectanglesCoordinates
        {
            get
            {
                return _upperBodyPartExtrudingRectanglesCoordinates;
            }
        }

        /// <summary>
        /// Возвращает массив координат углубления задней части корпуса.
        /// </summary>
        public double[,] BackBodyDeepingVertexes
        {
            get
            {
                return _backBodyDeepingVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив окружностей для выдавливания в задней части корпуса.
        /// </summary>
        public double[,] BackBodyPartExtrudingCirclesParameters
        {
            get
            {
                return _backBodyPartExtrudingCirclesParameters;
            }
        }
    }
}
