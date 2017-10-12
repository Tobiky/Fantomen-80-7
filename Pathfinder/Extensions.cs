using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static float Pyth(GridPoint a, GridPoint b) =>
            Pyth(a.Cordinates, b.Cordinates);
        public static float Pyth(Point a, Point b) =>
            (float)Math.Sqrt(
                Math.Pow(MathHelper.Distance(a.X, b.X), 2) +
                Math.Pow(MathHelper.Distance(a.Y, b.Y), 2)
                );
        public static GridNode LowestNode(this GridNode[] nodes)
        {
            GridNode lowNode = nodes[0];
            foreach (var node in nodes)
                if (node.FValue < lowNode.FValue)
                    lowNode = node;
            return lowNode;
        }
        public static GridNode LowestNode(this LinkedList<GridNode> nodes)
        {
            GridNode lowNode = nodes.ElementAt(0);
            foreach (var node in nodes)
                if (node.FValue < lowNode.FValue)
                    lowNode = node;
            return lowNode;
        }
        public static bool SameValues(this GridPoint a, GridPoint b) =>
            a.Walkable && b.Walkable && a.Cordinates.X == b.Cordinates.X && a.Cordinates.Y == b.Cordinates.Y;
        
    }
}
