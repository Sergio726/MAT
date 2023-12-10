#region Using directives

using System;

#endregion

namespace MAT.Entities
{	
	///<summary>
	/// An object representation of the 'Destino' table. [No description found the database]	
	///</summary>
	/// <remarks>
	/// This file is generated once and will never be overwritten.
	/// </remarks>	
	[Serializable]
	[CLSCompliant(true)]
	public partial class Destino : DestinoBase
	{		
		#region Constructors

		///<summary>
		/// Creates a new <see cref="Destino"/> instance.
		///</summary>
        public Destino() : base() { this.DestinoId = Guid.NewGuid(); }	
		
		#endregion
	}
}
