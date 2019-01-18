using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG
{
    class CollisionHandler
    {
        public static readonly CollisionHandler MainHandler = new CollisionHandler();

        public static void CheckCollision(Blob blobA, Blob blobB)
        {
            if (Math.Abs(blobA.Pivot.Z - blobB.Pivot.Z) < 100)
            {
                foreach (Models.Metaball ballA in blobA.metaballs)
                {
                    foreach (Models.Metaball ballB in blobB.metaballs)
                    {
                        // a *256-ok a sprite [0f.1f] mivolta miatt kell
                        float distance = Vector3.Distance(blobA.Pivot + ballA.Position * 256, blobB.Pivot + ballB.Position * 256);
                        float ballRadiusSum = 6.437752f * (ballA.Heat * ballA.Heat + ballB.Heat * ballB.Heat) * 256;
                        //Console.WriteLine(distance + " : " + ballRadiusSum);
                        if (distance < ballRadiusSum)
                        {
                            //Console.WriteLine("HIT!" + ballA.RGBA.ToString());
                            if (blobA.HeatSum > blobB.HeatSum && ballB.Heat > 0)
                            {
                                ballA.Heat += 0.0005f;
                                ballB.Heat -= 0.0005f;
                            }
                            else
                            {
                                ballB.Heat += 0.0005f;
                                ballA.Heat -= 0.0005f;
                            }
                        }
                    }
                } 
            }
        }
    }
}
