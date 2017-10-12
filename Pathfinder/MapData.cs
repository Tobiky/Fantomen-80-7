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
        public static Point Start { get; set;  }
        public static Point Goal { get; set; }
        public static Rectangle Screen { get; set; }
        private static bool IsSetUp { get; set; }

        public static LinkedList<Rectangle> Obstacles { get; set; }
    }
}
