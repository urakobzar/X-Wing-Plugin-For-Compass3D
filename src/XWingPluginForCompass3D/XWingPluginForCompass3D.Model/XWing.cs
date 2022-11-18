using System.Collections.Generic;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс звездолета X-Wing.
    /// </summary>
    public class XWing
    {
        /// <summary>
        /// Словарь, где ключ: тип параметра звездолета из перечисления, 
        /// значение: соответствующий параметр.
        /// </summary>
        public Dictionary<XWingParameterType, Parameter> Parameters { get; set; }
        
        /// <summary>
        /// Список ошибок введенного параметра.
        /// </summary>
        public Dictionary<XWingParameterType, string> Errors { get; set; }

        /// <summary>
        /// Создает объект класса звездолета для построения.
        /// </summary>
        public XWing()
        {
            Errors = new Dictionary<XWingParameterType, string>();
            Parameters = new Dictionary<XWingParameterType, Parameter>()
            {
                { XWingParameterType.BodyLength, 
                    new Parameter(300, 300, 400,"Длина корпуса",
                    XWingParameterType.BodyLength, Errors)},
                { XWingParameterType.WingWidth, 
                    new Parameter(300, 300, 400,"Ширина крыльев",
                    XWingParameterType.WingWidth, Errors)},
                { XWingParameterType.BowLength, 
                    new Parameter(50, 50, 100,"Длина носовой части корпуса",
                    XWingParameterType.BowLength, Errors)},
                { XWingParameterType.WeaponBlasterTipLength,
                    new Parameter(80, 80, 130,"Длина острия бластера",
                        XWingParameterType.WeaponBlasterTipLength, Errors)},
                { XWingParameterType.AcceleratorTurbineLength,
                    new Parameter(150, 150, 250,"Длина турбины",
                        XWingParameterType.AcceleratorTurbineLength, Errors)},
                { XWingParameterType.AcceleratorNozzleLength,
                    new Parameter(50, 50, 100,"Длина сопла ускорителя",
                        XWingParameterType.AcceleratorNozzleLength, Errors)},
                { XWingParameterType.CaseBodySetHeight,
                    new Parameter(10, 10, 20,"Высота установок крыши корпуса",
                        XWingParameterType.CaseBodySetHeight, Errors)}
            };
        }

        /// <summary>
        /// Создает объект класса звездолета для построения.
        /// </summary>
        /// <param name="bodyLength">Длина корпуса звездолета</param>
        /// <param name="wingWidth">Ширина крыльев звездолета</param>
        /// <param name="bowLength">Длина носовой части корпуса звездолета</param>
        /// <param name="weaponBlasterTipLength">Длина острия оружейного бластера звездолета</param>
        /// <param name="acceleratorTurbineLength">Длина турбины ускорителя звездолета</param>
        /// <param name="acceleratorNozzleLength">Длина сопла ускорителя звездолета</param>
        /// <param name="caseBodySetHeight">Высота установок крыши корпуса.</param>
        public void SetParameters (double bodyLength, double wingWidth, double bowLength,
            double weaponBlasterTipLength, double acceleratorTurbineLength,
            double acceleratorNozzleLength, double caseBodySetHeight)
        {
            Errors.Clear();
            Parameters[XWingParameterType.BodyLength].Value = bodyLength;
            CheckParametersRelationship(bodyLength, wingWidth + 20,
                XWingParameterType.WingWidth,
                "Ширина крыльев должна быть не меньше длины корпуса более, " +
                "чем на 20 мм.");
            if (!Errors.ContainsKey(XWingParameterType.WingWidth))
            {
                CheckParametersRelationship(wingWidth, bodyLength,
                    XWingParameterType.WingWidth,
                    "Ширина крыльев не может быть больше длины корпуса.");
            }
            CheckParametersRelationship(bowLength, weaponBlasterTipLength, 
                XWingParameterType.BowLength, 
                "Длина носа не может быть больше, чем длина острия.");
            CheckParametersRelationship(weaponBlasterTipLength, 2 * bowLength,
                XWingParameterType.WeaponBlasterTipLength, 
                "Длина острия не должна быть более, чем в 2 раза больше, " +
                "чем длина носовой части.");
            CheckParametersRelationship(acceleratorTurbineLength, 
                4 * acceleratorNozzleLength,
                XWingParameterType.AcceleratorTurbineLength, 
                "Длина турбины ускорителя не должна быть более, " +
                "чем в 4 раза больше, чем длина сопла.");
            Parameters[XWingParameterType.AcceleratorNozzleLength].Value = 
                acceleratorNozzleLength;
            Parameters[XWingParameterType.CaseBodySetHeight].Value =
                caseBodySetHeight;
        }

        /// <summary>
        /// Проверка взаимосвязи параметров между собой.
        /// </summary>
        /// <param name="value">Значение введенного параметра.</param>
        /// <param name="mainParameter">Значение параметра, от которого зависимость.</param>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="errorMessage">Сообщение об ошибке.</param>
        private void CheckParametersRelationship(double value, double mainParameter, 
            XWingParameterType parameterType, string errorMessage)
        {
            if (value > mainParameter)
            {
                Errors.Add(parameterType, errorMessage);
            }
            else
            {
                Parameters[parameterType].Value = value;
            }
        }
    }
}