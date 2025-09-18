using CodingTracker.Controller.Interfaces;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views.Interfaces;

namespace CodingTracker.Controller;
public class GoalsController : IGoalsController
{
    private readonly ICodingSessionDataService _service;
    private readonly IMenuView _menuView;

    public GoalsController(ICodingSessionDataService service, IMenuView menuView)
    {
        _service = service;
        _menuView = menuView;
    }

    public void Run()
    {
        Console.WriteLine("Not Implemented yet....");
        Console.ReadKey();
    }
}
