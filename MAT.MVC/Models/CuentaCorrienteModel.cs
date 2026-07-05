using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class CuentaCorrienteModel
    {
        //private CuentaCorrienteService ccService = new CuentaCorrienteService();
        //private ClienteService clienteService = new ClienteService();

        //public List<MAT.Entities.CuentaCorriente> MovimientosCC { get; set; }
        //public MAT.Entities.Cliente Cliente { get; set; }
        //public double TotalCC { get; set; }
        //public Entities.Persona Persona { get; set; }
        //public CuentaCorrienteModel(Guid clienteid)
        //{
        //    List<Entities.CuentaCorriente> _movimientoscc = new List<Entities.CuentaCorriente>();
        //    CuentaCorrienteService ccService = new CuentaCorrienteService();
        //    _movimientoscc = ccService.GetByClienteId(clienteid).OrderByDescending(cc => cc.Fecha).ToList();
        //    MovimientosCC = _movimientoscc;
        //    Persona = new PersonaService().GetByPersonaId(clienteid);
        //    TotalCC = CalcularTotalCC(_movimientoscc);
        //}

        //private double CalcularTotalCC(List<Entities.CuentaCorriente> _movs)
        //{
        //    double _total = 0;
        //    foreach (var item in _movs)
        //    {
        //        _total += item.Monto.Value;
        //    }
        //    return _total;
        //}

    }
}