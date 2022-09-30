using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingPluginForCompass3D.Model
{
    public class XWingParameters
    {
        /// <summary>
        /// Длина всей втулки
        /// </summary>
        private double _bodyLength = 300;

        /// <summary>
        /// Длина верхней части втулки
        /// </summary>
        private double _wingWidth = 300;

        /// <summary>
        /// Диаметр верхней части втулки
        /// </summary>
        private double _bowLength = 50;

        /// <summary>
        /// Внешний диаметр втулки
        /// </summary>
        private double _weaponBlasterTipLength = 80;

        /// <summary>
        /// Внутренний диаметр втулки
        /// </summary>
        private double _acceleratorTurbineLength = 150;

        /// <summary>
        /// Количество отверстий
        /// </summary>
        private double _acceleratorNozzleLength = 50;

        /// <summary>
        /// Возвращает и устанавливает значение длины корпуса звездолёта
        /// </summary>
        public double BodyLength
        {
            get
            {
                return _bodyLength;
            }

            set
            {
                _bodyLength = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение ширины крыльев звездолёта
        /// </summary>
        public double WingWidth
        {
            get
            {
                return _wingWidth;
            }

            set
            {
                _wingWidth = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение длины носовой части корпуса звездолёта
        /// </summary>
        public double BowLength
        {
            get
            {
                return _bowLength;
            }

            set
            {
                _bowLength = value;
            }
        }


        /// <summary>
        /// Возвращает и устанавливает значение длины острия оружейного бластера звездолёта
        /// </summary>
        public double WeaponBlasterTipLength
        {
            get
            {
                return _weaponBlasterTipLength;
            }

            set
            {
                _weaponBlasterTipLength = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение длины турбины ускорителя звездолёта
        /// </summary>
        public double AcceleratorTurbineLength
        {
            get
            {
                return _acceleratorTurbineLength;
            }

            set
            {
                _acceleratorTurbineLength = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает длины сопла ускорителя звездолёта
        /// </summary>
        public double AcceleratorNozzleLength
        {
            get
            {
                return _acceleratorNozzleLength;
            }

            set
            {
                _acceleratorNozzleLength = value;
            }
        }


        /// <summary>
        /// Конструктор звездолёта
        /// </summary>
        /// <param name="bodyLength">Длина корпуса звездолёта</param>
        /// <param name="wingWidth">Ширина крыльев звездолёта</param>
        /// <param name="bowLength">Длина носовой части корпуса звездолёта</param>
        /// <param name="weaponBlasterTipLength">Длина острия оружейного бластера звездолёта</param>
        /// <param name="acceleratorTurbineLength">Длина турбины ускорителя звездолёта</param>
        /// <param name="acceleratorNozzleLength">Длина сопла ускорителя звездолёта</param>
        public XWingParameters(double bodyLength, double wingWidth, double bowLength,
            double weaponBlasterTipLength, double acceleratorTurbineLength,
            double acceleratorNozzleLength)
        {
            BodyLength = bodyLength;
            WingWidth = wingWidth;
            BowLength = bowLength;
            WeaponBlasterTipLength = weaponBlasterTipLength;
            AcceleratorTurbineLength = acceleratorTurbineLength;
            AcceleratorNozzleLength = acceleratorNozzleLength;
        }
    }
}
