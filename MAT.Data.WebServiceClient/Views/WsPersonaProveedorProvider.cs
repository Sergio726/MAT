#region Using directives

using System;
using System.Data;
using System.Collections;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel;
using MAT.Data;
using MAT.Entities;

#endregion

namespace MAT.Data.WebServiceClient
{
	///<summary>
	/// This class is the WebServiceClient Data Access Logic Component implementation for the <see cref="PersonaProveedor"/> entity.
	///</summary>
	[DataObject]
	[CLSCompliant(true)]
	public partial class WsPersonaProveedorProvider: WsPersonaProveedorProviderBase
	{		
		/// <summary>
		/// Creates a new <see cref="WsPersonaProveedorProvider"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		/// <param name="url">The url to the nettiers webservice.</param>
		public WsPersonaProveedorProvider(string url): base(url){}
	}
}
