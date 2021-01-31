using ImageResearchNew.Model;
using ImageResearchNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageResearchNew.Helpers
{
    public static class DrawHelper
    {
        public static BitmapSource DrawGrid(ImageLayer layer, int gridSize)
        {
            var pen = new Pen(Brushes.White, 1);
            var bitmapSource = (BitmapSource)null;

            bitmapSource = ImageHelper.CreateRenderTarget(
                (int)layer.Image.Width, (int)layer.Image.Height,
                (visual, context) =>
                {
                    for (int i = 0; i < layer.Image.Width; i += gridSize)
                    {
                        context.DrawLine(pen, new System.Windows.Point(i, 0), new System.Windows.Point(i, layer.Image.Height));
                    }

                    for (int j = 0; j < layer.Image.Height; j += gridSize)
                    {
                        context.DrawLine(pen, new System.Windows.Point(0, j), new System.Windows.Point(layer.Image.Width, j));
                    }
                });

            return bitmapSource;
        }
    }
}
