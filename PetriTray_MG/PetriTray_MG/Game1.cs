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
            player = new Blob(Vector3.Zero, GraphicsDevice, Content.Load<Effect>("Metaball"));
            player.AddBall(new Models.Metaball(new Vector3(-0.2f, 0, 0), new Vector4(0,1,0,1), 0.2f));
            player.AddBall(new Models.Metaball(new Vector3(0.2f, 0, 0), new Vector4(1, 0, 0, 1), 0.2f));
            thing = new Blob(new Vector3(400, 0, 0), GraphicsDevice, Content.Load<Effect>("Metaball"));
            thing.AddBall(new Models.Metaball(Vector3.Zero, new Vector4(0, 1, 0, 1), 0.2f));

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
        Stopwatch watch = new Stopwatch();
        protected override void Draw(GameTime gameTime)
        {
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Restart();
            // TODO: Add your drawing code here
            player.Draw(spriteBatch, GraphicsDevice);
            //thing.Draw(spriteBatch, GraphicsDevice);


            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);           
                spriteBatch.Draw(player.sprite.Sprite, new Vector2(player.sprite.WorldPos.X, player.sprite.WorldPos.Y), Color.White);
                spriteBatch.Draw(thing.sprite.Sprite, new Vector2(thing.sprite.WorldPos.X, thing.sprite.WorldPos.Y), Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
