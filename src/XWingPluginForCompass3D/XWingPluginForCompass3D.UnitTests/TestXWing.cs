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
        /// Позитивный тест геттера Errors.
        /// </summary>
        [Test(Description = "Позитивный тест геттера Errors.")]
        public void TestErrorListGet_CorrectValue()
        {
            var expected = new Dictionary<XWingParameterType, string>();
            var actual = _xWing.Errors;
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
                        XWingParameterType.BodyLength, _xWing.Errors)},
                { XWingParameterType.WingWidth,
                    new Parameter(300, 300, 400,"Ширина крыльев",
                        XWingParameterType.WingWidth, _xWing.Errors)},
                { XWingParameterType.BowLength,
                    new Parameter(50, 50, 100,"Длина носовой части корпуса",
                        XWingParameterType.BowLength, _xWing.Errors)},
                { XWingParameterType.WeaponBlasterTipLength,
                    new Parameter(80, 80, 130,"Длина острия бластера",
                        XWingParameterType.WeaponBlasterTipLength, _xWing.Errors)},
                { XWingParameterType.AcceleratorTurbineLength,
                    new Parameter(150, 150, 250,"Длина турбины",
                        XWingParameterType.AcceleratorTurbineLength, _xWing.Errors)},
                { XWingParameterType.AcceleratorNozzleLength,
                    new Parameter(50, 50, 100,"Длина сопла ускорителя",
                        XWingParameterType.AcceleratorNozzleLength, _xWing.Errors)},
                { XWingParameterType.CaseBodySetHeight,
                    new Parameter(10, 10, 20,"Высота установок крыши корпуса",
                        XWingParameterType.CaseBodySetHeight, _xWing.Errors)}
            };
            var actual = _xWing.Parameters;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Позитивный и негативный тест сеттера Parameters.
        /// </summary>
        [Test(Description = "Позитивный и негативный тест сеттера Parameters.")]
        [TestCase(300,0, Description = "Позитивный тест сеттера Parameters.")]
        [TestCase(400, 1, Description = "Негативный тест сеттера Parameters.")]
        // TODO: дубль  ИСПРАВИЛ
        public void TestParametersSet_CorrectValue(double bodyLength, int expected)
        {
            const double wingWidth = 300;
            const double bowLength = 50;
            const double weaponBlasterTipLength = 80;
            const double acceleratorTurbineLength = 150;
            const double acceleratorNozzleLength = 50;
            const double caseBodySetHeight = 10;
            _xWing.SetParameters(bodyLength, wingWidth, bowLength, weaponBlasterTipLength, 
                acceleratorTurbineLength, acceleratorNozzleLength, caseBodySetHeight);
            var actual = _xWing.Errors.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}