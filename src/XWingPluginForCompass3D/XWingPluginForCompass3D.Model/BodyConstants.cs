namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант для построения основного корпуса звездолета.
    /// </summary>
    public class BodyConstants
    {
        /// <summary>
        /// Создает экземпляр класса констант для построения корпуса.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса звездолета.</param>
        public BodyConstants(double bodyLength)
        {
            UpperFacePlane 
                = new Point3D(0, 78.493198, -600 - (bodyLength / 2));
            UpperFaceVertexes = new[,]
            {
                {
                    new Point2D(-54.395689, 600 + bodyLength),
                    new Point2D(54.395689, 600 + bodyLength),
                    new Point2D(54.395689, 600),
                    new Point2D(-54.395689, 600)
                }                
            };
            LowerFacePlane 
                = new Point3D(0, -71.493198, -600 - (bodyLength / 2));
            LowerFaceVertexes = new[,]
            {
                {
                    new Point2D(-49.470255, -600-bodyLength),
                    new Point2D(49.470255, -600-bodyLength),
                    new Point2D(49.470255, -600),
                    new Point2D(-49.470255, -600)
                }                
            };
            LowerSideBackPlane 
                = new Point3D(-49.470255, -96.493198, -600 - (bodyLength / 2));
            BackBodyPlane 
                = new Point3D(0, 0, -600 - bodyLength);
            BackDeepPlane 
                = new Point3D(87.3, 11.5, -590 - bodyLength);
        }

        /// <summary>
        /// Координата центра плоскости верхнего основания корпуса звездолета.
        /// </summary>
        public Point3D UpperBasePlane { get; } = new Point3D(0, 0, -600);

        /// <summary>
        /// Координата центра плоскости верхней грани звездолета.
        /// </summary>
        public Point3D UpperFacePlane { get; }

        /// <summary>
        /// Координата центра плоскости нижней грани звездолета.
        /// </summary>
        public Point3D LowerFacePlane { get; }

        /// <summary>
        /// Координата центра плоскости боковой грани нижней части корпуса звездолета.
        /// </summary>
        public Point3D LowerSideBackPlane { get; }

        /// <summary>
        /// Координата центра задней плоскости носовой части корпуса звездолета.
        /// </summary>
        public Point3D BowBodyBackPlane { get; } = 
            new Point3D(0, 103.3980655, -597.8211064);

        /// <summary>
        /// Координата центра плоскости верхней выпуклой части корпуса звездолета.
        /// </summary>
        public Point3D UpperBodyPartFacePlane { get; } = 
            new Point3D(0, 128.493198, -660);

        /// <summary>
        /// Координата плоскости углубления верхней выпуклой части корпуса звездолета.
        /// </summary>
        public Point3D DeepBodyPartFacePlane { get; } = 
            new Point3D(0, 123.493198, -660);

        /// <summary>
        /// Координата центра плоскости основания головы робота звездолета.
        /// </summary>
        public Point3D BaseDroidHeadPlane { get; } = 
            new Point3D(-0.2033454, 133.493198, -632.6372698);

        /// <summary>
        /// Координата центра плоскости задней части корпуса звездолета.
        /// </summary>
        public Point3D BackBodyPlane { get; }

        /// <summary>
        /// Координата центра плоскости углубления задней части корпуса звездолета.
        /// </summary>
        public Point3D BackDeepPlane { get; }

        /// <summary>
        /// Массив координат вершин верхнего основания корпуса звездолета.
        /// </summary>
        public Point2D[,] BaseVertexes { get; } =
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
        /// Массив координат вершин верхней грани корпуса звездолета.
        /// </summary>
        public Point2D[,] UpperFaceVertexes { get; }

        /// <summary>
        /// Массив координат вершин нижней грани корпуса звездолета.
        /// </summary>
        public Point2D[,] LowerFaceVertexes { get; }

        /// <summary>
        /// Массив координат вершин выреза выдавленных частей корпуса звездолета.
        /// В ходе программы координаты отразятся.
        /// </summary>
        public Point2D[,] BodyCutoutVertexes { get; } =
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
        /// Массив координат вершин среза нижней части корпуса звездолета.
        /// </summary>
        public Point2D[,] LowerBodySliceVertexes { get; } =
        {
            {
                new Point2D (600, 71.493198),
                new Point2D (600, 121.493198),
                new Point2D (650, 121.493198)
            }            
        };

        /// <summary>
        /// Массив координат вершин задней плоскости носовой части корпуса звездолета.
        /// (Поле необходимо для того, чтобы убрать зазор между носом и корпусом)
        /// </summary>
        public Point2D[,] BowBodyFaceVertexes { get; } =
        {
            {
                new Point2D (54.395689, 25.901062),
                new Point2D (26.115362, 75.697928),
                new Point2D (-26, 75.901062150386),
                new Point2D (-54.395689, 25.901062)
            }            
        };

        /// <summary>
        /// Массив координат вершин углубления верхней части корпуса звездолета.
        /// </summary>
        public Point2D[,] DeepUpperBodyFaceVertexes { get; } =
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
        public Circle[,] UpperBodyExtrudingCircles { get; } =
        {
            {
                new Circle (new Point2D (-0.2033454, 632.6372698), 25),
                new Circle (new Point2D (-0.2033454, 750.3556376), 22),
                new Circle (new Point2D (-0.2033454, 706.4660939), 8),
                new Circle (new Point2D (-0.2033454, 678.8481419), 8),
                new Circle (new Point2D (-0.2033454, 750.3556376), 19)
            }
        };

        /// <summary>
        /// Массив прямоугольников для выдавливания в верхней части корпуса.
        /// </summary>
        public Point2D[,] UpperBodyExtrudingRectangles { get; } =
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
        /// Массив координат вершин выреза задней части корпуса звездолета.
        /// </summary>
        public Point2D[,] BackBodyDeepVertexes { get; } =
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
        /// Дуга для построения головы робота.
        /// </summary>
        public Arc DroidHeadArc { get; } = new Arc(
            new Point2D(0.2033454, 632.6372698), 25,
            new Point2D(-0.2463156, 657.6332256), 
            new Point2D(-0.2463156, 607.6413140), 1);

        /// <summary>
        /// Массив окружностей для выдавливания в задней части корпуса.
        /// </summary>
        public Circle[,] BackDrawingCircles { get; } =
        {
            {
                new Circle (new Point2D (0, 0), 20)
            }
        };

        /// <summary>
        /// Массив точек, используемых для построения отрезков рисунка задней части корпуса.
        /// </summary>
        public Point2D[,,] BackDrawingSegments { get; } =
        {
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
            }
        };
    }
}