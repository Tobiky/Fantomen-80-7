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
    public static class WorldGeneration
    {
        static Random r = new Random();
        public static LinkedList<Obstacle> Obstacles { get; } = new LinkedList<Obstacle>();
        public static Texture2D TileTexture { get; set; }
        public static Point Goal { get; set; }
        public static Point Start { get; set; }
        static int nodeSize;
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

        public static void Generate()
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
                        Obstacles.AddLast(tempRect);
                        flag = false;
                    }
                }
            }
        }

        public static void DrawWorld(SpriteBatch sb)
        {
            foreach (var obs in Obstacles)
                sb.Draw(TileTexture, obs, Color.Black);
            sb.Draw(TileTexture, new Rectangle(Start, new Point(NodeSize, NodeSize)), Color.Green);
            sb.Draw(TileTexture, new Rectangle(Goal, new Point(NodeSize, NodeSize)), Color.Red);
        }
    }
}
