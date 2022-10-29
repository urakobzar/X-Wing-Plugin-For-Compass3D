using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей класса 2D-точки.
    /// </summary>
    [TestFixture]
	// TODO: нет тестов на Equals. Так везде, где есть интерфейс IEquatable ИСПРАВИЛ
	public class TestPoint2D
    {
        /// <summary>
        /// Позитивный тест геттера X.
        /// </summary>
        [Test(Description = "Позитивный тест геттера X.")]
        public void TestXGet_CorrectValue()
        {
            const int value = 10;
            var point2D = new Point2D(value, 5);
            var actual = point2D.X;
            Assert.AreEqual(value,actual);
        }

        /// <summary>
        /// Позитивный тест геттера Y.
        /// </summary>
        [Test(Description = "Позитивный тест геттера Y.")]
        public void TestYGet_CorrectValue()
        {
            const int value = 10;
            var point2D = new Point2D(5, value);
            var actual = point2D.Y;
            Assert.AreEqual(value, actual);
        }

        /// <summary>
        /// Позитивный тест метода Equals.
        /// </summary>
        [Test(Description = "Позитивный тест метода Equals.")]
        public void TestEquals_CorrectValue()
        {
            var expected = new Point2D(0, 0);
            var actual = new Point2D(0, 0);
            Assert.AreEqual(expected, actual);
        }
    }
}