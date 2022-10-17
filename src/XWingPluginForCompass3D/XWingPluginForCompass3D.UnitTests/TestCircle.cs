﻿using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestCircle
    {
        [Test(Description = "Позитивный тест геттера Center")]
        public void TestCenterGet_CorrectValue()
        {
            var value = new Point2D(0, 0);
            var circle = new Circle(value, 5);
            var actual = circle.Center;
            Assert.AreEqual(value,actual);
        }

        [Test(Description = "Позитивный тест геттера Radius")]
        public void TestRadiusGet_CorrectValue()
        {
            const int value = 10;
            var circle = new Circle(new Point2D(0,0), value);
            var actual = circle.Radius;
            Assert.AreEqual(value, actual);
        }
    }
}
