using CodingTracker.Data.Interfaces;
using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;
using CodingTracker.Services.Interfaces;

namespace CodingTracker.Services;
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

    public List<CodingSessionDataRecord> GetSessionListByDateRange(DateTime startTime, DateTime endTime)
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

    public void UpdateSession(CodingSessionDataRecord session)
    {
        _repository.UpdateSession(session);
    }

    public void AddSession(CodingSession session)
    {
        _repository.AddSession(session);
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
    public ValidationResult<DateTime> ValidateUpdatedStartTime(CodingSessionDataRecord session, DateTime newStartTime)
    {
        if (newStartTime == DateTime.MinValue)
            return ValidationResult<DateTime>.Success(session.StartTime);

        else if (_repository.ExistsWithinTimeFrameExcludingSessionById(session, newStartTime))
            return ValidationResult<DateTime>.Fail("Start Time", "A record already exists for this time");

        else
            return ValidationResult<DateTime>.Success(newStartTime);
    }

    public ValidationResult<DateTime> ValidateUpdatedEndTime(CodingSessionDataRecord session, DateTime newStartTime, DateTime newEndTime)
    {
        if (newEndTime == DateTime.MinValue && session.EndTime > newStartTime)
            return ValidationResult<DateTime>.Success(session.EndTime);

        else if (newEndTime <= newStartTime)
            return ValidationResult<DateTime>.Fail("End Time", "End time cannot be earlier than start time.");

        else if (_repository.ExistsWithinTimeFrameExcludingSessionById(session, newEndTime))
            return ValidationResult<DateTime>.Fail("End Time", "A record already exists for this time");

        else
            return ValidationResult<DateTime>.Success(newEndTime);
    }
}
