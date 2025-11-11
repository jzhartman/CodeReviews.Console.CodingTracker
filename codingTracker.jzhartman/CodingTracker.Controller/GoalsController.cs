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

    public GoalsController( IGoalDataService goalService, ICodingSessionDataService codingSessionService,
                            IMenuView menuView, IConsoleOutputView outputView)
    {
        _goalService = goalService;
        _codingSessionService = codingSessionService;
        _menuView = menuView;
        _outputView = outputView;
    }

    public void Run()
    {
        //GenerateDummyGoal();

        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            _outputView.WelcomeMessage();
            PrintAllActiveGoals();

            var goalsInProgress = GetAllGoalsInProgress();
            EvaluateAllGoalsInProgress(goalsInProgress);

            var selection = _menuView.PrintGoalOptionsAndGetSelection();

            if (selection == "Return to Previous Menu") { returnToMainMenu = true; continue; }




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
                Type = GoalType.DaysPerPeriod
            };


            _goalService.AddGoal(goal);
        }
    }





}
