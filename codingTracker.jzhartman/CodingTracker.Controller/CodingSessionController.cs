using CodingTracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Controller
{
    internal class CodingSessionController
    {
        private readonly CodingSessionRepository _repository;

        public CodingSessionController(CodingSessionRepository repository)
        {
            _repository = repository;
        }

        //Method to run repo.GetAll then pass data up to View



        // Methods for calling repo methods
        // Used to handle the data and create lists
        // Can contain the Console.WriteLine statements -- these will be called in the View later often

    }
}
