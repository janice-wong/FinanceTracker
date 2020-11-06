using System;

namespace FinanceTracker.Database
{
    public static class DatabaseConfig
    {
        public static string GetConnectionString()
        {
                var host = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariables.DB_HOST);
                var port = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariables.DB_PORT);
                var username = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariables.DB_USER);
                var password = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariables.DB_PASSWORD);
                var database = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariables.DB_NAME);

                return $"Host={host};Port={port};Username={username};Password={password};Database={database};";
        }

        private static class ConnectionStringEnvironmentVariables
        {
            public const string DB_HOST = nameof(DB_HOST);
            public const string DB_PORT = nameof(DB_PORT);
            public const string DB_NAME = nameof(DB_NAME);
            public const string DB_USER = nameof(DB_USER);
            public const string DB_PASSWORD = nameof(DB_PASSWORD);
        }
    }
}
