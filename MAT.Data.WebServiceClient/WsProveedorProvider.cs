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
	/// This class is the WebServiceClient Data Access Logic Component implementation for the <see cref="Proveedor"/> entity.
	///</summary>
	[DataObject]
	[CLSCompliant(true)]
	public partial class WsProveedorProvider: WsProveedorProviderBase
	{		
		/// <summary>
		/// Creates a new <see cref="WsProveedorProvider"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		/// <param name="url">The url to the nettiers webservice.</param>
		public WsProveedorProvider(string url): base(url){}
	}
}
