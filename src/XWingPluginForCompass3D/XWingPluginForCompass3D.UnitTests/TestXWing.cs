using XWingPluginForCompass3D.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей класса звездолета.
    /// </summary>
    [TestFixture]
    public class TestXWing
    {
        /// <summary>
        /// Объект класса звездолета.
        /// </summary>
        private readonly XWing _xWing = new XWing();

        /// <summary>
        /// Позитивный тест геттера ErrorList.
        /// </summary>
        [Test(Description = "Позитивный тест геттера ErrorList.")]
        public void TestErrorListGet_CorrectValue()
        {
            var expected = new Dictionary<XWingParameterType, string>();
            var actual = _xWing.ErrorList;
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// Позитивный тест геттера Parameters.
        /// </summary>
        [Test(Description = "Позитивный тест геттера Parameters.")]
        public void TestParametersGet_CorrectValue()
        {
            var expected = new Dictionary<XWingParameterType, Parameter>()
            {
                { XWingParameterType.BodyLength,
                    new Parameter(300, 300, 400,"Длина корпуса",
                        XWingParameterType.BodyLength, _xWing.ErrorList)},
                { XWingParameterType.WingWidth,
                    new Parameter(300, 300, 400,"Ширина крыльев",
                        XWingParameterType.WingWidth, _xWing.ErrorList)},
                { XWingParameterType.BowLength,
                    new Parameter(50, 50, 100,"Длина носовой части корпуса",
                        XWingParameterType.BowLength, _xWing.ErrorList)},
                { XWingParameterType.WeaponBlasterTipLength,
                    new Parameter(80, 80, 130,"Длина острия бластера",
                        XWingParameterType.WeaponBlasterTipLength, _xWing.ErrorList)},
                { XWingParameterType.AcceleratorTurbineLength,
                    new Parameter(150, 150, 250,"Длина турбины",
                        XWingParameterType.AcceleratorTurbineLength, _xWing.ErrorList)},
                { XWingParameterType.AcceleratorNozzleLength,
                    new Parameter(50, 50, 100,"Длина сопла ускорителя",
                        XWingParameterType.AcceleratorNozzleLength, _xWing.ErrorList)},
            };
            var actual = _xWing.Parameters;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Позитивный тест сеттера Parameters.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера Parameters.")]
        public void TestParametersSet_CorrectValue()
        {
            const double bodyLength = 300;
            const double wingWidth = 300;
            const double bowLength = 50;
            const double weaponBlasterTipLength = 80;
            const double acceleratorTurbineLength = 150;
            const double acceleratorNozzleLength = 50;
            _xWing.SetParameters(bodyLength, wingWidth, bowLength, weaponBlasterTipLength, 
                acceleratorTurbineLength, acceleratorNozzleLength);
            const int expected = 0;
            var actual = _xWing.ErrorList.Count;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Негативный тест сеттера Parameters.
        /// </summary>
        [Test(Description = "Негативный тест сеттера Parameters.")]
        public void TestParametersSet_IncorrectValue()
        {
            const double bodyLength = 400;
            const double wingWidth = 300;
            const double bowLength = 50;
            const double weaponBlasterTipLength = 80;
            const double acceleratorTurbineLength = 150;
            const double acceleratorNozzleLength = 50;
            _xWing.SetParameters(bodyLength, wingWidth, bowLength, weaponBlasterTipLength,
                acceleratorTurbineLength, acceleratorNozzleLength);
            const int expected = 1;
            var actual = _xWing.ErrorList.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}