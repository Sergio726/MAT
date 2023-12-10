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
	/// This class is the base class for any <see cref="VPersonaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class VPersonaProviderBaseCore : EntityViewProviderBase<VPersona>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;VPersona&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;VPersona&gt;"/></returns>
		protected static VList&lt;VPersona&gt; Fill(DataSet dataSet, VList<VPersona> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<VPersona>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;VPersona&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<VPersona>"/></returns>
		protected static VList&lt;VPersona&gt; Fill(DataTable dataTable, VList<VPersona> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					VPersona c = new VPersona();
					c.PersonaId = (Convert.IsDBNull(row["PersonaID"]))?Guid.Empty:(System.Guid)row["PersonaID"];
					c.Apellido = (Convert.IsDBNull(row["Apellido"]))?string.Empty:(System.String)row["Apellido"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
					c.TipoDocumento = (Convert.IsDBNull(row["TipoDocumento"]))?(int)0:(System.Int32?)row["TipoDocumento"];
					c.NroDocumento = (Convert.IsDBNull(row["NroDocumento"]))?string.Empty:(System.String)row["NroDocumento"];
					c.Telefono = (Convert.IsDBNull(row["Telefono"]))?string.Empty:(System.String)row["Telefono"];
					c.Email = (Convert.IsDBNull(row["Email"]))?string.Empty:(System.String)row["Email"];
					c.FechaNacimiento = (Convert.IsDBNull(row["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)row["FechaNacimiento"];
					c.LocalidadId = (Convert.IsDBNull(row["LocalidadID"]))?(int)0:(System.Int32?)row["LocalidadID"];
					c.UserId = (Convert.IsDBNull(row["UserId"]))?(int)0:(System.Int32?)row["UserId"];
					c.Domicilio = (Convert.IsDBNull(row["Domicilio"]))?string.Empty:(System.String)row["Domicilio"];
					c.Ocupacion = (Convert.IsDBNull(row["Ocupacion"]))?string.Empty:(System.String)row["Ocupacion"];
					c.Nacionalidad = (Convert.IsDBNull(row["Nacionalidad"]))?string.Empty:(System.String)row["Nacionalidad"];
					c.PaisResidencia = (Convert.IsDBNull(row["PaisResidencia"]))?string.Empty:(System.String)row["PaisResidencia"];
					c.Sexo = (Convert.IsDBNull(row["Sexo"]))?(int)0:(System.Int32?)row["Sexo"];
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
		/// Fill an <see cref="VList&lt;VPersona&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;VPersona&gt;"/></returns>
		protected VList<VPersona> Fill(IDataReader reader, VList<VPersona> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					VPersona entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<VPersona>("VPersona",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new VPersona();
					}
					
					entity.SuppressEntityEvents = true;

					entity.PersonaId = (System.Guid)reader[((int)VPersonaColumn.PersonaId)];
					//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
					entity.Apellido = (reader.IsDBNull(((int)VPersonaColumn.Apellido)))?null:(System.String)reader[((int)VPersonaColumn.Apellido)];
					//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
					entity.Nombre = (reader.IsDBNull(((int)VPersonaColumn.Nombre)))?null:(System.String)reader[((int)VPersonaColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
					entity.TipoDocumento = (reader.IsDBNull(((int)VPersonaColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)VPersonaColumn.TipoDocumento)];
					//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
					entity.NroDocumento = (reader.IsDBNull(((int)VPersonaColumn.NroDocumento)))?null:(System.String)reader[((int)VPersonaColumn.NroDocumento)];
					//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
					entity.Telefono = (reader.IsDBNull(((int)VPersonaColumn.Telefono)))?null:(System.String)reader[((int)VPersonaColumn.Telefono)];
					//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
					entity.Email = (reader.IsDBNull(((int)VPersonaColumn.Email)))?null:(System.String)reader[((int)VPersonaColumn.Email)];
					//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
					entity.FechaNacimiento = (reader.IsDBNull(((int)VPersonaColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)VPersonaColumn.FechaNacimiento)];
					//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
					entity.LocalidadId = (reader.IsDBNull(((int)VPersonaColumn.LocalidadId)))?null:(System.Int32?)reader[((int)VPersonaColumn.LocalidadId)];
					//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
					entity.UserId = (reader.IsDBNull(((int)VPersonaColumn.UserId)))?null:(System.Int32?)reader[((int)VPersonaColumn.UserId)];
					//entity.UserId = (Convert.IsDBNull(reader["UserId"]))?(int)0:(System.Int32?)reader["UserId"];
					entity.Domicilio = (reader.IsDBNull(((int)VPersonaColumn.Domicilio)))?null:(System.String)reader[((int)VPersonaColumn.Domicilio)];
					//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
					entity.Ocupacion = (reader.IsDBNull(((int)VPersonaColumn.Ocupacion)))?null:(System.String)reader[((int)VPersonaColumn.Ocupacion)];
					//entity.Ocupacion = (Convert.IsDBNull(reader["Ocupacion"]))?string.Empty:(System.String)reader["Ocupacion"];
					entity.Nacionalidad = (reader.IsDBNull(((int)VPersonaColumn.Nacionalidad)))?null:(System.String)reader[((int)VPersonaColumn.Nacionalidad)];
					//entity.Nacionalidad = (Convert.IsDBNull(reader["Nacionalidad"]))?string.Empty:(System.String)reader["Nacionalidad"];
					entity.PaisResidencia = (reader.IsDBNull(((int)VPersonaColumn.PaisResidencia)))?null:(System.String)reader[((int)VPersonaColumn.PaisResidencia)];
					//entity.PaisResidencia = (Convert.IsDBNull(reader["PaisResidencia"]))?string.Empty:(System.String)reader["PaisResidencia"];
					entity.Sexo = (reader.IsDBNull(((int)VPersonaColumn.Sexo)))?null:(System.Int32?)reader[((int)VPersonaColumn.Sexo)];
					//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
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
		/// Refreshes the <see cref="VPersona"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="VPersona"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, VPersona entity)
		{
			reader.Read();
			entity.PersonaId = (System.Guid)reader[((int)VPersonaColumn.PersonaId)];
			//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
			entity.Apellido = (reader.IsDBNull(((int)VPersonaColumn.Apellido)))?null:(System.String)reader[((int)VPersonaColumn.Apellido)];
			//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
			entity.Nombre = (reader.IsDBNull(((int)VPersonaColumn.Nombre)))?null:(System.String)reader[((int)VPersonaColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			entity.TipoDocumento = (reader.IsDBNull(((int)VPersonaColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)VPersonaColumn.TipoDocumento)];
			//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
			entity.NroDocumento = (reader.IsDBNull(((int)VPersonaColumn.NroDocumento)))?null:(System.String)reader[((int)VPersonaColumn.NroDocumento)];
			//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
			entity.Telefono = (reader.IsDBNull(((int)VPersonaColumn.Telefono)))?null:(System.String)reader[((int)VPersonaColumn.Telefono)];
			//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
			entity.Email = (reader.IsDBNull(((int)VPersonaColumn.Email)))?null:(System.String)reader[((int)VPersonaColumn.Email)];
			//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
			entity.FechaNacimiento = (reader.IsDBNull(((int)VPersonaColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)VPersonaColumn.FechaNacimiento)];
			//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
			entity.LocalidadId = (reader.IsDBNull(((int)VPersonaColumn.LocalidadId)))?null:(System.Int32?)reader[((int)VPersonaColumn.LocalidadId)];
			//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
			entity.UserId = (reader.IsDBNull(((int)VPersonaColumn.UserId)))?null:(System.Int32?)reader[((int)VPersonaColumn.UserId)];
			//entity.UserId = (Convert.IsDBNull(reader["UserId"]))?(int)0:(System.Int32?)reader["UserId"];
			entity.Domicilio = (reader.IsDBNull(((int)VPersonaColumn.Domicilio)))?null:(System.String)reader[((int)VPersonaColumn.Domicilio)];
			//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
			entity.Ocupacion = (reader.IsDBNull(((int)VPersonaColumn.Ocupacion)))?null:(System.String)reader[((int)VPersonaColumn.Ocupacion)];
			//entity.Ocupacion = (Convert.IsDBNull(reader["Ocupacion"]))?string.Empty:(System.String)reader["Ocupacion"];
			entity.Nacionalidad = (reader.IsDBNull(((int)VPersonaColumn.Nacionalidad)))?null:(System.String)reader[((int)VPersonaColumn.Nacionalidad)];
			//entity.Nacionalidad = (Convert.IsDBNull(reader["Nacionalidad"]))?string.Empty:(System.String)reader["Nacionalidad"];
			entity.PaisResidencia = (reader.IsDBNull(((int)VPersonaColumn.PaisResidencia)))?null:(System.String)reader[((int)VPersonaColumn.PaisResidencia)];
			//entity.PaisResidencia = (Convert.IsDBNull(reader["PaisResidencia"]))?string.Empty:(System.String)reader["PaisResidencia"];
			entity.Sexo = (reader.IsDBNull(((int)VPersonaColumn.Sexo)))?null:(System.Int32?)reader[((int)VPersonaColumn.Sexo)];
			//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="VPersona"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="VPersona"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, VPersona entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PersonaId = (Convert.IsDBNull(dataRow["PersonaID"]))?Guid.Empty:(System.Guid)dataRow["PersonaID"];
			entity.Apellido = (Convert.IsDBNull(dataRow["Apellido"]))?string.Empty:(System.String)dataRow["Apellido"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.TipoDocumento = (Convert.IsDBNull(dataRow["TipoDocumento"]))?(int)0:(System.Int32?)dataRow["TipoDocumento"];
			entity.NroDocumento = (Convert.IsDBNull(dataRow["NroDocumento"]))?string.Empty:(System.String)dataRow["NroDocumento"];
			entity.Telefono = (Convert.IsDBNull(dataRow["Telefono"]))?string.Empty:(System.String)dataRow["Telefono"];
			entity.Email = (Convert.IsDBNull(dataRow["Email"]))?string.Empty:(System.String)dataRow["Email"];
			entity.FechaNacimiento = (Convert.IsDBNull(dataRow["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)dataRow["FechaNacimiento"];
			entity.LocalidadId = (Convert.IsDBNull(dataRow["LocalidadID"]))?(int)0:(System.Int32?)dataRow["LocalidadID"];
			entity.UserId = (Convert.IsDBNull(dataRow["UserId"]))?(int)0:(System.Int32?)dataRow["UserId"];
			entity.Domicilio = (Convert.IsDBNull(dataRow["Domicilio"]))?string.Empty:(System.String)dataRow["Domicilio"];
			entity.Ocupacion = (Convert.IsDBNull(dataRow["Ocupacion"]))?string.Empty:(System.String)dataRow["Ocupacion"];
			entity.Nacionalidad = (Convert.IsDBNull(dataRow["Nacionalidad"]))?string.Empty:(System.String)dataRow["Nacionalidad"];
			entity.PaisResidencia = (Convert.IsDBNull(dataRow["PaisResidencia"]))?string.Empty:(System.String)dataRow["PaisResidencia"];
			entity.Sexo = (Convert.IsDBNull(dataRow["Sexo"]))?(int)0:(System.Int32?)dataRow["Sexo"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region VPersonaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VPersona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VPersonaFilterBuilder : SqlFilterBuilder<VPersonaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VPersonaFilterBuilder class.
		/// </summary>
		public VPersonaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VPersonaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VPersonaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VPersonaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VPersonaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VPersonaFilterBuilder

	#region VPersonaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VPersona"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VPersonaParameterBuilder : ParameterizedSqlFilterBuilder<VPersonaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VPersonaParameterBuilder class.
		/// </summary>
		public VPersonaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VPersonaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VPersonaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VPersonaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VPersonaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VPersonaParameterBuilder
	
	#region VPersonaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VPersona"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VPersonaSortBuilder : SqlSortBuilder<VPersonaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VPersonaSqlSortBuilder class.
		/// </summary>
		public VPersonaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VPersonaSortBuilder

} // end namespace
