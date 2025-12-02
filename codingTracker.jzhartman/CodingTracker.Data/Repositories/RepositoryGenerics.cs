using CodingTracker.Data.Interfaces;
using Dapper;

namespace CodingTracker.Data.Repositories;
public class RepositoryGenerics
{
    protected internal readonly ISqliteConnectionFactory _connectionFactory;

    public RepositoryGenerics(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    protected internal List<T> LoadData<T>(string sql)
    {
        using var connection = _connectionFactory.CreateConnection();
        List<T> sessions = connection.Query<T>(sql).ToList();
        return sessions;
    }
    protected internal List<T> LoadData<T, U>(string sql, U parameters)
    {
        using var connection = _connectionFactory.CreateConnection();
        List<T> sessions = connection.Query<T>(sql, parameters).ToList();
        return sessions;
    }
    protected internal void SaveData(string sql)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql);
    }
    protected internal void SaveData<T>(string sql, T parameters)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Execute(sql, parameters);
    }
}
