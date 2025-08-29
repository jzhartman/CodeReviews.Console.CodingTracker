using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Repositories;
using CodingTracker.Models.Entities;
using CodingTracker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Services
{
    public class CodingSessionService : ICodingSessionService
    {
        private readonly ICodingSessionRepository _repository;

        public CodingSessionService(ICodingSessionRepository repository)
        {
            _repository = repository;
        }

        public List<CodingSession> GetAllCodingSessions()
        {
            return _repository.GetAll();
        }

        public List<CodingSession> GetByDateRange(DateTime startTime, DateTime endTime)
        {
            return _repository.GetByDateRange(startTime, endTime);
        }

        public CodingSession GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void DeleteById(int id)
        {
            _repository.DeleteById(id);
        }

        public void UpdateStartTimeById(int id, DateTime startTime)
        {
            _repository.UpdateStartTimeById(id, startTime);
        }

        public void UpdateEndTimeById(int id, DateTime endTime)
        {
            _repository.UpdateEndTimeById(id, endTime);
        }

        //Method to run repo.GetAll then pass data up to View



        // Methods for calling repo methods
        // Used to handle the data and create lists
        // Can contain the Console.WriteLine statements -- these will be called in the View later often

    }
}
