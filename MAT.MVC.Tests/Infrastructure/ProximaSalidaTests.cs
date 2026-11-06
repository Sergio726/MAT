using MAT.MVC.Models.Home;
using NUnit.Framework;

namespace MAT.MVC.Tests.Infrastructure
{
    [TestFixture]
    public sealed class ProximaSalidaTests
    {
        [TestCase(60, 0, "completo")]
        [TestCase(60, 1, "ultimos")]
        [TestCase(60, 5, "ultimos")]
        [TestCase(60, 6, "disponible")]
        [TestCase(0, 0, "sinPasajes")]
        public void Estado_SegunLugaresLibres(int total, int libres, string esperado)
        {
            Assert.AreEqual(esperado, new ProximaSalidaDto { Total = total, Libres = libres }.Estado);
        }
    }
}
