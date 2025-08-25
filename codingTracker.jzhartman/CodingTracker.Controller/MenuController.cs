using CodingTracker.Controller.Interfaces;
using CodingTracker.Services;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Controller
{
    public class MenuController : IMenuController
    {
        private readonly ICodingSessionService _service;

        public MenuController(ICodingSessionService service)
        {
            _service = service;
        }

        // Call services for business/data
        // Call views to render info

        public void Run()
        {
            //_view.ShowMainMenu();
            //var selection = _view.GetUserSelection();
            //_service.HandleSelection(selection);

            //All Code Below Here Is Basic Testing ONLY -- Not part of actual flow
            var sessions = _service.GetAllCodingSessions(); 
            CodingSessionView.RenderAllCodingSessions(sessions);
        }


    }
}
