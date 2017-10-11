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
        float g;
        float h;
        float f;
        public float GValue { get => g; }
        public float HValue { get => h; }
        public float FValue { get => f; }
        public GridNode Parrent { get; private set; }
        public GridPoint Location { get; private set; }

        public GridNode(GridNode parrent, GridPoint location)
        {
            Parrent = parrent;
            Location = location;
            g = parrent.GValue + Extensions.Pyth(parrent, this);
            CalH();
            f = g + h;
        }
        public GridNode(GridNode parrent, int x, int y, bool walkable)
        {
            Parrent = parrent;
            Location = new GridPoint(x, y, walkable);
            g = parrent.GValue + Extensions.Pyth(parrent, this);
            CalH();
            f = g + h;
        }
        public GridNode(GridNode parrent, Point cordinates, bool walkable)
        {
            Parrent = parrent;
            Location = new GridPoint(cordinates, walkable);
            g = parrent.GValue + Extensions.Pyth(parrent, this);
            CalH();
            f = g + h;
        }
        public GridNode(GridNode parrent, int x, int y)
        {
            Parrent = parrent;
            Location = new GridPoint(x, y);
            g = parrent.GValue + Extensions.Pyth(parrent, this);
            CalH();
            f = g + h;
        }
        public GridNode(GridNode parrent, Point cordinates)
        {
            Parrent = parrent;
            Location = new GridPoint(cordinates);
            g = parrent.GValue + Extensions.Pyth(parrent, this);
            CalH();
            f = g + h;
        }
        public GridNode(Point startGoalPoint)
        {
            Parrent = null;
            Location = new GridPoint(startGoalPoint, true);
            f = g = 0;
        }

        public LinkedListNode<GridNode> LLNode { get => new LinkedListNode<GridNode>(this); }

        public float CalH() =>
            h = Extensions.Pyth(Location.Cordinates, MapData.Goal);
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
    }
}
