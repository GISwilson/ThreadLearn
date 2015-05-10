using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo3
{
    public interface ISpecialProgressView:IProgressView
    {
        void CloseForm();
    }
}
