using System;
using MAT.Utilities;
using NUnit.Framework;

namespace MAT.Integration.Tests
{
    [TestFixture]
    [Category("Integration")]
    public sealed class LookupDataAccessIntegrationTests : IntegrationTestBase
    {
        [Test]
        public void GetProveedorSelectItems_ReturnsRowsWithGuidValue()
        {
            var items = LookupDataAccess.GetProveedorSelectItems();

            Assert.NotNull(items);
            foreach (var item in items)
            {
                Guid parsed;
                Assert.IsTrue(Guid.TryParse(item.Value, out parsed), "Value no es Guid: " + item.Value);
                Assert.IsFalse(string.IsNullOrWhiteSpace(item.Text));
            }
        }

        [Test]
        public void GetViajeSelectItems_DoesNotThrow()
        {
            var items = LookupDataAccess.GetViajeSelectItems();
            Assert.NotNull(items);
        }

        [Test]
        public void GetHotelSelectItems_DoesNotThrow()
        {
            var items = LookupDataAccess.GetHotelSelectItems();
            Assert.NotNull(items);
        }
    }
}
