using System;
using XWingPluginForCompass3D.Model;

namespace XWingPluginForCompass3D.Wrapper
{
    /// <summary>
    /// Класс построения 3D-модели звёздного истребителя X-Wing.
    /// </summary>
    public class XWingBuilder
    {
        /// <summary>
        /// Связь с Компас-3D.
        /// </summary>
        private KompasWrapper _wrapper = new KompasWrapper();     

        /// <summary>
        /// Устанавливает и возвращает связь с Компас-3D.
        /// </summary>
        public KompasWrapper Wrapper
        {
            set
            {
                _wrapper = value;
            }
            get
            {
                return _wrapper;
            }
        }

        /// <summary>
        /// Построение детали по заданным параметрам.
        /// </summary>
        /// <param name="xwing">Объект заданных параметров X-Wing.</param>
        public void BuildDetail(XWing xwing)
        {
            Wrapper.StartKompas();
            Wrapper.CreateDocument();
            Wrapper.SetDetailProperties();
            // Разница между длиной корпуса и шириной крыльев.
            double bodyAndWingsDifference = xwing.BodyLength - xwing.WingWidth;
            BuildBowBody(xwing.BowLength);
            BuildBody(xwing.BodyLength);
            BuildWings(xwing.WingWidth, xwing.BodyLength);
            BuildBlasters(xwing.WeaponBlasterTipLength,
                bodyAndWingsDifference, xwing.WingWidth);
            BuildAccelerators(xwing.AcceleratorTurbineLength,
                xwing.AcceleratorNozzleLength, bodyAndWingsDifference);
        }

        /// <summary>
        /// Построение носовой части корпуса.
        /// </summary>
        /// <param name="bowLength">Длина носа.</param>
        private void BuildBowBody(double bowLength)
        {
            // Объект класса констант для построения носовой части детали.            
            BowBodyConstants constants = new BowBodyConstants(bowLength);

            // Выдавливание основы носовой части корпуса
            Wrapper.Sketch = Wrapper.CreatePolygonByDefaultPlane(Wrapper.PlaneXOY,
                constants.UpperBaseVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 600, false, 5, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, bowLength, true, -3, false);

            // Выдавливание кабины            
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.UpperFacePlane,
                constants.UpperFaceVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 50, true, 0, false);

            // Вырез кабины                        
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.CockpitFrontFacePlane,
                constants.CockpitCutoutVertexes, true);
            Wrapper.CutExtrusion(Wrapper.Sketch, 602.5, true);

            // Срез кабины
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.CockpitSideFacePlane,
                constants.CockpitSliceVertexes, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 100, true);

            // Острие носа
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.TipFrontBasePlane,
                constants.TipBowBodyVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 100, true, -5, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 35, false, -5, false);

            // Фаска верхней части острия носовой части
            Wrapper.CreateChamfer(bowLength, constants.TipUpperEdgeChamferDistances,
                constants.TipUpperEdge);

            // Фаска нижней части острия носовой части
            Wrapper.CreateChamfer(bowLength, constants.TipLowerEdgeChamferDistances,
                constants.TipLowerEdge);

            // Скругление основной части острия
            Wrapper.CreateFillet(bowLength, constants.TipFrontFilletEdgeCoordinates, 5);

            // Скругление боковой части острия
            Wrapper.CreateFillet(bowLength, constants.TipSideFilletEdgeCoordinates, 10);
        }

        /// <summary>
        /// Построение корпуса.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса.</param>
        private void BuildBody(double bodyLength)
        {            
            // Объект класса констант для построения корпуса детали.            
            BodyConstants constants = new BodyConstants(bodyLength);

            // Выдавливание основного корпуса на заданную пользователем величину.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.UpperBasePlane,
                constants.BaseVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, bodyLength, true, 0, false);

            // Выдавливание верхней части корпуса.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.UpperFacePlane,
                constants.UpperFaceVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 50, true, 0, false);

            // Выдавливание нижней части корпуса.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.LowerFacePlane,
                constants.LowerFaceVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 50, true, 0, false);

            // Срез нижней части корпуса.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.LowerSideBackPlane,
                constants.LowerBodySliceVertexes, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 100, true);

            // Вырез нижней части корпуса: срезаются углы призмы.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.BackBodyPlane,
                constants.BodyCutoutVertexes, true);
            Wrapper.CutExtrusion(Wrapper.Sketch, bodyLength, true);

            // Выдавливание верхней задней грани носовой части корпуса:
            // чтобы не было зазора с основным корпусом.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.BowBodyBackPlane,
                constants.BowBodyFaceVertexes, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 4.5, true, 0, false);

            // Углубление для верхней части корпуса.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.UpperBodyPartFacePlane,
                constants.DeepingUpperBodyFaceVertexes, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 5, true);

            // Выдавливание окружностей в верхней части корпуса.
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.DeepingBodyPartFacePlane,
                constants.UpperBodyExtrudingCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, true, 0, false);

            // Выдавливание прямоугольников с вырезом в верхней части корпуса.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.DeepingBodyPartFacePlane,
                constants.UpperBodyExtrudingRectangles, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, true, 0, false);

            // Построение головы дроида в верхней части корпуса.
            Wrapper.BuildHemisphere(constants.BaseDroidHeadPlane, constants.DroidHeadArc);

            // Углубление задней части корпуса.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.BackBodyPlane,
                constants.BackBodyDeepingVertexes, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 10, true);

            // Первый рисунок задней части корпуса.
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.BackDeepingPlane,
                constants.BackDrawingCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, true, 0, false);

            // Второй рисунок задней части корпуса.
            Wrapper.Sketch = Wrapper.BuildSetSegments(constants.BackDeepingPlane, 
                constants.BackDrawingSegments);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, true, 0, true);
        }

        /// <summary>
        /// Построение крыльев.
        /// </summary>
        /// <param name="wingsWidth">Ширина крыльев</param>
        private void BuildWings(double wingsWidth, double bodyLength)
        {
            // Объект класса констант для построения крыльев.
            WingsConstants wingsConstants = new WingsConstants(wingsWidth, bodyLength);

            // Выдавливание крыльев, начиная с конца корпуса.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(wingsConstants.BackBodyPlane,
                wingsConstants.BaseVertexes, true);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, wingsWidth, false, 0, false);

            // Вырезание формы крыла.
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(wingsConstants.CuttingPlane,
                wingsConstants.WingsCutVertexes, true);
            Wrapper.CutExtrusion(Wrapper.Sketch, 350, true);
            Wrapper.CutExtrusion(Wrapper.Sketch, 100, false);
        }

        /// <summary>
        /// Построение бластерного оружия.
        /// </summary>
        /// <param name="blasterTipLength">Длина острия оружейного бластера.</param>
        /// <param name="difference">Разница между шириной крыльев и длиной корпуса в мм.</param>
        /// <param name="wingWidth">Ширина крыльев звездолёта.</param>
        private void BuildBlasters(double blasterTipLength, double difference, double wingWidth)
        {
            // Объект класса констант для построения бластерных оружий.
            BlastersConstants constants = new BlastersConstants(difference);

            // Построение основания тела бластера.
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, wingWidth-30, false, 0, false);

            // Построение первой основы бластера.
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 13);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 200, true, 0, false);
            constants.CurrentPlane.X = constants.CurrentBlasterCircles[0].Center.X;
            constants.CurrentPlane.Y = constants.CurrentBlasterCircles[0].Center.Y;
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + 200;

            // Построение перехода на вторую основу бластера.
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 11);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 5, true, 0, false);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + 5;

            // Построение второй основы бластера.
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 9);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 150, true, 0, false);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + 150;

            // Построение перехода на острие бластера.
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 15);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 5, true, 0, false);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + 5;

            // Построение каждого острия бластера.
            Wrapper.Sketch = Wrapper.CreateSegmentsWithArcs(constants.CurrentPlane, 
                constants.TipsBaseSegments, constants.TipsBaseArcs, true);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, blasterTipLength, true, 0, false);

            // Построение антенн на острие бластера.
            Wrapper.Sketch = Wrapper.CreateSegmentsWithArcs(constants.SideRightTipPlane,
                constants.RightAntennaSegments, constants.RightAntennaArcs, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 5, true, 0, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 15, false, 0, false);

            // Построение антенн на острие бластера.
            Wrapper.Sketch = Wrapper.CreateSegmentsWithArcs(constants.SideLeftTipPlane,
                constants.LeftAntennaSegments, constants.LeftAntennaArcs, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 5, true, 0, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 15, false, 0, false);

            // Построение начальной части батареи бластера.
            constants.CurrentPlane.Z = -600 - difference - wingWidth + 30;
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 20);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane,
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 15, true, -5, false);

            // Построение средней части батареи бластера.
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - 15;
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 18.5);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane,
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, true, 15, false);

            // Построение конечной части батареи бластера.
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - 10;
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 21);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane,
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, true, 0, false);

            // Вырез в бластере
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 24);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.FrontBlasterBodyPlane,
                constants.CurrentBlasterCircles);
            Wrapper.CutExtrusion(Wrapper.Sketch, 10, true);
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 15);
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.FrontBlasterBodyPlane,
                constants.CurrentBlasterCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, true, 0, false);

            // Построение рисунка в углублении корпуса бластера.
            Wrapper.Sketch = Wrapper.CreateSegmentsWithCircles(constants.FrontBlasterBodyPlane,
                constants.BlasterDrawingSegments, constants.BlasterDrawingCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 10, true, 0, true);            
        }

        /// <summary>
        /// Построение ускорителей.
        /// </summary>
        /// <param name="turbineLength">Длина турбины ускорителя.</param>
        /// <param name="nozzleLength">Длина сопла ускорителя.</param>
        /// <param name="difference">Разница между длинной корпуса и шириной крыльев в мм.</param>
        private void BuildAccelerators(double turbineLength, double nozzleLength, double difference)
        {
            // Объект класса констант для построения ускорителей.
            AcceleratorsConstants constants = new AcceleratorsConstants(difference);

            // Выдавливанние оснований ускорителей на крыльях.            
            Wrapper.Sketch = Wrapper.CreatePolygonSketchByPoint(constants.CurrentPlane,
                constants.AcceleratorsBaseVertexes, true);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 280, false, 0, false);

            // Выдавливание воздухозаборника ускорителя.
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.AirIntakeCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 200, false, 0, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 30, true, 0, false);

            // Построение основного выреза в воздухозаборнике.            
            Wrapper.Sketch = Wrapper.CreateSegmentsWithArcs(constants.AirIntakePlane, 
                constants.AirIntakeBaseCuttingSegments, constants.AirIntakeBaseCuttingArcs, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 150, true);

            // Построение среза нижней части воздухозаборника.
            Wrapper.Sketch = Wrapper.CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeLowerCuttingSegments, constants.AirIntakeLowerCuttingArcs, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 10, true);

            // Построение среднего выреза в воздухозаборнике.            
            Wrapper.Sketch = Wrapper.CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeMiddleCuttingSegments, constants.AirIntakeMiddleCuttingArcs, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 25, true);

            // Построение малого выреза в воздухозаборнике.            
            Wrapper.Sketch = Wrapper.CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeSmallCuttingSegments, constants.AirIntakeSmallCuttingArcs, false);
            Wrapper.CutExtrusion(Wrapper.Sketch, 5, true);

            // Построение перегородки в воздухозаборнике.
            Wrapper.Sketch = Wrapper.CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakePartitionSegments, constants.AirIntakePartitionArcs, false);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, 150, false, 0, false);

            // Выдавливание турбины ускорителя.
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.TurbinePlane, 
                constants.TurbineCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, turbineLength, true, 0, false);

            // Выдавливание верхней части сопла.
            constants.CurrentPlane = constants.TurbinePlane;
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - turbineLength;
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, nozzleLength/2, true, 10, false);

            // Выдавливание нижней части сопла.
            double angle = 10*Math.PI/180;
            double radius = constants.TurbineCircles[0].Radius +
                Math.Tan(angle) * nozzleLength / 2;
            ChangeCirclesRadius(constants.TurbineCircles, radius);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, nozzleLength / 2, true, -5, false);

            // Вырезание внешнего отверстия сопла.
           ChangeCirclesRadius(constants.TurbineCircles, 33);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.CutExtrusion(Wrapper.Sketch, nozzleLength / 2, true);

            // Выдавливание средней части сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 15);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + nozzleLength / 2;
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, nozzleLength / 2, true, 0, false);

            // Вырезание внутреннего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 12.5);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            Wrapper.Sketch = Wrapper.CreateCirclesSketch(constants.CurrentPlane, 
                constants.TurbineCircles);
            Wrapper.CutExtrusion(Wrapper.Sketch, nozzleLength / 2, true);

            // Выдавливание рисунка сопла.
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + nozzleLength / 2;
            Wrapper.Sketch = Wrapper.CreateSegmentsWithCircles(constants.CurrentPlane, 
                constants.NozzleDrawingSegments,
                constants.NozzleDrawingCircles);
            Wrapper.ExtrudeSketch(Wrapper.Sketch, nozzleLength / 2, true, 0, true);            
        }


        /// <summary>
        /// Изменение радиуса у массива кругов.
        /// </summary>
        /// <param name="circles">Массив кругов.</param>
        /// <param name="radius">Новый радиус.</param>
        public void ChangeCirclesRadius(Circle[] circles, double radius)
        {
            for (int i = 0; i < circles.GetLength(0); i++)
            {
                circles[i].Radius = radius;
            }
        }
    }
}