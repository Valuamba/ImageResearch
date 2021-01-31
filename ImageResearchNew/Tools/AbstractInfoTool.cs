using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageResearchNew.Tools
{
    public abstract class AbstractInfoTool
    {
        public abstract BitmapSource Icon { get; }
        public abstract string  ToolTip { get; }
    }
}
