using CodingTracker.Models.Entities;
using CodingTracker.Controller.Interfaces;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
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

            var dateRangeSelection = GetDateRangeSelectionFromUser();

            if (dateRangeSelection == "Return to Previous Menu") { returnToMainMenu = true; continue; }

            (DateTime startTime, DateTime endTime) = GetDatesBasedOnUserSelection(dateRangeSelection);
                
            var sessions = _service.GetSessionListByDateRange(startTime, endTime);
            var report = new ReportModel(sessions);
                
            _outputView.PrintCodingSessionListAsTable(sessions);
            _outputView.PrintReportDataAsTable(report);
            
        }
    }


    private string GetDateRangeSelectionFromUser()
    {
        return _menuView.PrintEntryViewOptionsAndGetSelection();
    }
    private (DateTime, DateTime) GetDatesBasedOnUserSelection(string selection)
    {
        DateTime startTime = new DateTime();
        DateTime endTime = new DateTime();

        switch (selection)
        {
            case "All":
                (startTime, endTime) = GetAllDates();
                break;
            case "One Year":
                (startTime, endTime) = GetDateRangeForPastYear();
                break;
            case "Year to Date":
                (startTime, endTime) = GetDateRangeForYearToDate();
                break;
            case "Enter Date Range":
                (startTime, endTime) = GetCustomDateRange();
                break;
        }

        return (startTime, endTime);
    }
    private (DateTime, DateTime) GetAllDates()
    {
        var startTime = DateTime.MinValue;
        var endTime = DateTime.MaxValue;
        return (startTime, endTime);
    }
    private (DateTime, DateTime) GetDateRangeForPastYear()
    {
        var endTime = DateTime.Now;
        var startTime = endTime.AddYears(-1);
        return (startTime, endTime);
    }
    private (DateTime, DateTime) GetDateRangeForYearToDate()
    {
        var endTime = DateTime.Now;
        var startTime = new DateTime(endTime.Year, 1, 1);
        return (startTime, endTime);
    }
    private (DateTime, DateTime) GetCustomDateRange()
    {
        var startTime = _inputView.GetTimeFromUser("start time");
        var endTime = new DateTime();
        bool endTimeValid = false;

        while (endTimeValid == false)
        {
            endTime = _inputView.GetTimeFromUser("end time");

            if (endTime <= startTime)
            {
                var parameter = "End Time";
                var message = $"The end time must be later than {startTime.ToString("yyyy-MM-dd HH:mm:ss")}";
                _outputView.ErrorMessage(parameter, message);
            }
            else
            {
                endTimeValid = true;
            }
        }

        return (startTime, endTime);

    }
}
