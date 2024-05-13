using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Domain.Enums
{
    public enum EnOrderStatus
    {
        [Description("Aguardando Pagamento")]
        WaitingPayment,
        [Description("Confirmado")]
        Confirmed,
        [Description("Em Transporte")]
        Shipping,
        [Description("Concluído")]
        Concluded,
        [Description("Cancelado")]
        Canceled,
    }
}
