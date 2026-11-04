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

        [TestCase(1, 1, 1, "anterior a 1910")]
        [TestCase(1909, 12, 31, "anterior a 1910")]
        [TestCase(2026, 10, 30, "no puede ser futura")]
        public void FechaImposible_LaRechazaConElMotivo(int anio, int mes, int dia, string motivo)
        {
            var errores = ClienteValidacion.ValidarDatosContacto(new DateTime(anio, mes, dia), null, "3874123456", Hoy);
            Assert.AreEqual(1, errores.Count);
            StringAssert.Contains(motivo, errores[0]);
        }

        [TestCase("31/12/2099", ClienteValidacion.MensajeFechaFutura)]
        [TestCase("31/02/1980", ClienteValidacion.MensajeFechaFormato)]
        [TestCase("1980-05-01", ClienteValidacion.MensajeFechaFormato)]
        [TestCase("01/01/1900", ClienteValidacion.MensajeFechaMinima)]
        public void FechaEscrita_MensajeSegunElProblema(string texto, string esperado)
        {
            Assert.AreEqual(esperado, ClienteValidacion.ValidarFechaNacimiento(texto, Hoy));
        }

        [TestCase("1/5/1980")]
        [TestCase("01/05/1980")]
        public void FechaEscrita_Valida(string texto)
        {
            Assert.IsNull(ClienteValidacion.ValidarFechaNacimiento(texto, Hoy));
        }

        [TestCase(1, "32.877.563", true)]
        [TestCase(1, "5787839", true)]
        [TestCase(1, "05.787.839", true)]
        [TestCase(1, "123456", false)]
        [TestCase(1, "123456789", false)]
        [TestCase(1, "12A45678", false)]
        [TestCase(2, "AB123456", true)]
        [TestCase(2, "AB1", false)]
        [TestCase(3, "1234567", true)]
        public void Documento_SegunElTipo(int tipo, string numero, bool valido)
        {
            Assert.AreEqual(valido, ClienteValidacion.ValidarDocumento(tipo, numero) == null);
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
