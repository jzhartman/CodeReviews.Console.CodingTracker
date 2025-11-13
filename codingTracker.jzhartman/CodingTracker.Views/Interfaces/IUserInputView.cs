using CodingTracker.Models.Entities;

namespace CodingTracker.Views.Interfaces;
public interface IUserInputView
{
    int GetRecordIdFromUser(string action, int max);
    bool GetAddSessionConfirmationFromUser(CodingSession session);
    DateTime GetTimeFromUser(string parameterName, bool allowNull = false);
    bool GetUpdateSessionConfirmationFromUser(CodingSessionDataRecord session, CodingSession updatedSession);
    bool GetDeleteSessionConfirmationFromUser(CodingSessionDataRecord session);
    string StartStopwatch();
    string StopStopwatch();
    int GetGoalValueForDaysPerPeriod(int maxDays);
    TimeSpan GetGoalValueTime(GoalType goalType, TimeSpan maxTime);
    //DateTime GetUpdatedStartTimeFromUser(DateTime originalTime);
}