using Kompas6API5;
using Kompas6Constants3D;
using System;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс построения 3D-модели звёздного истребителя X-Wing
    /// </summary>
    public class XWingBuilder
    {
        /// <summary>
        /// Объект Компас API.
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Деталь X-Wing.
        /// </summary>
        private ksPart _part;

        /// <summary>
        /// Параметризированный конструктор.
        /// </summary>
        /// <param name="kompas">Объект Компас API.</param>
        public XWingBuilder(KompasObject kompas)
        {
            _kompas = kompas;
            ksDocument3D document = (ksDocument3D)kompas.Document3D();
            document.Create();
        }

        /// <summary>
        /// Построение детали по заданным параметрам.
        /// </summary>
        /// <param name="xwing">Объект заданных параметров X-Wing.</param>
        public void BuildDetail(XWingParameters xwing)
        {
            double bodyLength = xwing.BodyLength;
            double wingWidth = xwing.WingWidth;
            double bowLength = xwing.BowLength;
            double weaponBlasterTipLength = xwing.WeaponBlasterTipLength;
            double turbineLength = xwing.AcceleratorTurbineLength;
            double nozzleLength = xwing.AcceleratorNozzleLength;
            double bodyAndWingsDifference = bodyLength - wingWidth;
            ksDocument3D document = (ksDocument3D)_kompas.ActiveDocument3D();
            _part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            _part.name = "X-Wing";
            _part.SetAdvancedColor(14211288, 0.5, 0.6, 0.8, 0.8, 1, 0.5);
            _part.Update();
            BuildBowBody(bowLength);
            BuildBody(bodyLength);
            BuildWings(wingWidth, bodyLength);
            BuildBlasters(weaponBlasterTipLength, bodyAndWingsDifference, wingWidth);
            BuildAccelerators(turbineLength, nozzleLength, bodyAndWingsDifference);
        }

        /// <summary>
        /// Построение носовой части корпуса.
        /// </summary>
        /// <param name="bowLength">Длина носа.</param>
        private void BuildBowBody(double bowLength)
        {
            // Объект класса констант для построения носовой части детали.            
            BowBodyConstants _bowBodyXWingConstants = new BowBodyConstants(bowLength);

            // Выдавливание основы носовой части корпуса
            ksEntity sketch = CreatePolygonSketchByDefaultPlane(Obj3dType.o3d_planeXOY,
                _bowBodyXWingConstants.UpperBaseVertexes);
            ExtrudeSketch(_part, sketch, 600, false, 5, false);
            ExtrudeSketch(_part, sketch, bowLength, true, -3, false);

            // Выдавливание кабины            
            sketch = CreatePolygonSketchByPoint(_bowBodyXWingConstants.UpperFacePlaneCoordinate,
                _bowBodyXWingConstants.UpperFaceVertexes);
            ExtrudeSketch(_part, sketch, 50, true, 0, false);

            // Вырез кабины                        
            sketch = CreatePolygonSketchByPoint(_bowBodyXWingConstants.CockpitFrontFacePlaneCoordinate,
                _bowBodyXWingConstants.FirstCockpitCutoutVertexes);
            CutExtrusion(_part, sketch, 602.5, 0, true);
            sketch = CreatePolygonSketchByPoint(_bowBodyXWingConstants.CockpitFrontFacePlaneCoordinate,
                _bowBodyXWingConstants.SecondCockpitCutoutVertexes);
            CutExtrusion(_part, sketch, 602.5, 0, true);

            // Срез кабины
            ksEntity ssketch = CreatePolygonSketchByPoint(_bowBodyXWingConstants.CockpitSideFacePlaneCoordinate,
                _bowBodyXWingConstants.CockpitSliceVertexes);
            CutExtrusion(_part, ssketch, 100, 0, true);

            // Острие носа
            sketch = CreatePolygonSketchByPoint(_bowBodyXWingConstants.TipBaseFrontPlaneCoordinate,
                _bowBodyXWingConstants.TipBowBodyVertexes);
            ExtrudeSketch(_part, sketch, 100, true, -5, false);
            ExtrudeSketch(_part, sketch, 35, false, -5, false); ;

            // Фаска верхней части острия носовой части
            CreateChamfer(bowLength, _bowBodyXWingConstants.TipUpperEdgeChamferDistances,
                _bowBodyXWingConstants.TipUpperEdgeCoordinate);

            // Фаска нижней части острия носовой части
            CreateChamfer(bowLength, _bowBodyXWingConstants.TipLowerEdgeChamferDistances,
                _bowBodyXWingConstants.TipLowerEdgeCoordinate);

            // Скругление основной части острия
            CreateFillet(bowLength, _bowBodyXWingConstants.TipFrontFilletEdgeCoordinates, 5);

            // Скругление боковой части острия
            CreateFillet(bowLength, _bowBodyXWingConstants.TipSideFilletEdgeCoordinates, 10);
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
            ksEntity sketch = CreatePolygonSketchByPoint(constants.UpperBasePlaneCoordinate,
                constants.BaseVertexes);
            ExtrudeSketch(_part, sketch, bodyLength, true, 0, false);

            // Выдавливание верхней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.UpperFacePlaneCoordinate,
                constants.UpperFaceVertexes);
            ExtrudeSketch(_part, sketch, 50, true, 0, false);

            // Выдавливание нижней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.LowerFacePlaneCoordinate,
                constants.LowerFaceVertexes);
            ExtrudeSketch(_part, sketch, 50, true, 0, false);

            // Вырез верхней части корпуса: срезаются углы призмы.
            sketch = CreatePolygonSketchByPoint(constants.UpperBodyFrontPlaneCoordinate,
                constants.FirstUpperBodyCutoutVertexes);
            CutExtrusion(_part, sketch, bodyLength, 0, true);
            sketch = CreatePolygonSketchByPoint(constants.UpperBodyFrontPlaneCoordinate,
                constants.SecondUpperBodyCutoutVertexes);
            CutExtrusion(_part, sketch, bodyLength, 0, true);

            // Срез нижней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.LowerSideBackPlaneCoordinate,
                constants.LowerBodySliceVertexes);
            CutExtrusion(_part, sketch, 100, 0, true);

            // Вырез нижней части корпуса: срезаются углы призмы.
            sketch = CreatePolygonSketchByPoint(constants.LowerBodyBackPlaneCoordinate,
                constants.FirstLowerBodyCutoutVertexes);
            CutExtrusion(_part, sketch, bodyLength, 0, true);
            sketch = CreatePolygonSketchByPoint(constants.LowerBodyBackPlaneCoordinate,
                constants.SecondLowerBodyCutoutVertexes);
            CutExtrusion(_part, sketch, bodyLength, 0, true);

            // Выдавливание верхней задней грани носовой части корпуса:
            // чтобы не было зазора с основным корпусом.
            sketch = CreatePolygonSketchByPoint(constants.BowBodyBackPlaneCoordinate,
                constants.BowBodyFaceVertexes);
            ExtrudeSketch(_part, sketch, 4.5, true, 0, false);

            // Углубление для верхней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.UpperBodyPartFacePlaneCoordinate,
                constants.DeepingUpperBodyFaceVertexes);
            CutExtrusion(_part, sketch, 5, 0, true);

            // Выдавливание окружностей в верхней части корпуса.
            sketch = CreateCirclesSketch(constants.DeepingBodyPartFacePlaneCoordinate,
                constants.UpperBodyPartExtrudingCirclesParameters);
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Выдавливание прямоугольников с вырезом в верхней части корпуса.
            sketch = CreateSeveralPolygon(constants.DeepingBodyPartFacePlaneCoordinate,
                constants.UpperBodyPartExtrudingRectanglesCoordinates, false);
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Построение головы дроида в верхней части корпуса.
            BuildDroidHead(constants.BaseDroidHeadPlaneCoordinate);

            // Углубление задней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.BackBodyPlaneCoordinate,
                constants.BackBodyDeepingVertexes);
            CutExtrusion(_part, sketch, 10, 0, true);

            // Первый рисунок задней части корпуса.
            sketch = CreateCirclesSketch(constants.BackDeepingPlaneCoordinate,
                constants.BackBodyPartExtrudingCirclesParameters);
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Второй рисунок задней части корпуса.
            sketch = BuildBackDrawing(constants.BackDeepingPlaneCoordinate, 
                constants.BackDrawingSegments);
            ExtrudeSketch(_part, sketch, 10, true, 0, true);
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
            ksEntity sketch = CreateSeveralPolygon(wingsConstants.BackBodyPlane,
                wingsConstants.BaseVertexes, true);
            ExtrudeSketch(_part, sketch, wingsWidth, false, 0, false);

            // Вырезание формы крыла.
            sketch = CreateSeveralPolygon(wingsConstants.CuttingPlane,
                wingsConstants.WingsCutVertexes, true);
            CutExtrusion(_part, sketch, 350, 80, true);
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
            BlastersConstants blastersConstants = new BlastersConstants(difference);

            // Построение основания тела бластера.
            ksEntity sketch = CreateCirclesSketch(blastersConstants.CurrentPlane, blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, wingWidth-30, false, 0, false);

            // Построение первой основы бластера.
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 13);
            sketch = CreateCirclesSketch(blastersConstants.CurrentPlane, 
                blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, 200, true, 0, false);
            blastersConstants.CurrentPlane.X = blastersConstants.CurrentBlasterCircles[0].Center.X;
            blastersConstants.CurrentPlane.Y = blastersConstants.CurrentBlasterCircles[0].Center.Y;
            blastersConstants.CurrentPlane.Z = blastersConstants.CurrentPlane.Z + 200;

            // Построение перехода на вторую основу бластера.
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 11);
            sketch = CreateCirclesSketch(blastersConstants.CurrentPlane, 
                blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, 5, true, 0, false);
            blastersConstants.CurrentPlane.Z = blastersConstants.CurrentPlane.Z + 5;

            // Построение второй основы бластера.
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 9);
            sketch = CreateCirclesSketch(blastersConstants.CurrentPlane, 
                blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, 150, true, 0, false);
            blastersConstants.CurrentPlane.Z = blastersConstants.CurrentPlane.Z + 150;

            // Построение перехода на острие бластера.
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 15);
            sketch = CreateCirclesSketch(blastersConstants.CurrentPlane, 
                blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, 5, true, 0, false);
            blastersConstants.CurrentPlane.Z = blastersConstants.CurrentPlane.Z + 5;

            // Построение каждого острия бластера.
            sketch = CreateSegmentsWithArcs(blastersConstants.CurrentPlane, 
                blastersConstants.TipsBaseSegments, blastersConstants.TipsBaseArcs, true);
            ExtrudeSketch(_part, sketch, blasterTipLength, true, 0, false);

            // Построение антенн на острие бластера.
            sketch = CreateSegmentsWithArcs(blastersConstants.SideRightTipPlane,
                blastersConstants.RightAntennaSegments, blastersConstants.RightAntennaArcs, false);
            ExtrudeSketch(_part, sketch, 5, true, 0, false);
            ExtrudeSketch(_part, sketch, 15, false, 0, false);

            // Построение антенн на острие бластера.
            sketch = CreateSegmentsWithArcs(blastersConstants.SideLeftTipPlane,
                blastersConstants.LeftAntennaSegments, blastersConstants.LeftAntennaArcs, false);
            ExtrudeSketch(_part, sketch, 5, true, 0, false);
            ExtrudeSketch(_part, sketch, 15, false, 0, false);

            // Построение начальной части батареи бластера.
            blastersConstants.CurrentPlane.Z = -600 - difference - wingWidth + 30;
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 20);
            sketch = CreateCirclesSketch(blastersConstants.CurrentPlane,
                blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, 15, true, -5, false);

            // Построение средней части батареи бластера.
            blastersConstants.CurrentPlane.Z = blastersConstants.CurrentPlane.Z - 15;
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 18.5);
            sketch = CreateCirclesSketch(blastersConstants.CurrentPlane,
                blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, 10, true, 15, false);

            // Построение конечной части батареи бластера.
            blastersConstants.CurrentPlane.Z = blastersConstants.CurrentPlane.Z - 10;
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 21);
            sketch = CreateCirclesSketch(blastersConstants.CurrentPlane,
                blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Вырез в бластере
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 24);
            sketch = CreateCirclesSketch(blastersConstants.FrontBlasterBodyPlane,
                blastersConstants.CurrentBlasterCircles);
            CutExtrusion(_part, sketch, 10, 0, true);
            ChangeCirclesRadius(blastersConstants.CurrentBlasterCircles, 15);
            sketch = CreateCirclesSketch(blastersConstants.FrontBlasterBodyPlane,
                blastersConstants.CurrentBlasterCircles);
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Построение рисунка в углублении корпуса бластера.
            sketch = CreateSegmentsWithCircles(blastersConstants.FrontBlasterBodyPlane,
                blastersConstants.BlasterDrawingSegments, blastersConstants.BlasterDrawingCircles);
            ExtrudeSketch(_part, sketch, 10, true, 0, true);            
        }

        /// <summary>
        /// Построение ускорителей.
        /// </summary>
        /// <param name="turbineLength">Длина турбины ускорителя.</param>
        /// <param name="nozzleLength">Длина сопла ускорителя.</param>
        /// <param name="difference">Разница между длинной корпуса и шириной крыльев в мм.</param>
        private void BuildAccelerators(double turbineLength, double nozzleLength, double difference)
        {
            AcceleratorsConstants constants = new AcceleratorsConstants(difference);

            // Выдавливанние оснований ускорителей на крыльях.
            ksEntity sketch = CreateSeveralPolygon(constants.CurrentPlane,
                constants.AcceleratorsBaseVertexes, true);
            ExtrudeSketch(_part, sketch, 280, false, 0, false);

            // Выдавливание воздухозаборника ускорителя.
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.AirIntakeCircles);
            ExtrudeSketch(_part, sketch, 200, false, 0, false);
            ExtrudeSketch(_part, sketch, 30, true, 0, false);

            // Построение основного выреза в воздухозаборнике.            
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane, 
                constants.AirIntakeBaseCuttingSegments, constants.AirIntakeBaseCuttingArcs, false);
            CutExtrusion(_part, sketch, 150, 0, true);

            // Построение среза нижней части воздухозаборника.
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeLowerCuttingSegments, constants.AirIntakeLowerCuttingArcs, false);
            CutExtrusion(_part, sketch, 10, 0, true);

            // Построение среднего выреза в воздухозаборнике.            
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeMiddleCuttingSegments, constants.AirIntakeMiddleCuttingArcs, false);
            CutExtrusion(_part, sketch, 25, 0, true);

            // Построение малого выреза в воздухозаборнике.            
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeSmallCuttingSegments, constants.AirIntakeSmallCuttingArcs, false);
            CutExtrusion(_part, sketch, 5, 0, true);

            // Построение перегородки в воздухозаборнике.
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakePartitionSegments, constants.AirIntakePartitionArcs, false);
            ExtrudeSketch(_part, sketch, 150, false, 0, false);

            // Выдавливание турбины ускорителя.
            sketch = CreateCirclesSketch(constants.TurbinePlane, constants.TurbineCircles);
            ExtrudeSketch(_part, sketch, turbineLength, true, 0, false);

            // Выдавливание верхней части сопла.
            constants.CurrentPlane = constants.TurbinePlane;
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - turbineLength;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            ExtrudeSketch(_part, sketch, nozzleLength/2, true, 10, false);

            // Выдавливание нижней части сопла.
            double angle = 10*Math.PI/180;
            double radius = constants.TurbineCircles[0].Radius +
                Math.Tan(angle) * nozzleLength / 2;
            ChangeCirclesRadius(constants.TurbineCircles, radius);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            ExtrudeSketch(_part, sketch, nozzleLength / 2, true, -5, false);

            // Вырезание внешнего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 33);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            CutExtrusion(_part, sketch, nozzleLength / 2, 0, true);

            // Выдавливание средней части сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 15);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + nozzleLength / 2;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            ExtrudeSketch(_part, sketch, nozzleLength / 2, true, 0, false);

            // Вырезание внутреннего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 12.5);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            CutExtrusion(_part, sketch, nozzleLength / 2, 0, true);

            // Выдавливание рисунка сопла.
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + nozzleLength / 2;
            sketch = CreateSegmentsWithCircles(constants.CurrentPlane, constants.NozzleDrawingSegments,
                constants.NozzleDrawingCircles);
            ExtrudeSketch(_part, sketch, nozzleLength / 2, true, 0, true);            
        }

        /// <summary>
        /// Построение головы робота.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Массив координат центра плоскости.</param>
        private void BuildDroidHead(Point3D centerPlaneCoordinates)
        {
            // Построение эскиза полуокружности с осью вращения.
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(
                centerPlaneCoordinates.X,
                centerPlaneCoordinates.Y,
                centerPlaneCoordinates.Z);
            ksEntity plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksLineSeg(-0.246315694343, 657.633225627145,
                -0.246315694343, 607.641314086781, 3);
            sketchEdit.ksArcByPoint(0.203345439322, 632.637269856963, 25, -0.246315694343,
                657.633225627145, -0.246315694343, 607.641314086781, 1, 1);
            definition.EndEdit();

            // Выдавливание вращением.
            ksEntity bossRotated = _part.NewEntity((short)Obj3dType.o3d_bossRotated);
            ksBossRotatedDefinition bossRotatedDef = bossRotated.GetDefinition();
            bossRotatedDef.directionType = (short)Direction_Type.dtNormal;
            bossRotatedDef.SetSketch(sketch);
            bossRotatedDef.SetSideParam(true, 360);
            bossRotated.Create();
        }

        /// <summary>
        /// Построение рисунка на задней части корпуса звездолёта.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="point2Ds">Массив отрезков.</param>
        /// <returns>Сформированный эскиз.</returns>
        private ksEntity BuildBackDrawing(Point3D centerPlaneCoordinates, Point2D[,] point2Ds)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity plane = CreateSketchByPoint(centerPlaneCoordinates);
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < point2Ds.GetLength(0); i++)
            {
                sketchEdit.ksLineSeg(point2Ds[i, 0].X, point2Ds[i, 0].Y,
                    point2Ds[i, 1].X, point2Ds[i, 1].Y, 1);                
            }            
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Выдавливание на определенное расстояние.
        /// </summary>
        /// <param name="part">Деталь.</param>
        /// <param name="sketch">Эскиз.</param>
        /// <param name="height">Высота выдавливания.</param>
        /// <param name="direction">Направление: true - прямое, false - обратное.</param>
        /// <param name="draftValue">Угол, на который изменяется проекция эскиза.</param>
        /// <param name="isThin">Толщина стенок: true - выдавливается контур, false - эскиз.</param>
        private void ExtrudeSketch(ksPart part, ksEntity sketch, double height, bool direction,
            double draftValue, bool isThin)
        {
            ksEntity entityExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);

            ksBaseExtrusionDefinition extrusionDefinition = (ksBaseExtrusionDefinition)entityExtrusion.GetDefinition();
            if (direction)
            {
                extrusionDefinition.directionType = (short)Direction_Type.dtNormal;
                extrusionDefinition.SetSideParam(true, (short)End_Type.etBlind, height, draftValue);
            }
            else
            {
                extrusionDefinition.directionType = (short)Direction_Type.dtReverse;
                extrusionDefinition.SetSideParam(false, (short)End_Type.etBlind, height, draftValue);
            }
            if (isThin)
            {
                extrusionDefinition.SetThinParam(true, (short)End_Type.etBlind, 1, 0);
            }
            extrusionDefinition.SetSketch(sketch);
            entityExtrusion.Create();
        }

        /// <summary>
        /// Метод, выполняющий вырезание выдавливанием по эскизу
        /// </summary>
        /// <param name="part">Интерфейс компонента</param>
        /// <param name="sketch">Эскиз</param>
        /// <param name="topHeight">Высота вырезания вверх</param>
        /// <param name="bottomHeight">Высота вырезания вниз</param>
        /// <param name="type">Тип выдавливания</param>
        private void CutExtrusion(ksPart part, ksEntity sketch,
            double topHeight, double bottomHeight, bool type)
        {
            ksEntity entityCutExtrusion = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition cutExtrusionDefinition =
                (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
            if (type == false)
            {
                cutExtrusionDefinition.directionType = (short)Direction_Type.
                    dtNormal;
                cutExtrusionDefinition.SetSideParam(true, (short)End_Type.
                    etBlind, topHeight);
            }
            if (type == true)
            {
                cutExtrusionDefinition.directionType = (short)Direction_Type.
                    dtBoth;
                cutExtrusionDefinition.SetSideParam(true, (short)End_Type.
                    etBlind, topHeight);
                cutExtrusionDefinition.SetSideParam(false, (short)End_Type.
                    etBlind, bottomHeight);
            }
            cutExtrusionDefinition.SetSketch(sketch);
            entityCutExtrusion.Create();
        }

        /// <summary>
        /// Создание эскиза по точке.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Центр плоскости, на которой строится эскиз.</param>
        /// <returns>Сформированный эскиз.</returns>
        private ksEntity CreateSketchByPoint(Point3D centerPlaneCoordinates)
        {
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(
               centerPlaneCoordinates.X,
               centerPlaneCoordinates.Y,
               centerPlaneCoordinates.Z);
            ksEntity basePlane = collection.First();
            return basePlane;
        }

        /// <summary>
        /// Создание эскиза по выбранной плоскости.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Массив координат центра плоскости.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <returns></returns>
        private ksEntity CreatePolygonSketchByPoint(Point3D centerPlaneCoordinates,
            Point2D[] polygonVertices)
        {
            ksEntity basePlane = CreateSketchByPoint(centerPlaneCoordinates);
            ksEntity skecth = CreatePolygonSkecth(basePlane,
                polygonVertices);
            return skecth;
        }

        /// <summary>
        /// Создание эскиза по базовой плоскости.
        /// </summary>
        /// <param name="planeType">Базовая плоскость.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <returns></returns>
        private ksEntity CreatePolygonSketchByDefaultPlane(Obj3dType planeType,
            Point2D[] polygonVertices)
        {
            ksEntity basePlane = (ksEntity)_part.GetDefaultEntity((short)planeType);
            ksEntity skecth = CreatePolygonSkecth(basePlane,
            polygonVertices);
            return skecth;
        }

        /// <summary>
        /// Создание эскиза многоугольника.
        /// </summary>
        /// <param name="plane">Плоскость эскиза.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <param name="verticesNumber">Количество вершин многоугольника.</param>
        /// <returns></returns>
        private ksEntity CreatePolygonSkecth(ksEntity plane,
            Point2D[] polygonVertices)
        {
            int verticesNumber = polygonVertices.GetLength(0) - 1;
            ksEntity sketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < polygonVertices.GetLength(0)-1; i++)
            {
                sketchEdit.ksLineSeg(polygonVertices[i].X, polygonVertices[i].Y,
                polygonVertices[i + 1].X, polygonVertices[i + 1].Y, 1);
            }
            sketchEdit.ksLineSeg(polygonVertices[verticesNumber].X,
                polygonVertices[verticesNumber].Y, polygonVertices[0].X,
                polygonVertices[0].Y, 1);
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание эскиза окружностей.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="circles">Параметры окружности, включающие центр и радиус.</param>
        /// <returns></returns>
        private ksEntity CreateCirclesSketch(Point3D centerPlaneCoordinates, Circle[] circles)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity basePlane = CreateSketchByPoint(centerPlaneCoordinates);
            definition.SetPlane(basePlane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < circles.GetLength(0); i++)
            {
                sketchEdit.ksCircle(circles[i].Center.X, circles[i].Center.Y,
                    circles[i].Radius, 1);
            }
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание нескольких многоугольников по координатам.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="polygonCoordinates">Координаты многоугольника.</param>
        /// <returns>Сформированный эскиз.</returns>
        private ksEntity CreateSeveralPolygon(Point3D centerPlaneCoordinates, 
            Point2D[,] polygonCoordinates, bool isMirrored)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity basePlane = CreateSketchByPoint(centerPlaneCoordinates);
            definition.SetPlane(basePlane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < polygonCoordinates.GetLength(0); i++)
            {
                for (int j = 0; j < polygonCoordinates.GetLength(1) - 1; j++)
                {
                    sketchEdit.ksLineSeg(polygonCoordinates[i, j].X, polygonCoordinates[i, j].Y,
                        polygonCoordinates[i, j + 1].X, polygonCoordinates[i, j + 1].Y, 1);
                    if (isMirrored)
                    {
                        sketchEdit.ksLineSeg(-polygonCoordinates[i, j].X, polygonCoordinates[i, j].Y,
                        -polygonCoordinates[i, j + 1].X, polygonCoordinates[i, j + 1].Y, 1);
                    }
                }
                sketchEdit.ksLineSeg(polygonCoordinates[i, polygonCoordinates.GetLength(1) - 1].X,
                    polygonCoordinates[i, polygonCoordinates.GetLength(1) - 1].Y,
                        polygonCoordinates[i, 0].X, polygonCoordinates[i, 0].Y, 1);
                if (isMirrored)
                {
                    sketchEdit.ksLineSeg(-polygonCoordinates[i, polygonCoordinates.GetLength(1) - 1].X,
                    polygonCoordinates[i, polygonCoordinates.GetLength(1) - 1].Y,
                        -polygonCoordinates[i, 0].X, polygonCoordinates[i, 0].Y, 1);
                }
            }
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создания фаски.
        /// </summary>
        /// <param name="shift">Сдвиг по координатам из-за изменяемого параметра.</param>
        /// <param name="chamferDiscance">Массив расстояний для создания фаски.</param>
        /// <param name="edgeCoordinate">Координата ребра, где будет фаска.</param>
        private void CreateChamfer(double shift, double[] chamferDiscance, Point3D edgeCoordinate)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_chamfer);
            ksChamferDefinition definition = sketch.GetDefinition();
            definition.tangent = true;
            definition.SetChamferParam(true, chamferDiscance[0], chamferDiscance[1]);
            var array = definition.array();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            collection.SelectByPoint(edgeCoordinate.X, edgeCoordinate.Y,
                edgeCoordinate.Z + shift);
            ksEntity edge = collection.Last();
            array.Add(edge);
            sketch.Create();
        }

        /// <summary>
        /// Создания скругления.
        /// </summary>
        /// <param name="shift">Сдвиг по координатам из-за изменяемого параметра.</param>
        /// <param name="edgeCoordinatesArray">Массив координат рёбер, где будет скругление.</param>
        /// <param name="radius">Радиус скругления.</param>
        private void CreateFillet(double shift, Point3D[] edgeCoordinatesArray, double radius)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_fillet);
            ksFilletDefinition definition = sketch.GetDefinition();
            ksEntityCollection collection;
            definition.radius = radius;
            definition.tangent = false;
            var array = definition.array();
            for (int i = 0; i < edgeCoordinatesArray.GetLength(0); i++)
            {
                collection = _part.EntityCollection((short)Obj3dType.o3d_edge);
                collection.SelectByPoint(edgeCoordinatesArray[i].X,
                    edgeCoordinatesArray[i].Y,
                    edgeCoordinatesArray[i].Z + shift);
                ksEntity iEdge = collection.Last();
                array.Add(iEdge);
            }
            sketch.Create();
        }

        /// <summary>
        /// Изменение радиуса у массива кругов.
        /// </summary>
        /// <param name="circles">Массив кругов.</param>
        /// <param name="radius">Новый радиус.</param>
        private void ChangeCirclesRadius(Circle[] circles, double radius)
        {
            for (int i = 0; i < circles.GetLength(0); i++)
            {
                circles[i].Radius = radius;
            }
        }

        /// <summary>
        /// Создание эскиза из отрезков и дуг.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="point2Ds">Массив точек, являющихся концами отрезков.</param>
        /// <param name="arcs">Массив дуг.</param>
        /// <returns></returns>
        private ksEntity CreateSegmentsWithArcs(Point3D centerPlaneCoordinates, Point2D[,,] point2Ds, 
            Arc[,] arcs, bool isMirrored)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity plane = CreateSketchByPoint(centerPlaneCoordinates);
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < arcs.GetLength(0); i++)
            {
                for (int j = 0; j < arcs.GetLength(1); j++)
                {
                    sketchEdit.ksLineSeg(point2Ds[i, j, 0].X, point2Ds[i, j, 0].Y,
                        point2Ds[i, j, 1].X, point2Ds[i, j, 1].Y, 1);
                    sketchEdit.ksArcByPoint(arcs[i, j].Center.X, arcs[i, j].Center.Y, arcs[i, j].Radius,
                        arcs[i, j].StartPoint.X, arcs[i, j].StartPoint.Y, 
                        arcs[i, j].EndPoint.X, arcs[i, j].EndPoint.Y,
                        arcs[i, j].Direction, 1);
                    if (isMirrored)
                    {
                        sketchEdit.ksLineSeg(-point2Ds[i, j, 0].X, point2Ds[i, j, 0].Y,
                            -point2Ds[i, j, 1].X, point2Ds[i, j, 1].Y, 1);
                        sketchEdit.ksArcByPoint(-arcs[i, j].Center.X, arcs[i, j].Center.Y, arcs[i, j].Radius,
                            -arcs[i, j].StartPoint.X, arcs[i, j].StartPoint.Y, 
                            -arcs[i, j].EndPoint.X, arcs[i, j].EndPoint.Y,
                            (short)-arcs[i, j].Direction, 1);
                    }
                }
            }
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание эскиза из отрезков и кругов.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="point2Ds">Массив точек, являющихся концами отрезков.</param>
        /// <param name="circles">Массив кругов.</param>
        /// <returns></returns>
        private ksEntity CreateSegmentsWithCircles(Point3D centerPlaneCoordinates, 
            Point2D[,,] point2Ds, Circle[,] circles)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity plane = CreateSketchByPoint(centerPlaneCoordinates);
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < point2Ds.GetLength(0); i++)
            {
                for (int j = 0; j < point2Ds.GetLength(1); j++)
                {
                    sketchEdit.ksLineSeg(point2Ds[i, j, 0].X, point2Ds[i, j, 0].Y,
                        point2Ds[i, j, 1].X, point2Ds[i, j, 1].Y, 1);
                    sketchEdit.ksLineSeg(-point2Ds[i, j, 0].X, point2Ds[i, j, 0].Y,
                        -point2Ds[i, j, 1].X, point2Ds[i, j, 1].Y, 1);
                }
            }
            for (int i = 0; i < circles.GetLength(0); i++)
            {
                for (int j = 0; j < circles.GetLength(1); j++)
                {
                    sketchEdit.ksCircle(circles[i, j].Center.X, circles[i, j].Center.Y,
                        circles[i, j].Radius, 1);
                    sketchEdit.ksCircle(-circles[i, j].Center.X, circles[i, j].Center.Y,
                        circles[i, j].Radius, 1);
                }
            }
            definition.EndEdit();
            return sketch;
        }
    }
}