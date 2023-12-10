using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Models
{
    public partial class Cliente
    {
        #region Constructors
        public Cliente(IEnumerable<SelectListItem> list)
        {
            TipoClienteList = list;
        }

        public Cliente(IEnumerable<SelectListItem> list, string id)
        {
            
        }
        #endregion

        #region Properties
        [DescriptionAttribute(@""), System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
        [DataObjectField(false, false, true)]
        public virtual IEnumerable<SelectListItem> TipoClienteList { get; set; }

        #endregion

        #region CustomMethods
        
        #endregion
    }
}