using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;

namespace CodingTracker.Services.Interfaces;
public interface IGoalDataService
{
    void AddGoal(GoalModel goal);
    List<GoalDTO> GetAllGoals();
    List<GoalDTO> GetAllGoalsByStatus(GoalStatus status);
    int GetGoalCount();
    void UpdateGoal(GoalDTO goal);
    ValidationResult<long> ValidateGoalValueInput(GoalType goalType, long input, long maxTime);
}