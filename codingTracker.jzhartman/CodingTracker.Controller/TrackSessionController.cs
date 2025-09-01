using CodingTracker.Controller.Interfaces;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
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

        public TrackSessionController(ICodingSessionDataService service)
        {
            _service = service;
        }

        public void Run()
        {
            bool returnToMainMenu = false;

            while (!returnToMainMenu)
            {

                ViewHelpers.RenderWelcome();
                var selection = TrackSessionView.RenderTrackSessionMenuAndGetSelection();

                switch (selection)
                {
                    case "Enter Start and End Times":
                        GetStartAndEndTimes();
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

        public void GetStartAndEndTimes()
        {
            // Code here
        }

        public void GetTimesWithStopwatch()
        {
            // Code here
        }
    }
}
