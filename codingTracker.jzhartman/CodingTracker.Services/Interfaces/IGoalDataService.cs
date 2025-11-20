using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;

namespace CodingTracker.Services.Interfaces;
public interface IGoalDataService
{
    void AddGoal(GoalModel goal);
    void EvaluateAverageTimeGoal(GoalDTO goal, List<CodingSessionDataRecord> codingSessions);
    void EvaluateDaysPerPeriodGoal(GoalDTO goal, List<CodingSessionDataRecord> codingSessions);
    void EvaluateTotalTimeGoal(GoalDTO goal, List<CodingSessionDataRecord> codingSessions);
    List<GoalDTO> GetAllGoals();
    List<GoalDTO> GetAllGoalsByStatus(GoalStatus status);
    int GetGoalCount();
    void UpdateGoal(GoalDTO goal);
    ValidationResult<long> ValidateGoalValueInput(GoalType goalType, long input, long maxTime);
}