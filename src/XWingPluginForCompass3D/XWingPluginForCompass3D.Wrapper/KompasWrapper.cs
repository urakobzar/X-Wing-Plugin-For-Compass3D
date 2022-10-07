using System;
using Kompas6API5;
using System.Runtime.InteropServices;
using XWingPluginForCompass3D.Model;

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
        /// 
        /// </summary>
        ksDocument3D _document;

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
        /// Утсанавливает и возвращает деталь X-Wing.
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
        /// Построение детали.
        /// </summary>
        /// <param name="xWingParam"></param>
        public void BuildXWing(XWing xWing)
        {
            try
            {
                
                ksDocument3D document = (ksDocument3D)Kompas.Document3D();
                document.Create();
                XWingBuilder detail = new XWingBuilder(Kompas);

                 Document = (ksDocument3D)Kompas.ActiveDocument3D();
                detail.BuildDetail(xWing);
            }
            catch
            {
                throw new ArgumentException("Не удается построить деталь");
            }
        }
    }
}
