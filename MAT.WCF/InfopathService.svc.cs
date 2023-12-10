using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MAT.MVC.Models;
using MAT.Services;
using MAT.Entities;

namespace MAT.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "InfopathService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione InfopathService.svc o InfopathService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class InfopathService : IInfopathService
    {
        public InfopathModel GetViaje(Guid viajeid)
        {
            InfopathModel viaje = new InfopathModel(viajeid);
            return viaje;
        }

        //public Dictionary<string,string> GetAllViajes()
        //{
        //    List<Viaje> _viajes = new ViajeService().GetAll().ToList();
        //    //List<PaqueteModel> _paquetes = new List<PaqueteModel>();
        //    Dictionary<string, string> _result = new Dictionary<string, string>();
        //    foreach (Viaje viaje in _viajes)
        //    {
        //        PaqueteModel _paquete = new PaqueteModel(viaje.ViajeId);
        //        string valor = string.Format("{0} - {1} - {2}", _paquete.Destino.Nombre, _paquete.Viaje.FechaSalida.Value.ToShortDateString(), _paquete.Viaje.HoraSalida);
        //        _result.Add(_paquete.Viaje.ViajeId.ToString(), _paquete.Destino.Nombre);
        //        //_paquetes.Add(new PaqueteModel(viaje.ViajeId));
        //    }
        //    return _result;

        //    return _viajes
        //}
    }
}
