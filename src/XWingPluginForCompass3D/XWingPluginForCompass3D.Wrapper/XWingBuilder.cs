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
        private readonly KompasWrapper _wrapper = new KompasWrapper();

        /// <summary>
        /// Построение детали по заданным параметрам.
        /// </summary>
        /// <param name="xwing">Объект заданных параметров X-Wing.</param>
        public void BuildDetail(XWing xwing)
        {
            _wrapper.StartKompas();
            _wrapper.CreateDocument();
            _wrapper.SetDetailProperties();
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
            var caseBodySetHeight =
                xwing.Parameters[XWingParameterType.CaseBodySetHeight].Value;
            // Разница между длиной корпуса и шириной крыльев.
            var bodyAndWingsDifference = bodyLength - wingWidth;
            BuildBowBody(bowBodyLength);
            BuildBody(bodyLength, caseBodySetHeight);
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
            var sketch = 
                _wrapper.BuildPolygonByDefaultPlane(_wrapper.DefaultPlaneXoY,
                constants.UpperBaseSegments, false);
            _wrapper.ExtrudeSketch(sketch, 600, 
                false, 5, false);
            _wrapper.ExtrudeSketch(sketch, bowLength,
	            true, -3, false);

            // Выдавливание кабины            
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.UpperFacePlane,
                constants.UpperFaceSegments, false);
            _wrapper.ExtrudeSketch(sketch, 50,
	            true, 0, false);

            // Вырез кабины                        
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.CockpitFrontFacePlane,
                constants.CockpitCutoutSegments, true);
            _wrapper.CutExtrusion(sketch, 602.5, true);

            // Срез кабины
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.CockpitSideFacePlane,
                constants.CockpitSliceSegments, false);
            _wrapper.CutExtrusion(sketch, 100, true);

            // Острие носа
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.TipFrontBasePlane,
                constants.TipBowBodySegments, false);
            _wrapper.ExtrudeSketch(sketch, 100, 
                true, -5, false);
            _wrapper.ExtrudeSketch(sketch, 35, 
                false, -5, false);

            // Фаска верхней части острия носовой части
            _wrapper.CreateChamfer(bowLength, constants.TipUpperChamferDistances,
                constants.TipUpperEdgePoint);

            // Фаска нижней части острия носовой части
            _wrapper.CreateChamfer(bowLength, constants.TipLowerChamferDistances,
                constants.TipLowerEdgePoint);

            // Скругление основной части острия
            _wrapper.CreateFillet(bowLength, 
                constants.TipFrontFilletEdgeCoordinates, 5);

            // Скругление боковой части острия
            _wrapper.CreateFillet(bowLength, 
                constants.TipSideFilletEdgeCoordinates, 10);
        }

        /// <summary>
        /// Построение корпуса.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса.</param>
        /// <param name="caseBodySetHeight">Высота установок крыши корпуса.</param>
        private void BuildBody(double bodyLength, double caseBodySetHeight)
        {            
            // Объект класса констант для построения корпуса детали.            
            var constants = new BodyConstants(bodyLength, caseBodySetHeight);

            // Выдавливание основного корпуса на заданную пользователем величину.
            var sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.UpperBasePlane,
                constants.BaseSegments, false);
            _wrapper.ExtrudeSketch(sketch, bodyLength, 
                true, 0, false);

            // Выдавливание верхней части корпуса.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.UpperFacePlane,
                constants.UpperFaceSegments, false);
            _wrapper.ExtrudeSketch(sketch, 50, 
                true, 0, false);

            // Выдавливание нижней части корпуса.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.LowerFacePlane,
                constants.LowerFaceSegments, false);
            _wrapper.ExtrudeSketch(sketch, 50, 
                true, 0, false);

            // Срез нижней части корпуса.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.LowerSideBackPlane,
                constants.LowerBodySliceSegments, false);
            _wrapper.CutExtrusion(sketch, 100, true);

            // Вырез нижней части корпуса: срезаются углы призмы.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.BackBodyPlane,
                constants.BodyCutoutSegments, true);
            _wrapper.CutExtrusion(sketch, bodyLength, true);

            // Выдавливание верхней задней грани носовой части корпуса:
            // чтобы не было зазора с основным корпусом.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.BowBodyBackPlane,
                constants.BowBodyFaceSegments, false);
            _wrapper.ExtrudeSketch(sketch, 4.5, 
                true, 0, false);

            // Углубление для верхней части корпуса.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.UpperBodyPartFacePlane,
                constants.DeepUpperBodyFaceSegments, false);
            _wrapper.CutExtrusion(sketch, 5, true);

            // Выдавливание окружностей в верхней части корпуса.
            sketch = 
                _wrapper.BuildCirclesSketch(constants.DeepBodyPartFacePlane,
                constants.UpperBodyExtrudingCircles);
            _wrapper.ExtrudeSketch(sketch, caseBodySetHeight, 
                true, 0, false);

            // Выдавливание прямоугольников с вырезом в верхней части корпуса.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.DeepBodyPartFacePlane,
                constants.UpperBodyExtrudingRectanglesSegments, false);
            _wrapper.ExtrudeSketch(sketch, caseBodySetHeight, 
                true, 0, false);

            // Построение головы робота в верхней части корпуса.
            _wrapper.BuildHemisphere(constants.BaseDroidHeadPlane, 
                constants.DroidHeadArc);

            // Углубление задней части корпуса.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.BackBodyPlane,
                constants.BackBodyDeepSegments, false);
            _wrapper.CutExtrusion(sketch, 10, true);

            // Первый рисунок задней части корпуса.
            sketch = 
                _wrapper.BuildCirclesSketch(constants.BackDeepPlane,
                constants.BackDrawingCircles);
            _wrapper.ExtrudeSketch(sketch, 10, 
                true, 0, false);

            // Второй рисунок задней части корпуса.
            sketch = _wrapper.BuildSetSegments(constants.BackDeepPlane, 
                constants.BackDrawingSegments, false);
            _wrapper.ExtrudeSketch(sketch, 10, 
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
            var sketch = 
                _wrapper.BuildPolygonSketchByPoint(wingsConstants.BackBodyPlane,
                wingsConstants.BaseSegments, true);
            _wrapper.ExtrudeSketch(sketch, wingsWidth,
                false, 0, false);

            // Вырезание формы крыла.
            sketch = 
                _wrapper.BuildPolygonSketchByPoint(wingsConstants.CuttingPlane,
                wingsConstants.WingsCutSegments, true);
            _wrapper.CutExtrusion(sketch, 350, true);
            _wrapper.CutExtrusion(sketch, 100, false);
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
            var sketch = 
                _wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            _wrapper.ExtrudeSketch(sketch, wingWidth-30, 
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
            sketch = 
                _wrapper.BuildSegmentsWithArcs(constants.CurrentPlane, 
                constants.TipsBaseSegments, constants.TipsBaseArcs, true);
            _wrapper.ExtrudeSketch(sketch, blasterTipLength, 
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
            sketch = _wrapper.BuildCirclesSketch(constants.FrontBlasterBodyPlane,
                constants.CurrentBlasterCircles);
            _wrapper.CutExtrusion(sketch, 10, true);
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 15);
            sketch = _wrapper.BuildCirclesSketch(constants.FrontBlasterBodyPlane,
                constants.CurrentBlasterCircles);
            _wrapper.ExtrudeSketch(sketch, 10, 
                true, 0, false);

            // Построение рисунка в углублении корпуса бластера.
            sketch = 
                _wrapper.BuildSegmentsWithCircles(constants.FrontBlasterBodyPlane,
                constants.BlasterDrawingSegments, constants.BlasterDrawingCircles);
            _wrapper.ExtrudeSketch(sketch, 10, 
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
            var sketch = 
                _wrapper.BuildPolygonSketchByPoint(constants.CurrentPlane,
                constants.AcceleratorsBaseSegments, true);
            _wrapper.ExtrudeSketch(sketch, 280, 
                false, 0, false);

            // Выдавливание воздухозаборника ускорителя.
            sketch = 
                _wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.AirIntakeCircles);
            _wrapper.ExtrudeSketch(sketch, 200, 
                false, 0, false);
            _wrapper.ExtrudeSketch(sketch, 30, 
                true, 0, false);

            // Построение основного выреза в воздухозаборнике.            
            sketch = 
                _wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane, 
                constants.AirIntakeBaseCuttingSegments, 
                constants.AirIntakeBaseCuttingArcs, false);
            _wrapper.CutExtrusion(sketch, 150, true);

            // Построение среза нижней части воздухозаборника.
            sketch = 
                _wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeLowerCuttingSegments, 
                constants.AirIntakeLowerCuttingArcs, false);
            _wrapper.CutExtrusion(sketch, 10, true);

            // Построение среднего выреза в воздухозаборнике.            
            sketch = _wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeMiddleCuttingSegments, 
                constants.AirIntakeMiddleCuttingArcs, false);
            _wrapper.CutExtrusion(sketch, 25, true);

            // Построение малого выреза в воздухозаборнике.            
            sketch = 
                _wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeSmallCuttingSegments, 
                constants.AirIntakeSmallCuttingArcs, false);
            _wrapper.CutExtrusion(sketch, 5, true);

            // Построение перегородки в воздухозаборнике.
            sketch = 
                _wrapper.BuildSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakePartitionSegments, 
                constants.AirIntakePartitionArcs, false);
            _wrapper.ExtrudeSketch(sketch, 150, 
                false, 0, false);

            // Выдавливание турбины ускорителя.
            sketch = 
                _wrapper.BuildCirclesSketch(constants.TurbinePlane, 
                constants.TurbineCircles);
            _wrapper.ExtrudeSketch(sketch, turbineLength, 
                true, 0, false);

            // Выдавливание верхней части сопла.
            constants.CurrentPlane = constants.TurbinePlane;
            constants.CurrentPlane.Z -= turbineLength;
            sketch = 
                _wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            _wrapper.ExtrudeSketch(sketch, nozzleLength/2, 
                true, 10, false);

            // Выдавливание нижней части сопла.
            var angleInRadians = 10 * Math.PI / 180;
            var radius = constants.TurbineCircles[0,0].Radius +
                         Math.Tan(angleInRadians) * nozzleLength / 2;
            ChangeCirclesRadius(constants.TurbineCircles, radius);
            constants.CurrentPlane.Z -= nozzleLength / 2;
            sketch = 
                _wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            _wrapper.ExtrudeSketch(sketch, nozzleLength / 2, 
                true, -5, false);

            // Вырезание внешнего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 33);
            constants.CurrentPlane.Z -= nozzleLength / 2;
            sketch = 
                _wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            _wrapper.CutExtrusion(sketch, nozzleLength / 2, true);

            // Выдавливание средней части сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 15);
            constants.CurrentPlane.Z += nozzleLength / 2;
            sketch = 
                _wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            _wrapper.ExtrudeSketch(sketch, nozzleLength / 2, 
                true, 0, false);

            // Вырезание внутреннего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 12.5);
            constants.CurrentPlane.Z -= nozzleLength / 2;
            sketch = _wrapper.BuildCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            _wrapper.CutExtrusion(sketch, nozzleLength / 2, true);

            // Выдавливание рисунка сопла.
            constants.CurrentPlane.Z += nozzleLength / 2;
            sketch = 
                _wrapper.BuildSegmentsWithCircles(constants.CurrentPlane, 
                constants.NozzleDrawingSegments,
                constants.NozzleDrawingCircles);
            _wrapper.ExtrudeSketch(sketch, nozzleLength / 2, 
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
            var sketch =
                _wrapper.BuildCirclesSketch(constants.CurrentPlane,
                    constants.CurrentBlasterCircles);
            _wrapper.ExtrudeSketch(sketch, Math.Abs(shift),
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
            var sketch =
                _wrapper.BuildSegmentsWithArcs(point3D, segmentPoints, arcs,
                    false);
            _wrapper.ExtrudeSketch(sketch, 5,
                true, 0, false);
            _wrapper.ExtrudeSketch(sketch, 15,
                false, 0, false);
        }
    }
}