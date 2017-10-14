using SubtitleKitLib.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleKitLib.Subtitle
{
    public interface ISubtitleTransformer
    {      
        void PerformAction(IAction action, Subtitle subtitle);

        IAction UndoPreviousAction(Subtitle subtitle);
    }
}
