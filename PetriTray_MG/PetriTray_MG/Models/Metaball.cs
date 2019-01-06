using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG.Models
{
    class Metaball
    {
        public Vector3 Position;
        private Vector4 rGBA;
        public float Heat;

        public Vector4 RGBA { get => rGBA;
            set
            {
                if ((value.X < 0 || value.X > 1) &&
                    (value.Y < 0 || value.Y > 1) &&
                    (value.Z < 0 || value.Z > 1) &&
                    (value.W < 0 || value.W > 1))
                {return;}
                rGBA = value;
            }
        }

        public Metaball()
        {
            Position = Vector3.Zero;
            rGBA = Vector4.UnitW;
            Heat = 1.0f;
        }

        public Metaball(Vector3 pos, Vector4 rgba, float heat)
        {
            Position = pos;
            rGBA = rgba;
            Heat = heat;
        }
    }
}
