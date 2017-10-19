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
    public static class Pathfinder
    {
        public static bool ReachedGoal { get; private set; }

        public static LinkedList<GridNode> AStar(int size, LinkedList<GridNode> openLLGN, LinkedList<GridNode> closedLLGN)
        {
            ReachedGoal = false;
            openLLGN.AddLast(new GridNode(WorldGeneration.Start));
            Point goal = WorldGeneration.Goal;
            while (openLLGN.Count != 0) {
                var leastNode = openLLGN.LowestNode();
                openLLGN.Remove(leastNode);
                var successors = SuccessorNodes();
                foreach (var successor in successors) {
                    if (successor.Location == goal) {
                        closedLLGN.AddLast(leastNode);
                        ReachedGoal = true;
                        return closedLLGN;
                    }

                    bool containsNodeOfSameCordinates(GridNode node) => node.Location == successor.Location;
                    if (openLLGN.Any(containsNodeOfSameCordinates) || closedLLGN.Any(containsNodeOfSameCordinates))
                        continue;

                    openLLGN.AddLast(successor);
                }
                closedLLGN.AddLast(leastNode);

                GridNode[] SuccessorNodes()
                {
                    List<GridNode> nodes = new List<GridNode>(4);
                    Point pos = leastNode.Location;
                    int screenW = WorldGeneration.Screen.Width;
                    int screenH = WorldGeneration.Screen.Height;
                    LinkedList<Obstacle> obstacles = WorldGeneration.Obstacles;

                    for (int i = -1; i < 2; i += 2) {   
                        var yLocation = new Point(pos.X, pos.Y + i * size);
                        var xLocation = new Point(pos.X + i * size, pos.Y);

                        AddNodeIfWalkable(yLocation);
                        AddNodeIfWalkable(xLocation);

                        void AddNodeIfWalkable(Point loc)
                        {
                            if (loc == leastNode.Location)
                                return;
                            if (loc.X < 0 || loc.X >= screenW || loc.Y < 0 || loc.Y >= screenH)
                                return;
                            if (obstacles.Any(obs => obs.ObstacleArea.Contains(loc)))
                                return;
                            nodes.Add(new GridNode(leastNode, loc));
                        };
                    }
                    return nodes.ToArray();
                }
            }
            ReachedGoal = false;
            return closedLLGN;
        }
    }
}
