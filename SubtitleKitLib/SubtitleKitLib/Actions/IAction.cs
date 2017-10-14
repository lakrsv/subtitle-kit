using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleKitLib.Actions
{
    public interface IAction
    {
        void PerformAction();

        void UndoAction();
    }
}
