using CodingTracker.Controller.Interfaces;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using CodingTracker.Views.Interfaces;

namespace CodingTracker.Controller;
public class MainMenuController : IMainMenuController
{
    private readonly ICodingSessionDataService _service;
    private readonly ITrackSessionController _trackController;
    private readonly IEntryListController _entryListController;
    private readonly IReportsController _reportsController;
    private readonly IGoalsController _goalsController;
    private readonly IMenuView _menuView;
    private readonly IConsoleOutputView _outputView;

    public MainMenuController(ICodingSessionDataService service, 
                                ITrackSessionController trackController, IEntryListController entryListController, IReportsController reportsController, IGoalsController goalsController,
                                IMenuView menuView, IConsoleOutputView outputView)
    {
        _service = service;
        _trackController = trackController;
        _entryListController = entryListController;
        _reportsController = reportsController;
        _goalsController = goalsController;
        _menuView = menuView;
        _outputView = outputView;
    }

    public void Run()
    {
        bool exitApp = false;

        while (!exitApp)
        {
            _outputView.WelcomeMessage();
            var selection = _menuView.PrintMainMenuAndGetSelection();

            switch (selection)
            {
                case "Track Session":    // Submenu: Enter times/Stopwatch/Return
                    _trackController.Run();
                    break;
                case "View/Manage Entries":  // Submenu: View Entries (range or all)/Update/Delete
                    _entryListController.Run();
                    break;
                case "View Reports":    // Enter Range-Period??? --> Print all records for period --> Print report data
                    _reportsController.Run();
                    break;
                case "Manage Goal":     // Print current goal+progress/Give option to change goal
                    _goalsController.Run();
                    break;
                case "Exit":            // Generic goodbye message
                    exitApp = true;
                    break;
                default:
                    break;
            }
        }
    }
}
