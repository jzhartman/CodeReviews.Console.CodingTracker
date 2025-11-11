using CodingTracker.Models.Entities;

namespace CodingTracker.Services.Interfaces;
public interface IGoalDataService
{
    void AddGoal(GoalModel goal);
    List<GoalDTO> GetAllGoals();
    List<GoalDTO> GetAllGoalsByStatus(GoalStatus status);
    int GetGoalCount();
    void UpdateGoal(GoalDTO goal);
}