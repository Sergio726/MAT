using MAT.MVC.Models.Seguimiento;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Seguimiento de clientes para vendedores: categorías, cumpleaños, ficha y registro de contactos
    /// (SPs <c>usp_MAT_Seguimiento_*</c> y <c>usp_MAT_ClienteContacto_Insert</c>).
    /// </summary>
    public static class SeguimientoDataAccess
    {
        public static Dictionary<string, int> GetResumenCategorias()
        {
            var resumen = new Dictionary<string, int>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Seguimiento_ResumenCategorias", null))
            {
                while (reader.Read())
                {
                    resumen[reader.GetString("Categoria")] = reader.GetInt("Cantidad");
                }
            }
            return resumen;
        }

        public static List<SeguimientoClienteDto> GetClientes(string categoria, string texto)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@Categoria", SqlDbType.VarChar, 20, (object)categoria ?? DBNull.Value),
                DBHelper.MakeParam("@Texto", SqlDbType.VarChar, 100, (object)texto ?? DBNull.Value)
            };
            var list = new List<SeguimientoClienteDto>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Seguimiento_Clientes", parameters))
            {
                while (reader.Read())
                {
                    list.Add(MapCliente(reader));
                }
            }
            return list;
        }

        public static List<SeguimientoClienteDto> GetCumpleanios(int dias)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@Dias", SqlDbType.Int, 0, dias)
            };
            var list = new List<SeguimientoClienteDto>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Seguimiento_Cumpleanios", parameters))
            {
                while (reader.Read())
                {
                    var cliente = MapCliente(reader);
                    cliente.ProximoCumple = reader.GetNullableDateTime("ProximoCumple");
                    cliente.DiasFaltan = reader.GetNullableInt("DiasFaltan");
                    cliente.EdadQueCumple = reader.GetNullableInt("EdadQueCumple");
                    cliente.UltimoSaludoFecha = reader.GetNullableDateTime("UltimoSaludoFecha");
                    cliente.UltimoSaludoPor = reader.GetString("UltimoSaludoPor");
                    list.Add(cliente);
                }
            }
            return list;
        }

        public static SeguimientoFichaDto GetFicha(Guid clienteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId)
            };
            var ficha = new SeguimientoFichaDto
            {
                Viajes = new List<SeguimientoViajeDto>(),
                Contactos = new List<SeguimientoContactoDto>()
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Seguimiento_Ficha", parameters))
            {
                if (reader.Read()) ficha.Cliente = MapCliente(reader);

                reader.NextResult();
                while (reader.Read())
                {
                    ficha.Viajes.Add(new SeguimientoViajeDto
                    {
                        FechaSalida = reader.GetNullableDateTime("FechaSalida"),
                        FechaRegreso = reader.GetNullableDateTime("FechaRegreso"),
                        Viaje = reader.GetString("Viaje"),
                        EstadoPasaje = reader.GetString("EstadoPasaje")
                    });
                }

                reader.NextResult();
                while (reader.Read())
                {
                    ficha.Contactos.Add(new SeguimientoContactoDto
                    {
                        Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                        Resultado = reader.GetInt("Resultado"),
                        Observacion = reader.GetString("Observacion"),
                        Quien = reader.GetString("Quien")
                    });
                }
            }
            return ficha.Cliente == null ? null : ficha;
        }

        public static Guid InsertContacto(Guid clienteId, Guid? vendedorId, string usuario, int resultado, string observacion)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId),
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, (object)vendedorId ?? DBNull.Value),
                DBHelper.MakeParam("@Usuario", SqlDbType.VarChar, 100, (object)usuario ?? DBNull.Value),
                DBHelper.MakeParam("@Resultado", SqlDbType.Int, 0, resultado),
                DBHelper.MakeParam("@Observacion", SqlDbType.VarChar, 500, (object)observacion ?? DBNull.Value)
            };
            return (Guid)DBHelper.ExecuteScalar("dbo.usp_MAT_ClienteContacto_Insert", parameters);
        }

        private static SeguimientoClienteDto MapCliente(SqlDataReader reader)
        {
            return new SeguimientoClienteDto
            {
                ClienteId = reader.GetGuid("ClienteID"),
                Apellido = reader.GetString("Apellido")?.Trim(),
                Nombre = reader.GetString("Nombre")?.Trim(),
                NroDocumento = reader.GetString("NroDocumento"),
                Celular = reader.GetString("Celular"),
                Telefono = reader.GetString("Telefono"),
                Email = reader.GetString("Email"),
                FechaNacimiento = reader.GetNullableDateTime("FechaNacimiento"),
                FechaNacimientoDudosa = reader.GetBool("FechaNacimientoDudosa"),
                ViajesTotales = reader.GetInt("ViajesTotales"),
                Viajes12Meses = reader.GetInt("Viajes12Meses"),
                PrimerViaje = reader.GetNullableDateTime("PrimerViaje"),
                UltimoViaje = reader.GetNullableDateTime("UltimoViaje"),
                ProximoViaje = reader.GetNullableDateTime("ProximoViaje"),
                GastoArs = GetNullableDouble(reader, "GastoARS"),
                GastoUsd = GetNullableDouble(reader, "GastoUSD"),
                Categoria = reader.GetString("Categoria"),
                UltimoContactoFecha = reader.GetNullableDateTime("UltimoContactoFecha"),
                UltimoContactoResultado = reader.GetNullableInt("UltimoContactoResultado"),
                UltimoContactoPor = reader.GetString("UltimoContactoPor"),
                NoContactar = reader.GetBool("NoContactar")
            };
        }

        private static double? GetNullableDouble(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (double?)null : Convert.ToDouble(reader.GetValue(ordinal));
        }
    }
}
