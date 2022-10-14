using System;
using Kompas6API5;
using System.Runtime.InteropServices;
using XWingPluginForCompass3D.Model;
using Kompas6Constants3D;

namespace XWingPluginForCompass3D.Wrapper
{
    /// <summary>
    /// Класс для запуска плагина в САПР Компас-3D.
    /// </summary>
    public class KompasWrapper
    {
        /// <summary>
        /// Объект Компас API.
        /// </summary>
        private KompasObject _kompas = null;

        /// <summary>
        /// Деталь.
        /// </summary>
        private ksPart _part;

        /// <summary>
        /// Документ-модель.
        /// </summary>
        private ksDocument3D _document;

        /// <summary>
        /// Эскиз.
        /// </summary>
        private ksEntity _sketch;

        /// <summary>
        /// Базовая плоскость XOY.
        /// </summary>
        private readonly Obj3dType _planeXOY = Obj3dType.o3d_planeXOY;

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
        /// Устанавливает и возвращает деталь X-Wing.
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
        /// Устанавливает и возвращает документ-модель.
        /// </summary>
        public ksDocument3D Document
        {
            set
            {
                _document = value;
            }
            get
            {
                return _document;
            }
        }

        /// <summary>
        /// Возвращает базовую плоскость XOY.
        /// </summary>
        public Obj3dType PlaneXOY
        {
            get
            {
                return _planeXOY;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает эскиз.
        /// </summary>
        public ksEntity Sketch
        {
            set
            {
                _sketch = value;
            }
            get
            {
                return _sketch;
            }
        }

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
                if (Kompas == null)
                {
                    Type kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    Kompas = (KompasObject)Activator.CreateInstance(kompasType);
                    StartKompas();
                    if (Kompas == null)
                    {
                        throw new Exception("Не удается открыть Koмпас-3D.");
                    }
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
            Part.SetAdvancedColor(14211288, 0.5, 0.6, 0.8, 0.8, 1, 0.5);
            Part.Update();
        }

        /// <summary>
        /// Построение полусферы.
        /// </summary>
        /// <param name="planePoint">Массив координат центра плоскости.</param>
        public void BuildHemisphere(Point3D planePoint, Arc arc)
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
        /// Построение набора отрезков.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="point2Ds">Массив отрезков.</param>
        /// <returns>Сформированный эскиз.</returns>
        public ksEntity BuildSetSegments(Point3D planePoint, Point2D[,] point2Ds)
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
		// TODO: переименовать параметр isThin
		/// <param name="isThin">Толщина стенок: true - выдавливается контур, false - эскиз.</param>
		public void ExtrudeSketch(ksEntity sketch, double height, bool direction,
            double draftValue, bool isThin)
        {
            ksEntity entity = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            ksBaseExtrusionDefinition definition =
                (ksBaseExtrusionDefinition)entity.GetDefinition();
	        // TODO: дубль
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
        public void CutExtrusion(ksEntity sketch, double height, bool direction)
        {
            ksEntity entity = (ksEntity)Part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition definition = (ksCutExtrusionDefinition)entity.GetDefinition();
			// TODO: дубль
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
        public ksEntity CreatePlaneByPoint(Point3D planePoint)
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
        public ksEntity CreatePolygonByDefaultPlane(Obj3dType planeType,
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
        public ksEntity CreatePolygonSketchByPoint(Point3D planePoint,
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
        public ksEntity CreatePolygons(ksEntity plane, Point2D[,] vertices, bool isMirrored)
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
        public ksEntity CreateCirclesSketch(Point3D planePoint, Circle[] circles)
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
        public void CreateChamfer(double shift, double[] chamferDistance, Point3D edgeCoordinate)
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
        public void CreateFillet(double shift, Point3D[] edgeCoordinatesArray, double radius)
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
        /// Создание эскиза из отрезков и дуг.
        /// </summary>
        /// <param name="planePoint">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="segmentPoints">Массив точек, являющихся концами отрезков.</param>
        /// <param name="arcs">Массив дуг.</param>
        /// <returns></returns>
        public ksEntity CreateSegmentsWithArcs(Point3D planePoint, Point2D[,,] segmentPoints,
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
					// TODO: дубль
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
        public ksEntity CreateSegmentsWithCircles(Point3D planePoint,
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