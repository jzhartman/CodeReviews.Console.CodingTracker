using CodingTracker.Models.Entities;

namespace CodingTracker.Services.Interfaces
{
    public interface ICodingSessionService
    {
        List<CodingSession> GetAllCodingSessions();
    }
}