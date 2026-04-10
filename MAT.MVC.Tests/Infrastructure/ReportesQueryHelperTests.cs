using System;
using MAT.MVC.Infrastructure;
using NUnit.Framework;

namespace MAT.MVC.Tests.Infrastructure
{
    [TestFixture]
    public sealed class ReportesQueryHelperTests
    {
        private static readonly Guid SampleViajeId = Guid.Parse("a1b2c3d4-e5f6-4789-a012-3456789abcde");

        [Test]
        public void Parse_DateRangeValid_YearMonthDayFormat_ShouldExecuteWithNormalizedDates()
        {
            var r = ReportesQueryHelper.Parse("2023-01-01", "2023-12-31", null);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ShouldExecute);
            Assert.IsNull(r.ViajeId);
            Assert.AreEqual("01-01-2023", r.FromDdMmYyyy);
            Assert.AreEqual("31-12-2023", r.ToDdMmYyyy);
            Assert.AreEqual(new DateTime(2023, 1, 1), r.FromDate);
            Assert.AreEqual(new DateTime(2023, 12, 31), r.ToDate);
        }

        [Test]
        public void Parse_DateRangeValid_DdMmYyyyFormat_ShouldExecute()
        {
            var r = ReportesQueryHelper.Parse("01-01-2023", "31-12-2023", null);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ShouldExecute);
            Assert.AreEqual("01-01-2023", r.FromDdMmYyyy);
            Assert.AreEqual("31-12-2023", r.ToDdMmYyyy);
        }

        [Test]
        public void Parse_RangeExceeds365InclusiveCalendarDays_IsInvalid()
        {
            var r = ReportesQueryHelper.Parse("01-01-2023", "01-01-2024", null);

            Assert.IsFalse(r.IsValid);
            Assert.IsFalse(r.ShouldExecute);
            StringAssert.Contains("365", r.ValidationMessage);
        }

        [Test]
        public void Parse_ViajeAndDateRange_IsInvalid()
        {
            var r = ReportesQueryHelper.Parse("01-01-2023", "31-01-2023", SampleViajeId.ToString());

            Assert.IsFalse(r.IsValid);
            Assert.IsFalse(r.ShouldExecute);
            StringAssert.Contains("viaje", r.ValidationMessage.ToLowerInvariant());
        }

        [Test]
        public void Parse_FromWithoutTo_IsInvalid()
        {
            var r = ReportesQueryHelper.Parse("01-01-2023", null, null);

            Assert.IsFalse(r.IsValid);
            Assert.IsFalse(r.ShouldExecute);
            StringAssert.Contains("inicio", r.ValidationMessage.ToLowerInvariant());
        }

        [Test]
        public void Parse_TipoVentaIdInvalid_IsInvalid()
        {
            var r = ReportesQueryHelper.Parse("01-01-2023", "31-01-2023", null, tipoVentaId: "3");

            Assert.IsFalse(r.IsValid);
            Assert.IsFalse(r.ShouldExecute);
        }

        [Test]
        public void Parse_TipoVentaIdNotInteger_IsInvalid()
        {
            var r = ReportesQueryHelper.Parse("01-01-2023", "31-01-2023", null, tipoVentaId: "x");

            Assert.IsFalse(r.IsValid);
            Assert.IsFalse(r.ShouldExecute);
        }

        [Test]
        public void Parse_TipoVentaIdOne_IsValidOnDateRange()
        {
            var r = ReportesQueryHelper.Parse("01-01-2023", "31-01-2023", null, tipoVentaId: "1");

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ShouldExecute);
            Assert.AreEqual(1, r.TipoVentaId);
        }

        [Test]
        public void Parse_OnlyViaje_ShouldExecuteForTrip()
        {
            var r = ReportesQueryHelper.Parse(null, null, SampleViajeId.ToString());

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ShouldExecute);
            Assert.AreEqual(SampleViajeId, r.ViajeId);
            Assert.IsNull(r.FromDdMmYyyy);
        }

        [Test]
        public void Parse_NoCriteria_IsEmptyValid()
        {
            var r = ReportesQueryHelper.Parse(null, null, null);

            Assert.IsTrue(r.IsValid);
            Assert.IsFalse(r.ShouldExecute);
            Assert.IsNull(r.ValidationMessage);
        }

        [Test]
        public void TryParseDateParameter_TrimsAndAcceptsBothFormats()
        {
            var a = ReportesQueryHelper.TryParseDateParameter("  2023-06-15  ");
            var b = ReportesQueryHelper.TryParseDateParameter("15-06-2023");

            Assert.IsNotNull(a);
            Assert.IsNotNull(b);
            Assert.AreEqual(new DateTime(2023, 6, 15), a.Value.Date);
            Assert.AreEqual(new DateTime(2023, 6, 15), b.Value.Date);
        }
    }
}
