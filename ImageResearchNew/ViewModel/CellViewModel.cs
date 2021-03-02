using ImageResearchNew.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.ViewModel
{
    public class CellViewModel : BindableObjectBase, ICellViewModel
    {
        public ICell Cell { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
