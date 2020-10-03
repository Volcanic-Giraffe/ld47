using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shadow
{
    public class ShadowCalculator
    {
/*
        List<Vector3> GenerateShadowPolygon(
            Vector3 origin,
            Rectangle3D bounds,
            Line[] obstacles 
        )
        {
            //first - find lines visible directly. i.e. line from origin to both ends deos not intersect other lines.

            Vector3 temp;
            var visibleObstacles = obstacles.Where(line =>
            {
                foreach (var aline in obstacles)
                {
                    if (aline == line) continue;
                    if (Math2d.LineSegmentsIntersection(origin, line.a, aline.a, aline.b, out temp))
                    {
                        return false;
                    }

                    if (Math2d.LineSegmentsIntersection(origin, line.b, aline.a, aline.b, out temp))
                    {
                        return false;
                    }
                }

                return true;
            });
            
            // then cast shadows to bounds
            List<Vector3> output = new List<Vector3>();
            foreach (var obstacle in visibleObstacles)
            {
                var vectorA = (obstacle.a - origin).normalized * 100f;
                var vectorB = (obstacle.b - origin).normalized * 100f;
                var boundLines = bounds.Lines;
                foreach (var bline in boundLines)
                {
                    if (Math2d.LineSegmentsIntersection(origin, origin + vectorA, bline.a, bline.b, out origin))
                    {
                        
                    }
                }
            }
        }*/
    }

    public class Rectangle3D
    {
        public Vector3 halfSize;
        public Vector3 center;

        public Rectangle3D(Vector3 center, Vector3 halfSize)
        {
            this.halfSize = halfSize;
            this.center = center;
        }

        public bool Contains(Vector3 v)
        {
            var relative = v - center;
            return Mathf.Abs(relative.x) <= halfSize.x && Mathf.Abs(relative.z) <= halfSize.y &&
                   Mathf.Abs(relative.z) <= halfSize.z;
        }
        
        public Line[] Lines => new Line[]
        {
            new Line(center + new Vector3(-halfSize.x, 0, halfSize.z), center + new Vector3(halfSize.x, 0, halfSize.z)),
            new Line(center + new Vector3(halfSize.x, 0, halfSize.z), center + new Vector3(halfSize.x, 0, -halfSize.z)),
            new Line(center + new Vector3(halfSize.x, 0, -halfSize.z), center + new Vector3(-halfSize.x, 0, -halfSize.z)),
            new Line(center + new Vector3(-halfSize.x, 0, -halfSize.z), center + new Vector3(-halfSize.x, 0, halfSize.z))
        };
    }

    public class Line
    {
        public Vector3 a;
        public Vector3 b;

        public Line(Vector3 a, Vector3 b)
        {
            this.a = a;
            this.b = b;
        }
    }
}