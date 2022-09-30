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
    class XWingBuilder
    {
        /// <summary>
        /// Объект Компас API
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Деталь
        /// </summary>
        private ksPart _part;

        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="kompas">Объект Компас API</param>
        public XWingBuilder(KompasObject kompas)
        {
            _kompas = kompas;
            var document = (ksDocument3D)kompas.Document3D();

            document.Create();
        }

        /// <summary>
        /// Построение детали по заданным параметрам
        /// </summary>
        /// <param name="xWingParameters">Объект втулки</param>
        public void CreateDetail(XWingParameters xWingParameters)
        {
            double bodyLength = xWingParameters.BodyLength;
            double wingWidth = xWingParameters.WingWidth;
            double bowLength = xWingParameters.BowLength;
            double weaponBlasterTipLength = xWingParameters.WeaponBlasterTipLength;
            double acceleratorTurbineLength = xWingParameters.AcceleratorTurbineLength;
            double acceleratorNozzleLength = xWingParameters.AcceleratorNozzleLength;


            var document = (ksDocument3D)_kompas.ActiveDocument3D();


            _part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);

            _part.name = "X-Wing";

            _part.SetAdvancedColor(14211288, 0.5, 0.6, 0.8, 0.8, 1, 0.5);

            _part.Update();

            BowBody();

            Body();

            Wing();

            CutWing();

            BlasterBody();

            Accelerators();
        }

        /// <summary>
        /// Построение носовой части корпуса
        /// </summary>
        private void BowBody()
        {
            ksEntity entitySketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            ksEntity basePlane = (ksEntity)_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)entitySketch.GetDefinition();
            sketchDefinition.SetPlane(basePlane);
            entitySketch.Create();

            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.BeginEdit();

            sketchEdit.ksLineSeg(-43, 0, -26, 26, 1);
            sketchEdit.ksLineSeg(-26, 26, 26, 26, 1);
            sketchEdit.ksLineSeg(26, 26, 43, 0, 1);
            sketchEdit.ksLineSeg(43, 0, 26, -19, 1);
            sketchEdit.ksLineSeg(26, -19, -26, -19, 1);
            sketchEdit.ksLineSeg(-26, -19, -43, 0, 1);

            sketchDefinition.EndEdit();

            ExtrudeSketch(_part, entitySketch, 600, false, 5);
            ExtrudeSketch(_part, entitySketch, 50, true, -3);

            // Выдавливание кабины

            ksEntity iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            ksEntityCollection iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 52.246599057777, -300);
            ksEntity iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            ksDocument2D _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(54.395689029929, 604.557951837448, -54.395689029929, 604.557951837448, 1);
            _ksDocument2D.ksLineSeg(-54.395689029929, 604.557951837448, -26, 2.266049311439, 1);
            _ksDocument2D.ksLineSeg(-26, 2.266049311439, 26, 2.266049311439, 1);
            _ksDocument2D.ksLineSeg(26, 2.266049311439, 54.395689029929, 604.557951837448, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 50, true, 0);


            // Вырез кабины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 50.904867452294, 2.178893568691);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();

            _ksDocument2D.ksLineSeg(-26, 75.901062150385, -54.395689029929, 75.901062150385, 1);
            _ksDocument2D.ksLineSeg(-54.395689029929, 75.901062150385, -54.395689029929, 25.901062150385, 1);
            _ksDocument2D.ksLineSeg(-54.395689029929, 25.901062150385, -26, 75.901062150385, 1);
            _ksDocument2D.ksLineSeg(26, 75.901062150385, 54.395689029929, 75.901062150385, 1);
            _ksDocument2D.ksLineSeg(54.395689029929, 75.901062150385, 54.395689029929, 25.901062150385, 1);
            _ksDocument2D.ksLineSeg(54.395689029929, 25.901062150385, 26, 75.901062150385, 1);

            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 602.5, 0, true);

            // Срез кабины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-39.913887624665, 76.626534528915, -291.821106431309);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-1, 25.901062150385, -1, 75.901062150385, 1);
            _ksDocument2D.ksLineSeg(-1, 75.901062150385, -605.111616473601, 75.901062150386, 1);
            _ksDocument2D.ksLineSeg(-605.111616473601, 75.901062150386, -1, 25.901062150385, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 100, 0, true);


            // Острие носа
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 0, 50);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-43, 0, -26, 26, 1);
            _ksDocument2D.ksLineSeg(-26, 26, 25, 26, 1);
            _ksDocument2D.ksLineSeg(25, 26, 43, 0, 1);
            _ksDocument2D.ksLineSeg(43, 0, 26, -19, 1);
            _ksDocument2D.ksLineSeg(26, -19, -26, -19, 1);
            _ksDocument2D.ksLineSeg(-26, -19, -43, 0, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 100, true, -5);

            // Острие носа
            iSketch = _part.NewEntity((short)Obj3dType.o3d_chamfer);
            ksChamferDefinition Definition = iSketch.GetDefinition();

            Definition.tangent = true;
            Definition.SetChamferParam(true, 20, 54.949548389092);

            var iArray = Definition.array();

            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(-0.425689731596, 17.251133647408, 150);
            var iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iSketch.Create();

            // Острие носа
            iSketch = _part.NewEntity((short)Obj3dType.o3d_chamfer);
            Definition = iSketch.GetDefinition();

            Definition.tangent = true;
            Definition.SetChamferParam(true, 15, 55.980762113533);

            iArray = Definition.array();

            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(0, -10.251133647408, 150);
            iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iSketch.Create();

            // Прицеп острия к корпусу
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-40.42, 3.5, 50);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-24.5, 23.3, 24.5, 23.3, 1);
            _ksDocument2D.ksLineSeg(24.5, 23.3, 39.7, 0.2, 1);
            _ksDocument2D.ksLineSeg(39.7, 0.2, 24.8, -16.3, 1);
            _ksDocument2D.ksLineSeg(24.8, -16.3, -24.8, -16.3, 1);
            _ksDocument2D.ksLineSeg(-24.8, -16.3, -39.7, 0.2, 1);
            _ksDocument2D.ksLineSeg(-39.7, 0.2, -24.5, 23.3, 1);

            _ksDocument2D.ksLineSeg(-25, 26, -43, 0, 1);
            _ksDocument2D.ksLineSeg(-43, 0, -26, -19, 1);
            _ksDocument2D.ksLineSeg(-26, -19, 26, -19, 1);
            _ksDocument2D.ksLineSeg(26, -19, 43, 0, 1);
            _ksDocument2D.ksLineSeg(43, 0, 26, 26, 1);
            _ksDocument2D.ksLineSeg(26, 26, -25, 26, 1);

            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 35, true, -5);

            iSketch = _part.NewEntity((short)Obj3dType.o3d_fillet);
            ksFilletDefinition definition = iSketch.GetDefinition();
            definition.radius = 5;
            definition.tangent = false;
            iArray = definition.array();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(28.720056217208, -7.259103331883, 116.313406124592);
            iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(-28.755495846165, -7.196422527305, 116.489247908264);
            iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(27.634400064284, 11.74339370277, 117.997602962077);
            iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(-0.113051518725, 1.446485054632, 140.735654691904);
            iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(-28.21381908901, 11.74339370277, 117.997602962077);
            iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iSketch.Create();



            iSketch = _part.NewEntity((short)Obj3dType.o3d_fillet);
            definition = iSketch.GetDefinition();
            definition.radius = 10;
            definition.tangent = false;
            iArray = definition.array();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(-38.479522122003, 0.341549134121, 91.109212708249);
            iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_edge);
            iCollection.SelectByPoint(38.43038578768, 0.284493891452, 91.092916697691);
            iEdge = iCollection.Last();
            iArray.Add(iEdge);
            iSketch.Create();


        }

        /// <summary>
        /// Построение корпуса
        /// </summary>
        private void Body()
        {
            // Основное тело


            ksEntity iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            ksEntityCollection iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 0, -600);
            ksEntity iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            ksDocument2D _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(108.977589, -4.985001, 54.395689, 78.493198, 1);
            _ksDocument2D.ksLineSeg(54.395689, 78.493198, -54.395689, 78.493198, 1);
            _ksDocument2D.ksLineSeg(-54.395689, 78.493198, -108.977589, -4.985001, 1);
            _ksDocument2D.ksLineSeg(-108.977589, -4.985001, -49.470255, -71.493198, 1);
            _ksDocument2D.ksLineSeg(-49.470255, -71.493198, 49.470255, -71.493198, 1);
            _ksDocument2D.ksLineSeg(49.470255, -71.493198, 108.977589, -4.985001, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 300, true, 0);

            // Выдавленная часть сверху
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 78.493198, -750);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-54.395689, 900, 54.395689, 900, 1);
            _ksDocument2D.ksLineSeg(54.395689, 900, 54.395689, 600, 1);
            _ksDocument2D.ksLineSeg(54.395689, 600, -54.395689, 600, 1);
            _ksDocument2D.ksLineSeg(-54.395689, 600, -54.395689, 900, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 50, true, 0);

            // Срез верхней части
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 78.493198 + 25, -600);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-54.395689, 128.493198, -54.395689, 78.493198, 1);
            _ksDocument2D.ksLineSeg(-54.395689, 128.493198, -31.395689, 128.493198, 1);
            _ksDocument2D.ksLineSeg(-31.395689, 128.493198, -54.395689, 78.493198, 1);
            _ksDocument2D.ksLineSeg(54.395689, 128.493198, 54.395689, 78.493198, 1);
            _ksDocument2D.ksLineSeg(54.395689, 128.493198, 31.395689, 128.493198, 1);
            _ksDocument2D.ksLineSeg(31.395689, 128.493198, 54.395689, 78.493198, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 300, 0, true);

            // Выдавленная часть снизу
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, -71.493198, -750);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-49.470255, -900, 49.470255, -900, 1);
            _ksDocument2D.ksLineSeg(49.470255, -900, 49.470255, -600, 1);
            _ksDocument2D.ksLineSeg(49.470255, -600, -49.470255, -600, 1);
            _ksDocument2D.ksLineSeg(-49.470255, -600, -49.470255, -900, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 50, true, 0);

            // Выдавленная часть снизу
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-49.470255, -96.493198, -750);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            // Фаска
            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(600, 71.493198, 600, 121.493198, 1);
            _ksDocument2D.ksLineSeg(600, 121.493198, 650, 121.493198, 1);
            _ksDocument2D.ksLineSeg(650, 121.493198, 600, 71.493198, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 100, 0, true);


            // Срез нижней части
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, -71.493198 - 25, -600 - 300);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-49.470255, -71.493198 - 50, -49.470255, -71.493198, 1);
            _ksDocument2D.ksLineSeg(-49.470255, -71.493198 - 50, -49.470255 + 23, -71.493198 - 50, 1);
            _ksDocument2D.ksLineSeg(-49.470255 + 23, -71.493198 - 50, -49.470255, -71.493198, 1);
            _ksDocument2D.ksLineSeg(49.470255, -71.493198 - 50, 49.470255, -71.493198, 1);
            _ksDocument2D.ksLineSeg(49.470255, -71.493198 - 50, 49.470255 - 23, -71.493198 - 50, 1);
            _ksDocument2D.ksLineSeg(49.470255 - 23, -71.493198 - 50, 49.470255, -71.493198, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 300, 0, true);


            // Срез кабины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 103.398065567848, -597.821106431309);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(54.395689029929, 25.901062150386, 26.115362390053, 75.697928516383, 1);
            _ksDocument2D.ksLineSeg(26.115362390053, 75.697928516383, -26.000000000002, 75.901062150386, 1);
            _ksDocument2D.ksLineSeg(-26.000000000002, 75.901062150386, -54.395689029929, 25.901062150386, 1);
            _ksDocument2D.ksLineSeg(-54.395689029929, 25.901062150386, 54.395689029929, 25.901062150386, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 4.5, true, 0);



            // Углубление для верхней части корпуса
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 128.493198, -750);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-25, 607.659914486825, 25, 607.659914486825, 1);
            _ksDocument2D.ksLineSeg(25, 607.659914486825, 25, 892.659914486825, 1);
            _ksDocument2D.ksLineSeg(25, 892.659914486825, -25, 892.659914486825, 1);
            _ksDocument2D.ksLineSeg(-25, 892.659914486825, -25, 607.659914486825, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 5, 0, true);

            // Рельеф верхней части корпуса
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 118.493198 + 5, -750.159914486825);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(0.695976828008, 632.637269856963, 25, 1);
            _ksDocument2D.ksCircle(-0.203345439322, 632.637269856963, 25, 1);
            _ksDocument2D.ksCircle(-0.203345439322, 750.355637638365, 22, 1);
            _ksDocument2D.ksCircle(-0.203345439322, 750.355637638365, 19, 1);
            _ksDocument2D.ksLineSeg(-19.203345439322, 663.772206526744, 18.796654560678, 663.772206526744, 1);
            _ksDocument2D.ksLineSeg(18.796654560678, 663.772206526744, 18.796654560678, 721.772206526744, 1);
            _ksDocument2D.ksLineSeg(18.796654560678, 721.772206526744, -19.203345439322, 721.772206526744, 1);
            _ksDocument2D.ksLineSeg(-19.203345439322, 721.772206526744, -19.203345439322, 663.772206526744, 1);
            _ksDocument2D.ksLineSeg(-15.203345439322, 669.272206526744, 14.796654560678, 669.272206526744, 1);
            _ksDocument2D.ksLineSeg(14.796654560678, 669.272206526744, 14.796654560678, 716.272206526744, 1);
            _ksDocument2D.ksLineSeg(14.796654560678, 716.272206526744, -15.203345439322, 716.272206526744, 1);
            _ksDocument2D.ksLineSeg(-15.203345439322, 716.272206526744, -15.203345439322, 669.272206526744, 1);
            _ksDocument2D.ksCircle(-0.203345439322, 706.466093921629, 8, 1);
            _ksDocument2D.ksCircle(-0.203345439322, 678.848141946151, 8, 1);
            _ksDocument2D.ksLineSeg(-19.203345439322, 858.526455211801, 18.796654560678, 858.526455211801, 1);
            _ksDocument2D.ksLineSeg(18.796654560678, 858.526455211801, 18.796654560678, 878.526455211801, 1);
            _ksDocument2D.ksLineSeg(18.796654560678, 878.526455211801, -19.203345439322, 878.526455211801, 1);
            _ksDocument2D.ksLineSeg(-19.203345439322, 878.526455211801, -19.203345439322, 858.526455211801, 1);

            _ksDocument2D.ksLineSeg(-15.203345439322, 861.526455211801, 14.796654560678, 861.526455211801, 1);
            _ksDocument2D.ksLineSeg(14.796654560678, 861.526455211801, 14.796654560678, 875.526455211801, 1);
            _ksDocument2D.ksLineSeg(14.796654560678, 875.526455211801, -15.203345439322, 875.526455211801, 1);
            _ksDocument2D.ksLineSeg(-15.203345439322, 875.526455211801, -15.203345439322, 861.526455211801, 1);



            _ksDocument2D.ksLineSeg(-22.203345439322, 784.715832632264, 21.796654560678, 784.715832632264, 1);
            _ksDocument2D.ksLineSeg(21.796654560678, 784.715832632264, 21.796654560678, 842.715832632264, 1);
            _ksDocument2D.ksLineSeg(21.796654560678, 842.715832632264, -22.203345439322, 842.715832632264, 1);
            _ksDocument2D.ksLineSeg(-22.203345439322, 842.715832632264, -22.203345439322, 784.715832632264, 1);

            _ksDocument2D.ksLineSeg(-19.203345439322, 788.215832632264, 18.796654560678, 788.215832632264, 1);
            _ksDocument2D.ksLineSeg(18.796654560678, 788.215832632264, 18.796654560678, 839.215832632264, 1);
            _ksDocument2D.ksLineSeg(18.796654560678, 839.215832632264, -19.203345439322, 839.215832632264, 1);
            _ksDocument2D.ksLineSeg(-19.203345439322, 839.215832632264, -19.203345439322, 788.215832632264, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 10, true, 0);




            // Голова дроида
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0.246315694343, 133.493198, -632.637269856963);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-0.246315694343, 657.633225627145, -0.246315694343, 607.641314086781, 3);
            _ksDocument2D.ksArcByPoint(0.203345439322, 632.637269856963, 25, -0.246315694343, 657.633225627145, -0.246315694343, 607.641314086781, 1, 1);
            iDefinition.EndEdit();


            ksEntity bossRotated = _part.NewEntity((short)Obj3dType.o3d_bossRotated);
            ksBossRotatedDefinition bossRotatedDef = bossRotated.GetDefinition();
            bossRotatedDef.directionType = (short)Direction_Type.dtNormal;
            bossRotatedDef.SetSketch(iSketch);
            bossRotatedDef.SetSideParam(true, 360);
            ksRotatedParam rotateParam = bossRotatedDef.RotatedParam();
            bossRotated.Create();


            // Углубление для верхней части корпуса
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 0, -600 - 300);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-97, -4, -20, 123, 1);
            _ksDocument2D.ksLineSeg(-20, 123, 20, 123, 1);
            _ksDocument2D.ksLineSeg(20, 123, 97, -4, 1);
            _ksDocument2D.ksLineSeg(97, -4, 20, -100, 1);
            _ksDocument2D.ksLineSeg(20, -100, -20, -100, 1);
            _ksDocument2D.ksLineSeg(-20, -100, -97, -4, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 10, 0, true);

            // Голова дроида
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 0, -600 - 300 + 10);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(0, 0, 20, 1);
            iDefinition.EndEdit();

            ExtrudeSketch(_part, iSketch, 10, true, 0);


            // Голова дроида
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(87.3, 11.5, -600 - 300 + 10);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-20, 40, -20, -100, 1);
            _ksDocument2D.ksLineSeg(20, -100, 20, 40, 1);
            _ksDocument2D.ksLineSeg(20, 40, -20, 40, 1);
            _ksDocument2D.ksLineSeg(0, 40, 0, 123, 1);
            _ksDocument2D.ksLineSeg(0, 75, 49, 75, 1);
            _ksDocument2D.ksLineSeg(0, 75, -49, 75, 1);
            _ksDocument2D.ksLineSeg(-20, 0, -94, 0, 1);
            _ksDocument2D.ksLineSeg(20, 0, 94, 0, 1);
            _ksDocument2D.ksLineSeg(-68, -40, -20, -40, 1);
            _ksDocument2D.ksLineSeg(68, -40, 20, -40, 1);
            _ksDocument2D.ksLineSeg(-20, 25, -79, 25, 1);
            _ksDocument2D.ksLineSeg(20, 25, 79, 25, 1);
            _ksDocument2D.ksLineSeg(0, -20, 0, -100, 1);
            iDefinition.EndEdit();

            ksEntity entityExtrusion = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_baseExtrusion);

            ksBaseExtrusionDefinition extrusionDefinition = (ksBaseExtrusionDefinition)entityExtrusion.GetDefinition();

            extrusionDefinition.directionType = (short)Direction_Type.dtNormal;

            extrusionDefinition.SetSideParam(true, (short)End_Type.etBlind, 10, 0);
            extrusionDefinition.SetThinParam(true, (short)End_Type.etBlind, 1, 0);

            extrusionDefinition.SetSketch(iSketch);
            entityExtrusion.Create();


        }

        /// <summary>
        /// Построение крыла
        /// </summary>
        private void Wing()
        {

            ksEntity iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            ksEntityCollection iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(0, 0, -600 - 300);
            ksEntity iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            ksDocument2D _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(69.173833, 55.779384, 78.023751, 42.034027, 1);
            _ksDocument2D.ksLineSeg(78.023751, 42.034027, 632.086192, 150.642161, 1);
            _ksDocument2D.ksLineSeg(632.086192, 150.642161, 639.549926, 169.837344, 1);
            _ksDocument2D.ksLineSeg(639.549926, 169.837344, 69.173833, 55.779384, 1);

            _ksDocument2D.ksLineSeg(-69.173833, 55.779384, -78.023751, 42.034027, 1);
            _ksDocument2D.ksLineSeg(-78.023751, 42.034027, -632.086192, 150.642161, 1);
            _ksDocument2D.ksLineSeg(-632.086192, 150.642161, -639.549926, 169.837344, 1);
            _ksDocument2D.ksLineSeg(-639.549926, 169.837344, -69.173833, 55.779384, 1);

            _ksDocument2D.ksLineSeg(77.853158, -39.308584, 64.171267, -54.321075, 1);
            _ksDocument2D.ksLineSeg(64.171267, -54.321075, 646.963505, -163.875661, 1);
            _ksDocument2D.ksLineSeg(646.963505, -163.875661, 640.123102, -145.081809, 1);
            _ksDocument2D.ksLineSeg(640.123102, -145.081809, 77.853158, -39.308584, 1);

            _ksDocument2D.ksLineSeg(-77.853158, -39.308584, -64.171267, -54.321075, 1);
            _ksDocument2D.ksLineSeg(-64.171267, -54.321075, -646.963505, -163.875661, 1);
            _ksDocument2D.ksLineSeg(-646.963505, -163.875661, -640.123102, -145.081809, 1);
            _ksDocument2D.ksLineSeg(-640.123102, -145.081809, -77.853158, -39.308584, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 300, false, 0);

        }

        /// <summary>
        /// Построения выреза на крыльях
        /// </summary>
        private void CutWing()
        {

            ksEntity entitySketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            ksEntity basePlane = (ksEntity)_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)entitySketch.GetDefinition();
            sketchDefinition.SetPlane(basePlane);
            entitySketch.Create();

            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.BeginEdit();

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
            CutExtrusion(_part, entitySketch, 200, 200, true);
        }

        /// <summary>
        /// Построение бластерного оружия
        /// </summary>
        private void BlasterBody()
        {

            // Основа тела бластера
            ksEntity iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            ksEntityCollection iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(98.760456157148, 54.834961060082, -600);
            ksEntity iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            ksDocument2D _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(-617.468507, -180, 25, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 25, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 25, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 25, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 270, false, 0);

            // Толская часть тела
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(98.760456157148, 54.834961060082, -600);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(-617.468507, -180, 13, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 13, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 13, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 13, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 200, true, 0);

            // Переход на узкую часть тела
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(617.468507, 185, -600 + 200);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(-617.468507, -180, 11, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 11, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 11, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 11, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 5, true, 0);

            // Узкая часть тела
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(617.468507, 185, -600 + 200 + 5);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(-617.468507, -180, 9, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 9, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 9, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 9, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 150, true, 0);


            // Переход на острие бластера
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(617.468507, 185, -600 + 200 + 5 + 150);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(-617.468507, -180, 15, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 15, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 15, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 15, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 5, true, 0);

            // Острие бластера
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(617.468507, 185, -600 + 200 + 5 + 150 + 5);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            // Правое нижнее Острие бластера
            _ksDocument2D.ksLineSeg(-622.468507, -179, -622.468507, -181, 1);
            _ksDocument2D.ksLineSeg(-612.468507, -179, -612.468507, -181, 1);
            _ksDocument2D.ksLineSeg(-616.468507, -175, -618.468507, -175, 1);
            _ksDocument2D.ksLineSeg(-616.468507, -185, -618.468507, -185, 1);
            _ksDocument2D.ksArcByPoint(-617.468507, -180, 5.099019513593, -612.468507, -181, -616.468507, -185, -1, 1);
            _ksDocument2D.ksArcByPoint(-617.468507, -180, 5.099019513593, -618.468507, -185, -622.468507, -181, -1, 1);
            _ksDocument2D.ksArcByPoint(-617.468507, -180, 5.099019513593, -622.468507, -179, -618.468507, -175, -1, 1);
            _ksDocument2D.ksArcByPoint(-617.468507, -180, 5.099019513593, -616.468507, -175, -612.468507, -179, -1, 1);
            // Левое нижнее Острие бластера
            _ksDocument2D.ksLineSeg(622.468507, -179, 622.468507, -181, 1);
            _ksDocument2D.ksLineSeg(612.468507, -179, 612.468507, -181, 1);
            _ksDocument2D.ksLineSeg(616.468507, -175, 618.468507, -175, 1);
            _ksDocument2D.ksLineSeg(616.468507, -185, 618.468507, -185, 1);
            _ksDocument2D.ksArcByPoint(617.468507, -180, 5.099019513593, 612.468507, -181, 616.468507, -185, 1, 1);
            _ksDocument2D.ksArcByPoint(617.468507, -180, 5.099019513593, 618.468507, -185, 622.468507, -181, 1, 1);
            _ksDocument2D.ksArcByPoint(617.468507, -180, 5.099019513593, 622.468507, -179, 618.468507, -175, 1, 1);
            _ksDocument2D.ksArcByPoint(617.468507, -180, 5.099019513593, 616.468507, -175, 612.468507, -179, 1, 1);
            // Правое вверхнее Острие бластера
            _ksDocument2D.ksLineSeg(-622.468507, 184, -622.468507, 186, 1);
            _ksDocument2D.ksLineSeg(-612.468507, 184, -612.468507, 186, 1);
            _ksDocument2D.ksLineSeg(-616.468507, 180, -618.468507, 180, 1);
            _ksDocument2D.ksLineSeg(-616.468507, 190, -618.468507, 190, 1);
            _ksDocument2D.ksArcByPoint(-617.468507, 185, 5.099019513593, -612.468507, 186, -616.468507, 190, 1, 1);
            _ksDocument2D.ksArcByPoint(-617.468507, 185, 5.099019513593, -618.468507, 190, -622.468507, 186, 1, 1);
            _ksDocument2D.ksArcByPoint(-617.468507, 185, 5.099019513593, -622.468507, 184, -618.468507, 180, 1, 1);
            _ksDocument2D.ksArcByPoint(-617.468507, 185, 5.099019513593, -616.468507, 180, -612.468507, 184, 1, 1);
            // Левое верхнее Острие бластера
            _ksDocument2D.ksLineSeg(622.468507, 184, 622.468507, 186, 1);
            _ksDocument2D.ksLineSeg(612.468507, 184, 612.468507, 186, 1);
            _ksDocument2D.ksLineSeg(616.468507, 180, 618.468507, 180, 1);
            _ksDocument2D.ksLineSeg(616.468507, 190, 618.468507, 190, 1);
            _ksDocument2D.ksArcByPoint(617.468507, 185, 5.099019513593, 612.468507, 186, 616.468507, 190, -1, 1);
            _ksDocument2D.ksArcByPoint(617.468507, 185, 5.099019513593, 618.468507, 190, 622.468507, 186, -1, 1);
            _ksDocument2D.ksArcByPoint(617.468507, 185, 5.099019513593, 622.468507, 184, 618.468507, 180, -1, 1);
            _ksDocument2D.ksArcByPoint(617.468507, 185, 5.099019513593, 616.468507, 180, 612.468507, 184, -1, 1);

            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 80, true, 0);


            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-612.468507, -180, -600 + 400);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            // Правый нижний
            _ksDocument2D.ksArcByPoint(-210, 180, 25, -210, 155, -210, 205, -1, 1);
            _ksDocument2D.ksArcByPoint(-210, 180, 20, -210, 160, -210, 200, -1, 1);
            _ksDocument2D.ksLineSeg(-210, 205, -210, 200, 1);
            _ksDocument2D.ksLineSeg(-210, 160, -210, 155, 1);

            // Правый верхний
            _ksDocument2D.ksArcByPoint(-210, -185, 25, -210, -160, -210, -210, 1, 1);
            _ksDocument2D.ksArcByPoint(-210, -185, 20, -210, -165, -210, -205, 1, 1);
            _ksDocument2D.ksLineSeg(-210, -210, -210, -205, 1);
            _ksDocument2D.ksLineSeg(-210, -165, -210, -160, 1);


            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 5, true, 0);

            ExtrudeSketch(_part, iSketch, 15, false, 0);


            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(612.468507, -180, -600 + 400);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            // Левый нижний
            _ksDocument2D.ksArcByPoint(210, 180, 25, 210, 155, 210, 205, 1, 1);
            _ksDocument2D.ksArcByPoint(210, 180, 20, 210, 160, 210, 200, 1, 1);
            _ksDocument2D.ksLineSeg(210, 205, 210, 200, 1);
            _ksDocument2D.ksLineSeg(210, 160, 210, 155, 1);

            // Левый верхний
            _ksDocument2D.ksArcByPoint(210, -185, 25, 210, -160, 210, -210, -1, 1);
            _ksDocument2D.ksArcByPoint(210, -185, 20, 210, -165, 210, -205, -1, 1);
            _ksDocument2D.ksLineSeg(210, -210, 210, -205, 1);
            _ksDocument2D.ksLineSeg(210, -165, 210, -160, 1);


            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 5, true, 0);
            ExtrudeSketch(_part, iSketch, 15, false, 0);

            // Начальная часть батареи бластера
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-617.468507, 185, -600 - 300 + 30);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(-617.468507, -180, 20, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 20, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 20, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 20, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 15, true, -5);

            // Средняя часть батареи бластера
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-617.468507, 185, -600 - 300 + 30 - 15);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(-617.468507, -180, 18.5, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 18.5, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 18.5, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 18.5, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 10, true, 15);

            // Конечная часть батареи бластера
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-617.468507, 185, -600 - 300 + 30 - 15 - 10);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(-617.468507, -180, 21, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 21, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 21, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 21, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 10, true, 0);



            // Вырез в бластере
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-641.968507, 185, -600);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            //правый верхний
            _ksDocument2D.ksCircle(-617.468507, 185, 24, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 15, 1);
            //правый нижний
            _ksDocument2D.ksCircle(-617.468507, -180, 24, 1);
            _ksDocument2D.ksCircle(-617.468507, -180, 15, 1);
            //левый верхний
            _ksDocument2D.ksCircle(617.468507, 185, 24, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 15, 1);
            //левый нижний
            _ksDocument2D.ksCircle(617.468507, -180, 24, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 15, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 10, 0, true);


            // Конечная часть батареи бластера
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-641.968507, 185, -600);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();

            _ksDocument2D.ksCircle(-617.468507, 185, 24, 1);
            _ksDocument2D.ksCircle(-617.468507, 185, 15, 1);
            _ksDocument2D.ksLineSeg(-634.439069748477, 201.970562748477, -628.075108717798, 195.606601717798, 1);
            _ksDocument2D.ksLineSeg(-617.468507, 209, -617.468507, 200, 1);
            _ksDocument2D.ksLineSeg(-600.497944251523, 201.970562748477, -606.861905282202, 195.606601717798, 1);
            _ksDocument2D.ksLineSeg(-593.468507, 185, -602.468507, 185, 1);
            _ksDocument2D.ksLineSeg(-600.497944251523, 168.029437251523, -606.861905282202, 174.393398282202, 1);
            _ksDocument2D.ksLineSeg(-617.468507, 161, -617.468507, 170, 1);
            _ksDocument2D.ksLineSeg(-634.439069748477, 168.029437251523, -628.075108717798, 174.393398282202, 1);
            _ksDocument2D.ksLineSeg(-641.468507, 185, -632.468507, 185, 1);


            _ksDocument2D.ksCircle(-617.468507, -180, 24, 1);
            _ksDocument2D.ksCircle(-617.468507, -180, 15, 1);
            _ksDocument2D.ksLineSeg(-634.439069748477, -163.029437251523, -628.075108717798, -169.393398282202, 1);
            _ksDocument2D.ksLineSeg(-617.468507, -156, -617.468507, -165, 1);
            _ksDocument2D.ksLineSeg(-600.497944251523, -163.029437251523, -606.861905282202, -169.393398282202, 1);
            _ksDocument2D.ksLineSeg(-593.468507, -180, -602.468507, -180, 1);
            _ksDocument2D.ksLineSeg(-600.497944251523, -196.970562748477, -606.861905282202, -190.606601717798, 1);
            _ksDocument2D.ksLineSeg(-617.468507, -204, -617.468507, -195, 1);
            _ksDocument2D.ksLineSeg(-634.439069748477, -196.970562748477, -628.075108717798, -190.606601717798, 1);
            _ksDocument2D.ksLineSeg(-641.468507, -180, -632.468507, -180, 1);


            _ksDocument2D.ksCircle(617.468507, 185, 24, 1);
            _ksDocument2D.ksCircle(617.468507, 185, 15, 1);
            _ksDocument2D.ksLineSeg(634.439069748477, 201.970562748477, 628.075108717798, 195.606601717798, 1);
            _ksDocument2D.ksLineSeg(617.468507, 209, 617.468507, 200, 1);
            _ksDocument2D.ksLineSeg(600.497944251523, 201.970562748477, 606.861905282202, 195.606601717798, 1);
            _ksDocument2D.ksLineSeg(593.468507, 185, 602.468507, 185, 1);
            _ksDocument2D.ksLineSeg(600.497944251523, 168.029437251523, 606.861905282202, 174.393398282202, 1);
            _ksDocument2D.ksLineSeg(617.468507, 161, 617.468507, 170, 1);
            _ksDocument2D.ksLineSeg(634.439069748477, 168.029437251523, 628.075108717798, 174.393398282202, 1);
            _ksDocument2D.ksLineSeg(641.468507, 185, 632.468507, 185, 1);


            _ksDocument2D.ksCircle(617.468507, -180, 24, 1);
            _ksDocument2D.ksCircle(617.468507, -180, 15, 1);
            _ksDocument2D.ksLineSeg(634.439069748477, -163.029437251523, 628.075108717798, -169.393398282202, 1);
            _ksDocument2D.ksLineSeg(617.468507, -156, 617.468507, -165, 1);
            _ksDocument2D.ksLineSeg(600.497944251523, -163.029437251523, 606.861905282202, -169.393398282202, 1);
            _ksDocument2D.ksLineSeg(593.468507, -180, 602.468507, -180, 1);
            _ksDocument2D.ksLineSeg(600.497944251523, -196.970562748477, 606.861905282202, -190.606601717798, 1);
            _ksDocument2D.ksLineSeg(617.468507, -204, 617.468507, -195, 1);
            _ksDocument2D.ksLineSeg(634.439069748477, -196.970562748477, 628.075108717798, -190.606601717798, 1);
            _ksDocument2D.ksLineSeg(641.468507, -180, 632.468507, -180, 1);

            iDefinition.EndEdit();
            
            ksEntity entityExtrusion = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_baseExtrusion);

            ksBaseExtrusionDefinition extrusionDefinition = (ksBaseExtrusionDefinition)entityExtrusion.GetDefinition();

            extrusionDefinition.directionType = (short)Direction_Type.dtNormal;

            extrusionDefinition.SetSideParam(true, (short)End_Type.etBlind, 10, 0);
            extrusionDefinition.SetThinParam(true, (short)End_Type.etBlind, 1, 0);

            extrusionDefinition.SetSketch(iSketch);
            entityExtrusion.Create();
        }

        private void Accelerators()
        {
            // основание на крыле
            ksEntity iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            ksEntityCollection iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(-98.760456157148, 54.834961060082, -600);
            ksEntity iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            ksDocument2D _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(-69.238565314295, 55.792328504171, -216.326522495816, 85.20546931176, 1);
            _ksDocument2D.ksLineSeg(-69.238565314295, 55.792328504171, -54.395688959911, 78.493198176107, 1);
            _ksDocument2D.ksLineSeg(-54.395688959911, 78.493198176107, -39.142134225441, 111.653099683237, 1);
            _ksDocument2D.ksLineSeg(-39.142134225441, 111.653099683237, -208.910182359792, 95.073148929672, 1);
            _ksDocument2D.ksLineSeg(-208.910182359792, 95.073148929672, -216.326522495816, 85.20546931176, 1);


            _ksDocument2D.ksLineSeg(-64.739253678729, -54.42784643247, -49.470254859108, -71.493198059993, 1);
            _ksDocument2D.ksLineSeg(-49.470254859108, -71.493198059993, -34.204924615894, -104.678698835012, 1);
            _ksDocument2D.ksLineSeg(-64.739253678729, -54.42784643247, -216.699337266712, -82.993642172126, 1);
            _ksDocument2D.ksLineSeg(-216.699337266712, -82.993642172126, -211.874390428137, -90.591002766762, 1);
            _ksDocument2D.ksLineSeg(-211.874390428137, -90.591002766762, -34.204924615894, -104.678698835012, 1);

            _ksDocument2D.ksLineSeg(69.238565314295, 55.792328504171, 216.326522495816, 85.20546931176, 1);
            _ksDocument2D.ksLineSeg(69.238565314295, 55.792328504171, 54.395688959911, 78.493198176107, 1);
            _ksDocument2D.ksLineSeg(54.395688959911, 78.493198176107, 39.142134225441, 111.653099683237, 1);
            _ksDocument2D.ksLineSeg(39.142134225441, 111.653099683237, 208.910182359792, 95.073148929672, 1);
            _ksDocument2D.ksLineSeg(208.910182359792, 95.073148929672, 216.326522495816, 85.20546931176, 1);


            _ksDocument2D.ksLineSeg(64.739253678729, -54.42784643247, 49.470254859108, -71.493198059993, 1);
            _ksDocument2D.ksLineSeg(49.470254859108, -71.493198059993, 34.204924615894, -104.678698835012, 1);
            _ksDocument2D.ksLineSeg(64.739253678729, -54.42784643247, 216.699337266712, -82.993642172126, 1);
            _ksDocument2D.ksLineSeg(216.699337266712, -82.993642172126, 211.874390428137, -90.591002766762, 1);
            _ksDocument2D.ksLineSeg(211.874390428137, -90.591002766762, 34.204924615894, -104.678698835012, 1);

            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 280, false, 0);


            // Турбины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(121.217288365422, 85.28191092109, -600);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(112.667829571579, 104.472405663619, 40, 1);
            _ksDocument2D.ksCircle(105.609456757216, -99.016919293413, 40, 1);
            _ksDocument2D.ksCircle(-112.667829571579, 104.472405663619, 40, 1);
            _ksDocument2D.ksCircle(-105.609456757216, -99.016919293413, 40, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 200, false, 0);
            ExtrudeSketch(_part, iSketch, 30, true, 0);





            // Основной вырез в Турбины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();


            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            // левая верхняя
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 15, 97.673226100878, 104.874732342367, 126.620543976279, 109.978925502944, -1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 35, 77.805465245013, 101.371510057214, 146.488304832143, 113.482147788097, -1, 1);
            _ksDocument2D.ksLineSeg(77.805465245013, 101.371510057214, 97.673226100878, 104.874732342367, 1);
            _ksDocument2D.ksLineSeg(126.620543976279, 109.978925502944, 146.488304832143, 113.482147788097, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 35.000000000001, 78.199558216151, 98.394719445276, 147.136100927007, 110.550091881962, 1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 15, 97.895713276396, 101.867682998615, 127.439945866762, 107.077128328623, 1, 1);
            _ksDocument2D.ksLineSeg(97.895713276396, 101.867682998615, 78.199558216151, 98.394719445276, 1);
            _ksDocument2D.ksLineSeg(127.439945866762, 107.077128328623, 147.136100927007, 110.550091881962, 1);
            // правая верхняя
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 35, -147.136100927007, 110.550091881962, -78.199558216151, 98.394719445276, 1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 35, -146.488304832143, 113.482147788097, -77.805465245013, 101.371510057214, -1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 15, -127.439945866762, 107.077128328623, -97.895713276396, 101.867682998615, 1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 15, -126.620543976279, 109.978925502944, -97.673226100878, 104.874732342367, -1, 1);
            _ksDocument2D.ksLineSeg(-146.488304832143, 113.482147788097, -126.620543976279, 109.978925502944, 1);
            _ksDocument2D.ksLineSeg(-147.136100927007, 110.550091881962, -127.439945866762, 107.077128328623, 1);
            _ksDocument2D.ksLineSeg(-97.673226100878, 104.874732342367, -77.805465245013, 101.371510057214, 1);
            _ksDocument2D.ksLineSeg(-97.895713276396, 101.867682998615, -78.199558216151, 98.394719445276, 1);

            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 35.000000000001, 70.74709243065, -95.916023687008, 139.429932017781, -108.026661417891, 1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 15, 90.614853286515, -99.419245972161, 119.562171161916, -104.523439132739, 1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 35, 71.141185401788, -92.93923307507, 140.077728112644, -105.094605511756, -1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 15, 90.837340462033, -96.412196628409, 120.381573052399, -101.621641958417, -1, 1);
            _ksDocument2D.ksLineSeg(71.141185401788, -92.93923307507, 90.837340462033, -96.412196628409, 1);
            _ksDocument2D.ksLineSeg(70.74709243065, -95.916023687008, 90.614853286515, -99.419245972161, 1);
            _ksDocument2D.ksLineSeg(120.381573052399, -101.621641958417, 140.077728112644, -105.094605511756, 1);
            _ksDocument2D.ksLineSeg(119.562171161916, -104.523439132739, 139.429932017781, -108.026661417891, 1);

            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 35.000000000001, -140.077728112644, -105.094605511756, -71.141185401788, -92.93923307507, -1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 35, -139.42993201778, -108.026661417891, -70.74709243065, -95.916023687008, 1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 15, -120.381573052399, -101.621641958417, -90.837340462033, -96.412196628409, -1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 15, -119.562171161916, -104.523439132738, -90.614853286514, -99.419245972161, 1, 1);
            _ksDocument2D.ksLineSeg(-140.077728112644, -105.094605511756, -120.381573052399, -101.621641958417, 1);
            _ksDocument2D.ksLineSeg(-90.837340462033, -96.412196628409, -71.141185401788, -92.93923307507, 1);
            _ksDocument2D.ksLineSeg(-139.42993201778, -108.026661417891, -119.562171161916, -104.523439132738, 1);
            _ksDocument2D.ksLineSeg(-90.614853286514, -99.419245972161, -70.74709243065, -95.916023687008, 1);

            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 150, 0, true);


            // срез нижней части турбины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            //
            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 40, 73.275519451091, 97.526478556942, 152.060139692067, 111.418332770296, 1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 35.000000000001, 78.199558216151, 98.394719445276, 147.136100927007, 110.550091881962, 1, 1);
            _ksDocument2D.ksLineSeg(78.199558216151, 98.394719445276, 73.275519451091, 97.526478556942, 1);
            _ksDocument2D.ksLineSeg(147.136100927007, 110.550091881962, 152.060139692067, 111.418332770296, 1);
            //
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 40, -152.060139692067, 111.418332770296, -73.275519451091, 97.526478556942, 1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 35, -147.136100927007, 110.550091881962, -78.199558216151, 98.394719445276, 1, 1);
            _ksDocument2D.ksLineSeg(-147.136100927007, 110.550091881962, -152.060139692067, 111.418332770296, 1);
            _ksDocument2D.ksLineSeg(-78.199558216151, 98.394719445276, -73.275519451091, 97.526478556942, 1);

            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 40, 66.217146636728, -92.070992186736, 145.001766877704, -105.96284640009, -1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 35, 71.141185401788, -92.93923307507, 140.077728112644, -105.094605511756, -1, 1);
            _ksDocument2D.ksLineSeg(66.217146636728, -92.070992186736, 71.141185401788, -92.93923307507, 1);
            _ksDocument2D.ksLineSeg(140.077728112644, -105.094605511756, 145.001766877704, -105.96284640009, 1);


            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 40, -145.001766877704, -105.96284640009, -66.217146636728, -92.070992186736, -1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 35.000000000001, -140.077728112644, -105.094605511756, -71.141185401788, -92.93923307507, -1, 1);
            _ksDocument2D.ksLineSeg(-140.077728112644, -105.094605511756, -145.001766877704, -105.96284640009, 1);
            _ksDocument2D.ksLineSeg(-71.141185401788, -92.93923307507, -66.217146636728, -92.070992186736, 1);

            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 10, 0, true);


            // Срез среднего круга турбины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();


            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 15, 97.895713276396, 101.867682998615, 127.439945866762, 107.077128328623, 1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 15, 97.673226100878, 104.874732342367, 126.620543976279, 109.978925502944, -1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 10, 102.752417824, 105.770330883344, 121.541352253157, 109.083326961967, -1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 10, 102.819752041457, 102.73592388695, 122.515907101701, 106.208887440288, 1, 1);
            _ksDocument2D.ksLineSeg(102.752417824, 105.770330883344, 97.673226100878, 104.874732342367, 1);
            _ksDocument2D.ksLineSeg(121.541352253157, 109.083326961967, 126.620543976279, 109.978925502944, 1);
            _ksDocument2D.ksLineSeg(127.439945866762, 107.077128328623, 122.515907101701, 106.208887440288, 1);
            _ksDocument2D.ksLineSeg(102.819752041457, 102.73592388695, 97.895713276396, 101.867682998615, 1);

            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 15, -127.439945866762, 107.077128328623, -97.895713276396, 101.867682998615, 1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 15, -126.620543976279, 109.978925502944, -97.673226100878, 104.874732342367, -1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 10, -122.515907101701, 106.208887440288, -102.819752041457, 102.73592388695, 1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 10, -121.541352253156, 109.083326961967, -102.752417824001, 105.770330883344, -1, 1);
            _ksDocument2D.ksLineSeg(-126.620543976279, 109.978925502944, -121.541352253156, 109.083326961967, 1);
            _ksDocument2D.ksLineSeg(-127.439945866762, 107.077128328623, -122.515907101701, 106.208887440288, 1);
            _ksDocument2D.ksLineSeg(-102.752417824001, 105.770330883344, -97.673226100878, 104.874732342367, 1);
            _ksDocument2D.ksLineSeg(-102.819752041457, 102.73592388695, -97.895713276396, 101.867682998615, 1);

            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 15, 90.614853286515, -99.419245972161, 119.562171161916, -104.523439132739, 1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 15, 90.837340462033, -96.412196628409, 120.381573052399, -101.621641958417, -1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 10, 95.761379227093, -97.280437516744, 115.457534287338, -100.753401070082, -1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 10, 95.694045009638, -100.314844513139, 114.482979438792, -103.627840591761, 1, 1);
            _ksDocument2D.ksLineSeg(90.837340462033, -96.412196628409, 95.761379227093, -97.280437516744, 1);
            _ksDocument2D.ksLineSeg(90.614853286515, -99.419245972161, 95.694045009638, -100.314844513139, 1);
            _ksDocument2D.ksLineSeg(115.457534287338, -100.753401070082, 120.381573052399, -101.621641958417, 1);
            _ksDocument2D.ksLineSeg(114.482979438792, -103.627840591761, 119.562171161916, -104.523439132739, 1);


            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 15, -120.381573052399, -101.621641958417, -90.837340462033, -96.412196628409, -1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 15, -90.614853286514, -99.419245972161, -119.562171161916, -104.523439132738, -1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 10, -115.457534287338, -100.753401070082, -95.761379227094, -97.280437516744, -1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 10, -114.482979438792, -103.627840591761, -95.694045009638, -100.314844513138, 1, 1);
            _ksDocument2D.ksLineSeg(-120.381573052399, -101.621641958417, -115.457534287338, -100.753401070082, 1);
            _ksDocument2D.ksLineSeg(-95.761379227094, -97.280437516744, -90.837340462033, -96.412196628409, 1);
            _ksDocument2D.ksLineSeg(-90.614853286514, -99.419245972161, -95.694045009638, -100.314844513138, 1);
            _ksDocument2D.ksLineSeg(-114.482979438792, -103.627840591761, -119.562171161916, -104.523439132738, 1);

            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 25, 0, true);

            // Срез малого круга турбины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();


            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 9.999999999999, 102.752417824002, 105.770330883345, 121.541352253155, 109.083326961966, -1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 10, 102.819752041457, 102.73592388695, 122.515907101701, 106.208887440288, 1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 5, 108.20765402653, 106.732236211988, 116.086116050627, 108.121421633323, -1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 5, 107.743790806518, 103.604164775284, 117.59186833664, 105.340646551954, 1, 1);
            _ksDocument2D.ksLineSeg(108.20765402653, 106.732236211988, 102.752417824002, 105.770330883345, 1);
            _ksDocument2D.ksLineSeg(116.086116050627, 108.121421633323, 121.541352253155, 109.083326961966, 1);
            _ksDocument2D.ksLineSeg(122.515907101701, 106.208887440288, 117.59186833664, 105.340646551954, 1);
            _ksDocument2D.ksLineSeg(107.743790806518, 103.604164775284, 102.819752041457, 102.73592388695, 1);

            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 5, -116.086116050627, 108.121421633323, -108.207654026529, 106.732236211988, -1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 5, -117.59186833664, 105.340646551954, -107.743790806518, 103.604164775284, 1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 10, -121.541352253156, 109.083326961967, -102.752417824001, 105.770330883344, -1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 10, -122.515907101701, 106.208887440288, -102.819752041457, 102.73592388695, 1, 1);
            _ksDocument2D.ksLineSeg(-116.086116050627, 108.121421633323, -121.541352253156, 109.083326961967, 1);
            _ksDocument2D.ksLineSeg(-108.207654026529, 106.732236211988, -102.752417824001, 105.770330883344, 1);
            _ksDocument2D.ksLineSeg(-117.59186833664, 105.340646551954, -122.515907101701, 106.208887440288, 1);
            _ksDocument2D.ksLineSeg(-107.743790806518, 103.604164775284, -102.819752041457, 102.73592388695, 1);

            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 10, 95.761379227093, -97.280437516744, 115.457534287338, -100.753401070082, -1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 10, 95.694045009638, -100.314844513139, 114.482979438792, -103.627840591761, 1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 5, 100.685417992155, -98.148678405078, 110.533495522277, -99.885160181748, -1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 5, 101.149281212166, -101.276749841782, 109.027743236264, -102.665935263118, 1, 1);
            _ksDocument2D.ksLineSeg(95.761379227093, -97.280437516744, 100.685417992155, -98.148678405078, 1);
            _ksDocument2D.ksLineSeg(110.533495522277, -99.885160181748, 115.457534287338, -100.753401070082, 1);
            _ksDocument2D.ksLineSeg(95.694045009638, -100.314844513139, 101.149281212166, -101.276749841782, 1);
            _ksDocument2D.ksLineSeg(109.027743236264, -102.665935263118, 114.482979438792, -103.627840591761, 1);


            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 10, -115.457534287338, -100.753401070082, -95.761379227094, -97.280437516744, -1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 10, -114.482979438792, -103.627840591761, -95.694045009638, -100.314844513138, 1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 5, -110.533495522277, -99.885160181748, -100.685417992155, -98.148678405078, -1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 5, -109.027743236264, -102.665935263117, -101.149281212166, -101.276749841782, 1, 1);
            _ksDocument2D.ksLineSeg(-115.457534287338, -100.753401070082, -110.533495522277, -99.885160181748, 1);
            _ksDocument2D.ksLineSeg(-100.685417992155, -98.148678405078, -95.761379227094, -97.280437516744, 1);
            _ksDocument2D.ksLineSeg(-95.694045009638, -100.314844513138, -101.149281212166, -101.276749841782, 1);
            _ksDocument2D.ksLineSeg(-109.027743236264, -102.665935263117, -114.482979438792, -103.627840591761, 1);

            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 5, 0, true);


            //Палка в турбине
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(112.667829571579, 104.472405663619, -570);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();


            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksLineSeg(105.607816794944, 138.752957286861, 110.83232295847, 109.123310465185, 1);
            _ksDocument2D.ksLineSeg(107.577432300969, 139.100253642195, 112.801938464494, 109.47060682052, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 35, 105.607816794944, 138.752957286861, 107.577432300969, 139.100253642195, -1, 1);
            _ksDocument2D.ksArcByPoint(112.667829571579, 104.472405663619, 5, 110.83232295847, 109.123310465185, 112.801938464494, 109.470606820519, -1, 1);

            _ksDocument2D.ksLineSeg(-112.801938464495, 109.470606820519, -107.577432300971, 139.100253642195, 1);
            _ksDocument2D.ksLineSeg(-105.607816794946, 138.752957286862, -110.832322958471, 109.123310465186, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 35, -107.577432300971, 139.100253642195, -105.607816794946, 138.752957286862, -1, 1);
            _ksDocument2D.ksArcByPoint(-112.667829571579, 104.472405663619, 5, -112.801938464495, 109.470606820519, -110.832322958471, 109.123310465186, -1, 1);


            _ksDocument2D.ksLineSeg(103.773950144107, -103.66782409498, 98.549443980583, -133.297470916656, 1);
            _ksDocument2D.ksLineSeg(100.519059486607, -133.64476727199, 105.743565650132, -104.015120450314, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 5, 103.773950144107, -103.66782409498, 105.743565650132, -104.015120450314, 1, 1);
            _ksDocument2D.ksArcByPoint(105.609456757216, -99.016919293413, 35.000000000001, 98.549443980583, -133.297470916656, 100.519059486607, -133.64476727199, 1, 1);


            _ksDocument2D.ksLineSeg(-105.743565650132, -104.015120450313, -100.519059486608, -133.64476727199, 1);
            _ksDocument2D.ksLineSeg(-98.549443980583, -133.297470916656, -103.773950144108, -103.667824094979, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 5, -105.743565650132, -104.015120450313, -103.773950144108, -103.667824094979, 1, 1);
            _ksDocument2D.ksArcByPoint(-105.609456757216, -99.016919293413, 35, -100.519059486608, -133.64476727199, -98.549443980583, -133.297470916656, 1, 1);

            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 150, false, 0);



            // Задняя часть Турбины
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 33, 1);
            _ksDocument2D.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 33, 1);
            _ksDocument2D.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 33, 1);
            _ksDocument2D.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 33, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 150, true, 0);


            // Верхняя часть сопла
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 33, 1);
            _ksDocument2D.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 33, 1);
            _ksDocument2D.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 33, 1);
            _ksDocument2D.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 33, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 50, true, 10);


            // Нижняя часть сопла
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150 - 50);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 41.82, 1);
            _ksDocument2D.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 41.82, 1);
            _ksDocument2D.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 41.82, 1);
            _ksDocument2D.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 41.82, 1);
            iDefinition.EndEdit();
            ExtrudeSketch(_part, iSketch, 50, true, -5);

            // Вырез сопла
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150 - 50 - 50);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 33, 1);
            _ksDocument2D.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 33, 1);
            _ksDocument2D.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 33, 1);
            _ksDocument2D.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 33, 1);
            _ksDocument2D.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 15, 1);
            _ksDocument2D.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 15, 1);
            _ksDocument2D.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 15, 1);
            _ksDocument2D.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 15, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 50, 0, true);

            // Внутренний Вырез сопла
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150 - 50 - 50);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 12.5, 1);
            _ksDocument2D.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 12.5, 1);
            _ksDocument2D.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 12.5, 1);
            _ksDocument2D.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 12.5, 1);
            iDefinition.EndEdit();
            CutExtrusion(_part, iSketch, 50, 0, true);


            // Рисунок сопла
            iSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            iDefinition = (ksSketchDefinition)iSketch.GetDefinition();
            iCollection = _part.EntityCollection((short)Obj3dType.o3d_face);
            iCollection.SelectByPoint(121.217288365422, 104.472405663619, -600 - 200 - 150 - 50);
            iPlane = iCollection.First();
            iDefinition.SetPlane(iPlane);
            iSketch.Create();

            _ksDocument2D = (ksDocument2D)iDefinition.BeginEdit();
            _ksDocument2D.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 15, 1);
            _ksDocument2D.ksCircle(112.667829571579 - 3, 104.472405663619 + 3, 33, 1);
            _ksDocument2D.ksLineSeg(-109.667829571579, 140.472405663619, -109.667829571579, 122.472405663619, 1);
            _ksDocument2D.ksLineSeg(-133.002353350735, 130.806929442775, -120.274431289377, 118.079007381417, 1);
            _ksDocument2D.ksLineSeg(-142.667829571579, 107.472405663619, -124.667829571579, 107.472405663619, 1);
            _ksDocument2D.ksLineSeg(-133.002353350735, 84.137881884463, -120.274431289377, 96.865803945821, 1);
            _ksDocument2D.ksLineSeg(-109.667829571579, 74.472405663619, -109.667829571579, 92.472405663619, 1);
            _ksDocument2D.ksLineSeg(-86.333305792423, 84.137881884463, -99.061227853781, 96.865803945821, 1);
            _ksDocument2D.ksLineSeg(-76.667829571579, 107.472405663619, -94.667829571579, 107.472405663619, 1);
            _ksDocument2D.ksLineSeg(-86.333305792423, 130.806929442775, -99.061227853781, 118.079007381417, 1);

            _ksDocument2D.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 33, 1);
            _ksDocument2D.ksCircle(105.609456757216 - 3, -99.016919293413 - 3, 15, 1);
            _ksDocument2D.ksLineSeg(-79.27493297806, -78.682395514257, -92.002855039418, -91.410317575615, 1);
            _ksDocument2D.ksLineSeg(-102.609456757216, -69.016919293413, -102.609456757216, -87.016919293413, 1);
            _ksDocument2D.ksLineSeg(-125.943980536372, -78.682395514257, -113.216058475014, -91.410317575615, 1);
            _ksDocument2D.ksLineSeg(-135.609456757216, -102.016919293413, -117.609456757216, -102.016919293413, 1);
            _ksDocument2D.ksLineSeg(-125.943980536372, -125.351443072569, -113.216058475014, -112.623521011211, 1);
            _ksDocument2D.ksLineSeg(-102.609456757216, -135.016919293413, -102.609456757216, -117.016919293413, 1);
            _ksDocument2D.ksLineSeg(-79.27493297806, -125.351443072569, -92.002855039418, -112.623521011211, 1);
            _ksDocument2D.ksLineSeg(-69.609456757216, -102.016919293413, -87.609456757216, -102.016919293413, 1);



            _ksDocument2D.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 33, 1);
            _ksDocument2D.ksCircle(-112.667829571579 + 3, 104.472405663619 + 3, 15, 1);
            _ksDocument2D.ksLineSeg(109.667829571579, 140.472405663619, 109.667829571579, 122.472405663619, 1);
            _ksDocument2D.ksLineSeg(133.002353350735, 130.806929442775, 120.274431289377, 118.079007381417, 1);
            _ksDocument2D.ksLineSeg(142.667829571579, 107.472405663619, 124.667829571579, 107.472405663619, 1);
            _ksDocument2D.ksLineSeg(133.002353350735, 84.137881884463, 120.274431289377, 96.865803945821, 1);
            _ksDocument2D.ksLineSeg(109.667829571579, 74.472405663619, 109.667829571579, 92.472405663619, 1);
            _ksDocument2D.ksLineSeg(86.333305792423, 84.137881884463, 99.061227853781, 96.865803945821, 1);
            _ksDocument2D.ksLineSeg(76.667829571579, 107.472405663619, 94.667829571579, 107.472405663619, 1);
            _ksDocument2D.ksLineSeg(86.333305792423, 130.806929442775, 99.061227853781, 118.079007381417, 1);

            _ksDocument2D.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 33, 1);
            _ksDocument2D.ksCircle(-105.609456757216 + 3, -99.016919293413 - 3, 15, 1);
            _ksDocument2D.ksLineSeg(79.27493297806, -78.682395514257, 92.002855039418, -91.410317575615, 1);
            _ksDocument2D.ksLineSeg(102.609456757216, -69.016919293413, 102.609456757216, -87.016919293413, 1);
            _ksDocument2D.ksLineSeg(125.943980536372, -78.682395514257, 113.216058475014, -91.410317575615, 1);
            _ksDocument2D.ksLineSeg(135.609456757216, -102.016919293413, 117.609456757216, -102.016919293413, 1);
            _ksDocument2D.ksLineSeg(125.943980536372, -125.351443072569, 113.216058475014, -112.623521011211, 1);
            _ksDocument2D.ksLineSeg(102.609456757216, -135.016919293413, 102.609456757216, -117.016919293413, 1);
            _ksDocument2D.ksLineSeg(79.27493297806, -125.351443072569, 92.002855039418, -112.623521011211, 1);
            _ksDocument2D.ksLineSeg(69.609456757216, -102.016919293413, 87.609456757216, -102.016919293413, 1);

            iDefinition.EndEdit();


            ksEntity entityExtrusion = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            ksBaseExtrusionDefinition extrusionDefinition = (ksBaseExtrusionDefinition)entityExtrusion.GetDefinition();
            extrusionDefinition.directionType = (short)Direction_Type.dtNormal;
            extrusionDefinition.SetSideParam(true, (short)End_Type.etBlind, 50, 0);
            extrusionDefinition.SetThinParam(true, (short)End_Type.etBlind, 1, 0);

            extrusionDefinition.SetSketch(iSketch);
            entityExtrusion.Create();
        }




        /// <summary>
        /// Метод, выполняющий выдавливание по эскизу
        /// </summary>
        /// <param name="part">Интерфейс компонента</param>
        /// <param name="sketch">Эскиз</param>
        /// <param name="height">Высота выдавливания</param>
        /// <param name="type">Тип выдавливания</param>
        private void ExtrudeSketch(ksPart part, ksEntity sketch, double height, bool type, double draftValue)
        {
            ksEntity entityExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);

            ksBaseExtrusionDefinition extrusionDefinition = (ksBaseExtrusionDefinition)entityExtrusion.GetDefinition();
            if (type == false)
            {
                extrusionDefinition.directionType = (short)Direction_Type.dtReverse;

                extrusionDefinition.SetSideParam(false, (short)End_Type.etBlind, height, draftValue);
            }
            if (type == true)
            {
                extrusionDefinition.directionType = (short)Direction_Type.dtNormal;

                extrusionDefinition.SetSideParam(true, (short)End_Type.etBlind, height, draftValue);
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
    }
}
