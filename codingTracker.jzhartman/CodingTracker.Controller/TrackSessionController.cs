using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using CodingTracker.Views.Interfaces;

namespace CodingTracker.Controller;
public class TrackSessionController : ITrackSessionController
{
    private readonly ICodingSessionDataService _service;
    private readonly IMenuView _menuView;
    private readonly IUserInputView _inputView;
    private readonly IConsoleOutputView _outputView;

    public TrackSessionController(ICodingSessionDataService service, IMenuView menuView, IUserInputView inputView, IConsoleOutputView outputView)
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
            var selection = _menuView.RenderTrackingMenuAndGetSelection();

            switch (selection)
            {
                case "Enter Start and End Times":
                    var session = GetNewSessionFromUserInput();
                    ConfirmAndAddSession(session);
                    break;
                case "Begin Timer":
                    var stopwatchSession = GetNewSessionFromStopwatch();
                    ConfirmAndAddSession(stopwatchSession);
                    break;
                case "Return to Main Menu":
                    returnToMainMenu = true;
                    break;
                default:
                    break;
            }
        }
    }

    private CodingSession GetNewSessionFromUserInput()
    {
        var startTime = GetStartTimeFromUser();
        var endTime = GetEndTimeFromUser(startTime);

        return new CodingSession(startTime, endTime);
    }
    private DateTime GetStartTimeFromUser()
    {
        var output = new DateTime();
        bool startTimeValid = false;
        
        while (startTimeValid == false)
        {
            output = _inputView.GetTimeFromUser("start time");
            
            var result = _service.ValidateStartTime(output);

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
            output = _inputView.GetTimeFromUser("end time");

            var result = _service.ValidateEndTime(output, startTime);

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
    private void ConfirmAndAddSession(CodingSession session)
    {
        var sessionConfirmed = _inputView.GetAddSessionConfirmationFromUser(session);

        if (sessionConfirmed)
        {
            AddSession(session);
        }
        else
        {
            _outputView.ActionCancelledMessage("addition");
        }
    }
    private void AddSession(CodingSession session)
    {
        _service.AddSession(session);
        _outputView.ActionCompleteMessage(true, "Success", "Coding session successfully added!");
    }

    private CodingSession GetNewSessionFromStopwatch()
    {
        var startTime = GetStartTimeWithStopwatch();
        _outputView.ConfirmationMessage(startTime.ToString("yyyy-MM-dd HH:mm:ss"));

        var endTime = GetStopTimeWithStopwatch();
        _outputView.ConfirmationMessage(endTime.ToString("yyyy-MM-dd HH:mm:ss"));

        return new CodingSession(startTime, endTime);
    }

    private DateTime GetStartTimeWithStopwatch()
    {
        var selection = _inputView.StartStopwatch();
        return DateTime.Now;
    }

    private DateTime GetStopTimeWithStopwatch()
    {
        var selection = _inputView.StopStopwatch();
        return DateTime.Now;
    }
}
