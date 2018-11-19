using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG
{
    class Blob
    {
        public EntitySprite sprite;
        public Vector3 Pivot;
        public Effect Effect;
        public List<Models.Metaball> metaballs = new List<Models.Metaball>();

        public Blob(Vector3 pos, GraphicsDevice device, Effect effect)
        {
            int width = 600;
            int height = 600;
            sprite = new EntitySprite(device);
            Pivot = pos;
            Effect = effect;
            sprite.Sprite = new Texture2D(device, width, height);
            sprite.WorldPos = new Vector3(Pivot.X-(width/2), Pivot.Y-(height/2), Pivot.Z);
        }

        public void AddBall(Models.Metaball ball)
        {
            ball.SpritePos = ball.Position - Camera.Main.Position;
            metaballs.Add(ball);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < metaballs.Count; i++)
            {
                
                Effect.Parameters["positions"].Elements[i].SetValue(metaballs[i].SpritePos);
                Effect.Parameters["colors"].Elements[i].SetValue(metaballs[i].RGBA);
                Effect.Parameters["parameters"].Elements[i].SetValue(metaballs[i].Parameters);
                
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                Effect.CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(sprite.Sprite, new Vector2(0, 0), Color.White);
                spriteBatch.End();
            }

        }

    }
}
