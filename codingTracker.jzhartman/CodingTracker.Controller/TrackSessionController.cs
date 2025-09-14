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
    private readonly IUserInput _inputView;

    public TrackSessionController(ICodingSessionDataService service, IMenuView menuView, IUserInput inputView)
    {
        _service = service;
        _menuView = menuView;
        _inputView = inputView;
    }

    public void Run()
    {
        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
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
                Messages.Confirmation(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                Messages.Error(result.Parameter, result.Message);
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

            if (output <= startTime)
            {
                var parameter = "End Time";
                var message = $"The end time must be later than {startTime.ToString("yyyy-MM-dd HH:mm:ss")}";
                Messages.Error(parameter, message);
            }
            else
            {
                var result = _service.ValidateEndTime(output);

                if (result.IsValid)
                {
                    endTimeValid = true;
                    output = result.Value;
                    Messages.Confirmation(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    Messages.Error(result.Parameter, result.Message);
                } 
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
            Messages.ActionCancelled("addition");
        }
    }
    private void AddSession(CodingSession session)
    {
        _service.AddSession(session);
        Messages.ActionComplete(true, "Success", "Coding session successfully added!");
    }

    private CodingSession GetNewSessionFromStopwatch()
    {
        var startTime = GetStartTimeWithStopwatch();
        Messages.Confirmation(startTime.ToString("yyyy-MM-dd HH:mm:ss"));

        var endTime = GetStopTimeWithStopwatch();
        Messages.Confirmation(endTime.ToString("yyyy-MM-dd HH:mm:ss"));

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
