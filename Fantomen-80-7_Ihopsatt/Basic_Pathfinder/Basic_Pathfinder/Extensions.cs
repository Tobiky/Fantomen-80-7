using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Basic_Pathfinder
{
    public static class Extensions
    {
        public static Point CombinePoints(this Point a, Point b) =>
            new Point(a.X + b.X, a.Y + b.Y);

        public static float Pyth(GridNode a, GridNode b) =>
            Pyth(a.Location, b.Location);
        public static float Pyth(Point a, Point b) =>
            (float)Math.Sqrt(
                Math.Pow(MathHelper.Distance(a.X, b.X), 2) +
                Math.Pow(MathHelper.Distance(a.Y, b.Y), 2)
                );
        public static GridNode LowestNode(this LinkedList<GridNode> nodes)
        {
            var lowNode = nodes.First();
            foreach (var item in nodes)
                if (item.FValue < lowNode.FValue)
                    lowNode = item;
            return lowNode;
        }
        public static int IndexOf<T>(this LinkedList<T> list, T item)
        {
            var count = 0;
            for (var node = list.First; node != null; node = node.Next, count++) {
                if (item.Equals(node.Value))
                    return count;
            }
            return -1;
        }
        public static int IndexOf(this LinkedList<GridNode> list, Point item)
        {
            var count = 0;
            for (var node = list.First; node != null; node = node.Next, count++) {
                if (node.Value.Location == item)
                    return count;
            }
            return -1;
        }
        public static Point ScalePoint(this Point p) => new Point(p.X/WorldGeneration.NodeSize, p.Y/WorldGeneration.NodeSize);
    }
}
