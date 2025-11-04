using CodingTracker.Models.Entities;

namespace CodingTracker.Data.Interfaces;
public interface IGoalRepository
{
    void AddGoal(GoalModel goal);
    List<GoalDTO> GetAllGoalsByStatus(GoalStatus status);
    void UpdateGoal(GoalDTO goal);
}