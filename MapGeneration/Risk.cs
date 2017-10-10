using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GymnasieArbete1
{
    public class Risk
    {
        protected Texture2D texture;
        protected Vector2 vector;
        Random r = new Random();

        public Risk () { }

        public Risk(Texture2D texture, int x, int y)
        {
            this.texture = texture;
            this.vector.X = x;     //r.Next(0,19) * 40; //Sets the coordinates for the risks in a gridlike system (prevents overlapping)
            this.vector.Y = y;     //r.Next(0,19) * 40;        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }

        public float X
        {
            get { return vector.X; }
            set { vector.X = value; }
        }

        public float Y
        {
            get { return vector.Y; }
            set { vector.Y = value; }
        }

        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }
    }
}
