using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingPluginForCompass3D.Model
{
    class XWingParametersEnum
    {
        /// <summary>
        /// Перечисление параметров XWing
        /// </summary>
        public enum XWingParameters
        {
            /// <summary>
            /// Длина корпуса звездолёта
            /// </summary>
            BodyLength,

            /// <summary>
            /// Ширина крыльев звездолёта
            /// </summary>
            WingWidth,

            /// <summary>
            /// Длина носовой части корпуса звездолёта
            /// </summary>
            BowLength,

            /// <summary>
            /// Длина острия оружейного бластера звездолёта
            /// </summary>
            WeaponBlasterTipLength,

            /// <summary>
            /// Длина турбины ускорителя звездолёта
            /// </summary>
            AcceleratorTurbineLength,

            /// <summary>
            /// Длина сопла ускорителя звездолёта
            /// </summary>
            AcceleratorNozzleLength
        }
    }
}
