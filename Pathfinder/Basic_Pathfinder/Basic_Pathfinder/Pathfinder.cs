using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//http://blog.two-cats.com/2014/06/a-star-example/
//https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
//http://mat.uab.cat/~alseda/MasterOpt/AStar-Algorithm.pdf
//http://web.mit.edu/eranki/www/tutorials/search/

namespace Basic_Pathfinder
{
    public class Node
    {
        public LinkedListNode<Node> GetListNode { get => new LinkedListNode<Node>(this); }

    }
    public class Pathfinder
    {
        public Point Start { get; set; }
        public Point Goal { get; set; }
        public bool[,] Map { get; private set; }

        public Pathfinder()
        {
            
        }
    }

    public struct GridNode
    {
        public Point Cordinates { get; set; }
        public bool Walkable { get; set; }

        public LinkedListNode<GridNode> LLNode { get => new LinkedListNode<GridNode>(this); }

        public GridNode(Point cords)
        {
            Cordinates = cords;
            Walkable = false;
        }
        public GridNode(int x, int y)
        {
            Cordinates = new Point(x, y);
            Walkable = false;
        }
        public GridNode(Point cords, bool walkable)
        {
            Cordinates = cords;
            Walkable = walkable;
        }
        public GridNode(int x, int y, bool walkable)
        {
            Cordinates = new Point(x, y);
            Walkable = walkable;
        }
        public static GridNode operator +(GridNode a, GridNode b) =>
            new GridNode(a.Cordinates.CombinePoints(b.Cordinates), a.Walkable);
    }
}
