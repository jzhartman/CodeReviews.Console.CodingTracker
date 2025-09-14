using CodingTracker.Models.Entities;

namespace CodingTracker.Views.Interfaces;
public interface IUserInput
{
    int GetRecordIdFromUser(string action, int max);
    bool GetAddSessionConfirmationFromUser(CodingSession session);
    DateTime GetTimeFromUser(string parameterName, string nullBehavior = "", bool allowNull = false);
    bool GetUpdateSessionConfirmationFromUser(CodingSessionDataRecord session, CodingSession updatedSession);
    bool GetDeleteSessionConfirmationFromUser(CodingSessionDataRecord session);
    //DateTime GetUpdatedStartTimeFromUser(DateTime originalTime);
}