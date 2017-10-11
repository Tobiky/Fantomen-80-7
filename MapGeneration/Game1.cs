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
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Grid[ ,] riskArray = new Grid[20, 20];                           //Enter risks at various points
        Random r = new Random();                                    // Random variable

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;        //Sets the width of the window
            graphics.PreferredBackBufferHeight = 800;       //Sets the height of the window
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);          

            for (int x = 0; x < 20; x++)                            
            {
                for (int y = 0; y < 20; y++)
                {
                    riskArray[x, y] = new Grid(x, y);                           // Creates an empty grid block for every coordinate
                }
            }

            int tempInt = r.Next(1, 4);

            switch (tempInt)
                {
                case 1:                                                                              // Start is left, goal is right
                    tempInt = r.Next(0, 19);
                    riskArray[0, tempInt].Texture = Content.Load<Texture2D>("Start");
                    riskArray[0, tempInt].IsStart = true;
                    tempInt = r.Next(0, 19);
                    riskArray[19, tempInt].Texture = Content.Load<Texture2D>("Goal");
                    riskArray[19, tempInt].IsGoal = true;
                    break;
                case 2:                                                                             // Start is right, goal is left
                    tempInt = r.Next(0, 19);
                    riskArray[19, tempInt].Texture = Content.Load<Texture2D>("Start");
                    riskArray[19, tempInt].IsStart = true;
                    tempInt = r.Next(0, 19);
                    riskArray[0, tempInt].Texture = Content.Load<Texture2D>("Goal");
                    riskArray[0, tempInt].IsGoal = true;
                    break;
                case 3:                                                                             // Start is top, goal is bottom
                    tempInt = r.Next(0, 19);
                    riskArray[tempInt, 0].Texture = Content.Load<Texture2D>("Start");
                    riskArray[tempInt, 0].IsStart = true;
                    tempInt = r.Next(0, 19);
                    riskArray[tempInt, 19].Texture = Content.Load<Texture2D>("Goal");
                    riskArray[tempInt, 19].IsGoal = true;
                    break;
                case 4:                                                                             // Start is bottom, goal is top          
                    tempInt = r.Next(0, 19);
                    riskArray[tempInt, 19].Texture = Content.Load<Texture2D>("Start");
                    riskArray[tempInt, 19].IsStart = true;
                    tempInt = r.Next(0, 19);
                    riskArray[tempInt, 0].Texture = Content.Load<Texture2D>("Goal");
                    riskArray[tempInt, 0].IsGoal = true;
                    break;
            }


            for (int i = 0; i < 100; i++)
            {
                bool flag = true;

                while (flag == true)                    // Randomizes a location to place a risk and checks if it is occupies or not. Does so until an available spot has been found
                {
                    int tempX = r.Next(0, 19);
                    int tempY = r.Next(0, 19);
                    if (riskArray[tempX, tempY].IsRisk == false && riskArray[tempX, tempY].IsStart == false && riskArray[tempX, tempY].IsGoal == false)
                    {
                        riskArray[tempX, tempY].IsRisk = true;
                        riskArray[tempX, tempY].Texture = Content.Load<Texture2D>("Risk");
                        flag = false;
                    }
                }
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();                                        // Begins drawing process

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (riskArray[i, j].Texture != null)                    // Only draws object if a texture is available to avoid crashing
                    {
                        riskArray[i, j].Draw(spriteBatch);
                    }
                }
            }
            spriteBatch.End();                                          // Ends drawing process
        }
    }
}


//Perlin noise