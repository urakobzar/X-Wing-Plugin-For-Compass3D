namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант, необходимых для построения носовой части детали XWing.
    /// </summary>
    public class BowBodyConstants
    {
        /// <summary>
        /// Создаёт экземпляр класса констант для построения носовой части.
        /// </summary>
        /// <param name="bowLength">Длина носа звездолёта</param>
        public BowBodyConstants(double bowLength)
        {
            _tipFrontBasePlane = new Point3D(0, 0, bowLength);
        }

        /// <summary>
        /// Координата центра плоскости верхней грани носовой части звездолёта.
        /// </summary>
        private readonly Point3D _upperFacePlane = 
            new Point3D(0, 52.2465990, -300);

        /// <summary>
        /// Координата центра плоскости передней грани кокпита.
        /// </summary>
        private readonly Point3D _cockpitFrontFacePlane =
            new Point3D(0, 50.9048674, 2.1788935);

        /// <summary>
        /// Координата центра плоскости боковой грани кокпита.
        /// </summary>
        private readonly Point3D _cockpitSideFacePlane =
            new Point3D(-39.9138876, 76.6265345, -291.8211064);

        /// <summary>
        /// Координата центра плоскости основания острия носовой части звездолёта.
        /// </summary>
        private readonly Point3D _tipFrontBasePlane;

        /// <summary>
        /// Координата центра верхнего ребра острия носовой части звездолёта.
        /// </summary>
        private readonly Point3D _tipUpperEdge =
            new Point3D(-0.4256897, 17.2511336, 100);

        /// <summary>
        /// Координата центра нижнего ребра острия носовой части звездолёта.
        /// </summary>
        private readonly Point3D _tipLowerEdge =
            new Point3D(0, -10.2511336, 100);

        /// <summary>
        /// Расстояния фаски верхнего ребра острия носовой части звездолёта.
        /// Первый элемент массива, это катет.
        /// Второй элемент массива - это гипотенуза.
        /// </summary>
        private readonly double[] _tipUpperEdgeChamferDistances =
            {20, 54.9495483};

        /// <summary>
        /// Расстояния фаски нижнего ребра острия носовой части звездолёта.
        /// Первый элемент массива, это катет.
        /// Второй элемент массива - это гипотенуза.
        /// </summary>
        private readonly double[] _tipLowerEdgeChamferDistances =
            {15, 55.9807621};

        /// <summary>
        /// Массив координат вершин в верхнем основании носовой части звездолёта.
        /// </summary>
        private readonly Point2D[,] _upperBaseVertexes =
        {
            {
                new Point2D(-43, 0),
                new Point2D(-26, 26),
                new Point2D(26, 26),
                new Point2D(43, 0),
                new Point2D(26, -19),
                new Point2D(-26, -19)
            }            
        };

        /// <summary>
        /// Массив координат вершин грани носовой части звездолёта.
        /// </summary>
        private readonly Point2D[,] _upperFaceVertexes =
        {
            {
                new Point2D(54.3956890, 604.5579518),
                new Point2D(-54.3956890, 604.5579518),
                new Point2D(-26, 2.2660493),
                new Point2D(26, 2.2660493)
            }            
        };

        /// <summary>
        /// Массив координат вершин выреза кокпита звездолёта.
        /// В ходе программы координаты отзеркалятся.
        /// </summary>
        private readonly Point2D[,] _cockpitCutoutVertexes =
        {
            {
                new Point2D(-26, 75.9010621),
                new Point2D(-54.3956890, 75.9010621),
                new Point2D(-54.3956890, 25.9010621)
            }
        };

        /// <summary>
        /// Массив координат вершин второго выреза кокпита.
        /// </summary>
        private readonly Point2D[,] _cockpitSliceVertexes =
        {
            {
                new Point2D(-1, 25.9010621),
                new Point2D(-1, 75.9010621),
                new Point2D(-605.1116164, 75.9010621)
            }            
        };

        /// <summary>
        /// Массив координат вершин основания острия носовой части.
        /// </summary>
        private readonly Point2D[,] _tipBowBodyVertexes =
        {
            {
                new Point2D(-43, 0),
                new Point2D(-26, 26),
                new Point2D(25, 26),
                new Point2D(43, 0),
                new Point2D(26, -19),
                new Point2D(-26, -19)
            }            
        };

        /// <summary>
        /// Массив координат центра ребер скругления передней носовой части.
        /// </summary>
        private readonly Point3D[] _tipFrontFilletEdgeCoordinates =
        {
            new Point3D(28.7200562, -7.2591033, 66.3134061),
            new Point3D(-28.7554958, -7.1964225, 66.4892479),
            new Point3D(27.6344000, 11.7433937, 67.9976029),
            new Point3D(-0.1130515, 1.4464850, 90.7356546),
            new Point3D(-28.2138190, 11.7433937, 67.9976029)
        };

        /// <summary>
        /// Массив координат центра ребер скругления боковой носовой части.
        /// </summary>
        private readonly Point3D[] _tipSideFilletEdgeCoordinates =
        {
            new Point3D(-38.4795221, 0.3415491, 41.1092127),
            new Point3D(38.4303857, 0.2844938, 41.0929166)
        };

        /// <summary>
        /// Возвращает массив координат вершин в верхнем основании носовой части. 
        /// </summary>
        public Point2D[,] UpperBaseVertexes
        {
            get
            {
                return _upperBaseVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат верхней грани носовой части. 
        /// </summary>
        public Point2D[,] UpperFaceVertexes
        {
            get
            {
                return _upperFaceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин первого выреза кокпита звездолёта. 
        /// </summary>
        public Point2D[,] CockpitCutoutVertexes
        {
            get
            {
                return _cockpitCutoutVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин среза кокпита звездолёта. 
        /// </summary>
        public Point2D[,] CockpitSliceVertexes
        {
            get
            {
                return _cockpitSliceVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат вершин основания острия носовой части.
        /// </summary>
        public Point2D[,] TipBowBodyVertexes
        {
            get
            {
                return _tipBowBodyVertexes;
            }
        }

        /// <summary>
        /// Возвращает массив координат центра ребер скругления передней носовой части.
        /// </summary>
        public Point3D[] TipFrontFilletEdgeCoordinates
        {
            get
            {
                return _tipFrontFilletEdgeCoordinates;
            }
        }

        /// <summary>
        /// Возвращает массив координат центра ребер скругления боковой носовой части.
        /// </summary>
        public Point3D[] TipSideFilletEdgeCoordinates
        {
            get
            {
                return _tipSideFilletEdgeCoordinates;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости верхней грани носовой части. 
        /// </summary>
        public Point3D UpperFacePlane
        {
            get
            {
                return _upperFacePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости передней грани кокпита. 
        /// </summary>
        public Point3D CockpitFrontFacePlane
        {
            get
            {
                return _cockpitFrontFacePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости боковой грани кокпита. 
        /// </summary>
        public Point3D CockpitSideFacePlane
        {
            get
            {
                return _cockpitSideFacePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра плоскости основания острия носовой части. 
        /// </summary>
        public Point3D TipFrontBasePlane
        {
            get
            {
                return _tipFrontBasePlane;
            }
        }

        /// <summary>
        /// Возвращает координату центра верхнего ребра острия носовой части.  
        /// </summary>
        public Point3D TipUpperEdge
        {
            get
            {
                return _tipUpperEdge;
            }
        }

        /// <summary>
        /// Возвращает координату центра нижнего ребра острия носовой части. 
        /// </summary>
        public Point3D TipLowerEdge
        {
            get
            {
                return _tipLowerEdge;
            }
        }

        /// <summary>
        /// Возвращает расстояния фаски верхнего ребра острия носовой части.  
        /// </summary>
        public double[] TipUpperEdgeChamferDistances
        {
            get
            {
                return _tipUpperEdgeChamferDistances;
            }
        }

        /// <summary>
        /// Возвращает расстояния фаски нижнего ребра острия носовой части. 
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