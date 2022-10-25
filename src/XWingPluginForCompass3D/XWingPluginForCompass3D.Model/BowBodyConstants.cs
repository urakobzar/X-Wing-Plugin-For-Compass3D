namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант, необходимых для построения носовой части детали XWing.
    /// </summary>
    public class BowBodyConstants
    {
        /// <summary>
        /// Создает экземпляр класса констант для построения носовой части.
        /// </summary>
        /// <param name="bowLength">Длина носа звездолета</param>
        public BowBodyConstants(double bowLength)
        {
            TipFrontBasePlane = new Point3D(0, 0, bowLength);
        }

        /// <summary>
        /// Массив точек для построения отрезков верхнего основания
        /// носовой части звездолета.
        /// </summary>
        public Point2D[,,] UpperBaseSegments { get; } =
        {
            {
                { new Point2D(-43, 0), new Point2D(-26, 26) },
                { new Point2D(-26, 26), new Point2D(26, 26) },
                { new Point2D(26, 26), new Point2D(43, 0) },
                { new Point2D(43, 0), new Point2D(26, -19) },
                { new Point2D(26, -19), new Point2D(-26, -19) },
                { new Point2D(-26, -19), new Point2D(-43, 0) }
            }            
        };

        /// <summary>
        /// Массив точек для построения отрезков
        /// грани носовой части звездолета.
        /// </summary>
        public Point2D[,,] UpperFaceSegments { get; } =
        {
            {
                { new Point2D(54.3956890, 604.5579518), 
                    new Point2D(-54.3956890, 604.5579518) },
                { new Point2D(-54.3956890, 604.5579518), 
                    new Point2D(-26, 2.2660493) },
                { new Point2D(-26, 2.2660493), 
                    new Point2D(26, 2.2660493) },
                { new Point2D(26, 2.2660493), 
                    new Point2D(54.3956890, 604.5579518) }
            }            
        };

        /// <summary>
        /// Массив точек для построения отрезков выреза кокпита звездолета.
        /// В ходе программы координаты отразятся.
        /// </summary>
        public Point2D[,,] CockpitCutoutSegments { get; } =
        {
            {
                { new Point2D(-26, 75.9010621), 
                    new Point2D(-54.3956890, 75.9010621) },
                {new Point2D(-54.3956890, 75.9010621), 
                    new Point2D(-54.3956890, 25.9010621)},
                { new Point2D(-54.3956890, 25.9010621), 
                    new Point2D(-26, 75.9010621) }
            }
        };

        /// <summary>
        /// Массив точек для построения отрезков среза кокпита.
        /// </summary>
        public Point2D[,,] CockpitSliceSegments { get; } =
        {
            {
                { new Point2D(-1, 25.9010621), 
                    new Point2D(-1, 75.9010621) },
                { new Point2D(-1, 75.9010621), 
                    new Point2D(-605.1116164, 75.9010621) },
                { new Point2D(-605.1116164, 75.9010621), 
                    new Point2D(-1, 25.9010621) }
            }            
        };

        /// <summary>
        /// Массив точек для построения отрезков основания острия носовой части.
        /// </summary>
        public Point2D[,,] TipBowBodySegments { get; } =
        {
            {
                { new Point2D(-43, 0), new Point2D(-26, 26) },
                { new Point2D(-26, 26), new Point2D(25, 26) },
                { new Point2D(25, 26), new Point2D(43, 0) },
                { new Point2D(43, 0), new Point2D(26, -19) },
                { new Point2D(26, -19), new Point2D(-26, -19) },
                { new Point2D(-26, -19), new Point2D(-43, 0) }
            }            
        };

        /// <summary>
        /// Массив координат центра ребер скругления передней носовой части.
        /// </summary>
        public Point3D[] TipFrontFilletEdgeCoordinates { get; } =
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
        public Point3D[] TipSideFilletEdgeCoordinates { get; } =
        {
            new Point3D(-38.4795221, 0.3415491, 41.1092127),
            new Point3D(38.4303857, 0.2844938, 41.0929166)
        };

        /// <summary>
        /// Координата центра плоскости верхней грани носовой части звездолета.
        /// </summary>
        public Point3D UpperFacePlane { get; } = 
            new Point3D(0, 52.2465990, -300);

        /// <summary>
        /// Координата центра плоскости передней грани кокпита.
        /// </summary>
        public Point3D CockpitFrontFacePlane { get; } = 
            new Point3D(0, 50.9048674, 2.1788935);

        /// <summary>
        /// Координата центра плоскости боковой грани кокпита.
        /// </summary>
        public Point3D CockpitSideFacePlane { get; } = 
            new Point3D(-39.9138876, 76.6265345, -291.8211064);

        /// <summary>
        /// Координата центра плоскости основания острия носовой части звездолета.
        /// </summary>
        public Point3D TipFrontBasePlane { get; }

        /// <summary>
        /// Координата центра верхнего ребра острия носовой части звездолета.
        /// </summary>
        public Point3D TipUpperEdgePoint { get; } = 
            new Point3D(-0.4256897, 17.2511336, 100);

        /// <summary>
        /// Координата центра нижнего ребра острия носовой части звездолета.
        /// </summary>
        public Point3D TipLowerEdgePoint { get; } = 
            new Point3D(0, -10.2511336, 100);

        /// <summary>
        /// Расстояния фаски верхнего ребра острия носовой части звездолета.
        /// Первый элемент массива, это катет.
        /// Второй элемент массива - это гипотенуза.
        /// </summary>
        public double[] TipUpperChamferDistances { get; } = {20, 54.9495483};

        /// <summary>
        /// Расстояния фаски нижнего ребра острия носовой части звездолета.
        /// Первый элемент массива, это катет.
        /// Второй элемент массива - это гипотенуза.
        /// </summary>
        public double[] TipLowerChamferDistances { get; } = {15, 55.9807621};
    }
}