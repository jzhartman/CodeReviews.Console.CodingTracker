using CodingTracker.Models.Entities;

namespace CodingTracker.Data.Interfaces
{
    public interface ICodingSessionRepository
    {
        List<CodingSession> GetAll();
        List<CodingSession> GetByDateRange(DateTime startTime, DateTime endTime);
        CodingSession GetById(int id);
    }
}