using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
using MAT.Utilities;
using MAT.MVC.Infrastructure.Data;
using System.Text;
using MAT.Enums;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MAT.MVC.Models
{
    [DataContractAttribute]
    public class InfopathModel
    {
        ViajeService viajeServ;
        PasajeService pasajeServ;
        PasajeroService pasajeroServ;

        [DataMemberAttribute]
        public Viaje Viaje { get; set; }

        [DataMemberAttribute]
        public Paquete Paquete { get; set; }

        [DataMemberAttribute]
        public List<Servicio> Servicios { get; set; }

        [DataMemberAttribute]
        public Localidad Destino { get; set; }

        [DataMemberAttribute]
        public List<Pasajero> Pasajeros { get; set; }

        public InfopathModel(Guid viajeid)
        {
            viajeServ = new ViajeService();
            pasajeServ = new PasajeService();
            pasajeroServ = new PasajeroService();


            List<PaqueteServicio> listpaqueteservicio = new List<PaqueteServicio>();
            List<Servicio> _servicios = new List<Servicio>();
            try
            {
                viajeServ = new ViajeService();
                pasajeServ = new PasajeService();
                pasajeroServ = new PasajeroService();
                Viaje = viajeServ.GetByViajeId(viajeid);
                Paquete = PaqueteDataAccess.GetPaqueteById(Viaje.PaqueteId.Value);
                listpaqueteservicio = PaqueteDataAccess.GetPaqueteServiciosByPaqueteId(Paquete.PaqueteId);
                Servicios = new List<Servicio>();
                Destino = GeoDataAccess.GetLocalidadById(Paquete.DestinoId);
                foreach (PaqueteServicio ps in listpaqueteservicio)
                {
                    _servicios.Add(MaestrosDataAccess.GetServicioById(ps.ServicioId.Value));
                }
                Servicios = _servicios;
                List<Pasaje> _pasajes = pasajeServ.GetByViajeId(viajeid).Where(p => p.PasajeroId.HasValue).ToList();
                List<Pasajero> _pasajeros = new List<Pasajero>();
                foreach (Pasaje _pasaje in _pasajes)
                {
                    _pasajeros.Add(pasajeroServ.GetByPasajeroId(_pasaje.PasajeroId.Value));
                }
                Pasajeros = _pasajeros;

            }
            catch (Exception ex)
            {
                StringBuilder excepcion = new StringBuilder();
                excepcion.AppendLine(viajeid.ToString());
                excepcion.AppendLine(listpaqueteservicio.Count.ToString());
                excepcion.AppendLine(ex.Message);
                excepcion.AppendLine(ex.Source);
                excepcion.AppendLine(ex.StackTrace);
                HttpContext.Current.Response.Write(excepcion.ToString());
            }
        }
    }
}