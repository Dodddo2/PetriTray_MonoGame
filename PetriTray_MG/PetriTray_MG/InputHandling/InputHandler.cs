using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG.InputHandling
{
    class InputHandler
    {
        public static void KeyboardInputs(Keys[] keys)
        {
            Vector3 move = Vector3.Zero;
            foreach (Keys key in keys)
            {
                switch (key)
                {
                    case Keys.W:
                        move.Y++;
                        break;
                    case Keys.A:
                        move.X--;
                        break;
                    case Keys.S:
                        move.Y--;
                        break;
                    case Keys.D:
                        move.X++;
                        break;
                    case Keys.Space:
                        move.Z++;
                        break;
                    case Keys.LeftControl:
                        move.Z--;
                        break;
                    default:
                        Console.WriteLine("Key not recognized");
                        break;
                }
            }
            Camera.Main.Move(move);
        }
    }
}
