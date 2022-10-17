using XWingPluginForCompass3D.Model;
using NUnit.Framework;

namespace XWingPluginForCompass3D.UnitTests
{
    [TestFixture]
    public class TestWingConstants
    {
        private const int BodyLength = 300;

        private const int WingsWidth = 300;

        private readonly WingsConstants _constants =
            new WingsConstants(WingsWidth,BodyLength);

        private readonly CheckingObjectEquality _check =
            new CheckingObjectEquality();

        [Test(Description = "Позитивный тест геттера BackBodyPlane")]
        public void TestUpperBaseVertexesGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, 0, -600 - BodyLength);
            var actual = _constants.BackBodyPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера CuttingPlane")]
        public void TestCuttingPlaneGet_CorrectValue()
        {
            var excepted =
                new Point3D(0, -121.493198, -600 - BodyLength);
            var actual = _constants.CuttingPlane;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера BaseVertexes")]
        public void TestBaseVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    { new Point2D(69.173833, 55.779384),
                        new Point2D(78.023751, 42.034027) },
                    { new Point2D(78.023751, 42.034027),
                        new Point2D(632.086192, 150.642161) },
                    { new Point2D(632.086192, 150.642161),
                        new Point2D(639.549926, 169.837344) },
                    { new Point2D(639.549926, 169.837344),
                        new Point2D(69.173833, 55.779384) }
                },
                {
                    { new Point2D(77.853158, -39.308584),
                        new Point2D(64.171267, -54.321075) },
                    { new Point2D(64.171267, -54.321075),
                        new Point2D(646.963505, -163.875661) },
                    { new Point2D(646.963505, -163.875661),
                        new Point2D(640.123102, -145.081809) },
                    { new Point2D(640.123102, -145.081809),
                        new Point2D(77.853158, -39.308584) }
                }
            };
            var actual = _constants.BaseVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }

        [Test(Description = "Позитивный тест геттера WingsCutVertexes")]
        public void TestWingsCutVertexesGet_CorrectValue()
        {
            Point2D[,,] excepted =
            {
                {
                    {
                        new Point2D(646.963505,
                            -600 - BodyLength + WingsWidth),
                        new Point2D(646.963505,
                            -666.419923 - BodyLength + WingsWidth)
                    },
                    {
                        new Point2D(646.963505,
                            -666.419923 - BodyLength + WingsWidth),
                        new Point2D(128.282347,
                            -600 - BodyLength + WingsWidth)
                    },
                    {
                        new Point2D(128.282347,
                            -600 - BodyLength + WingsWidth),
                        new Point2D(646.963505,
                            -600 - BodyLength + WingsWidth)
                    }
                },

                {
                    { new Point2D(646.963505, -600 - BodyLength),
                        new Point2D(646.963505, -550 - BodyLength) },
                    {new Point2D(646.963505, -550 - BodyLength),
                        new Point2D(128.282347, -600 - BodyLength)},
                    { new Point2D(128.282347, -600 - BodyLength),
                        new Point2D(646.963505, -600 - BodyLength) }
                }
            };
            var actual = _constants.WingsCutVertexes;
            Assert.IsTrue(_check.CheckEqual(excepted, actual));
        }
    }
}