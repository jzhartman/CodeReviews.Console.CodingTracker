using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Parameters;
using CodingTracker.Models.Entities;
using Dapper;

namespace CodingTracker.Data.Repositories
{
    public class CodingSessionRepository : ICodingSessionRepository
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

        public List<CodingSession> GetByDateRange(DateTime begin, DateTime finish)
        {
            var dateRange = new DateRangeQuery { StartTime = begin, EndTime = finish };
            string sql = @"Select * from CodingSessions where (StartTime >= @StartTime) AND (endTime <= @EndTime)";
            return LoadData<CodingSession, DateRangeQuery>(sql, dateRange);
        }

        public CodingSession GetById(int id)
        {
            string sql = $"select * from CodingSessions where Id = {id}";
            return LoadData<CodingSession>(sql).FirstOrDefault();
        }

        public void DeleteById(int id)
        {
            string sql = $"delete from CodingSessions where Id = {id}";
            SaveData(sql);
        }

        //Not Working.....
        public List<CodingSession> GetLongestDuration()
        {
            string sql = $"select * from CodingSessions where ";
            return LoadData<CodingSession>(sql);
        }

        private List<T> LoadData<T>(string sql)
        {
            using var connection = _connectionFactory.CreateConnection();
            List<T> sessions = connection.Query<T>(sql).ToList();
            return sessions;
        }
        private List<T> LoadData<T, U>(string sql, U parameters)
        {
            using var connection = _connectionFactory.CreateConnection();
            List<T> sessions = connection.Query<T>(sql, parameters).ToList();
            return sessions;
        }

        private void SaveData(string sql)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute(sql);
        }


        // Method here using the following snippet
        // using var connection = connectionFactory.CreateConnection();
        // connection.Open();
        // insert Dapper query here

    }
}