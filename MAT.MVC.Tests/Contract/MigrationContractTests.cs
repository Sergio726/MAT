using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace MAT.MVC.Tests.Contract
{
    [TestFixture]
    public sealed class MigrationContractTests
    {
        private static readonly string[] LegacyFolders =
        {
            "MAT.Data",
            "MAT.Data.SqlClient",
            "MAT.Services",
            "MAT.Web",
            "MAT.WCF"
        };

        private static readonly string[] RequiredF9F12Sps =
        {
            "usp_MAT_Proveedor_GetSelectList",
            "usp_MAT_PrecioServicio_GetActiveByServicioId",
            "usp_MAT_Viaje_GetSelectList",
            "usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje"
        };

        private static string RepoRoot
        {
            get
            {
                var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                while (dir != null && !File.Exists(Path.Combine(dir.FullName, "MAT.sln")))
                {
                    dir = dir.Parent;
                }
                if (dir == null)
                {
                    throw new InvalidOperationException("No se encontró MAT.sln desde el directorio de tests.");
                }
                return dir.FullName;
            }
        }

        [Test]
        public void Entities_NoGeneratedCsFiles()
        {
            var entitiesDir = Path.Combine(RepoRoot, "MAT.Entities");
            var generated = Directory.GetFiles(entitiesDir, "*.generated.cs", SearchOption.AllDirectories);
            Assert.AreEqual(0, generated.Length, "Quedan archivos *.generated.cs en MAT.Entities: " + string.Join(", ", generated));
        }

        [Test]
        public void Solution_NoLegacyNetTiersFolders()
        {
            foreach (var folder in LegacyFolders)
            {
                var path = Path.Combine(RepoRoot, folder);
                Assert.IsFalse(Directory.Exists(path), "Carpeta legado NetTiers presente: " + folder);
            }
        }

        [Test]
        public void MatDb_F9F12StoredProceduresExist()
        {
            var spDir = Path.Combine(RepoRoot, "MAT.DB", "dbo", "Stored Procedures");
            Assert.IsTrue(Directory.Exists(spDir), "Directorio de SPs no encontrado: " + spDir);

            var dbSps = Directory.GetFiles(spDir, "usp_MAT_*.sql")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .ToList();

            foreach (var sp in RequiredF9F12Sps)
            {
                CollectionAssert.Contains(dbSps, sp, "SP F9/F12 faltante en MAT.DB: " + sp);
            }
        }
    }
}
