using CodingTracker.Core.Models;
using CodingTracker.Data.Interfaces;
using Dapper;

namespace CodingTracker.Data.Repositories
{
    public class CodingSessionRepository : ICodingSessionRepository
    //: ICodingSessionRepository
    {
        private readonly SqliteConnectionFactory _connectionFactory;

        public CodingSessionRepository(SqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Sample method
        public List<CodingSession> GetAll()
        {
            using var connection = _connectionFactory.CreateConnection();
            List<CodingSession> sessions = connection.Query<CodingSession>("Select * from CodingSessions order by StartDate").ToList();
            return sessions;
        }

        // Method here using the following snippet
        // using var connection = connectionFactory.CreateConnection();
        // connection.Open();
        // insert Dapper query here
    }
}
