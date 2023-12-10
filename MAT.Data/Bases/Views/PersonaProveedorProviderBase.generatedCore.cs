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
	/// This class is the base class for any <see cref="PersonaProveedorProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class PersonaProveedorProviderBaseCore : EntityViewProviderBase<PersonaProveedor>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;PersonaProveedor&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;PersonaProveedor&gt;"/></returns>
		protected static VList&lt;PersonaProveedor&gt; Fill(DataSet dataSet, VList<PersonaProveedor> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<PersonaProveedor>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;PersonaProveedor&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<PersonaProveedor>"/></returns>
		protected static VList&lt;PersonaProveedor&gt; Fill(DataTable dataTable, VList<PersonaProveedor> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					PersonaProveedor c = new PersonaProveedor();
					c.PersonaId = (Convert.IsDBNull(row["PersonaID"]))?Guid.Empty:(System.Guid)row["PersonaID"];
					c.Apellido = (Convert.IsDBNull(row["Apellido"]))?string.Empty:(System.String)row["Apellido"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
					c.NroDocumento = (Convert.IsDBNull(row["NroDocumento"]))?string.Empty:(System.String)row["NroDocumento"];
					c.LocalidadId = (Convert.IsDBNull(row["LocalidadID"]))?(int)0:(System.Int32?)row["LocalidadID"];
					c.Telefono = (Convert.IsDBNull(row["Telefono"]))?string.Empty:(System.String)row["Telefono"];
					c.Email = (Convert.IsDBNull(row["Email"]))?string.Empty:(System.String)row["Email"];
					c.FechaNacimiento = (Convert.IsDBNull(row["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)row["FechaNacimiento"];
					c.Sexo = (Convert.IsDBNull(row["Sexo"]))?(int)0:(System.Int32?)row["Sexo"];
					c.Domicilio = (Convert.IsDBNull(row["Domicilio"]))?string.Empty:(System.String)row["Domicilio"];
					c.ProveedorId = (Convert.IsDBNull(row["ProveedorID"]))?Guid.Empty:(System.Guid)row["ProveedorID"];
					c.RazonSocial = (Convert.IsDBNull(row["RazonSocial"]))?string.Empty:(System.String)row["RazonSocial"];
					c.ProveedorLocalidadId = (Convert.IsDBNull(row["ProveedorLocalidadID"]))?(int)0:(System.Int32?)row["ProveedorLocalidadID"];
					c.ProveedorTelefono = (Convert.IsDBNull(row["ProveedorTelefono"]))?string.Empty:(System.String)row["ProveedorTelefono"];
					c.Fax = (Convert.IsDBNull(row["Fax"]))?string.Empty:(System.String)row["Fax"];
					c.Web = (Convert.IsDBNull(row["Web"]))?string.Empty:(System.String)row["Web"];
					c.ProveedorEmail = (Convert.IsDBNull(row["ProveedorEmail"]))?string.Empty:(System.String)row["ProveedorEmail"];
					c.Idioma = (Convert.IsDBNull(row["Idioma"]))?string.Empty:(System.String)row["Idioma"];
					c.CondicionIva = (Convert.IsDBNull(row["CondicionIva"]))?(int)0:(System.Int32?)row["CondicionIva"];
					c.Cuit = (Convert.IsDBNull(row["Cuit"]))?string.Empty:(System.String)row["Cuit"];
					c.FormaPago = (Convert.IsDBNull(row["FormaPago"]))?(int)0:(System.Int32?)row["FormaPago"];
					c.TipoDocumento = (Convert.IsDBNull(row["TipoDocumento"]))?(int)0:(System.Int32?)row["TipoDocumento"];
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
		/// Fill an <see cref="VList&lt;PersonaProveedor&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;PersonaProveedor&gt;"/></returns>
		protected VList<PersonaProveedor> Fill(IDataReader reader, VList<PersonaProveedor> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					PersonaProveedor entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<PersonaProveedor>("PersonaProveedor",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new PersonaProveedor();
					}
					
					entity.SuppressEntityEvents = true;

					entity.PersonaId = (System.Guid)reader[((int)PersonaProveedorColumn.PersonaId)];
					//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
					entity.Apellido = (reader.IsDBNull(((int)PersonaProveedorColumn.Apellido)))?null:(System.String)reader[((int)PersonaProveedorColumn.Apellido)];
					//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
					entity.Nombre = (reader.IsDBNull(((int)PersonaProveedorColumn.Nombre)))?null:(System.String)reader[((int)PersonaProveedorColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
					entity.NroDocumento = (reader.IsDBNull(((int)PersonaProveedorColumn.NroDocumento)))?null:(System.String)reader[((int)PersonaProveedorColumn.NroDocumento)];
					//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
					entity.LocalidadId = (reader.IsDBNull(((int)PersonaProveedorColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.LocalidadId)];
					//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
					entity.Telefono = (reader.IsDBNull(((int)PersonaProveedorColumn.Telefono)))?null:(System.String)reader[((int)PersonaProveedorColumn.Telefono)];
					//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
					entity.Email = (reader.IsDBNull(((int)PersonaProveedorColumn.Email)))?null:(System.String)reader[((int)PersonaProveedorColumn.Email)];
					//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
					entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaProveedorColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PersonaProveedorColumn.FechaNacimiento)];
					//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
					entity.Sexo = (reader.IsDBNull(((int)PersonaProveedorColumn.Sexo)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.Sexo)];
					//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
					entity.Domicilio = (reader.IsDBNull(((int)PersonaProveedorColumn.Domicilio)))?null:(System.String)reader[((int)PersonaProveedorColumn.Domicilio)];
					//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
					entity.ProveedorId = (System.Guid)reader[((int)PersonaProveedorColumn.ProveedorId)];
					//entity.ProveedorId = (Convert.IsDBNull(reader["ProveedorID"]))?Guid.Empty:(System.Guid)reader["ProveedorID"];
					entity.RazonSocial = (reader.IsDBNull(((int)PersonaProveedorColumn.RazonSocial)))?null:(System.String)reader[((int)PersonaProveedorColumn.RazonSocial)];
					//entity.RazonSocial = (Convert.IsDBNull(reader["RazonSocial"]))?string.Empty:(System.String)reader["RazonSocial"];
					entity.ProveedorLocalidadId = (reader.IsDBNull(((int)PersonaProveedorColumn.ProveedorLocalidadId)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.ProveedorLocalidadId)];
					//entity.ProveedorLocalidadId = (Convert.IsDBNull(reader["ProveedorLocalidadID"]))?(int)0:(System.Int32?)reader["ProveedorLocalidadID"];
					entity.ProveedorTelefono = (reader.IsDBNull(((int)PersonaProveedorColumn.ProveedorTelefono)))?null:(System.String)reader[((int)PersonaProveedorColumn.ProveedorTelefono)];
					//entity.ProveedorTelefono = (Convert.IsDBNull(reader["ProveedorTelefono"]))?string.Empty:(System.String)reader["ProveedorTelefono"];
					entity.Fax = (reader.IsDBNull(((int)PersonaProveedorColumn.Fax)))?null:(System.String)reader[((int)PersonaProveedorColumn.Fax)];
					//entity.Fax = (Convert.IsDBNull(reader["Fax"]))?string.Empty:(System.String)reader["Fax"];
					entity.Web = (reader.IsDBNull(((int)PersonaProveedorColumn.Web)))?null:(System.String)reader[((int)PersonaProveedorColumn.Web)];
					//entity.Web = (Convert.IsDBNull(reader["Web"]))?string.Empty:(System.String)reader["Web"];
					entity.ProveedorEmail = (reader.IsDBNull(((int)PersonaProveedorColumn.ProveedorEmail)))?null:(System.String)reader[((int)PersonaProveedorColumn.ProveedorEmail)];
					//entity.ProveedorEmail = (Convert.IsDBNull(reader["ProveedorEmail"]))?string.Empty:(System.String)reader["ProveedorEmail"];
					entity.Idioma = (reader.IsDBNull(((int)PersonaProveedorColumn.Idioma)))?null:(System.String)reader[((int)PersonaProveedorColumn.Idioma)];
					//entity.Idioma = (Convert.IsDBNull(reader["Idioma"]))?string.Empty:(System.String)reader["Idioma"];
					entity.CondicionIva = (reader.IsDBNull(((int)PersonaProveedorColumn.CondicionIva)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.CondicionIva)];
					//entity.CondicionIva = (Convert.IsDBNull(reader["CondicionIva"]))?(int)0:(System.Int32?)reader["CondicionIva"];
					entity.Cuit = (reader.IsDBNull(((int)PersonaProveedorColumn.Cuit)))?null:(System.String)reader[((int)PersonaProveedorColumn.Cuit)];
					//entity.Cuit = (Convert.IsDBNull(reader["Cuit"]))?string.Empty:(System.String)reader["Cuit"];
					entity.FormaPago = (reader.IsDBNull(((int)PersonaProveedorColumn.FormaPago)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.FormaPago)];
					//entity.FormaPago = (Convert.IsDBNull(reader["FormaPago"]))?(int)0:(System.Int32?)reader["FormaPago"];
					entity.TipoDocumento = (reader.IsDBNull(((int)PersonaProveedorColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.TipoDocumento)];
					//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
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
		/// Refreshes the <see cref="PersonaProveedor"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="PersonaProveedor"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, PersonaProveedor entity)
		{
			reader.Read();
			entity.PersonaId = (System.Guid)reader[((int)PersonaProveedorColumn.PersonaId)];
			//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
			entity.Apellido = (reader.IsDBNull(((int)PersonaProveedorColumn.Apellido)))?null:(System.String)reader[((int)PersonaProveedorColumn.Apellido)];
			//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
			entity.Nombre = (reader.IsDBNull(((int)PersonaProveedorColumn.Nombre)))?null:(System.String)reader[((int)PersonaProveedorColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			entity.NroDocumento = (reader.IsDBNull(((int)PersonaProveedorColumn.NroDocumento)))?null:(System.String)reader[((int)PersonaProveedorColumn.NroDocumento)];
			//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
			entity.LocalidadId = (reader.IsDBNull(((int)PersonaProveedorColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.LocalidadId)];
			//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
			entity.Telefono = (reader.IsDBNull(((int)PersonaProveedorColumn.Telefono)))?null:(System.String)reader[((int)PersonaProveedorColumn.Telefono)];
			//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
			entity.Email = (reader.IsDBNull(((int)PersonaProveedorColumn.Email)))?null:(System.String)reader[((int)PersonaProveedorColumn.Email)];
			//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
			entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaProveedorColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PersonaProveedorColumn.FechaNacimiento)];
			//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
			entity.Sexo = (reader.IsDBNull(((int)PersonaProveedorColumn.Sexo)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.Sexo)];
			//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
			entity.Domicilio = (reader.IsDBNull(((int)PersonaProveedorColumn.Domicilio)))?null:(System.String)reader[((int)PersonaProveedorColumn.Domicilio)];
			//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
			entity.ProveedorId = (System.Guid)reader[((int)PersonaProveedorColumn.ProveedorId)];
			//entity.ProveedorId = (Convert.IsDBNull(reader["ProveedorID"]))?Guid.Empty:(System.Guid)reader["ProveedorID"];
			entity.RazonSocial = (reader.IsDBNull(((int)PersonaProveedorColumn.RazonSocial)))?null:(System.String)reader[((int)PersonaProveedorColumn.RazonSocial)];
			//entity.RazonSocial = (Convert.IsDBNull(reader["RazonSocial"]))?string.Empty:(System.String)reader["RazonSocial"];
			entity.ProveedorLocalidadId = (reader.IsDBNull(((int)PersonaProveedorColumn.ProveedorLocalidadId)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.ProveedorLocalidadId)];
			//entity.ProveedorLocalidadId = (Convert.IsDBNull(reader["ProveedorLocalidadID"]))?(int)0:(System.Int32?)reader["ProveedorLocalidadID"];
			entity.ProveedorTelefono = (reader.IsDBNull(((int)PersonaProveedorColumn.ProveedorTelefono)))?null:(System.String)reader[((int)PersonaProveedorColumn.ProveedorTelefono)];
			//entity.ProveedorTelefono = (Convert.IsDBNull(reader["ProveedorTelefono"]))?string.Empty:(System.String)reader["ProveedorTelefono"];
			entity.Fax = (reader.IsDBNull(((int)PersonaProveedorColumn.Fax)))?null:(System.String)reader[((int)PersonaProveedorColumn.Fax)];
			//entity.Fax = (Convert.IsDBNull(reader["Fax"]))?string.Empty:(System.String)reader["Fax"];
			entity.Web = (reader.IsDBNull(((int)PersonaProveedorColumn.Web)))?null:(System.String)reader[((int)PersonaProveedorColumn.Web)];
			//entity.Web = (Convert.IsDBNull(reader["Web"]))?string.Empty:(System.String)reader["Web"];
			entity.ProveedorEmail = (reader.IsDBNull(((int)PersonaProveedorColumn.ProveedorEmail)))?null:(System.String)reader[((int)PersonaProveedorColumn.ProveedorEmail)];
			//entity.ProveedorEmail = (Convert.IsDBNull(reader["ProveedorEmail"]))?string.Empty:(System.String)reader["ProveedorEmail"];
			entity.Idioma = (reader.IsDBNull(((int)PersonaProveedorColumn.Idioma)))?null:(System.String)reader[((int)PersonaProveedorColumn.Idioma)];
			//entity.Idioma = (Convert.IsDBNull(reader["Idioma"]))?string.Empty:(System.String)reader["Idioma"];
			entity.CondicionIva = (reader.IsDBNull(((int)PersonaProveedorColumn.CondicionIva)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.CondicionIva)];
			//entity.CondicionIva = (Convert.IsDBNull(reader["CondicionIva"]))?(int)0:(System.Int32?)reader["CondicionIva"];
			entity.Cuit = (reader.IsDBNull(((int)PersonaProveedorColumn.Cuit)))?null:(System.String)reader[((int)PersonaProveedorColumn.Cuit)];
			//entity.Cuit = (Convert.IsDBNull(reader["Cuit"]))?string.Empty:(System.String)reader["Cuit"];
			entity.FormaPago = (reader.IsDBNull(((int)PersonaProveedorColumn.FormaPago)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.FormaPago)];
			//entity.FormaPago = (Convert.IsDBNull(reader["FormaPago"]))?(int)0:(System.Int32?)reader["FormaPago"];
			entity.TipoDocumento = (reader.IsDBNull(((int)PersonaProveedorColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PersonaProveedorColumn.TipoDocumento)];
			//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="PersonaProveedor"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="PersonaProveedor"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, PersonaProveedor entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PersonaId = (Convert.IsDBNull(dataRow["PersonaID"]))?Guid.Empty:(System.Guid)dataRow["PersonaID"];
			entity.Apellido = (Convert.IsDBNull(dataRow["Apellido"]))?string.Empty:(System.String)dataRow["Apellido"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.NroDocumento = (Convert.IsDBNull(dataRow["NroDocumento"]))?string.Empty:(System.String)dataRow["NroDocumento"];
			entity.LocalidadId = (Convert.IsDBNull(dataRow["LocalidadID"]))?(int)0:(System.Int32?)dataRow["LocalidadID"];
			entity.Telefono = (Convert.IsDBNull(dataRow["Telefono"]))?string.Empty:(System.String)dataRow["Telefono"];
			entity.Email = (Convert.IsDBNull(dataRow["Email"]))?string.Empty:(System.String)dataRow["Email"];
			entity.FechaNacimiento = (Convert.IsDBNull(dataRow["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)dataRow["FechaNacimiento"];
			entity.Sexo = (Convert.IsDBNull(dataRow["Sexo"]))?(int)0:(System.Int32?)dataRow["Sexo"];
			entity.Domicilio = (Convert.IsDBNull(dataRow["Domicilio"]))?string.Empty:(System.String)dataRow["Domicilio"];
			entity.ProveedorId = (Convert.IsDBNull(dataRow["ProveedorID"]))?Guid.Empty:(System.Guid)dataRow["ProveedorID"];
			entity.RazonSocial = (Convert.IsDBNull(dataRow["RazonSocial"]))?string.Empty:(System.String)dataRow["RazonSocial"];
			entity.ProveedorLocalidadId = (Convert.IsDBNull(dataRow["ProveedorLocalidadID"]))?(int)0:(System.Int32?)dataRow["ProveedorLocalidadID"];
			entity.ProveedorTelefono = (Convert.IsDBNull(dataRow["ProveedorTelefono"]))?string.Empty:(System.String)dataRow["ProveedorTelefono"];
			entity.Fax = (Convert.IsDBNull(dataRow["Fax"]))?string.Empty:(System.String)dataRow["Fax"];
			entity.Web = (Convert.IsDBNull(dataRow["Web"]))?string.Empty:(System.String)dataRow["Web"];
			entity.ProveedorEmail = (Convert.IsDBNull(dataRow["ProveedorEmail"]))?string.Empty:(System.String)dataRow["ProveedorEmail"];
			entity.Idioma = (Convert.IsDBNull(dataRow["Idioma"]))?string.Empty:(System.String)dataRow["Idioma"];
			entity.CondicionIva = (Convert.IsDBNull(dataRow["CondicionIva"]))?(int)0:(System.Int32?)dataRow["CondicionIva"];
			entity.Cuit = (Convert.IsDBNull(dataRow["Cuit"]))?string.Empty:(System.String)dataRow["Cuit"];
			entity.FormaPago = (Convert.IsDBNull(dataRow["FormaPago"]))?(int)0:(System.Int32?)dataRow["FormaPago"];
			entity.TipoDocumento = (Convert.IsDBNull(dataRow["TipoDocumento"]))?(int)0:(System.Int32?)dataRow["TipoDocumento"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region PersonaProveedorFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaProveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaProveedorFilterBuilder : SqlFilterBuilder<PersonaProveedorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorFilterBuilder class.
		/// </summary>
		public PersonaProveedorFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaProveedorFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaProveedorFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaProveedorFilterBuilder

	#region PersonaProveedorParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaProveedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaProveedorParameterBuilder : ParameterizedSqlFilterBuilder<PersonaProveedorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorParameterBuilder class.
		/// </summary>
		public PersonaProveedorParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaProveedorParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaProveedorParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaProveedorParameterBuilder
	
	#region PersonaProveedorSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaProveedor"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PersonaProveedorSortBuilder : SqlSortBuilder<PersonaProveedorColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaProveedorSqlSortBuilder class.
		/// </summary>
		public PersonaProveedorSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PersonaProveedorSortBuilder

} // end namespace
