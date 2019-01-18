using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG
{
    class Background
    {
        public RenderTarget2D Sprite;
        public Effect effect;
        Texture2D backgroundImage;

        public Background(GraphicsDevice device, Effect effect, Texture2D backgroundImage)
        {
            Sprite = new RenderTarget2D(device, Camera.Main.Resolution.Width, Camera.Main.Resolution.Height);
            this.effect = effect;
            this.backgroundImage = backgroundImage;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice device, GameTime gameTime)
        {
            effect.Parameters["worldXY"].SetValue(Camera.Main.GetXY());
            effect.Parameters["gameTime"].SetValue((float)gameTime.TotalGameTime.TotalMilliseconds*0.001f);

            device.SetRenderTarget(Sprite);
            device.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            effect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(backgroundImage, new Vector2(0, 0), Color.Transparent);
            spriteBatch.End();
        }
    }
}
