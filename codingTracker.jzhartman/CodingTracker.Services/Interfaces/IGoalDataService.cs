using CodingTracker.Models.Entities;

namespace CodingTracker.Services.Interfaces;
public interface IGoalDataService
{
    void AddGoal(GoalModel goal);
    List<GoalDTO> GetAllGoalsByStatus(GoalStatus status);
    void UpdateGoal(GoalDTO goal);
}