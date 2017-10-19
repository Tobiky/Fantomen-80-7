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

        public bool HasParent => Parrent != null;
        public GridNode Parrent { get; private set; }
        public Point Location { get; private set; }
        public Point LocationNodeScaled { get => new Point(Location.X / WorldGeneration.NodeSize, Location.Y / WorldGeneration.NodeSize); }

        public GridNode(Point startGoalPoint)
        {
            Parrent = null;
            Location = startGoalPoint;
        }

        public GridNode(GridNode parrent, Point location)
        {
            Parrent = parrent;
            Location = location;

            GValue = parrent.GValue + Extensions.Pyth(parrent, this);
            HValue = Weight * Extensions.Pyth(Location, MapData.Goal);
            FValue = GValue + HValue;

        }

        public GridNode(GridNode parrent, int x, int y)
            : this(parrent, new Point(x, y)) { }
        //public LinkedListNode<GridNode> LLNode => new LinkedListNode<GridNode>(this);


        public override string ToString() => $"GValue : {GValue}, FValue {FValue}, HValue {HValue}, GridPoint {LocationNodeScaled.ToString()}";
    }
}
