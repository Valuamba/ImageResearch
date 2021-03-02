using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ImageProcessor;
using ImageResearch.Engine.Processing;

namespace ImageResearch.Engine
{
    public class VimsImageFactory 
    {
        public Image Image { get; private set; }  

        public VimsImageFactory Load(Image img)
        {
            Image = img;
            return this;
        }

        public int[] BuildHistogram()
        {
            return new Histogram().GetHistogram((Bitmap)Image);
        }
    }
}
