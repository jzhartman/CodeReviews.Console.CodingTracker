using CodingTracker.Models.Entities;

namespace CodingTracker.Views.Interfaces
{
    public interface IUserInput
    {
        DateTime GetEndTimeFromUser();
        int GetRecordIdFromUser(string action, int max);
        bool GetAddSessionConfirmationFromUser(CodingSession session);
        DateTime GetStartTimeFromUser();
        DateTime GetUpdatedStartTimeFromUser(DateTime originalTime);
    }
}