using CodingTracker.Data.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;
using CodingTracker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Services;
public class GoalDataService : IGoalDataService
{
    private readonly IGoalRepository _repository;

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
    public List<GoalDTO> GetAllGoals()
    {
        return _repository.GetAllGoals();
    }
    public int GetGoalCount()
    {
        return _repository.GetGoalCount();
    }




    // Unit Conversions
    public long GetGoalValueFromTimeSpan(TimeSpan time)
    {
        return (long)time.TotalSeconds;
    }
    public int GetGoalDaysFromSeconds(long seconds)
    {
        return (int)TimeSpan.FromSeconds(seconds).TotalDays;
    }
    public TimeSpan GetGoalTimeFromSeconds(long seconds)
    {
        return TimeSpan.FromSeconds(seconds);
    }


    //Validation
    public ValidationResult<long> ValidateGoalValueInput(GoalType goalType, long input, long maxTime)
    {
        if (input < 1)
            return ValidationResult<long>.Fail("Goal Value", "Goal value cannot be zero or lower.");
        else if (input > maxTime)
            return ValidationResult<long>.Fail("Goal Value", $"Goal value exceeds maximum time {TimeSpan.FromSeconds(maxTime)}");
        else
            return ValidationResult<long>.Success(input);

    }

    //public ValidationResult<TimeSpan> ValidateGoalValueInput(TimeSpan input, TimeSpan maxTime)
    //{
    //    if (input.TotalSeconds < 1)
    //        return ValidationResult<TimeSpan>.Fail("Goal Value", "Goal value cannot be zero or lower.");
    //    else if (input > maxTime)
    //        return ValidationResult<TimeSpan>.Fail("Goal Value", $"Goal value exceeds maximum time {maxTime}");
    //    else
    //        return ValidationResult<TimeSpan>.Success(input);

    //    //    if (input.TotalSeconds < 1) return Spectre.Console.ValidationResult.Error("Value must be greater than zero!");
    //    //    else if (input > maxTime) return Spectre.Console.ValidationResult.Error($"Input cannot exceed {maxTime}!");
    //    //    else return Spectre.Console.ValidationResult.Success();
    //}

}