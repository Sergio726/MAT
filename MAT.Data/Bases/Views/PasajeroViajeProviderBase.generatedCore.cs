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
	/// This class is the base class for any <see cref="PasajeroViajeProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class PasajeroViajeProviderBaseCore : EntityViewProviderBase<PasajeroViaje>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;PasajeroViaje&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;PasajeroViaje&gt;"/></returns>
		protected static VList&lt;PasajeroViaje&gt; Fill(DataSet dataSet, VList<PasajeroViaje> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<PasajeroViaje>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;PasajeroViaje&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<PasajeroViaje>"/></returns>
		protected static VList&lt;PasajeroViaje&gt; Fill(DataTable dataTable, VList<PasajeroViaje> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					PasajeroViaje c = new PasajeroViaje();
					c.Nro = (Convert.IsDBNull(row["Nro"]))?(long)0:(System.Int64?)row["Nro"];
					c.ViajeId = (Convert.IsDBNull(row["ViajeID"]))?Guid.Empty:(System.Guid?)row["ViajeID"];
					c.PersonaId = (Convert.IsDBNull(row["PersonaID"]))?Guid.Empty:(System.Guid)row["PersonaID"];
					c.BusId = (Convert.IsDBNull(row["BusID"]))?Guid.Empty:(System.Guid?)row["BusID"];
					c.Apellido = (Convert.IsDBNull(row["Apellido"]))?string.Empty:(System.String)row["Apellido"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
					c.TipoDocumento = (Convert.IsDBNull(row["TipoDocumento"]))?(int)0:(System.Int32?)row["TipoDocumento"];
					c.NroDocumento = (Convert.IsDBNull(row["NroDocumento"]))?string.Empty:(System.String)row["NroDocumento"];
					c.Telefono = (Convert.IsDBNull(row["Telefono"]))?string.Empty:(System.String)row["Telefono"];
					c.Email = (Convert.IsDBNull(row["Email"]))?string.Empty:(System.String)row["Email"];
					c.FechaNacimiento = (Convert.IsDBNull(row["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)row["FechaNacimiento"];
					c.Sexo = (Convert.IsDBNull(row["Sexo"]))?(int)0:(System.Int32?)row["Sexo"];
					c.LocalidadId = (Convert.IsDBNull(row["LocalidadID"]))?(int)0:(System.Int32?)row["LocalidadID"];
					c.Nacionalidad = (Convert.IsDBNull(row["Nacionalidad"]))?string.Empty:(System.String)row["Nacionalidad"];
					c.PaisResidencia = (Convert.IsDBNull(row["PaisResidencia"]))?string.Empty:(System.String)row["PaisResidencia"];
					c.Domicilio = (Convert.IsDBNull(row["Domicilio"]))?string.Empty:(System.String)row["Domicilio"];
					c.Ocupacion = (Convert.IsDBNull(row["Ocupacion"]))?string.Empty:(System.String)row["Ocupacion"];
					c.Pasaporte = (Convert.IsDBNull(row["Pasaporte"]))?string.Empty:(System.String)row["Pasaporte"];
					c.VencimientoPasaporte = (Convert.IsDBNull(row["VencimientoPasaporte"]))?DateTime.MinValue:(System.DateTime?)row["VencimientoPasaporte"];
					c.EmisionPasaporte = (Convert.IsDBNull(row["EmisionPasaporte"]))?DateTime.MinValue:(System.DateTime?)row["EmisionPasaporte"];
					c.PaisOrigen = (Convert.IsDBNull(row["PaisOrigen"]))?string.Empty:(System.String)row["PaisOrigen"];
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
		/// Fill an <see cref="VList&lt;PasajeroViaje&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;PasajeroViaje&gt;"/></returns>
		protected VList<PasajeroViaje> Fill(IDataReader reader, VList<PasajeroViaje> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					PasajeroViaje entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<PasajeroViaje>("PasajeroViaje",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new PasajeroViaje();
					}
					
					entity.SuppressEntityEvents = true;

					entity.Nro = (reader.IsDBNull(((int)PasajeroViajeColumn.Nro)))?null:(System.Int64?)reader[((int)PasajeroViajeColumn.Nro)];
					//entity.Nro = (Convert.IsDBNull(reader["Nro"]))?(long)0:(System.Int64?)reader["Nro"];
					entity.ViajeId = (reader.IsDBNull(((int)PasajeroViajeColumn.ViajeId)))?null:(System.Guid?)reader[((int)PasajeroViajeColumn.ViajeId)];
					//entity.ViajeId = (Convert.IsDBNull(reader["ViajeID"]))?Guid.Empty:(System.Guid?)reader["ViajeID"];
					entity.PersonaId = (System.Guid)reader[((int)PasajeroViajeColumn.PersonaId)];
					//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
					entity.BusId = (reader.IsDBNull(((int)PasajeroViajeColumn.BusId)))?null:(System.Guid?)reader[((int)PasajeroViajeColumn.BusId)];
					//entity.BusId = (Convert.IsDBNull(reader["BusID"]))?Guid.Empty:(System.Guid?)reader["BusID"];
					entity.Apellido = (reader.IsDBNull(((int)PasajeroViajeColumn.Apellido)))?null:(System.String)reader[((int)PasajeroViajeColumn.Apellido)];
					//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
					entity.Nombre = (reader.IsDBNull(((int)PasajeroViajeColumn.Nombre)))?null:(System.String)reader[((int)PasajeroViajeColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
					entity.TipoDocumento = (reader.IsDBNull(((int)PasajeroViajeColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PasajeroViajeColumn.TipoDocumento)];
					//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
					entity.NroDocumento = (reader.IsDBNull(((int)PasajeroViajeColumn.NroDocumento)))?null:(System.String)reader[((int)PasajeroViajeColumn.NroDocumento)];
					//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
					entity.Telefono = (reader.IsDBNull(((int)PasajeroViajeColumn.Telefono)))?null:(System.String)reader[((int)PasajeroViajeColumn.Telefono)];
					//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
					entity.Email = (reader.IsDBNull(((int)PasajeroViajeColumn.Email)))?null:(System.String)reader[((int)PasajeroViajeColumn.Email)];
					//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
					entity.FechaNacimiento = (reader.IsDBNull(((int)PasajeroViajeColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PasajeroViajeColumn.FechaNacimiento)];
					//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
					entity.Sexo = (reader.IsDBNull(((int)PasajeroViajeColumn.Sexo)))?null:(System.Int32?)reader[((int)PasajeroViajeColumn.Sexo)];
					//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
					entity.LocalidadId = (reader.IsDBNull(((int)PasajeroViajeColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PasajeroViajeColumn.LocalidadId)];
					//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
					entity.Nacionalidad = (reader.IsDBNull(((int)PasajeroViajeColumn.Nacionalidad)))?null:(System.String)reader[((int)PasajeroViajeColumn.Nacionalidad)];
					//entity.Nacionalidad = (Convert.IsDBNull(reader["Nacionalidad"]))?string.Empty:(System.String)reader["Nacionalidad"];
					entity.PaisResidencia = (reader.IsDBNull(((int)PasajeroViajeColumn.PaisResidencia)))?null:(System.String)reader[((int)PasajeroViajeColumn.PaisResidencia)];
					//entity.PaisResidencia = (Convert.IsDBNull(reader["PaisResidencia"]))?string.Empty:(System.String)reader["PaisResidencia"];
					entity.Domicilio = (reader.IsDBNull(((int)PasajeroViajeColumn.Domicilio)))?null:(System.String)reader[((int)PasajeroViajeColumn.Domicilio)];
					//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
					entity.Ocupacion = (reader.IsDBNull(((int)PasajeroViajeColumn.Ocupacion)))?null:(System.String)reader[((int)PasajeroViajeColumn.Ocupacion)];
					//entity.Ocupacion = (Convert.IsDBNull(reader["Ocupacion"]))?string.Empty:(System.String)reader["Ocupacion"];
					entity.Pasaporte = (reader.IsDBNull(((int)PasajeroViajeColumn.Pasaporte)))?null:(System.String)reader[((int)PasajeroViajeColumn.Pasaporte)];
					//entity.Pasaporte = (Convert.IsDBNull(reader["Pasaporte"]))?string.Empty:(System.String)reader["Pasaporte"];
					entity.VencimientoPasaporte = (reader.IsDBNull(((int)PasajeroViajeColumn.VencimientoPasaporte)))?null:(System.DateTime?)reader[((int)PasajeroViajeColumn.VencimientoPasaporte)];
					//entity.VencimientoPasaporte = (Convert.IsDBNull(reader["VencimientoPasaporte"]))?DateTime.MinValue:(System.DateTime?)reader["VencimientoPasaporte"];
					entity.EmisionPasaporte = (reader.IsDBNull(((int)PasajeroViajeColumn.EmisionPasaporte)))?null:(System.DateTime?)reader[((int)PasajeroViajeColumn.EmisionPasaporte)];
					//entity.EmisionPasaporte = (Convert.IsDBNull(reader["EmisionPasaporte"]))?DateTime.MinValue:(System.DateTime?)reader["EmisionPasaporte"];
					entity.PaisOrigen = (reader.IsDBNull(((int)PasajeroViajeColumn.PaisOrigen)))?null:(System.String)reader[((int)PasajeroViajeColumn.PaisOrigen)];
					//entity.PaisOrigen = (Convert.IsDBNull(reader["PaisOrigen"]))?string.Empty:(System.String)reader["PaisOrigen"];
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
		/// Refreshes the <see cref="PasajeroViaje"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="PasajeroViaje"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, PasajeroViaje entity)
		{
			reader.Read();
			entity.Nro = (reader.IsDBNull(((int)PasajeroViajeColumn.Nro)))?null:(System.Int64?)reader[((int)PasajeroViajeColumn.Nro)];
			//entity.Nro = (Convert.IsDBNull(reader["Nro"]))?(long)0:(System.Int64?)reader["Nro"];
			entity.ViajeId = (reader.IsDBNull(((int)PasajeroViajeColumn.ViajeId)))?null:(System.Guid?)reader[((int)PasajeroViajeColumn.ViajeId)];
			//entity.ViajeId = (Convert.IsDBNull(reader["ViajeID"]))?Guid.Empty:(System.Guid?)reader["ViajeID"];
			entity.PersonaId = (System.Guid)reader[((int)PasajeroViajeColumn.PersonaId)];
			//entity.PersonaId = (Convert.IsDBNull(reader["PersonaID"]))?Guid.Empty:(System.Guid)reader["PersonaID"];
			entity.BusId = (reader.IsDBNull(((int)PasajeroViajeColumn.BusId)))?null:(System.Guid?)reader[((int)PasajeroViajeColumn.BusId)];
			//entity.BusId = (Convert.IsDBNull(reader["BusID"]))?Guid.Empty:(System.Guid?)reader["BusID"];
			entity.Apellido = (reader.IsDBNull(((int)PasajeroViajeColumn.Apellido)))?null:(System.String)reader[((int)PasajeroViajeColumn.Apellido)];
			//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
			entity.Nombre = (reader.IsDBNull(((int)PasajeroViajeColumn.Nombre)))?null:(System.String)reader[((int)PasajeroViajeColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			entity.TipoDocumento = (reader.IsDBNull(((int)PasajeroViajeColumn.TipoDocumento)))?null:(System.Int32?)reader[((int)PasajeroViajeColumn.TipoDocumento)];
			//entity.TipoDocumento = (Convert.IsDBNull(reader["TipoDocumento"]))?(int)0:(System.Int32?)reader["TipoDocumento"];
			entity.NroDocumento = (reader.IsDBNull(((int)PasajeroViajeColumn.NroDocumento)))?null:(System.String)reader[((int)PasajeroViajeColumn.NroDocumento)];
			//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
			entity.Telefono = (reader.IsDBNull(((int)PasajeroViajeColumn.Telefono)))?null:(System.String)reader[((int)PasajeroViajeColumn.Telefono)];
			//entity.Telefono = (Convert.IsDBNull(reader["Telefono"]))?string.Empty:(System.String)reader["Telefono"];
			entity.Email = (reader.IsDBNull(((int)PasajeroViajeColumn.Email)))?null:(System.String)reader[((int)PasajeroViajeColumn.Email)];
			//entity.Email = (Convert.IsDBNull(reader["Email"]))?string.Empty:(System.String)reader["Email"];
			entity.FechaNacimiento = (reader.IsDBNull(((int)PasajeroViajeColumn.FechaNacimiento)))?null:(System.DateTime?)reader[((int)PasajeroViajeColumn.FechaNacimiento)];
			//entity.FechaNacimiento = (Convert.IsDBNull(reader["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)reader["FechaNacimiento"];
			entity.Sexo = (reader.IsDBNull(((int)PasajeroViajeColumn.Sexo)))?null:(System.Int32?)reader[((int)PasajeroViajeColumn.Sexo)];
			//entity.Sexo = (Convert.IsDBNull(reader["Sexo"]))?(int)0:(System.Int32?)reader["Sexo"];
			entity.LocalidadId = (reader.IsDBNull(((int)PasajeroViajeColumn.LocalidadId)))?null:(System.Int32?)reader[((int)PasajeroViajeColumn.LocalidadId)];
			//entity.LocalidadId = (Convert.IsDBNull(reader["LocalidadID"]))?(int)0:(System.Int32?)reader["LocalidadID"];
			entity.Nacionalidad = (reader.IsDBNull(((int)PasajeroViajeColumn.Nacionalidad)))?null:(System.String)reader[((int)PasajeroViajeColumn.Nacionalidad)];
			//entity.Nacionalidad = (Convert.IsDBNull(reader["Nacionalidad"]))?string.Empty:(System.String)reader["Nacionalidad"];
			entity.PaisResidencia = (reader.IsDBNull(((int)PasajeroViajeColumn.PaisResidencia)))?null:(System.String)reader[((int)PasajeroViajeColumn.PaisResidencia)];
			//entity.PaisResidencia = (Convert.IsDBNull(reader["PaisResidencia"]))?string.Empty:(System.String)reader["PaisResidencia"];
			entity.Domicilio = (reader.IsDBNull(((int)PasajeroViajeColumn.Domicilio)))?null:(System.String)reader[((int)PasajeroViajeColumn.Domicilio)];
			//entity.Domicilio = (Convert.IsDBNull(reader["Domicilio"]))?string.Empty:(System.String)reader["Domicilio"];
			entity.Ocupacion = (reader.IsDBNull(((int)PasajeroViajeColumn.Ocupacion)))?null:(System.String)reader[((int)PasajeroViajeColumn.Ocupacion)];
			//entity.Ocupacion = (Convert.IsDBNull(reader["Ocupacion"]))?string.Empty:(System.String)reader["Ocupacion"];
			entity.Pasaporte = (reader.IsDBNull(((int)PasajeroViajeColumn.Pasaporte)))?null:(System.String)reader[((int)PasajeroViajeColumn.Pasaporte)];
			//entity.Pasaporte = (Convert.IsDBNull(reader["Pasaporte"]))?string.Empty:(System.String)reader["Pasaporte"];
			entity.VencimientoPasaporte = (reader.IsDBNull(((int)PasajeroViajeColumn.VencimientoPasaporte)))?null:(System.DateTime?)reader[((int)PasajeroViajeColumn.VencimientoPasaporte)];
			//entity.VencimientoPasaporte = (Convert.IsDBNull(reader["VencimientoPasaporte"]))?DateTime.MinValue:(System.DateTime?)reader["VencimientoPasaporte"];
			entity.EmisionPasaporte = (reader.IsDBNull(((int)PasajeroViajeColumn.EmisionPasaporte)))?null:(System.DateTime?)reader[((int)PasajeroViajeColumn.EmisionPasaporte)];
			//entity.EmisionPasaporte = (Convert.IsDBNull(reader["EmisionPasaporte"]))?DateTime.MinValue:(System.DateTime?)reader["EmisionPasaporte"];
			entity.PaisOrigen = (reader.IsDBNull(((int)PasajeroViajeColumn.PaisOrigen)))?null:(System.String)reader[((int)PasajeroViajeColumn.PaisOrigen)];
			//entity.PaisOrigen = (Convert.IsDBNull(reader["PaisOrigen"]))?string.Empty:(System.String)reader["PaisOrigen"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="PasajeroViaje"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="PasajeroViaje"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, PasajeroViaje entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Nro = (Convert.IsDBNull(dataRow["Nro"]))?(long)0:(System.Int64?)dataRow["Nro"];
			entity.ViajeId = (Convert.IsDBNull(dataRow["ViajeID"]))?Guid.Empty:(System.Guid?)dataRow["ViajeID"];
			entity.PersonaId = (Convert.IsDBNull(dataRow["PersonaID"]))?Guid.Empty:(System.Guid)dataRow["PersonaID"];
			entity.BusId = (Convert.IsDBNull(dataRow["BusID"]))?Guid.Empty:(System.Guid?)dataRow["BusID"];
			entity.Apellido = (Convert.IsDBNull(dataRow["Apellido"]))?string.Empty:(System.String)dataRow["Apellido"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.TipoDocumento = (Convert.IsDBNull(dataRow["TipoDocumento"]))?(int)0:(System.Int32?)dataRow["TipoDocumento"];
			entity.NroDocumento = (Convert.IsDBNull(dataRow["NroDocumento"]))?string.Empty:(System.String)dataRow["NroDocumento"];
			entity.Telefono = (Convert.IsDBNull(dataRow["Telefono"]))?string.Empty:(System.String)dataRow["Telefono"];
			entity.Email = (Convert.IsDBNull(dataRow["Email"]))?string.Empty:(System.String)dataRow["Email"];
			entity.FechaNacimiento = (Convert.IsDBNull(dataRow["FechaNacimiento"]))?DateTime.MinValue:(System.DateTime?)dataRow["FechaNacimiento"];
			entity.Sexo = (Convert.IsDBNull(dataRow["Sexo"]))?(int)0:(System.Int32?)dataRow["Sexo"];
			entity.LocalidadId = (Convert.IsDBNull(dataRow["LocalidadID"]))?(int)0:(System.Int32?)dataRow["LocalidadID"];
			entity.Nacionalidad = (Convert.IsDBNull(dataRow["Nacionalidad"]))?string.Empty:(System.String)dataRow["Nacionalidad"];
			entity.PaisResidencia = (Convert.IsDBNull(dataRow["PaisResidencia"]))?string.Empty:(System.String)dataRow["PaisResidencia"];
			entity.Domicilio = (Convert.IsDBNull(dataRow["Domicilio"]))?string.Empty:(System.String)dataRow["Domicilio"];
			entity.Ocupacion = (Convert.IsDBNull(dataRow["Ocupacion"]))?string.Empty:(System.String)dataRow["Ocupacion"];
			entity.Pasaporte = (Convert.IsDBNull(dataRow["Pasaporte"]))?string.Empty:(System.String)dataRow["Pasaporte"];
			entity.VencimientoPasaporte = (Convert.IsDBNull(dataRow["VencimientoPasaporte"]))?DateTime.MinValue:(System.DateTime?)dataRow["VencimientoPasaporte"];
			entity.EmisionPasaporte = (Convert.IsDBNull(dataRow["EmisionPasaporte"]))?DateTime.MinValue:(System.DateTime?)dataRow["EmisionPasaporte"];
			entity.PaisOrigen = (Convert.IsDBNull(dataRow["PaisOrigen"]))?string.Empty:(System.String)dataRow["PaisOrigen"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region PasajeroViajeFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroViaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroViajeFilterBuilder : SqlFilterBuilder<PasajeroViajeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeFilterBuilder class.
		/// </summary>
		public PasajeroViajeFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroViajeFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroViajeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroViajeFilterBuilder

	#region PasajeroViajeParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroViaje"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PasajeroViajeParameterBuilder : ParameterizedSqlFilterBuilder<PasajeroViajeColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeParameterBuilder class.
		/// </summary>
		public PasajeroViajeParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PasajeroViajeParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PasajeroViajeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PasajeroViajeParameterBuilder
	
	#region PasajeroViajeSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="PasajeroViaje"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PasajeroViajeSortBuilder : SqlSortBuilder<PasajeroViajeColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PasajeroViajeSqlSortBuilder class.
		/// </summary>
		public PasajeroViajeSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PasajeroViajeSortBuilder

} // end namespace
