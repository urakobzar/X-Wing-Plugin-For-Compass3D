using System.Collections.Generic;
using XWingPluginForCompass3D.Model;

namespace XWingPluginForCompass3D.UnitTests
{
	// TODO: XML
	// TODO: для класса Point2D можно реализовать интерфейс IEquatable 
	public class CheckingObjectEquality
    {
        public bool CheckEqual(Point2D[,,] expected, Point2D[,,] actual)
        {
            for (var i = 0; i < expected.GetLength(0); i++)
            {
                for (var j = 0; j < expected.GetLength(1); j++)
                {
                    for (var k = 0; k < expected.GetLength(2); k++)
                    {
                        if (!actual[i, j, k].Equals(expected[i, j, k]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool CheckEqual(Point3D[] expected, Point3D[] actual)
        {
            for (var i = 0; i < expected.GetLength(0); i++)
            {
                if (!actual[i].Equals(expected[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckEqual(Point3D excepted, Point3D actual)
        {
            return (excepted.X == actual.X) &&
                   (excepted.Y == actual.Y) &&
                   (excepted.Z == actual.Z);
        }

        public bool CheckEqual(Circle[,] excepted, Circle[,] actual)
        {
            for (var i = 0; i < excepted.GetLength(0); i++)
            {
                for (var j = 0; j < excepted.GetLength(1); j++)
                {
                    if (!actual[i, j].Equals(excepted[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckEqual(Arc[,] excepted, Arc[,] actual)
        {
            for (var i = 0; i < excepted.GetLength(0); i++)
            {
                for (var j = 0; j < excepted.GetLength(1); j++)
                {
                    if (!actual[i, j].Equals(excepted[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckEqual(Dictionary<XWingParameterType, Parameter> excepted, 
            Dictionary<XWingParameterType, Parameter> actual)
        {
            foreach (var key in excepted.Keys)
            {
                if (excepted[key].Value != actual[key].Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
