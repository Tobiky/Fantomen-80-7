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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D defaultTexture;
        Color defaultColor;
        Pathfinder pf;
        LinkedList<GridNode> pfPath;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = false;
            graphics.PreferredBackBufferHeight = 100;
            graphics.PreferredBackBufferWidth = 100;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            defaultColor = new Color(new Random().Next(0, 256), new Random().Next(0, 256), new Random().Next(0, 256));

            MapData.Obstacles = new LinkedList<Rectangle>();

            var tempRand = new Random();
            //int tempInt = Window.ClientBounds.Bottom / new Random().Next(1, Window.ClientBounds.Bottom / new Random().Next(1,101));
            //for (int i = 0; i < tempInt; i++)
            //    MapData.Obstacles.AddLast(new Rectangle(tempRand.Next(0, Window.ClientBounds.Right), tempRand.Next(0, Window.ClientBounds.Bottom), 30, 12));
            MapData.Obstacles.AddLast(new Rectangle(tempRand.Next(50, 150), tempRand.Next(50,150), 30, 12));

            MapData.Start = new Point(1, 99);
            MapData.Goal = new Point(99, 1);
            pf = new Pathfinder(MapData.Start, MapData.Goal);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultTexture = Content.Load<Texture2D>("whitesquare");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            pfPath = pf.AStar();//.Result;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (var obst in MapData.Obstacles)
                spriteBatch.Draw(defaultTexture, obst, defaultColor);
            foreach (var node in pfPath)
                spriteBatch.Draw(defaultTexture, new Rectangle(node.Location.Cordinates, new Point(1, 1)), new Color(0, 161, 255));

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
