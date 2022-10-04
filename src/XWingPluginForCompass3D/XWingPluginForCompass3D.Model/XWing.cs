using System.Collections.Generic;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс звездолёта X-Wing.
    /// </summary>
    public class XWing
    {
        /// <summary>
        /// Длина корпуса звездолёта.
        /// </summary>
        private double _bodyLength;

        /// <summary>
        /// Ширина крыльев звездолёта.
        /// </summary>
        private double _wingWidth;

        /// <summary>
        /// Длина носовой части корпуса звездолёта.
        /// </summary>
        private double _bowLength;

        /// <summary>
        /// Длина острия оружейного бластера звездолёта.
        /// </summary>
        private double _weaponBlasterTipLength;

        /// <summary>
        /// Длина турбины ускорителя звездолёта.
        /// </summary>
        private double _acceleratorTurbineLength;

        /// <summary>
        /// Длина сопла ускорителя звездолёта.
        /// </summary>
        private double _acceleratorNozzleLength;

        /// <summary>
        /// Словарь, где ключ: параметр звездолёта, значение: соотвествующее 
        /// сообщение об ошибке при попытке присвоить параметр.
        /// </summary>
        private Dictionary <XWingParameters, string> _errorList;

        /// <summary>
        /// Устанавливает и возвращает значение длины корпуса звездолёта.
        /// </summary>
        public double BodyLength
        {
            set
            {
                const int minTotalLength = 300;
                const int maxTotalLength = 400;
                string minMessage = "Длина корпуса не может быть менее 300 мм.";
                string maxMessage = "Длина корпуса не может быть более 400 мм.";
                CheckRange(value, minTotalLength, maxTotalLength,
                    XWingParameters.BodyLength, minMessage, maxMessage);
                _bodyLength = value;
            }
            get
            {
                return _bodyLength;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает значение ширины крыльев звездолёта.
        /// </summary>
        public double WingWidth
        {
            set
            {
                const int minWingWidth = 300;
                const int maxWingWidth = 400;
                string minMessage = "Ширина крыльев не может быть менее 300 мм.";
                string maxMessage = "Ширина крыльев не может быть более 400 мм.";
                CheckRange(value, minWingWidth, maxWingWidth,
                    XWingParameters.WingWidth, minMessage, maxMessage);
                _wingWidth = value;
            }
            get
            {
                return _wingWidth;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает значение длины носовой части корпуса звездолёта.
        /// </summary>
        public double BowLength
        {
            set
            {
                const int minBowLength = 50;
                const int maxBowLength = 100;
                string minMessage = "Длина носовой части корпуса не может быть менее 50 мм.";
                string maxMessage = "Длина носовой части корпуса не может быть более 100 мм.";
                CheckRange(value, minBowLength, maxBowLength,
                    XWingParameters.BowLength, minMessage, maxMessage);
                _bowLength = value;
            }
            get
            {
                return _bowLength;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает значение длины острия оружейного бластера звездолёта.
        /// </summary>
        public double WeaponBlasterTipLength
        {
            set
            {
                const int minTipLength = 80;
                const int maxTipLength = 130;
                string minMessage = "Длина острия бластера не может быть менее 80 мм.";
                string maxMessage = "Длина острия бластера не может быть более 130 мм.";
                CheckRange(value, minTipLength, maxTipLength,
                    XWingParameters.WeaponBlasterTipLength, minMessage, maxMessage);
                _weaponBlasterTipLength = value;
            }
            get
            {
                return _weaponBlasterTipLength;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает значение длины турбины ускорителя звездолёта.
        /// </summary>
        public double AcceleratorTurbineLength
        {
            set
            {
                const int minTurbineLength = 150;
                const int maxTurbineLength = 250;
                string minMessage = "Длина турбины не может быть менее 150 мм.";
                string maxMessage = "Длина турбины не может быть более 250 мм.";
                CheckRange(value, minTurbineLength, maxTurbineLength,
                    XWingParameters.AcceleratorTurbineLength, minMessage, maxMessage);
                _acceleratorTurbineLength = value;
            }
            get
            {
                return _acceleratorTurbineLength;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает длины сопла ускорителя звездолёта.
        /// </summary>
        public double AcceleratorNozzleLength
        {
            set
            {
                const int minNozzleLength = 50;
                const int maxNozzleLength = 100;
                string minMessage = "Длина корпуса не может быть менее 50 мм.";
                string maxMessage = "Длина корпуса не может быть более 100 мм.";
                CheckRange(value, minNozzleLength, maxNozzleLength,
                    XWingParameters.AcceleratorNozzleLength, minMessage, maxMessage);
                _acceleratorNozzleLength = value;
            }
            get
            {
                return _acceleratorNozzleLength;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает словарь параметров-сообщений.
        /// </summary>
        public Dictionary<XWingParameters, string> ErrorList
        {
            set
            {
                _errorList = value;
            }
            get
            {
                return _errorList;
            }
        }

        /// <summary>
        /// Создаёт объект класса звездолёта для построения.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса звездолёта</param>
        /// <param name="wingWidth">Ширина крыльев звездолёта</param>
        /// <param name="bowLength">Длина носовой части корпуса звездолёта</param>
        /// <param name="weaponBlasterTipLength">Длина острия оружейного бластера звездолёта</param>
        /// <param name="acceleratorTurbineLength">Длина турбины ускорителя звездолёта</param>
        /// <param name="acceleratorNozzleLength">Длина сопла ускорителя звездолёта</param>
        public XWing(double bodyLength, double wingWidth, double bowLength,
            double weaponBlasterTipLength, double acceleratorTurbineLength,
            double acceleratorNozzleLength)
        {
            ErrorList = new Dictionary<XWingParameters, string>();
            BodyLength = bodyLength;
            if (wingWidth < bodyLength-20)
            {
                ErrorList.Add(XWingParameters.WingWidth, 
                    "Ширина крыльев должна быть не меньше длины корпуса более," +
                    " чем на 20 мм.");
            }
            else if (wingWidth > bodyLength)
            {
                ErrorList.Add(XWingParameters.WingWidth, 
                    "Ширина крыльев не может быть больше длины корпуса.");
            }
            else
            {
                WingWidth = wingWidth;
            }
            if (bowLength > weaponBlasterTipLength)
            {
                ErrorList.Add(XWingParameters.BowLength, 
                    "Длина носа не может быть больше, чем длина острия.");
            }
            else
            {
                BowLength = bowLength;
            }
            if (weaponBlasterTipLength > 2 * bowLength)
            {
                ErrorList.Add(XWingParameters.WeaponBlasterTipLength,
                    "Длина острия не должна быть более, чем в 2 раза больше," +
                    " чем длина носовой части.");
            }
            else
            {
                WeaponBlasterTipLength = weaponBlasterTipLength;
            }
            if (acceleratorTurbineLength > 4 * acceleratorNozzleLength)
            {
                ErrorList.Add(XWingParameters.AcceleratorTurbineLength,
                    "Длина турбины ускорителя не должна быть более," +
                    " чем в 4 раза больше, чем длина сопла.");
            }
            else
            {
                AcceleratorTurbineLength = acceleratorTurbineLength;
            }
            AcceleratorNozzleLength = acceleratorNozzleLength;            
        }

        /// <summary>
        /// Проверка принадлежности диапазону у введённого параметра.
        /// </summary>
        /// <param name="value">Введеное значение параметра.</param>
        /// <param name="minValue">Минимальное возможное значение.</param>
        /// <param name="maxValue">Максимальное возможное значение.</param>
        /// <param name="parameter">Параметр.</param>
        /// <param name="minMessage">Сообщение, если меньше минимума.</param>
        /// <param name="maxMessage">Сообщение, если больше максимума.</param>
        private void CheckRange(double value, double minValue,
            double maxValue, XWingParameters parameter,
            string minMessage, string maxMessage)
        {
            if (value < minValue)
            {
                ErrorList.Add(parameter, minMessage);
            }
            else if (value > maxValue)
            {
                ErrorList.Add(parameter, maxMessage);
            }
        }
    }
}
