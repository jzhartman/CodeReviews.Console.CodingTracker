using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using CodingTracker.Data.Interfaces;

namespace CodingTracker.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ISqliteConnectionFactory _connectionFactory;

        public DatabaseInitializer(ISqliteConnectionFactory connectionfactory)
        {
            _connectionFactory = connectionfactory;
        }

        public void Initialize()
        {
            CreateTable();
            SeedData();
        }

        private void CreateTable()
        {
            using var connection = _connectionFactory.CreateConnection();

            var command = connection.CreateCommand();
            command.CommandText = @"create table if not exists CodingSessions(
	                                    Id integer primary key not null,
	                                    StartTime text not null,
	                                    EndTime text not null,
                                        Duration text not null
                                    )";
            command.ExecuteNonQuery();
        }

        private void SeedData()
        {
            using var connection = _connectionFactory.CreateConnection();

            var command = connection.CreateCommand();
            command.CommandText = @"insert into CodingSessions(StartTime, EndTime, Duration)
                                    Values  ('2025-08-01 21:00:00', '2025-08-01 23:30:00', '150'),
                                            ('2025-08-02 21:15:00', '2025-08-02 23:30:00', '135'),
                                            ('2025-08-03 22:00:00', '2025-08-03 23:45:00', '105'),
                                            ('2025-08-04 21:00:00', '2025-08-04 23:30:00', '150'),
                                            ('2025-08-05 21:00:00', '2025-08-05 23:30:00', '150');
                                    ";
            command.ExecuteNonQuery();
        }

    }
}
