using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Parameters;
using CodingTracker.Models.Entities;
using Dapper;

namespace CodingTracker.Data.Repositories;
public class CodingSessionRepository : RepositoryGenerics, ICodingSessionRepository
{
    public CodingSessionRepository(ISqliteConnectionFactory connectionFactory) : base(connectionFactory) {}
    public List<CodingSessionDataRecord> GetAll()
    {
        string sql = "Select * from CodingSessions order by StartTime";
        return LoadData<CodingSessionDataRecord>(sql);
    }
    public List<CodingSessionDataRecord> GetByDateRange(DateTime begin, DateTime finish)
    {
        var dateRange = new DateRangeQuery { StartTime = begin, EndTime = finish };
        string sql = @"Select * from CodingSessions where (StartTime >= @StartTime) AND (endTime <= @EndTime) order by StartTime";
        return LoadData<CodingSessionDataRecord, DateRangeQuery>(sql, dateRange);
    }
    public CodingSessionDataRecord GetById(int id)
    {
        string sql = $"select * from CodingSessions where Id = {id}";
        return LoadData<CodingSessionDataRecord>(sql).FirstOrDefault();
    }
    public int GetRecordCount()
    {
        string sql = "select count(*) from CodingSessions";
        return LoadData<int>(sql).First();
    }
    public DateTime GetStartTimeOfNextRecord(DateTime time)
    {
        var parameter = new DateValue { Time = time };
        using var connection = _connectionFactory.CreateConnection();

        string sql = @"select StartTime from CodingSessions
                            where StartTime > @Time
                            order by StartTime
                            limit 1";

        return LoadData<DateTime, DateValue>(sql, parameter).FirstOrDefault();
;    }
    public DateTime GetStartTimeOfNextRecordExcludingCurrentSession(DateTime time, long id)
    {
        var parameter = new TimeUpdate { Time = time, Id = (int)id };
        using var connection = _connectionFactory.CreateConnection();

        string sql = @"select StartTime from CodingSessions
                            where StartTime > @Time
                            and Id != @Id
                            order by StartTime
                            limit 1";

        return LoadData<DateTime, TimeUpdate>(sql, parameter).FirstOrDefault();
        ;
    }
    public DateTime GetStartTimeOfLastRecord()
    {
        using var connection = _connectionFactory.CreateConnection();

        string sql = @"select StartTime from CodingSessions
                            order by StartTime DESC
                            limit 1";

        return LoadData<DateTime>(sql).FirstOrDefault();
    }
    public bool ExistsWithinTimeFrame(DateTime time)
    {
        var parameter = new DateValue { Time = time};
        using var connection = _connectionFactory.CreateConnection();

        string sql = @"select count(1) from CodingSessions
                            where StartTime <= @Time
                            and EndTime >= @Time";

        int count = connection.ExecuteScalar<int>(sql, parameter);

        return (count > 0);
    }
    public bool ExistsWithinTimeFrameExcludingSessionById(CodingSessionDataRecord session, DateTime newTime)
    {
        var parameter = new TimeUpdate { Id = (int)session.Id, Time = newTime };
        using var connection = _connectionFactory.CreateConnection();

        string sql = @"select count(1) from CodingSessions
                            where StartTime <= @Time
                            and EndTime >= @Time
                            and Id != @Id";

        int count = connection.ExecuteScalar<int>(sql, parameter);

        return (count > 0);
    }


    public void AddSession(CodingSession session)
    {
        string sql = "insert into CodingSessions (StartTime, EndTime, Duration) values (@StartTime, @EndTime, @Duration)";
        SaveData(sql, session);
    }
    public void UpdateSession(CodingSessionDataRecord session)
    {
        string sql = "update CodingSessions Set StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration where Id = @Id";
        SaveData(sql, session);
    }
    public void DeleteById(int id)
    {
        string sql = $"delete from CodingSessions where Id = {id}";
        SaveData(sql);
    }
}