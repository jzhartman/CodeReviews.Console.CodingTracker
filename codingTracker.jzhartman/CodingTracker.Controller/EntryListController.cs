using CodingTracker.Models.Entities;
using CodingTracker.Services;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using CodingTracker.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CodingTracker.Controller.Interfaces
{
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
                (returnToPreviousMenu, var sessions) = GetSessionListBasedOnUserDateSelection();

                if (returnToPreviousMenu) continue;

                CodingSessionView.RenderCodingSessions(sessions);

                bool returnToDateSelection = false;

                while (!returnToDateSelection)
                {
                    var selection = _menuView.RenderUpdateOrDeleteOptionsAndGetSelection();
                    var recordId = 0;

                    switch (selection)
                    {
                        case "Change Record":
                            recordId = _inputView.GetRecordIdFromUser("update", sessions.Count());
                            ManageUserUpdate(sessions[recordId-1]);
                            break;
                        case "Delete Record":
                            recordId = _inputView.GetRecordIdFromUser("delete", sessions.Count());

                            break;
                        case "Return to Previous Menu":
                            returnToDateSelection = true;
                            break;
                    }

                }


            }


            // Get Update/Delete/New Date Range/Return

        }

        private void ManageUserUpdate(CodingSessionDataRecord session)
        {
            var newStartTime = GetUpdatedStartTime(session);
            var newEndTime = GetUpdatedEndTime(session, newStartTime);

            var updatedSession = new CodingSession(newStartTime, newEndTime);

            if (ConfirmUpdate(session, updatedSession))
                UpdateSession(updatedSession, session.Id);

            // get confirmation
            // if confirmed => _repository.UpdateSession(updatedsession);
            // else => cancelled update message -- return to menu
        }

        private void UpdateSession(CodingSession session, long id)
        {
            var sessionDTO = new CodingSessionDataRecord {Id = id, StartTime = session.StartTime, EndTime = session.EndTime, Duration = (int)session.Duration };
            _service.UpdateSession(sessionDTO);
            Messages.ActionCompleteMessage(true, "Success", "Coding session successfully added!");
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
                    Messages.ConfirmationMessage(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    Messages.ErrorMessage(result.Parameter, result.Message);
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
                    Messages.ConfirmationMessage(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    Messages.ErrorMessage(result.Parameter, result.Message);
                }
            }
            return output;
        }
        private (bool, List<CodingSessionDataRecord>) GetSessionListBasedOnUserDateSelection()
        {
            bool returnToPreviousMenu = false;
            var sessions = new List<CodingSessionDataRecord>();
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();

            var selection = _menuView.RenderEntryViewOptionsAndGetSelection();

            switch (selection)
            {
                case "All":
                    sessions = _service.GetAllCodingSessions();
                    break;
                case "One Year":
                    sessions = GetSessionsForPastYear();
                    break;
                case "Year to Date":
                    sessions = GetSessionsForYearToDate();
                    break;
                case "Enter Date Range":
                    sessions = GetSessionsByDateRange();
                    break;
                case "Return to Previous Menu":
                    returnToPreviousMenu = true;
                    break;
            }

            return (returnToPreviousMenu, sessions);
        }

        private List<CodingSessionDataRecord> GetSessionsForPastYear()
        {
            var endTime = DateTime.Now;
            var startTime = endTime.AddYears(-1);

            return _service.GetByDateRange(startTime, endTime);
        }
        private List<CodingSessionDataRecord> GetSessionsForYearToDate()
        {
            var endTime = DateTime.Now;
            var startTime = new DateTime(endTime.Year, 1, 1);
            return _service.GetByDateRange(startTime, endTime);
        }
        private List<CodingSessionDataRecord> GetSessionsByDateRange()
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
                    Messages.ErrorMessage(parameter, message);
                }
                else
                {
                    endTimeValid = true;
                }
            }

            return _service.GetByDateRange(startTime, endTime);

        }


    }
}
