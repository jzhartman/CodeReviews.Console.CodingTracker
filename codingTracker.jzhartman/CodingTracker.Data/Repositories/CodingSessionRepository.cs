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

        public List<CodingSession> GetAll()
        {
            string sql = "Select * from CodingSessions";
            return LoadData<CodingSession>(sql);
        }


        //Not Working.....
        public List<CodingSession> GetByDateRange(DateTime startTime, DateTime endTime)
        {
            string sql = @"Select * from CodingSessions";
            return LoadData<CodingSession>(sql);
        }

        public CodingSession GetById(int id)
        {
            string sql = $"select * from CodingSessions where Id = {id}";
            return LoadData<CodingSession>(sql).FirstOrDefault();
        }

        public CodingSession GetLongestDuration()
        {
            string sql = $"select * from........... ";
            return LoadData<CodingSession>(sql).FirstOrDefault();
        }

        private List<T> LoadData<T>(string sql)
        {
            using var connection = _connectionFactory.CreateConnection();
            List<T> sessions = connection.Query<T>(sql).ToList();
            return sessions;
        }

        // Method here using the following snippet
        // using var connection = connectionFactory.CreateConnection();
        // connection.Open();
        // insert Dapper query here

    }
}
