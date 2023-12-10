#region Using directives

using System;
using System.Data;
using System.Collections;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel;
using MAT.Entities;
using MAT.Data;

#endregion

namespace MAT.Data.WebServiceClient
{
	///<summary>
	/// This class is the WebServiceClient Data Access Logic Component implementation for the <see cref="Habitacion"/> entity.
	///</summary>
	[DataObject]
	[CLSCompliant(true)]
	public partial class WsHabitacionProvider: WsHabitacionProviderBase
	{		
		/// <summary>
		/// Creates a new <see cref="WsHabitacionProvider"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		/// <param name="url">The url to the nettiers webservice.</param>
		public WsHabitacionProvider(string url): base(url){}
	}
}
