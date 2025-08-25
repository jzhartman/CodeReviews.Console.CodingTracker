using CodingTracker.Controller.Interfaces;
using CodingTracker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Models.Services
{
    public class MenuController : IMenuController
    {
        private readonly CodingSessionView _view;

        public MenuController(CodingSessionView codingSessionView)
        {
            _view = codingSessionView;
        }

        public void Run()
        {
            //_view.ShowMainMenu();
            //var selection = _view.GetUserSelection();
            //_service.HandleSelection(selection);

            //All Code Below Here Is Basic Testing ONLY -- Not part of actual flow
            _view.RenderAllCodingSessions();
        }


    }
}
