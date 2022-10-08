using System.Collections.Generic;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс звездолёта X-Wing.
    /// </summary>
    public class XWing
    {
        /// <summary>
        /// Словарь, где ключ: тип параметра звездолёта из перечисления, 
        /// значение: соотвествующий параметр.
        /// </summary>
        private Dictionary<XWingParameters, Parameter> _parameters = new Dictionary<XWingParameters, Parameter>();

        /// <summary>
        /// Устанавливает и возвращает словарь типов параметров - параметров. 
        /// </summary>
        public Dictionary<XWingParameters, Parameter> Parameters
        {
            set
            {
                _parameters = value;
            }
            get
            {
                return _parameters;
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
            ErrorList.Errors = new Dictionary<XWingParameters, string>();
            Parameters.Add(XWingParameters.BodyLength, 
                new Parameter(bodyLength, 300, 400, "Длина корпуса", XWingParameters.BodyLength));
            if (wingWidth < bodyLength - 20)
            {
                ErrorList.Errors.Add(XWingParameters.WingWidth,
                    "Ширина крыльев должна быть не меньше длины корпуса более, " +
                    "чем на 20 мм.");
            }
            else if (wingWidth > bodyLength)
            {
                ErrorList.Errors.Add(XWingParameters.WingWidth,
                    "Ширина крыльев не может быть больше длины корпуса.");
            }
            else
            {
                Parameters.Add(XWingParameters.WingWidth, new Parameter(wingWidth, 300, 400,
                "Ширина крыльев", XWingParameters.WingWidth));
            }
            if (bowLength > weaponBlasterTipLength)
            {
                ErrorList.Errors.Add(XWingParameters.BowLength,
                    "Длина носа не может быть больше, чем длина острия.");
            }
            else
            {
                Parameters.Add(XWingParameters.BowLength, new Parameter(bowLength, 50, 100,
                "Длина носовой части корпуса", XWingParameters.BowLength));
            }
            if (weaponBlasterTipLength > 2 * bowLength)
            {
                ErrorList.Errors.Add(XWingParameters.WeaponBlasterTipLength,
                    "Длина острия не должна быть более, чем в 2 раза больше, " +
                    "чем длина носовой части.");
            }
            else
            {
                Parameters.Add(XWingParameters.WeaponBlasterTipLength, 
                    new Parameter(weaponBlasterTipLength, 80, 130,
                "Длина острия бластера", XWingParameters.WeaponBlasterTipLength));
            }
            if (acceleratorTurbineLength > 4 * acceleratorNozzleLength)
            {
                ErrorList.Errors.Add(XWingParameters.AcceleratorTurbineLength,
                    "Длина турбины ускорителя не должна быть более, " +
                    "чем в 4 раза больше, чем длина сопла.");
            }
            else
            {
                Parameters.Add(XWingParameters.AcceleratorTurbineLength, 
                    new Parameter(acceleratorTurbineLength, 150, 250,
                "Длина турбины", XWingParameters.AcceleratorTurbineLength));
            }
            Parameters.Add(XWingParameters.AcceleratorNozzleLength, 
                new Parameter(acceleratorNozzleLength, 50, 100,
                "Длина сопла ускорителя", XWingParameters.AcceleratorNozzleLength));
        }
    }
}