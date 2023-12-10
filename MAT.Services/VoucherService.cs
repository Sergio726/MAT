
#region Using Directives
using System;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Data;

using MAT.Entities;
using MAT.Entities.Validation;

using MAT.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace MAT.Services
{		
	/// <summary>
	/// An component type implementation of the 'Voucher' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
#pragma warning disable CS3014 // Type or member cannot be marked as CLS-compliant because the assembly does not have a CLSCompliant attribute
    public partial class VoucherService : MAT.Services.VoucherServiceBase
#pragma warning restore CS3014 // Type or member cannot be marked as CLS-compliant because the assembly does not have a CLSCompliant attribute
    {
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the VoucherService class.
		/// </summary>
		public VoucherService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
