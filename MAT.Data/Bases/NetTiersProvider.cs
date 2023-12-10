#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using MAT.Entities;

#endregion

namespace MAT.Data.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current ViajeHotelProviderBase instance.
		///</summary>
		public virtual ViajeHotelProviderBase ViajeHotelProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PaqueteServicioProviderBase instance.
		///</summary>
		public virtual PaqueteServicioProviderBase PaqueteServicioProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PasajeroProviderBase instance.
		///</summary>
		public virtual PasajeroProviderBase PasajeroProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PasajeroMenorProviderBase instance.
		///</summary>
		public virtual PasajeroMenorProviderBase PasajeroMenorProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PersonaProviderBase instance.
		///</summary>
		public virtual PersonaProviderBase PersonaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PlanillaProviderBase instance.
		///</summary>
		public virtual PlanillaProviderBase PlanillaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PlanillaHabitacionItemProviderBase instance.
		///</summary>
		public virtual PlanillaHabitacionItemProviderBase PlanillaHabitacionItemProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PlanillaServicioItemProviderBase instance.
		///</summary>
		public virtual PlanillaServicioItemProviderBase PlanillaServicioItemProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PaqueteExcursionProviderBase instance.
		///</summary>
		public virtual PaqueteExcursionProviderBase PaqueteExcursionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PrecioProviderBase instance.
		///</summary>
		public virtual PrecioProviderBase PrecioProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PrecioServicioProviderBase instance.
		///</summary>
		public virtual PrecioServicioProviderBase PrecioServicioProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ProveedorProviderBase instance.
		///</summary>
		public virtual ProveedorProviderBase ProveedorProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ProvinciaProviderBase instance.
		///</summary>
		public virtual ProvinciaProviderBase ProvinciaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current TransporteProviderBase instance.
		///</summary>
		public virtual TransporteProviderBase TransporteProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ReservaHabitacionProviderBase instance.
		///</summary>
		public virtual ReservaHabitacionProviderBase ReservaHabitacionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ServicioProviderBase instance.
		///</summary>
		public virtual ServicioProviderBase ServicioProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current VendedorProviderBase instance.
		///</summary>
		public virtual VendedorProviderBase VendedorProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ViajeProviderBase instance.
		///</summary>
		public virtual ViajeProviderBase ViajeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PasajeProviderBase instance.
		///</summary>
		public virtual PasajeProviderBase PasajeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PaquetePrecioProviderBase instance.
		///</summary>
		public virtual PaquetePrecioProviderBase PaquetePrecioProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PrecioHabitacionProviderBase instance.
		///</summary>
		public virtual PrecioHabitacionProviderBase PrecioHabitacionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current VoucherProviderBase instance.
		///</summary>
		public virtual VoucherProviderBase VoucherProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current AdicionalProviderBase instance.
		///</summary>
		public virtual AdicionalProviderBase AdicionalProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PaqueteAdicionalProviderBase instance.
		///</summary>
		public virtual PaqueteAdicionalProviderBase PaqueteAdicionalProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current AuditFacturaProviderBase instance.
		///</summary>
		public virtual AuditFacturaProviderBase AuditFacturaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ButacaProviderBase instance.
		///</summary>
		public virtual ButacaProviderBase ButacaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CiudadProviderBase instance.
		///</summary>
		public virtual CiudadProviderBase CiudadProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ClienteProviderBase instance.
		///</summary>
		public virtual ClienteProviderBase ClienteProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CuentaProviderBase instance.
		///</summary>
		public virtual CuentaProviderBase CuentaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current CuentaCorrienteProviderBase instance.
		///</summary>
		public virtual CuentaCorrienteProviderBase CuentaCorrienteProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current DebitoProviderBase instance.
		///</summary>
		public virtual DebitoProviderBase DebitoProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current DepartamentoProviderBase instance.
		///</summary>
		public virtual DepartamentoProviderBase DepartamentoProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current EstadoPasajeProviderBase instance.
		///</summary>
		public virtual EstadoPasajeProviderBase EstadoPasajeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PaqueteProviderBase instance.
		///</summary>
		public virtual PaqueteProviderBase PaqueteProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ExcursionProviderBase instance.
		///</summary>
		public virtual ExcursionProviderBase ExcursionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HabitacionProviderBase instance.
		///</summary>
		public virtual HabitacionProviderBase HabitacionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HabitacionTipoProviderBase instance.
		///</summary>
		public virtual HabitacionTipoProviderBase HabitacionTipoProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HistorialProviderBase instance.
		///</summary>
		public virtual HistorialProviderBase HistorialProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current LocalidadProviderBase instance.
		///</summary>
		public virtual LocalidadProviderBase LocalidadProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current HotelProviderBase instance.
		///</summary>
		public virtual HotelProviderBase HotelProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PasajeAdicionalProviderBase instance.
		///</summary>
		public virtual PasajeAdicionalProviderBase PasajeAdicionalProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current NotaProviderBase instance.
		///</summary>
		public virtual NotaProviderBase NotaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PagoProviderBase instance.
		///</summary>
		public virtual PagoProviderBase PagoProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current FacturaProviderBase instance.
		///</summary>
		public virtual FacturaProviderBase FacturaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current MovimientoCuentaProviderBase instance.
		///</summary>
		public virtual MovimientoCuentaProviderBase MovimientoCuentaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PaisProviderBase instance.
		///</summary>
		public virtual PaisProviderBase PaisProvider{get {throw new NotImplementedException();}}
		
		
		///<summary>
		/// Current PasajeroViajeProviderBase instance.
		///</summary>
		public virtual PasajeroViajeProviderBase PasajeroViajeProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PersonaClienteProviderBase instance.
		///</summary>
		public virtual PersonaClienteProviderBase PersonaClienteProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PersonaPasajeroProviderBase instance.
		///</summary>
		public virtual PersonaPasajeroProviderBase PersonaPasajeroProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PersonaProveedorProviderBase instance.
		///</summary>
		public virtual PersonaProveedorProviderBase PersonaProveedorProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PersonaVendedorProviderBase instance.
		///</summary>
		public virtual PersonaVendedorProviderBase PersonaVendedorProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current ReservaProviderBase instance.
		///</summary>
		public virtual ReservaProviderBase ReservaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current VConsultaReservaHabitacionProviderBase instance.
		///</summary>
		public virtual VConsultaReservaHabitacionProviderBase VConsultaReservaHabitacionProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current VLocalidadProviderBase instance.
		///</summary>
		public virtual VLocalidadProviderBase VLocalidadProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current VPersonaProviderBase instance.
		///</summary>
		public virtual VPersonaProviderBase VPersonaProvider{get {throw new NotImplementedException();}}
		
	}
}
