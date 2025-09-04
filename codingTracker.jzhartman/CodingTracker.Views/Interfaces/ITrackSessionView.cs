namespace CodingTracker.Views.Interfaces
{
    public interface ITrackSessionView
    {
        void ConfirmationMessage(string valueText);
        void ErrorMessage(string parameter, string message);
        DateTime GetEndTimeFromUser();
        DateTime GetStartTimeFromUser();
        string RenderMenuAndGetSelection();
    }
}