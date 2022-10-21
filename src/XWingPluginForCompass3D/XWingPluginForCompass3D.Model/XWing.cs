﻿using System.Collections.Generic;

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
        public Dictionary<XWingParameterType, Parameter> Parameters { set; get; }

        /// <summary>
        /// Список ошибок введенного параметра.
        /// </summary>
        public Dictionary<XWingParameterType, string> ErrorList { set; get; }

        /// <summary>
        /// Создает объект класса звездолета для построения.
        /// </summary>
        public XWing()
        {
            ErrorList = new Dictionary<XWingParameterType, string>();
            Parameters = new Dictionary<XWingParameterType, Parameter>()
            {
                { XWingParameterType.BodyLength, 
                    new Parameter(300, 300, 400,"Длина корпуса",
                    XWingParameterType.BodyLength, ErrorList)},
                { XWingParameterType.WingWidth, 
                    new Parameter(300, 300, 400,"Ширина крыльев",
                    XWingParameterType.WingWidth, ErrorList)},
                { XWingParameterType.BowLength, 
                    new Parameter(50, 50, 100,"Длина носовой части корпуса",
                    XWingParameterType.BowLength, ErrorList)},
                { XWingParameterType.WeaponBlasterTipLength,
                    new Parameter(80, 80, 130,"Длина острия бластера",
                        XWingParameterType.WeaponBlasterTipLength, ErrorList)},
                { XWingParameterType.AcceleratorTurbineLength,
                    new Parameter(150, 150, 250,"Длина турбины",
                        XWingParameterType.AcceleratorTurbineLength, ErrorList)},
                { XWingParameterType.AcceleratorNozzleLength,
                    new Parameter(50, 50, 100,"Длина сопла ускорителя",
                        XWingParameterType.AcceleratorNozzleLength, ErrorList)},
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
        public void SetParameters (double bodyLength, double wingWidth, double bowLength,
            double weaponBlasterTipLength, double acceleratorTurbineLength,
            double acceleratorNozzleLength)
        {
            ErrorList.Clear();
            Parameters[XWingParameterType.BodyLength].Value = bodyLength;
            CheckParametersRelationship(bodyLength, wingWidth + 20,
                XWingParameterType.WingWidth,
                "Ширина крыльев должна быть не меньше длины корпуса более, " +
                "чем на 20 мм.");
            CheckParametersRelationship(wingWidth, bodyLength,
                XWingParameterType.WingWidth,
                "Ширина крыльев не может быть больше длины корпуса.");
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
                ErrorList.Add(parameterType, errorMessage);
            }
            else
            {
                Parameters[parameterType].Value = value;
            }
        }
    }
}