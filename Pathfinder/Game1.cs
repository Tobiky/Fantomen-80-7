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
        int lowerRight;
        Texture2D defaultTexture;
        Pathfinder pf;
        LinkedList<GridNode> pfPath;
        SpriteFont sf;
        int time;
        int size;

        //RenderTarget2D target;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            //target = new RenderTarget2D(GraphicsDevice, MapData.Screen.Width, MapData.Screen.Height);
            size = 816;
            Window.AllowUserResizing = false;
            graphics.PreferredBackBufferHeight = size;
            graphics.PreferredBackBufferWidth = size;
            MapData.Screen = new Rectangle(0, 0, size, size);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        private void GenerateWorld()
        {
            var tempRand = new Random();
            int tempInt = Window.ClientBounds.Bottom / new Random().Next(1, Window.ClientBounds.Bottom / new Random().Next(1, 1001));
            var rSize = new Point(5, 5);
            for (int i = 0; i < tempInt; i++)
            {
                var p = new Point(tempRand.Next(0, Window.ClientBounds.Right), tempRand.Next(0, Window.ClientBounds.Bottom));
                Rectangle r;
                if (p.X % 6 != 0 || p.Y % 6 != 0)
                {
                    i--;
                    continue;
                }
                if (p == MapData.Goal)
                {
                    i--;
                    continue;
                }
                if (new Random().Next(1, 101) == 1)
                    r = new Rectangle(p, new Point(35,35));
                else
                {
                    r = new Rectangle(p, rSize);
                    if (MapData.Obstacles.Any(obs => obs.Intersects(r)))
                    {
                        i--;
                        continue;
                    }
                }

                MapData.Obstacles.AddLast(r);
            }
        }
        protected override void Initialize()
        {
            MapData.Obstacles = new LinkedList<Rectangle>();
            Func<int, int> nodes = n => n * 5 + n - 1;
            Func<int, int, Point> placement = (x, y) => new Point(x * 6, y * 6);


            MapData.Start = placement(0, 0);
            MapData.Goal = placement(size/6-1, size/6-1); //3 4

            GenerateWorld();

            MapData.Obstacles.AddLast(new Rectangle(408, 102, 5, nodes(100)));
            MapData.Obstacles.AddLast(new Rectangle(0, 18, nodes(8), 5));

            pf = new Pathfinder(MapData.Start, MapData.Goal, 5);

            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            pfPath = pf.AStar().Result;//.Result;
            //sw.Stop();
            //time = sw.Elapsed.Seconds;

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
            sf = Content.Load<SpriteFont>("textStuff");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

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

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Enter))
            {
                MapData.Obstacles.Clear();
                GenerateWorld();
                pf = new Pathfinder(MapData.Start, MapData.Goal, 5);
                pfPath = pf.AStar().Result;
            }

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
            foreach (var node in pf.OpenLLGN)
                spriteBatch.Draw(defaultTexture, new Rectangle(node.Location.Cordinates, new Point(5, 5)), new Color(200, 200, 200));
            foreach (var node in pf.ClosedLLGN)
                spriteBatch.Draw(defaultTexture, new Rectangle(node.Location.Cordinates, new Point(5, 5)), new Color(200, 200, 200));

            LinkedList<GridNode> path = Path(pf.ClosedLLGN);
            foreach (var node in path)
                spriteBatch.Draw(defaultTexture, new Rectangle(node.Location.Cordinates, new Point(5, 5)), new Color(255, 255, 255));
            //spriteBatch.DrawString(sf, $"OpenLLGN:{pf.OpenLLGN.Count}\nClosedLLGN:{pf.ClosedLLGN.Count}\nPath:{path.Count}\nTime:{time}s", new Vector2(0, 125), new Color(255, 255, 255));

            foreach (var obst in MapData.Obstacles)
                spriteBatch.Draw(defaultTexture, obst, Color.Black);


            spriteBatch.Draw(defaultTexture, new Rectangle(MapData.Start, new Point(5, 5)), new Color(0, 255, 0));
            spriteBatch.Draw(defaultTexture, new Rectangle(MapData.Goal, new Point(5, 5)), new Color(255, 0, 0));

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private LinkedList<GridNode> Path(LinkedList<GridNode> closedList)
        {
            var path = new LinkedList<GridNode>();
            var nodeToCheck = closedList.Last();
            while (nodeToCheck.HasParent)
            {
                path.AddFirst(nodeToCheck);
                nodeToCheck = nodeToCheck.Parrent;
            }
            return path;
        }
    }
}
