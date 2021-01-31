using ImageResearchNew.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew
{
    public abstract class ImageProcessor : BindableObjectBase
    {
        public abstract string Name { get; }

        public abstract void Process(EditedImage image);
    }
}
