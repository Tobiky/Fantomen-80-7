using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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

        SpriteFont sf;
        SpriteFont nodeSF;
        bool showPathInfo = false;
        bool showNodeInfo = false;
        bool showInvalidPath = false;
        int pathTime;
        float keyDelay;

        PreFab.PreFabLoader pfLoader;
        PreFab.PreFabSaver pfSaver;

        //int lowerRight;

        //RenderTarget2D target;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;

            string directory = Directory.GetCurrentDirectory() + "//PreFabs";
            if (!Directory.GetCurrentDirectory().EndsWith("PreFabs")) {
                Directory.CreateDirectory(directory);
                Directory.SetCurrentDirectory(directory);
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>

        LinkedList<GridNode> openLLGN = new LinkedList<GridNode>();
        LinkedList<GridNode> closedLLGN = new LinkedList<GridNode>();
        public void PathFindAStar()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            openLLGN.Clear();
            closedLLGN.Clear();
            Pathfinder.Pathfind(this, WorldGeneration.NodeSize, openLLGN, closedLLGN);
            pfPath = Pathfinder.Path; //.Result;
            sw.Stop();
            pathTime = sw.Elapsed.Seconds;
        }


        protected override void Initialize()
        {
            WorldGeneration.Screen = new Rectangle(0, 0, 800, 800);
            WorldGeneration.NodeSize = 80;
            WorldGeneration.WorldGenerationType = WorldGenType.Random;
            WorldGeneration.Generate();
            //GridNode.Weight = 5;
            PathFindAStar();
            keyDelay = 0f;
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
            nodeSF = Content.Load<SpriteFont>("NodeSF");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
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
            if (IsActive) {
                if (keyDelay == 0f) {
                    if (state.IsKeyDown(Keys.R)) {
                        WorldGeneration.Generate();
                        PathFindAStar();
                    }
                    else if (state.IsKeyDown(Keys.F11))
                        showPathInfo = !showPathInfo;
                    else if (state.IsKeyDown(Keys.F10))
                        showNodeInfo = !showNodeInfo;
                    else if (state.IsKeyDown(Keys.F9))
                        showInvalidPath = !showInvalidPath;
                    keyDelay = 100f;
                }
                if (state.IsKeyDown(Keys.LeftControl)) {
                    if (state.IsKeyDown(Keys.L)) {
                        pfLoader = new PreFab.PreFabLoader();
                        pfLoader.Show();
                    }
                    else if (state.IsKeyDown(Keys.S)) {
                        pfSaver = new PreFab.PreFabSaver();
                        pfSaver.Show();
                    }
                }
            }
            keyDelay -= gameTime.ElapsedGameTime.Milliseconds > keyDelay ? keyDelay : gameTime.ElapsedGameTime.Milliseconds;
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
            if (showPathInfo)
                times = $"PathTime: {pathTime}s\nOpenLLGN: {openLLGN.Count}\nClosedLLGN: {closedLLGN.Count}\nReachedGoal: {Pathfinder.ReachedGoal}\n" +
                    $"NodeSize: {WorldGeneration.NodeSize}\nObstacleCount: {WorldGeneration.NumberOfObstacles}\n\nStart: {WorldGeneration.Start.ScalePoint()}\n" +
                    $"Goal: {WorldGeneration.Goal.ScalePoint()}";

            void DrawNodeInfo(GridNode node) =>
                spriteBatch.DrawString(
                    nodeSF,
                    $"X:{node.LocationNodeScaled.X}\nY:{node.LocationNodeScaled.Y}",
                    node.Location.ToVector2(),
                    Color.Red);

            void DrawListConditional(LinkedList<GridNode> list, Color listNodeColor, Action<GridNode> drawData, bool drawDataIf)
            {
                foreach (var node in list) {
                    spriteBatch.Draw(WorldGeneration.TileTexture, new Rectangle(node.Location, new Point(WorldGeneration.NodeSize, WorldGeneration.NodeSize)), listNodeColor);
                    if (drawDataIf)
                        drawData(node);
                }
            }

            DrawListConditional(openLLGN, Color.Gray, DrawNodeInfo, showNodeInfo);
            DrawListConditional(closedLLGN, Color.LightGray, DrawNodeInfo, showNodeInfo);
            DrawListConditional(pfPath, Color.White, node => { }, false);
            WorldGeneration.DrawWorld(spriteBatch);
            if (showInvalidPath) {
                DrawListConditional(Pathfinder.InvalidNodes, Color.DarkRed, node => { }, false);
                DrawListConditional(Pathfinder.ChildStartGoals, Color.Magenta, node => { }, false);
            }


            if (!string.IsNullOrEmpty(times))
                spriteBatch.DrawString(sf, times, new Vector2(6, 6), Color.Red);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
