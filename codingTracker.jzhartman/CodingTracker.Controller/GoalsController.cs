using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views.Interfaces;

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
        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            _outputView.WelcomeMessage();

            var goalsInProgress = _goalService.GetAllGoalsByStatus(GoalStatus.InProgress);
            EvaluateGoals(goalsInProgress);

            var selection = _menuView.PrintGoalOptionsAndGetSelection();

            switch (selection)
            {
                case "Add Goal":
                    var goal = GetGoalDataFromUser();
                    ConfirmAddGoal(goal);
                    break;
                case "Delete Goal":
                    ManageGoalDelete();
                    break;
                case "View Completed Goals":
                    ViewCompletedGoals();
                    break;
                case "Return to Previous Menu":
                    returnToMainMenu = true;
                    break;
            }
        }
    }

    private void ViewCompletedGoals()
    {
        var goals = _goalService.GetAllGoalsByStatus(GoalStatus.Complete);

        _outputView.WelcomeMessage();
        PrintGoalsList(goals);
        _inputView.PressAnyKeyToContinue();
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
            GoalValue = goalValue,
            Status = GoalStatus.InProgress,
            CurrentValue = 0,
            Progress = 0
        };
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
    private long GetGoalValue(DateTime startTime, DateTime endTime, GoalType goalType)
    {
        long output = -1;
        bool valueIsValid = false;

        var maxValue = GetMaxGoalValueByType(goalType, startTime, endTime);

        while (valueIsValid == false)
        {
            var input = GetGoalValueFromUser(goalType);
            var result = _goalService.ValidateGoalValueInput(goalType, input, maxValue);

            if (result.IsValid)
            {
                valueIsValid = true;
                output = result.Value;
                _outputView.ConfirmationMessage(result.Value.ToString());
            }
            else
            {
                _outputView.ErrorMessage(result.Parameter, result.Message);
            }
        }

        return output;
    }
    private long GetGoalValueFromUser(GoalType goalType)
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
            _inputView.PressAnyKeyToContinue();
        }
    }


    private void ManageGoalDelete()
    {
        _outputView.WelcomeMessage();
        var goals = _goalService.GetAllGoals();

        PrintGoalsList(goals);

        var recordId = _inputView.GetRecordIdFromUser("delete", goals.Count()) - 1;

        if (_inputView.GetDeleteGoalConfirmationFromUser(goals[recordId]))
        {
            _goalService.DeleteGoalById(recordId);
        }
        else
        {
            _outputView.GoalCancelledMessage("deletion");
            _inputView.PressAnyKeyToContinue();
        }
    }

    private void PrintGoalsList(List<GoalDTO> goals)
    {
        _outputView.WelcomeMessage();

        if (goals.Count <= 0)
            _outputView.NoRecordsMessage("goals");
        else
            _outputView.PrintGoalListAsTable(goals);
    }

    private void EvaluateGoals(List<GoalDTO> goals)
    {
        UpdateGoalStatusAndProgress(goals);
        PrintGoalsList(goals);

    }
    private void UpdateGoalStatusAndProgress(List<GoalDTO> goals)
    {
        foreach (var goal in goals)
        {
            var codingSessions = _codingSessionService.GetSessionListByDateRange(goal.StartTime, goal.EndTime);

            switch (goal.Type)
            {
                case GoalType.TotalTime:
                    _goalService.EvaluateTotalTimeGoal(goal, codingSessions);
                    break;
                case GoalType.AverageTime:
                    _goalService.EvaluateAverageTimeGoal(goal, codingSessions);
                    break;
                case GoalType.DaysPerPeriod:
                    _goalService.EvaluateDaysPerPeriodGoal(goal, codingSessions);
                    break;
                default:
                    _outputView.ErrorMessage("Goal Type", "Invalid Goal Type detected!");
                    break;
            }

            _goalService.EvaluateGoal(goal);

            if (goal.Status == GoalStatus.Complete ||  goal.Status == GoalStatus.Failed)
            {
                _outputView.WelcomeMessage();
                _outputView.GoalEvaluationMessage(goal);
                _inputView.PressAnyKeyToContinue();
            }
        }
    }
}
