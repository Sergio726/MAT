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
	/// This class is the base class for any <see cref="VLocalidadProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract class VLocalidadProviderBaseCore : EntityViewProviderBase<VLocalidad>
	{
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions
		
		/*
		///<summary>
		/// Fill an VList&lt;VLocalidad&gt; From a DataSet
		///</summary>
		/// <param name="dataSet">the DataSet</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList&lt;VLocalidad&gt;"/></returns>
		protected static VList&lt;VLocalidad&gt; Fill(DataSet dataSet, VList<VLocalidad> rows, int start, int pagelen)
		{
			if (dataSet.Tables.Count == 1)
			{
				return Fill(dataSet.Tables[0], rows, start, pagelen);
			}
			else
			{
				return new VList<VLocalidad>();
			}	
		}
		
		
		///<summary>
		/// Fill an VList&lt;VLocalidad&gt; From a DataTable
		///</summary>
		/// <param name="dataTable">the DataTable that hold the data.</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pagelen">number of row.</param>
		///<returns><see chref="VList<VLocalidad>"/></returns>
		protected static VList&lt;VLocalidad&gt; Fill(DataTable dataTable, VList<VLocalidad> rows, int start, int pagelen)
		{
			int recordnum = 0;
			
			System.Collections.IEnumerator dataRows =  dataTable.Rows.GetEnumerator();
			
			while (dataRows.MoveNext() && (pagelen != 0))
			{
				if(recordnum >= start)
				{
					DataRow row = (DataRow)dataRows.Current;
				
					VLocalidad c = new VLocalidad();
					c.Id = (Convert.IsDBNull(row["ID"]))?(int)0:(System.Int32)row["ID"];
					c.Nombre = (Convert.IsDBNull(row["Nombre"]))?string.Empty:(System.String)row["Nombre"];
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
		/// Fill an <see cref="VList&lt;VLocalidad&gt;"/> From a DataReader.
		///</summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Start row</param>
		/// <param name="pageLength">number of row.</param>
		///<returns>a <see cref="VList&lt;VLocalidad&gt;"/></returns>
		protected VList<VLocalidad> Fill(IDataReader reader, VList<VLocalidad> rows, int start, int pageLength)
		{
			int recordnum = 0;
			while (reader.Read() && (pageLength != 0))
			{
				if(recordnum >= start)
				{
					VLocalidad entity = null;
					if (DataRepository.Provider.UseEntityFactory)
					{
						entity = EntityManager.CreateViewEntity<VLocalidad>("VLocalidad",  DataRepository.Provider.EntityCreationalFactoryType); 
					}
					else
					{
						entity = new VLocalidad();
					}
					
					entity.SuppressEntityEvents = true;

					entity.Id = (System.Int32)reader[((int)VLocalidadColumn.Id)];
					//entity.Id = (Convert.IsDBNull(reader["ID"]))?(int)0:(System.Int32)reader["ID"];
					entity.Nombre = (System.String)reader[((int)VLocalidadColumn.Nombre)];
					//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
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
		/// Refreshes the <see cref="VLocalidad"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="VLocalidad"/> object to refresh.</param>
		protected void RefreshEntity(IDataReader reader, VLocalidad entity)
		{
			reader.Read();
			entity.Id = (System.Int32)reader[((int)VLocalidadColumn.Id)];
			//entity.Id = (Convert.IsDBNull(reader["ID"]))?(int)0:(System.Int32)reader["ID"];
			entity.Nombre = (System.String)reader[((int)VLocalidadColumn.Nombre)];
			//entity.Nombre = (Convert.IsDBNull(reader["Nombre"]))?string.Empty:(System.String)reader["Nombre"];
			reader.Close();
	
			entity.AcceptChanges();
		}
		
		/*
		/// <summary>
		/// Refreshes the <see cref="VLocalidad"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="VLocalidad"/> object.</param>
		protected static void RefreshEntity(DataSet dataSet, VLocalidad entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (Convert.IsDBNull(dataRow["ID"]))?(int)0:(System.Int32)dataRow["ID"];
			entity.Nombre = (Convert.IsDBNull(dataRow["Nombre"]))?string.Empty:(System.String)dataRow["Nombre"];
			entity.AcceptChanges();
		}
		*/
			
		#endregion Helper Functions
	}//end class

	#region VLocalidadFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VLocalidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VLocalidadFilterBuilder : SqlFilterBuilder<VLocalidadColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VLocalidadFilterBuilder class.
		/// </summary>
		public VLocalidadFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VLocalidadFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VLocalidadFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VLocalidadFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VLocalidadFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VLocalidadFilterBuilder

	#region VLocalidadParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VLocalidad"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class VLocalidadParameterBuilder : ParameterizedSqlFilterBuilder<VLocalidadColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VLocalidadParameterBuilder class.
		/// </summary>
		public VLocalidadParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the VLocalidadParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public VLocalidadParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the VLocalidadParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public VLocalidadParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion VLocalidadParameterBuilder
	
	#region VLocalidadSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="VLocalidad"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class VLocalidadSortBuilder : SqlSortBuilder<VLocalidadColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the VLocalidadSqlSortBuilder class.
		/// </summary>
		public VLocalidadSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion VLocalidadSortBuilder

} // end namespace
