using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PetriTray_MG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BasicEffect effect;
        Blob player;

        BasicGeometry ball;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = Camera.Main.Resolution.Height;
            graphics.PreferredBackBufferWidth = Camera.Main.Resolution.Width;
            graphics.ApplyChanges();

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
            ball = BasicGeometry.CreateSphere(GraphicsDevice);
            ball.Effect.EnableDefaultLighting();
            player = new Blob(Vector3.Zero, GraphicsDevice, Content.Load<Effect>("Metaball"));
            player.AddBall(new Models.Metaball());
            player.metaballs[0].Parameters.X = 0.1f;
            //Console.WriteLine(GraphicsDevice.Viewport.Project(new Vector3(-200, 0, 0), Camera.Main.Projection, Camera.Main.View, Matrix.Identity));



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

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                InputHandling.InputHandler.KeyboardInputs(Keyboard.GetState().GetPressedKeys());
            }
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

            // TODO: Add your drawing code here
            //ball.Draw(Matrix.Identity, Camera.Main.View, Camera.Main.Projection);
            player.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
