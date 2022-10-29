using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей класса 3D-точки.
    /// </summary>
    [TestFixture]
    public class TestPoint3D
    {
        /// <summary>
        /// Позитивный тест геттера Z.
        /// </summary>
        [Test(Description = "Позитивный тест геттера Z.")]
        public void TestZGet_CorrectValue()
        {
            const int value = 10;
            var point3D = new Point3D(5, 5,value);
            var actual = point3D.Z;
            Assert.AreEqual(value,actual);
        }

        /// <summary>
        /// Позитивный тест метода Equals.
        /// </summary>
        [Test(Description = "Позитивный тест метода Equals.")]
        public void TestEquals_CorrectValue()
        {
            var expected = new Point3D(0, 0, 0);
            var actual = new Point3D(0, 0, 0);
            Assert.AreEqual(expected, actual);
        }
    }
}