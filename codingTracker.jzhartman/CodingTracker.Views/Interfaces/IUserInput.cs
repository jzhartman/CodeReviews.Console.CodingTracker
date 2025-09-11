using CodingTracker.Models.Entities;

namespace CodingTracker.Views.Interfaces
{
    public interface IUserInput
    {
        int GetRecordIdFromUser(string action, int max);
        bool GetAddSessionConfirmationFromUser(CodingSession session);
        DateTime GetTimeFromUser(string parameterName, bool allowNull = false);
        //DateTime GetUpdatedStartTimeFromUser(DateTime originalTime);
    }
}