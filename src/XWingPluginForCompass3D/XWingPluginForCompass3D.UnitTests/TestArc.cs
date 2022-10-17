using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestArc
    {
        [Test(Description = "Позитивный тест геттера StartPoint")]
        public void TestStartPointGet_CorrectValue()
        {
            var value = new Point2D(-5,0);
            var arc = new Arc(new Point2D(0,0), 5,
                value, new Point2D(5,0), 1);
            var actual = arc.StartPoint;
            Assert.AreEqual(value, actual);
        }

        [Test(Description = "Позитивный тест геттера EndPoint")]
        public void TestEndPointGet_CorrectValue()
        {
            var value = new Point2D(5, 0);
            var arc = new Arc(new Point2D(0, 0), 5,
                new Point2D(-5, 0), value, 1);
            var actual = arc.EndPoint;
            Assert.AreEqual(value, actual);
        }

        [Test(Description = "Позитивный тест геттера Direction")]
        public void TestDirectionGet_CorrectValue()
        {
            short value = 1;
            var arc = new Arc(new Point2D(0, 0), 5,
                new Point2D(-5, 0), new Point2D(5, 0), value);
            var actual = arc.Direction;
            Assert.AreEqual(value, actual);
        }
    }
}