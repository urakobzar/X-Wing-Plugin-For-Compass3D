using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants3D;
using System.Runtime.InteropServices;

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
        /// Объект класса констант для построения детали.
        /// </summary>
        XWingConstants _xWingConstants = new XWingConstants();

        /// <summary>
        /// Объект класса констант для построения носовой части детали.
        /// </summary>
        BowBodyXWingConstants _bowBodyXWingConstants;

        /// <summary>
        /// Объект класса констант для построения корпуса детали.
        /// </summary>
        BodyXWingConstants _bodyXWingConstants;


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
        public void CreateDetail(XWingParameters xwing)
        {
            double bodyLength = xwing.BodyLength;
            double wingWidth = xwing.WingWidth;
            double bowLength = xwing.BowLength;
            double weaponBlasterTipLength = xwing.WeaponBlasterTipLength;
            double acceleratorTurbineLength = xwing.AcceleratorTurbineLength;
            double acceleratorNozzleLength = xwing.AcceleratorNozzleLength;

            _bowBodyXWingConstants = new BowBodyXWingConstants(bowLength);
            _bodyXWingConstants = new BodyXWingConstants(bodyLength);

            ksDocument3D document = (ksDocument3D)_kompas.ActiveDocument3D();

            _part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);

            _part.name = "X-Wing";

            _part.SetAdvancedColor(14211288, 0.5, 0.6, 0.8, 0.8, 1, 0.5);

            _part.Update();

            BuildBowBody(bowLength);

            BuildBody(bodyLength);

            Wing(wingWidth);

            BlasterBody();

            Accelerators();
        }

        /// <summary>
        /// Построение носовой части корпуса.
        /// </summary>
        /// <param name="bowLength">Длина носа.</param>
        private void BuildBowBody(double bowLength)
        {
            // Выдавливание основы носовой части корпуса
            ksEntity sketch = CreateSketchByDefaultPlane(Obj3dType.o3d_planeXOY,
                _bowBodyXWingConstants.UpperBaseVertexes, _xWingConstants.HexagonVerticesNumber);
            ExtrudeSketch(_part, sketch, 600, false, 5, false);
            ExtrudeSketch(_part, sketch, bowLength, true, -3, false);

            // Выдавливание кабины            
            sketch = CreateSketchByPoint(_bowBodyXWingConstants.UpperFacePlaneCoordinate,
                _bowBodyXWingConstants.UpperFaceVertexes, _xWingConstants.QadrangleVerticesNumber);
            ExtrudeSketch(_part, sketch, 50, true, 0, false);

            // Вырез кабины                        
            sketch = CreateSketchByPoint(_bowBodyXWingConstants.CockpitFrontFacePlaneCoordinate,
                _bowBodyXWingConstants.FirstCockpitCutoutVertexes, _xWingConstants.TriangleVerticesNumber);
            CutExtrusion(_part, sketch, 602.5, 0, true);
            sketch = CreateSketchByPoint(_bowBodyXWingConstants.CockpitFrontFacePlaneCoordinate,
                _bowBodyXWingConstants.SecondCockpitCutoutVertexes, _xWingConstants.TriangleVerticesNumber);
            CutExtrusion(_part, sketch, 602.5, 0, true);

            // Срез кабины
            ksEntity ssketch = CreateSketchByPoint(_bowBodyXWingConstants.CockpitSideFacePlaneCoordinate,
                _bowBodyXWingConstants.CockpitSliceVertexes, _xWingConstants.TriangleVerticesNumber);
            CutExtrusion(_part, ssketch, 100, 0, true);

            // Острие носа
            sketch = CreateSketchByPoint(_bowBodyXWingConstants.TipBaseFrontPlaneCoordinate,
                _bowBodyXWingConstants.TipBowBodyVertexes, _xWingConstants.HexagonVerticesNumber);
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
            // Выдавливание основного корпуса на заданную пользователем величину.
            ksEntity sketch = CreateSketchByPoint(_bodyXWingConstants.UpperBasePlaneCoordinate,
                _bodyXWingConstants.BaseVertexes, _xWingConstants.HexagonVerticesNumber);
            ExtrudeSketch(_part, sketch, bodyLength, true, 0, false);

            // Выдавливание верхней части корпуса.
            sketch = CreateSketchByPoint(_bodyXWingConstants.UpperFacePlaneCoordinate,
                _bodyXWingConstants.UpperFaceVertexes, _xWingConstants.QadrangleVerticesNumber);
            ExtrudeSketch(_part, sketch, 50, true, 0, false);

            // Выдавливание нижней части корпуса.
            sketch = CreateSketchByPoint(_bodyXWingConstants.LowerFacePlaneCoordinate,
                _bodyXWingConstants.LowerFaceVertexes, _xWingConstants.QadrangleVerticesNumber);
            ExtrudeSketch(_part, sketch, 50, true, 0, false);

            // Вырез верхней части корпуса: срезаются углы призмы.
            sketch = CreateSketchByPoint(_bodyXWingConstants.UpperBodyFrontPlaneCoordinate,
                _bodyXWingConstants.FirstUpperBodyCutoutVertexes, _xWingConstants.TriangleVerticesNumber);
            CutExtrusion(_part, sketch, bodyLength, 0, true);
            sketch = CreateSketchByPoint(_bodyXWingConstants.UpperBodyFrontPlaneCoordinate,
                _bodyXWingConstants.SecondUpperBodyCutoutVertexes, _xWingConstants.TriangleVerticesNumber);
            CutExtrusion(_part, sketch, bodyLength, 0, true);

            // Срез нижней части корпуса.
            sketch = CreateSketchByPoint(_bodyXWingConstants.LowerSideBackPlaneCoordinate,
                _bodyXWingConstants.LowerBodySliceVertexes, _xWingConstants.TriangleVerticesNumber);
            CutExtrusion(_part, sketch, 100, 0, true);

            // Вырез нижней части корпуса: срезаются углы призмы.
            sketch = CreateSketchByPoint(_bodyXWingConstants.LowerBodyBackPlaneCoordinate,
                _bodyXWingConstants.FirstLowerBodyCutoutVertexes, _xWingConstants.TriangleVerticesNumber);
            CutExtrusion(_part, sketch, bodyLength, 0, true);
            sketch = CreateSketchByPoint(_bodyXWingConstants.LowerBodyBackPlaneCoordinate,
                _bodyXWingConstants.SecondLowerBodyCutoutVertexes, _xWingConstants.TriangleVerticesNumber);
            CutExtrusion(_part, sketch, bodyLength, 0, true);

            // Выдавливание верхней задней грани носовой части корпуса:
            // чтобы не было зазора с основным корпусом.
            sketch = CreateSketchByPoint(_bodyXWingConstants.BowBodyBackPlaneCoordinate,
                _bodyXWingConstants.BowBodyFaceVertexes, _xWingConstants.QadrangleVerticesNumber);
            ExtrudeSketch(_part, sketch, 4.5, true, 0, false);

            // Углубление для верхней части корпуса.
            sketch = CreateSketchByPoint(_bodyXWingConstants.UpperBodyPartFacePlaneCoordinate,
                _bodyXWingConstants.DeepingUpperBodyFaceVertexes, _xWingConstants.QadrangleVerticesNumber);
            CutExtrusion(_part, sketch, 5, 0, true);

            // Выдавливание окружностей в верхней части корпуса.
            sketch = CreateCirclesSketch(_bodyXWingConstants.DeepingBodyPartFacePlaneCoordinate,
                _bodyXWingConstants.UpperBodyPartExtrudingCirclesParameters);
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Выдавливание прямоугольников с вырезом в верхней части корпуса.
            sketch = CreatePolygonWithCutout(_bodyXWingConstants.DeepingBodyPartFacePlaneCoordinate,
                _bodyXWingConstants.UpperBodyPartExtrudingRectanglesCoordinates);
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Построение головы дроида в верхней части корпуса.
            BuildDroidHead(_bodyXWingConstants.BaseDroidHeadPlaneCoordinate);

            // Углубление задней части корпуса.
            sketch = CreateSketchByPoint(_bodyXWingConstants.BackBodyPlaneCoordinate,
                _bodyXWingConstants.BackBodyDeepingVertexes, _xWingConstants.HexagonVerticesNumber);
            CutExtrusion(_part, sketch, 10, 0, true);

            // Первый рисунок задней части корпуса.
            sketch = CreateCirclesSketch(_bodyXWingConstants.BackDeepingPlaneCoordinate,
                _bodyXWingConstants.BackBodyPartExtrudingCirclesParameters);
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Второй рисунок задней части корпуса.
            BuildBackDrawing(_bodyXWingConstants.BackDeepingPlaneCoordinate);
        }

        /// <summary>
        /// Построение крыльев.
        /// </summary>
        private void Wing(double wingWidth)
        {

            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(0, 0, -600 - 300);
            ksEntity plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksLineSeg(69.173833, 55.779384, 78.023751, 42.034027, 1);
            sketchEdit.ksLineSeg(78.023751, 42.034027, 632.086192, 150.642161, 1);
            sketchEdit.ksLineSeg(632.086192, 150.642161, 639.549926, 169.837344, 1);
            sketchEdit.ksLineSeg(639.549926, 169.837344, 69.173833, 55.779384, 1);

            sketchEdit.ksLineSeg(-69.173833, 55.779384, -78.023751, 42.034027, 1);
            sketchEdit.ksLineSeg(-78.023751, 42.034027, -632.086192, 150.642161, 1);
            sketchEdit.ksLineSeg(-632.086192, 150.642161, -639.549926, 169.837344, 1);
            sketchEdit.ksLineSeg(-639.549926, 169.837344, -69.173833, 55.779384, 1);

            sketchEdit.ksLineSeg(77.853158, -39.308584, 64.171267, -54.321075, 1);
            sketchEdit.ksLineSeg(64.171267, -54.321075, 646.963505, -163.875661, 1);
            sketchEdit.ksLineSeg(646.963505, -163.875661, 640.123102, -145.081809, 1);
            sketchEdit.ksLineSeg(640.123102, -145.081809, 77.853158, -39.308584, 1);

            sketchEdit.ksLineSeg(-77.853158, -39.308584, -64.171267, -54.321075, 1);
            sketchEdit.ksLineSeg(-64.171267, -54.321075, -646.963505, -163.875661, 1);
            sketchEdit.ksLineSeg(-646.963505, -163.875661, -640.123102, -145.081809, 1);
            sketchEdit.ksLineSeg(-640.123102, -145.081809, -77.853158, -39.308584, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 300, false, 0, false);

            sketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            plane = (ksEntity)_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)sketch.GetDefinition();
            sketchDefinition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)sketchDefinition.BeginEdit();

            sketchEdit.ksLineSeg(646.963505, 600, 646.963505, 666.419923, 1);
            sketchEdit.ksLineSeg(646.963505, 666.419923, 128.282347, 600, 1);
            sketchEdit.ksLineSeg(128.282347, 600, 646.963505, 600, 1);

            sketchEdit.ksLineSeg(646.963505, 600 + 300, 646.963505, 850, 1);
            sketchEdit.ksLineSeg(646.963505, 600 + 300 - 50, 128.282347, 600 + 300, 1);
            sketchEdit.ksLineSeg(128.282347, 600 + 300, 646.963505, 600 + 300, 1);

            sketchEdit.ksLineSeg(-646.963505, 600, -646.963505, 666.419923, 1);
            sketchEdit.ksLineSeg(-646.963505, 666.419923, -128.282347, 600, 1);
            sketchEdit.ksLineSeg(-128.282347, 600, -646.963505, 600, 1);

            sketchEdit.ksLineSeg(-646.963505, 600 + 300, -646.963505, 850, 1);
            sketchEdit.ksLineSeg(-646.963505, 850, -128.282347, 600 + 300, 1);
            sketchEdit.ksLineSeg(-128.282347, 600 + 300, -646.963505, 600 + 300, 1);

            sketchDefinition.EndEdit();
            CutExtrusion(_part, sketch, 200, 200, true);
        }

        /// <summary>
        /// Построение бластерного оружия.
        /// </summary>
        private void BlasterBody()
        {
            // Основа тела бластера
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(98.760456157148, 54.834961060082, -600);
            ksEntity plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(-617.468507, -180, 25, 1);
            sketchEdit.ksCircle(617.468507, -180, 25, 1);
            sketchEdit.ksCircle(-617.468507, 185, 25, 1);
            sketchEdit.ksCircle(617.468507, 185, 25, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 270, false, 0, false);

            // Толская часть тела
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(98.760456157148, 54.834961060082, -600);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(-617.468507, -180, 13, 1);
            sketchEdit.ksCircle(617.468507, -180, 13, 1);
            sketchEdit.ksCircle(-617.468507, 185, 13, 1);
            sketchEdit.ksCircle(617.468507, 185, 13, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 200, true, 0, false);

            // Переход на узкую часть тела
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(617.468507, 185, -600 + 200);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(-617.468507, -180, 11, 1);
            sketchEdit.ksCircle(617.468507, -180, 11, 1);
            sketchEdit.ksCircle(-617.468507, 185, 11, 1);
            sketchEdit.ksCircle(617.468507, 185, 11, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 5, true, 0, false);

            // Узкая часть тела
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(617.468507, 185, -600 + 200 + 5);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(-617.468507, -180, 9, 1);
            sketchEdit.ksCircle(617.468507, -180, 9, 1);
            sketchEdit.ksCircle(-617.468507, 185, 9, 1);
            sketchEdit.ksCircle(617.468507, 185, 9, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 150, true, 0, false);


            // Переход на острие бластера
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(617.468507, 185, -600 + 200 + 5 + 150);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(-617.468507, -180, 15, 1);
            sketchEdit.ksCircle(617.468507, -180, 15, 1);
            sketchEdit.ksCircle(-617.468507, 185, 15, 1);
            sketchEdit.ksCircle(617.468507, 185, 15, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 5, true, 0, false);

            // Острие бластера
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(617.468507, 185, -600 + 200 + 5 + 150 + 5);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            // Правое нижнее Острие бластера
            sketchEdit.ksLineSeg(-622.468507, -179, -622.468507, -181, 1);
            sketchEdit.ksLineSeg(-612.468507, -179, -612.468507, -181, 1);
            sketchEdit.ksLineSeg(-616.468507, -175, -618.468507, -175, 1);
            sketchEdit.ksLineSeg(-616.468507, -185, -618.468507, -185, 1);
            sketchEdit.ksArcByPoint(-617.468507, -180, 5.099019513593, -612.468507, -181, -616.468507, -185, -1, 1);
            sketchEdit.ksArcByPoint(-617.468507, -180, 5.099019513593, -618.468507, -185, -622.468507, -181, -1, 1);
            sketchEdit.ksArcByPoint(-617.468507, -180, 5.099019513593, -622.468507, -179, -618.468507, -175, -1, 1);
            sketchEdit.ksArcByPoint(-617.468507, -180, 5.099019513593, -616.468507, -175, -612.468507, -179, -1, 1);
            // Левое нижнее Острие бластера
            sketchEdit.ksLineSeg(622.468507, -179, 622.468507, -181, 1);
            sketchEdit.ksLineSeg(612.468507, -179, 612.468507, -181, 1);
            sketchEdit.ksLineSeg(616.468507, -175, 618.468507, -175, 1);
            sketchEdit.ksLineSeg(616.468507, -185, 618.468507, -185, 1);
            sketchEdit.ksArcByPoint(617.468507, -180, 5.099019513593, 612.468507, -181, 616.468507, -185, 1, 1);
            sketchEdit.ksArcByPoint(617.468507, -180, 5.099019513593, 618.468507, -185, 622.468507, -181, 1, 1);
            sketchEdit.ksArcByPoint(617.468507, -180, 5.099019513593, 622.468507, -179, 618.468507, -175, 1, 1);
            sketchEdit.ksArcByPoint(617.468507, -180, 5.099019513593, 616.468507, -175, 612.468507, -179, 1, 1);
            // Правое вверхнее Острие бластера
            sketchEdit.ksLineSeg(-622.468507, 184, -622.468507, 186, 1);
            sketchEdit.ksLineSeg(-612.468507, 184, -612.468507, 186, 1);
            sketchEdit.ksLineSeg(-616.468507, 180, -618.468507, 180, 1);
            sketchEdit.ksLineSeg(-616.468507, 190, -618.468507, 190, 1);
            sketchEdit.ksArcByPoint(-617.468507, 185, 5.099019513593, -612.468507, 186, -616.468507, 190, 1, 1);
            sketchEdit.ksArcByPoint(-617.468507, 185, 5.099019513593, -618.468507, 190, -622.468507, 186, 1, 1);
            sketchEdit.ksArcByPoint(-617.468507, 185, 5.099019513593, -622.468507, 184, -618.468507, 180, 1, 1);
            sketchEdit.ksArcByPoint(-617.468507, 185, 5.099019513593, -616.468507, 180, -612.468507, 184, 1, 1);
            // Левое верхнее Острие бластера
            sketchEdit.ksLineSeg(622.468507, 184, 622.468507, 186, 1);
            sketchEdit.ksLineSeg(612.468507, 184, 612.468507, 186, 1);
            sketchEdit.ksLineSeg(616.468507, 180, 618.468507, 180, 1);
            sketchEdit.ksLineSeg(616.468507, 190, 618.468507, 190, 1);
            sketchEdit.ksArcByPoint(617.468507, 185, 5.099019513593, 612.468507, 186, 616.468507, 190, -1, 1);
            sketchEdit.ksArcByPoint(617.468507, 185, 5.099019513593, 618.468507, 190, 622.468507, 186, -1, 1);
            sketchEdit.ksArcByPoint(617.468507, 185, 5.099019513593, 622.468507, 184, 618.468507, 180, -1, 1);
            sketchEdit.ksArcByPoint(617.468507, 185, 5.099019513593, 616.468507, 180, 612.468507, 184, -1, 1);

            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 80, true, 0, false);

            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(-612.468507, -180, -600 + 400);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            // Правый нижний
            sketchEdit.ksArcByPoint(-210, 180, 25, -210, 155, -210, 205, -1, 1);
            sketchEdit.ksArcByPoint(-210, 180, 20, -210, 160, -210, 200, -1, 1);
            sketchEdit.ksLineSeg(-210, 205, -210, 200, 1);
            sketchEdit.ksLineSeg(-210, 160, -210, 155, 1);

            // Правый верхний
            sketchEdit.ksArcByPoint(-210, -185, 25, -210, -160, -210, -210, 1, 1);
            sketchEdit.ksArcByPoint(-210, -185, 20, -210, -165, -210, -205, 1, 1);
            sketchEdit.ksLineSeg(-210, -210, -210, -205, 1);
            sketchEdit.ksLineSeg(-210, -165, -210, -160, 1);


            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 5, true, 0, false);
            ExtrudeSketch(_part, sketch, 15, false, 0, false);


            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(612.468507, -180, -600 + 400);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            // Левый нижний
            sketchEdit.ksArcByPoint(210, 180, 25, 210, 155, 210, 205, 1, 1);
            sketchEdit.ksArcByPoint(210, 180, 20, 210, 160, 210, 200, 1, 1);
            sketchEdit.ksLineSeg(210, 205, 210, 200, 1);
            sketchEdit.ksLineSeg(210, 160, 210, 155, 1);

            // Левый верхний
            sketchEdit.ksArcByPoint(210, -185, 25, 210, -160, 210, -210, -1, 1);
            sketchEdit.ksArcByPoint(210, -185, 20, 210, -165, 210, -205, -1, 1);
            sketchEdit.ksLineSeg(210, -210, 210, -205, 1);
            sketchEdit.ksLineSeg(210, -165, 210, -160, 1);

            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 5, true, 0, false);
            ExtrudeSketch(_part, sketch, 15, false, 0, false);

            // Начальная часть батареи бластера
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(-617.468507, 185, -600 - 300 + 30);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(-617.468507, -180, 20, 1);
            sketchEdit.ksCircle(617.468507, -180, 20, 1);
            sketchEdit.ksCircle(-617.468507, 185, 20, 1);
            sketchEdit.ksCircle(617.468507, 185, 20, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 15, true, -5, false);

            // Средняя часть батареи бластера
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(-617.468507, 185, -600 - 300 + 30 - 15);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(-617.468507, -180, 18.5, 1);
            sketchEdit.ksCircle(617.468507, -180, 18.5, 1);
            sketchEdit.ksCircle(-617.468507, 185, 18.5, 1);
            sketchEdit.ksCircle(617.468507, 185, 18.5, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 10, true, 15, false);

            // Конечная часть батареи бластера
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(-617.468507, 185, -600 - 300 + 30 - 15 - 10);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(-617.468507, -180, 21, 1);
            sketchEdit.ksCircle(617.468507, -180, 21, 1);
            sketchEdit.ksCircle(-617.468507, 185, 21, 1);
            sketchEdit.ksCircle(617.468507, 185, 21, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 10, true, 0, false);

            // Вырез в бластере
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(-641.968507, 185, -600);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            //правый верхний
            sketchEdit.ksCircle(-617.468507, 185, 24, 1);
            sketchEdit.ksCircle(-617.468507, 185, 15, 1);
            //правый нижний
            sketchEdit.ksCircle(-617.468507, -180, 24, 1);
            sketchEdit.ksCircle(-617.468507, -180, 15, 1);
            //левый верхний
            sketchEdit.ksCircle(617.468507, 185, 24, 1);
            sketchEdit.ksCircle(617.468507, 185, 15, 1);
            //левый нижний
            sketchEdit.ksCircle(617.468507, -180, 24, 1);
            sketchEdit.ksCircle(617.468507, -180, 15, 1);
            definition.EndEdit();
            CutExtrusion(_part, sketch, 10, 0, true);


            // Тонкие линии в углублении корпуса бластера.
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(-641.968507, 185, -600);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();

            sketchEdit.ksCircle(-617.468507, 185, 24, 1);
            sketchEdit.ksCircle(-617.468507, 185, 15, 1);
            sketchEdit.ksLineSeg(-634.439069748477, 201.970562748477, -628.075108717798, 195.606601717798, 1);
            sketchEdit.ksLineSeg(-617.468507, 209, -617.468507, 200, 1);
            sketchEdit.ksLineSeg(-600.497944251523, 201.970562748477, -606.861905282202, 195.606601717798, 1);
            sketchEdit.ksLineSeg(-593.468507, 185, -602.468507, 185, 1);
            sketchEdit.ksLineSeg(-600.497944251523, 168.029437251523, -606.861905282202, 174.393398282202, 1);
            sketchEdit.ksLineSeg(-617.468507, 161, -617.468507, 170, 1);
            sketchEdit.ksLineSeg(-634.439069748477, 168.029437251523, -628.075108717798, 174.393398282202, 1);
            sketchEdit.ksLineSeg(-641.468507, 185, -632.468507, 185, 1);


            sketchEdit.ksCircle(-617.468507, -180, 24, 1);
            sketchEdit.ksCircle(-617.468507, -180, 15, 1);
            sketchEdit.ksLineSeg(-634.439069748477, -163.029437251523, -628.075108717798, -169.393398282202, 1);
            sketchEdit.ksLineSeg(-617.468507, -156, -617.468507, -165, 1);
            sketchEdit.ksLineSeg(-600.497944251523, -163.029437251523, -606.861905282202, -169.393398282202, 1);
            sketchEdit.ksLineSeg(-593.468507, -180, -602.468507, -180, 1);
            sketchEdit.ksLineSeg(-600.497944251523, -196.970562748477, -606.861905282202, -190.606601717798, 1);
            sketchEdit.ksLineSeg(-617.468507, -204, -617.468507, -195, 1);
            sketchEdit.ksLineSeg(-634.439069748477, -196.970562748477, -628.075108717798, -190.606601717798, 1);
            sketchEdit.ksLineSeg(-641.468507, -180, -632.468507, -180, 1);


            sketchEdit.ksCircle(617.468507, 185, 24, 1);
            sketchEdit.ksCircle(617.468507, 185, 15, 1);
            sketchEdit.ksLineSeg(634.439069748477, 201.970562748477, 628.075108717798, 195.606601717798, 1);
            sketchEdit.ksLineSeg(617.468507, 209, 617.468507, 200, 1);
            sketchEdit.ksLineSeg(600.497944251523, 201.970562748477, 606.861905282202, 195.606601717798, 1);
            sketchEdit.ksLineSeg(593.468507, 185, 602.468507, 185, 1);
            sketchEdit.ksLineSeg(600.497944251523, 168.029437251523, 606.861905282202, 174.393398282202, 1);
            sketchEdit.ksLineSeg(617.468507, 161, 617.468507, 170, 1);
            sketchEdit.ksLineSeg(634.439069748477, 168.029437251523, 628.075108717798, 174.393398282202, 1);
            sketchEdit.ksLineSeg(641.468507, 185, 632.468507, 185, 1);


            sketchEdit.ksCircle(617.468507, -180, 24, 1);
            sketchEdit.ksCircle(617.468507, -180, 15, 1);
            sketchEdit.ksLineSeg(634.439069748477, -163.029437251523, 628.075108717798, -169.393398282202, 1);
            sketchEdit.ksLineSeg(617.468507, -156, 617.468507, -165, 1);
            sketchEdit.ksLineSeg(600.497944251523, -163.029437251523, 606.861905282202, -169.393398282202, 1);
            sketchEdit.ksLineSeg(593.468507, -180, 602.468507, -180, 1);
            sketchEdit.ksLineSeg(600.497944251523, -196.970562748477, 606.861905282202, -190.606601717798, 1);
            sketchEdit.ksLineSeg(617.468507, -204, 617.468507, -195, 1);
            sketchEdit.ksLineSeg(634.439069748477, -196.970562748477, 628.075108717798, -190.606601717798, 1);
            sketchEdit.ksLineSeg(641.468507, -180, 632.468507, -180, 1);

            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 10, true, 0, true);
        }

        /// <summary>
        /// Построение ускорителей.
        /// </summary>
        private void Accelerators()
        {
            // основание на крыле
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(-98.760456157148, 54.834961060082, -600);
            ksEntity plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksLineSeg(-69.238565314295, 55.792328504171, -216.326522495816, 85.20546931176, 1);
            sketchEdit.ksLineSeg(-69.238565314295, 55.792328504171, -54.395688959911, 78.493198176107, 1);
            sketchEdit.ksLineSeg(-54.395688959911, 78.493198176107, -39.142134225441, 111.653099683237, 1);
            sketchEdit.ksLineSeg(-39.142134225441, 111.653099683237, -208.910182359792, 95.073148929672, 1);
            sketchEdit.ksLineSeg(-208.910182359792, 95.073148929672, -216.326522495816, 85.20546931176, 1);


            sketchEdit.ksLineSeg(-64.739253678729, -54.42784643247, -49.470254859108, -71.493198059993, 1);
            sketchEdit.ksLineSeg(-49.470254859108, -71.493198059993, -34.204924615894, -104.678698835012, 1);
            sketchEdit.ksLineSeg(-64.739253678729, -54.42784643247, -216.699337266712, -82.993642172126, 1);
            sketchEdit.ksLineSeg(-216.699337266712, -82.993642172126, -211.874390428137, -90.591002766762, 1);
            sketchEdit.ksLineSeg(-211.874390428137, -90.591002766762, -34.204924615894, -104.678698835012, 1);

            sketchEdit.ksLineSeg(69.238565314295, 55.792328504171, 216.326522495816, 85.20546931176, 1);
            sketchEdit.ksLineSeg(69.238565314295, 55.792328504171, 54.395688959911, 78.493198176107, 1);
            sketchEdit.ksLineSeg(54.395688959911, 78.493198176107, 39.142134225441, 111.653099683237, 1);
            sketchEdit.ksLineSeg(39.142134225441, 111.653099683237, 208.910182359792, 95.073148929672, 1);
            sketchEdit.ksLineSeg(208.910182359792, 95.073148929672, 216.326522495816, 85.20546931176, 1);


            sketchEdit.ksLineSeg(64.739253678729, -54.42784643247, 49.470254859108, -71.493198059993, 1);
            sketchEdit.ksLineSeg(49.470254859108, -71.493198059993, 34.204924615894, -104.678698835012, 1);
            sketchEdit.ksLineSeg(64.739253678729, -54.42784643247, 216.699337266712, -82.993642172126, 1);
            sketchEdit.ksLineSeg(216.699337266712, -82.993642172126, 211.874390428137, -90.591002766762, 1);
            sketchEdit.ksLineSeg(211.874390428137, -90.591002766762, 34.204924615894, -104.678698835012, 1);

            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 280, false, 0, false);

            // Турбины
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(121.217288365422, 85.28191092109, -600);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(112.667829571579, 104.472405663619, 40, 1);
            sketchEdit.ksCircle(105.609456757216, -99.016919293413, 40, 1);
            sketchEdit.ksCircle(-112.667829571579, 104.472405663619, 40, 1);
            sketchEdit.ksCircle(-105.609456757216, -99.016919293413, 40, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 200, false, 0, false);
            ExtrudeSketch(_part, sketch, 30, true, 0, false);

            // Основной вырез в Турбины
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();


            sketchEdit = (ksDocument2D)definition.BeginEdit();
            // левая верхняя
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 15, 97.673226100878, 104.874732342367, 126.620543976279, 109.978925502944, -1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 35, 77.805465245013, 101.371510057214, 146.488304832143, 113.482147788097, -1, 1);
            sketchEdit.ksLineSeg(77.805465245013, 101.371510057214, 97.673226100878, 104.874732342367, 1);
            sketchEdit.ksLineSeg(126.620543976279, 109.978925502944, 146.488304832143, 113.482147788097, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 35.000000000001, 78.199558216151, 98.394719445276, 147.136100927007, 110.550091881962, 1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 15, 97.895713276396, 101.867682998615, 127.439945866762, 107.077128328623, 1, 1);
            sketchEdit.ksLineSeg(97.895713276396, 101.867682998615, 78.199558216151, 98.394719445276, 1);
            sketchEdit.ksLineSeg(127.439945866762, 107.077128328623, 147.136100927007, 110.550091881962, 1);
            // правая верхняя
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 35, -147.136100927007, 110.550091881962, -78.199558216151, 98.394719445276, 1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 35, -146.488304832143, 113.482147788097, -77.805465245013, 101.371510057214, -1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 15, -127.439945866762, 107.077128328623, -97.895713276396, 101.867682998615, 1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 15, -126.620543976279, 109.978925502944, -97.673226100878, 104.874732342367, -1, 1);
            sketchEdit.ksLineSeg(-146.488304832143, 113.482147788097, -126.620543976279, 109.978925502944, 1);
            sketchEdit.ksLineSeg(-147.136100927007, 110.550091881962, -127.439945866762, 107.077128328623, 1);
            sketchEdit.ksLineSeg(-97.673226100878, 104.874732342367, -77.805465245013, 101.371510057214, 1);
            sketchEdit.ksLineSeg(-97.895713276396, 101.867682998615, -78.199558216151, 98.394719445276, 1);

            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 35.000000000001, 70.74709243065, -95.916023687008, 139.429932017781, -108.026661417891, 1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 15, 90.614853286515, -99.419245972161, 119.562171161916, -104.523439132739, 1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 35, 71.141185401788, -92.93923307507, 140.077728112644, -105.094605511756, -1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 15, 90.837340462033, -96.412196628409, 120.381573052399, -101.621641958417, -1, 1);
            sketchEdit.ksLineSeg(71.141185401788, -92.93923307507, 90.837340462033, -96.412196628409, 1);
            sketchEdit.ksLineSeg(70.74709243065, -95.916023687008, 90.614853286515, -99.419245972161, 1);
            sketchEdit.ksLineSeg(120.381573052399, -101.621641958417, 140.077728112644, -105.094605511756, 1);
            sketchEdit.ksLineSeg(119.562171161916, -104.523439132739, 139.429932017781, -108.026661417891, 1);

            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 35.000000000001, -140.077728112644, -105.094605511756, -71.141185401788, -92.93923307507, -1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 35, -139.42993201778, -108.026661417891, -70.74709243065, -95.916023687008, 1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 15, -120.381573052399, -101.621641958417, -90.837340462033, -96.412196628409, -1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 15, -119.562171161916, -104.523439132738, -90.614853286514, -99.419245972161, 1, 1);
            sketchEdit.ksLineSeg(-140.077728112644, -105.094605511756, -120.381573052399, -101.621641958417, 1);
            sketchEdit.ksLineSeg(-90.837340462033, -96.412196628409, -71.141185401788, -92.93923307507, 1);
            sketchEdit.ksLineSeg(-139.42993201778, -108.026661417891, -119.562171161916, -104.523439132738, 1);
            sketchEdit.ksLineSeg(-90.614853286514, -99.419245972161, -70.74709243065, -95.916023687008, 1);

            definition.EndEdit();
            CutExtrusion(_part, sketch, 150, 0, true);


            // срез нижней части турбины
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            //
            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 40, 73.275519451091, 97.526478556942, 152.060139692067, 111.418332770296, 1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 35.000000000001, 78.199558216151, 98.394719445276, 147.136100927007, 110.550091881962, 1, 1);
            sketchEdit.ksLineSeg(78.199558216151, 98.394719445276, 73.275519451091, 97.526478556942, 1);
            sketchEdit.ksLineSeg(147.136100927007, 110.550091881962, 152.060139692067, 111.418332770296, 1);
            //
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 40, -152.060139692067, 111.418332770296, -73.275519451091, 97.526478556942, 1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 35, -147.136100927007, 110.550091881962, -78.199558216151, 98.394719445276, 1, 1);
            sketchEdit.ksLineSeg(-147.136100927007, 110.550091881962, -152.060139692067, 111.418332770296, 1);
            sketchEdit.ksLineSeg(-78.199558216151, 98.394719445276, -73.275519451091, 97.526478556942, 1);

            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 40, 66.217146636728, -92.070992186736, 145.001766877704, -105.96284640009, -1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 35, 71.141185401788, -92.93923307507, 140.077728112644, -105.094605511756, -1, 1);
            sketchEdit.ksLineSeg(66.217146636728, -92.070992186736, 71.141185401788, -92.93923307507, 1);
            sketchEdit.ksLineSeg(140.077728112644, -105.094605511756, 145.001766877704, -105.96284640009, 1);


            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 40, -145.001766877704, -105.96284640009, -66.217146636728, -92.070992186736, -1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 35.000000000001, -140.077728112644, -105.094605511756, -71.141185401788, -92.93923307507, -1, 1);
            sketchEdit.ksLineSeg(-140.077728112644, -105.094605511756, -145.001766877704, -105.96284640009, 1);
            sketchEdit.ksLineSeg(-71.141185401788, -92.93923307507, -66.217146636728, -92.070992186736, 1);

            definition.EndEdit();
            CutExtrusion(_part, sketch, 10, 0, true);


            // Срез среднего круга турбины
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();


            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 15, 97.895713276396, 101.867682998615, 127.439945866762, 107.077128328623, 1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 15, 97.673226100878, 104.874732342367, 126.620543976279, 109.978925502944, -1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 10, 102.752417824, 105.770330883344, 121.541352253157, 109.083326961967, -1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 10, 102.819752041457, 102.73592388695, 122.515907101701, 106.208887440288, 1, 1);
            sketchEdit.ksLineSeg(102.752417824, 105.770330883344, 97.673226100878, 104.874732342367, 1);
            sketchEdit.ksLineSeg(121.541352253157, 109.083326961967, 126.620543976279, 109.978925502944, 1);
            sketchEdit.ksLineSeg(127.439945866762, 107.077128328623, 122.515907101701, 106.208887440288, 1);
            sketchEdit.ksLineSeg(102.819752041457, 102.73592388695, 97.895713276396, 101.867682998615, 1);

            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 15, -127.439945866762, 107.077128328623, -97.895713276396, 101.867682998615, 1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 15, -126.620543976279, 109.978925502944, -97.673226100878, 104.874732342367, -1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 10, -122.515907101701, 106.208887440288, -102.819752041457, 102.73592388695, 1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 10, -121.541352253156, 109.083326961967, -102.752417824001, 105.770330883344, -1, 1);
            sketchEdit.ksLineSeg(-126.620543976279, 109.978925502944, -121.541352253156, 109.083326961967, 1);
            sketchEdit.ksLineSeg(-127.439945866762, 107.077128328623, -122.515907101701, 106.208887440288, 1);
            sketchEdit.ksLineSeg(-102.752417824001, 105.770330883344, -97.673226100878, 104.874732342367, 1);
            sketchEdit.ksLineSeg(-102.819752041457, 102.73592388695, -97.895713276396, 101.867682998615, 1);

            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 15, 90.614853286515, -99.419245972161, 119.562171161916, -104.523439132739, 1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 15, 90.837340462033, -96.412196628409, 120.381573052399, -101.621641958417, -1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 10, 95.761379227093, -97.280437516744, 115.457534287338, -100.753401070082, -1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 10, 95.694045009638, -100.314844513139, 114.482979438792, -103.627840591761, 1, 1);
            sketchEdit.ksLineSeg(90.837340462033, -96.412196628409, 95.761379227093, -97.280437516744, 1);
            sketchEdit.ksLineSeg(90.614853286515, -99.419245972161, 95.694045009638, -100.314844513139, 1);
            sketchEdit.ksLineSeg(115.457534287338, -100.753401070082, 120.381573052399, -101.621641958417, 1);
            sketchEdit.ksLineSeg(114.482979438792, -103.627840591761, 119.562171161916, -104.523439132739, 1);


            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 15, -120.381573052399, -101.621641958417, -90.837340462033, -96.412196628409, -1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 15, -90.614853286514, -99.419245972161, -119.562171161916, -104.523439132738, -1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 10, -115.457534287338, -100.753401070082, -95.761379227094, -97.280437516744, -1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 10, -114.482979438792, -103.627840591761, -95.694045009638, -100.314844513138, 1, 1);
            sketchEdit.ksLineSeg(-120.381573052399, -101.621641958417, -115.457534287338, -100.753401070082, 1);
            sketchEdit.ksLineSeg(-95.761379227094, -97.280437516744, -90.837340462033, -96.412196628409, 1);
            sketchEdit.ksLineSeg(-90.614853286514, -99.419245972161, -95.694045009638, -100.314844513138, 1);
            sketchEdit.ksLineSeg(-114.482979438792, -103.627840591761, -119.562171161916, -104.523439132738, 1);

            definition.EndEdit();
            CutExtrusion(_part, sketch, 25, 0, true);

            // Срез малого круга турбины
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();


            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 9.999999999999, 102.752417824002, 105.770330883345, 121.541352253155, 109.083326961966, -1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 10, 102.819752041457, 102.73592388695, 122.515907101701, 106.208887440288, 1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 5, 108.20765402653, 106.732236211988, 116.086116050627, 108.121421633323, -1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 5, 107.743790806518, 103.604164775284, 117.59186833664, 105.340646551954, 1, 1);
            sketchEdit.ksLineSeg(108.20765402653, 106.732236211988, 102.752417824002, 105.770330883345, 1);
            sketchEdit.ksLineSeg(116.086116050627, 108.121421633323, 121.541352253155, 109.083326961966, 1);
            sketchEdit.ksLineSeg(122.515907101701, 106.208887440288, 117.59186833664, 105.340646551954, 1);
            sketchEdit.ksLineSeg(107.743790806518, 103.604164775284, 102.819752041457, 102.73592388695, 1);

            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 5, -116.086116050627, 108.121421633323, -108.207654026529, 106.732236211988, -1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 5, -117.59186833664, 105.340646551954, -107.743790806518, 103.604164775284, 1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 10, -121.541352253156, 109.083326961967, -102.752417824001, 105.770330883344, -1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 10, -122.515907101701, 106.208887440288, -102.819752041457, 102.73592388695, 1, 1);
            sketchEdit.ksLineSeg(-116.086116050627, 108.121421633323, -121.541352253156, 109.083326961967, 1);
            sketchEdit.ksLineSeg(-108.207654026529, 106.732236211988, -102.752417824001, 105.770330883344, 1);
            sketchEdit.ksLineSeg(-117.59186833664, 105.340646551954, -122.515907101701, 106.208887440288, 1);
            sketchEdit.ksLineSeg(-107.743790806518, 103.604164775284, -102.819752041457, 102.73592388695, 1);

            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 10, 95.761379227093, -97.280437516744, 115.457534287338, -100.753401070082, -1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 10, 95.694045009638, -100.314844513139, 114.482979438792, -103.627840591761, 1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 5, 100.685417992155, -98.148678405078, 110.533495522277, -99.885160181748, -1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 5, 101.149281212166, -101.276749841782, 109.027743236264, -102.665935263118, 1, 1);
            sketchEdit.ksLineSeg(95.761379227093, -97.280437516744, 100.685417992155, -98.148678405078, 1);
            sketchEdit.ksLineSeg(110.533495522277, -99.885160181748, 115.457534287338, -100.753401070082, 1);
            sketchEdit.ksLineSeg(95.694045009638, -100.314844513139, 101.149281212166, -101.276749841782, 1);
            sketchEdit.ksLineSeg(109.027743236264, -102.665935263118, 114.482979438792, -103.627840591761, 1);


            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 10, -115.457534287338, -100.753401070082, -95.761379227094, -97.280437516744, -1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 10, -114.482979438792, -103.627840591761, -95.694045009638, -100.314844513138, 1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 5, -110.533495522277, -99.885160181748, -100.685417992155, -98.148678405078, -1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 5, -109.027743236264, -102.665935263117, -101.149281212166, -101.276749841782, 1, 1);
            sketchEdit.ksLineSeg(-115.457534287338, -100.753401070082, -110.533495522277, -99.885160181748, 1);
            sketchEdit.ksLineSeg(-100.685417992155, -98.148678405078, -95.761379227094, -97.280437516744, 1);
            sketchEdit.ksLineSeg(-95.694045009638, -100.314844513138, -101.149281212166, -101.276749841782, 1);
            sketchEdit.ksLineSeg(-109.027743236264, -102.665935263117, -114.482979438792, -103.627840591761, 1);

            definition.EndEdit();
            CutExtrusion(_part, sketch, 5, 0, true);


            //Палка в турбине
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();


            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksLineSeg(105.607816794944, 138.752957286861, 110.83232295847, 109.123310465185, 1);
            sketchEdit.ksLineSeg(107.577432300969, 139.100253642195, 112.801938464494, 109.47060682052, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 35, 105.607816794944, 138.752957286861, 107.577432300969, 139.100253642195, -1, 1);
            sketchEdit.ksArcByPoint(112.667829571579, 104.472405663619, 5, 110.83232295847, 109.123310465185, 112.801938464494, 109.470606820519, -1, 1);

            sketchEdit.ksLineSeg(-112.801938464495, 109.470606820519, -107.577432300971, 139.100253642195, 1);
            sketchEdit.ksLineSeg(-105.607816794946, 138.752957286862, -110.832322958471, 109.123310465186, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 35, -107.577432300971, 139.100253642195, -105.607816794946, 138.752957286862, -1, 1);
            sketchEdit.ksArcByPoint(-112.667829571579, 104.472405663619, 5, -112.801938464495, 109.470606820519, -110.832322958471, 109.123310465186, -1, 1);


            sketchEdit.ksLineSeg(103.773950144107, -103.66782409498, 98.549443980583, -133.297470916656, 1);
            sketchEdit.ksLineSeg(100.519059486607, -133.64476727199, 105.743565650132, -104.015120450314, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 5, 103.773950144107, -103.66782409498, 105.743565650132, -104.015120450314, 1, 1);
            sketchEdit.ksArcByPoint(105.609456757216, -99.016919293413, 35.000000000001, 98.549443980583, -133.297470916656, 100.519059486607, -133.64476727199, 1, 1);


            sketchEdit.ksLineSeg(-105.743565650132, -104.015120450313, -100.519059486608, -133.64476727199, 1);
            sketchEdit.ksLineSeg(-98.549443980583, -133.297470916656, -103.773950144108, -103.667824094979, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 5, -105.743565650132, -104.015120450313, -103.773950144108, -103.667824094979, 1, 1);
            sketchEdit.ksArcByPoint(-105.609456757216, -99.016919293413, 35, -100.519059486608, -133.64476727199, -98.549443980583, -133.297470916656, 1, 1);

            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 150, false, 0, false);

            // Задняя часть Турбины
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 33, 1);
            sketchEdit.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 33, 1);
            sketchEdit.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 33, 1);
            sketchEdit.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 33, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 150, true, 0, false);


            // Верхняя часть сопла
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 33, 1);
            sketchEdit.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 33, 1);
            sketchEdit.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 33, 1);
            sketchEdit.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 33, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 50, true, 10, false);


            // Нижняя часть сопла
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150 - 50);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 41.82, 1);
            sketchEdit.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 41.82, 1);
            sketchEdit.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 41.82, 1);
            sketchEdit.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 41.82, 1);
            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 50, true, -5, false);

            // Вырез сопла
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150 - 50 - 50);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 33, 1);
            sketchEdit.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 33, 1);
            sketchEdit.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 33, 1);
            sketchEdit.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 33, 1);
            sketchEdit.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 15, 1);
            sketchEdit.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 15, 1);
            sketchEdit.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 15, 1);
            sketchEdit.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 15, 1);
            definition.EndEdit();
            CutExtrusion(_part, sketch, 50, 0, true);

            // Внутренний Вырез сопла
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150 - 50 - 50);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 12.5, 1);
            sketchEdit.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 12.5, 1);
            sketchEdit.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 12.5, 1);
            sketchEdit.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 12.5, 1);
            definition.EndEdit();
            CutExtrusion(_part, sketch, 50, 0, true);

            // Рисунок сопла
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            definition = (ksSketchDefinition)sketch.GetDefinition();
            collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150 - 50);
            plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();

            sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 15, 1);
            sketchEdit.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 33, 1);
            sketchEdit.ksLineSeg(-109.667829571579, 140.472405663619, -109.667829571579, 122.472405663619, 1);
            sketchEdit.ksLineSeg(-133.002353350735, 130.806929442775, -120.274431289377, 118.079007381417, 1);
            sketchEdit.ksLineSeg(-142.667829571579, 107.472405663619, -124.667829571579, 107.472405663619, 1);
            sketchEdit.ksLineSeg(-133.002353350735, 84.137881884463, -120.274431289377, 96.865803945821, 1);
            sketchEdit.ksLineSeg(-109.667829571579, 74.472405663619, -109.667829571579, 92.472405663619, 1);
            sketchEdit.ksLineSeg(-86.333305792423, 84.137881884463, -99.061227853781, 96.865803945821, 1);
            sketchEdit.ksLineSeg(-76.667829571579, 107.472405663619, -94.667829571579, 107.472405663619, 1);
            sketchEdit.ksLineSeg(-86.333305792423, 130.806929442775, -99.061227853781, 118.079007381417, 1);

            sketchEdit.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 33, 1);
            sketchEdit.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 15, 1);
            sketchEdit.ksLineSeg(-79.27493297806, -78.682395514257, -92.002855039418, -91.410317575615, 1);
            sketchEdit.ksLineSeg(-102.609456757216, -69.016919293413, -102.609456757216, -87.016919293413, 1);
            sketchEdit.ksLineSeg(-125.943980536372, -78.682395514257, -113.216058475014, -91.410317575615, 1);
            sketchEdit.ksLineSeg(-135.609456757216, -102.016919293413, -117.609456757216, -102.016919293413, 1);
            sketchEdit.ksLineSeg(-125.943980536372, -125.351443072569, -113.216058475014, -112.623521011211, 1);
            sketchEdit.ksLineSeg(-102.609456757216, -135.016919293413, -102.609456757216, -117.016919293413, 1);
            sketchEdit.ksLineSeg(-79.27493297806, -125.351443072569, -92.002855039418, -112.623521011211, 1);
            sketchEdit.ksLineSeg(-69.609456757216, -102.016919293413, -87.609456757216, -102.016919293413, 1);


            sketchEdit.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 33, 1);
            sketchEdit.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 15, 1);
            sketchEdit.ksLineSeg(109.667829571579, 140.472405663619, 109.667829571579, 122.472405663619, 1);
            sketchEdit.ksLineSeg(133.002353350735, 130.806929442775, 120.274431289377, 118.079007381417, 1);
            sketchEdit.ksLineSeg(142.667829571579, 107.472405663619, 124.667829571579, 107.472405663619, 1);
            sketchEdit.ksLineSeg(133.002353350735, 84.137881884463, 120.274431289377, 96.865803945821, 1);
            sketchEdit.ksLineSeg(109.667829571579, 74.472405663619, 109.667829571579, 92.472405663619, 1);
            sketchEdit.ksLineSeg(86.333305792423, 84.137881884463, 99.061227853781, 96.865803945821, 1);
            sketchEdit.ksLineSeg(76.667829571579, 107.472405663619, 94.667829571579, 107.472405663619, 1);
            sketchEdit.ksLineSeg(86.333305792423, 130.806929442775, 99.061227853781, 118.079007381417, 1);

            sketchEdit.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 33, 1);
            sketchEdit.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 15, 1);
            sketchEdit.ksLineSeg(79.27493297806, -78.682395514257, 92.002855039418, -91.410317575615, 1);
            sketchEdit.ksLineSeg(102.609456757216, -69.016919293413, 102.609456757216, -87.016919293413, 1);
            sketchEdit.ksLineSeg(125.943980536372, -78.682395514257, 113.216058475014, -91.410317575615, 1);
            sketchEdit.ksLineSeg(135.609456757216, -102.016919293413, 117.609456757216, -102.016919293413, 1);
            sketchEdit.ksLineSeg(125.943980536372, -125.351443072569, 113.216058475014, -112.623521011211, 1);
            sketchEdit.ksLineSeg(102.609456757216, -135.016919293413, 102.609456757216, -117.016919293413, 1);
            sketchEdit.ksLineSeg(79.27493297806, -125.351443072569, 92.002855039418, -112.623521011211, 1);
            sketchEdit.ksLineSeg(69.609456757216, -102.016919293413, 87.609456757216, -102.016919293413, 1);

            definition.EndEdit();
            ExtrudeSketch(_part, sketch, 50, true, 0, true);
        }

        /// <summary>
        /// Построение головы робота.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Массив координат центра плоскости.</param>
        private void BuildDroidHead(double[] centerPlaneCoordinates)
        {
            // Построение эскиза полуокружности с осью вращения.
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(
                centerPlaneCoordinates[0],
                centerPlaneCoordinates[1],
                centerPlaneCoordinates[2]);
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
        /// <param name="centerPlaneCoordinates">Массив координат центра плоскости.</param>
        private void BuildBackDrawing(double[] centerPlaneCoordinates)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(
                centerPlaneCoordinates[0],
                centerPlaneCoordinates[1],
                centerPlaneCoordinates[2]);
            ksEntity plane = collection.First();
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            sketchEdit.ksLineSeg(-20, 40, -20, -100, 1);
            sketchEdit.ksLineSeg(20, -100, 20, 40, 1);
            sketchEdit.ksLineSeg(20, 40, -20, 40, 1);
            sketchEdit.ksLineSeg(0, 40, 0, 123, 1);
            sketchEdit.ksLineSeg(0, 75, 49, 75, 1);
            sketchEdit.ksLineSeg(0, 75, -49, 75, 1);
            sketchEdit.ksLineSeg(-20, 0, -94, 0, 1);
            sketchEdit.ksLineSeg(20, 0, 94, 0, 1);
            sketchEdit.ksLineSeg(-68, -40, -20, -40, 1);
            sketchEdit.ksLineSeg(68, -40, 20, -40, 1);
            sketchEdit.ksLineSeg(-20, 25, -79, 25, 1);
            sketchEdit.ksLineSeg(20, 25, 79, 25, 1);
            sketchEdit.ksLineSeg(0, -20, 0, -100, 1);
            definition.EndEdit();

            ExtrudeSketch(_part, sketch, 10, true, 0, true);

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
        /// Создание эскиза по выбранной плоскости.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Массив координат центра плоскости.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <param name="verticesNumber">Количество вершин многоугольника.</param>
        /// <returns></returns>
        private ksEntity CreateSketchByPoint(double[] centerPlaneCoordinates,
            double[,] polygonVertices, int verticesNumber)
        {
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(
               centerPlaneCoordinates[0],
               centerPlaneCoordinates[1],
               centerPlaneCoordinates[2]);
            ksEntity basePlane = collection.First();
            ksEntity skecth = CreatePolygonSkecth(basePlane,
                polygonVertices, verticesNumber);
            return skecth;
        }

        /// <summary>
        /// Создание эскиза по базовой плоскости.
        /// </summary>
        /// <param name="planeType">Базовая плоскость.</param>
        /// <param name="polygonVertices">Массив координат вершин многоугольника.</param>
        /// <param name="verticesNumber">Количество вершин многоугольника.</param>
        /// <returns></returns>
        private ksEntity CreateSketchByDefaultPlane(Obj3dType planeType,
            double[,] polygonVertices, int verticesNumber)
        {
            ksEntity basePlane = (ksEntity)_part.GetDefaultEntity((short)planeType);
            ksEntity skecth = CreatePolygonSkecth(basePlane,
            polygonVertices, verticesNumber);
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
            double[,] polygonVertices, int verticesNumber)
        {
            verticesNumber = verticesNumber - 1;
            ksEntity sketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            definition.SetPlane(plane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < verticesNumber; i++)
            {
                sketchEdit.ksLineSeg(polygonVertices[i, 0], polygonVertices[i, 1],
                polygonVertices[i + 1, 0], polygonVertices[i + 1, 1], 1);
            }
            sketchEdit.ksLineSeg(polygonVertices[verticesNumber, 0],
                polygonVertices[verticesNumber, 1], polygonVertices[0, 0],
                polygonVertices[0, 1], 1);
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание эскиза окружностей.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="circleParameters">Параметры окружности, включающие центр и радиус.</param>
        /// <returns></returns>
        private ksEntity CreateCirclesSketch(double[] centerPlaneCoordinates, double[,] circleParameters)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(
               centerPlaneCoordinates[0],
               centerPlaneCoordinates[1],
               centerPlaneCoordinates[2]);
            ksEntity basePlane = collection.First();
            definition.SetPlane(basePlane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < circleParameters.GetLength(0); i++)
            {
                sketchEdit.ksCircle(circleParameters[i, 0], circleParameters[i, 1],
                    circleParameters[i, 2], 1);
            }
            definition.EndEdit();
            return sketch;
        }

        /// <summary>
        /// Создание эскиза многоугольника с вырезом.
        /// Вырез - многоугольник с тем же числом вершин.
        /// </summary>
        /// <param name="centerPlaneCoordinates">Центр плоскости, на которой строится эскиз.</param>
        /// <param name="polygonCoordinates">Координаты многоугольника.</param>
        /// <returns></returns>
        private ksEntity CreatePolygonWithCutout(double[] centerPlaneCoordinates, double[,,] polygonCoordinates)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition definition = (ksSketchDefinition)sketch.GetDefinition();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_face);
            collection.SelectByPoint(
               centerPlaneCoordinates[0],
               centerPlaneCoordinates[1],
               centerPlaneCoordinates[2]);
            ksEntity basePlane = collection.First();
            definition.SetPlane(basePlane);
            sketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)definition.BeginEdit();
            for (int i = 0; i < polygonCoordinates.GetLength(0); i++)
            {
                for (int j = 0; j < polygonCoordinates.GetLength(1) - 1; j++)
                {
                    sketchEdit.ksLineSeg(polygonCoordinates[i, j, 0], polygonCoordinates[i, j, 1],
                        polygonCoordinates[i, j + 1, 0], polygonCoordinates[i, j + 1, 1], 1);
                }
                sketchEdit.ksLineSeg(polygonCoordinates[i, polygonCoordinates.GetLength(1) - 1, 0],
                    polygonCoordinates[i, polygonCoordinates.GetLength(1) - 1, 1],
                        polygonCoordinates[i, 0, 0], polygonCoordinates[i, 0, 1], 1);
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
        private void CreateChamfer(double shift, double[] chamferDiscance, double[] edgeCoordinate)
        {
            ksEntity sketch = _part.NewEntity((short)Obj3dType.o3d_chamfer);
            ksChamferDefinition definition = sketch.GetDefinition();
            definition.tangent = true;
            definition.SetChamferParam(true, chamferDiscance[0], chamferDiscance[1]);
            var array = definition.array();
            ksEntityCollection collection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            collection.SelectByPoint(edgeCoordinate[0], edgeCoordinate[1],
                edgeCoordinate[2] + shift);
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
        private void CreateFillet(double shift, double[,,] edgeCoordinatesArray, double radius)
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
                collection.SelectByPoint(edgeCoordinatesArray[i, 0, 0],
                    edgeCoordinatesArray[i, 0, 1],
                    edgeCoordinatesArray[i, 0, 2] + shift);
                ksEntity iEdge = collection.Last();
                array.Add(iEdge);
            }
            sketch.Create();
        }
    }
}
