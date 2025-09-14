using System.Data;

namespace CodingTracker.Data.Interfaces;
public interface ISqliteConnectionFactory
{
    IDbConnection CreateConnection();
}