using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Services;
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
    public class MainMenuController : IMainMenuController
    {
        private readonly ICodingSessionDataService _service;
        private readonly ITrackSessionController _trackController;
        private readonly IMainMenuView _view;

        public MainMenuController(ICodingSessionDataService service, ITrackSessionController trackController, IMainMenuView view)
        {
            _service = service;
            _trackController = trackController;
            _view = view;
        }

        public void Run()
        {
            bool exitApp = false;

            while (!exitApp)
            {

                ViewHelpers.RenderWelcome();
                var selection = _view.RenderMenuAndGetSelection();

                switch (selection)
                {
                    case "Track Session":    // Submenu: Enter times/Stopwatch/Return
                        _trackController.Run();
                        break;
                    case "Manage Entries":  // Submenu: View Entries (range or all)/Update/Delete
                        break;
                    case "View Reports":    // Enter Range-Period??? --> Print all records for period --> Print report data
                        break;
                    case "Manage Goal":     // Print current goal+progress/Give option to change goal
                        break;
                    case "Exit":            // Generic goodbye message
                        exitApp = true;
                        break;
                    default:
                        break;
                }
            }


            //All Code Below Here Is Basic Testing ONLY -- Not part of actual flow


            //var sessions = _service.GetAllCodingSessions(); 
            //CodingSessionView.RenderCodingSessions(sessions);

            //Console.WriteLine();
            //int id = (int)sessions[5].Id;
            //Console.WriteLine($"Deleting record {id}");
            //_service.DeleteById(id);
            //sessions = _service.GetAllCodingSessions();
            //CodingSessionView.RenderCodingSessions(sessions);


            //Console.WriteLine();
            //var startTime = DateTime.Parse("2025-01-07 00:00:00.1234");
            //var endTime = DateTime.Parse("2025-02-01 23:59:59");
            //var sessions = _service.GetByDateRange(startTime, endTime);
            //CodingSessionView.RenderCodingSessions(sessions);

            //_service.UpdateStartTimeById((int)sessions[9].Id, DateTime.Now);
            //_service.UpdateEndTimeById((int)sessions[15].Id, DateTime.Now);

            //startTime = DateTime.Parse("2025-01-01 20:00:00");
            //endTime = DateTime.Parse("2025-01-01 20:01:15");

            //int duration = (int)endTime.Subtract(startTime).TotalSeconds;

            //_service.AddSession(new CodingSession{StartTime = startTime, EndTime = endTime, Duration = duration});

            //sessions = _service.GetAllCodingSessions();
            //CodingSessionView.RenderCodingSessions(sessions);

            //Console.WriteLine();
            //var session = _service.GetById(2);
            //CodingSessionView.RenderCodingSession(session);



        }
    }
}
