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


    //  MOVE TO CRUD SERVICES FILE

    public void DeleteSessionById(int id)
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
    public List<CodingSessionDataRecord> GetAllCodingSessions()
    {
        return _repository.GetAll();
    }
    public List<CodingSessionDataRecord> GetSessionListByDateRange(DateTime startTime, DateTime endTime)
    {
        return _repository.GetByDateRange(startTime, endTime);
    }
    public CodingSessionDataRecord GetSessionById(int id)
    {
        // Validate that ID exists

        return _repository.GetById(id);
    }





    //  Separate to new file?
    // Provides Calculated Values Based On User Input

    public (DateTime, DateTime) GetBasicDateRange(string selection)
    {
        DateTime startTime = new DateTime();
        DateTime endTime = new DateTime();

        switch (selection)
        {
            case "All":
                (startTime, endTime) = GetAllDates();
                break;
            case "Past Year":
                (startTime, endTime) = GetDateRangeForPastYear();
                break;
            case "Year to Date":
                (startTime, endTime) = GetDateRangeForYearToDate();
                break;
        }

        return (startTime, endTime);
    }
    public DateTime GetEndTimeForAdvancedDateRange(string selection, DateTime startTime)
    {
        DateTime endTime = new DateTime();

        switch (selection)
        {

            case "Custom Week":
                endTime = endTime = startTime.AddDays(7);
                break;
            case "Custom Month":
                endTime = startTime.AddMonths(1);
                break;
            case "Custom Year":
                endTime = startTime.AddYears(1);
                break;
        }

        return endTime;
    }
    private (DateTime, DateTime) GetAllDates()
    {
        var startTime = DateTime.MinValue;
        var endTime = DateTime.MaxValue;
        return (startTime, endTime);
    }
    private (DateTime, DateTime) GetDateRangeForPastYear()
    {
        var endTime = DateTime.Now;
        var startTime = endTime.AddYears(-1);
        return (startTime, endTime);
    }
    private (DateTime, DateTime) GetDateRangeForYearToDate()
    {
        var endTime = DateTime.Now;
        var startTime = new DateTime(endTime.Year, 1, 1);
        return (startTime, endTime);
    }









    // MOVE TO VALIDATION SERVICES FILE

    public ValidationResult<DateTime> ValidateStartTime(DateTime input)
    {
        if (_repository.ExistsWithinTimeFrame(input))
            return ValidationResult<DateTime>.Fail("Start Time", "A record already exists for this time");
        else if (input > DateTime.Now)
            return ValidationResult<DateTime>.Fail("Start Time", "Cannot enter a future time");
        else
            return ValidationResult<DateTime>.Success(input);
    }
    public ValidationResult<DateTime> ValidateEndTime(DateTime input, DateTime startTime)
    {
        var nextRecordStartTime = _repository.GetStartTimeOfNextRecord(input);

        if (_repository.ExistsWithinTimeFrame(input))
            return ValidationResult<DateTime>.Fail("End Time", "A record already exists for this time");
        else if (TimeOverlapsNextEntry(input, startTime))
            return ValidationResult<DateTime>.Fail("End Time", "A record already exists for this time");
        else if (input <= startTime)
            return ValidationResult<DateTime>.Fail("End Time", $"The end time must be later than {startTime.ToString("yyyy-MM-dd HH:mm:ss")}");
        else if (input > DateTime.Now)
            return ValidationResult<DateTime>.Fail("Start Time", "Cannot enter a future time");
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
    public ValidationResult<DateTime> ValidateReportEndTime(DateTime input, DateTime startTime)
    {
        if (input <= startTime)
            return ValidationResult<DateTime>.Fail("End Time", $"The end time must be later than {startTime.ToString("yyyy-MM-dd HH:mm:ss")}");
        else
            return ValidationResult<DateTime>.Success(input);
    }
    public ValidationResult<DateTime> ValidateDateRangeStartTime(DateTime input)
    {
        if (input > DateTime.Now)
            return ValidationResult<DateTime>.Fail("Start Time", "Cannot enter a future time");
        else
            return ValidationResult<DateTime>.Success(input);
    }

    private bool TimeOverlapsNextEntry(DateTime input, DateTime startTime)
    {
        var nextRecordStartTime = _repository.GetStartTimeOfNextRecord(startTime);

        if (nextRecordStartTime == null || nextRecordStartTime == DateTime.MinValue)
            return false;
        else if (input >= nextRecordStartTime)
            return true;
        else
            return false;
    }
}
