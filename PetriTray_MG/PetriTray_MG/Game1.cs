using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
        Blob player, thing;


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
            player = new Blob(Vector3.Zero, GraphicsDevice, Content.Load<Effect>("Metaball"));
            player.AddBall(new Models.Metaball(new Vector3(0.3f, -0.5f, 0), new Vector4(0,1,0,1), 0.1f));
            player.AddBall(new Models.Metaball(new Vector3(0.5f, -0.5f, 0), new Vector4(1, 0, 0, 1), 0.1f));
            player.AddBall(new Models.Metaball(new Vector3(0.7f, -0.3f, 0), new Vector4(0, 0, 1, 1), 0.1f));
            player.AddBall(new Models.Metaball(new Vector3(0.6f, -0.7f, 0), new Vector4(0, 0.5f, 0.5f, 1), 0.1f));

            thing = new Blob(new Vector3(400, 0, 0), GraphicsDevice, Content.Load<Effect>("Metaball"));
            thing.AddBall(new Models.Metaball(new Vector3(0.5f, -0.5f, 0), new Vector4(0, 1, 0, 1), 0.2f));

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
        float rotation = 0;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        Stopwatch watch = new Stopwatch();
        protected override void Draw(GameTime gameTime)
        {
            watch.Stop();
            //Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Restart();
            // TODO: Add your drawing code here
            player.Draw(spriteBatch, GraphicsDevice, gameTime);
            thing.Draw(spriteBatch, GraphicsDevice, gameTime);
            Vector2 shift = Camera.Main.GetTopLeft() - Camera.Main.GetXY();
            
            rotation += 0.01f;
            if (rotation > 6.28f) { rotation = 0; }
            
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Purple);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
                spriteBatch.Draw(player.sprite.Sprite, new Vector2(500, 500), null, null, new Vector2(128, 128), rotation, new Vector2(rotation, rotation), Color.White);
                spriteBatch.Draw(thing.sprite.Sprite, new Vector2(thing.sprite.WorldPos.X, thing.sprite.WorldPos.Y) + shift, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
