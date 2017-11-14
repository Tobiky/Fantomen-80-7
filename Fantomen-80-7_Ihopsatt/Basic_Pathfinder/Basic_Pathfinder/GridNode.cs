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
    public class GridNode
    {
        public static float Weight { get; set; } = 1f;

        public float GValue { get; } = 0;
        public float HValue { get; } = 0;
        public float FValue { get; } = 0;

        public bool ChildStartGoal { get; set; }

        public bool HasParent => Parrent != null;
        public GridNode Parrent { get; set; }
        public Point Location { get; private set; }
        public Point LocationNodeScaled { get => new Point(Location.X / WorldGeneration.NodeSize, Location.Y / WorldGeneration.NodeSize); }

        public GridNode(Point startGoalPoint)
        {
            Parrent = null;
            Location = startGoalPoint;
        }

        public GridNode(GridNode parrent, Point location, Point goal)
        {
            Parrent = parrent;
            Location = location;

            GValue = parrent.GValue + Extensions.Pyth(parrent, this);
            HValue = Weight * Extensions.Pyth(Location, WorldGeneration.Goal);
            FValue = GValue + HValue;

        }

        public GridNode(GridNode parrent, int x, int y, Point goal)
            : this(parrent, new Point(x, y), goal) { }
        //public LinkedListNode<GridNode> LLNode => new LinkedListNode<GridNode>(this);

        public bool Contains(Point location) =>
            location.X >= Location.X &&
            location.X <= Location.X + WorldGeneration.NodeSize &&
            location.Y >= Location.Y &&
            location.Y <= Location.Y + WorldGeneration.NodeSize;
        public bool Contains(int x, int y) =>
            x >= Location.X &&
            x <= Location.X + WorldGeneration.NodeSize &&
            y >= Location.Y &&
            y <= Location.Y + WorldGeneration.NodeSize;

        public override string ToString() => $"GValue : {GValue}, FValue {FValue}, HValue {HValue}, GridPoint {LocationNodeScaled.ToString()}";

        public static implicit operator Point(GridNode node) => node.Location;
    }
}
