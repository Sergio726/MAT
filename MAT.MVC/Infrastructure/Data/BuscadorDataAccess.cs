using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MAT.MVC.Models.Home;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Buscador global, etapa 2 (bloque G del plan del panel principal). Solo reutiliza búsquedas que ya existen:
    /// clientes con <c>usp_MAT_PersonaCliente_Buscar</c> (listado de Clientes) y viajes con
    /// <c>usp_MAT_Reportes_BuscarViajes</c> (reportes). Las facturas se encuentran por su titular: el número de
    /// factura casi nunca se carga (51 de ~2.000 en el último año, varios con texto libre), así que no sirve de clave.
    /// </summary>
    public static class BuscadorDataAccess
    {
        public const int MaximoPorGrupo = 5;

        public static IList<BuscadorGrupoDto> Buscar(string texto)
        {
            var grupos = new List<BuscadorGrupoDto>();
            texto = (texto ?? string.Empty).Trim();
            if (texto.Length < 2) return grupos;

            // Cada grupo por separado: si uno falla, el resto se muestra igual
            try
            {
                var pagina = ClienteListadoDataAccess.Buscar(texto, null, null, "apellido", 1, 25);
                var clientes = pagina.Items.Take(MaximoPorGrupo).ToList();
                if (clientes.Count > 0)
                {
                    var grupo = new BuscadorGrupoDto { Tipo = "cliente", Titulo = "Clientes" };
                    foreach (var c in clientes)
                    {
                        grupo.Items.Add(new BuscadorItemDto
                        {
                            Titulo = NombreCliente(c.Apellido, c.Nombre),
                            Descripcion = string.Join(" · ", new[] { Documento(c.NroDocumento), c.LocalidadNombre }.Where(s => !string.IsNullOrWhiteSpace(s))),
                            Url = "/PersonaCliente/Details/" + c.PersonaId,
                            Icono = "bi-person"
                        });
                    }
                    if (pagina.Total > clientes.Count)
                    {
                        grupo.Items.Add(new BuscadorItemDto
                        {
                            Titulo = "Ver los " + pagina.Total.ToString("N0") + " clientes",
                            Descripcion = "En el listado de Clientes",
                            Url = "/PersonaCliente/Index?q=" + Uri.EscapeDataString(texto),
                            Icono = "bi-list-ul",
                            VerTodos = true
                        });
                    }
                    grupos.Add(grupo);

                    // Facturas: de los clientes encontrados, los que son titulares, a la pestaña Facturas de la ficha
                    var titulares = clientes.Where(c => c.EsTitularFacturas).ToList();
                    if (titulares.Count > 0)
                    {
                        var facturas = new BuscadorGrupoDto { Tipo = "factura", Titulo = "Facturas" };
                        foreach (var c in titulares)
                        {
                            facturas.Items.Add(new BuscadorItemDto
                            {
                                Titulo = "Facturas de " + NombreCliente(c.Apellido, c.Nombre),
                                Descripcion = "Ficha del cliente, pestaña Facturas",
                                Url = "/PersonaCliente/Details/" + c.PersonaId + "#facturas",
                                Icono = "bi-receipt"
                            });
                        }
                        grupos.Add(facturas);
                    }
                }
            }
            catch (Exception ex)
            {
                MATLogger.Log("Buscador global, clientes: " + ex.Message, 1);
            }

            try
            {
                var viajes = BuscarViajes(texto).Take(MaximoPorGrupo).ToList();
                if (viajes.Count > 0)
                {
                    var grupo = new BuscadorGrupoDto { Tipo = "viaje", Titulo = "Viajes" };
                    foreach (var v in viajes)
                    {
                        grupo.Items.Add(new BuscadorItemDto
                        {
                            Titulo = v.Descripcion,
                            Descripcion = v.FechaSalida.HasValue ? "Sale el " + v.FechaSalida.Value.ToString("dd/MM/yyyy") : "Sin fecha de salida",
                            Url = "/Reserva/Index?viajeid=" + v.ViajeId,
                            Icono = "bi-bus-front"
                        });
                    }
                    grupos.Add(grupo);
                }
            }
            catch (Exception ex)
            {
                MATLogger.Log("Buscador global, viajes: " + ex.Message, 1);
            }

            return grupos;
        }

        /// <summary>Viajes por descripción (sin acentos), salida más reciente primero (máximo 30, lo corta el SP).</summary>
        public static IList<BuscadorViajeDto> BuscarViajes(string texto)
        {
            var lista = new List<BuscadorViajeDto>();
            var parametros = new[]
            {
                DBHelper.MakeParam("@q", SqlDbType.NVarChar, 200, texto),
                DBHelper.MakeParam("@RecentOnly", SqlDbType.Bit, 0, false)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reportes_BuscarViajes", parametros))
            {
                var ordId = reader.GetOrdinal("ViajeID");
                var ordDesc = reader.GetOrdinal("Descripcion");
                var ordFecha = reader.GetOrdinal("FechaSalida");
                while (reader.Read())
                {
                    lista.Add(new BuscadorViajeDto
                    {
                        ViajeId = reader.GetGuid(ordId),
                        Descripcion = reader.IsDBNull(ordDesc) ? string.Empty : reader.GetString(ordDesc).Trim(),
                        FechaSalida = reader.IsDBNull(ordFecha) ? (DateTime?)null : reader.GetDateTime(ordFecha)
                    });
                }
            }
            return lista;
        }

        private static string NombreCliente(string apellido, string nombre)
        {
            apellido = (apellido ?? string.Empty).Trim();
            nombre = (nombre ?? string.Empty).Trim();
            if (apellido.Length == 0) return nombre;
            return nombre.Length == 0 ? apellido : apellido + ", " + nombre;
        }

        private static string Documento(string nro)
        {
            return string.IsNullOrWhiteSpace(nro) ? null : "DNI " + nro.Trim();
        }
    }
}
