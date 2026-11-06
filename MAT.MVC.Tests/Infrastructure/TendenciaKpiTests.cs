using System;
using MAT.MVC.Models.Home;
using NUnit.Framework;

namespace MAT.MVC.Tests.Infrastructure
{
    [TestFixture]
    public sealed class TendenciaKpiTests
    {
        [TestCase(112, 100, "sube", "+12 %")]
        [TestCase(92, 100, "baja", "−8 %")]
        [TestCase(100, 100, "igual", "Igual")]
        [TestCase(1001, 1000, "igual", "Igual")]   // +0,1 % redondea a 0
        [TestCase(3, 0, "sube", "+3")]             // sin base: diferencia absoluta
        [TestCase(0, 0, "igual", "Igual")]
        [TestCase(0, 5, "baja", "−100 %")]
        public void Calcular(double actual, double anterior, string direccion, string texto)
        {
            var t = TendenciaKpi.Calcular(actual, anterior, "1–6 oct");
            Assert.AreEqual(direccion, t.Direccion);
            Assert.AreEqual(texto, t.Texto);
            StringAssert.Contains("1–6 oct", t.Lectura);
        }

        [TestCase("2026-11-06", "2026-10-01", "2026-10-06", "1–6 oct")]
        [TestCase("2026-03-31", "2026-02-01", "2026-02-28", "1–28 feb")]   // febrero más corto
        [TestCase("2026-01-15", "2025-12-01", "2025-12-15", "1–15 dic")]   // cambio de año
        [TestCase("2026-11-01", "2026-10-01", "2026-10-01", "1 oct")]           // un solo día
        public void PeriodoAnterior_MismoTramoDelMesPasado(string hoy, string desde, string hasta, string etiqueta)
        {
            DateTime d, h;
            TendenciaKpi.PeriodoAnterior(DateTime.Parse(hoy), out d, out h);
            Assert.AreEqual(DateTime.Parse(desde), d);
            Assert.AreEqual(DateTime.Parse(hasta), h);
            Assert.AreEqual(etiqueta, TendenciaKpi.EtiquetaPeriodo(d, h));
        }
    }
}
