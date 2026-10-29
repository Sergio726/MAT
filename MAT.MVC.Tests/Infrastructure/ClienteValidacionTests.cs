using System;
using MAT.MVC.Infrastructure;
using NUnit.Framework;

namespace MAT.MVC.Tests.Infrastructure
{
    [TestFixture]
    public sealed class ClienteValidacionTests
    {
        private static readonly DateTime Hoy = new DateTime(2026, 10, 29);

        [Test]
        public void DatosCompletos_SinErrores()
        {
            CollectionAssert.IsEmpty(ClienteValidacion.ValidarDatosContacto(new DateTime(1980, 5, 1), null, "3874123456", Hoy));
            CollectionAssert.IsEmpty(ClienteValidacion.ValidarDatosContacto(new DateTime(1980, 5, 1), "0387-4123456", null, Hoy));
        }

        [Test]
        public void SoloCodigoDeArea_PideTelefonoCompleto()
        {
            var errores = ClienteValidacion.ValidarDatosContacto(new DateTime(1980, 5, 1), "387", "0387-", Hoy);
            Assert.AreEqual(1, errores.Count);
            StringAssert.Contains("teléfono o celular completo", errores[0]);
        }

        [Test]
        public void FechaFaltante_PideFecha()
        {
            var errores = ClienteValidacion.ValidarDatosContacto(null, null, "3874123456", Hoy);
            Assert.AreEqual(1, errores.Count);
            StringAssert.Contains("Ingrese la fecha de nacimiento", errores[0]);
        }

        [TestCase(1, 1, 1)]
        [TestCase(1909, 12, 31)]
        [TestCase(2026, 10, 30)]
        public void FechaImposible_LaRechaza(int anio, int mes, int dia)
        {
            var errores = ClienteValidacion.ValidarDatosContacto(new DateTime(anio, mes, dia), null, "3874123456", Hoy);
            Assert.AreEqual(1, errores.Count);
            StringAssert.Contains("no es válida", errores[0]);
        }

        [Test]
        public void NacidoHoy_EsValido()
        {
            CollectionAssert.IsEmpty(ClienteValidacion.ValidarDatosContacto(Hoy, null, "3874123456", Hoy));
        }

        [Test]
        public void HoyArgentina_EsUnaFechaSinHora()
        {
            var hoy = ClienteValidacion.HoyArgentina();
            Assert.AreEqual(TimeSpan.Zero, hoy.TimeOfDay);
            Assert.That((hoy - DateTime.UtcNow.Date).TotalDays, Is.InRange(-1, 0));
        }
    }
}
