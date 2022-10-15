namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс констант, необходимый для построения крыльев звездолета.
    /// </summary>
    public class WingsConstants
    {
        /// <summary>
        /// Создает экземпляр класса констант для построения крыльев.
        /// </summary>
        /// <param name="wingsWidth">Ширина крыльев.</param>
        /// <param name="bodyLength">Длина корпуса.</param>
        public WingsConstants(double wingsWidth, double bodyLength)
        {
            BackBodyPlane = new Point3D(0, 0, -600 - bodyLength);

            CuttingPlane = new Point3D(0, -121.493198, -600 - bodyLength);

            WingsCutVertexes = new[,,]
            {
                {
                    {
                        new Point2D(646.963505, 
                            -600 - bodyLength + wingsWidth),
                        new Point2D(646.963505, 
                            -666.419923 - bodyLength + wingsWidth)
                    },
                    {
                        new Point2D(646.963505, 
                            -666.419923 - bodyLength + wingsWidth),
                        new Point2D(128.282347, 
                            -600 - bodyLength + wingsWidth)
                    },
                    {
                        new Point2D(128.282347, 
                            -600 - bodyLength + wingsWidth),
                        new Point2D(646.963505, 
                            -600 - bodyLength + wingsWidth)
                    }
                },

                {
                    { new Point2D(646.963505, -600 - bodyLength), 
                        new Point2D(646.963505, -550 - bodyLength) },
                    {new Point2D(646.963505, -550 - bodyLength), 
                        new Point2D(128.282347, -600 - bodyLength)},
                    { new Point2D(128.282347, -600 - bodyLength), 
                        new Point2D(646.963505, -600 - bodyLength) }
                }
            };
        }

        /// <summary>
        /// Координата центра плоскости задней части корпуса.
        /// </summary>
        public Point3D BackBodyPlane { get; }

        /// <summary>
        /// Координата центра плоскости выреза у крыльев.
        /// </summary>
        public Point3D CuttingPlane { get; }

        /// <summary>
        /// Массив точек для построения отрезков эскиза каждого крыла.
        /// В ходе программы координаты будут отражены.
        /// </summary>
        public Point2D[,,] BaseVertexes { get; } =
        {
            {
                { new Point2D(69.173833, 55.779384), 
                    new Point2D(78.023751, 42.034027) },
                { new Point2D(78.023751, 42.034027), 
                    new Point2D(632.086192, 150.642161) },
                { new Point2D(632.086192, 150.642161), 
                    new Point2D(639.549926, 169.837344) },
                { new Point2D(639.549926, 169.837344), 
                    new Point2D(69.173833, 55.779384) }
            },
            {
                { new Point2D(77.853158, -39.308584), 
                    new Point2D(64.171267, -54.321075) },
                { new Point2D(64.171267, -54.321075), 
                    new Point2D(646.963505, -163.875661) },
                { new Point2D(646.963505, -163.875661), 
                    new Point2D(640.123102, -145.081809) },
                { new Point2D(640.123102, -145.081809), 
                    new Point2D(77.853158, -39.308584) }
            }
        };

        /// <summary>
        /// Массив точек для построения отрезков выреза на крыльях.
        /// В ходе программы координаты будут отражены.
        /// </summary>
        public Point2D[,,] WingsCutVertexes { get; }
    }
}