using CodingTracker.Models.Entities;

namespace CodingTracker.Data.Interfaces
{
    public interface ICodingSessionRepository
    {
        List<CodingSession> GetAll();
    }
}