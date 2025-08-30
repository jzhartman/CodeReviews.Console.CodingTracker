using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Services;
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
    public class MenuController : IMenuController
    {
        private readonly ICodingSessionDataService _service;

        public MenuController(ICodingSessionDataService service)
        {
            _service = service;
        }

        // Call services for business/data
        // Call views to render info

        public void Run()
        {
            ViewHelpers.RenderWelcome();
            var selection = MainMenuView.RenderMainMenuAndGetSelection();

            HandleMainMenuSelection(selection);



            //All Code Below Here Is Basic Testing ONLY -- Not part of actual flow


            //



            //var sessions = _service.GetAllCodingSessions(); 
            //CodingSessionView.RenderCodingSessions(sessions);

            //Console.WriteLine();
            //int id = (int)sessions[5].Id;
            //Console.WriteLine($"Deleting record {id}");
            //_service.DeleteById(id);
            //sessions = _service.GetAllCodingSessions();
            //CodingSessionView.RenderCodingSessions(sessions);


            //Console.WriteLine();
            var startTime = DateTime.Parse("2025-01-07 00:00:00.1234");
            var endTime = DateTime.Parse("2025-02-01 23:59:59");
            var sessions = _service.GetByDateRange(startTime, endTime);
            CodingSessionView.RenderCodingSessions(sessions);

            //_service.UpdateStartTimeById((int)sessions[9].Id, DateTime.Now);
            //_service.UpdateEndTimeById((int)sessions[15].Id, DateTime.Now);

            startTime = DateTime.Parse("2025-01-01 20:00:00");
            endTime = DateTime.Parse("2025-01-01 20:01:15");

            int duration = (int)endTime.Subtract(startTime).TotalSeconds;

            _service.AddSession(new CodingSession{StartTime = startTime, EndTime = endTime, Duration = duration});

            sessions = _service.GetAllCodingSessions();
            CodingSessionView.RenderCodingSessions(sessions);

            //Console.WriteLine();
            //var session = _service.GetById(2);
            //CodingSessionView.RenderCodingSession(session);



        }

        private void HandleMainMenuSelection(string selection)
        {
            switch (selection)
            {
                case "Track Coding":
                case "Manage Entries":
                case "View Reports":
                case "Manage Goal":
                case "Exit":
                default:
                    break;
            }
        }
    }
}
