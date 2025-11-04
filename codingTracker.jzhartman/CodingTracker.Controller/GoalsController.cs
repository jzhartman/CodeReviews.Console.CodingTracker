using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views.Interfaces;

namespace CodingTracker.Controller;
public class GoalsController : IGoalsController
{
    private readonly IGoalDataService _service;
    private readonly IMenuView _menuView;
    private readonly IConsoleOutputView _outputView;

    public GoalsController(IGoalDataService service,
                            IMenuView menuView, IConsoleOutputView outputView)
    {
        _service = service;
        _menuView = menuView;
        _outputView = outputView;
    }

    public void Run()
    {
        GoalModel goal = new GoalModel {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2).AddMinutes(30),
                Status = GoalStatus.InProgress,
                Type = GoalType.TotalTime};


        _service.AddGoal(goal);

        var goals = _service.GetAllGoalsByStatus(GoalStatus.InProgress);

        _outputView.PrintGoalListAsTable(goals);

        Console.ReadKey();
    }
}
