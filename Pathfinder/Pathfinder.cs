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
    public class Pathfinder
    {
        Point Start { get; set; }
        Point Goal { get; set; }

        LinkedList<GridNode> OpenLLGN = new LinkedList<GridNode>();
        LinkedList<GridNode> ClosedLLGN = new LinkedList<GridNode>();

        public Pathfinder(Point start, Point goal)
        {
            Start = start;
            Goal = goal;
        }
    }
}
