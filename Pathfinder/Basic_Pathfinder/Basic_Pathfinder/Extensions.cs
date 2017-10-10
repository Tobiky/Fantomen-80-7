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

        public static void AddToList(this Node toAdd, LinkedList<Node> list) =>
            list.AddLast(toAdd);
        public static void AddToList(this LinkedListNode<Node> toAdd, LinkedList<Node> list) =>
            list.AddLast(toAdd);
    }
}
