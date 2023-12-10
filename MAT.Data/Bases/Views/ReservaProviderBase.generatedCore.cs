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
	/// This class is the base class for any <see cref="ReservaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class ReservaProviderBaseCore : EntityViewProviderBase<Reserva>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;Reserva&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;Reserva&gt;"/></returns>
		protected static VList&lt;Reserva&gt; Fill(DataSet dataSet, VList<Reserva> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<Reserva>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;Reserva&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<Reserva>"/></returns>
		protected static VList&lt;Reserva&gt; Fill(DataTable dataTable, VList<Reserva> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					Reserva c = new Reserva();
					c.PasajeId = (Convert.IsDBNull(row["PasajeID"]))?Guid.Empty:(System.Guid)row["PasajeID"];
					c.FechaReserva = (Convert.IsDBNull(row["FechaReserva"]))?DateTime.MinValue:(System.DateTime?)row["FechaReserva"];
					c.Apellido = (Convert.IsDBNull(row["Apellido"]))?string.Empty:(System.String)row["Apellido"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
					c.NroDocumento = (Convert.IsDBNull(row["NroDocumento"]))?string.Empty:(System.String)row["NroDocumento"];
					c.ViajeId = (Convert.IsDBNull(row["ViajeID"]))?Guid.Empty:(System.Guid?)row["ViajeID"];
					c.Paquete = (Convert.IsDBNull(row["Paquete"]))?string.Empty:(System.String)row["Paquete"];
					c.FacturaId = (Convert.IsDBNull(row["FacturaID"]))?Guid.Empty:(System.Guid?)row["FacturaID"];
					c.ClienteId = (Convert.IsDBNull(row["ClienteID"]))?Guid.Empty:(System.Guid?)row["ClienteID"];
					c.TipoCliente = (Convert.IsDBNull(row["TipoCliente"]))?(int)0:(System.Int32?)row["TipoCliente"];
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
		/// Fill an <see cref="VList&lt;Reserva&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;Reserva&gt;"/></returns>
		protected VList<Reserva> Fill(IDataReader reader, VList<Reserva> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					Reserva entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<Reserva>("Reserva",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new Reserva();
					}
					
					entity.SuppressEntityEvents = true;

					entity.PasajeId = (System.Guid)reader[((int)ReservaColumn.PasajeId)];
					//entity.PasajeId = (Convert.IsDBNull(reader["PasajeID"]))?Guid.Empty:(System.Guid)reader["PasajeID"];
					entity.FechaReserva = (reader.IsDBNull(((int)ReservaColumn.FechaReserva)))?null:(System.DateTime?)reader[((int)ReservaColumn.FechaReserva)];
					//entity.FechaReserva = (Convert.IsDBNull(reader["FechaReserva"]))?DateTime.MinValue:(System.DateTime?)reader["FechaReserva"];
					entity.Apellido = (reader.IsDBNull(((int)ReservaColumn.Apellido)))?null:(System.String)reader[((int)ReservaColumn.Apellido)];
					//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
					entity.Nombre = (reader.IsDBNull(((int)ReservaColumn.Nombre)))?null:(System.String)reader[((int)ReservaColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
					entity.NroDocumento = (reader.IsDBNull(((int)ReservaColumn.NroDocumento)))?null:(System.String)reader[((int)ReservaColumn.NroDocumento)];
					//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
					entity.ViajeId = (reader.IsDBNull(((int)ReservaColumn.ViajeId)))?null:(System.Guid?)reader[((int)ReservaColumn.ViajeId)];
					//entity.ViajeId = (Convert.IsDBNull(reader["ViajeID"]))?Guid.Empty:(System.Guid?)reader["ViajeID"];
					entity.Paquete = (reader.IsDBNull(((int)ReservaColumn.Paquete)))?null:(System.String)reader[((int)ReservaColumn.Paquete)];
					//entity.Paquete = (Convert.IsDBNull(reader["Paquete"]))?string.Empty:(System.String)reader["Paquete"];
					entity.FacturaId = (reader.IsDBNull(((int)ReservaColumn.FacturaId)))?null:(System.Guid?)reader[((int)ReservaColumn.FacturaId)];
					//entity.FacturaId = (Convert.IsDBNull(reader["FacturaID"]))?Guid.Empty:(System.Guid?)reader["FacturaID"];
					entity.ClienteId = (reader.IsDBNull(((int)ReservaColumn.ClienteId)))?null:(System.Guid?)reader[((int)ReservaColumn.ClienteId)];
					//entity.ClienteId = (Convert.IsDBNull(reader["ClienteID"]))?Guid.Empty:(System.Guid?)reader["ClienteID"];
					entity.TipoCliente = (reader.IsDBNull(((int)ReservaColumn.TipoCliente)))?null:(System.Int32?)reader[((int)ReservaColumn.TipoCliente)];
					//entity.TipoCliente = (Convert.IsDBNull(reader["TipoCliente"]))?(int)0:(System.Int32?)reader["TipoCliente"];
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
		/// Refreshes the <see cref="Reserva"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="Reserva"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, Reserva entity)
		{
			reader.Read();
			entity.PasajeId = (System.Guid)reader[((int)ReservaColumn.PasajeId)];
			//entity.PasajeId = (Convert.IsDBNull(reader["PasajeID"]))?Guid.Empty:(System.Guid)reader["PasajeID"];
			entity.FechaReserva = (reader.IsDBNull(((int)ReservaColumn.FechaReserva)))?null:(System.DateTime?)reader[((int)ReservaColumn.FechaReserva)];
			//entity.FechaReserva = (Convert.IsDBNull(reader["FechaReserva"]))?DateTime.MinValue:(System.DateTime?)reader["FechaReserva"];
			entity.Apellido = (reader.IsDBNull(((int)ReservaColumn.Apellido)))?null:(System.String)reader[((int)ReservaColumn.Apellido)];
			//entity.Apellido = (Convert.IsDBNull(reader["Apellido"]))?string.Empty:(System.String)reader["Apellido"];
			entity.Nombre = (reader.IsDBNull(((int)ReservaColumn.Nombre)))?null:(System.String)reader[((int)ReservaColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			entity.NroDocumento = (reader.IsDBNull(((int)ReservaColumn.NroDocumento)))?null:(System.String)reader[((int)ReservaColumn.NroDocumento)];
			//entity.NroDocumento = (Convert.IsDBNull(reader["NroDocumento"]))?string.Empty:(System.String)reader["NroDocumento"];
			entity.ViajeId = (reader.IsDBNull(((int)ReservaColumn.ViajeId)))?null:(System.Guid?)reader[((int)ReservaColumn.ViajeId)];
			//entity.ViajeId = (Convert.IsDBNull(reader["ViajeID"]))?Guid.Empty:(System.Guid?)reader["ViajeID"];
			entity.Paquete = (reader.IsDBNull(((int)ReservaColumn.Paquete)))?null:(System.String)reader[((int)ReservaColumn.Paquete)];
			//entity.Paquete = (Convert.IsDBNull(reader["Paquete"]))?string.Empty:(System.String)reader["Paquete"];
			entity.FacturaId = (reader.IsDBNull(((int)ReservaColumn.FacturaId)))?null:(System.Guid?)reader[((int)ReservaColumn.FacturaId)];
			//entity.FacturaId = (Convert.IsDBNull(reader["FacturaID"]))?Guid.Empty:(System.Guid?)reader["FacturaID"];
			entity.ClienteId = (reader.IsDBNull(((int)ReservaColumn.ClienteId)))?null:(System.Guid?)reader[((int)ReservaColumn.ClienteId)];
			//entity.ClienteId = (Convert.IsDBNull(reader["ClienteID"]))?Guid.Empty:(System.Guid?)reader["ClienteID"];
			entity.TipoCliente = (reader.IsDBNull(((int)ReservaColumn.TipoCliente)))?null:(System.Int32?)reader[((int)ReservaColumn.TipoCliente)];
			//entity.TipoCliente = (Convert.IsDBNull(reader["TipoCliente"]))?(int)0:(System.Int32?)reader["TipoCliente"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="Reserva"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="Reserva"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, Reserva entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.PasajeId = (Convert.IsDBNull(dataRow["PasajeID"]))?Guid.Empty:(System.Guid)dataRow["PasajeID"];
			entity.FechaReserva = (Convert.IsDBNull(dataRow["FechaReserva"]))?DateTime.MinValue:(System.DateTime?)dataRow["FechaReserva"];
			entity.Apellido = (Convert.IsDBNull(dataRow["Apellido"]))?string.Empty:(System.String)dataRow["Apellido"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.NroDocumento = (Convert.IsDBNull(dataRow["NroDocumento"]))?string.Empty:(System.String)dataRow["NroDocumento"];
			entity.ViajeId = (Convert.IsDBNull(dataRow["ViajeID"]))?Guid.Empty:(System.Guid?)dataRow["ViajeID"];
			entity.Paquete = (Convert.IsDBNull(dataRow["Paquete"]))?string.Empty:(System.String)dataRow["Paquete"];
			entity.FacturaId = (Convert.IsDBNull(dataRow["FacturaID"]))?Guid.Empty:(System.Guid?)dataRow["FacturaID"];
			entity.ClienteId = (Convert.IsDBNull(dataRow["ClienteID"]))?Guid.Empty:(System.Guid?)dataRow["ClienteID"];
			entity.TipoCliente = (Convert.IsDBNull(dataRow["TipoCliente"]))?(int)0:(System.Int32?)dataRow["TipoCliente"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region ReservaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Reserva"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaFilterBuilder : SqlFilterBuilder<ReservaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaFilterBuilder class.
		/// </summary>
		public ReservaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ReservaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ReservaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ReservaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ReservaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ReservaFilterBuilder

	#region ReservaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Reserva"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ReservaParameterBuilder : ParameterizedSqlFilterBuilder<ReservaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaParameterBuilder class.
		/// </summary>
		public ReservaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the ReservaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ReservaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ReservaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ReservaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ReservaParameterBuilder
	
	#region ReservaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Reserva"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class ReservaSortBuilder : SqlSortBuilder<ReservaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ReservaSqlSortBuilder class.
		/// </summary>
		public ReservaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion ReservaSortBuilder

} // end namespace
