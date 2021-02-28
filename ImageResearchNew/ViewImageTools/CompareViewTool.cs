using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.ViewImageTools
{
    public class CompareViewTool : AbstractInfoViewTool
    {
        public override string Name => "Compare Single";

        public override void OpenCompareWindow(IList<object> imagesViews)
        {
            var count = imagesViews.Count;
            var numColumns = Math.Ceiling(Math.Sqrt(count));
            var numRows = Math.Ceiling(count / (double)numColumns);
        }
    }
}
