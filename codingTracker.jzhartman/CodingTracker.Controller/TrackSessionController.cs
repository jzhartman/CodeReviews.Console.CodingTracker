using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using CodingTracker.Views.Interfaces;
using CodingTracker.Views.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Controller
{
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

                Messages.RenderWelcome();
                var selection = _menuView.RenderTrackingMenuAndGetSelection();

                switch (selection)
                {
                    case "Enter Start and End Times":
                        var startTime = GetStartTime();
                        var endTime = GetEndTime(startTime);
                        AddSession(new CodingSession(startTime, endTime));
                        break;
                    case "Begin Timer":
                        GetTimesWithStopwatch();
                        break;
                    case "Return to Main Menu":
                        returnToMainMenu = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private DateTime GetStartTime()
        {
            var output = new DateTime();
            bool startTimeValid = false;
            
            while (startTimeValid == false)
            {
                output = _inputView.GetStartTimeFromUser();
                
                var result = _service.ValidateStartTime(output);

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
        private DateTime GetEndTime(DateTime startTime)
        {
            var output = new DateTime();
            bool endTimeValid = false;

            while (endTimeValid == false)
            {
                output = _inputView.GetEndTimeFromUser();

                if (output <= startTime)
                {
                    var parameter = "End Time";
                    var message = $"The end time must be later than {startTime.ToString("yyyy-MM-dd HH:mm:ss")}";
                    Messages.ErrorMessage(parameter, message);
                }
                else
                {
                    var result = _service.ValidateEndTime(output);

                    if (result.IsValid)
                    {
                        endTimeValid = true;
                        output = result.Value;
                        Messages.ConfirmationMessage(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        Messages.ErrorMessage(result.Parameter, result.Message);
                    } 
                }
            }
            return output;
        }
        private void AddSession(CodingSession session)
        {
            _service.AddSession(session);
            Messages.ActionCompleteMessage(true, "Success", "Coding session successfully added!");
        }

        private void GetTimesWithStopwatch()
        {
            // Code here
        }
    }
}
