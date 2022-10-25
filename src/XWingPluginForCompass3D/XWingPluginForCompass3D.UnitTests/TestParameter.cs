using System.Collections.Generic;
using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей класса параметра.
    /// </summary>
    [TestFixture]
    public class TestParameter
    {
        /// <summary>
        /// Позитивный тест геттера Value.
        /// </summary>
        [Test(Description = "Позитивный тест геттера Value.")]
        public void TestValueGet_CorrectValue()
        {
            var parameter = new Parameter(10, 1, 20,
                "Value", XWingParameterType.BodyLength,
                new Dictionary<XWingParameterType, string>());
            const int expected = 10;
            var actual = parameter.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Негативный тест геттера Value, когда Value < MinValue.
        /// </summary>
        [Test(Description = "Негативный тест геттера Value, когда Value < MinValue.")]
        public void TestValueMixValueGet_IncorrectValue()
        {
            var parameter = new Parameter(-10, 1, 20,
                "Value", XWingParameterType.BodyLength,
                new Dictionary<XWingParameterType, string>());
            const int expected = 0;
            var actual = parameter.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Негативный тест геттера Value, когда Value > MaxValue.
        /// </summary>
        [Test(Description = "Негативный тест геттера Value, когда Value > MaxValue.")]
        public void TestValueMaxValueGet_IncorrectValue()
        {
            var parameter = new Parameter(25, 1, 20,
                "Value", XWingParameterType.BodyLength,
                new Dictionary<XWingParameterType, string>());
            const int expected = 0;
            var actual = parameter.Value;
            Assert.AreEqual(expected, actual);
        }
    }
}