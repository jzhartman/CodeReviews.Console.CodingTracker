using CodingTracker.Models.Entities;

namespace CodingTracker.Services.Interfaces
{
    public interface ICodingSessionDataService
    {
        void AddSession(CodingSession session);
        void DeleteById(int id);
        List<CodingSession> GetAllCodingSessions();
        List<CodingSession> GetByDateRange(DateTime startTime, DateTime endTime);
        CodingSession GetById(int id);
        void UpdateEndTimeById(int id, DateTime endTime);
        void UpdateStartTimeById(int id, DateTime startTime);
    }
}