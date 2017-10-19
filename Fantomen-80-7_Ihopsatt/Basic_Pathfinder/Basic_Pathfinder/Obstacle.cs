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
    public struct Obstacle
    {
        public Rectangle ObstacleArea { get; set; }
        public Point Size { get => ObstacleArea.Size; }
        public int Width { get => ObstacleArea.Width; }
        public int Height { get => ObstacleArea.Height; }
        public Point Location { get => ObstacleArea.Location; }
        public int X { get => ObstacleArea.X; }
        public int Y { get => ObstacleArea.Y; }

        public double Risk { get; set; }

        public Obstacle(Rectangle area, double risk)
        {
            ObstacleArea = area;
            Risk = risk;
        }
        public Obstacle(Point location, Point size, double risk) :
            this(new Rectangle(location, size), risk){ }
        public Obstacle(int x, int y, int width, int heigh, double risk) :
            this(new Rectangle(x, y, width, heigh), risk){ }
        public Obstacle(Rectangle area) :
            this(area, 0){ }
        public Obstacle(Point location, Point size) :
            this(location, size, 0){ }
        public Obstacle(int x, int y, int width, int height) :
            this(x, y, width, height, 0){ }

        private static double MergeRisk(params double[] riskValues)
        {
            return 0;
        }
        private static double MergeRisk(params Obstacle[] obstacles) => MergeRisk(obstacles.Select(obs => obs.Risk).ToArray());

        public static Obstacle operator +(Obstacle a, Obstacle b) =>
            new Obstacle(a.ObstacleArea.Size + b.ObstacleArea.Size, a.ObstacleArea.Size+ b.ObstacleArea.Size, MergeRisk(a, b));
        public static implicit operator Obstacle(Rectangle rect) =>
            new Obstacle(rect);
        public static implicit operator Rectangle(Obstacle obs) =>
            obs.ObstacleArea;
    }
    class Test
    {
        Obstacle o = new Obstacle();
    }
}
