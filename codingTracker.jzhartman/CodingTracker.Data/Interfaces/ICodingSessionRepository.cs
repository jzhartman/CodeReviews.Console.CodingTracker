using CodingTracker.Core.Models;

namespace CodingTracker.Data.Interfaces
{
    public interface ICodingSessionRepository
    {
        List<CodingSession> GetAll();
    }
}