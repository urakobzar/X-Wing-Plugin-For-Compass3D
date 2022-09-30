using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант, необходимых для построения носовой части детали XWing
    /// </summary>
    public class BowBodyXWingConstants
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bowLength">Длина носа звездолёта</param>
        public BowBodyXWingConstants(double bowLength)
        {
            _tipBasePlaneCoordinate = new double[]
            { 0, 0, bowLength};
        }

        /// <summary>
        /// Координата центра плоскости верхней грани носовой части звездолёта.
        /// </summary>
        private readonly double[] _upperFacePlaneCoordinate =
            { 0, 52.246599057777, -300 };

        /// <summary>
        /// Координата центра плоскости передней грани кокпита.
        /// </summary>
        private readonly double[] _cockpitFrontFacePlaneCoordinate =
            { 0, 50.904867452294, 2.178893568691 };

        /// <summary>
        /// Координата центра плоскости боковой грани кокпита.
        /// </summary>
        private readonly double[] _cockpitSideFacePlaneCoordinate =
            { -39.913887624665, 76.626534528915, -291.821106431309 };

        /// <summary>
        /// Координата центра плоскости основания острия носовой части звездолёта.
        /// </summary>
        private readonly double[] _tipBasePlaneCoordinate;

        /// <summary>
        /// Координата центра верхнего ребра острия носовой части звездолёта.
        /// </summary>
        private readonly double[] _tipUpperEdgeCoordinate =
            {-0.425689731596, 17.251133647408, 100};

        /// <summary>
        /// Координата центра нижнего ребра острия носовой части звездолёта.
        /// </summary>
        private readonly double[] _tipLowerEdgeCoordinate =
            {0, -10.251133647408, 100};

        /// <summary>
        /// Расстояния фаски верхнего ребра острия носовой части звездолёта.
        /// Первый элемент массива, это катет.
        /// Второй элемент массива - это гипотенуза.
        /// </summary>
        private readonly double[] _tipUpperEdgeChamferDistances =
            {20, 54.949548389092};

        /// <summary>
        /// Расстояния фаски нижнего ребра острия носовой части звездолёта.
        /// Первый элемент массива, это катет.
        /// Второй элемент массива - это гипотенуза.
        /// </summary>
        private readonly double[] _tipLowerEdgeChamferDistances =
            {15, 55.980762113533};

        /// <summary>
        /// Массив координат вершин в верхнем основании носовой части звездолёта.
        /// </summary>
        private readonly double[,] _upperBaseVertexes =
        {
            { -43, 0 },
            {-26, 26 },
            {26, 26 },
            {43, 0 },
            {26, -19 },
            {-26, -19 }
        };

        /// <summary>
        /// Массив координат вершин грани носовой части звездолёта.
        /// </summary>
        private readonly double[,] _upperFaceVertexes =
        {
            { 54.395689029929, 604.557951837448 },
            {-54.395689029929, 604.557951837448},
            {-26, 2.266049311439 },
            {26, 2.266049311439 }
        };

        /// <summary>
        /// Массив координат вершин первого выреза кокпита звездолёта.
        /// </summary>
        private readonly double[,] _firstCockpitCutoutVertexes =
        {
            { -26, 75.901062150385 },
            {-54.395689029929, 75.901062150385},
            {-54.395689029929, 25.901062150385 }
        };

        /// <summary>
        /// Массив координат вершин второго выреза кокпита звездолёта.
        /// </summary>
        private readonly double[,] _secondCockpitCutoutVertexes =
        {
            {26, 75.901062150385 },
            {54.395689029929, 75.901062150385},
            {54.395689029929, 25.901062150385}
        };

        /// <summary>
        /// Массив координат вершин второго выреза кокпита звездолёта.
        /// </summary>
        private readonly double[,] _cockpitSliceVertexes =
        {
            {-1, 25.901062150385},
            {-1, 75.901062150385},
            {-605.111616473601, 75.901062150386}
        };

        /// <summary>
        /// Массив координат вершин основания острия носовой части звездолёта.
        /// </summary>
        private readonly double[,] _tipBowBodyVertexes =
        {
            {-43, 0},
            {-26, 26},
            {25, 26},
            {43, 0},
            {26, -19},
            {-26, -19}
        };

        /// <summary>
        /// Массив координат центра ребер скругления передней носовой части звездолёта.
        /// </summary>
        private readonly double[,,] _tipFrontFilletEdgeCoordinates =
        {
            {
                {28.720056217208, -7.259103331883, 66.313406124592}
            },
            {
                {-28.755495846165, -7.196422527305, 66.489247908264}
            },
            {
                {27.634400064284, 11.74339370277, 67.997602962077}
            },
            {
                {-0.113051518725, 1.446485054632, 90.735654691904}
            },
            {
                {-28.21381908901, 11.74339370277, 67.997602962077}
            }
        };

        /// <summary>
        /// Массив координат центра ребер скругления боковой носовой части звездолёта.
        /// </summary>
        private readonly double[,,] _tipSideFilletEdgeCoordinates =
        {
            {
                {-38.479522122003, 0.341549134121, 41.109212708249}
            },
            {
                {38.43038578768, 0.284493891452, 41.092916697691}
            }
        };

        /// <summary>
        /// Возвращает массив координат вершин в верхнем основании носовой части звездолёта. 
        /// </summary>
        public double[,] UpperBaseVertexes
        {
            get
            {
                return _upperBaseVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат верхней грани носовой части звездолёта. 
        /// </summary>
        public double[,] UpperFaceVertexes
        {
            get
            {
                return _upperFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин первого выреза кокпита звездолёта. 
        /// </summary>
        public double[,] FirstCockpitCutoutVertexes
        {
            get
            {
                return _firstCockpitCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин второго выреза кокпита звездолёта. 
        /// </summary>
        public double[,] SecondCockpitCutoutVertexes
        {
            get
            {
                return _secondCockpitCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин среза кокпита звездолёта. 
        /// </summary>
        public double[,] CockpitSliceVertexes
        {
            get
            {
                return _cockpitSliceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин основания острия носовой части звездолёта.
        /// </summary>
        public double[,] TipBowBodyVertexes
        {
            get
            {
                return _tipBowBodyVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат центра ребер скругления передней носовой части звездолёта.
        /// </summary>
        public double[,,] TipFrontFilletEdgeCoordinates
        {
            get
            {
                return _tipFrontFilletEdgeCoordinates;
            }
        }

        /// <summary>
        /// Возвращает массив координат центра ребер скругления боковой носовой части звездолёта.
        /// </summary>
        public double[,,] TipSideFilletEdgeCoordinates
        {
            get
            {
                return _tipSideFilletEdgeCoordinates;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости верхней грани носовой части звездолёта. 
        /// </summary>
        public double[] UpperFacePlaneCoordinate
        {
            get
            {
                return _upperFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости передней грани кокпита. 
        /// </summary>
        public double[] CockpitFrontFacePlaneCoordinate
        {
            get
            {
                return _cockpitFrontFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости боковой грани кокпита. 
        /// </summary>
        public double[] CockpitSideFacePlaneCoordinate
        {
            get
            {
                return _cockpitSideFacePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости основания острия носовой части звездолёта. 
        /// </summary>
        public double[] TipBaseFrontPlaneCoordinate
        {
            get
            {
                return _tipBasePlaneCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра верхнего ребра острия носовой части звездолёта.  
        /// </summary>
        public double[] TipUpperEdgeCoordinate
        {
            get
            {
                return _tipUpperEdgeCoordinate;
            }
        }

        /// <summary>
        /// Возвращает координату центра нижнего ребра острия носовой части звездолёта. 
        /// </summary>
        public double[] TipLowerEdgeCoordinate
        {
            get
            {
                return _tipLowerEdgeCoordinate;
            }
        }

        /// <summary>
        /// Возвращает расстояния фаски нижнего ребра острия носовой части звездолёта.  
        /// </summary>
        public double[] TipUpperEdgeChamferDistances
        {
            get
            {
                return _tipUpperEdgeChamferDistances;
            }
        }

        /// <summary>
        /// Возвращает расстояния фаски нижнего ребра острия носовой части звездолёта. 
        /// </summary>
        public double[] TipLowerEdgeChamferDistances
        {
            get
            {
                return _tipLowerEdgeChamferDistances;
            }
        }
    }
}
