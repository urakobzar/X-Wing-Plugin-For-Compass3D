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
    /// Класс для запуска плагина в САПР Компас 3D.
    /// </summary>
    public class KompasWrapper
    {
        /// <summary>
        /// Объект Компас API.
        /// </summary>
        private KompasObject _kompas = null;

        /// <summary>
        /// Запуск Компас-3D.
        /// </summary>
        public void StartKompas()
        {
            try
            {
                if (_kompas != null)
                {
                    _kompas.Visible = true;
                    _kompas.ActivateControllerAPI();
                }
                if (_kompas == null)
                {
                    Type kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _kompas = (KompasObject)Activator.CreateInstance(kompasType);
                    StartKompas();
                    if (_kompas == null)
                    {
                        throw new Exception("Не удается открыть Koмпас-3D");
                    }
                }
            }
            catch (COMException)
            {
                _kompas = null;
                StartKompas();
            }
        }

        /// <summary>
        /// Построение детали.
        /// </summary>
        /// <param name="xWingParam"></param>
        public void BuildXWing(XWingParameters xWing)
        {
            try
            {
                XWingBuilder detail = new XWingBuilder(_kompas);
                detail.BuildDetail(xWing);
            }
            catch
            {
                throw new ArgumentException("Не удается построить деталь");
            }
        }
    }
}
