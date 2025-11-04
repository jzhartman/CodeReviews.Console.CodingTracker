using CodingTracker.Data.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodingTracker.Data;
public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ISqliteConnectionFactory _connectionFactory;

    public DatabaseInitializer(ISqliteConnectionFactory connectionfactory)
    {
        _connectionFactory = connectionfactory;
    }

    public void Run()
    {
        InitializeTable("CodingSessions");
        InitializeTable("Goals");
    }

    private void InitializeTable(string tableName)
    {
        if (TableExists(tableName) == false)
        {
            CreateTable(tableName);
            SeedData(tableName);
        }
    }

    private bool TableExists(string tableName)
    {
        using var connection = _connectionFactory.CreateConnection();
        int count = connection.ExecuteScalar<int>($"select count(*) from sqlite_master where type='table' and name='{tableName}'");
        return count == 1;
    }

    private void CreateTable(string tableName)
    {
        string parameters = string.Empty;

        if (tableName == "CodingSessions")
            parameters = @"Id integer primary key not null,
	                        StartTime text not null,
	                        EndTime text not null,
                            Duration integer not null";

        if (tableName == "Goals")
            parameters = @"Id integer primary key not null,
                            Type text not null,
                            StartTime text not null,
                            EndTime text not null,
                            Status text not null";

        using var connection = _connectionFactory.CreateConnection();

        var command = connection.CreateCommand();
        command.CommandText = $"create table if not exists {tableName}({parameters})";
        command.ExecuteNonQuery();
    }

    private void SeedData(string tableName)
    {
        using var connection = _connectionFactory.CreateConnection();

        var command = connection.CreateCommand();

        string sql = string.Empty;

        if (tableName == "CodingSessions")
            sql = CreateCodingSessionsSqlString();

        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    private string CreateCodingSessionsSqlString()
    {
        string sql = "insert into CodingSessions(StartTime, EndTime, Duration)\nValues\n";
        Random rand = new Random();
        int seedRecordCount = 100;

        DateOnly date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5 * (seedRecordCount+1)));
        TimeOnly time = new TimeOnly(21,0,0);
        DateTime startDate = date.ToDateTime(time);

        DateTime endDate = startDate.AddHours(2);
        TimeSpan duration = endDate - startDate;

        for (int i = 0; i < seedRecordCount; i++)
        {
            if (i != 0) sql += ",\n";

            sql += $"('{startDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{endDate.ToString("yyyy-MM-dd HH:mm:ss")}', {duration.TotalSeconds})";
            startDate = startDate.AddDays(rand.Next(1, 6));
            endDate = startDate.AddHours(rand.Next(1, 3)).AddMinutes(rand.Next(0, 60)).AddSeconds(rand.Next(0, 60));
            duration = endDate - startDate;
        }
        sql += ";";

        return sql;
    }
}
