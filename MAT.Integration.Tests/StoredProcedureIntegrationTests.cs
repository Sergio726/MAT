using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;

namespace MAT.Integration.Tests
{
    [TestFixture]
    [Category("Integration")]
    public sealed class StoredProcedureIntegrationTests : IntegrationTestBase
    {
        private static readonly string[] RequiredF9F12Procedures =
        {
            "usp_MAT_Proveedor_GetSelectList",
            "usp_MAT_PrecioServicio_GetActiveByServicioId",
            "usp_MAT_Viaje_GetSelectList",
            "usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje"
        };

        [Test]
        public void StoredProcedures_F9F12_ExistInDatabase()
        {
            var missing = new List<string>();
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(@"
SELECT name FROM sys.procedures WHERE name = @Name AND schema_id = SCHEMA_ID('dbo')", connection))
            {
                connection.Open();
                foreach (var procedureName in RequiredF9F12Procedures)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", procedureName);
                    var result = command.ExecuteScalar();
                    if (result == null)
                    {
                        missing.Add(procedureName);
                    }
                }
            }

            Assert.AreEqual(0, missing.Count, "SPs F9/F12 no desplegados en la BD: " + string.Join(", ", missing));
        }

        [Test]
        public void DBHelper_ExecuteScalar_CountReservas_DoesNotThrow()
        {
            var habitacionId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var viajeId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var count = MAT.Utilities.LookupDataAccess.CountReservasByHabitacionAndViaje(habitacionId, viajeId);

            Assert.GreaterOrEqual(count, 0);
        }

        private static SqlConnection OpenConnection()
        {
            var connectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["MAT.Data.ConnectionString"]
                .ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
