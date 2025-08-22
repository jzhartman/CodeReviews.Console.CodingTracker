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
        private readonly CodingSessionView _codingSessionView;

        public MenuController(CodingSessionView codingSessionView)
        {
            _codingSessionView = codingSessionView;
        }

        public void Run()
        {
            _codingSessionView.RenderAllCodingSessions();
        }


    }
}
