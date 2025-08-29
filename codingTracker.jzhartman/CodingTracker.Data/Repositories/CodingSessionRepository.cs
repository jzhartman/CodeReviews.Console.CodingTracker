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
            string sql = "Select * from CodingSessions order by StartTime";
            return LoadData<CodingSession>(sql);
        }

        public List<CodingSession> GetByDateRange(DateTime begin, DateTime finish)
        {
            var dateRange = new DateRangeQuery { StartTime = begin, EndTime = finish };
            string sql = @"Select * from CodingSessions where (StartTime >= @StartTime) AND (endTime <= @EndTime) order by StartTime";
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

        public void UpdateStartTimeById(int id, DateTime startTime)
        {
            var parameters = new StartTimeUpdate { Id = id, StartTime = startTime };
            string sql = "update CodingSessions Set StartTime = @StartTime where Id = @Id";
            SaveData(sql, parameters);
        }

        public void UpdateEndTimeById(int id, DateTime endTime)
        {
            var parameters = new EndTimeUpdate { Id = id, EndTime = endTime };
            string sql = "update CodingSessions Set EndTime = @EndTime where Id = @Id";
            SaveData(sql, parameters);
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

        private void SaveData<T>(string sql, T parameters)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute(sql, parameters);
        }


        // Method here using the following snippet
        // using var connection = connectionFactory.CreateConnection();
        // connection.Open();
        // insert Dapper query here

    }
}