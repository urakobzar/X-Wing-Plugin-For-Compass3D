using System.Collections.Generic;
using XWingPluginForCompass3D.Model;

namespace XWingPluginForCompass3D.UnitTests
{
    public class CheckingObjectEquality
    {
        public bool CheckEqual(Point2D[,,] excepted, Point2D[,,] actual)
        {
            for (var i = 0; i < excepted.GetLength(0); i++)
            {
                for (var j = 0; j < excepted.GetLength(1); j++)
                {
                    for (var k = 0; k < excepted.GetLength(2); k++)
                    {
                        if (((excepted[i, j, k].X != actual[i, j, k].X) ||
                             (excepted[i, j, k].Y != actual[i, j, k].Y)))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool CheckEqual(Point3D[] excepted, Point3D[] actual)
        {
            for (var i = 0; i < excepted.GetLength(0); i++)
            {
                if ((excepted[i].X != actual[i].X) ||
                    (excepted[i].Y != actual[i].Y) ||
                    (excepted[i].Z != actual[i].Z))
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

        public bool CheckEqual(Arc excepted, Arc actual)
        {
            return (excepted.Center.X == actual.Center.X) &&
                   (excepted.Center.Y == actual.Center.Y) &&
                   (excepted.Radius == actual.Radius) &&
                   (excepted.StartPoint.X == actual.StartPoint.X) &&
                   (excepted.StartPoint.Y == actual.StartPoint.Y) &&
                   (excepted.EndPoint.X == actual.EndPoint.X) &&
                   (excepted.EndPoint.Y == actual.EndPoint.Y) &&
                   (excepted.Direction == actual.Direction);
        }

        public bool CheckEqual(Circle[,] excepted, Circle[,] actual)
        {
            for (var i = 0; i < excepted.GetLength(0); i++)
            {
                for (var j = 0; j < excepted.GetLength(1); j++)
                {
                    if ((excepted[i, j].Center.X != actual[i, j].Center.X) ||
                         (excepted[i, j].Center.Y != actual[i, j].Center.Y) ||
                         (excepted[i, j].Radius != actual[i, j].Radius))
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
                    if ((excepted[i, j].Center.X != actual[i, j].Center.X) ||
                        (excepted[i, j].Center.Y != actual[i, j].Center.Y) ||
                        (excepted[i, j].Radius != actual[i, j].Radius)||
                        (excepted[i, j].StartPoint.X != actual[i, j].StartPoint.X) ||
                        (excepted[i, j].StartPoint.Y != actual[i, j].StartPoint.Y) ||
                        (excepted[i, j].EndPoint.X != actual[i, j].EndPoint.X) ||
                        (excepted[i, j].EndPoint.Y != actual[i, j].EndPoint.Y) || 
                        (excepted[i, j].Direction != actual[i, j].Direction))
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
