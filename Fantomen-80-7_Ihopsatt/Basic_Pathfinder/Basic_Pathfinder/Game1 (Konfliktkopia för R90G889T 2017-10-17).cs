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
        LinkedList<GridNode> pfPath;
        const int size = 40;

        SpriteFont sf;
        bool showInfo = false;
        int pathTime;
        int worldTime;
        //int lowerRight;

        //RenderTarget2D target;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            //target = new RenderTarget2D(GraphicsDevice, MapData.Screen.Width, MapData.Screen.Height);
            IsMouseVisible = true;
            //Window.AllowUserResizing = false;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
            MapData.Screen = new Rectangle(0, 0, 800, 800);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        private void GenerateWorld()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var tempRand = new Random();
            int tempInt = Window.ClientBounds.Bottom / tempRand.Next(1, Window.ClientBounds.Right / tempRand.Next(1, Window.ClientBounds.Bottom));
            var rSize = new Point(5, 5);

            for (int i = 0; i < tempInt; i++) {
                i--;
                var p = new Point(tempRand.Next(0, Window.ClientBounds.Right), tempRand.Next(0, Window.ClientBounds.Bottom));
                if (p.X % 6 != 0 || p.Y % 6 != 0 || p == MapData.Goal)
                    continue;
                
                var r = new Rectangle(p, rSize);
                if (new Random().Next(1, 101) <= 50) {
                    r.Size = new Point(17, 17);

                    if (r.Contains(MapData.Goal) || r.Contains(MapData.Start)) {
                        continue;
                    }

                } else if (MapData.Obstacles.Any(obs => obs.Intersects(r)) || r.Contains(p)) {
                    continue;
                }
                i++;

                MapData.Obstacles.AddLast(r);
            }
            sw.Stop();
            worldTime = sw.Elapsed.Seconds;
        }

        LinkedList<GridNode> openLLGN = new LinkedList<GridNode>();
        LinkedList<GridNode> closedLLGN = new LinkedList<GridNode>();
        void PathFindAStar()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            openLLGN.Clear();
            closedLLGN.Clear();

            pfPath = Pathfinder.AStar(size, openLLGN, closedLLGN);//.Result;
            sw.Stop();
            pathTime = sw.Elapsed.Seconds;
        }


        protected override void Initialize()
        {
            WorldGeneration.Generate();

            PathFindAStar();

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
            WorldGeneration.TileTexture = Content.Load<Texture2D>("whitesquare");
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
            if (state.IsKeyDown(Keys.Enter)) {
                int obG = GC.GetGeneration(WorldGeneration.Obstacles);
                WorldGeneration.Obstacles.Clear();
                GC.Collect(obG);
                WorldGeneration.Generate();
                PathFindAStar();
            }
            if (state.IsKeyDown(Keys.F11))
                showInfo = !showInfo;

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
            string times = "";
            if (showInfo)
                times += $"PathTime:{pathTime}s\nWorldTime:{worldTime}\nOpenLLGN:{openLLGN.Count}\nClosedLLGN:{closedLLGN.Count}";

            foreach (var node in openLLGN)
                spriteBatch.Draw(WorldGeneration.TileTexture, new Rectangle(node.Location, new Point(size, size)), Color.Gray);

            foreach (var node in closedLLGN)
                spriteBatch.Draw(WorldGeneration.TileTexture, new Rectangle(node.Location, new Point(size, size)), Color.LightGray);

            LinkedList<GridNode> path = Path(pfPath);
            foreach (var node in path)
                spriteBatch.Draw(WorldGeneration.TileTexture, new Rectangle(node.Location, new Point(size, size)), Color.White);

            WorldGeneration.DrawObstacles(spriteBatch);

            WorldGeneration.DrawStartGoal(spriteBatch);

            if (!string.IsNullOrEmpty(times))
                spriteBatch.DrawString(sf, times, new Vector2(6, 6), Color.Red);


            spriteBatch.End();

            base.Draw(gameTime);
        }

        private LinkedList<GridNode> Path(LinkedList<GridNode> closedList)
        {
            var path = new LinkedList<GridNode>();
            var nodeToCheck = closedList.Last();
            while (nodeToCheck.HasParent) {
                path.AddFirst(nodeToCheck);
                nodeToCheck = nodeToCheck.Parrent;
            }
            return path;
        }

        private void Testm()
        {


        }
    }
}
