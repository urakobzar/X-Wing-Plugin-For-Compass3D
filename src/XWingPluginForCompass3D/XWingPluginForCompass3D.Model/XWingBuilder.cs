using Kompas6API5;
using Kompas6Constants3D;
using System;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс построения 3D-модели звёздного истребителя X-Wing.
    /// </summary>
    public class XWingBuilder
    {
        /// <summary>
        /// Объект Компас API.
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Устанавливает и возвращает объект Компас API.
        /// </summary>
        public KompasObject Kompas
        {
            set
            {
                _kompas = value;
            }
            get
            {
                return _kompas;
            }
        }

        /// <summary>
        /// Деталь X-Wing.
        /// </summary>
        private ksPart _part;

        /// <summary>
        /// Утсанавливает и возвращает деталь X-Wing.
        /// </summary>
        public ksPart Part
        {
            set
            {
                _part = value;
            }
            get
            {
                return _part;
            }
        }        

        /// <summary>
        /// Создаёт экземпляр класса для построения детали.
        /// </summary>
        /// <param name="kompas">Объект Компас API.</param>
        public XWingBuilder(KompasObject kompas)
        {
            Kompas = kompas;
            ksDocument3D document = (ksDocument3D)kompas.Document3D();
            document.Create();
        }

        /// <summary>
        /// Построение детали по заданным параметрам.
        /// </summary>
        /// <param name="xwing">Объект заданных параметров X-Wing.</param>
        public void BuildDetail(XWing xwing)
        {
            // Разница между длиной корпуса и шириной крыльев.
            double bodyAndWingsDifference = xwing.BodyLength - xwing.WingWidth;
            ksDocument3D document = (ksDocument3D)Kompas.ActiveDocument3D();
            Part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            Part.name = "X-Wing";
            Part.SetAdvancedColor(14211288, 0.5, 0.6, 0.8, 0.8, 1, 0.5);
            Part.Update();
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
            ksEntity sketch = CreatePolygonByDefaultPlane(Obj3dType.o3d_planeXOY,
                constants.UpperBaseVertexes, false);
            ExtrudeSketch(sketch, 600, false, 5, false);
            ExtrudeSketch(sketch, bowLength, true, -3, false);

            // Выдавливание кабины            
            sketch = CreatePolygonSketchByPoint(constants.UpperFacePlane,
                constants.UpperFaceVertexes, false);
            ExtrudeSketch(sketch, 50, true, 0, false);

            // Вырез кабины                        
            sketch = CreatePolygonSketchByPoint(constants.CockpitFrontFacePlane,
                constants.CockpitCutoutVertexes, true);
            CutExtrusion(sketch, 602.5, true);

            // Срез кабины
            sketch = CreatePolygonSketchByPoint(constants.CockpitSideFacePlane,
                constants.CockpitSliceVertexes, false);
            CutExtrusion(sketch, 100, true);

            // Острие носа
            sketch = CreatePolygonSketchByPoint(constants.TipFrontBasePlane,
                constants.TipBowBodyVertexes, false);
            ExtrudeSketch(sketch, 100, true, -5, false);
            ExtrudeSketch(sketch, 35, false, -5, false);

            // Фаска верхней части острия носовой части
            CreateChamfer(bowLength, constants.TipUpperEdgeChamferDistances,
                constants.TipUpperEdge);

            // Фаска нижней части острия носовой части
            CreateChamfer(bowLength, constants.TipLowerEdgeChamferDistances,
                constants.TipLowerEdge);

            // Скругление основной части острия
            CreateFillet(bowLength, constants.TipFrontFilletEdgeCoordinates, 5);

            // Скругление боковой части острия
            CreateFillet(bowLength, constants.TipSideFilletEdgeCoordinates, 10);
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
            ksEntity sketch = CreatePolygonSketchByPoint(constants.UpperBasePlane,
                constants.BaseVertexes, false);
            ExtrudeSketch(sketch, bodyLength, true, 0, false);

            // Выдавливание верхней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.UpperFacePlane,
                constants.UpperFaceVertexes, false);
            ExtrudeSketch(sketch, 50, true, 0, false);

            // Выдавливание нижней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.LowerFacePlane,
                constants.LowerFaceVertexes, false);
            ExtrudeSketch(sketch, 50, true, 0, false);     

            // Срез нижней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.LowerSideBackPlane,
                constants.LowerBodySliceVertexes, false);
            CutExtrusion(sketch, 100, true);

            // Вырез нижней части корпуса: срезаются углы призмы.
            sketch = CreatePolygonSketchByPoint(constants.BackBodyPlane,
                constants.BodyCutoutVertexes, true);
            CutExtrusion(sketch, bodyLength, true);

            // Выдавливание верхней задней грани носовой части корпуса:
            // чтобы не было зазора с основным корпусом.
            sketch = CreatePolygonSketchByPoint(constants.BowBodyBackPlane,
                constants.BowBodyFaceVertexes, false);
            ExtrudeSketch(sketch, 4.5, true, 0, false);

            // Углубление для верхней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.UpperBodyPartFacePlane,
                constants.DeepingUpperBodyFaceVertexes, false);
            CutExtrusion(sketch, 5, true);

            // Выдавливание окружностей в верхней части корпуса.
            sketch = CreateCirclesSketch(constants.DeepingBodyPartFacePlane,
                constants.UpperBodyExtrudingCircles);
            ExtrudeSketch(sketch, 10, true, 0, false);

            // Выдавливание прямоугольников с вырезом в верхней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.DeepingBodyPartFacePlane,
                constants.UpperBodyExtrudingRectangles, false);
            ExtrudeSketch(sketch, 10, true, 0, false);

            // Построение головы дроида в верхней части корпуса.
            BuildDroidHead(constants.BaseDroidHeadPlane, constants.DroidHeadArc);

            // Углубление задней части корпуса.
            sketch = CreatePolygonSketchByPoint(constants.BackBodyPlane,
                constants.BackBodyDeepingVertexes, false);
            CutExtrusion(sketch, 10, true);

            // Первый рисунок задней части корпуса.
            sketch = CreateCirclesSketch(constants.BackDeepingPlane,
                constants.BackDrawingCircles);
            ExtrudeSketch(sketch, 10, true, 0, false);

            // Второй рисунок задней части корпуса.
            sketch = BuildBackDrawing(constants.BackDeepingPlane, 
                constants.BackDrawingSegments);
            ExtrudeSketch(sketch, 10, true, 0, true);
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
            ksEntity sketch = CreatePolygonSketchByPoint(wingsConstants.BackBodyPlane,
                wingsConstants.BaseVertexes, true);
            ExtrudeSketch(sketch, wingsWidth, false, 0, false);

            // Вырезание формы крыла.
            sketch = CreatePolygonSketchByPoint(wingsConstants.CuttingPlane,
                wingsConstants.WingsCutVertexes, true);
            CutExtrusion(sketch, 350, true);
            CutExtrusion(sketch, 100, false);
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
            ksEntity sketch = CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, wingWidth-30, false, 0, false);

            // Построение первой основы бластера.
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 13);
            sketch = CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, 200, true, 0, false);
            constants.CurrentPlane.X = constants.CurrentBlasterCircles[0].Center.X;
            constants.CurrentPlane.Y = constants.CurrentBlasterCircles[0].Center.Y;
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + 200;

            // Построение перехода на вторую основу бластера.
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 11);
            sketch = CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, 5, true, 0, false);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + 5;

            // Построение второй основы бластера.
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 9);
            sketch = CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, 150, true, 0, false);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + 150;

            // Построение перехода на острие бластера.
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 15);
            sketch = CreateCirclesSketch(constants.CurrentPlane, 
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, 5, true, 0, false);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + 5;

            // Построение каждого острия бластера.
            sketch = CreateSegmentsWithArcs(constants.CurrentPlane, 
                constants.TipsBaseSegments, constants.TipsBaseArcs, true);
            ExtrudeSketch(sketch, blasterTipLength, true, 0, false);

            // Построение антенн на острие бластера.
            sketch = CreateSegmentsWithArcs(constants.SideRightTipPlane,
                constants.RightAntennaSegments, constants.RightAntennaArcs, false);
            ExtrudeSketch(sketch, 5, true, 0, false);
            ExtrudeSketch(sketch, 15, false, 0, false);

            // Построение антенн на острие бластера.
            sketch = CreateSegmentsWithArcs(constants.SideLeftTipPlane,
                constants.LeftAntennaSegments, constants.LeftAntennaArcs, false);
            ExtrudeSketch(sketch, 5, true, 0, false);
            ExtrudeSketch(sketch, 15, false, 0, false);

            // Построение начальной части батареи бластера.
            constants.CurrentPlane.Z = -600 - difference - wingWidth + 30;
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 20);
            sketch = CreateCirclesSketch(constants.CurrentPlane,
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, 15, true, -5, false);

            // Построение средней части батареи бластера.
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - 15;
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 18.5);
            sketch = CreateCirclesSketch(constants.CurrentPlane,
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, 10, true, 15, false);

            // Построение конечной части батареи бластера.
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - 10;
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 21);
            sketch = CreateCirclesSketch(constants.CurrentPlane,
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, 10, true, 0, false);

            // Вырез в бластере
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 24);
            sketch = CreateCirclesSketch(constants.FrontBlasterBodyPlane,
                constants.CurrentBlasterCircles);
            CutExtrusion(sketch, 10, true);
            ChangeCirclesRadius(constants.CurrentBlasterCircles, 15);
            sketch = CreateCirclesSketch(constants.FrontBlasterBodyPlane,
                constants.CurrentBlasterCircles);
            ExtrudeSketch(sketch, 10, true, 0, false);

            // Построение рисунка в углублении корпуса бластера.
            sketch = CreateSegmentsWithCircles(constants.FrontBlasterBodyPlane,
                constants.BlasterDrawingSegments, constants.BlasterDrawingCircles);
            ExtrudeSketch(sketch, 10, true, 0, true);            
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
            ksEntity sketch = CreatePolygonSketchByPoint(constants.CurrentPlane,
                constants.AcceleratorsBaseVertexes, true);
            ExtrudeSketch(sketch, 280, false, 0, false);

            // Выдавливание воздухозаборника ускорителя.
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.AirIntakeCircles);
            ExtrudeSketch(sketch, 200, false, 0, false);
            ExtrudeSketch(sketch, 30, true, 0, false);

            // Построение основного выреза в воздухозаборнике.            
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane, 
                constants.AirIntakeBaseCuttingSegments, constants.AirIntakeBaseCuttingArcs, false);
            CutExtrusion(sketch, 150, true);

            // Построение среза нижней части воздухозаборника.
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeLowerCuttingSegments, constants.AirIntakeLowerCuttingArcs, false);
            CutExtrusion(sketch, 10, true);

            // Построение среднего выреза в воздухозаборнике.            
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeMiddleCuttingSegments, constants.AirIntakeMiddleCuttingArcs, false);
            CutExtrusion(sketch, 25, true);

            // Построение малого выреза в воздухозаборнике.            
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakeSmallCuttingSegments, constants.AirIntakeSmallCuttingArcs, false);
            CutExtrusion(sketch, 5, true);

            // Построение перегородки в воздухозаборнике.
            sketch = CreateSegmentsWithArcs(constants.AirIntakePlane,
                constants.AirIntakePartitionSegments, constants.AirIntakePartitionArcs, false);
            ExtrudeSketch(sketch, 150, false, 0, false);

            // Выдавливание турбины ускорителя.
            sketch = CreateCirclesSketch(constants.TurbinePlane, constants.TurbineCircles);
            ExtrudeSketch(sketch, turbineLength, true, 0, false);

            // Выдавливание верхней части сопла.
            constants.CurrentPlane = constants.TurbinePlane;
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - turbineLength;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            ExtrudeSketch(sketch, nozzleLength/2, true, 10, false);

            // Выдавливание нижней части сопла.
            double angle = 10*Math.PI/180;
            double radius = constants.TurbineCircles[0].Radius +
                Math.Tan(angle) * nozzleLength / 2;
            ChangeCirclesRadius(constants.TurbineCircles, radius);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            ExtrudeSketch(sketch, nozzleLength / 2, true, -5, false);

            // Вырезание внешнего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 33);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            CutExtrusion(sketch, nozzleLength / 2, true);

            // Выдавливание средней части сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 15);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + nozzleLength / 2;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            ExtrudeSketch(sketch, nozzleLength / 2, true, 0, false);

            // Вырезание внутреннего отверстия сопла.
            ChangeCirclesRadius(constants.TurbineCircles, 12.5);
            constants.CurrentPlane.Z = constants.CurrentPlane.Z - nozzleLength / 2;
            sketch = CreateCirclesSketch(constants.CurrentPlane, constants.TurbineCircles);
            CutExtrusion(sketch, nozzleLength / 2, true);

            // Выдавливание рисунка сопла.
            constants.CurrentPlane.Z = constants.CurrentPlane.Z + nozzleLength / 2;
            sketch = CreateSegmentsWithCircles(constants.CurrentPlane, constants.NozzleDrawingSegments,
                constants.NozzleDrawingCircles);
            ExtrudeSketch(sketch, nozzleLength / 2, true, 0, true);            
        }

        /// <summary>
        /// Построение головы робота на верхней части корпуса звездолёта.
        /// </summary>
        /// <param name="planePoint">Массив координат центра плоскости.</param>
        private void BuildDroidHead(Point3D planePoint, Arc arc)
        {
            // Построение эскиза полуокружности с осью вращения.

            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity plane = CreatePlaneByPoint(planePoint);
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksLineSeg(arc.StartPoint.X, arc.StartPoint.Y, 
                arc.EndPoint.X, arc.EndPoint.Y, 3);
            sketchEdit.ksArcByPoint(arc.Center.X, arc.Center.Y, arc.Radius, arc.StartPoint.X, 
                arc.StartPoint.Y, arc.EndPoint.X, arc.EndPoint.Y, arc.Direction, 1);
            definition.EndEdit();

            // Выдавливание вращением.

            ksEntity bossRotated = Part.NewEntity((short)Obj3dType.o3d_bossRotated);
            ksBossRotatedDefinition bossRotatedDef = bossRotated.GetDefinition();
            bossRotatedDef.directionType = (short)Direction_Type.dtNormal;
            bossRotatedDef.SetSketch(sketch);
            bossRotatedDef.SetSideParam(true, 360);
            bossRotated.Create();
        }

        /// <summary>
        /// Построение рисунка на задней части корпуса звездолёта.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="point2Ds">Массив отрезков.</param>
        /// <returns>Сформированный эскиз.</returns>
        private ksEntity BuildBackDrawing(Point3D planePoint, Point2D[,] point2Ds)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity plane = CreatePlaneByPoint(planePoint);
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
        /// Выдавливание эскиза на определенное расстояние.
        /// </summary>
        /// <param name="sketch">Эскиз.</param>
        /// <param name="height">Высота выдавливания.</param>
        /// <param name="direction">Направление: true - прямое, false - обратное.</param>
        /// <param name="draftValue">Угол, на который изменяется проекция эскиза.</param>
        /// <param name="isThin">Толщина стенок: true - выдавливается контур, false - эскиз.</param>
        private void ExtrudeSketch(ksEntity sketch, double height, bool direction,
            double draftValue, bool isThin)
        {
            ksEntity entity = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            ksBaseExtrusionDefinition definition = 
                (ksBaseExtrusionDefinition)entity.GetDefinition();
            if (direction)
            {
                definition.directionType = (short)Direction_Type.dtNormal;
                definition.SetSideParam(true, (short)End_Type.etBlind, height, draftValue);
            }
            else
            {
                definition.directionType = (short)Direction_Type.dtReverse;
                definition.SetSideParam(false, (short)End_Type.etBlind, height, draftValue);
            }
            if (isThin)
            {
                definition.SetThinParam(true, (short)End_Type.etBlind, 1, 0);
            }
            definition.SetSketch(sketch);
            entity.Create();
        }

        /// <summary>
        /// Вырезание выдавливанием по эскизу.
        /// </summary>
        /// <param name="sketch">Эскиз.</param>
        /// <param name="height">Высота выдавливания.</param>
        /// <param name="direction">Направление: true - прямое, false - обратное.</param>
        private void CutExtrusion(ksEntity sketch, double height, bool direction)
        {
            ksEntity entity = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition definition =(ksCutExtrusionDefinition)entity.GetDefinition();
            if (direction)
            {
                definition.directionType = (short)Direction_Type.dtNormal;
                definition.SetSideParam(true, (short)End_Type.etBlind, height);
            }
            else
            {
                definition.directionType = (short)Direction_Type.dtReverse;
                definition.SetSideParam(false, (short)End_Type.etBlind, height);
            }
            definition.SetSketch(sketch);
            entity.Create();
        }

        /// <summary>
        /// Создание плоскости эскиза по точке.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <returns>Сформированная плоскость эскиз.</returns>
        private ksEntity CreatePlaneByPoint(Point3D planePoint)
        {
            ksEntityCollection collection = Part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(planePoint.X, planePoint.Y, planePoint.Z);
            ksEntity plane = collection.First();
            return plane;
        }

        /// <summary>
        /// Создание эскиза многоугольника по базовой плоскости.
        /// </summary>
        /// <param name="planeType">Базовая плоскость.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <returns>Сформированный эскиз.</returns>
        private ksEntity CreatePolygonByDefaultPlane(Obj3dType planeType,
            Point2D[,] polygonVertices, bool isMirrored)
        {
            ksEntity plane = (ksEntity)Part.GetDefaultEntity((short)planeType);
            ksEntity skecth = CreatePolygons(plane, polygonVertices, isMirrored);
            return skecth;
        }

        /// <summary>
        /// Создание эскиза по выбранной плоскости.
        /// </summary>
        /// <param name="planePoint">Массив координат центра плоскости.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <returns></returns>
        private ksEntity CreatePolygonSketchByPoint(Point3D planePoint,
            Point2D[,] polygonVertices, bool isMirrored)
        {
            ksEntity basePlane = CreatePlaneByPoint(planePoint);
            ksEntity skecth = CreatePolygons(basePlane,
                polygonVertices, isMirrored);
            return skecth;
        }

        /// <summary>
        /// Создание многоугольников по координатам вершин.
        /// </summary>
        /// <param name="plane">Плоскость эскиза.</param>
        /// <param name="vertices">Координаты вершины многоугольника.</param>
        /// <returns>Сформированный эскиз.</returns>
        private ksEntity CreatePolygons(ksEntity plane, Point2D[,] vertices, bool isMirrored)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                for (int j = 0; j < vertices.GetLength(1) - 1; j++)
                {
                    sketchEdit.ksLineSeg(vertices[i, j].X, vertices[i, j].Y,
                        vertices[i, j + 1].X, vertices[i, j + 1].Y, 1);
                    if (isMirrored)
                    {
                        sketchEdit.ksLineSeg(-vertices[i, j].X, vertices[i, j].Y,
                        -vertices[i, j + 1].X, vertices[i, j + 1].Y, 1);
                    }
                }
                sketchEdit.ksLineSeg(vertices[i, vertices.GetLength(1) - 1].X,
                    vertices[i, vertices.GetLength(1) - 1].Y,
                        vertices[i, 0].X, vertices[i, 0].Y, 1);
                if (isMirrored)
                {
                    sketchEdit.ksLineSeg(-vertices[i, vertices.GetLength(1) - 1].X,
                    vertices[i, vertices.GetLength(1) - 1].Y,
                        -vertices[i, 0].X, vertices[i, 0].Y, 1);
                }
            }
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание эскиза окружностей.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="circles">Параметры окружности, включающие центр и радиус.</param>
        /// <returns>Сформированный эскиз.</returns>
        private ksEntity CreateCirclesSketch(Point3D planePoint, Circle[] circles)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity basePlane = CreatePlaneByPoint(planePoint);
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
        /// Создания фаски.
        /// </summary>
        /// <param name="shift">Сдвиг по координатам из-за изменяемого параметра.</param>
        /// <param name="chamferDistance">Массив расстояний для создания фаски.</param>
        /// <param name="edgeCoordinate">Координата ребра, где будет фаска.</param>
        private void CreateChamfer(double shift, double[] chamferDistance, Point3D edgeCoordinate)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_chamfer);
            ksChamferDefinition definition = sketch.GetDefinition();
            definition.tangent = true;
            definition.SetChamferParam(true, chamferDistance[0], chamferDistance[1]);
            ksEntityCollection array = definition.array();
            ksEntityCollection collection = Part.EntityCollection((short)Obj3dType.o3d_edge);
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
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_fillet);
            ksFilletDefinition definition = sketch.GetDefinition();
            ksEntityCollection collection;
            definition.radius = radius;
            definition.tangent = false;
            ksEntityCollection array = definition.array();
            for (int i = 0; i < edgeCoordinatesArray.GetLength(0); i++)
            {
                collection = Part.EntityCollection((short)Obj3dType.o3d_edge);
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
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="segmentPoints">Массив точек, являющихся концами отрезков.</param>
        /// <param name="arcs">Массив дуг.</param>
        /// <returns></returns>
        private ksEntity CreateSegmentsWithArcs(Point3D planePoint, Point2D[,,] segmentPoints, 
            Arc[,] arcs, bool isMirrored)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity plane = CreatePlaneByPoint(planePoint);
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < arcs.GetLength(0); i++)
            {
                for (int j = 0; j < arcs.GetLength(1); j++)
                {
                    sketchEdit.ksLineSeg(segmentPoints[i, j, 0].X, segmentPoints[i, j, 0].Y,
                        segmentPoints[i, j, 1].X, segmentPoints[i, j, 1].Y, 1);
                    sketchEdit.ksArcByPoint(arcs[i, j].Center.X, arcs[i, j].Center.Y, 
                        arcs[i, j].Radius,
                        arcs[i, j].StartPoint.X, arcs[i, j].StartPoint.Y, 
                        arcs[i, j].EndPoint.X, arcs[i, j].EndPoint.Y,
                        arcs[i, j].Direction, 1);
                    if (isMirrored)
                    {
                        sketchEdit.ksLineSeg(-segmentPoints[i, j, 0].X, segmentPoints[i, j, 0].Y,
                            -segmentPoints[i, j, 1].X, segmentPoints[i, j, 1].Y, 1);
                        sketchEdit.ksArcByPoint(-arcs[i, j].Center.X, arcs[i, j].Center.Y, 
                            arcs[i, j].Radius,
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
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="segmentPoints">Массив точек, являющихся концами отрезков.</param>
        /// <param name="circles">Массив кругов.</param>
        /// <returns></returns>
        private ksEntity CreateSegmentsWithCircles(Point3D planePoint, 
            Point2D[,,] segmentPoints, Circle[,] circles)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntity plane = CreatePlaneByPoint(planePoint);
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < segmentPoints.GetLength(0); i++)
            {
                for (int j = 0; j < segmentPoints.GetLength(1); j++)
                {
                    sketchEdit.ksLineSeg(segmentPoints[i, j, 0].X, segmentPoints[i, j, 0].Y,
                        segmentPoints[i, j, 1].X, segmentPoints[i, j, 1].Y, 1);
                    sketchEdit.ksLineSeg(-segmentPoints[i, j, 0].X, segmentPoints[i, j, 0].Y,
                        -segmentPoints[i, j, 1].X, segmentPoints[i, j, 1].Y, 1);
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