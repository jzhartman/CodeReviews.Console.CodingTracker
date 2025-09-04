using CodingTracker.Controller.Interfaces;
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
        private readonly ITrackSessionView _view;

        public TrackSessionController(ICodingSessionDataService service, ITrackSessionView view)
        {
            _service = service;
            _view = view;
        }

        public void Run()
        {
            bool returnToMainMenu = false;

            while (!returnToMainMenu)
            {

                ViewHelpers.RenderWelcome();
                var selection = _view.RenderMenuAndGetSelection();

                switch (selection)
                {
                    case "Enter Start and End Times":
                        var startTime = GetStartTime();
                        var endTime = GetEndTime(startTime);


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

        public DateTime GetStartTime()
        {
            var output = new DateTime();
            bool startTimeValid = false;
            
            while (startTimeValid == false)
            {
                output = _view.GetStartTimeFromUser();
                
                var result = _service.ValidateStartTime(output);

                if (result.IsValid)
                {
                    startTimeValid = true;
                    output = result.Value;
                    _view.ConfirmationMessage(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    _view.ErrorMessage(result.Parameter, result.Message);
                }
            }
            return output;
        }

        public DateTime GetEndTime(DateTime startTime)
        {
            var output = new DateTime();
            bool endTimeValid = false;

            while (endTimeValid == false)
            {
                output = _view.GetEndTimeFromUser();

                if (output <= startTime)
                {
                    var parameter = "End Time";
                    var message = $"The end time must be later than {startTime.ToString("yyyy-MM-dd HH:mm:ss")}";
                    _view.ErrorMessage(parameter, message);
                }
                else
                {
                    var result = _service.ValidateEndTime(output);

                    if (result.IsValid)
                    {
                        endTimeValid = true;
                        output = result.Value;
                        _view.ConfirmationMessage(result.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        _view.ErrorMessage(result.Parameter, result.Message);
                    } 
                }
            }
            return output;
        }

        public void GetTimesWithStopwatch()
        {
            // Code here
        }

        public void AddRecord()
        {

        }
    }
}
