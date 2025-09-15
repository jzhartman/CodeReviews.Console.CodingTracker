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
    private readonly IMenuView _view;

    public MainMenuController(ICodingSessionDataService service, 
                                ITrackSessionController trackController, IEntryListController entryListController, IReportsController reportsController,
                                IMenuView view)
    {
        _service = service;
        _trackController = trackController;
        _entryListController = entryListController;
        _reportsController = reportsController;
        _view = view;
    }

    public void Run()
    {
        bool exitApp = false;

        while (!exitApp)
        {
            Messages.RenderWelcome();
            var selection = _view.RenderMainMenuAndGetSelection();

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
