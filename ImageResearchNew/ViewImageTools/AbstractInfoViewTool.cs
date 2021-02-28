using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.ViewImageTools
{
    public abstract class AbstractInfoViewTool
    {
        public abstract string Name { get; }

        public abstract void OpenCompareWindow(IList<object> imagesViews);
    }
}
