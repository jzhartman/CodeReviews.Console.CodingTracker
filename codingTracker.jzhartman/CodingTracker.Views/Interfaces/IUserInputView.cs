using CodingTracker.Models.Entities;

namespace CodingTracker.Views.Interfaces;
public interface IUserInputView
{
    int GetRecordIdFromUser(string action, int max);
    bool GetAddSessionConfirmationFromUser(CodingSession session);
    DateTime GetTimeFromUser(string parameterName, bool allowNull = false);
    bool GetUpdateSessionConfirmationFromUser(CodingSessionDataRecord session, CodingSession updatedSession);
    bool GetDeleteSessionConfirmationFromUser(CodingSessionDataRecord session);
    void StartStopwatch();
    void StopStopwatch();
    long GetGoalValueTime(GoalType goalType);
    long GetGoalValueForDaysPerPeriod();
    bool GetAddGoalConfirmationFromUser(GoalModel goal);
    bool GetDeleteGoalConfirmationFromUser(GoalDTO goal);
    void PressAnyKeyToContinue();
}