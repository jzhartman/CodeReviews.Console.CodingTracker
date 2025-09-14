using CodingTracker.Data.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Data;
public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ISqliteConnectionFactory _connectionFactory;

    public DatabaseInitializer(ISqliteConnectionFactory connectionfactory)
    {
        _connectionFactory = connectionfactory;
    }

    public void Initialize()
    {
        if (TableExists() == false)
        {
            CreateTable();
            SeedData();
        }
    }

    private bool TableExists()
    {
        using var connection = _connectionFactory.CreateConnection();
        int count = connection.ExecuteScalar<int>("select count(*) from sqlite_master where type='table' and name='CodingSessions'");
        return count == 1;
    }

    private void CreateTable()
    {
        using var connection = _connectionFactory.CreateConnection();

        var command = connection.CreateCommand();
        command.CommandText = @"create table if not exists CodingSessions(
	                                    Id integer primary key not null,
	                                    StartTime text not null,
	                                    EndTime text not null,
                                        Duration integer not null
                                    )";
        command.ExecuteNonQuery();
    }

    private void SeedData()
    {
        using var connection = _connectionFactory.CreateConnection();

        var command = connection.CreateCommand();

        string sql = CreateSqlString();

        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    private string CreateSqlString()
    {
        string sql = "insert into CodingSessions(StartTime, EndTime, Duration)\nValues\n";

        Random rand = new Random();

        DateTime startDate = DateTime.Parse("2025-01-01 21:00:00");
        DateTime endDate = startDate.AddHours(2);
        TimeSpan duration = endDate - startDate;

        for (int i = 0; i < 50; i++)
        {
            if (i != 0) sql += ",\n";

            sql += $"('{startDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{endDate.ToString("yyyy-MM-dd HH:mm:ss")}', {duration.TotalSeconds})";
            startDate = startDate.AddDays(1);
            endDate = startDate.AddHours(rand.Next(1, 3)).AddMinutes(rand.Next(0, 60)).AddSeconds(rand.Next(0, 60));
            duration = endDate - startDate;
        }
        sql += ";";

        return sql;
    }
}
