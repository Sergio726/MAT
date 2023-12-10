#region Using directives

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MAT.Entities;
using MAT.Data;

#endregion

namespace MAT.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="PersonaClienteProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class PersonaClienteProviderBaseCore : EntityViewProviderBase<PersonaCliente>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;PersonaCliente&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;PersonaCliente&gt;"/></returns>
		protected static VList&lt;PersonaCliente&gt; Fill(DataSet dataSet, VList<PersonaCliente> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<PersonaCliente>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;PersonaCliente&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<PersonaCliente>"/></returns>
		protected static VList&lt;PersonaCliente&gt; Fill(DataTable dataTable, VList<PersonaCliente> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					PersonaCliente c = new PersonaCliente();
					c.PersonaId = (Convert.IsDBNull(row["PersonaID"]))?Guid.Empty:(System.Guid)row["PersonaID"];
					c.Apellido = (Convert.IsDBNull(row["Apellido"]))?string.Empty:(System.String)row["Apellido"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
					c.NroDocumento = (Convert.IsDBNull(row["NroDocumento"]))?string.Empty:(System.String)row["NroDocumento"];
					c.Telefono = (Convert.IsDBNull(row["Telefono"]))?string.Empty:(System.String)row["Telefono"];
					c.Email = (Convert.IsDBNull(row["Email"]))?string.Empty:(System.String)row["Email"];
					c.FechaNacimiento = (Convert.IsDBNull(row["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)row["FechaNacimiento"];
					c.Domicilio = (Convert.IsDBNull(row["Domicilio"]))?string.Empty:(System.String)row["Domicilio"];
					c.Sexo = (Convert.IsDBNull(row["Sexo"]))?(int)0:(System.Int32?)row["Sexo"];
					c.LocalidadId = (Convert.IsDBNull(row["LocalidadID"]))?(int)0:(System.Int32?)row["LocalidadID"];
					c.ClienteId = (Convert.IsDBNull(row["ClienteID"]))?Guid.Empty:(System.Guid)row["ClienteID"];
					c.RazonSocial = (Convert.IsDBNull(row["RazonSocial"]))?string.Empty:(System.String)row["RazonSocial"];
					c.Cuit = (Convert.IsDBNull(row["Cuit"]))?string.Empty:(System.String)row["Cuit"];
					c.Moneda = (Convert.IsDBNull(row["Moneda"]))?string.Empty:(System.String)row["Moneda"];
					c.Empresa = (Convert.IsDBNull(row["Empresa"]))?string.Empty:(System.String)row["Empresa"];
					c.Ocupacion = (Convert.IsDBNull(row["Ocupacion"]))?string.Empty:(System.String)row["Ocupacion"];
					c.FormaPago = (Convert.IsDBNull(row["FormaPago"]))?(int)0:(System.Int32?)row["FormaPago"];
					c.CondicionIva = (Convert.IsDBNull(row["CondicionIva"]))?(int)0:(System.Int32?)row["CondicionIva"];
					c.VendedorId = (Convert.IsDBNull(row["VendedorID"]))?Guid.Empty:(System.Guid?)row["VendedorID"];
					c.Fax = (Convert.IsDBNull(row["Fax"]))?string.Empty:(System.String)row["Fax"];
					c.Web = (Convert.IsDBNull(row["Web"]))?string.Empty:(System.String)row["Web"];
					c.Idioma = (Convert.IsDBNull(row["Idioma"]))?string.Empty:(System.String)row["Idioma"];
					c.Promotor = (Convert.IsDBNull(row["Promotor"]))?string.Empty:(System.String)row["Promotor"];
					c.Observacion = (Convert.IsDBNull(row["Observacion"]))?string.Empty:(System.String)row["Observacion"];
					c.TipoId = (Convert.IsDBNull(row["TipoID"]))?(int)0:(System.Int32)row["TipoID"];
					c.TipoDocumento = (Convert.IsDBNull(row["TipoDocumento"]))?(int)0:(System.Int32?)row["TipoDocumento"];
					c.Celular = (Convert.IsDBNull(row["Celular"]))?string.Empty:(System.String)row["Celular"];
					c.Nacionalidad = (Convert.IsDBNull(row["Nacionalidad"]))?string.Empty:(System.String)row["Nacionalidad"];
					c.PaisResidencia = (Convert.IsDBNull(row["PaisResidencia"]))?string.Empty:(System.String)row["PaisResidencia"];
					c.Provincia = (Convert.IsDBNull(row["Provincia"]))?(int)0:(System.Int32?)row["Provincia"];
					c.AcceptChanges();
					rows.Add(c);
					pagelen -= 1;
				}
				recordnum += 1;
			}
			return rows;
		}
		*/	
						
		///<summary>
		/// Fill an <see cref="VList&lt;PersonaCliente&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;PersonaCliente&gt;"/></returns>
		protected VList<PersonaCliente> Fill(IDataReader reader, VList<PersonaCliente> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					PersonaCliente entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<PersonaCliente>("PersonaCliente",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new PersonaCliente();
					}
					
					entity.SuppressEntityEvents = true;

					entity.PersonaId = (System.Guid)reader[((int)PersonaClienteColumn.PersonaId)];
					//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
					entity.Apellido = (reader.IsDBNull(((int)PersonaClienteColumn.Apellido)))?null:(System.String)reader[((int)PersonaClienteColumn.Apellido)];
					//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
					entity.Nombre = (reader.IsDBNull(((int)PersonaClienteColumn.Nombre)))?null:(System.String)reader[((int)PersonaClienteColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
					entity.NroDocumento = (reader.IsDBNull(((int)PersonaClienteColumn.NroDocumento)))?null:(System.String)reader[((int)PersonaClienteColumn.NroDocumento)];
					//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
					entity.Telefono = (reader.IsDBNull(((int)PersonaClienteColumn.Telefono)))?null:(System.String)reader[((int)PersonaClienteColumn.Telefono)];
					//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
					entity.Email = (reader.IsDBNull(((int)PersonaClienteColumn.Email)))?null:(System.String)reader[((int)PersonaClienteColumn.Email)];
					//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
					entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaClienteColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PersonaClienteColumn.FechaNacimiento)];
					//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
					entity.Domicilio = (reader.IsDBNull(((int)PersonaClienteColumn.Domicilio)))?null:(System.String)reader[((int)PersonaClienteColumn.Domicilio)];
					//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
					entity.Sexo = (reader.IsDBNull(((int)PersonaClienteColumn.Sexo)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.Sexo)];
					//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
					entity.LocalidadId = (reader.IsDBNull(((int)PersonaClienteColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.LocalidadId)];
					//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
					entity.ClienteId = (System.Guid)reader[((int)PersonaClienteColumn.ClienteId)];
					//entity.ClienteId = (Convert.IsDBNull(reader["ClienteID"]))?Guid.Empty:(System.Guid)reader["ClienteID"];
					entity.RazonSocial = (reader.IsDBNull(((int)PersonaClienteColumn.RazonSocial)))?null:(System.String)reader[((int)PersonaClienteColumn.RazonSocial)];
					//entity.RazonSocial = (Convert.IsDBNull(reader["RazonSocial"]))?string.Empty:(System.String)reader["RazonSocial"];
					entity.Cuit = (reader.IsDBNull(((int)PersonaClienteColumn.Cuit)))?null:(System.String)reader[((int)PersonaClienteColumn.Cuit)];
					//entity.Cuit = (Convert.IsDBNull(reader["Cuit"]))?string.Empty:(System.String)reader["Cuit"];
					entity.Moneda = (reader.IsDBNull(((int)PersonaClienteColumn.Moneda)))?null:(System.String)reader[((int)PersonaClienteColumn.Moneda)];
					//entity.Moneda = (Convert.IsDBNull(reader["Moneda"]))?string.Empty:(System.String)reader["Moneda"];
					entity.Empresa = (reader.IsDBNull(((int)PersonaClienteColumn.Empresa)))?null:(System.String)reader[((int)PersonaClienteColumn.Empresa)];
					//entity.Empresa = (Convert.IsDBNull(reader["Empresa"]))?string.Empty:(System.String)reader["Empresa"];
					entity.Ocupacion = (reader.IsDBNull(((int)PersonaClienteColumn.Ocupacion)))?null:(System.String)reader[((int)PersonaClienteColumn.Ocupacion)];
					//entity.Ocupacion = (Convert.IsDBNull(reader["Ocupacion"]))?string.Empty:(System.String)reader["Ocupacion"];
					entity.FormaPago = (reader.IsDBNull(((int)PersonaClienteColumn.FormaPago)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.FormaPago)];
					//entity.FormaPago = (Convert.IsDBNull(reader["FormaPago"]))?(int)0:(System.Int32?)reader["FormaPago"];
					entity.CondicionIva = (reader.IsDBNull(((int)PersonaClienteColumn.CondicionIva)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.CondicionIva)];
					//entity.CondicionIva = (Convert.IsDBNull(reader["CondicionIva"]))?(int)0:(System.Int32?)reader["CondicionIva"];
					entity.VendedorId = (reader.IsDBNull(((int)PersonaClienteColumn.VendedorId)))?null:(System.Guid?)reader[((int)PersonaClienteColumn.VendedorId)];
					//entity.VendedorId = (Convert.IsDBNull(reader["VendedorID"]))?Guid.Empty:(System.Guid?)reader["VendedorID"];
					entity.Fax = (reader.IsDBNull(((int)PersonaClienteColumn.Fax)))?null:(System.String)reader[((int)PersonaClienteColumn.Fax)];
					//entity.Fax = (Convert.IsDBNull(reader["Fax"]))?string.Empty:(System.String)reader["Fax"];
					entity.Web = (reader.IsDBNull(((int)PersonaClienteColumn.Web)))?null:(System.String)reader[((int)PersonaClienteColumn.Web)];
					//entity.Web = (Convert.IsDBNull(reader["Web"]))?string.Empty:(System.String)reader["Web"];
					entity.Idioma = (reader.IsDBNull(((int)PersonaClienteColumn.Idioma)))?null:(System.String)reader[((int)PersonaClienteColumn.Idioma)];
					//entity.Idioma = (Convert.IsDBNull(reader["Idioma"]))?string.Empty:(System.String)reader["Idioma"];
					entity.Promotor = (reader.IsDBNull(((int)PersonaClienteColumn.Promotor)))?null:(System.String)reader[((int)PersonaClienteColumn.Promotor)];
					//entity.Promotor = (Convert.IsDBNull(reader["Promotor"]))?string.Empty:(System.String)reader["Promotor"];
					entity.Observacion = (reader.IsDBNull(((int)PersonaClienteColumn.Observacion)))?null:(System.String)reader[((int)PersonaClienteColumn.Observacion)];
					//entity.Observacion = (Convert.IsDBNull(reader["Observacion"]))?string.Empty:(System.String)reader["Observacion"];
					entity.TipoId = (System.Int32)reader[((int)PersonaClienteColumn.TipoId)];
					//entity.TipoId = (Convert.IsDBNull(reader["TipoID"]))?(int)0:(System.Int32)reader["TipoID"];
					entity.TipoDocumento = (reader.IsDBNull(((int)PersonaClienteColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.TipoDocumento)];
					//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
					entity.Celular = (reader.IsDBNull(((int)PersonaClienteColumn.Celular)))?null:(System.String)reader[((int)PersonaClienteColumn.Celular)];
					//entity.Celular = (Convert.IsDBNull(reader["Celular"]))?string.Empty:(System.String)reader["Celular"];
					entity.Nacionalidad = (reader.IsDBNull(((int)PersonaClienteColumn.Nacionalidad)))?null:(System.String)reader[((int)PersonaClienteColumn.Nacionalidad)];
					//entity.Nacionalidad = (Convert.IsDBNull(reader["Nacionalidad"]))?string.Empty:(System.String)reader["Nacionalidad"];
					entity.PaisResidencia = (reader.IsDBNull(((int)PersonaClienteColumn.PaisResidencia)))?null:(System.String)reader[((int)PersonaClienteColumn.PaisResidencia)];
					//entity.PaisResidencia = (Convert.IsDBNull(reader["PaisResidencia"]))?string.Empty:(System.String)reader["PaisResidencia"];
					entity.Provincia = (reader.IsDBNull(((int)PersonaClienteColumn.Provincia)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.Provincia)];
					//entity.Provincia = (Convert.IsDBNull(reader["Provincia"]))?(int)0:(System.Int32?)reader["Provincia"];
					entity.AcceptChanges();
					entity.SuppressEntityEvents = false;
					
					rows.Add(entity);
					pageLength -= 1;
				}
				recordnum += 1;
			}
			return rows;
		}
		
		
		/// <summary>
		/// Refreshes the <see cref="PersonaCliente"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="PersonaCliente"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, PersonaCliente entity)
		{
			reader.Read();
			entity.PersonaId = (System.Guid)reader[((int)PersonaClienteColumn.PersonaId)];
			//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
			entity.Apellido = (reader.IsDBNull(((int)PersonaClienteColumn.Apellido)))?null:(System.String)reader[((int)PersonaClienteColumn.Apellido)];
			//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
			entity.Nombre = (reader.IsDBNull(((int)PersonaClienteColumn.Nombre)))?null:(System.String)reader[((int)PersonaClienteColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			entity.NroDocumento = (reader.IsDBNull(((int)PersonaClienteColumn.NroDocumento)))?null:(System.String)reader[((int)PersonaClienteColumn.NroDocumento)];
			//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
			entity.Telefono = (reader.IsDBNull(((int)PersonaClienteColumn.Telefono)))?null:(System.String)reader[((int)PersonaClienteColumn.Telefono)];
			//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
			entity.Email = (reader.IsDBNull(((int)PersonaClienteColumn.Email)))?null:(System.String)reader[((int)PersonaClienteColumn.Email)];
			//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
			entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaClienteColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PersonaClienteColumn.FechaNacimiento)];
			//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
			entity.Domicilio = (reader.IsDBNull(((int)PersonaClienteColumn.Domicilio)))?null:(System.String)reader[((int)PersonaClienteColumn.Domicilio)];
			//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
			entity.Sexo = (reader.IsDBNull(((int)PersonaClienteColumn.Sexo)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.Sexo)];
			//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
			entity.LocalidadId = (reader.IsDBNull(((int)PersonaClienteColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.LocalidadId)];
			//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
			entity.ClienteId = (System.Guid)reader[((int)PersonaClienteColumn.ClienteId)];
			//entity.ClienteId = (Convert.IsDBNull(reader["ClienteID"]))?Guid.Empty:(System.Guid)reader["ClienteID"];
			entity.RazonSocial = (reader.IsDBNull(((int)PersonaClienteColumn.RazonSocial)))?null:(System.String)reader[((int)PersonaClienteColumn.RazonSocial)];
			//entity.RazonSocial = (Convert.IsDBNull(reader["RazonSocial"]))?string.Empty:(System.String)reader["RazonSocial"];
			entity.Cuit = (reader.IsDBNull(((int)PersonaClienteColumn.Cuit)))?null:(System.String)reader[((int)PersonaClienteColumn.Cuit)];
			//entity.Cuit = (Convert.IsDBNull(reader["Cuit"]))?string.Empty:(System.String)reader["Cuit"];
			entity.Moneda = (reader.IsDBNull(((int)PersonaClienteColumn.Moneda)))?null:(System.String)reader[((int)PersonaClienteColumn.Moneda)];
			//entity.Moneda = (Convert.IsDBNull(reader["Moneda"]))?string.Empty:(System.String)reader["Moneda"];
			entity.Empresa = (reader.IsDBNull(((int)PersonaClienteColumn.Empresa)))?null:(System.String)reader[((int)PersonaClienteColumn.Empresa)];
			//entity.Empresa = (Convert.IsDBNull(reader["Empresa"]))?string.Empty:(System.String)reader["Empresa"];
			entity.Ocupacion = (reader.IsDBNull(((int)PersonaClienteColumn.Ocupacion)))?null:(System.String)reader[((int)PersonaClienteColumn.Ocupacion)];
			//entity.Ocupacion = (Convert.IsDBNull(reader["Ocupacion"]))?string.Empty:(System.String)reader["Ocupacion"];
			entity.FormaPago = (reader.IsDBNull(((int)PersonaClienteColumn.FormaPago)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.FormaPago)];
			//entity.FormaPago = (Convert.IsDBNull(reader["FormaPago"]))?(int)0:(System.Int32?)reader["FormaPago"];
			entity.CondicionIva = (reader.IsDBNull(((int)PersonaClienteColumn.CondicionIva)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.CondicionIva)];
			//entity.CondicionIva = (Convert.IsDBNull(reader["CondicionIva"]))?(int)0:(System.Int32?)reader["CondicionIva"];
			entity.VendedorId = (reader.IsDBNull(((int)PersonaClienteColumn.VendedorId)))?null:(System.Guid?)reader[((int)PersonaClienteColumn.VendedorId)];
			//entity.VendedorId = (Convert.IsDBNull(reader["VendedorID"]))?Guid.Empty:(System.Guid?)reader["VendedorID"];
			entity.Fax = (reader.IsDBNull(((int)PersonaClienteColumn.Fax)))?null:(System.String)reader[((int)PersonaClienteColumn.Fax)];
			//entity.Fax = (Convert.IsDBNull(reader["Fax"]))?string.Empty:(System.String)reader["Fax"];
			entity.Web = (reader.IsDBNull(((int)PersonaClienteColumn.Web)))?null:(System.String)reader[((int)PersonaClienteColumn.Web)];
			//entity.Web = (Convert.IsDBNull(reader["Web"]))?string.Empty:(System.String)reader["Web"];
			entity.Idioma = (reader.IsDBNull(((int)PersonaClienteColumn.Idioma)))?null:(System.String)reader[((int)PersonaClienteColumn.Idioma)];
			//entity.Idioma = (Convert.IsDBNull(reader["Idioma"]))?string.Empty:(System.String)reader["Idioma"];
			entity.Promotor = (reader.IsDBNull(((int)PersonaClienteColumn.Promotor)))?null:(System.String)reader[((int)PersonaClienteColumn.Promotor)];
			//entity.Promotor = (Convert.IsDBNull(reader["Promotor"]))?string.Empty:(System.String)reader["Promotor"];
			entity.Observacion = (reader.IsDBNull(((int)PersonaClienteColumn.Observacion)))?null:(System.String)reader[((int)PersonaClienteColumn.Observacion)];
			//entity.Observacion = (Convert.IsDBNull(reader["Observacion"]))?string.Empty:(System.String)reader["Observacion"];
			entity.TipoId = (System.Int32)reader[((int)PersonaClienteColumn.TipoId)];
			//entity.TipoId = (Convert.IsDBNull(reader["TipoID"]))?(int)0:(System.Int32)reader["TipoID"];
			entity.TipoDocumento = (reader.IsDBNull(((int)PersonaClienteColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.TipoDocumento)];
			//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
			entity.Celular = (reader.IsDBNull(((int)PersonaClienteColumn.Celular)))?null:(System.String)reader[((int)PersonaClienteColumn.Celular)];
			//entity.Celular = (Convert.IsDBNull(reader["Celular"]))?string.Empty:(System.String)reader["Celular"];
			entity.Nacionalidad = (reader.IsDBNull(((int)PersonaClienteColumn.Nacionalidad)))?null:(System.String)reader[((int)PersonaClienteColumn.Nacionalidad)];
			//entity.Nacionalidad = (Convert.IsDBNull(reader["Nacionalidad"]))?string.Empty:(System.String)reader["Nacionalidad"];
			entity.PaisResidencia = (reader.IsDBNull(((int)PersonaClienteColumn.PaisResidencia)))?null:(System.String)reader[((int)PersonaClienteColumn.PaisResidencia)];
			//entity.PaisResidencia = (Convert.IsDBNull(reader["PaisResidencia"]))?string.Empty:(System.String)reader["PaisResidencia"];
			entity.Provincia = (reader.IsDBNull(((int)PersonaClienteColumn.Provincia)))?null:(System.Int32?)reader[((int)PersonaClienteColumn.Provincia)];
			//entity.Provincia = (Convert.IsDBNull(reader["Provincia"]))?(int)0:(System.Int32?)reader["Provincia"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="PersonaCliente"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="PersonaCliente"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, PersonaCliente entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PersonaId = (Convert.IsDBNull(dataRow["PersonaID"]))?Guid.Empty:(System.Guid)dataRow["PersonaID"];
			entity.Apellido = (Convert.IsDBNull(dataRow["Apellido"]))?string.Empty:(System.String)dataRow["Apellido"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.NroDocumento = (Convert.IsDBNull(dataRow["NroDocumento"]))?string.Empty:(System.String)dataRow["NroDocumento"];
			entity.Telefono = (Convert.IsDBNull(dataRow["Telefono"]))?string.Empty:(System.String)dataRow["Telefono"];
			entity.Email = (Convert.IsDBNull(dataRow["Email"]))?string.Empty:(System.String)dataRow["Email"];
			entity.FechaNacimiento = (Convert.IsDBNull(dataRow["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)dataRow["FechaNacimiento"];
			entity.Domicilio = (Convert.IsDBNull(dataRow["Domicilio"]))?string.Empty:(System.String)dataRow["Domicilio"];
			entity.Sexo = (Convert.IsDBNull(dataRow["Sexo"]))?(int)0:(System.Int32?)dataRow["Sexo"];
			entity.LocalidadId = (Convert.IsDBNull(dataRow["LocalidadID"]))?(int)0:(System.Int32?)dataRow["LocalidadID"];
			entity.ClienteId = (Convert.IsDBNull(dataRow["ClienteID"]))?Guid.Empty:(System.Guid)dataRow["ClienteID"];
			entity.RazonSocial = (Convert.IsDBNull(dataRow["RazonSocial"]))?string.Empty:(System.String)dataRow["RazonSocial"];
			entity.Cuit = (Convert.IsDBNull(dataRow["Cuit"]))?string.Empty:(System.String)dataRow["Cuit"];
			entity.Moneda = (Convert.IsDBNull(dataRow["Moneda"]))?string.Empty:(System.String)dataRow["Moneda"];
			entity.Empresa = (Convert.IsDBNull(dataRow["Empresa"]))?string.Empty:(System.String)dataRow["Empresa"];
			entity.Ocupacion = (Convert.IsDBNull(dataRow["Ocupacion"]))?string.Empty:(System.String)dataRow["Ocupacion"];
			entity.FormaPago = (Convert.IsDBNull(dataRow["FormaPago"]))?(int)0:(System.Int32?)dataRow["FormaPago"];
			entity.CondicionIva = (Convert.IsDBNull(dataRow["CondicionIva"]))?(int)0:(System.Int32?)dataRow["CondicionIva"];
			entity.VendedorId = (Convert.IsDBNull(dataRow["VendedorID"]))?Guid.Empty:(System.Guid?)dataRow["VendedorID"];
			entity.Fax = (Convert.IsDBNull(dataRow["Fax"]))?string.Empty:(System.String)dataRow["Fax"];
			entity.Web = (Convert.IsDBNull(dataRow["Web"]))?string.Empty:(System.String)dataRow["Web"];
			entity.Idioma = (Convert.IsDBNull(dataRow["Idioma"]))?string.Empty:(System.String)dataRow["Idioma"];
			entity.Promotor = (Convert.IsDBNull(dataRow["Promotor"]))?string.Empty:(System.String)dataRow["Promotor"];
			entity.Observacion = (Convert.IsDBNull(dataRow["Observacion"]))?string.Empty:(System.String)dataRow["Observacion"];
			entity.TipoId = (Convert.IsDBNull(dataRow["TipoID"]))?(int)0:(System.Int32)dataRow["TipoID"];
			entity.TipoDocumento = (Convert.IsDBNull(dataRow["TipoDocumento"]))?(int)0:(System.Int32?)dataRow["TipoDocumento"];
			entity.Celular = (Convert.IsDBNull(dataRow["Celular"]))?string.Empty:(System.String)dataRow["Celular"];
			entity.Nacionalidad = (Convert.IsDBNull(dataRow["Nacionalidad"]))?string.Empty:(System.String)dataRow["Nacionalidad"];
			entity.PaisResidencia = (Convert.IsDBNull(dataRow["PaisResidencia"]))?string.Empty:(System.String)dataRow["PaisResidencia"];
			entity.Provincia = (Convert.IsDBNull(dataRow["Provincia"]))?(int)0:(System.Int32?)dataRow["Provincia"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region PersonaClienteFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaClienteFilterBuilder : SqlFilterBuilder<PersonaClienteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaClienteFilterBuilder class.
		/// </summary>
		public PersonaClienteFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaClienteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaClienteFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaClienteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaClienteFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaClienteFilterBuilder

	#region PersonaClienteParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaCliente"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaClienteParameterBuilder : ParameterizedSqlFilterBuilder<PersonaClienteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaClienteParameterBuilder class.
		/// </summary>
		public PersonaClienteParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaClienteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaClienteParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaClienteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaClienteParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaClienteParameterBuilder
	
	#region PersonaClienteSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaCliente"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PersonaClienteSortBuilder : SqlSortBuilder<PersonaClienteColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaClienteSqlSortBuilder class.
		/// </summary>
		public PersonaClienteSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PersonaClienteSortBuilder

} // end namespace
