using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Pathfinder
{
    class ScrapClass
    {
        //private static bool Connector(GridNode startingNode, int startIndex, LinkedList<Obstacle> tempStacles)
        //{

        //    //GetPath
        //    //Take Last from 'newPath' place Before 'end' in 'path'
        //    var newPath = GetPath(AStar(
        //        size: WorldGeneration.NodeSize,
        //        start: startingNode,
        //        goal: path.ElementAt(startIndex + 1),
        //        openLLGN: new LinkedList<GridNode>(),
        //        closedLLGN: new LinkedList<GridNode>(),
        //        temporaryObstacles: tempStacles.ToArray()
        //        ));

        //    if (newPath.Count == 0)
        //        return false;

        //    childStartGials.AddLast(newPath.First.Value);
        //    childStartGials.AddLast(newPath.Last.Value);

        //    newPath.First.Value.Parrent = path.ElementAt(startIndex);
        //    newPath.Last.Value.Parrent = path.ElementAt(startIndex + 1);
        //    //path.AddBefore(path.Find(path.ElementAt(startIndex + 1)), new LinkedListNode<GridNode>(newPath.Last()));

        //    for (int i = newPath.Count - 1; i > 0; i--) {
        //        path.AddBefore(path.Find(path.ElementAt(startIndex + 1)), newPath.ElementAt(i));
        //    }
        //    return true;
        //}
    }
}
