using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG
{
    class EntitySprite
    {
        public GraphicsDevice Device;
        public Texture2D Sprite;
        public Vector3 ScreenPos => WorldPos + Device.Viewport.Project(Vector3.Zero, Camera.Main.Projection, Camera.Main.View, Matrix.Identity);
        public Vector3 WorldPos;

        public EntitySprite(GraphicsDevice device) { Device = device; }
    }
}
