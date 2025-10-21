using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using CodingTracker.Views.Interfaces;

namespace CodingTracker.Controller.Interfaces;

public class EntryListController : IEntryListController
{
    private readonly ICodingSessionDataService _service;
    private readonly IMenuView _menuView;
    private readonly IUserInputView _inputView;
    private readonly IConsoleOutputView _outputView;

    public EntryListController(ICodingSessionDataService service,
                                IMenuView menuView, IUserInputView inputView, IConsoleOutputView outputView)
    {
        _service = service;
        _menuView = menuView;
        _inputView = inputView;
        _outputView = outputView;
    }

    public void Run()
    {
        bool returnToPreviousMenu = false;

        while (!returnToPreviousMenu)
        {
            _outputView.WelcomeMessage();

            var dateRangeSelection = GetDateRangeSelectionFromUser();

            if (dateRangeSelection == "Return to Previous Menu") { returnToPreviousMenu = true; continue; }

            (DateTime startTime, DateTime endTime) = GetDatesBasedOnUserSelection(dateRangeSelection);
            var sessions = _service.GetSessionListByDateRange(startTime, endTime);

            bool returnToDateSelection = false;

            while (!returnToDateSelection)
            {
                _outputView.PrintCodingSessionListAsTable(sessions);

                var selection = _menuView.PrintUpdateOrDeleteOptionsAndGetSelection();

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
                _outputView.WelcomeMessage();

            }
        }
    }

    private void ManageSessionDelete(List<CodingSessionDataRecord> sessions)
    {
        var recordId = _inputView.GetRecordIdFromUser("delete", sessions.Count()) - 1;

        if (ConfirmDelete(sessions[recordId]))
            DeleteSession(sessions[recordId]);
        else
            _outputView.ActionCancelledMessage("deletion");

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
        _outputView.PrintCodingSessionToUpdateById(sessions[recordId], recordId+1);

        var newStartTime = GetUpdatedStartTime(sessions[recordId]);
        var newEndTime = GetUpdatedEndTime(sessions[recordId], newStartTime);

        var updatedSession = new CodingSession(newStartTime, newEndTime);

        if (ConfirmUpdate(sessions[recordId], updatedSession))
            UpdateSession(updatedSession, sessions[recordId].Id);
        else
            _outputView.ActionCancelledMessage("update");

        // get confirmation
        // if confirmed => _repository.UpdateSession(updatedsession);
        // else => cancelled update message -- return to menu
    }
    private void UpdateSession(CodingSession session, long id)
    {
        var sessionDTO = new CodingSessionDataRecord {Id = id, StartTime = session.StartTime, EndTime = session.EndTime, Duration = (int)session.Duration };
        _service.UpdateSession(sessionDTO);
        _outputView.ActionCompleteMessage(true, "Success", "Coding session successfully added!");
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
            var newStartTime = _inputView.GetTimeFromUser("new start time", true);
            var result = _service.ValidateUpdatedStartTime(session, newStartTime);

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
    private DateTime GetUpdatedEndTime(CodingSessionDataRecord session, DateTime newStartTime)
    {
        var output = new DateTime();
        bool startTimeValid = false;

        while (startTimeValid == false)
        {
            var newEndTime = _inputView.GetTimeFromUser("new end time", true);
            var result = _service.ValidateUpdatedEndTime(session, newStartTime, newEndTime);

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
