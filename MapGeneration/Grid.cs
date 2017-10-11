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
    public class Grid
    {
        Texture2D texture;
        Vector2 vector;
        // Rectangle rectangle;
        // Color color;
        bool isStart = false;
        bool isGoal;
        bool isRisk;            // Swap to double later
        double riskValue;

        public Grid () { }

        public Grid(float x, float y)
        {
            // color = Color.White;
            // this.rectangle.Height = 40;
            // this.rectangle.Width = 40;
            this.vector.X = x * 40;
            this.vector.Y = y * 40;
            isStart = false;
            isGoal = false;
            isRisk = false;
        }

        // public Rectangle Rectangle { get { return rectangle; } }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        
        public Vector2 Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        public bool IsStart
        {
            get { return isStart; }
            set { isStart = value; }
        }

        public bool IsGoal
        {
            get { return isGoal; }
            set { isGoal = value; }
        }

        public bool IsRisk
        {
            get { return isRisk; }
            set { isRisk = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }

        /*
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, vector, rectangle, color);
        }
        */
    }
}
