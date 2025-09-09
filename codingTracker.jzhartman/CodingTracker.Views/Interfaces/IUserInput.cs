namespace CodingTracker.Views.Interfaces
{
    public interface IUserInput
    {
        DateTime GetEndTimeFromUser();
        int GetRecordIdFromUser(string action, int max);
        DateTime GetStartTimeFromUser();
        DateTime GetUpdatedStartTimeFromUser(DateTime originalTime);
    }
}