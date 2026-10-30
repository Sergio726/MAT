using MAT.MVC.Infrastructure;
using NUnit.Framework;

namespace MAT.MVC.Tests.Infrastructure
{
    [TestFixture]
    public sealed class WhatsAppHelperTests
    {
        [TestCase("3874123456", "5493874123456")]
        [TestCase("387-4123456", "5493874123456")]
        [TestCase("0387-4123456", "5493874123456")]
        [TestCase("0387 15 4123456", "5493874123456")]
        [TestCase("387154123456", "5493874123456")]
        [TestCase("+54 9 387 4123456", "5493874123456")]
        [TestCase("+54 387 4123456", "5493874123456")]
        [TestCase("543874123456", "5493874123456")]
        [TestCase("5493874123456", "5493874123456")]
        [TestCase("0054 9 387 4123456", "5493874123456")]
        [TestCase("93874123456", "5493874123456")]
        public void NormalizarTelefono_FormatosSalta(string entrada, string esperado)
        {
            Assert.AreEqual(esperado, WhatsAppHelper.NormalizarTelefono(entrada));
        }

        [TestCase("15-4123456", "5493874123456")]
        [TestCase("4123456", "5493874123456")]
        public void NormalizarTelefono_SinArea_UsaAreaDefault(string entrada, string esperado)
        {
            Assert.AreEqual(esperado, WhatsAppHelper.NormalizarTelefono(entrada));
        }

        [TestCase("011 15 23456789", "5491123456789")]
        [TestCase("0388 15 4123456", "5493884123456")]
        [TestCase("03868 15 412345", "5493868412345")]
        public void NormalizarTelefono_OtrasAreas(string entrada, string esperado)
        {
            Assert.AreEqual(esperado, WhatsAppHelper.NormalizarTelefono(entrada));
        }

        [Test]
        public void NormalizarTelefono_OtroPais_SeRespeta()
        {
            Assert.AreEqual("56912345678", WhatsAppHelper.NormalizarTelefono("+56 9 1234 5678"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("387")]
        [TestCase("0")]
        [TestCase("sin telefono")]
        [TestCase("12345678901234567")]
        public void NormalizarTelefono_Invalido_DevuelveNull(string entrada)
        {
            Assert.IsNull(WhatsAppHelper.NormalizarTelefono(entrada));
        }

        [Test]
        public void ElegirTelefono_UsaElPrimeroValido()
        {
            Assert.AreEqual("5493874123456", WhatsAppHelper.ElegirTelefono("", "0387-4123456"));
            Assert.AreEqual("5493875000000", WhatsAppHelper.ElegirTelefono("3875000000", "0387-4123456"));
            Assert.IsNull(WhatsAppHelper.ElegirTelefono(null, "387"));
        }

        [Test]
        public void ConstruirEnlace_CodificaEspaciosComoPorcientoVeinte()
        {
            var enlace = WhatsAppHelper.ConstruirEnlace("3874123456", "¡Hola Ana! Feliz día");
            Assert.AreEqual("https://wa.me/5493874123456?text=%C2%A1Hola%20Ana%21%20Feliz%20d%C3%ADa", enlace);
        }

        [Test]
        public void ConstruirEnlace_TelefonoInvalido_DevuelveNull()
        {
            Assert.IsNull(WhatsAppHelper.ConstruirEnlace("387", "hola"));
        }
    }
}
