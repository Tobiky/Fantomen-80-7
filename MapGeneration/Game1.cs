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
        //Risk risk;
        //Coordinates[ ,] coordinateArray = new Coordinates[20, 20];
        List<Risk> riskList = new List<Risk>();
        Random r = new Random();
        List<Coordinates> coordinateList = new List<Coordinates>();
        Texture2D startPoint;
        Texture2D goalPoint;

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

            for (int i = 0; i < 20; i++)                                            //Divides the window into a grid with 40px sized squares
            {
                for (int j = 0; j < 20; j++)
                {
                    //coordinateArray[i, j] = new Coordinates
                    coordinateList.Add(new Coordinates(i, j));
                }
            }

            for (int i = 0; i < 50; i++)                                            //Generates all risks at coordinates from coordinatesList
            {
                int tempPos = r.Next(0, coordinateList.Count());

                riskList.Add(new Risk(Content.Load<Texture2D>("Risk"), coordinateList[tempPos].X, coordinateList[tempPos].Y)); 

                coordinateList.RemoveAt(tempPos);
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

            spriteBatch.Begin();

            for(int i = 0; i < 50; i++)
            {
                riskList[i].Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}

//Perlin noise