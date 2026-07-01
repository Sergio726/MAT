using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
using MAT.MVC.Infrastructure.Data;
using System.Text;
using MAT.Enums;
using MAT.Data;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MAT.MVC.Models
{
    [DataContractAttribute]
    public class InfopathModel
    {
        ViajeService viajeServ;
        PaqueteService paqueteServ;
        PaqueteServicioService paqueteservicioServ;
        LocalidadService localidadServ;
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
            paqueteServ = new PaqueteService();
            paqueteservicioServ = new PaqueteServicioService();
            localidadServ = new LocalidadService();
            pasajeServ = new PasajeService();
            pasajeroServ = new PasajeroService();


            List<PaqueteServicio> listpaqueteservicio = new List<PaqueteServicio>();
            List<Servicio> _servicios = new List<Servicio>();
            try
            {
                viajeServ = new ViajeService();
                paqueteServ = new PaqueteService();
                paqueteservicioServ = new PaqueteServicioService();
                localidadServ = new LocalidadService();
                pasajeServ = new PasajeService();
                pasajeroServ = new PasajeroService();
                pasajeServ = new PasajeService();
                Viaje = viajeServ.GetByViajeId(viajeid);
                Paquete = paqueteServ.GetByPaqueteId(Viaje.PaqueteId.Value);
                listpaqueteservicio = paqueteservicioServ.GetByPaqueteId(Paquete.PaqueteId).ToList();
                Servicios = new List<Servicio>();
                Destino = localidadServ.GetById(Paquete.DestinoId);
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