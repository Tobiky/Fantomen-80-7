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
        public float GValue { get; } = 0;
        public float HValue { get; } = 0;
        public float FValue { get; } = 0;

        public bool HasParent => Parrent != null;
        public GridNode Parrent { get; private set; }
        public GridPoint Location { get; private set; }

        public GridNode(Point startGoalPoint)
        {
            Parrent = null;
            Location = new GridPoint(startGoalPoint, true);
        }

        public GridNode(GridNode parrent, GridPoint location)
        {
            Parrent = parrent;
            Location = location;

            GValue = parrent.GValue + Extensions.Pyth(parrent, this);
            HValue = Extensions.Pyth(Location.Cordinates, MapData.Goal);
            FValue = GValue + HValue;

        }

        public GridNode(GridNode parrent, int x, int y, bool walkable)
            : this(parrent, new GridPoint(x, y, walkable)) { }

        public GridNode(GridNode parrent, Point cordinates, bool walkable)
            : this(parrent, new GridPoint(cordinates, walkable)) { }

        public GridNode(GridNode parrent, int x, int y)
            : this(parrent, new GridPoint(x, y)) { }

        public GridNode(GridNode parrent, Point cordinates)
            : this(parrent, new GridPoint(cordinates)) { }
        //public LinkedListNode<GridNode> LLNode => new LinkedListNode<GridNode>(this);


        public override string ToString() => $"GValue : {GValue}, FValue {FValue}, HValue {HValue}, GridPoint {Location.ToString()}";
    }

    public struct GridPoint
    {
        public Point Cordinates { get; private set; }
        public bool Walkable { get; private set; }

        public GridPoint(Point cords)
        {
            Cordinates = cords;
            Walkable = false;
        }
        public GridPoint(int x, int y)
        {
            Cordinates = new Point(x, y);
            Walkable = false;
        }
        public GridPoint(Point cords, bool walkable)
        {
            Cordinates = cords;
            Walkable = walkable;
        }
        public GridPoint(int x, int y, bool walkable)
        {
            Cordinates = new Point(x, y);
            Walkable = walkable;
        }

        public static GridPoint operator +(GridPoint a, GridPoint b) =>
            new GridPoint(a.Cordinates.CombinePoints(b.Cordinates), a.Walkable);

        public override string ToString() => $"Location {Cordinates.ToString()}, Walkable {Walkable.ToString()}";


    }
}
