using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Enums
{
    public enum eFormaPago
    {
        [Description("Contado")]
        Contado = 1,
        [Description("Tarjeta de Crédito")]
        Credito = 2,
        [Description("Transferencia Bancaria")]
        Transferencia = 3,
        [Description("Nota de Crédito")]
        Nota_de_Credito = 4,
        [Description("Mercado Pago")]
        Mercado_Pago = 5,
        [Description("Mutual")]
        Mutual = 6,
        [Description("Tarjeta de Débito")]
        Debito = 7,
        [Description("Depósito CTA CTE")]
        Deposito_CTACTE = 8,
        [Description("Cheque")]
        Cheque = 9,
        [Description("ADP")]
        ADP = 10
    }
}
