using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MAT.MVC.Models;
using MAT.Entities;

namespace MAT.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IInfopathService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IInfopathService
    {
        [OperationContract]
        InfopathModel GetViaje(Guid viajeid);

        [OperationContract]
        Dictionary<string, string> GetAllViajes();


    }
}
