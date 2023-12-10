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
	/// This class is the base class for any <see cref="PersonaVendedorProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class PersonaVendedorProviderBaseCore : EntityViewProviderBase<PersonaVendedor>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;PersonaVendedor&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;PersonaVendedor&gt;"/></returns>
		protected static VList&lt;PersonaVendedor&gt; Fill(DataSet dataSet, VList<PersonaVendedor> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<PersonaVendedor>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;PersonaVendedor&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<PersonaVendedor>"/></returns>
		protected static VList&lt;PersonaVendedor&gt; Fill(DataTable dataTable, VList<PersonaVendedor> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					PersonaVendedor c = new PersonaVendedor();
					c.PersonaId = (Convert.IsDBNull(row["PersonaID"]))?Guid.Empty:(System.Guid)row["PersonaID"];
					c.Apellido = (Convert.IsDBNull(row["Apellido"]))?string.Empty:(System.String)row["Apellido"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
					c.NroDocumento = (Convert.IsDBNull(row["NroDocumento"]))?string.Empty:(System.String)row["NroDocumento"];
					c.Domicilio = (Convert.IsDBNull(row["Domicilio"]))?string.Empty:(System.String)row["Domicilio"];
					c.Telefono = (Convert.IsDBNull(row["Telefono"]))?string.Empty:(System.String)row["Telefono"];
					c.Email = (Convert.IsDBNull(row["Email"]))?string.Empty:(System.String)row["Email"];
					c.FechaNacimiento = (Convert.IsDBNull(row["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)row["FechaNacimiento"];
					c.Sexo = (Convert.IsDBNull(row["Sexo"]))?(int)0:(System.Int32?)row["Sexo"];
					c.LocalidadId = (Convert.IsDBNull(row["LocalidadID"]))?(int)0:(System.Int32?)row["LocalidadID"];
					c.Descripcion = (Convert.IsDBNull(row["Descripcion"]))?string.Empty:(System.String)row["Descripcion"];
					c.VendedorId = (Convert.IsDBNull(row["VendedorID"]))?Guid.Empty:(System.Guid)row["VendedorID"];
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
		/// Fill an <see cref="VList&lt;PersonaVendedor&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;PersonaVendedor&gt;"/></returns>
		protected VList<PersonaVendedor> Fill(IDataReader reader, VList<PersonaVendedor> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					PersonaVendedor entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<PersonaVendedor>("PersonaVendedor",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new PersonaVendedor();
					}
					
					entity.SuppressEntityEvents = true;

					entity.PersonaId = (System.Guid)reader[((int)PersonaVendedorColumn.PersonaId)];
					//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
					entity.Apellido = (reader.IsDBNull(((int)PersonaVendedorColumn.Apellido)))?null:(System.String)reader[((int)PersonaVendedorColumn.Apellido)];
					//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
					entity.Nombre = (reader.IsDBNull(((int)PersonaVendedorColumn.Nombre)))?null:(System.String)reader[((int)PersonaVendedorColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
					entity.NroDocumento = (reader.IsDBNull(((int)PersonaVendedorColumn.NroDocumento)))?null:(System.String)reader[((int)PersonaVendedorColumn.NroDocumento)];
					//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
					entity.Domicilio = (reader.IsDBNull(((int)PersonaVendedorColumn.Domicilio)))?null:(System.String)reader[((int)PersonaVendedorColumn.Domicilio)];
					//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
					entity.Telefono = (reader.IsDBNull(((int)PersonaVendedorColumn.Telefono)))?null:(System.String)reader[((int)PersonaVendedorColumn.Telefono)];
					//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
					entity.Email = (reader.IsDBNull(((int)PersonaVendedorColumn.Email)))?null:(System.String)reader[((int)PersonaVendedorColumn.Email)];
					//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
					entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaVendedorColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PersonaVendedorColumn.FechaNacimiento)];
					//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
					entity.Sexo = (reader.IsDBNull(((int)PersonaVendedorColumn.Sexo)))?null:(System.Int32?)reader[((int)PersonaVendedorColumn.Sexo)];
					//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
					entity.LocalidadId = (reader.IsDBNull(((int)PersonaVendedorColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PersonaVendedorColumn.LocalidadId)];
					//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
					entity.Descripcion = (reader.IsDBNull(((int)PersonaVendedorColumn.Descripcion)))?null:(System.String)reader[((int)PersonaVendedorColumn.Descripcion)];
					//entity.Descripcion = (Convert.IsDBNull(reader["Descripcion"]))?string.Empty:(System.String)reader["Descripcion"];
					entity.VendedorId = (System.Guid)reader[((int)PersonaVendedorColumn.VendedorId)];
					//entity.VendedorId = (Convert.IsDBNull(reader["VendedorID"]))?Guid.Empty:(System.Guid)reader["VendedorID"];
					entity.TipoDocumento = (reader.IsDBNull(((int)PersonaVendedorColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PersonaVendedorColumn.TipoDocumento)];
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
		/// Refreshes the <see cref="PersonaVendedor"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="PersonaVendedor"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, PersonaVendedor entity)
		{
			reader.Read();
			entity.PersonaId = (System.Guid)reader[((int)PersonaVendedorColumn.PersonaId)];
			//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
			entity.Apellido = (reader.IsDBNull(((int)PersonaVendedorColumn.Apellido)))?null:(System.String)reader[((int)PersonaVendedorColumn.Apellido)];
			//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
			entity.Nombre = (reader.IsDBNull(((int)PersonaVendedorColumn.Nombre)))?null:(System.String)reader[((int)PersonaVendedorColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			entity.NroDocumento = (reader.IsDBNull(((int)PersonaVendedorColumn.NroDocumento)))?null:(System.String)reader[((int)PersonaVendedorColumn.NroDocumento)];
			//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
			entity.Domicilio = (reader.IsDBNull(((int)PersonaVendedorColumn.Domicilio)))?null:(System.String)reader[((int)PersonaVendedorColumn.Domicilio)];
			//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
			entity.Telefono = (reader.IsDBNull(((int)PersonaVendedorColumn.Telefono)))?null:(System.String)reader[((int)PersonaVendedorColumn.Telefono)];
			//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
			entity.Email = (reader.IsDBNull(((int)PersonaVendedorColumn.Email)))?null:(System.String)reader[((int)PersonaVendedorColumn.Email)];
			//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
			entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaVendedorColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PersonaVendedorColumn.FechaNacimiento)];
			//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
			entity.Sexo = (reader.IsDBNull(((int)PersonaVendedorColumn.Sexo)))?null:(System.Int32?)reader[((int)PersonaVendedorColumn.Sexo)];
			//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
			entity.LocalidadId = (reader.IsDBNull(((int)PersonaVendedorColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PersonaVendedorColumn.LocalidadId)];
			//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
			entity.Descripcion = (reader.IsDBNull(((int)PersonaVendedorColumn.Descripcion)))?null:(System.String)reader[((int)PersonaVendedorColumn.Descripcion)];
			//entity.Descripcion = (Convert.IsDBNull(reader["Descripcion"]))?string.Empty:(System.String)reader["Descripcion"];
			entity.VendedorId = (System.Guid)reader[((int)PersonaVendedorColumn.VendedorId)];
			//entity.VendedorId = (Convert.IsDBNull(reader["VendedorID"]))?Guid.Empty:(System.Guid)reader["VendedorID"];
			entity.TipoDocumento = (reader.IsDBNull(((int)PersonaVendedorColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PersonaVendedorColumn.TipoDocumento)];
			//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="PersonaVendedor"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="PersonaVendedor"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, PersonaVendedor entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PersonaId = (Convert.IsDBNull(dataRow["PersonaID"]))?Guid.Empty:(System.Guid)dataRow["PersonaID"];
			entity.Apellido = (Convert.IsDBNull(dataRow["Apellido"]))?string.Empty:(System.String)dataRow["Apellido"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.NroDocumento = (Convert.IsDBNull(dataRow["NroDocumento"]))?string.Empty:(System.String)dataRow["NroDocumento"];
			entity.Domicilio = (Convert.IsDBNull(dataRow["Domicilio"]))?string.Empty:(System.String)dataRow["Domicilio"];
			entity.Telefono = (Convert.IsDBNull(dataRow["Telefono"]))?string.Empty:(System.String)dataRow["Telefono"];
			entity.Email = (Convert.IsDBNull(dataRow["Email"]))?string.Empty:(System.String)dataRow["Email"];
			entity.FechaNacimiento = (Convert.IsDBNull(dataRow["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)dataRow["FechaNacimiento"];
			entity.Sexo = (Convert.IsDBNull(dataRow["Sexo"]))?(int)0:(System.Int32?)dataRow["Sexo"];
			entity.LocalidadId = (Convert.IsDBNull(dataRow["LocalidadID"]))?(int)0:(System.Int32?)dataRow["LocalidadID"];
			entity.Descripcion = (Convert.IsDBNull(dataRow["Descripcion"]))?string.Empty:(System.String)dataRow["Descripcion"];
			entity.VendedorId = (Convert.IsDBNull(dataRow["VendedorID"]))?Guid.Empty:(System.Guid)dataRow["VendedorID"];
			entity.TipoDocumento = (Convert.IsDBNull(dataRow["TipoDocumento"]))?(int)0:(System.Int32?)dataRow["TipoDocumento"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region PersonaVendedorFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaVendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaVendedorFilterBuilder : SqlFilterBuilder<PersonaVendedorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorFilterBuilder class.
		/// </summary>
		public PersonaVendedorFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaVendedorFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaVendedorFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaVendedorFilterBuilder

	#region PersonaVendedorParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaVendedor"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaVendedorParameterBuilder : ParameterizedSqlFilterBuilder<PersonaVendedorColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorParameterBuilder class.
		/// </summary>
		public PersonaVendedorParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaVendedorParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaVendedorParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaVendedorParameterBuilder
	
	#region PersonaVendedorSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaVendedor"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PersonaVendedorSortBuilder : SqlSortBuilder<PersonaVendedorColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaVendedorSqlSortBuilder class.
		/// </summary>
		public PersonaVendedorSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PersonaVendedorSortBuilder

} // end namespace
