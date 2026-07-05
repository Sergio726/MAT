using System;
using System.Configuration;
using NUnit.Framework;

namespace MAT.Integration.Tests
{
    public abstract class IntegrationTestBase
    {
        [OneTimeSetUp]
        public void ConfigureConnectionFromEnvironment()
        {
            var connectionString = Environment.GetEnvironmentVariable("MAT_TEST_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Assert.Ignore("Definir MAT_TEST_CONNECTION_STRING para tests de integracion.");
            }

            ApplyConnectionString(connectionString.Trim());
        }

        private static void ApplyConnectionString(string connectionString)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.ConnectionStrings.ConnectionStrings["MAT.Data.ConnectionString"];
            if (settings == null)
            {
                config.ConnectionStrings.ConnectionStrings.Add(
                    new ConnectionStringSettings("MAT.Data.ConnectionString", connectionString, "System.Data.SqlClient"));
            }
            else
            {
                settings.ConnectionString = connectionString;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
