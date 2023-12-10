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
	/// This class is the base class for any <see cref="DatosPaqueteProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class DatosPaqueteProviderBaseCore : EntityViewProviderBase<DatosPaquete>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;DatosPaquete&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;DatosPaquete&gt;"/></returns>
		protected static VList&lt;DatosPaquete&gt; Fill(DataSet dataSet, VList<DatosPaquete> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<DatosPaquete>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;DatosPaquete&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<DatosPaquete>"/></returns>
		protected static VList&lt;DatosPaquete&gt; Fill(DataTable dataTable, VList<DatosPaquete> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					DatosPaquete c = new DatosPaquete();
					c.ViajeId = (Convert.IsDBNull(row["ViajeID"]))?Guid.Empty:(System.Guid)row["ViajeID"];
					c.ViajeOrigen = (Convert.IsDBNull(row["ViajeOrigen"]))?string.Empty:(System.String)row["ViajeOrigen"];
					c.ViajeFechaSalida = (Convert.IsDBNull(row["ViajeFechaSalida"]))?DateTime.MinValue:(System.DateTime?)row["ViajeFechaSalida"];
					c.ViajeHoraSalida = (Convert.IsDBNull(row["ViajeHoraSalida"]))?string.Empty:(System.String)row["ViajeHoraSalida"];
					c.ViajePaisOrigen = (Convert.IsDBNull(row["ViajePaisOrigen"]))?string.Empty:(System.String)row["ViajePaisOrigen"];
					c.ViajePaisDestino = (Convert.IsDBNull(row["ViajePaisDestino"]))?string.Empty:(System.String)row["ViajePaisDestino"];
					c.ViajePaso = (Convert.IsDBNull(row["ViajePaso"]))?string.Empty:(System.String)row["ViajePaso"];
					c.ViajeMedio = (Convert.IsDBNull(row["ViajeMedio"]))?string.Empty:(System.String)row["ViajeMedio"];
					c.PaqueteId = (Convert.IsDBNull(row["PaqueteID"]))?Guid.Empty:(System.Guid)row["PaqueteID"];
					c.Descripcion = (Convert.IsDBNull(row["Descripcion"]))?string.Empty:(System.String)row["Descripcion"];
					c.Precio = (Convert.IsDBNull(row["Precio"]))?0.0f:(System.Double?)row["Precio"];
					c.Moneda = (Convert.IsDBNull(row["Moneda"]))?(int)0:(System.Int32?)row["Moneda"];
					c.Iva = (Convert.IsDBNull(row["Iva"]))?string.Empty:(System.String)row["Iva"];
					c.Alicuota = (Convert.IsDBNull(row["Alicuota"]))?string.Empty:(System.String)row["Alicuota"];
					c.Temporada = (Convert.IsDBNull(row["Temporada"]))?(int)0:(System.Int32?)row["Temporada"];
					c.Cotizacion = (Convert.IsDBNull(row["Cotizacion"]))?0.0f:(System.Double?)row["Cotizacion"];
					c.Codigo = (Convert.IsDBNull(row["Codigo"]))?string.Empty:(System.String)row["Codigo"];
					c.DestinoId = (Convert.IsDBNull(row["DestinoID"]))?(int)0:(System.Int32?)row["DestinoID"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
					c.DescripcionServicio = (Convert.IsDBNull(row["DescripcionServicio"]))?string.Empty:(System.String)row["DescripcionServicio"];
					c.PrecioServicio = (Convert.IsDBNull(row["PrecioServicio"]))?0.0f:(System.Double?)row["PrecioServicio"];
					c.MonedaServicio = (Convert.IsDBNull(row["MonedaServicio"]))?string.Empty:(System.String)row["MonedaServicio"];
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
		/// Fill an <see cref="VList&lt;DatosPaquete&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;DatosPaquete&gt;"/></returns>
		protected VList<DatosPaquete> Fill(IDataReader reader, VList<DatosPaquete> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					DatosPaquete entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<DatosPaquete>("DatosPaquete",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new DatosPaquete();
					}
					
					entity.SuppressEntityEvents = true;

					entity.ViajeId = (System.Guid)reader[((int)DatosPaqueteColumn.ViajeId)];
					//entity.ViajeId = (Convert.IsDBNull(reader["ViajeID"]))?Guid.Empty:(System.Guid)reader["ViajeID"];
					entity.ViajeOrigen = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajeOrigen)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajeOrigen)];
					//entity.ViajeOrigen = (Convert.IsDBNull(reader["ViajeOrigen"]))?string.Empty:(System.String)reader["ViajeOrigen"];
					entity.ViajeFechaSalida = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajeFechaSalida)))?null:(System.DateTime?)reader[((int)DatosPaqueteColumn.ViajeFechaSalida)];
					//entity.ViajeFechaSalida = (Convert.IsDBNull(reader["ViajeFechaSalida"]))?DateTime.MinValue:(System.DateTime?)reader["ViajeFechaSalida"];
					entity.ViajeHoraSalida = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajeHoraSalida)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajeHoraSalida)];
					//entity.ViajeHoraSalida = (Convert.IsDBNull(reader["ViajeHoraSalida"]))?string.Empty:(System.String)reader["ViajeHoraSalida"];
					entity.ViajePaisOrigen = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajePaisOrigen)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajePaisOrigen)];
					//entity.ViajePaisOrigen = (Convert.IsDBNull(reader["ViajePaisOrigen"]))?string.Empty:(System.String)reader["ViajePaisOrigen"];
					entity.ViajePaisDestino = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajePaisDestino)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajePaisDestino)];
					//entity.ViajePaisDestino = (Convert.IsDBNull(reader["ViajePaisDestino"]))?string.Empty:(System.String)reader["ViajePaisDestino"];
					entity.ViajePaso = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajePaso)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajePaso)];
					//entity.ViajePaso = (Convert.IsDBNull(reader["ViajePaso"]))?string.Empty:(System.String)reader["ViajePaso"];
					entity.ViajeMedio = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajeMedio)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajeMedio)];
					//entity.ViajeMedio = (Convert.IsDBNull(reader["ViajeMedio"]))?string.Empty:(System.String)reader["ViajeMedio"];
					entity.PaqueteId = (System.Guid)reader[((int)DatosPaqueteColumn.PaqueteId)];
					//entity.PaqueteId = (Convert.IsDBNull(reader["PaqueteID"]))?Guid.Empty:(System.Guid)reader["PaqueteID"];
					entity.Descripcion = (reader.IsDBNull(((int)DatosPaqueteColumn.Descripcion)))?null:(System.String)reader[((int)DatosPaqueteColumn.Descripcion)];
					//entity.Descripcion = (Convert.IsDBNull(reader["Descripcion"]))?string.Empty:(System.String)reader["Descripcion"];
					entity.Precio = (reader.IsDBNull(((int)DatosPaqueteColumn.Precio)))?null:(System.Double?)reader[((int)DatosPaqueteColumn.Precio)];
					//entity.Precio = (Convert.IsDBNull(reader["Precio"]))?0.0f:(System.Double?)reader["Precio"];
					entity.Moneda = (reader.IsDBNull(((int)DatosPaqueteColumn.Moneda)))?null:(System.Int32?)reader[((int)DatosPaqueteColumn.Moneda)];
					//entity.Moneda = (Convert.IsDBNull(reader["Moneda"]))?(int)0:(System.Int32?)reader["Moneda"];
					entity.Iva = (reader.IsDBNull(((int)DatosPaqueteColumn.Iva)))?null:(System.String)reader[((int)DatosPaqueteColumn.Iva)];
					//entity.Iva = (Convert.IsDBNull(reader["Iva"]))?string.Empty:(System.String)reader["Iva"];
					entity.Alicuota = (reader.IsDBNull(((int)DatosPaqueteColumn.Alicuota)))?null:(System.String)reader[((int)DatosPaqueteColumn.Alicuota)];
					//entity.Alicuota = (Convert.IsDBNull(reader["Alicuota"]))?string.Empty:(System.String)reader["Alicuota"];
					entity.Temporada = (reader.IsDBNull(((int)DatosPaqueteColumn.Temporada)))?null:(System.Int32?)reader[((int)DatosPaqueteColumn.Temporada)];
					//entity.Temporada = (Convert.IsDBNull(reader["Temporada"]))?(int)0:(System.Int32?)reader["Temporada"];
					entity.Cotizacion = (reader.IsDBNull(((int)DatosPaqueteColumn.Cotizacion)))?null:(System.Double?)reader[((int)DatosPaqueteColumn.Cotizacion)];
					//entity.Cotizacion = (Convert.IsDBNull(reader["Cotizacion"]))?0.0f:(System.Double?)reader["Cotizacion"];
					entity.Codigo = (reader.IsDBNull(((int)DatosPaqueteColumn.Codigo)))?null:(System.String)reader[((int)DatosPaqueteColumn.Codigo)];
					//entity.Codigo = (Convert.IsDBNull(reader["Codigo"]))?string.Empty:(System.String)reader["Codigo"];
					entity.DestinoId = (reader.IsDBNull(((int)DatosPaqueteColumn.DestinoId)))?null:(System.Int32?)reader[((int)DatosPaqueteColumn.DestinoId)];
					//entity.DestinoId = (Convert.IsDBNull(reader["DestinoID"]))?(int)0:(System.Int32?)reader["DestinoID"];
					entity.Nombre = (System.String)reader[((int)DatosPaqueteColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
					entity.DescripcionServicio = (reader.IsDBNull(((int)DatosPaqueteColumn.DescripcionServicio)))?null:(System.String)reader[((int)DatosPaqueteColumn.DescripcionServicio)];
					//entity.DescripcionServicio = (Convert.IsDBNull(reader["DescripcionServicio"]))?string.Empty:(System.String)reader["DescripcionServicio"];
					entity.PrecioServicio = (reader.IsDBNull(((int)DatosPaqueteColumn.PrecioServicio)))?null:(System.Double?)reader[((int)DatosPaqueteColumn.PrecioServicio)];
					//entity.PrecioServicio = (Convert.IsDBNull(reader["PrecioServicio"]))?0.0f:(System.Double?)reader["PrecioServicio"];
					entity.MonedaServicio = (reader.IsDBNull(((int)DatosPaqueteColumn.MonedaServicio)))?null:(System.String)reader[((int)DatosPaqueteColumn.MonedaServicio)];
					//entity.MonedaServicio = (Convert.IsDBNull(reader["MonedaServicio"]))?string.Empty:(System.String)reader["MonedaServicio"];
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
		/// Refreshes the <see cref="DatosPaquete"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="DatosPaquete"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, DatosPaquete entity)
		{
			reader.Read();
			entity.ViajeId = (System.Guid)reader[((int)DatosPaqueteColumn.ViajeId)];
			//entity.ViajeId = (Convert.IsDBNull(reader["ViajeID"]))?Guid.Empty:(System.Guid)reader["ViajeID"];
			entity.ViajeOrigen = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajeOrigen)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajeOrigen)];
			//entity.ViajeOrigen = (Convert.IsDBNull(reader["ViajeOrigen"]))?string.Empty:(System.String)reader["ViajeOrigen"];
			entity.ViajeFechaSalida = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajeFechaSalida)))?null:(System.DateTime?)reader[((int)DatosPaqueteColumn.ViajeFechaSalida)];
			//entity.ViajeFechaSalida = (Convert.IsDBNull(reader["ViajeFechaSalida"]))?DateTime.MinValue:(System.DateTime?)reader["ViajeFechaSalida"];
			entity.ViajeHoraSalida = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajeHoraSalida)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajeHoraSalida)];
			//entity.ViajeHoraSalida = (Convert.IsDBNull(reader["ViajeHoraSalida"]))?string.Empty:(System.String)reader["ViajeHoraSalida"];
			entity.ViajePaisOrigen = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajePaisOrigen)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajePaisOrigen)];
			//entity.ViajePaisOrigen = (Convert.IsDBNull(reader["ViajePaisOrigen"]))?string.Empty:(System.String)reader["ViajePaisOrigen"];
			entity.ViajePaisDestino = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajePaisDestino)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajePaisDestino)];
			//entity.ViajePaisDestino = (Convert.IsDBNull(reader["ViajePaisDestino"]))?string.Empty:(System.String)reader["ViajePaisDestino"];
			entity.ViajePaso = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajePaso)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajePaso)];
			//entity.ViajePaso = (Convert.IsDBNull(reader["ViajePaso"]))?string.Empty:(System.String)reader["ViajePaso"];
			entity.ViajeMedio = (reader.IsDBNull(((int)DatosPaqueteColumn.ViajeMedio)))?null:(System.String)reader[((int)DatosPaqueteColumn.ViajeMedio)];
			//entity.ViajeMedio = (Convert.IsDBNull(reader["ViajeMedio"]))?string.Empty:(System.String)reader["ViajeMedio"];
			entity.PaqueteId = (System.Guid)reader[((int)DatosPaqueteColumn.PaqueteId)];
			//entity.PaqueteId = (Convert.IsDBNull(reader["PaqueteID"]))?Guid.Empty:(System.Guid)reader["PaqueteID"];
			entity.Descripcion = (reader.IsDBNull(((int)DatosPaqueteColumn.Descripcion)))?null:(System.String)reader[((int)DatosPaqueteColumn.Descripcion)];
			//entity.Descripcion = (Convert.IsDBNull(reader["Descripcion"]))?string.Empty:(System.String)reader["Descripcion"];
			entity.Precio = (reader.IsDBNull(((int)DatosPaqueteColumn.Precio)))?null:(System.Double?)reader[((int)DatosPaqueteColumn.Precio)];
			//entity.Precio = (Convert.IsDBNull(reader["Precio"]))?0.0f:(System.Double?)reader["Precio"];
			entity.Moneda = (reader.IsDBNull(((int)DatosPaqueteColumn.Moneda)))?null:(System.Int32?)reader[((int)DatosPaqueteColumn.Moneda)];
			//entity.Moneda = (Convert.IsDBNull(reader["Moneda"]))?(int)0:(System.Int32?)reader["Moneda"];
			entity.Iva = (reader.IsDBNull(((int)DatosPaqueteColumn.Iva)))?null:(System.String)reader[((int)DatosPaqueteColumn.Iva)];
			//entity.Iva = (Convert.IsDBNull(reader["Iva"]))?string.Empty:(System.String)reader["Iva"];
			entity.Alicuota = (reader.IsDBNull(((int)DatosPaqueteColumn.Alicuota)))?null:(System.String)reader[((int)DatosPaqueteColumn.Alicuota)];
			//entity.Alicuota = (Convert.IsDBNull(reader["Alicuota"]))?string.Empty:(System.String)reader["Alicuota"];
			entity.Temporada = (reader.IsDBNull(((int)DatosPaqueteColumn.Temporada)))?null:(System.Int32?)reader[((int)DatosPaqueteColumn.Temporada)];
			//entity.Temporada = (Convert.IsDBNull(reader["Temporada"]))?(int)0:(System.Int32?)reader["Temporada"];
			entity.Cotizacion = (reader.IsDBNull(((int)DatosPaqueteColumn.Cotizacion)))?null:(System.Double?)reader[((int)DatosPaqueteColumn.Cotizacion)];
			//entity.Cotizacion = (Convert.IsDBNull(reader["Cotizacion"]))?0.0f:(System.Double?)reader["Cotizacion"];
			entity.Codigo = (reader.IsDBNull(((int)DatosPaqueteColumn.Codigo)))?null:(System.String)reader[((int)DatosPaqueteColumn.Codigo)];
			//entity.Codigo = (Convert.IsDBNull(reader["Codigo"]))?string.Empty:(System.String)reader["Codigo"];
			entity.DestinoId = (reader.IsDBNull(((int)DatosPaqueteColumn.DestinoId)))?null:(System.Int32?)reader[((int)DatosPaqueteColumn.DestinoId)];
			//entity.DestinoId = (Convert.IsDBNull(reader["DestinoID"]))?(int)0:(System.Int32?)reader["DestinoID"];
			entity.Nombre = (System.String)reader[((int)DatosPaqueteColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			entity.DescripcionServicio = (reader.IsDBNull(((int)DatosPaqueteColumn.DescripcionServicio)))?null:(System.String)reader[((int)DatosPaqueteColumn.DescripcionServicio)];
			//entity.DescripcionServicio = (Convert.IsDBNull(reader["DescripcionServicio"]))?string.Empty:(System.String)reader["DescripcionServicio"];
			entity.PrecioServicio = (reader.IsDBNull(((int)DatosPaqueteColumn.PrecioServicio)))?null:(System.Double?)reader[((int)DatosPaqueteColumn.PrecioServicio)];
			//entity.PrecioServicio = (Convert.IsDBNull(reader["PrecioServicio"]))?0.0f:(System.Double?)reader["PrecioServicio"];
			entity.MonedaServicio = (reader.IsDBNull(((int)DatosPaqueteColumn.MonedaServicio)))?null:(System.String)reader[((int)DatosPaqueteColumn.MonedaServicio)];
			//entity.MonedaServicio = (Convert.IsDBNull(reader["MonedaServicio"]))?string.Empty:(System.String)reader["MonedaServicio"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="DatosPaquete"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="DatosPaquete"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, DatosPaquete entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.ViajeId = (Convert.IsDBNull(dataRow["ViajeID"]))?Guid.Empty:(System.Guid)dataRow["ViajeID"];
			entity.ViajeOrigen = (Convert.IsDBNull(dataRow["ViajeOrigen"]))?string.Empty:(System.String)dataRow["ViajeOrigen"];
			entity.ViajeFechaSalida = (Convert.IsDBNull(dataRow["ViajeFechaSalida"]))?DateTime.MinValue:(System.DateTime?)dataRow["ViajeFechaSalida"];
			entity.ViajeHoraSalida = (Convert.IsDBNull(dataRow["ViajeHoraSalida"]))?string.Empty:(System.String)dataRow["ViajeHoraSalida"];
			entity.ViajePaisOrigen = (Convert.IsDBNull(dataRow["ViajePaisOrigen"]))?string.Empty:(System.String)dataRow["ViajePaisOrigen"];
			entity.ViajePaisDestino = (Convert.IsDBNull(dataRow["ViajePaisDestino"]))?string.Empty:(System.String)dataRow["ViajePaisDestino"];
			entity.ViajePaso = (Convert.IsDBNull(dataRow["ViajePaso"]))?string.Empty:(System.String)dataRow["ViajePaso"];
			entity.ViajeMedio = (Convert.IsDBNull(dataRow["ViajeMedio"]))?string.Empty:(System.String)dataRow["ViajeMedio"];
			entity.PaqueteId = (Convert.IsDBNull(dataRow["PaqueteID"]))?Guid.Empty:(System.Guid)dataRow["PaqueteID"];
			entity.Descripcion = (Convert.IsDBNull(dataRow["Descripcion"]))?string.Empty:(System.String)dataRow["Descripcion"];
			entity.Precio = (Convert.IsDBNull(dataRow["Precio"]))?0.0f:(System.Double?)dataRow["Precio"];
			entity.Moneda = (Convert.IsDBNull(dataRow["Moneda"]))?(int)0:(System.Int32?)dataRow["Moneda"];
			entity.Iva = (Convert.IsDBNull(dataRow["Iva"]))?string.Empty:(System.String)dataRow["Iva"];
			entity.Alicuota = (Convert.IsDBNull(dataRow["Alicuota"]))?string.Empty:(System.String)dataRow["Alicuota"];
			entity.Temporada = (Convert.IsDBNull(dataRow["Temporada"]))?(int)0:(System.Int32?)dataRow["Temporada"];
			entity.Cotizacion = (Convert.IsDBNull(dataRow["Cotizacion"]))?0.0f:(System.Double?)dataRow["Cotizacion"];
			entity.Codigo = (Convert.IsDBNull(dataRow["Codigo"]))?string.Empty:(System.String)dataRow["Codigo"];
			entity.DestinoId = (Convert.IsDBNull(dataRow["DestinoID"]))?(int)0:(System.Int32?)dataRow["DestinoID"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.DescripcionServicio = (Convert.IsDBNull(dataRow["DescripcionServicio"]))?string.Empty:(System.String)dataRow["DescripcionServicio"];
			entity.PrecioServicio = (Convert.IsDBNull(dataRow["PrecioServicio"]))?0.0f:(System.Double?)dataRow["PrecioServicio"];
			entity.MonedaServicio = (Convert.IsDBNull(dataRow["MonedaServicio"]))?string.Empty:(System.String)dataRow["MonedaServicio"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region DatosPaqueteFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DatosPaquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DatosPaqueteFilterBuilder : SqlFilterBuilder<DatosPaqueteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteFilterBuilder class.
		/// </summary>
		public DatosPaqueteFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DatosPaqueteFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DatosPaqueteFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DatosPaqueteFilterBuilder

	#region DatosPaqueteParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DatosPaquete"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class DatosPaqueteParameterBuilder : ParameterizedSqlFilterBuilder<DatosPaqueteColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteParameterBuilder class.
		/// </summary>
		public DatosPaqueteParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public DatosPaqueteParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public DatosPaqueteParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion DatosPaqueteParameterBuilder
	
	#region DatosPaqueteSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="DatosPaquete"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class DatosPaqueteSortBuilder : SqlSortBuilder<DatosPaqueteColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DatosPaqueteSqlSortBuilder class.
		/// </summary>
		public DatosPaqueteSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion DatosPaqueteSortBuilder

} // end namespace
