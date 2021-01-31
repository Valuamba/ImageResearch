using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.ViewModel
{
    public interface IToolContainer
    {
        void Clear(CanvasViewModel sender);
        void ShowResult(CanvasViewModel sender);
    }
}
