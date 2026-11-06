using MAT.MVC.Models.Home;
using NUnit.Framework;

namespace MAT.MVC.Tests.Infrastructure
{
    [TestFixture]
    public sealed class ProximaSalidaTests
    {
        // "Últimos lugares" = 15 % libre o menos (60 lugares → hasta 9; 50 → hasta 7)
        [TestCase(60, 0, "completo")]
        [TestCase(60, 1, "ultimos")]
        [TestCase(60, 9, "ultimos")]
        [TestCase(60, 10, "disponible")]
        [TestCase(50, 7, "ultimos")]
        [TestCase(50, 8, "disponible")]
        [TestCase(0, 0, "sinPasajes")]
        public void Estado_SegunLugaresLibres(int total, int libres, string esperado)
        {
            Assert.AreEqual(esperado, new ProximaSalidaDto { Total = total, Libres = libres }.Estado);
        }

        [Test]
        public void Ocupados_EsTotalMenosLibres()
        {
            Assert.AreEqual(17, new ProximaSalidaDto { Total = 60, Libres = 43, Vendidos = 7, PreReservas = 10 }.Ocupados);
            Assert.AreEqual(0, new ProximaSalidaDto { Total = 0, Libres = 0 }.Ocupados);
        }
    }
}
