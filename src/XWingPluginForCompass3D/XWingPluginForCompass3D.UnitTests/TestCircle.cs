using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    /// <summary>
    /// Класс тестирования полей класса круга.
    /// </summary>
    [TestFixture]
    public class TestCircle
    {
        /// <summary>
        /// Позитивный тест геттера Center.
        /// </summary>
        [Test(Description = "Позитивный тест геттера Center.")]
        public void TestCenterGet_CorrectValue()
        {
            var value = new Point2D(0, 0);
            var circle = new Circle(value, 5);
            var actual = circle.Center;
            Assert.AreEqual(value,actual);
        }

        /// <summary>
        /// Позитивный тест геттера Radius.
        /// </summary>
        [Test(Description = "Позитивный тест геттера Radius.")]
        public void TestRadiusGet_CorrectValue()
        {
            const int value = 10;
            var circle = new Circle(new Point2D(0,0), value);
            var actual = circle.Radius;
            Assert.AreEqual(value, actual);
        }

        /// <summary>
        /// Позитивный тест метода Equals.
        /// </summary>
        [Test(Description = "Позитивный тест метода Equals.")]
        public void TestEquals_CorrectValue()
        {
            var expected = new Circle(new Point2D(0, 0), 5);
            var actual = new Circle(new Point2D(0, 0), 5);
            Assert.AreEqual(expected, actual);
        }
    }
}