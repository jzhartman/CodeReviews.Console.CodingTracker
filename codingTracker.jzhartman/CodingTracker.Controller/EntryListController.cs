using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using CodingTracker.Views.Interfaces;

namespace CodingTracker.Controller.Interfaces;

public class EntryListController : IEntryListController
{
    private readonly ICodingSessionDataService _service;
    private readonly IMenuView _menuView;
    private readonly IUserInput _inputView;

    public EntryListController(ICodingSessionDataService service,
                                IMenuView menuView,
                                IUserInput inputView)
    {
        _service = service;
        _menuView = menuView;
        _inputView = inputView;
    }

    public void Run()
    {
        bool returnToPreviousMenu = false;

        while (!returnToPreviousMenu)
        {
            Messages.RenderWelcome();

            var dateRangeSelection = GetDateRangeSelectionFromUser();

            if (dateRangeSelection == "Return to Previous Menu") { returnToPreviousMenu = true; continue; }

            (DateTime startTime, DateTime endTime) = GetDatesBasedOnUserSelection(dateRangeSelection);
            var sessions = _service.GetSessionListByDateRange(startTime, endTime);

            bool returnToDateSelection = false;

            while (!returnToDateSelection)
            {
                CodingSessionView.RenderCodingSessions(sessions);

                var selection = _menuView.RenderUpdateOrDeleteOptionsAndGetSelection();

                switch (selection)
                {
                    case "Change Record":
                        ManageSessionUpdate(sessions);
                        break;
                    case "Delete Record":
                        ManageSessionDelete(sessions);
                        break;
                    case "Return to Previous Menu":
                        returnToDateSelection = true;
                        break;
                }
                sessions = _service.GetSessionListByDateRange(startTime, endTime);
                Messages.RenderWelcome();

            }
        }
    }

    private void ManageSessionDelete(List<CodingSessionDataRecord> sessions)
    {
        var recordId = _inputView.GetRecordIdFromUser("delete", sessions.Count()) - 1;

        if (ConfirmDelete(sessions[recordId]))
            DeleteSession(sessions[recordId]);
        else
            Messages.ActionCancelled("deletion");

    }
    private void DeleteSession(CodingSessionDataRecord session)
    {
        _service.DeleteSessionById((int)session.Id);
    }
    private bool ConfirmDelete(CodingSessionDataRecord session)
    {
        return _inputView.GetDeleteSessionConfirmationFromUser(session);
    }
    private void ManageSessionUpdate(List<CodingSessionDataRecord> sessions)
    {
        var recordId = _inputView.GetRecordIdFromUser("update", sessions.Count()) - 1;

        var newStartTime = GetUpdatedStartTime(sessions[recordId]);
        var newEndTime = GetUpdatedEndTime(sessions[recordId], newStartTime);

        var updatedSession = new CodingSession(newStartTime, newEndTime);

        if (ConfirmUpdate(sessions[recordId], updatedSession))
            UpdateSession(updatedSession, sessions[recordId].Id);
        else
            Messages.ActionCancelled("update");

        // get confirmation
        // if confirmed => _repository.UpdateSession(updatedsession);
        // else => cancelled update message -- return to menu
    }
    private void UpdateSession(CodingSession session, long id)
    {
        var sessionDTO = new CodingSessionDataRecord {Id = id, StartTime = session.StartTime, EndTime = session.EndTime, Duration = (int)session.Duration };
        _service.UpdateSession(sessionDTO);
        Messages.ActionComplete(true, "Success", "Coding session successfully added!");
    }
    private bool ConfirmUpdate(CodingSessionDataRecord session, CodingSession updatedSession)
    {
        return _inputView.GetUpdateSessionConfirmationFromUser(session, updatedSession);
    }
    private DateTime GetUpdatedStartTime(CodingSessionDataRecord session)
    {
        var output = new DateTime();
        bool startTimeValid = false;

        while (startTimeValid == false)
        {
            var newStartTime = _inputView.GetTimeFromUser("new start time", "current start time", true);
            var result = _service.ValidateUpdatedStartTime(session, newStartTime);

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
    private DateTime GetUpdatedEndTime(CodingSessionDataRecord session, DateTime newStartTime)
    {
        var output = new DateTime();
        bool startTimeValid = false;

        while (startTimeValid == false)
        {
            var newEndTime = _inputView.GetTimeFromUser("new end time", "current end time", true);
            var result = _service.ValidateUpdatedEndTime(session, newStartTime, newEndTime);

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
    private string GetDateRangeSelectionFromUser()
    {
        return _menuView.RenderEntryViewOptionsAndGetSelection();
    }
    private (DateTime, DateTime) GetDatesBasedOnUserSelection(string selection)
    {
        bool returnToPreviousMenu = false;
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
                Messages.Error(parameter, message);
            }
            else
            {
                endTimeValid = true;
            }
        }

        return (startTime, endTime);

    }
}
