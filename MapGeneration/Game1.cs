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
                     
            //todo skrv 20 som längeden av riskarray i den första (getlength(0))
            for (int x = 0; x < 20; x++)                            
            {
                for (int y = 0; y < 20; y++)
                {
                    riskArray[x, y] = new Grid(x, y);                           // Creates an empty grid block for every coordinate
                }
            }

            // Start and Goal point generator
            int tempInt = r.Next(1, 4);
            int temp1 = r.Next(0, 19);
            int temp2 = r.Next(0, 19);
            switch (tempInt)
                {
                case 1:                                                                              // Start is left, goal is right
                    StartGoalPoint(0, 19, temp1, temp2);
                    break;
                case 2:                                                                             // Start is right, goal is left
                    StartGoalPoint(19, 0, temp1, temp2);
                    break;
                case 3:                                                                             // Start is top, goal is bottom
                    StartGoalPoint(temp1, temp2, 0, 19);
                    break;
                case 4:                                                                             // Start is bottom, goal is top          
                    StartGoalPoint(temp1, temp2, 19, 0);
                    break;
            }

            for (int i = 0; i < 100; i++)
            {
                bool flag = true;

                while (flag == true)                    // Randomizes a location to place a risk and checks if it is occupies or not. Does so until an available spot has been found
                {
                    int tempX = r.Next(0, 20);         // Randomizes an X position for a risk
                    int tempY = r.Next(0, 20);         // Randomizes a Y position for a risk
                    if (riskArray[tempX, tempY].IsRisk == false && riskArray[tempX, tempY].IsStart == false && riskArray[tempX, tempY].IsGoal == false)
                    {
                        riskArray[tempX, tempY].IsRisk = true;
                        riskArray[tempX, tempY].Texture = Content.Load<Texture2D>("Empty");
                        riskArray[tempX, tempY].Color = Color.DarkRed;
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

        void StartGoalPoint(int x1, int x2, int y1, int y2) 
        {
            riskArray[x1, y1].Texture = Content.Load<Texture2D>("Empty");
            riskArray[x1, y1].Color = Color.DarkSlateGray;
            riskArray[x1, y1].IsStart = true;
            riskArray[x2, y2].Texture = Content.Load<Texture2D>("Empty");
            riskArray[x2, y2].Color = Color.DarkOliveGreen;
            riskArray[x2, y2].IsGoal = true;
        }
    }
}

//Perin noise