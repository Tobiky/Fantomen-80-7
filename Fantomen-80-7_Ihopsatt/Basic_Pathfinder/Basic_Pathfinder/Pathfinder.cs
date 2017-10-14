using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
        Point start;
        Point goal;

        int size;

        LinkedList<GridNode> openLLGN;
        LinkedList<GridNode> closedLLGN;

        public int Size => size;
        public LinkedList<GridNode> OpenLLGN => openLLGN;
        public LinkedList<GridNode> ClosedLLGN => closedLLGN;

        public Pathfinder(Point start, Point goal, int size)
        {
            this.start = start;
            this.goal = goal;
            this.size = size;
            openLLGN = new   LinkedList<GridNode>();
            closedLLGN = new LinkedList<GridNode>();

            openLLGN.AddLast(new GridNode(start));
        }

        public async Task<LinkedList<GridNode>> AStar()
        {
            var lockRef = new object();
            while (openLLGN.Count != 0 && openLLGN != null)
            {
                var leastNode = openLLGN.LowestNode();
                openLLGN.Remove(leastNode);
                var successors = SuccessorNodes(leastNode).Result;
                foreach (var successor in successors)
                {
                    if (successor.Location.Cordinates == goal)
                    {
                        closedLLGN.AddLast(leastNode);
                        return closedLLGN;
                    }

                    Func<GridNode, bool> containsWhatever = node =>
                        node.Location.Cordinates == successor.Location.Cordinates;

                    if (openLLGN.Any(containsWhatever) || closedLLGN.Any(containsWhatever))
                        continue;

                    openLLGN.AddLast(successor);
                }
                closedLLGN.AddLast(leastNode);
            }
            return closedLLGN;
        }

        private Task<GridNode[]> SuccessorNodes(GridNode parent)
        {
            //TODO: Fix this nasty ass shit
            //-------------------------------------------------------------------------------------------------------------FIX---------------------------------------------------------------------
            var tcs = new TaskCompletionSource<GridNode[]>();
            List<GridNode> nodes = new List<GridNode>(4);
            Point pos = parent.Location.Cordinates;
            for (int i = -1; i < 2; i += 2)
            {
                var yLocation = new Point(pos.X, pos.Y + i * (size + 1));
                var xLocation = new Point(pos.X + i * (size + 1), pos.Y);

                Action<Point, bool> AddNode = (loc, walkable) => { if (walkable) nodes.Add(new GridNode(parent, loc, walkable)); };
                Func<Point, bool> Walkable = loc =>
                    !MapData.Screen.Contains(loc) || loc == parent.Location.Cordinates ? false : !MapData.Obstacles.Any(obs => obs.Contains(loc));

                AddNode(yLocation, Walkable(yLocation));
                AddNode(xLocation, Walkable(xLocation));
            }
            tcs.SetResult(nodes.ToArray());
            return tcs.Task;
        }

    }
}
