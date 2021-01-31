using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.Classes
{
    public class Pixel
    {
        public Pixel(Color color)
        {
            Color = color;
        }

        public Color Color { get; private set; }

        public override string ToString()
        {
            return string.Format("R:{0} G:{1} B:{2}", Color.R, Color.G, Color.B);
        }
    }
}
