using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
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
        static LinkedList<GridNode> path = new LinkedList<GridNode>();
        public static LinkedList<GridNode> Path => new LinkedList<GridNode>(path);

        static LinkedList<GridNode> invalidNodes = new LinkedList<GridNode>();
        public static LinkedList<GridNode> InvalidNodes => new LinkedList<GridNode>(invalidNodes);

        static LinkedList<GridNode> childStartGials = new LinkedList<GridNode>();
        public static LinkedList<GridNode> ChildStartGoals => new LinkedList<GridNode>(childStartGials);

        public static bool ReachedGoal { get; private set; }

        public static void Pathfind(Game g, int size, LinkedList<GridNode> openLLGN, LinkedList<GridNode> closedLLGN)
        {
            invalidNodes.Clear();
            childStartGials.Clear();
            path = GetPath(AStar(size, WorldGeneration.Start, WorldGeneration.Goal, openLLGN, closedLLGN));
            //while (invalidNodes.Count != 0)
            //    Checker();
            if (closedLLGN.Any(node => node.Location == WorldGeneration.Goal))
                ReachedGoal = true;
            else
                ReachedGoal = false;

            //Checker();
        }

        private static LinkedList<GridNode> AStar(int size, Point start, Point goal, LinkedList<GridNode> openLLGN, LinkedList<GridNode> closedLLGN, params Obstacle[] temporaryObstacles)
        {
            ReachedGoal = false;
            openLLGN.AddLast(new GridNode(start));
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
                            if (temporaryObstacles != null && temporaryObstacles.Any(obs => obs.ObstacleArea.Contains(loc)))
                                return;
                            if (obstacles.Any(obs => obs.ObstacleArea.Contains(loc)))
                                return;
                            nodes.Add(new GridNode(leastNode, loc, goal));
                        };
                    }
                    return nodes.ToArray();
                }
            }
            ReachedGoal = false;
            return closedLLGN;
        }

        private static LinkedList<GridNode> GetPath(LinkedList<GridNode> closedList)
        {
            var newPath = new LinkedList<GridNode>();
            var nodeToCheck = closedList.Last();
            while (nodeToCheck.HasParent) {
                newPath.AddFirst(nodeToCheck);
                nodeToCheck = nodeToCheck.Parrent;
            }
            return newPath;
        }

        //TODO: Checker, Remover, Replacer(, Connector) multithreaded
        private static void Checker()
        {
            List<GridNode> pathList = path.ToList();
            //                                          Gridnoden för de delar som har tagits bort
            var gridnodeStartingPoints = new HashSet<GridNode>();
            
            //SAME: Ifall det finns en node på samma x- eller y-axel två steg bort 
            bool Same(int i, int additional)
            {
                if (i - 2 - additional < 0 || i + 2 + additional > pathList.Count - 1)
                    return false;
                bool firstSameX = pathList[i - 2 - additional].Location.X == pathList[i].Location.X && pathList[i - 2 - additional].Location.Y != pathList[i].Location.Y;
                bool firstSameY = pathList[i - 2 - additional].Location.Y == pathList[i].Location.Y && pathList[i - 2 - additional].Location.X != pathList[i].Location.X;
                bool secondSameX = pathList[i + 2 + additional].Location.X == pathList[i].Location.X && pathList[i + 2 + additional].Location.Y != pathList[i].Location.Y;
                bool secondSameY = pathList[i + 2 + additional].Location.Y == pathList[i].Location.Y && pathList[i + 2 + additional].Location.X != pathList[i].Location.X;
                return (firstSameX || secondSameX) && (firstSameY || secondSameY);
            }

            void AddListRange(int index, int range)
            {
                for (int i = index - range; i <= index + range; i++)
                    invalidNodes.AddLast(pathList[i]);
            }

            for (int i = 2; i < pathList.Count; i++) {
                int skip = 2;
                if (Same(i, 0)) {
                    //SAME fast [i +/- 2 +/- j], ska kolla ifall det finns några nodes innan/efter som ligger på samma x- eller y-axel
                    for (int j = 1; Same(i, j); j++, skip++) { }
                    AddListRange(i, skip);
                    gridnodeStartingPoints.Add(i == skip ? path.First() : path.ElementAt(i - skip - 1));
                    Obstacle temp = new Obstacle(new Rectangle(pathList[i].Location, new Point(WorldGeneration.NodeSize)));
                    if (!WorldGeneration.Obstacles.Contains(temp)) {
                        WorldGeneration.AddTempStacle(temp);
                    }
                    i += skip;
                }
            }
            if (gridnodeStartingPoints.Count > 0) {
                //Ta bort path
                //Gör ny path genom att köra AStar mellan Start, startingNodes, goal
                Replacer(gridnodeStartingPoints, WorldGeneration.TemporaryObstacles);
            }
        }

        //Path -> start - startingnode - startingnode - ... - startingnode - goal
        private static void Replacer(HashSet<GridNode> startingNodes, LinkedList<Obstacle> tempObstacles)
        {
            //TODO: Multithread

        }

        private static void LogException(Action a)
        {
            try {
                a();
            }
            catch (Exception e) {
                int crashNumber = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.dat").Select(file => file.StartsWith("Crash_")).Count() + 1;
                PreFab.PreFabUser.Write($"Crash_{crashNumber}.dat");
                System.IO.File.WriteAllText($"CrashReport_{crashNumber}.txt", e.Source + "\n\n" + e.Message);
            }
            finally {
                WorldGeneration.Generate();
                //Program.GameT.PathFindAStar();
            }
        }
    }
}
