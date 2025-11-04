using CodingTracker.Data.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Services;
public class GoalDataService : IGoalDataService
{
    public readonly IGoalRepository _repository;

    public GoalDataService(IGoalRepository repository)
    {
        _repository = repository;
    }

    public void AddGoal(GoalModel goal)
    {
        _repository.AddGoal(goal);
    }
    public void UpdateGoal(GoalDTO goal)
    {
        _repository.UpdateGoal(goal);
    }
    public List<GoalDTO> GetAllGoalsByStatus(GoalStatus status)
    {
        return _repository.GetAllGoalsByStatus(status);
    }

}
