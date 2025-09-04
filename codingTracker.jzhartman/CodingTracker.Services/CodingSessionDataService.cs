using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Repositories;
using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;
using CodingTracker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CodingTracker.Services
{
    public class CodingSessionDataService : ICodingSessionDataService
    {
        private readonly ICodingSessionRepository _repository;

        public CodingSessionDataService(ICodingSessionRepository repository)
        {
            _repository = repository;
        }

        public List<CodingSessionDataRecord> GetAllCodingSessions()
        {
            return _repository.GetAll();
        }

        public List<CodingSessionDataRecord> GetByDateRange(DateTime startTime, DateTime endTime)
        {
            return _repository.GetByDateRange(startTime, endTime);
        }

        public CodingSessionDataRecord GetById(int id)
        {
            // Validate that ID exists

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

        public ValidationResult<CodingSession> AddSession(CodingSession session)
        {

            //if(_repository.ExistsByTimeFrame())//If session.StartTime is within the bounds of an existing
            //Other validation to add in later....
            _repository.AddSession(session);
            return ValidationResult<CodingSession>.Success(session);
        }

        public ValidationResult<DateTime> ValidateStartTime(DateTime input)
        {
            if (_repository.ExistsWithinTimeFrame(input))
                return ValidationResult<DateTime>.Fail("Start Time", "A record already exists for this time");
            else
                return ValidationResult<DateTime>.Success(input);
        }
        public ValidationResult<DateTime> ValidateEndTime(DateTime input)
        {
            if (_repository.ExistsWithinTimeFrame(input))
                return ValidationResult<DateTime>.Fail("End Time", "A record already exists for this time");
            else
                return ValidationResult<DateTime>.Success(input);
        }

        //    if (input.StartTime > session.EndTime)
        //return ValidationResult<DateTime>.Fail("Start Time",
        //                                            "ERROR: End Time cannot be earlier than Start Time");
    }
}
