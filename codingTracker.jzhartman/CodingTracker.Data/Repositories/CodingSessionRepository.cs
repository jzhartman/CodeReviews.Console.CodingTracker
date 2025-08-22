using CodingTracker.Data.Interfaces;
using CodingTracker.Models.Entities;
using Dapper;

namespace CodingTracker.Data.Repositories
{
    public class CodingSessionRepository : ICodingSessionRepository
    //: ICodingSessionRepository
    {
        private readonly ISqliteConnectionFactory _connectionFactory;

        public CodingSessionRepository(ISqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Sample method
        public List<CodingSession> GetAll()
        {
            using var connection = _connectionFactory.CreateConnection();
            List<CodingSession> sessions = connection.Query<CodingSession>("Select * from CodingSessions").ToList();
            return sessions;
        }

        // Method here using the following snippet
        // using var connection = connectionFactory.CreateConnection();
        // connection.Open();
        // insert Dapper query here
    }
}
