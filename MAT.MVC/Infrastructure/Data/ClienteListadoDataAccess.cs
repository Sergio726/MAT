using System;
using System.Collections.Generic;
using System.Data;
using MAT.MVC.Models.Clientes;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Listado de clientes y "Altas por período", paginados en la base
    /// (<c>usp_MAT_PersonaCliente_Buscar</c>, <c>usp_MAT_Reportes_ClientesNuevosPagina</c>).
    /// </summary>
    public static class ClienteListadoDataAccess
    {
        public static PaginaDto<ClienteListadoItemDto> Buscar(string texto, int? tipoId, string categoria, string orden, int pagina, int tamanio,
            bool incluirInactivos = false)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@IncluirInactivos", SqlDbType.Bit, 0, incluirInactivos),
                DBHelper.MakeParam("@Texto", SqlDbType.NVarChar, 200, (object)texto ?? DBNull.Value),
                DBHelper.MakeParam("@TipoID", SqlDbType.Int, 0, (object)tipoId ?? DBNull.Value),
                DBHelper.MakeParam("@Categoria", SqlDbType.VarChar, 20, (object)categoria ?? DBNull.Value),
                DBHelper.MakeParam("@Orden", SqlDbType.VarChar, 20, orden),
                DBHelper.MakeParam("@Pagina", SqlDbType.Int, 0, pagina),
                DBHelper.MakeParam("@Tamanio", SqlDbType.Int, 0, tamanio)
            };
            var resultado = new PaginaDto<ClienteListadoItemDto> { Pagina = pagina, Tamanio = tamanio, Items = new List<ClienteListadoItemDto>() };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_PersonaCliente_Buscar", parameters))
            {
                if (reader.Read()) resultado.Total = reader.GetInt("Total");
                reader.NextResult();
                while (reader.Read())
                {
                    resultado.Items.Add(new ClienteListadoItemDto
                    {
                        PersonaId = reader.GetGuid("PersonaID"),
                        Apellido = reader.GetString("Apellido")?.Trim(),
                        Nombre = reader.GetString("Nombre")?.Trim(),
                        NroDocumento = reader.GetString("NroDocumento")?.Trim(),
                        Celular = reader.GetString("Celular")?.Trim(),
                        Telefono = reader.GetString("Telefono")?.Trim(),
                        Email = reader.GetString("Email")?.Trim(),
                        LocalidadNombre = reader.GetString("LocalidadNombre")?.Trim(),
                        TipoId = reader.GetInt("TipoID"),
                        FechaAlta = reader.GetNullableDateTime("FechaAlta"),
                        Categoria = reader.GetString("Categoria"),
                        EsMenor = reader.GetBool("EsMenor"),
                        EsTitularFacturas = reader.GetBool("EsTitularFacturas"),
                        Activo = reader.GetBool("Activo")
                    });
                }
            }
            return resultado;
        }

        /// <param name="tamanio">0 = todas las filas (exportación a Excel).</param>
        public static PaginaDto<ClienteAltaDto> ClientesNuevos(DateTime desde, DateTime hasta, int pagina, int tamanio)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@FechaInicio", SqlDbType.Date, 0, desde.Date),
                DBHelper.MakeParam("@FechaFin", SqlDbType.Date, 0, hasta.Date),
                DBHelper.MakeParam("@Pagina", SqlDbType.Int, 0, pagina),
                DBHelper.MakeParam("@Tamanio", SqlDbType.Int, 0, tamanio)
            };
            var resultado = new PaginaDto<ClienteAltaDto> { Pagina = pagina, Tamanio = tamanio, Items = new List<ClienteAltaDto>() };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reportes_ClientesNuevosPagina", parameters))
            {
                if (reader.Read()) resultado.Total = reader.GetInt("Total");
                reader.NextResult();
                while (reader.Read())
                {
                    resultado.Items.Add(new ClienteAltaDto
                    {
                        PersonaId = reader.GetGuid("PersonaID"),
                        ClienteNombre = reader.GetString("ClienteNombre"),
                        NroDocumento = reader.GetString("NroDocumento"),
                        Telefono = reader.GetString("Telefono"),
                        Email = reader.GetString("Email"),
                        FechaAlta = reader.GetNullableDateTime("FechaAlta"),
                        PrimeraFactura = reader.GetNullableDateTime("PrimeraFecha"),
                        VendedorNombre = reader.GetString("VendedorNombre")
                    });
                }
            }
            return resultado;
        }
    }
}
