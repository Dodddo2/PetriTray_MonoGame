using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG.Models
{
    class Metaball
    {
        public float Treshold;

        /// <param r="Distance">
        public float Falloff(float r)
        {
            if (r < Treshold && r > 0)
            {
                return (float)Math.Cos(r);
            }
            return 0;
        }
    }
}
