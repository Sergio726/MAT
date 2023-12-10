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
	/// This class is the base class for any <see cref="VConsultaReservaHabitacionProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class VConsultaReservaHabitacionProviderBaseCore : EntityViewProviderBase<VConsultaReservaHabitacion>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;VConsultaReservaHabitacion&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;VConsultaReservaHabitacion&gt;"/></returns>
		protected static VList&lt;VConsultaReservaHabitacion&gt; Fill(DataSet dataSet, VList<VConsultaReservaHabitacion> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<VConsultaReservaHabitacion>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;VConsultaReservaHabitacion&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<VConsultaReservaHabitacion>"/></returns>
		protected static VList&lt;VConsultaReservaHabitacion&gt; Fill(DataTable dataTable, VList<VConsultaReservaHabitacion> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					VConsultaReservaHabitacion c = new VConsultaReservaHabitacion();
					c.ReservaHabitacionId = (Convert.IsDBNull(row["ReservaHabitacionID"]))?Guid.Empty:(System.Guid)row["ReservaHabitacionID"];
					c.HotelId = (Convert.IsDBNull(row["HotelID"]))?Guid.Empty:(System.Guid?)row["HotelID"];
					c.Expiro = (Convert.IsDBNull(row["Expiro"]))?false:(System.Boolean?)row["Expiro"];
					c.HabitacionId = (Convert.IsDBNull(row["HabitacionID"]))?Guid.Empty:(System.Guid?)row["HabitacionID"];
					c.Capacidad = (Convert.IsDBNull(row["Capacidad"]))?(int)0:(System.Int32?)row["Capacidad"];
					c.Ocupacion = (Convert.IsDBNull(row["Ocupacion"]))?(int)0:(System.Int32?)row["Ocupacion"];
					c.Estado = (Convert.IsDBNull(row["Estado"]))?(int)0:(System.Int32?)row["Estado"];
					c.Desde = (Convert.IsDBNull(row["Desde"]))?DateTime.MinValue:(System.DateTime?)row["Desde"];
					c.Hasta = (Convert.IsDBNull(row["Hasta"]))?DateTime.MinValue:(System.DateTime?)row["Hasta"];
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
		/// Fill an <see cref="VList&lt;VConsultaReservaHabitacion&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;VConsultaReservaHabitacion&gt;"/></returns>
		protected VList<VConsultaReservaHabitacion> Fill(IDataReader reader, VList<VConsultaReservaHabitacion> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					VConsultaReservaHabitacion entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<VConsultaReservaHabitacion>("VConsultaReservaHabitacion",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new VConsultaReservaHabitacion();
					}
					
					entity.SuppressEntityEvents = true;

					entity.ReservaHabitacionId = (System.Guid)reader[((int)VConsultaReservaHabitacionColumn.ReservaHabitacionId)];
					//entity.ReservaHabitacionId = (Convert.IsDBNull(reader["ReservaHabitacionID"]))?Guid.Empty:(System.Guid)reader["ReservaHabitacionID"];
					entity.HotelId = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.HotelId)))?null:(System.Guid?)reader[((int)VConsultaReservaHabitacionColumn.HotelId)];
					//entity.HotelId = (Convert.IsDBNull(reader["HotelID"]))?Guid.Empty:(System.Guid?)reader["HotelID"];
					entity.Expiro = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Expiro)))?null:(System.Boolean?)reader[((int)VConsultaReservaHabitacionColumn.Expiro)];
					//entity.Expiro = (Convert.IsDBNull(reader["Expiro"]))?false:(System.Boolean?)reader["Expiro"];
					entity.HabitacionId = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.HabitacionId)))?null:(System.Guid?)reader[((int)VConsultaReservaHabitacionColumn.HabitacionId)];
					//entity.HabitacionId = (Convert.IsDBNull(reader["HabitacionID"]))?Guid.Empty:(System.Guid?)reader["HabitacionID"];
					entity.Capacidad = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Capacidad)))?null:(System.Int32?)reader[((int)VConsultaReservaHabitacionColumn.Capacidad)];
					//entity.Capacidad = (Convert.IsDBNull(reader["Capacidad"]))?(int)0:(System.Int32?)reader["Capacidad"];
					entity.Ocupacion = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Ocupacion)))?null:(System.Int32?)reader[((int)VConsultaReservaHabitacionColumn.Ocupacion)];
					//entity.Ocupacion = (Convert.IsDBNull(reader["Ocupacion"]))?(int)0:(System.Int32?)reader["Ocupacion"];
					entity.Estado = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Estado)))?null:(System.Int32?)reader[((int)VConsultaReservaHabitacionColumn.Estado)];
					//entity.Estado = (Convert.IsDBNull(reader["Estado"]))?(int)0:(System.Int32?)reader["Estado"];
					entity.Desde = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Desde)))?null:(System.DateTime?)reader[((int)VConsultaReservaHabitacionColumn.Desde)];
					//entity.Desde = (Convert.IsDBNull(reader["Desde"]))?DateTime.MinValue:(System.DateTime?)reader["Desde"];
					entity.Hasta = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Hasta)))?null:(System.DateTime?)reader[((int)VConsultaReservaHabitacionColumn.Hasta)];
					//entity.Hasta = (Convert.IsDBNull(reader["Hasta"]))?DateTime.MinValue:(System.DateTime?)reader["Hasta"];
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
		/// Refreshes the <see cref="VConsultaReservaHabitacion"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="VConsultaReservaHabitacion"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, VConsultaReservaHabitacion entity)
		{
			reader.Read();
			entity.ReservaHabitacionId = (System.Guid)reader[((int)VConsultaReservaHabitacionColumn.ReservaHabitacionId)];
			//entity.ReservaHabitacionId = (Convert.IsDBNull(reader["ReservaHabitacionID"]))?Guid.Empty:(System.Guid)reader["ReservaHabitacionID"];
			entity.HotelId = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.HotelId)))?null:(System.Guid?)reader[((int)VConsultaReservaHabitacionColumn.HotelId)];
			//entity.HotelId = (Convert.IsDBNull(reader["HotelID"]))?Guid.Empty:(System.Guid?)reader["HotelID"];
			entity.Expiro = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Expiro)))?null:(System.Boolean?)reader[((int)VConsultaReservaHabitacionColumn.Expiro)];
			//entity.Expiro = (Convert.IsDBNull(reader["Expiro"]))?false:(System.Boolean?)reader["Expiro"];
			entity.HabitacionId = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.HabitacionId)))?null:(System.Guid?)reader[((int)VConsultaReservaHabitacionColumn.HabitacionId)];
			//entity.HabitacionId = (Convert.IsDBNull(reader["HabitacionID"]))?Guid.Empty:(System.Guid?)reader["HabitacionID"];
			entity.Capacidad = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Capacidad)))?null:(System.Int32?)reader[((int)VConsultaReservaHabitacionColumn.Capacidad)];
			//entity.Capacidad = (Convert.IsDBNull(reader["Capacidad"]))?(int)0:(System.Int32?)reader["Capacidad"];
			entity.Ocupacion = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Ocupacion)))?null:(System.Int32?)reader[((int)VConsultaReservaHabitacionColumn.Ocupacion)];
			//entity.Ocupacion = (Convert.IsDBNull(reader["Ocupacion"]))?(int)0:(System.Int32?)reader["Ocupacion"];
			entity.Estado = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Estado)))?null:(System.Int32?)reader[((int)VConsultaReservaHabitacionColumn.Estado)];
			//entity.Estado = (Convert.IsDBNull(reader["Estado"]))?(int)0:(System.Int32?)reader["Estado"];
			entity.Desde = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Desde)))?null:(System.DateTime?)reader[((int)VConsultaReservaHabitacionColumn.Desde)];
			//entity.Desde = (Convert.IsDBNull(reader["Desde"]))?DateTime.MinValue:(System.DateTime?)reader["Desde"];
			entity.Hasta = (reader.IsDBNull(((int)VConsultaReservaHabitacionColumn.Hasta)))?null:(System.DateTime?)reader[((int)VConsultaReservaHabitacionColumn.Hasta)];
			//entity.Hasta = (Convert.IsDBNull(reader["Hasta"]))?DateTime.MinValue:(System.DateTime?)reader["Hasta"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="VConsultaReservaHabitacion"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="VConsultaReservaHabitacion"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, VConsultaReservaHabitacion entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ReservaHabitacionId = (Convert.IsDBNull(dataRow["ReservaHabitacionID"]))?Guid.Empty:(System.Guid)dataRow["ReservaHabitacionID"];
			entity.HotelId = (Convert.IsDBNull(dataRow["HotelID"]))?Guid.Empty:(System.Guid?)dataRow["HotelID"];
			entity.Expiro = (Convert.IsDBNull(dataRow["Expiro"]))?false:(System.Boolean?)dataRow["Expiro"];
			entity.HabitacionId = (Convert.IsDBNull(dataRow["HabitacionID"]))?Guid.Empty:(System.Guid?)dataRow["HabitacionID"];
			entity.Capacidad = (Convert.IsDBNull(dataRow["Capacidad"]))?(int)0:(System.Int32?)dataRow["Capacidad"];
			entity.Ocupacion = (Convert.IsDBNull(dataRow["Ocupacion"]))?(int)0:(System.Int32?)dataRow["Ocupacion"];
			entity.Estado = (Convert.IsDBNull(dataRow["Estado"]))?(int)0:(System.Int32?)dataRow["Estado"];
			entity.Desde = (Convert.IsDBNull(dataRow["Desde"]))?DateTime.MinValue:(System.DateTime?)dataRow["Desde"];
			entity.Hasta = (Convert.IsDBNull(dataRow["Hasta"]))?DateTime.MinValue:(System.DateTime?)dataRow["Hasta"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region VConsultaReservaHabitacionFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VConsultaReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VConsultaReservaHabitacionFilterBuilder : SqlFilterBuilder<VConsultaReservaHabitacionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionFilterBuilder class.
		/// </summary>
		public VConsultaReservaHabitacionFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VConsultaReservaHabitacionFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VConsultaReservaHabitacionFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VConsultaReservaHabitacionFilterBuilder

	#region VConsultaReservaHabitacionParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VConsultaReservaHabitacion"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VConsultaReservaHabitacionParameterBuilder : ParameterizedSqlFilterBuilder<VConsultaReservaHabitacionColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionParameterBuilder class.
		/// </summary>
		public VConsultaReservaHabitacionParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VConsultaReservaHabitacionParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VConsultaReservaHabitacionParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VConsultaReservaHabitacionParameterBuilder
	
	#region VConsultaReservaHabitacionSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VConsultaReservaHabitacion"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VConsultaReservaHabitacionSortBuilder : SqlSortBuilder<VConsultaReservaHabitacionColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VConsultaReservaHabitacionSqlSortBuilder class.
		/// </summary>
		public VConsultaReservaHabitacionSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VConsultaReservaHabitacionSortBuilder

} // end namespace
