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
    public static class MapData
    {
        private static bool IsSetUp { get; set; }
        public static LinkedList<Rectangle> Obstacles { get; set; }
        public static LinkedList<LinkedList<GridNode>> GridSystem
        {
            get
            {
                if (IsSetUp)
                    return GridSystem;
                else
                    throw new Exception("GridSystem has not been initialized!");
            }
            private set => GridSystem = value;
        }
        public static Task SetMap(GameWindow gw, params Rectangle[] obstacles)
        {
            var tsc = new TaskCompletionSource<LinkedList<LinkedList<GridNode>>>();
            IsSetUp = true;
            GridSystem = new LinkedList<LinkedList<GridNode>>();
            Parallel.For(0, gw.ClientBounds.Bottom, (y) =>
            {
                var tGPL = new LinkedList<GridNode>();
                GridSystem.AddLast(tGPL);
                Parallel.For(0, gw.ClientBounds.Right, (x) =>
                {
                    var tGP = new GridNode(x, y);
                    tGPL.AddLast(tGP);
                    Parallel.ForEach(obstacles, (obs) => tGP.Walkable = obs.Contains(tGP.Cordinates) );
                });
            });
            tsc.SetResult(GridSystem);
            return tsc.Task;
        }
    }
}
