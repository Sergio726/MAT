using System.Collections.Specialized;
using NUnit.Framework;

namespace MAT.MVC.Tests.Helpers
{
    [TestFixture]
    public sealed class HelperTests
    {
        private sealed class SampleEntity
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }

        [Test]
        public void ToSelectItem_EmptySelectValue_ReturnsSinDefinir()
        {
            Assert.AreEqual("Sin Definir", MAT.Utilities.Helper.ToSelectItem("Transporte", null));
            Assert.AreEqual("Sin Definir", MAT.Utilities.Helper.ToSelectItem("Transporte", string.Empty));
        }

        [Test]
        public void ToSelectItem_InvalidGuid_ReturnsSinDefinir()
        {
            Assert.AreEqual("Sin Definir", MAT.Utilities.Helper.ToSelectItem("Transporte", "not-a-guid"));
            Assert.AreEqual("Sin Definir", MAT.Utilities.Helper.ToSelectItem("Paquete", "12345"));
            Assert.AreEqual("Sin Definir", MAT.Utilities.Helper.ToSelectItem("Hotel", "xyz"));
        }

        [Test]
        public void ToSelectItem_InvalidLocalidadId_ReturnsSinDefinir()
        {
            Assert.AreEqual("Sin Definir", MAT.Utilities.Helper.ToSelectItem("Localidad", "abc"));
            Assert.AreEqual("Sin Definir", MAT.Utilities.Helper.ToSelectItem("Provincia", "not-int"));
        }

        [Test]
        public void IsNumeric_ValidAndInvalidInputs()
        {
            Assert.IsTrue(MAT.Utilities.Helper.IsNumeric("123"));
            Assert.IsTrue(MAT.Utilities.Helper.IsNumeric("12.3"));
            Assert.IsFalse(MAT.Utilities.Helper.IsNumeric("abc"));
            Assert.IsFalse(MAT.Utilities.Helper.IsNumeric(null));
        }

        [Test]
        public void FillEntity_NameValueCollection_AssignsProperties()
        {
            var entity = new SampleEntity();
            var datos = new NameValueCollection
            {
                { "Name", "Test" },
                { "Count", "42" }
            };

            MAT.Utilities.Helper.FillEntity(ref entity, datos);

            Assert.AreEqual("Test", entity.Name);
            Assert.AreEqual(42, entity.Count);
        }
    }
}
