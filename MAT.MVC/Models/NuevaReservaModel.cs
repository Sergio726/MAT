using AutoMapper;
using MAT.Entities;
using MAT.Enums.SharedModels;
using MAT.MVC.Controllers.NuevaReserva;
using MAT.MVC.Integration;
using MAT.MVC.Integration.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MAT.MVC.Models
{
    public class NuevaReservaModel
    {
        private BackendAPI _backendAPI;
        public Guid ViajeId { get; set; }
        public List<ReservaStandard> Reservas { get; set; }
        public DetalleViaje DetalleViaje { get; set; }

        public DatosReserva Reserva { get; set; }

        public Pago Pago { get; set; }


        public NuevaReservaModel(Guid viajeId)
        {
            _backendAPI = new BackendAPI();
            ViajeId = viajeId;
        }        
        public static async Task<NuevaReservaModel> CreateAsync(Guid viajeId)
        {
            var model = new NuevaReservaModel(viajeId);
            model.Reservas = await model.GetReservas();                        
            model.DetalleViaje = await model.GetDetalleViaje();

            return model;
        }

        public async Task<List<ReservaStandard>> GetReservas()
        {
            try
            {
                var resultPasajeDto = await _backendAPI.GetListOfPasajesByViajeID(ViajeId.ToString());
                return Mapper.Map<List<ReservaStandard>>(resultPasajeDto);
            }
            catch (Exception)
            {
                // Fallback a base de datos cuando el servicio API no está disponible
                return ReservaMethod.GetListOfPasajesByViajeID(ViajeId.ToString());
            }
        }

        public async Task<DetalleViaje> GetDetalleViaje()
        {
            try
            {
                var resultDetalleViaje = await _backendAPI.GetDetalleViajeAsync(ViajeId.ToString());
                return Mapper.Map<DetalleViaje>(resultDetalleViaje);
            }
            catch (Exception)
            {
                // Fallback a base de datos cuando el servicio API no está disponible
                return await Task.FromResult(ViajeMethod.GetDetalleViajeModel(ViajeId));
            }
        }

    }
}