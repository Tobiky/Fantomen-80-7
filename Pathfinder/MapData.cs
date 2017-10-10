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
        static Point start = new Point();
        static Point goal = new Point();
        public static Point Start { get => start; }
        public static Point Goal { get => start; }

        private static bool IsSetUp { get; set; }
        public static LinkedList<Rectangle> Obstacles { get; set; }
        public static LinkedList<LinkedList<GridPoint>> GridSystem
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
            var tsc = new TaskCompletionSource<LinkedList<LinkedList<GridPoint>>>();
            IsSetUp = true;
            GridSystem = new LinkedList<LinkedList<GridPoint>>();
            Parallel.For(0, gw.ClientBounds.Bottom, (y) =>
            {
                var tGPL = new LinkedList<GridPoint>();
                GridSystem.AddLast(tGPL);
                Parallel.For(0, gw.ClientBounds.Right, (x) =>
                {
                    var tP = new Point(x, y);
                    bool t = obstacles.Any((obs) => obs.Contains(tP));
                    tGPL.AddLast(new GridPoint(tP, t));
                });
            });
            tsc.SetResult(GridSystem);
            return tsc.Task;
        }
    }
}
