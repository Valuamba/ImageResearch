using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageResearch.Engine.Processing
{
    public class Histogram
    {
        public int[] GetHistogram(Bitmap OriginalImage)
        {
            var histogram = new int[256];

            var data = OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            var offset = data.Stride - OriginalImage.Width * 3;

            unsafe
            {
                var ptr = (byte*)data.Scan0;

                for (var i = 0; i < OriginalImage.Height; i++)
                {
                    var p = ptr;
                    for (var j = 0; j < OriginalImage.Width; j++)
                    {
                        histogram[*p++]++;
                        histogram[*p++]++;
                        histogram[*p++]++;
                    }
                    ptr += data.Stride;
                }
            }

            OriginalImage.UnlockBits(data);

            return histogram;
        }
    }
}
