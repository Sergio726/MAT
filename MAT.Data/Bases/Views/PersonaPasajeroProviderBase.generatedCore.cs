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
	/// This class is the base class for any <see cref="PersonaPasajeroProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class PersonaPasajeroProviderBaseCore : EntityViewProviderBase<PersonaPasajero>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;PersonaPasajero&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;PersonaPasajero&gt;"/></returns>
		protected static VList&lt;PersonaPasajero&gt; Fill(DataSet dataSet, VList<PersonaPasajero> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<PersonaPasajero>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;PersonaPasajero&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<PersonaPasajero>"/></returns>
		protected static VList&lt;PersonaPasajero&gt; Fill(DataTable dataTable, VList<PersonaPasajero> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					PersonaPasajero c = new PersonaPasajero();
					c.PersonaId = (Convert.IsDBNull(row["PersonaID"]))?Guid.Empty:(System.Guid)row["PersonaID"];
					c.Apellido = (Convert.IsDBNull(row["Apellido"]))?string.Empty:(System.String)row["Apellido"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
					c.NroDocumento = (Convert.IsDBNull(row["NroDocumento"]))?string.Empty:(System.String)row["NroDocumento"];
					c.Telefono = (Convert.IsDBNull(row["Telefono"]))?string.Empty:(System.String)row["Telefono"];
					c.Domicilio = (Convert.IsDBNull(row["Domicilio"]))?string.Empty:(System.String)row["Domicilio"];
					c.Email = (Convert.IsDBNull(row["Email"]))?string.Empty:(System.String)row["Email"];
					c.FechaNacimiento = (Convert.IsDBNull(row["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)row["FechaNacimiento"];
					c.Sexo = (Convert.IsDBNull(row["Sexo"]))?(int)0:(System.Int32?)row["Sexo"];
					c.PasajeroId = (Convert.IsDBNull(row["PasajeroID"]))?Guid.Empty:(System.Guid)row["PasajeroID"];
					c.Pasaporte = (Convert.IsDBNull(row["Pasaporte"]))?string.Empty:(System.String)row["Pasaporte"];
					c.VencimientoPasaporte = (Convert.IsDBNull(row["VencimientoPasaporte"]))?DateTime.MinValue:(System.DateTime?)row["VencimientoPasaporte"];
					c.EmisionPasaporte = (Convert.IsDBNull(row["EmisionPasaporte"]))?DateTime.MinValue:(System.DateTime?)row["EmisionPasaporte"];
					c.PaisOrigen = (Convert.IsDBNull(row["PaisOrigen"]))?string.Empty:(System.String)row["PaisOrigen"];
					c.LocalidadId = (Convert.IsDBNull(row["LocalidadID"]))?(int)0:(System.Int32?)row["LocalidadID"];
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
		/// Fill an <see cref="VList&lt;PersonaPasajero&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;PersonaPasajero&gt;"/></returns>
		protected VList<PersonaPasajero> Fill(IDataReader reader, VList<PersonaPasajero> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					PersonaPasajero entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<PersonaPasajero>("PersonaPasajero",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new PersonaPasajero();
					}
					
					entity.SuppressEntityEvents = true;

					entity.PersonaId = (System.Guid)reader[((int)PersonaPasajeroColumn.PersonaId)];
					//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
					entity.Apellido = (reader.IsDBNull(((int)PersonaPasajeroColumn.Apellido)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Apellido)];
					//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
					entity.Nombre = (reader.IsDBNull(((int)PersonaPasajeroColumn.Nombre)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
					entity.NroDocumento = (reader.IsDBNull(((int)PersonaPasajeroColumn.NroDocumento)))?null:(System.String)reader[((int)PersonaPasajeroColumn.NroDocumento)];
					//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
					entity.Telefono = (reader.IsDBNull(((int)PersonaPasajeroColumn.Telefono)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Telefono)];
					//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
					entity.Domicilio = (reader.IsDBNull(((int)PersonaPasajeroColumn.Domicilio)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Domicilio)];
					//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
					entity.Email = (reader.IsDBNull(((int)PersonaPasajeroColumn.Email)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Email)];
					//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
					entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaPasajeroColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PersonaPasajeroColumn.FechaNacimiento)];
					//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
					entity.Sexo = (reader.IsDBNull(((int)PersonaPasajeroColumn.Sexo)))?null:(System.Int32?)reader[((int)PersonaPasajeroColumn.Sexo)];
					//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
					entity.PasajeroId = (System.Guid)reader[((int)PersonaPasajeroColumn.PasajeroId)];
					//entity.PasajeroId = (Convert.IsDBNull(reader["PasajeroID"]))?Guid.Empty:(System.Guid)reader["PasajeroID"];
					entity.Pasaporte = (reader.IsDBNull(((int)PersonaPasajeroColumn.Pasaporte)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Pasaporte)];
					//entity.Pasaporte = (Convert.IsDBNull(reader["Pasaporte"]))?string.Empty:(System.String)reader["Pasaporte"];
					entity.VencimientoPasaporte = (reader.IsDBNull(((int)PersonaPasajeroColumn.VencimientoPasaporte)))?null:(System.DateTime?)reader[((int)PersonaPasajeroColumn.VencimientoPasaporte)];
					//entity.VencimientoPasaporte = (Convert.IsDBNull(reader["VencimientoPasaporte"]))?DateTime.MinValue:(System.DateTime?)reader["VencimientoPasaporte"];
					entity.EmisionPasaporte = (reader.IsDBNull(((int)PersonaPasajeroColumn.EmisionPasaporte)))?null:(System.DateTime?)reader[((int)PersonaPasajeroColumn.EmisionPasaporte)];
					//entity.EmisionPasaporte = (Convert.IsDBNull(reader["EmisionPasaporte"]))?DateTime.MinValue:(System.DateTime?)reader["EmisionPasaporte"];
					entity.PaisOrigen = (reader.IsDBNull(((int)PersonaPasajeroColumn.PaisOrigen)))?null:(System.String)reader[((int)PersonaPasajeroColumn.PaisOrigen)];
					//entity.PaisOrigen = (Convert.IsDBNull(reader["PaisOrigen"]))?string.Empty:(System.String)reader["PaisOrigen"];
					entity.LocalidadId = (reader.IsDBNull(((int)PersonaPasajeroColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PersonaPasajeroColumn.LocalidadId)];
					//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
					entity.TipoDocumento = (reader.IsDBNull(((int)PersonaPasajeroColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PersonaPasajeroColumn.TipoDocumento)];
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
		/// Refreshes the <see cref="PersonaPasajero"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="PersonaPasajero"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, PersonaPasajero entity)
		{
			reader.Read();
			entity.PersonaId = (System.Guid)reader[((int)PersonaPasajeroColumn.PersonaId)];
			//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
			entity.Apellido = (reader.IsDBNull(((int)PersonaPasajeroColumn.Apellido)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Apellido)];
			//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
			entity.Nombre = (reader.IsDBNull(((int)PersonaPasajeroColumn.Nombre)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			entity.NroDocumento = (reader.IsDBNull(((int)PersonaPasajeroColumn.NroDocumento)))?null:(System.String)reader[((int)PersonaPasajeroColumn.NroDocumento)];
			//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
			entity.Telefono = (reader.IsDBNull(((int)PersonaPasajeroColumn.Telefono)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Telefono)];
			//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
			entity.Domicilio = (reader.IsDBNull(((int)PersonaPasajeroColumn.Domicilio)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Domicilio)];
			//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
			entity.Email = (reader.IsDBNull(((int)PersonaPasajeroColumn.Email)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Email)];
			//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
			entity.FechaNacimiento = (reader.IsDBNull(((int)PersonaPasajeroColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PersonaPasajeroColumn.FechaNacimiento)];
			//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
			entity.Sexo = (reader.IsDBNull(((int)PersonaPasajeroColumn.Sexo)))?null:(System.Int32?)reader[((int)PersonaPasajeroColumn.Sexo)];
			//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
			entity.PasajeroId = (System.Guid)reader[((int)PersonaPasajeroColumn.PasajeroId)];
			//entity.PasajeroId = (Convert.IsDBNull(reader["PasajeroID"]))?Guid.Empty:(System.Guid)reader["PasajeroID"];
			entity.Pasaporte = (reader.IsDBNull(((int)PersonaPasajeroColumn.Pasaporte)))?null:(System.String)reader[((int)PersonaPasajeroColumn.Pasaporte)];
			//entity.Pasaporte = (Convert.IsDBNull(reader["Pasaporte"]))?string.Empty:(System.String)reader["Pasaporte"];
			entity.VencimientoPasaporte = (reader.IsDBNull(((int)PersonaPasajeroColumn.VencimientoPasaporte)))?null:(System.DateTime?)reader[((int)PersonaPasajeroColumn.VencimientoPasaporte)];
			//entity.VencimientoPasaporte = (Convert.IsDBNull(reader["VencimientoPasaporte"]))?DateTime.MinValue:(System.DateTime?)reader["VencimientoPasaporte"];
			entity.EmisionPasaporte = (reader.IsDBNull(((int)PersonaPasajeroColumn.EmisionPasaporte)))?null:(System.DateTime?)reader[((int)PersonaPasajeroColumn.EmisionPasaporte)];
			//entity.EmisionPasaporte = (Convert.IsDBNull(reader["EmisionPasaporte"]))?DateTime.MinValue:(System.DateTime?)reader["EmisionPasaporte"];
			entity.PaisOrigen = (reader.IsDBNull(((int)PersonaPasajeroColumn.PaisOrigen)))?null:(System.String)reader[((int)PersonaPasajeroColumn.PaisOrigen)];
			//entity.PaisOrigen = (Convert.IsDBNull(reader["PaisOrigen"]))?string.Empty:(System.String)reader["PaisOrigen"];
			entity.LocalidadId = (reader.IsDBNull(((int)PersonaPasajeroColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PersonaPasajeroColumn.LocalidadId)];
			//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
			entity.TipoDocumento = (reader.IsDBNull(((int)PersonaPasajeroColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PersonaPasajeroColumn.TipoDocumento)];
			//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="PersonaPasajero"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="PersonaPasajero"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, PersonaPasajero entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PersonaId = (Convert.IsDBNull(dataRow["PersonaID"]))?Guid.Empty:(System.Guid)dataRow["PersonaID"];
			entity.Apellido = (Convert.IsDBNull(dataRow["Apellido"]))?string.Empty:(System.String)dataRow["Apellido"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.NroDocumento = (Convert.IsDBNull(dataRow["NroDocumento"]))?string.Empty:(System.String)dataRow["NroDocumento"];
			entity.Telefono = (Convert.IsDBNull(dataRow["Telefono"]))?string.Empty:(System.String)dataRow["Telefono"];
			entity.Domicilio = (Convert.IsDBNull(dataRow["Domicilio"]))?string.Empty:(System.String)dataRow["Domicilio"];
			entity.Email = (Convert.IsDBNull(dataRow["Email"]))?string.Empty:(System.String)dataRow["Email"];
			entity.FechaNacimiento = (Convert.IsDBNull(dataRow["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)dataRow["FechaNacimiento"];
			entity.Sexo = (Convert.IsDBNull(dataRow["Sexo"]))?(int)0:(System.Int32?)dataRow["Sexo"];
			entity.PasajeroId = (Convert.IsDBNull(dataRow["PasajeroID"]))?Guid.Empty:(System.Guid)dataRow["PasajeroID"];
			entity.Pasaporte = (Convert.IsDBNull(dataRow["Pasaporte"]))?string.Empty:(System.String)dataRow["Pasaporte"];
			entity.VencimientoPasaporte = (Convert.IsDBNull(dataRow["VencimientoPasaporte"]))?DateTime.MinValue:(System.DateTime?)dataRow["VencimientoPasaporte"];
			entity.EmisionPasaporte = (Convert.IsDBNull(dataRow["EmisionPasaporte"]))?DateTime.MinValue:(System.DateTime?)dataRow["EmisionPasaporte"];
			entity.PaisOrigen = (Convert.IsDBNull(dataRow["PaisOrigen"]))?string.Empty:(System.String)dataRow["PaisOrigen"];
			entity.LocalidadId = (Convert.IsDBNull(dataRow["LocalidadID"]))?(int)0:(System.Int32?)dataRow["LocalidadID"];
			entity.TipoDocumento = (Convert.IsDBNull(dataRow["TipoDocumento"]))?(int)0:(System.Int32?)dataRow["TipoDocumento"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region PersonaPasajeroFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaPasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaPasajeroFilterBuilder : SqlFilterBuilder<PersonaPasajeroColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroFilterBuilder class.
		/// </summary>
		public PersonaPasajeroFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaPasajeroFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaPasajeroFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaPasajeroFilterBuilder

	#region PersonaPasajeroParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaPasajero"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PersonaPasajeroParameterBuilder : ParameterizedSqlFilterBuilder<PersonaPasajeroColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroParameterBuilder class.
		/// </summary>
		public PersonaPasajeroParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PersonaPasajeroParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PersonaPasajeroParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PersonaPasajeroParameterBuilder
	
	#region PersonaPasajeroSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PersonaPasajero"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PersonaPasajeroSortBuilder : SqlSortBuilder<PersonaPasajeroColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PersonaPasajeroSqlSortBuilder class.
		/// </summary>
		public PersonaPasajeroSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PersonaPasajeroSortBuilder

} // end namespace
