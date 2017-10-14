
namespace SubtitleKitLib.Actions
{
    public interface IAction
    {
        void PerformAction();

        void UndoAction();
    }
}
