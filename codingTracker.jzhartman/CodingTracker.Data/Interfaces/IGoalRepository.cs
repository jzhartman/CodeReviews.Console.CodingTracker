using CodingTracker.Models.Entities;

namespace CodingTracker.Data.Interfaces;
public interface IGoalRepository
{
    void AddGoal(GoalModel goal);
    List<GoalDTO> GetAllGoals();
    List<GoalDTO> GetAllGoalsByStatus(GoalStatus status);
    int GetGoalCount();
    void UpdateGoal(GoalDTO goal);
}