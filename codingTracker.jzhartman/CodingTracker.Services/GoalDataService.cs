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
    public void DeleteGoalById(int id)
    {
        _repository.DeleteById(id);
    }
    public void EvaluateGoal(GoalDTO goal)
    {
        _repository.EvaluateGoal(goal);
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



    // Evaluations
    public void EvaluateTotalTimeGoal(GoalDTO goal, List<CodingSessionDataRecord> codingSessions)
    {
        var timeRemaining = (goal.EndTime - DateTime.Now).TotalSeconds;

        goal.CurrentValue = SumTotalTimeFromSessions(codingSessions);
        goal.Progress = (goal.CurrentValue / goal.GoalValue) * 100;

        if (goal.Progress >= 100 && timeRemaining < 0)
            goal.Status = GoalStatus.Complete;

        else if (timeRemaining > 0 && (timeRemaining + goal.CurrentValue) >= goal.GoalValue)
            goal.Status = GoalStatus.InProgress;

        else
            goal.Status = GoalStatus.Failed;
    }
    public void EvaluateAverageTimeGoal(GoalDTO goal, List<CodingSessionDataRecord> codingSessions)
    {
        var timeRemaining = (goal.EndTime - DateTime.Now).TotalSeconds;
        var daysRemaining = (goal.EndTime - DateTime.Now).TotalDays;

        var totalTime = SumTotalTimeFromSessions(codingSessions);
        goal.CurrentValue = (long)(totalTime / (goal.EndTime - goal.StartTime).TotalDays);
        goal.Progress = ((double)goal.CurrentValue / goal.GoalValue) * 100;

        if (goal.Progress >= 100 && timeRemaining < 0)
            goal.Status = GoalStatus.Complete;

        else if (timeRemaining > 0 && (totalTime + timeRemaining)/daysRemaining >= goal.GoalValue)
            goal.Status = GoalStatus.InProgress;

        else
            goal.Status = GoalStatus.Failed;
    }

    public void EvaluateDaysPerPeriodGoal(GoalDTO goal, List<CodingSessionDataRecord> codingSessions)
    {
        var daysRemaining = (goal.EndTime - DateTime.Now).TotalDays;

        if (daysRemaining > (goal.EndTime - goal.StartTime).TotalDays)
            daysRemaining = (goal.EndTime - goal.StartTime).TotalDays;


        goal.CurrentValue = GetUniqueDaysPerPeriod(codingSessions) * 86400;
        goal.Progress = ((double)goal.CurrentValue / goal.GoalValue) * 100;

        if (goal.Progress >= 100 && daysRemaining < 0)
            goal.Status = GoalStatus.Complete;

        else if (daysRemaining > 0 && (goal.CurrentValue + daysRemaining) >= goal.GoalValue)
            goal.Status = GoalStatus.InProgress;

        else
            goal.Status = GoalStatus.Failed;
    }

    // TODO: Address cases where codingSessions is empty
    private int GetUniqueDaysPerPeriod(List<CodingSessionDataRecord> codingSessions)
    {
        int uniqueDays = 1;

        for (int i = 1; i < codingSessions.Count; i++)
        {
            if (codingSessions[i].StartTime.Date != codingSessions[i - 1].StartTime.Date)
                uniqueDays++;
        }

        return uniqueDays;
    }
    private long SumTotalTimeFromSessions(List<CodingSessionDataRecord> codingSessions)
    {
        long totalTime = 0;

        foreach (var session in codingSessions)
        {
            totalTime += session.Duration;
        }

        return totalTime;
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
}