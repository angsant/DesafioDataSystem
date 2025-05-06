using Dapper;
using Microsoft.Data.SqlClient;

namespace DesafioDataSystem.Infrasturcture.Migrations
{
    public static class DatabaseMigration
    {
        public static void Migrate(string connectionString)
        {
            EnsureDatabaseCreated_SqlServer(connectionString);
        }

        private static void EnsureDatabaseCreated_SqlServer(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.InitialCatalog;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);

            var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

            if (records.Any() == false)
                dbConnection.Execute($"CREATE DATABASE {databaseName}");

        }
    }
}
