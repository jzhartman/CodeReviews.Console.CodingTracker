using CodingTracker.Controller.Interfaces;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views.Interfaces;

namespace CodingTracker.Controller;
public class ReportsController : IReportsController
{
    private readonly ICodingSessionDataService _codingSessionDataService;
    private readonly IMenuView _menuView;
    private readonly IUserInput _inputView;
    public ReportsController(ICodingSessionDataService service, IMenuView menuView, IUserInput inputView)
    {
        _codingSessionDataService = service;
        _menuView = menuView;
        _inputView = inputView;
    }

    public void Run()
    {
        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            
        }
    }
}
