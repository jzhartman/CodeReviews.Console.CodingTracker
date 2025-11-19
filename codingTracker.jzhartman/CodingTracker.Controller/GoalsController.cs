using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views.Interfaces;
using System;
using static System.Collections.Specialized.BitVector32;

namespace CodingTracker.Controller;
public class GoalsController : IGoalsController
{
    private readonly IGoalDataService _goalService;
    private readonly ICodingSessionDataService _codingSessionService;
    private readonly IMenuView _menuView;
    private readonly IConsoleOutputView _outputView;
    private readonly IUserInputView _inputView;

    public GoalsController( IGoalDataService goalService, ICodingSessionDataService codingSessionService,
                            IMenuView menuView, IConsoleOutputView outputView, IUserInputView inputView)
    {
        _goalService = goalService;
        _codingSessionService = codingSessionService;
        _menuView = menuView;
        _outputView = outputView;
        _inputView = inputView;
    }

    public void Run()
    {
        GenerateDummyGoal();

        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            _outputView.WelcomeMessage();
            PrintAllActiveGoals();

            var goalsInProgress = GetAllGoalsInProgress();
            EvaluateAllGoalsInProgress(goalsInProgress);

            var selection = _menuView.PrintGoalOptionsAndGetSelection();

            switch (selection)
            {
                case "Add Goal":
                    var goal = GetGoalDataFromUser();
                    ConfirmAddGoal(goal);
                    break;
                case "Delete Goal":
                case "Extend Goal":
                case "Return to Previous Menu":
                    returnToMainMenu = true;
                    break;

            }




        }
    }

    private GoalModel GetGoalDataFromUser()
    {
        var startTime = GetStartTimeFromUser();
        var endTime = GetEndTimeFromUser(startTime);
        var goalType = GetGoalTypeFromUser();
        var goalValue = GetGoalValue(startTime, endTime, goalType);

        return new GoalModel
        {
            StartTime = startTime,
            EndTime = endTime,
            Type = goalType,
            Value = goalValue,
            Status = GoalStatus.InProgress
        };
    }

    private void ConfirmAddGoal(GoalModel goal)
    {
        var goalConfirmed = _inputView.GetAddGoalConfirmationFromUser(goal);

        if (goalConfirmed)
        {
            _goalService.AddGoal(goal);
        }
        else
        {
            _outputView.GoalCancelledMessage("addition");
        }
    }

    private long GetGoalValue(DateTime startTime, DateTime endTime, GoalType goalType)
    {
        long output = -1;
        bool valueIsValid = false;

        var maxValue = GetMaxGoalValueByType(goalType, startTime, endTime);

        while (valueIsValid == false)
        {
            var input = GetGoalValueFromUser(startTime, endTime, goalType);
            var result = _goalService.ValidateGoalValueInput(goalType, input, maxValue);

            if (result.IsValid)
            {
                valueIsValid = true;
                output = result.Value;
                _outputView.ConfirmationMessage(result.Value.ToString()); //ToDo: Fix printing -- This prints all values as seconds
            }
            else
            {
                _outputView.ErrorMessage(result.Parameter, result.Message);
            }
        }

        return output;
    }



    private long GetGoalValueFromUser(DateTime startTime, DateTime endTime, GoalType goalType)
    {
        switch (goalType)
        {
            case GoalType.AverageTime:
            case GoalType.TotalTime:
                return _inputView.GetGoalValueTime(goalType);

            case GoalType.DaysPerPeriod:
                return _inputView.GetGoalValueForDaysPerPeriod();
                
            default:
                return -1;
        }
    }
    private long GetMaxGoalValueByType(GoalType goalType, DateTime startTime, DateTime endTime)
    {
        TimeSpan maxTime = new TimeSpan();
        
        switch (goalType)
        {
            case GoalType.AverageTime:
                maxTime = new TimeSpan(23, 59, 59);
                return (long)maxTime.TotalSeconds;

            case GoalType.TotalTime:
                maxTime = endTime - startTime;
                return (long)maxTime.TotalSeconds;

            case GoalType.DaysPerPeriod:
                maxTime = endTime - startTime;
                return (long)maxTime.TotalDays;

            default:
                return -1;
        }
    }

    private DateTime GetStartTimeFromUser()
    {
        var output = new DateTime();
        bool startTimeValid = false;

        while (startTimeValid == false)
        {
            output = _inputView.GetTimeFromUser("Goal start time");

            var result = _codingSessionService.ValidateGoalStartTime(output);

            if (result.IsValid)
            {
                startTimeValid = true;
                output = result.Value;
                _outputView.ConfirmationMessage(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                _outputView.ErrorMessage(result.Parameter, result.Message);
            }
        }
        return output;
    }
    private DateTime GetEndTimeFromUser(DateTime startTime)
    {
        var output = new DateTime();
        bool endTimeValid = false;

        while (endTimeValid == false)
        {
            output = _inputView.GetTimeFromUser("Goal end time");

            var result = _codingSessionService.ValidateGoalEndTime(output, startTime);

            if (result.IsValid)
            {
                endTimeValid = true;
                output = result.Value;
                _outputView.ConfirmationMessage(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                _outputView.ErrorMessage(result.Parameter, result.Message);
            }
        }
        return output;
    } 
    private GoalType GetGoalTypeFromUser()
    {
        var selection = _menuView.PrintGoalTypesAndGetSelection();

        switch (selection)
        {
            case "Total Time":
                return GoalType.TotalTime;
            case "Average Time":
                return GoalType.AverageTime;
            case "Days Per Period":
                return GoalType.DaysPerPeriod;
            default:
                return new GoalType();
        }
    }


    private void PrintAllActiveGoals()
    {
        var goals = _goalService.GetAllGoalsByStatus(GoalStatus.InProgress);

        if (goals.Count <= 0)
            _outputView.NoRecordsMessage("Goals");
        else
            _outputView.PrintGoalListAsTable(goals);
    }

    private List<GoalDTO> GetAllGoalsInProgress()
    {
        return _goalService.GetAllGoalsByStatus(GoalStatus.InProgress);
    }
    private List<GoalDTO> GetAllGoals()
    {
        return _goalService.GetAllGoals();
    }

    private void EvaluateAllGoalsInProgress(List<GoalDTO> goals)
    {
        var codingSessionsLists = new List<List<CodingSessionDataRecord>>();

        foreach (var goal in goals)
        {
            codingSessionsLists.Add(_codingSessionService.GetSessionListByDateRange(goal.StartTime, goal.EndTime));
        }
    }

    private void EvaluateGoal(GoalModel goal)
    {
        
    }


    private void GenerateDummyGoal()
    {
        if (_goalService.GetGoalCount() <= 0)
        {
            GoalModel goal = new GoalModel
            {
                StartTime = DateTime.Now.AddDays(-5),
                EndTime = DateTime.Now.AddDays(5),
                Status = GoalStatus.InProgress,
                Type = GoalType.DaysPerPeriod,
                Value = (long)TimeSpan.FromDays(5).TotalSeconds
            };


            _goalService.AddGoal(goal);
        }
    }





}
