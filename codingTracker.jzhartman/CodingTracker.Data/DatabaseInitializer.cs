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
        private readonly SqliteConnectionFactory _connectionFactory;

        public DatabaseInitializer(SqliteConnectionFactory connectionfactory)
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
            command.CommandText = " SQL HERE";
            command.ExecuteNonQuery();
        }

        private void SeedData()
        {
            using var connection = _connectionFactory.CreateConnection();

            var command = connection.CreateCommand();
            command.CommandText = " SQL HERE";
            command.ExecuteNonQuery();
        }

    }
}
