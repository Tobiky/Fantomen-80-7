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
        Rectangle rectangle;
        Color color;
        bool isStart;
        bool isGoal;
        bool isRisk;            // Swap to double later
        double riskValue;

        public Grid () { }

        public Grid(int x, int y)
        {
            rectangle.Height = 40;
            rectangle.Width = 40;
            rectangle.X = x * 40;
            rectangle.Y = y * 40;            
            isStart = false;
            isGoal = false;
            isRisk = false;
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

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
            spriteBatch.Draw(Texture, rectangle, color);
        }

        /*
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }
        */
    }
}
