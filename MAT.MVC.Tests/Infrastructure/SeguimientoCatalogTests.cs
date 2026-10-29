using MAT.MVC.Infrastructure;
using MAT.MVC.Models.Seguimiento;
using NUnit.Framework;

namespace MAT.MVC.Tests.Infrastructure
{
    [TestFixture]
    public sealed class SeguimientoCatalogTests
    {
        [Test]
        public void ArmarSaludo_ConViajes_IncluyeAgradecimientoYFirma()
        {
            var cliente = new SeguimientoClienteDto { Nombre = "RITA NERINA", ViajesTotales = 6 };
            Assert.AreEqual(
                "¡Feliz cumpleaños, Rita! 🎉 De parte de todo el equipo de Marco Antonio Tours te deseamos un gran día. Gracias por viajar con nosotros. — Ivana Oña",
                SeguimientoCatalog.ArmarSaludoCumpleanios(cliente, "Ivana Oña"));
        }

        [Test]
        public void ArmarSaludo_SinViajesNiVendedor_OmiteAgradecimientoYFirma()
        {
            var cliente = new SeguimientoClienteDto { Nombre = "ana", ViajesTotales = 0 };
            Assert.AreEqual(
                "¡Feliz cumpleaños, Ana! 🎉 De parte de todo el equipo de Marco Antonio Tours te deseamos un gran día.",
                SeguimientoCatalog.ArmarSaludoCumpleanios(cliente, null));
        }

        [Test]
        public void ArmarSaludo_SinNombre_SaludoGenerico()
        {
            var saludo = SeguimientoCatalog.ArmarSaludoCumpleanios(new SeguimientoClienteDto(), null);
            StringAssert.StartsWith("¡Feliz cumpleaños! 🎉", saludo);
        }

        [Test]
        public void NombreParaFirma_UsaPrimerNombreYApellidoEnTitulo()
        {
            Assert.AreEqual("Amira Gallardo", SeguimientoCatalog.NombreParaFirma("Amira Stella Maris", "GALLARDO"));
            Assert.IsNull(SeguimientoCatalog.NombreParaFirma(null, " "));
        }

        [Test]
        public void Completar_ArmaEtiquetasYWhatsAppDesdeTelefonoSiNoHayCelular()
        {
            var cliente = new SeguimientoClienteDto { Categoria = "EnRiesgo", Celular = "387", Telefono = "0387-4123456", UltimoContactoResultado = 1 };
            SeguimientoCatalog.Completar(cliente, "Hola");

            Assert.AreEqual("En riesgo", cliente.CategoriaNombre);
            Assert.AreEqual("Saludo de cumpleaños", cliente.UltimoContactoResultadoNombre);
            Assert.IsTrue(cliente.TieneWhatsApp);
            Assert.AreEqual("https://wa.me/5493874123456?text=Hola", cliente.WhatsAppUrl);
        }

        [Test]
        public void Completar_SinTelefonoValido_NoArmaEnlace()
        {
            var cliente = new SeguimientoClienteDto { Categoria = "VIP", Celular = "387" };
            SeguimientoCatalog.Completar(cliente);

            Assert.IsFalse(cliente.TieneWhatsApp);
            Assert.IsNull(cliente.WhatsAppUrl);
        }

        [Test]
        public void Categorias_SonLasAcordadas()
        {
            CollectionAssert.AreEqual(
                new[] { "VIP", "EnRiesgo", "Frecuente", "Nuevo", "Ocasional", "Dormido", "SinViajes" },
                System.Linq.Enumerable.Select(SeguimientoCatalog.GetCategorias(), c => c.Codigo));
            Assert.IsFalse(SeguimientoCatalog.EsCategoriaValida("Otra"));
        }
    }
}
