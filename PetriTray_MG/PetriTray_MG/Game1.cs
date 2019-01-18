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
        Blob player, thing, merge;
        Background background;


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

            background = new Background(GraphicsDevice, Content.Load<Effect>("Background"), Content.Load<Texture2D>("Water"));

            player = new Blob(Vector3.Zero, GraphicsDevice, Content.Load<Effect>("Metaball"));
            player.AddBall(new Models.Metaball(new Vector3(0.3f, -0.5f, 0), new Vector4(0,1,0,1), 0.1f));
            player.AddBall(new Models.Metaball(new Vector3(0.5f, -0.5f, 0), new Vector4(1, 0, 0, 1), 0.1f));
            player.AddBall(new Models.Metaball(new Vector3(0.7f, -0.3f, 0), new Vector4(0, 0, 1, 1), 0.1f));
            player.AddBall(new Models.Metaball(new Vector3(0.6f, -0.7f, 0), new Vector4(0, 0.5f, 0.5f, 1), 0.1f));

            thing = new Blob(new Vector3(400, 0, 0), GraphicsDevice, Content.Load<Effect>("Metaball"));
            thing.AddBall(new Models.Metaball(new Vector3(0.5f, -0.5f, 0), new Vector4(0, 1, 0, 1), 0.2f));

            merge = new Blob(new Vector3(200, 200, 0), GraphicsDevice, Content.Load<Effect>("Metaball"));
            merge.AddBall(new Models.Metaball(new Vector3(0.3f, -0.5f, 0), new Vector4(0, 1, 0, 1), 0.1f));
            merge.AddBall(new Models.Metaball(new Vector3(0.7f, -0.5f, 0), new Vector4(1, 0, 0, 1), 0.1f));

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

            CollisionHandler.CheckCollision(player, thing);
            player.Pivot = Camera.Main.Position;
            merge.metaballs[0].Position.X = 0.2f * (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 1000) + 0.5f;

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
            background.Draw(spriteBatch, GraphicsDevice, gameTime);

            //mélység számolása
            float scale;
            float depth = thing.Pivot.Z - player.Pivot.Z;
            if (depth < 0)
            {
                depth = -depth + 100;
                scale = 100 / depth;
            }
            else
            {
                depth += 100;
                scale = depth / 100;
            }

            player.Draw(spriteBatch, GraphicsDevice, gameTime, 0.1f);
            thing.Draw(spriteBatch, GraphicsDevice, gameTime, depth / 100);
            merge.Draw(spriteBatch, GraphicsDevice, gameTime, depth / 100);
            
            Vector2 shift = Camera.Main.GetTopLeft() - Camera.Main.GetXY();
            
            rotation += 0.01f;
            if (rotation > 6.28f) { rotation = 0; }
            
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(new Color(0.8f, 0.8f, 0.6f, 1.0f));

            //Háttér kirajzolása
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            spriteBatch.Draw(background.Sprite, Vector2.Zero, Color.White);
            spriteBatch.End();

            //Blob-ok kirajzolása
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            spriteBatch.Draw(player.sprite.Sprite, new Vector2(500, 500), null, null, new Vector2(128, 128), 0.0f, null, Color.White);
            spriteBatch.Draw(thing.sprite.Sprite, new Vector2(thing.Pivot.X, thing.Pivot.Y) + shift + new Vector2(128, 128), null, null, new Vector2(128, 128), 0.0f, new Vector2(scale, scale), new Color(1.0f, 1.0f, 1.0f, 100 / depth));
            spriteBatch.Draw(merge.sprite.Sprite, new Vector2(merge.Pivot.X, merge.Pivot.Y) + shift + new Vector2(128, 128), null, null, new Vector2(128, 128), 0.0f, new Vector2(scale, scale), new Color(1.0f, 1.0f, 1.0f, 100 / depth));
            spriteBatch.End();

            //player.sprite.Sprite.SaveAsPng(File.Create("out.png"), 256, 256);

            base.Draw(gameTime);
        }
    }
}
