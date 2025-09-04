using CodingTracker.Models.Entities;

namespace CodingTracker.Data.Interfaces
{
    public interface ICodingSessionRepository
    {
        void AddSession(CodingSession session);
        List<CodingSessionDataRecord> GetAll();
        List<CodingSessionDataRecord> GetByDateRange(DateTime startTime, DateTime endTime);
        CodingSessionDataRecord GetById(int id);
        void UpdateEndTimeById(int id, DateTime endTime);
        void UpdateStartTimeById(int id, DateTime startTime);
        void DeleteById(int id);
        bool ExistsWithinTimeFrame(DateTime time);
    }
}