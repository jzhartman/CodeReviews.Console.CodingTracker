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
                    AddGoal();
                    break;
                case "Delete Goal":
                case "Extend Goal":
                case "Return to Previous Menu":
                    returnToMainMenu = true;
                    break;

            }




        }
    }

    private void AddGoal()
    {
        var startTime = GetStartTimeFromUser();
        var endTime = GetEndTimeFromUser(startTime);
        var goalType = GetGoalTypeFromUser();
        var goalValue = GetGoalValueFromUser();


        // Confirm, then add or cancel!

        _goalService.AddGoal(
            new GoalModel
            {
                StartTime = startTime,
                EndTime = endTime,
                Type = goalType,
                Value = goalValue,
                Status = GoalStatus.InProgress
            }
            );

    }

    private int GetGoalValueFromUser()
    {
        throw new NotImplementedException();
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
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2).AddMinutes(30),
                Status = GoalStatus.InProgress,
                Type = GoalType.DaysPerPeriod,
                Value = 5
            };


            _goalService.AddGoal(goal);
        }
    }





}
