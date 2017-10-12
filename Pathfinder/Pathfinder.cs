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
        Point start;
        Point goal;

        LinkedList<GridNode> openLLGN;
        LinkedList<GridNode> closedLLGN;

        public LinkedList<GridNode> OpenLLGN => openLLGN;
        public LinkedList<GridNode> ClosedLLGN => closedLLGN;

        public Pathfinder(Point start, Point goal)
        {
            this.start = start;
            this.goal = goal;
            openLLGN = new LinkedList<GridNode>();
            closedLLGN = new LinkedList<GridNode>();

            openLLGN.AddLast(new GridNode(start));
        }

        public Task<LinkedList<GridNode>> AStar() //Task<LinkedList<GridNode>>
        {
            var tcs = new TaskCompletionSource<LinkedList<GridNode>>();
            while (openLLGN.Count != 0 && openLLGN != null)
            {
                var leastNode = openLLGN.LowestNode();
                openLLGN.Remove(leastNode);
                var successors = SuccessorNodes(leastNode);
                foreach (var successor in successors)
                {
                    if (successor.Location.Cordinates == goal)
                    {
                        tcs.SetResult(closedLLGN);
                        return tcs.Task;
                    }

                    Func<GridNode, bool> containsWhatever = node => 
                        node.Location.Cordinates == successor.Location.Cordinates && node.FValue < successor.FValue;

                    if (openLLGN.Any(containsWhatever) || closedLLGN.Any(containsWhatever))
                        continue;

                    openLLGN.AddLast(successor);
                }
                closedLLGN.AddLast(leastNode);
            }
            return tcs.Task;
        }

        private GridNode[] SuccessorNodes(GridNode parent)
        {
            List<GridNode> nodes = new List<GridNode>(8);
            Point pos = parent.Location.Cordinates;

            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    var location = new Point(pos.X + x, pos.Y + y);
                    if (!MapData.Screen.Contains(location) || location == parent.Location.Cordinates)
                        continue;

                    bool walkable = !MapData.Obstacles.Any(obs => obs.Contains(location));

                    if (walkable)
                    {
                        var tempPoint = new GridPoint(location, walkable);
                        var tempNode = new GridNode(parent, tempPoint);
                        nodes.Add(tempNode);
                    }
                }
            }

            return nodes.ToArray();
        }
    }
}
