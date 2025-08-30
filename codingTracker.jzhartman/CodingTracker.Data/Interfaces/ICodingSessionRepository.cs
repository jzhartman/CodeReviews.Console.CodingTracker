using CodingTracker.Models.Entities;

namespace CodingTracker.Data.Interfaces
{
    public interface ICodingSessionRepository
    {
        void AddSession(CodingSession session);
        List<CodingSession> GetAll();
        List<CodingSession> GetByDateRange(DateTime startTime, DateTime endTime);
        CodingSession GetById(int id);
        void UpdateEndTimeById(int id, DateTime endTime);
        void UpdateStartTimeById(int id, DateTime startTime);
        void DeleteById(int id);

    }
}