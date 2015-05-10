using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo3
{
    public interface IProgressView
    {
        void SetMessage(string msg);
        void SetProgress(int percent);
        DialogResult ShowDialog();
    }
}
