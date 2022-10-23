using System;
using XWingPluginForCompass3D.Model;

namespace XWingPluginForCompass3D.Wrapper
{
    /// <summary>
    /// Класс построения 3D-модели звездного истребителя X-Wing.
    /// </summary>
    public class XWingBuilder
    {
        /// <summary>
        /// Связь с Компас-3D.
        /// </summary>
        private KompasWrapper Wrapper { get; } = new KompasWrapper();

        /// <summary>
        /// Построение детали по заданным параметрам.
        /// </summary>
        /// <param name="xwing">Объект заданных параметров X-Wing.</param>
        public void BuildDetail(XWing xwing)
        {
            Wrapper.StartKompas();
            Wrapper.CreateDocument();
            Wrapper.SetDetailProperties();
            var bowBodyLength = 
                xwing.Parameters[XWingParameterType.BowLength].Value;
            var bodyLength = 
                xwing.Parameters[XWingParameterType.BodyLength].Value;
            var wingWidth = 
                xwing.Parameters[XWingParameterType.WingWidth].Value;
            var blasterTipLength = 
                xwing.Parameters[XWingParameterType.WeaponBlasterTipLength].Value;
            var turbineLength = 
                xwing.Parameters[XWingParameterType.AcceleratorTurbineLength].Value;
            var nozzleLength = 
                xwing.Parameters[XWingParameterType.AcceleratorNozzleLength].Value;
            // Разница между длиной корпуса и шириной крыльев.
            var bodyAndWingsDifference = bodyLength - wingWidth;
            BuildBowBody(bowBodyLength);
            BuildBody(bodyLength);
            BuildWings(wingWidth, bodyLength);
            BuildBlasters(blasterTipLength,
                bodyAndWingsDifference, wingWidth);
            BuildAccelerators(turbineLength,
                nozzleLength, bodyAndWingsDifference);
        }

        /// <summary>
        /// Построение носовой части корпуса.
        /// </summary>
        /// <param name="bowLength">Длина носа.</param>
        private void BuildBowBody(double bowLength)
        {
            // Объект класса констант для построения носовой части детали.            
            var constants = new BowBodyConstants(bowLength);

            // Выдавливание основы носовой части корпуса
            Wrapper.Sketch = 
                Wrapper.BuildPolygonByDefaultPlane(Wrapper.DefaultPlaneXoY,
                constants.UpperBaseVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 600, 
                false, 5, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, bowLength,
	            true, -3, false);

            // Выдавливание кабины            
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.UpperFacePlane,
                constants.UpperFaceVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 50,
	            true, 0, false);

            // Вырез кабины                        
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.CockpitFrontFacePlane,
                constants.CockpitCutoutVertexes, true);
            Wrapper.CutExtrusion(Wrapper.Sketch, 602.5, true);

            // Срез кабины
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.CockpitSideFacePlane,
                constants.CockpitSliceVertexes, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 100, true);

            // Острие носа
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.TipFrontBasePlane,
                constants.TipBowBodyVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 100, 
                true, -5, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 35, 
                false, -5, false);

            // Фаска верхней части острия носовой части
            Wrapper.CreateChamfer(bowLength, constants.TipUpperChamferDistances,
                constants.TipUpperEdge);

            // Фаска нижней части острия носовой части
            Wrapper.CreateChamfer(bowLength, constants.TipLowerChamferDistances,
                constants.TipLowerEdge);

            // Скругление основной части острия
            Wrapper.CreateFillet(bowLength, 
                constants.TipFrontFilletEdgeCoordinates, 5);

            // Скругление боковой части острия
            Wrapper.CreateFillet(bowLength, 
                constants.TipSideFilletEdgeCoordinates, 10);
        }

        /// <summary>
        /// Построение корпуса.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса.</param>
        private void BuildBody(double bodyLength)
        {            
            // Объект класса констант для построения корпуса детали.            
            var constants = new BodyConstants(bodyLength);

            // Выдавливание основного корпуса на заданную пользователем величину.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.UpperBasePlane,
                constants.BaseVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, bodyLength, 
                true, 0, false);

            // Выдавливание верхней части корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.UpperFacePlane,
                constants.UpperFaceVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 50, 
                true, 0, false);

            // Выдавливание нижней части корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.LowerFacePlane,
                constants.LowerFaceVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 50, 
                true, 0, false);

            // Срез нижней части корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.LowerSideBackPlane,
                constants.LowerBodySliceVertexes, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 100, true);

            // Вырез нижней части корпуса: срезаются углы призмы.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.BackBodyPlane,
                constants.BodyCutoutVertexes, true);
            Wrapper.CutExtrusion(Wrapper.Sketch, bodyLength, true);

            // Выдавливание верхней задней грани носовой части корпуса:
            // чтобы не было зазора с основным корпусом.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.BowBodyBackPlane,
                constants.BowBodyFaceVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 4.5, 
                true, 0, false);

            // Углубление для верхней части корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.UpperBodyPartFacePlane,
                constants.DeepUpperBodyFaceVertexes, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 5, true);

            // Выдавливание окружностей в верхней части корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.DeepBodyPartFacePlane,
                constants.UpperBodyExtrudingCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, 
                true, 0, false);

            // Выдавливание прямоугольников с вырезом в верхней части корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.DeepBodyPartFacePlane,
                constants.UpperBodyExtrudingRectangles, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, 
                true, 0, false);

            // Построение головы робота в верхней части корпуса.
            Wrapper.BuildHemisphere(constants.BaseDroidHeadPlane, 
                constants.DroidHeadArc);

            // Углубление задней части корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.BackBodyPlane,
                constants.BackBodyDeepVertexes, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 10, true);

            // Первый рисунок задней части корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.BackDeepPlane,
                constants.BackDrawingCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, 
                true, 0, false);

            // Второй рисунок задней части корпуса.
            Wrapper.Sketch = Wrapper.BuildSetSegments(constants.BackDeepPlane, 
                constants.BackDrawingSegments, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, 
                true, 0, true);
        }

        /// <summary>
        /// Построение крыльев.
        /// </summary>
        /// <param name="wingsWidth">Ширина крыльев.</param>
        /// <param name="bodyLength">Длина корпуса.</param>
        private void BuildWings(double wingsWidth, double bodyLength)
        {
            // Объект класса констант для построения крыльев.
            var wingsConstants = new WingsConstants(wingsWidth, bodyLength);

            // Выдавливание крыльев, начиная с конца корпуса.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(wingsConstants.BackBodyPlane,
                wingsConstants.BaseVertexes, true);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, wingsWidth,
                false, 0, false);

            // Вырезание формы крыла.
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(wingsConstants.CuttingPlane,
                wingsConstants.WingsCutVertexes, true);
            Wrapper.CutExtrusion(Wrapper.Sketch, 350, true);
            Wrapper.CutExtrusion(Wrapper.Sketch, 100, false);
        }

        /// <summary>
        /// Построение бластеров.
        /// </summary>
        /// <param name="blasterTipLength">Длина острия оружейного бластера.</param>
        /// <param name="difference">Разница между шириной крыльев и длиной
        /// корпуса в мм.</param>
        /// <param name="wingWidth">Ширина крыльев звездолета.</param>
        private void BuildBlasters(double blasterTipLength, 
            double difference, double wingWidth)
        {
            // Объект класса констант для построения бластеров.
            var constants = new BlastersConstants(difference);

            // Построение основания тела бластера.
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, wingWidth-30, 
                false, 0, false);

            // Построение первой основы бластера.
            BuildBlasterCylindersWithShift(constants, 13, 0,  200);
            constants.CurrentPlane.X = 
                constants.CurrentBlasterCircles[0,0].Center.X;
            constants.CurrentPlane.Y = 
                constants.CurrentBlasterCircles[0,0].Center.Y;
            
            // Построение перехода на вторую основу бластера.
            BuildBlasterCylindersWithShift(constants, 11, 0,  5);

            // Построение второй основы бластера.
            BuildBlasterCylindersWithShift(constants, 9, 0,  150);

            // Построение перехода на острие бластера.
            BuildBlasterCylindersWithShift(constants, 15, 0,  5);

            // Построение каждого острия бластера.
            Wrapper.Sketch = 
                Wrapper.BuildSegmentsWithArcs(constants.CurrentPlane, 
                constants.TipsBaseSegments, constants.TipsBaseArcs, true);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, blasterTipLength, 
                true, 0, false);
            
            // Построение антенн на острие бластера.
            BuildAntenna(constants.SideRightTipPlane,
                constants.RightAntennaSegments, constants.RightAntennaArcs);

            // Построение антенн на острие бластера.
            BuildAntenna(constants.SideLeftTipPlane,
                constants.LeftAntennaSegments, constants.LeftAntennaArcs);

            // Построение начальной части батареи бластера.
            constants.CurrentPlane.Z = -600 - difference - wingWidth + 30;
            BuildBlasterCylindersWithShift(constants, 20, -5, -15);

            // Построение средней части батареи бластера.
            BuildBlasterCylindersWithShift(constants, 18.5, 15, -10);

            // Построение конечной части батареи бластера.
            BuildBlasterCylindersWithShift(constants, 21, 0, -10);

            // Вырез в бластере
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 24);
            Wrapper.Sketch = Wrapper.BuildCirclesSketch(constants.FrontBlasterBodyPlane,
                constants.CurrentBlasterCircles);
            Wrapper.CutExtrusion(Wrapper.Sketch, 10, true);
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 15);
            Wrapper.Sketch = Wrapper.BuildCirclesSketch(constants.FrontBlasterBodyPlane,
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, 
                true, 0, false);

            // Построение рисунка в углублении корпуса бластера.
            Wrapper.Sketch = 
                Wrapper.BuildSegmentsWithCircles(constants.FrontBlasterBodyPlane,
                constants.BlasterDrawingSegments, constants.BlasterDrawingCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, 
                true, 0, true);            
        }

        /// <summary>
        /// Построение ускорителей.
        /// </summary>
        /// <param name="turbineLength">Длина турбины ускорителя.</param>
        /// <param name="nozzleLength">Длина сопла ускорителя.</param>
        /// <param name="difference">Разница между длинной корпуса и шириной крыльев в мм.</param>
        private void BuildAccelerators(double turbineLength, 
            double nozzleLength, double difference)
        {
            // Объект класса констант для построения ускорителей.
            var constants = new AcceleratorsConstants(difference);

            // Выдавливание оснований ускорителей на крыльях.            
            Wrapper.Sketch = 
                Wrapper.BuildPolygonSketchByPoint(constants.CurrentPlane,
                constants.AcceleratorsBaseSegments, true);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 280, 
                false, 0, false);

            // Выдавливание воздухозаборника ускорителя.
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.AirIntakeCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 200, 
                false, 0, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 30, 
                true, 0, false);

            // Построение основного выреза в воздухозаборнике.            
            Wrapper.Sketch = 
                Wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane, 
                constants.AirIntakeBaseCuttingSegments, 
                constants.AirIntakeBaseCuttingArcs, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 150, true);

            // Построение среза нижней части воздухозаборника.
            Wrapper.Sketch = 
                Wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeLowerCuttingSegments, 
                constants.AirIntakeLowerCuttingArcs, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 10, true);

            // Построение среднего выреза в воздухозаборнике.            
            Wrapper.Sketch = Wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeMiddleCuttingSegments, 
                constants.AirIntakeMiddleCuttingArcs, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 25, true);

            // Построение малого выреза в воздухозаборнике.            
            Wrapper.Sketch = 
                Wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeSmallCuttingSegments, 
                constants.AirIntakeSmallCuttingArcs, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 5, true);

            // Построение перегородки в воздухозаборнике.
            Wrapper.Sketch = 
                Wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakePartitionSegments, 
                constants.AirIntakePartitionArcs, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 150, 
                false, 0, false);

            // Выдавливание турбины ускорителя.
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.TurbinePlane, 
                constants.TurbineCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, turbineLength, 
                true, 0, false);

            // Выдавливание верхней части сопла.
            constants.CurrentPlane = constants.TurbinePlane;
            constants.CurrentPlane.Z -= turbineLength;
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, nozzleLength/2, 
                true, 10, false);

            // Выдавливание нижней части сопла.
            var angleInRadians = 10 * Math.PI / 180;
            var radius = constants.TurbineCircles[0,0].Radius +
                         Math.Tan(angleInRadians) * nozzleLength / 2;
            ChangeCirclesRadius(constants.TurbineCircles, radius);
            constants.CurrentPlane.Z -= nozzleLength / 2;
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, nozzleLength / 2, 
                true, -5, false);

            // Вырезание внешнего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 33);
            constants.CurrentPlane.Z -= nozzleLength / 2;
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.CutExtrusion(Wrapper.Sketch, nozzleLength / 2, true);

            // Выдавливание средней части сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 15);
            constants.CurrentPlane.Z += nozzleLength / 2;
            Wrapper.Sketch = 
                Wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, nozzleLength / 2, 
                true, 0, false);

            // Вырезание внутреннего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 12.5);
            constants.CurrentPlane.Z -= nozzleLength / 2;
            Wrapper.Sketch = Wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.CutExtrusion(Wrapper.Sketch, nozzleLength / 2, true);

            // Выдавливание рисунка сопла.
            constants.CurrentPlane.Z += nozzleLength / 2;
            Wrapper.Sketch = 
                Wrapper.BuildSegmentsWithCircles(constants.CurrentPlane, 
                constants.NozzleDrawingSegments,
                constants.NozzleDrawingCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, nozzleLength / 2, 
                true, 0, true);            
        }

        /// <summary>
        /// Изменение радиуса у массива кругов.
        /// </summary>
        /// <param name="circles">Массив кругов.</param>
        /// <param name="radius">Новый радиус.</param>
        private static void ChangeCirclesRadius(Circle[,] circles, double radius)
        {
            for (var i = 0; i < circles.GetLength(0); i++)
            {
                for (var j = 0; j < circles.GetLength(1); j++)
                {
                    circles[i,j].Radius = radius;
                }
            }
        }

        /// <summary>
        /// Построение цилиндров со смещением плоскости их основания.
        /// </summary>
        /// <param name="constants">Объект констант для построения бластеров.</param>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="draftValue">Угол выдавливания.</param>
        /// <param name="shift">Смещение.</param>
        private void BuildBlasterCylindersWithShift(BlastersConstants constants,
            double radius, double draftValue, double shift)
        {
            ChangeCirclesRadius(constants.CurrentBlasterCircles, radius);
            Wrapper.Sketch =
                Wrapper.BuildCirclesSketch(constants.CurrentPlane,
                    constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, Math.Abs(shift),
                true, draftValue, false);
            constants.CurrentPlane.Z += shift;
        }

        /// <summary>
        /// Построение антенны.
        /// </summary>
        /// <param name="point3D">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="segmentPoints">Массив точек для построения отрезков.</param>
        /// <param name="arcs">Массив дуг.</param>
        private void BuildAntenna(Point3D point3D, 
            Point2D[,,] segmentPoints, Arc[,] arcs)
        {
            Wrapper.Sketch =
                Wrapper.BuildSegmentsWithArcs(point3D, segmentPoints, arcs,
                    false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 5,
                true, 0, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 15,
                false, 0, false);
        }
    }
}