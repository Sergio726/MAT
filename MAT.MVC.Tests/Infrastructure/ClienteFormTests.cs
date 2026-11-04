using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using MAT.MVC.Infrastructure.Clientes;
using MAT.MVC.Models.Clientes;
using NUnit.Framework;

namespace MAT.MVC.Tests.Infrastructure
{
    [TestFixture]
    public sealed class ClienteFormTests
    {
        private static ClienteFormModel Completo()
        {
            var m = ClienteFormDefaults.Nuevo();
            m.Apellido = "Mamani";
            m.Nombre = "Blanca Elisa";
            m.NroDocumento = "11470759";
            m.FechaNacimiento = "01/05/1955";
            m.Celular = "387 4140863";
            m.Domicilio = "Calle 1";
            m.LocalidadId = 5445;
            return m;
        }

        /// <summary>Valida con los mismos atributos que usa MVC al enlazar el formulario. Todos los mensajes por campo.</summary>
        private static List<ValidationResult> Validar(ClienteFormModel m)
        {
            var resultados = new List<ValidationResult>();
            Validator.TryValidateObject(m, new ValidationContext(m), resultados, true);
            return resultados;
        }

        /// <summary>Primer mensaje de cada campo (el que muestra la pantalla).</summary>
        private static Dictionary<string, string> Errores(ClienteFormModel m)
        {
            return Validar(m)
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "")
                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
        }

        [Test]
        public void FormularioCompleto_SinErrores()
        {
            CollectionAssert.IsEmpty(Errores(Completo()));
        }

        [Test]
        public void Vacio_PideLosObligatoriosConEtiquetaLegible()
        {
            var errores = Errores(new ClienteFormModel());
            Assert.AreEqual("Completá N° de documento.", errores["NroDocumento"]);
            Assert.AreEqual("Completá Fecha de nacimiento.", errores["FechaNacimiento"]);
            Assert.AreEqual("Completá País de residencia.", errores["PaisResidencia"]);
            Assert.AreEqual("Elegí la localidad.", errores["LocalidadId"]);
            Assert.AreEqual("Elegí el tipo de cliente.", errores["TipoId"]);
            Assert.That(errores.ContainsKey("Apellido") && errores.ContainsKey("Nombre") && errores.ContainsKey("Domicilio"));
        }

        [Test]
        public void SoloCodigoDeArea_MarcaElCampoYPideUnTelefono()
        {
            var m = Completo();
            m.Celular = "387";
            m.Telefono = null;
            var mensajes = Validar(m).Where(r => r.MemberNames.Contains("Celular")).Select(r => r.ErrorMessage).ToList();
            CollectionAssert.AreEquivalent(new[]
            {
                "El número está incompleto: cargalo con código de área (al menos 7 números).",
                "Cargá al menos un teléfono o celular."
            }, mensajes);
        }

        [Test]
        public void SoloTelefonoFijo_EsSuficiente()
        {
            var m = Completo();
            m.Celular = null;
            m.Telefono = "0387 4220000";
            CollectionAssert.IsEmpty(Errores(m));
        }

        [Test]
        public void FechaFutura_MensajePropio()
        {
            var m = Completo();
            m.FechaNacimiento = "31/12/2099";
            Assert.AreEqual("La fecha de nacimiento no puede ser futura.", Errores(m)["FechaNacimiento"]);
        }

        [Test]
        public void DniCorto_MensajePropio()
        {
            var m = Completo();
            m.NroDocumento = "12345";
            Assert.AreEqual("El DNI debe tener 7 u 8 números.", Errores(m)["NroDocumento"]);
        }

        [Test]
        public void Pasaporte_AceptaLetras()
        {
            var m = Completo();
            m.TipoDocumento = 2 /* Pasaporte */;
            m.NroDocumento = "AB123456";
            CollectionAssert.IsEmpty(Errores(m));
        }

        [TestCase("5787839", "05.787.839")]
        [TestCase("05.787.839", "05.787.839")]
        [TestCase("32877563", "32.877.563")]
        [TestCase(" 32.877.563 ", "32.877.563")]
        public void NormalizarDni_FormatoDeLaBase(string entrada, string esperado)
        {
            Assert.AreEqual(esperado, ClienteFormMapper.NormalizarDocumento(1 /* DNI */, entrada));
        }

        [Test]
        public void Normalizar_LimpiaEspaciosYMayusculas()
        {
            var m = Completo();
            m.Apellido = "  mamani   choque ";
            m.Email = " Blanca@Mail.COM ";
            m.Observacion = "   ";
            ClienteFormMapper.Normalizar(m);
            Assert.AreEqual("MAMANI CHOQUE", m.Apellido);
            Assert.AreEqual("blanca@mail.com", m.Email);
            Assert.IsNull(m.Observacion);
            Assert.AreEqual("11.470.759", m.NroDocumento);
        }

        [Test]
        public void Defaults_SinSexoPreseleccionado()
        {
            var m = ClienteFormDefaults.Nuevo();
            Assert.IsNull(m.Sexo);
            Assert.AreEqual("ARG", m.Nacionalidad);
            Assert.AreEqual(2 /* Minorista */, m.TipoId);
            Assert.IsTrue(m.CuentaCorrienteHabilitada);
            Assert.IsTrue(m.EsAlta);
        }

        [Test]
        public void Opciones_TextosLegiblesYOcupacionVieja()
        {
            var m = Completo();
            m.Ocupacion = "T INDEP";
            var opciones = ClienteFormOpciones.Crear(m, null, new[] { "JUBILADO", "jubilado", "EMPLEADA" });

            Assert.That(opciones.CondicionesIva.Any(o => o.Text == "Consumidor Final"));
            Assert.AreEqual("Seleccioná…", opciones.Sexos[0].Text);
            Assert.AreEqual("", opciones.Sexos[0].Value);
            CollectionAssert.AreEqual(new[] { "Sin especificar", "T INDEP", "EMPLEADA", "JUBILADO" }, opciones.Ocupaciones.Select(o => o.Text));
        }
    }
}
