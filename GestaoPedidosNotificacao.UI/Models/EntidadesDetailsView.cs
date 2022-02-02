using GestaoPedidosNotificacao.UI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class EntidadesDetailsView
    {
        public Entidade entidade { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}