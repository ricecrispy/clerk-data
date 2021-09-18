using clerk_data_data_access.FluentMap;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;

namespace clerk_data_data_access.Factory
{
    public class PostgreSqlConnectionFactory : IDbConnectionFactory
    {
        public int CommandTimeout { get; private set; }
        private readonly PostgreSqlConnectionFactoryOptions _options;
        private readonly string _connectionString;

        static PostgreSqlConnectionFactory()
        {
            FluentMapInitializer.EnsureMapsInitialized();
        }

        public PostgreSqlConnectionFactory(PostgreSqlConnectionFactoryOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            CommandTimeout = _options.CommandTimeout;
            _connectionString = CreateDatabaseConnectionString();
        }

        public PostgreSqlConnectionFactory(IOptionsSnapshot<PostgreSqlConnectionFactoryOptions> options) : this(options.Value) { }

        public IDbConnection GetDataBaseConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        private string CreateDatabaseConnectionString()
        {
            string connectionStringTemplate = $"Port=5432;Database=${_options.Database};Trust Server Certificate=true;Application Name=ClerkDataAPIRW";
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(connectionStringTemplate)
            {
                Username = _options.UserName,
                Password = _options.Password,
                Host = _options.Host
            };
            return builder.ConnectionString;
        }
    }
}
