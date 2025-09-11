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
                            ManageUserUpdate(sessions[recordId]);
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
            var newStartTime = _inputView.GetTimeFromUser("new start time", true);

            if (newStartTime == null) newStartTime = session.StartTime;
            // Run validation on start time
            // Same as previous validation except it can be >= current start time

            var newEndTime = _inputView.GetTimeFromUser("new end time", true);
            // Validate
            

            // Run validation on both dates
            // update if good
        }

        private DateTime GetUpdatedStartTime(DateTime originalTime)
        {
            var output = new DateTime();
            bool startTimeValid = false;

            while (startTimeValid == false)
            {
                //output = _inputView.GetUpdatedStartTimeFromUser(originalTime);

                // Allow user to enter a start date with almost no validation -- will validate later?
                startTimeValid = true;


                //var result = _service.ValidateStartTime(output);

                //if (result.IsValid)
                //{
                //    startTimeValid = true;
                //    output = result.Value;
                //    Messages.ConfirmationMessage(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //}
                //else
                //{
                //    Messages.ErrorMessage(result.Parameter, result.Message);
                //}
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
