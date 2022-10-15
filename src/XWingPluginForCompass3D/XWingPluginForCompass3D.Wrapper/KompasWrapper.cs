using System;
using Kompas6API5;
using System.Runtime.InteropServices;
using XWingPluginForCompass3D.Model;
using Kompas6Constants3D;

namespace XWingPluginForCompass3D.Wrapper
{
    /// <summary>
    /// Класс для запуска библиотеки в Компас-3D.
    /// </summary>
    public class KompasWrapper
    {
        /// <summary>
        /// Объект Компас API.
        /// </summary>
        public KompasObject Kompas { set; get; }

        /// <summary>
        /// Деталь.
        /// </summary>
        public ksPart Part { set; get; }

        /// <summary>
        /// Документ-модель.
        /// </summary>
        public ksDocument3D Document { set; get; }

        /// <summary>
        /// Возвращает базовую плоскость XOY.
        /// </summary>
        public Obj3dType DefaultPlaneXoY => Obj3dType.o3d_planeXOY;

        /// <summary>
        /// Эскиз.
        /// </summary>
        public ksEntity Sketch { set; get; }

        /// <summary>
        /// Запуск Компас-3D.
        /// </summary>
        public void StartKompas()
        {
            try
            {
                if (Kompas != null)
                {
                    Kompas.Visible = true;
                    Kompas.ActivateControllerAPI();
                }
                if (Kompas != null) return;
                var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                Kompas = (KompasObject)Activator.CreateInstance(kompasType);
                StartKompas();
                if (Kompas == null)
                {
                    throw new Exception("Не удается открыть Компас-3D.");
                }
            }
            catch (COMException)
            {
                Kompas = null;
                StartKompas();
            }
        }

        /// <summary>
        /// Создание документа в Компас-3D.
        /// </summary>
        public void CreateDocument()
        {
            try
            {
                Document = (ksDocument3D)Kompas.Document3D();
                Document.Create();
                Document = (ksDocument3D)Kompas.ActiveDocument3D();
            }
            catch
            {
                throw new ArgumentException("Не удается построить деталь");
            }
        }

        /// <summary>
        /// Установка свойств детали: цвета и имени.
        /// </summary>
        public void SetDetailProperties()
        {
            Part = (ksPart)Document.GetPart((short)Part_Type.pTop_Part);
            Part.name = "X-Wing";
            Part.SetAdvancedColor(14211288, 0.5, 0.6, 
                0.8, 0.8, 1, 0.5);
            Part.Update();
        }

        /// <summary>
        /// Создание плоскости эскиза по точке.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <returns>Сформированная плоскость эскиз.</returns>
        private ksEntity CreatePlaneByPoint(Point3D planePoint)
        {
            ksEntityCollection collection =
                Part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(planePoint.X, planePoint.Y, planePoint.Z);
            ksEntity plane = collection.First();
            return plane;
        }

        /// <summary>
        /// Формирование параметров эскиза.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="sketch">Эскиз.</param>
        /// <returns>Сформированные параметры эскиза.</returns>
        private ksSketchDefinition CreateSketchDefinition(Point3D planePoint,
            ksEntity sketch)
        {
            ksSketchDefinition definition =
                (ksSketchDefinition)sketch.GetDefinition();
            ksEntity plane = CreatePlaneByPoint(planePoint);
            definition.SetPlane(plane);
            sketch.Create();
            return definition;
        }

        /// <summary>
        /// Создание на эскизе отрезков.
        /// </summary>
        /// <param name="sketchEdit">Объект графического документа.</param>
        /// <param name="segmentPoints">Массив точек для построения отрезков.</param>
        /// <param name="isMustBeMirrored">Отражение координат:
        /// true - координаты отражаются, false - не отражаются.</param>
        private static void CreateSegments(ksDocument2D sketchEdit,
            Point2D[,,] segmentPoints, bool isMustBeMirrored)
        {
            for (var i = 0; i < segmentPoints.GetLength(0); i++)
            {
                for (var j = 0; j < segmentPoints.GetLength(1); j++)
                {
                    sketchEdit.ksLineSeg(segmentPoints[i, j, 0].X,
                        segmentPoints[i, j, 0].Y,
                        segmentPoints[i, j, 1].X,
                        segmentPoints[i, j, 1].Y, 1);
                    if (!isMustBeMirrored) continue;
                    sketchEdit.ksLineSeg(-segmentPoints[i, j, 0].X,
                        segmentPoints[i, j, 0].Y,
                        -segmentPoints[i, j, 1].X,
                        segmentPoints[i, j, 1].Y, 1);
                }
            }
        }

        /// <summary>
        /// Создание на эскизе окружностей.
        /// </summary>
        /// <param name="sketchEdit">Объект графического документа.</param>
        /// <param name="circles">Массив окружностей для построения.</param>
        /// <param name="isMustBeMirrored">Отражение координат:
        /// true - координаты отражаются, false - не отражаются.</param>
        private static void CreateCircles(ksDocument2D sketchEdit,
            Circle[,] circles, bool isMustBeMirrored)
        {
            for (var i = 0; i < circles.GetLength(0); i++)
            {
                for (var j = 0; j < circles.GetLength(1); j++)
                {
                    sketchEdit.ksCircle(circles[i, j].Center.X,
                        circles[i, j].Center.Y,
                        circles[i, j].Radius, 1);
                    if (!isMustBeMirrored) continue;
                    sketchEdit.ksCircle(-circles[i, j].Center.X,
                        circles[i, j].Center.Y,
                        circles[i, j].Radius, 1);
                }
            }
        }

        /// <summary>
        /// Создание на эскизе дуг.
        /// </summary>
        /// <param name="sketchEdit">Объект графического документа.</param>
        /// <param name="arcs">Массив дуг для построения.</param>
        /// <param name="isMustBeMirrored">Отражение координат:
        /// true - координаты отражаются, false - не отражаются.</param>
        private static void CreateArs(ksDocument2D sketchEdit, Arc[,] arcs,
            bool isMustBeMirrored)
        {
            for (var i = 0; i < arcs.GetLength(0); i++)
            {
                for (var j = 0; j < arcs.GetLength(1); j++)
                {
                    sketchEdit.ksArcByPoint(arcs[i, j].Center.X,
                        arcs[i, j].Center.Y,
                        arcs[i, j].Radius,
                        arcs[i, j].StartPoint.X,
                        arcs[i, j].StartPoint.Y,
                        arcs[i, j].EndPoint.X,
                        arcs[i, j].EndPoint.Y,
                        arcs[i, j].Direction, 1);
                    if (!isMustBeMirrored) continue;
                    sketchEdit.ksArcByPoint(-arcs[i, j].Center.X,
                        arcs[i, j].Center.Y,
                        arcs[i, j].Radius,
                        -arcs[i, j].StartPoint.X,
                        arcs[i, j].StartPoint.Y,
                        -arcs[i, j].EndPoint.X,
                        arcs[i, j].EndPoint.Y,
                        (short)-arcs[i, j].Direction, 1);
                }
            }
        }

        /// <summary>
        /// Выдавливание эскиза на определенное расстояние.
        /// </summary>
        /// <param name="sketch">Эскиз.</param>
        /// <param name="height">Высота выдавливания.</param>
        /// <param name="direction">Направление: true - прямое, false - обратное.</param>
        /// <param name="draftValue">Угол, на который изменяется проекция эскиза.</param>
        /// <param name="isMustBeThin">Толщина стенок: true - выдавливается контур,
        /// false - эскиз.</param>
        // TODO: переименовать параметр isThin  ИСПРАВИЛ
        public void ExtrudeSketch(ksEntity sketch, double height, bool direction,
            double draftValue, bool isMustBeThin)
        {
            ksEntity entity =
                (ksEntity)Part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            ksBaseExtrusionDefinition definition =
                (ksBaseExtrusionDefinition)entity.GetDefinition();
            // TODO: дубль  ИСПРАВИЛ
            if (direction)
            {
                definition.directionType = (short)Direction_Type.dtNormal;
            }
            else
            {
                definition.directionType = (short)Direction_Type.dtReverse;
            }
            definition.SetSideParam(direction, (short)End_Type.etBlind,
                height, draftValue);
            if (isMustBeThin)
            {
                definition.SetThinParam(true, (short)End_Type.etBlind,
                    1, 0);
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
        public void CutExtrusion(ksEntity sketch, double height, bool direction)
        {
            ksEntity entity = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition definition = (ksCutExtrusionDefinition)entity.GetDefinition();
            // TODO: дубль  ИСПРАВИЛ
            if (direction)
            {
                definition.directionType = (short)Direction_Type.dtNormal;
            }
            else
            {
                definition.directionType = (short)Direction_Type.dtReverse;
            }
            definition.SetSideParam(direction, (short)End_Type.etBlind, height);
            definition.SetSketch(sketch);
            entity.Create();
        }

        /// <summary>
        /// Создания фаски.
        /// </summary>
        /// <param name="shift">Сдвиг по координатам из-за изменяемого параметра.</param>
        /// <param name="chamferDistance">Массив расстояний для создания фаски.</param>
        /// <param name="edgeCoordinate">Координата ребра, где будет фаска.</param>
        public void CreateChamfer(double shift, double[] chamferDistance, 
            Point3D edgeCoordinate)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_chamfer);
            ksChamferDefinition definition = sketch.GetDefinition();
            definition.tangent = true;
            definition.SetChamferParam(true, 
                chamferDistance[0], chamferDistance[1]);
            ksEntityCollection array = definition.array();
            ksEntityCollection collection = 
                Part.EntityCollection((short)Obj3dType.o3d_edge);
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
        /// <param name="edgeCoordinatesArray">Массив координат ребер, где будет скругление.</param>
        /// <param name="radius">Радиус скругления.</param>
        public void CreateFillet(double shift, 
            Point3D[] edgeCoordinatesArray, double radius)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_fillet);
            ksFilletDefinition definition = sketch.GetDefinition();
            definition.radius = radius;
            definition.tangent = false;
            ksEntityCollection array = definition.array();
            for (var i = 0; i < edgeCoordinatesArray.GetLength(0); i++)
            {
                ksEntityCollection collection = 
                    Part.EntityCollection((short)Obj3dType.o3d_edge);
                collection.SelectByPoint(edgeCoordinatesArray[i].X,
                    edgeCoordinatesArray[i].Y,
                    edgeCoordinatesArray[i].Z + shift);
                ksEntity edge = collection.Last();
                array.Add(edge);
            }

            sketch.Create();
        }

        /// <summary>
        /// Создание эскиза многоугольника по базовой плоскости.
        /// </summary>
        /// <param name="planeType">Базовая плоскость.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <param name="isMirrored"></param>
        /// <returns>Сформированный эскиз.</returns>
        public ksEntity BuildPolygonByDefaultPlane(Obj3dType planeType,
            Point2D[,,] polygonVertices, bool isMirrored)
        {
            ksEntity plane = (ksEntity)Part.GetDefaultEntity((short)planeType);
            ksEntity sketch = 
                BuildPolygons(plane, polygonVertices, isMirrored);
            return sketch;
        }

        /// <summary>
        /// Создание эскиза многоугольника по точке.
        /// </summary>
        /// <param name="planePoint">Массив координат центра плоскости.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <param name="isMustBeMirrored">Отражение координат:
        /// true - координаты отражаются, false - не отражаются.</param>
        /// <returns>Сформированный эскиз.</returns>
        public ksEntity BuildPolygonSketchByPoint(Point3D planePoint,
            Point2D[,,] polygonVertices, bool isMustBeMirrored)
        {
            ksEntity plane = CreatePlaneByPoint(planePoint);
            ksEntity sketch = 
                BuildPolygons(plane, polygonVertices, isMustBeMirrored);
            return sketch;
        }

        /// <summary>
        /// Создание многоугольников по координатам вершин.
        /// </summary>
        /// <param name="plane">Плоскость эскиза.</param>
        /// <param name="segmentPoints">Координаты вершины многоугольника.</param>
        /// <param name="isMustBeMirrored">Отражение координат:
        /// true - координаты отражаются, false - не отражаются.</param>
        /// <returns>Сформированный эскиз.</returns>
        private ksEntity BuildPolygons(ksEntity plane,
            Point2D[,,] segmentPoints, bool isMustBeMirrored)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition =
                (ksSketchDefinition)sketch.GetDefinition();
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            CreateSegments(sketchEdit, segmentPoints, isMustBeMirrored);
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание эскиза окружностей.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="circles">Параметры окружности, включающие центр и радиус.</param>
        /// <returns>Сформированный эскиз.</returns>
        public ksEntity BuildCirclesSketch(Point3D planePoint, Circle[,] circles)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition =
                CreateSketchDefinition(planePoint, sketch);
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            CreateCircles(sketchEdit, circles, false);
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание эскиза из отрезков и дуг.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="segmentPoints">Массив точек, являющихся концами отрезков.</param>
        /// <param name="arcs">Массив дуг.</param>
        /// <param name="isMustBeMirrored">Отражение координат:
        /// true - координаты отражаются, false - не отражаются.</param>
        /// <returns>Сформированный эскиз.</returns>
        public ksEntity BuildSegmentsWithArcs(Point3D planePoint, 
            Point2D[,,] segmentPoints, Arc[,] arcs, bool isMustBeMirrored)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = CreateSketchDefinition(planePoint, sketch);
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            // TODO: дубль  ИСПРАВИЛ
            CreateSegments(sketchEdit, segmentPoints, isMustBeMirrored);
            CreateArs(sketchEdit, arcs, isMustBeMirrored);
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание эскиза из отрезков и кругов.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="segmentPoints">Массив точек, являющихся концами отрезков.</param>
        /// <param name="circles">Массив кругов.</param>
        /// <returns>Сформированный эскиз.</returns>
        public ksEntity BuildSegmentsWithCircles(Point3D planePoint,
            Point2D[,,] segmentPoints, Circle[,] circles)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = CreateSketchDefinition(planePoint, sketch);
            var sketchEdit = (ksDocument2D)definition.BeginEdit();
            CreateSegments(sketchEdit, segmentPoints, true);
            CreateCircles(sketchEdit, circles, true);
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Построение набора отрезков.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="segmentPoints">Массив отрезков.</param>
        ///  <param name="isMustBeMirrored">Отражение координат:
        /// true - координаты отражаются, false - не отражаются.</param>
        /// <returns>Сформированный эскиз.</returns>
        public ksEntity BuildSetSegments(Point3D planePoint, 
            Point2D[,,] segmentPoints, bool isMustBeMirrored)
        {
            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = 
                CreateSketchDefinition(planePoint, sketch);
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            CreateSegments(sketchEdit, segmentPoints, isMustBeMirrored);
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Построение полусферы.
        /// </summary>
        /// <param name="planePoint">Массив координат центра плоскости.</param>
        /// <param name="arc"></param>
        public void BuildHemisphere(Point3D planePoint, Arc arc)
        {
            // Построение эскиза полуокружности с осью вращения.

            ksEntity sketch = Part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = 
                CreateSketchDefinition(planePoint, sketch);
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksLineSeg(arc.StartPoint.X, arc.StartPoint.Y,
                arc.EndPoint.X, arc.EndPoint.Y, 3);
            sketchEdit.ksArcByPoint(arc.Center.X, arc.Center.Y,
                arc.Radius, arc.StartPoint.X, arc.StartPoint.Y,
                arc.EndPoint.X, arc.EndPoint.Y, arc.Direction, 1);
            definition.EndEdit();

            // Выдавливание вращением.

            ksEntity bossRotated = Part.NewEntity((short)Obj3dType.o3d_bossRotated);
            ksBossRotatedDefinition bossRotatedDef = bossRotated.GetDefinition();
            bossRotatedDef.directionType = (short)Direction_Type.dtNormal;
            bossRotatedDef.SetSketch(sketch);
            bossRotatedDef.SetSideParam(true, 360);
            bossRotated.Create();
        }
    }
}