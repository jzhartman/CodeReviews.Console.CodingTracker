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




            }


            // Get Update/Delete/New Date Range/Return

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
            var startTime = _inputView.GetStartTimeFromUser();
            var endTime = new DateTime();
            bool endTimeValid = false;

            while (endTimeValid == false)
            {
                endTime = _inputView.GetEndTimeFromUser();

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
