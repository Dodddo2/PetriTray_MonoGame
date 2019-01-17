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
        public RenderTarget2D Sprite;
        public Vector3 ScreenPos => WorldPos + Device.Viewport.Project(Vector3.Zero, Camera.Main.Projection, Camera.Main.View, Matrix.Identity);
        public Vector3 WorldPos;

        public EntitySprite(GraphicsDevice device, int width, int height) {
            Device = device;
            Sprite = new RenderTarget2D(device, width, height, false,
                SurfaceFormat.Color, DepthFormat.Depth16, 1, RenderTargetUsage.PreserveContents);
        }
    }
}
