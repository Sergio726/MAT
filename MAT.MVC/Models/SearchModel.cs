using MAT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MAT.Utilities;

namespace MAT.MVC.Models
{
    public class SearchModel
    {
        #region Propiedades Privadas
        private GridView _grid;
        #endregion

        #region Propiedades Publicas
        public string HtmlGrid { get; set; }
        public GridView GridSearch { get; set; }
        #endregion

        #region Constructores
        public SearchModel()
        {
            _grid = new GridView();
        }
        #endregion

        #region Metodos Publicos
        public void FillGrid(string entity)
        {
            switch (entity)
            {
                case "PersonaPasajero":
                    PersonaPasajeroService pasajeroService = new PersonaPasajeroService();
                    List<MAT.Entities.PersonaPasajero> pasajeros = new List<Entities.PersonaPasajero>();
                    pasajeros = pasajeroService.GetAll().ToList();
                    _grid.DataSource = pasajeros;
                    break;
                case "Viaje":
                    MAT.Services.ViajeService viajeService = new ViajeService();
                    List<Entities.Viaje> viajes = viajeService.GetAll().ToList();
                    _grid.DataSource = viajes;
                    break;
                default:
                    break;
            }

            // Bootstrap 5: grid devuelto por QuickSearch (diálogo búsqueda rápida en Reserva).
            _grid.CssClass = "table table-sm table-striped table-hover align-middle w-100";
            _grid.HeaderStyle.CssClass = "table-light";
            _grid.GridLines = GridLines.None;

            _grid.DataBind();
            HtmlGrid = _grid.RenderToString();
            GridSearch = _grid;
        }
        #endregion

    }
}