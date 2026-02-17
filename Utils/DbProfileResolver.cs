using System;
using System.Configuration;

namespace Review.Utils
{
    public static class DbProfileResolver
    {
        private const string DbProfileSettingName = "DbProfile";
        private const string ProdProfile = "prod";
        private const string TestProfile = "test";
        private const string ProdConnectionKey = "connectMySql_prod";
        private const string TestConnectionKey = "connectMySql_test";

        private static readonly Lazy<DbProfileConfig> CachedConfig =
            new Lazy<DbProfileConfig>(ResolveProfileConfig, true);

        public static void ValidateOrThrow()
        {
            var _ = CachedConfig.Value;
        }

        public static string GetActiveProfile()
        {
            return CachedConfig.Value.Profile;
        }

        public static string GetConnectionStringKey()
        {
            return CachedConfig.Value.ConnectionKey;
        }

        public static string ResolveConnectionString()
        {
            return CachedConfig.Value.ConnectionString;
        }

        private static DbProfileConfig ResolveProfileConfig()
        {
            string rawProfile = ConfigurationManager.AppSettings[DbProfileSettingName];
            if (string.IsNullOrWhiteSpace(rawProfile))
            {
                throw new ConfigurationErrorsException(
                    $"AppSettings key '{DbProfileSettingName}' is missing or empty. Allowed values: '{ProdProfile}', '{TestProfile}'.");
            }

            string profile = rawProfile.Trim().ToLowerInvariant();
            string connectionKey;
            if (profile == ProdProfile)
            {
                connectionKey = ProdConnectionKey;
            }
            else if (profile == TestProfile)
            {
                connectionKey = TestConnectionKey;
            }
            else
            {
                throw new ConfigurationErrorsException(
                    $"Unsupported DbProfile '{rawProfile}'. Allowed values: '{ProdProfile}', '{TestProfile}'.");
            }

            ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings[connectionKey];
            if (connection == null || string.IsNullOrWhiteSpace(connection.ConnectionString))
            {
                throw new ConfigurationErrorsException(
                    $"Connection string '{connectionKey}' is missing or empty for DbProfile '{profile}'.");
            }

            return new DbProfileConfig(profile, connectionKey, connection.ConnectionString);
        }

        private sealed class DbProfileConfig
        {
            public DbProfileConfig(string profile, string connectionKey, string connectionString)
            {
                Profile = profile;
                ConnectionKey = connectionKey;
                ConnectionString = connectionString;
            }

            public string Profile { get; private set; }
            public string ConnectionKey { get; private set; }
            public string ConnectionString { get; private set; }
        }
    }
}
