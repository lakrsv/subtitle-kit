using SubtitleKitLib.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleKitLib.Actions
{
    public interface IActionPerformer
    {      
        void PerformAction(IAction action);

        IAction UndoPreviousAction();
    }
}
