using CodingTracker.Models.Entities;
namespace CodingTracker.Data.Interfaces;
public interface ICodingSessionRepository
{
    void AddSession(CodingSession session);
    List<CodingSessionDataRecord> GetAll();
    List<CodingSessionDataRecord> GetByDateRange(DateTime startTime, DateTime endTime);
    CodingSessionDataRecord GetById(int id);
    void UpdateSession(CodingSessionDataRecord session);
    void DeleteById(int id);
    bool ExistsWithinTimeFrame(DateTime time);
    bool ExistsWithinTimeFrameExcludingSessionById(CodingSessionDataRecord session, DateTime newTime);
    DateTime GetStartTimeOfNextRecord(DateTime time);
}