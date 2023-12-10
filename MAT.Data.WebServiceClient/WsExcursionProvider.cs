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
	/// This class is the WebServiceClient Data Access Logic Component implementation for the <see cref="Excursion"/> entity.
	///</summary>
	[DataObject]
	[CLSCompliant(true)]
	public partial class WsExcursionProvider: WsExcursionProviderBase
	{		
		/// <summary>
		/// Creates a new <see cref="WsExcursionProvider"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		/// <param name="url">The url to the nettiers webservice.</param>
		public WsExcursionProvider(string url): base(url){}
	}
}
