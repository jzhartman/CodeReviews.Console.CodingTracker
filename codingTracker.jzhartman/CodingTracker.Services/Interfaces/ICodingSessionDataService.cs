using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;

namespace CodingTracker.Services.Interfaces;
public interface ICodingSessionDataService
{
    void AddSession(CodingSession session);
    void DeleteById(int id);
    List<CodingSessionDataRecord> GetAllCodingSessions();
    List<CodingSessionDataRecord> GetSessionListByDateRange(DateTime startTime, DateTime endTime);
    CodingSessionDataRecord GetById(int id);
    void UpdateSession(CodingSessionDataRecord session);
    ValidationResult<DateTime> ValidateEndTime(DateTime input);
    ValidationResult<DateTime> ValidateStartTime(DateTime input);
    ValidationResult<DateTime> ValidateUpdatedEndTime(CodingSessionDataRecord session, DateTime newStartTime, DateTime newEndTime);
    ValidationResult<DateTime> ValidateUpdatedStartTime(CodingSessionDataRecord session, DateTime updatedStartTime);
}