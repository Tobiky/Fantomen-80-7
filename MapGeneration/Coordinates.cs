using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymnasieArbete1
{
    class Coordinates
    {
        int x;
        int y;
        bool isDangerous;

        public Coordinates () { }

        public Coordinates (int x, int y)
        {
            this.x = x * 40;
            this.y = y * 40;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public bool IsDangerous
        {
            get { return isDangerous; }
            set { isDangerous = value; }
        }
    }
}
