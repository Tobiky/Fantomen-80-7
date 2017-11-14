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
    public enum WorldGenType
    {
        Random,
        PreFab
    }
    public static class WorldGeneration
    {
        static Random r = new Random();
        [ThreadStatic]
        private static LinkedList<Obstacle> obstacles = new LinkedList<Obstacle>();
        private static LinkedList<Obstacle> tempObstacles = new LinkedList<Obstacle>();
        static int nodeSize;
        static bool repeat;

        public static LinkedList<Obstacle> Obstacles { get => new LinkedList<Obstacle>(obstacles); }
        public static LinkedList<Obstacle> TemporaryObstacles { get => new LinkedList<Obstacle>(tempObstacles); }
        
        public static Texture2D TileTexture { get; set; }
        public static Point Goal { get; set; }
        public static Point Start { get; set; }
        public static int NodeSize
        {
            get => nodeSize;
            set {
                nodeSize = value;
                NumberOfObstacles = (int)Math.Pow(Screen.Width/nodeSize/10,2) * 100 / 4;
            }
        }
        public static int NumberOfObstacles { get; private set; }
        public static Rectangle Screen { get; set; }
        public static WorldGenType WorldGenerationType { get; set; }
        public static bool RepeatPreFab
        {
            get {
                if (WorldGenerationType != WorldGenType.PreFab)
                    return false;
                return repeat;
            }
            set => repeat = value;
        }
        public static PreFab.PreFab WorldPreFab { private get; set; }
        
        public static void Generate(object data = null) //PreFab,
        {
            obstacles.Clear();
            tempObstacles.Clear();
            switch (WorldGenerationType) {
                case WorldGenType.Random:
                    //WorlGen weights, baserat på statistik etc.
                    GenerateRandom(data as object[]);
                    break;
                case WorldGenType.PreFab:
                    LoadPreFab(WorldPreFab);
                    break;
            }
        }

        public static void GenerateRandom(object[] data)
        {
            int tempInt = r.Next(1, 4);
            int temp1 = r.Next(0, Screen.Width / NodeSize - 1) * NodeSize;
            int temp2 = r.Next(0, Screen.Height / NodeSize - 1) * NodeSize;
            switch (tempInt) {
                case 1: // Start is left, goal is right
                    Start = new Point(0, temp1);
                    Goal = new Point(Screen.Height - NodeSize, temp2);
                    break;
                case 2:                                                                             // Start is right, goal is left
                    Start = new Point(Screen.Width - NodeSize, temp1);
                    Goal = new Point(0, temp2);
                    break;
                case 3:                                                                             // Start is top, goal is bottom
                    Start = new Point(temp1, 0);
                    Goal = new Point(temp2, Screen.Height - NodeSize);
                    break;
                case 4:                                                                             // Start is bottom, goal is top          
                    Start = new Point(temp1, Screen.Width - NodeSize);
                    Goal = new Point(temp2, 0);
                    break;
            }


            for (int i = 0; i < NumberOfObstacles; i++) {
                bool flag = true;

                while (flag)                    // Randomizes a location to place a risk and checks if it is occupies or not. Does so until an available spot has been found
                {
                    var location = new Point(r.Next(0, Screen.Width / NodeSize) * NodeSize, r.Next(0, Screen.Height / NodeSize) * NodeSize);
                    // Randomizes an X position for a risk
                    // Randomizes a Y position for a risk
                    var tempRect = new Rectangle(location, new Point(NodeSize, NodeSize));
                    if (location != Start && location != Goal && !Obstacles.Any(obs => obs.ObstacleArea.Intersects(tempRect))) {
                        obstacles.AddLast(tempRect);
                        flag = false;
                    }
                }
            }
        }

        public static void DrawWorld(SpriteBatch sb)
        {
            foreach (var obs in obstacles)
                sb.Draw(TileTexture, obs, Color.Black);
            foreach (var tobs in tempObstacles) {
                sb.Draw(TileTexture, tobs, Color.Yellow);
            }
            sb.Draw(TileTexture, new Rectangle(Start, new Point(NodeSize, NodeSize)), Color.Green);
            sb.Draw(TileTexture, new Rectangle(Goal, new Point(NodeSize, NodeSize)), Color.Red);
        }

        public static void AddTempStacle(Obstacle obs) => tempObstacles.AddLast(obs);

        public static void LoadPreFab(PreFab.PreFab prefab)
        {
            foreach (var item in prefab.Prefab)
                obstacles.AddLast(item);
        }

        public static Google.Protobuf.Collections.RepeatedField<PreFab.Obs> ObstaclesToObs()
        {
            var list = new Google.Protobuf.Collections.RepeatedField<PreFab.Obs>();
            foreach (var item in obstacles) {
                list.Add(new PreFab.Obs{ Pos = item.Location });
            }
            return list;
        }
    }
}
