using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views.Interfaces;

namespace CodingTracker.Controller;
public class ReportsController : IReportsController
{
    private readonly ICodingSessionDataService _service;
    private readonly IMenuView _menuView;
    private readonly IUserInputView _inputView;
    private readonly IConsoleOutputView _outputView;
    public ReportsController(ICodingSessionDataService service,
                                IMenuView menuView, IUserInputView inputView, IConsoleOutputView outputView)
    {
        _service = service;
        _menuView = menuView;
        _inputView = inputView;
        _outputView = outputView;
    }

    public void Run()
    {
        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            _outputView.WelcomeMessage();

            var dateRangeSelection = _menuView.PrintEntryViewOptionsAndGetSelection();

            if (dateRangeSelection == "Return to Previous Menu") { returnToMainMenu = true; continue; }

            (DateTime startTime, DateTime endTime) = GetDatesBasedOnUserSelection(dateRangeSelection);
                
            var sessions = _service.GetSessionListByDateRange(startTime, endTime);
            var report = new ReportModel(sessions);
                
            _outputView.PrintCodingSessionListAsTable(sessions);
            _outputView.PrintReportDataAsTable(report);  
        }
    }

    private (DateTime, DateTime) GetDatesBasedOnUserSelection(string selection)
    {
        DateTime startTime = new DateTime();
        DateTime endTime = new DateTime();

        switch (selection)
        {
            case "All":
            case "Past Year":
            case "Year to Date":
                (startTime, endTime) = _service.GetBasicDateRange(selection);
                break;
            case "Custom Week":
            case "Custom Month":
            case "Custom Year":
                startTime = GetRangeStartTime();
                endTime = _service.GetEndTimeForAdvancedDateRange(selection, startTime);
                break;
        }

        return (startTime, endTime);
    }
    private DateTime GetRangeStartTime()
    {
        var output = new DateTime();
        bool startTimeValid = false;

        while (startTimeValid == false)
        {
            var startTime = _inputView.GetTimeFromUser("start time");
            var result = _service.ValidateDateRangeStartTime(startTime);

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
}
