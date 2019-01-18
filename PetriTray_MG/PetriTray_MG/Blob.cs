using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace PetriTray_MG
{
    class Blob
    {
        public float HeatSum;
        private int blurIterations;
        public EntitySprite sprite;
        private RenderTarget2D preRender;
        private RenderTarget2D blurA;
        private RenderTarget2D blurB;
        public Vector3 Pivot;
        private Effect effect;
        public List<Models.Metaball> metaballs = new List<Models.Metaball>();
        private GraphicStructure graphicStructure;
        private int maxBalls = 10; // maximális metalabdák száma

        

        public Blob(Vector3 pos, GraphicsDevice device, Effect effect)
        {
            int width = 256;
            int height = 256;
            sprite = new EntitySprite(device, width, height);
            Pivot = pos;
            this.effect = effect;
            sprite.WorldPos = pos;
            preRender = new RenderTarget2D(sprite.Device, sprite.Sprite.Width, sprite.Sprite.Height);
            blurA = new RenderTarget2D(sprite.Device, sprite.Sprite.Width, sprite.Sprite.Height);
            blurB = new RenderTarget2D(sprite.Device, sprite.Sprite.Width, sprite.Sprite.Height);
            HeatSum = 0.0f;

            graphicStructure = new GraphicStructure(maxBalls);
        }

        public void AddBall(Models.Metaball ball)
        {
            metaballs.Add(ball);
            HeatSum += ball.Heat;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice device, GameTime elapsedTime, float blurAmount)
        {
            
            device.SetRenderTarget(preRender);
            graphicStructure.RefreshStructure(metaballs);


            //Kártya adatainak feltöltése, ha nem használt a paraméter ki kell kommentezni!

            effect.Parameters["colors"].SetValue(graphicStructure.Colors);
            effect.Parameters["positions"].SetValue(graphicStructure.Positions);
            effect.Parameters["ballCount"].SetValue(metaballs.Count);
            effect.Parameters["blurAmount"].SetValue(blurAmount);

            device.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            //pass0
            sprite.Device.SetRenderTarget(preRender);
            device.Clear(Color.Transparent);
            effect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(sprite.Sprite, new Vector2(0, 0), Color.Transparent);

            //effect.Parameters["Blob"].SetValue(preRender);
            sprite.Device.SetRenderTarget(blurB);
            device.Clear(Color.Transparent);
            effect.CurrentTechnique.Passes[1].Apply();
            spriteBatch.Draw(preRender, new Vector2(0, 0), Color.Transparent);

            //passBlur 
            for (blurIterations = 0; blurIterations < 10; blurIterations++)
            {
                sprite.Device.SetRenderTarget(blurA);
                //effect.Parameters["Blob"].SetValue(blurB);
                device.Clear(Color.Transparent);
                effect.CurrentTechnique.Passes[1].Apply();
                spriteBatch.Draw(blurB, new Vector2(0, 0), Color.Transparent);
                sprite.Device.SetRenderTarget(blurB);
                device.Clear(Color.Transparent);
                //effect.Parameters["Blob"].SetValue(blurA);
                effect.CurrentTechnique.Passes[1].Apply();
                spriteBatch.Draw(blurA, new Vector2(0, 0), Color.Transparent);
            }
            sprite.Device.SetRenderTarget(sprite.Sprite);
            //device.Clear(Color.Transparent);
            spriteBatch.Draw(blurB, new Vector2(0, 0), Color.Transparent);
            spriteBatch.End();

            /*
             * A SpriteBatch texture2d-je feltölti a Metaball.fx Blob textúráját
             */
        }

        private class GraphicStructure
        {
            public Vector4[] Colors;
            public Vector4[] Positions;
            public float[] Heats;
        
            public GraphicStructure(int maxBalls)
            {
                Colors = new Vector4[maxBalls];
                Positions = new Vector4[maxBalls];
                Heats = new float[maxBalls];
            }

            public void ClearStructure()
            {
                int maxBalls = Colors.Length;
                Colors = new Vector4[maxBalls];
                Positions = new Vector4[maxBalls];
                Heats = new float[maxBalls];
            }

            public void RefreshStructure(List<Models.Metaball> metaballs)
            {
                int counter = 0;
                ClearStructure();
                foreach (Models.Metaball balls in metaballs)
                {
                    Colors[counter] = balls.RGBA;
                    Positions[counter] = new Vector4(balls.Animate(0.001f),balls.Heat);
                    Heats[counter] = balls.Heat;
                    counter++;
                }
            }
        }
    }
}
