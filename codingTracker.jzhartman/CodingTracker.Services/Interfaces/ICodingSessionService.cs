using CodingTracker.Models.Entities;

namespace CodingTracker.Services.Interfaces
{
    public interface ICodingSessionService
    {
        List<CodingSession> GetAllCodingSessions();
        List<CodingSession> GetByDateRange(DateTime startTime, DateTime endTime);
    }
}