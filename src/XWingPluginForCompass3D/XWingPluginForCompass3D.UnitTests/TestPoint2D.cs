using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestPoint2D
    {
        [Test(Description = "Позитивный тест геттера X")]
        public void TestXGet_CorrectValue()
        {
            const int value = 10;
            var point2D = new Point2D(value, 5);
            var actual = point2D.X;
            Assert.AreEqual(value,actual);
        }

        [Test(Description = "Позитивный тест геттера Y")]
        public void TestYGet_CorrectValue()
        {
            const int value = 10;
            var point2D = new Point2D(5, value);
            var actual = point2D.Y;
            Assert.AreEqual(value, actual);
        }
    }
}
