using ImageResearchNew.AppWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.ViewImageTools
{
    public class RotationIncreaseViewTool : AbstractInfoViewTool
    {
        public override string Name => "Compare many";

        public override void OpenCompareWindow(IList<object> imagesViews)
        {
            var comparePage = new CompareWindow();
        }
    }
}
