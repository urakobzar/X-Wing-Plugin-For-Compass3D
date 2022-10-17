using System.Collections.Generic;
using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestParameter
    {

        [Test(Description = "Позитивный тест геттера Value")]
        public void TestValueGet_CorrectValue()
        {
            var parameter = new Parameter(10, 1, 20,
                "Value", XWingParameterType.BodyLength,
                new Dictionary<XWingParameterType, string>());
            const int expected = 10;
            var actual = parameter.Value;
            Assert.AreEqual(expected, actual);
        }

        [Test(Description = "Негативный тест геттера Value, Value < MinValue")]
        public void TestValueMixValueGet_IncorrectValue()
        {
            var parameter = new Parameter(-10, 1, 20,
                "Value", XWingParameterType.BodyLength,
                new Dictionary<XWingParameterType, string>());
            const int expected = 0;
            var actual = parameter.Value;
            Assert.AreEqual(expected, actual);
        }

        [Test(Description = "Негативный тест геттера Value, Value < MinValue")]
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
