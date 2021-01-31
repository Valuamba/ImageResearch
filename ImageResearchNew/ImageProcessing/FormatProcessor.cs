using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageResearchNew.Model;

namespace ImageResearchNew.ImageProcessing
{
    public class FormatProcessor : ListImageProcessor
    {
        public override string Name => "Format";

        public FormatProcessor(List<ImageProcessor> processors) : base(processors)
        {
        }

        public override void Process(EditedImage image)
        {
            //throw new NotImplementedException();
        }
    }
}
