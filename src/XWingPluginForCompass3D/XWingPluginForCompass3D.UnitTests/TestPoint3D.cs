using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestPoint3D
    {
        [Test(Description = "Позитивный тест геттера Z")]
        public void TestZGet_CorrectValue()
        {
            const int value = 10;
            var point3D = new Point3D(5, 5,value);
            var actual = point3D.Z;
            Assert.AreEqual(value,actual);
        }
    }
}
