using CodingTracker.Models.Entities;

namespace CodingTracker.Services.Interfaces
{
    public interface ICodingSessionService
    {
        void DeleteById(int id);
        List<CodingSession> GetAllCodingSessions();
        List<CodingSession> GetByDateRange(DateTime startTime, DateTime endTime);
        CodingSession GetById(int id);
    }
}