using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;

namespace CodingTracker.Services.Interfaces
{
    public interface ICodingSessionDataService
    {
        ValidationResult<CodingSession> AddSession(CodingSession session);
        void DeleteById(int id);
        List<CodingSessionDataRecord> GetAllCodingSessions();
        List<CodingSessionDataRecord> GetByDateRange(DateTime startTime, DateTime endTime);
        CodingSessionDataRecord GetById(int id);
        void UpdateEndTimeById(int id, DateTime endTime);
        void UpdateStartTimeById(int id, DateTime startTime);
        ValidationResult<DateTime> ValidateEndTime(DateTime input);
        ValidationResult<DateTime> ValidateStartTime(DateTime input);
    }
}