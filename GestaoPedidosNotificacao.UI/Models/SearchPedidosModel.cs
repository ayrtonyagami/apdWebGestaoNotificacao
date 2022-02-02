using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class SearchPedidosModel
    {
        public int? EntidadeId { get; set; }
        public string NumProcesso { get; set; }
        public int? EstadoId { get; set; }
        public DateTime? DataFacStart { get; set; }
        public DateTime? DataFacEnd { get; set; }
        public DateTime? DataPagStart { get; set; }
        public DateTime? DataPagEnd { get; set; }

        public bool IsEmpty()
        {
            if (EntidadeId.isID()) return false;
            if (NumProcesso != null) return false;
            if (EstadoId.isID()) return false;
            if (DataFacStart != null) return false;
            if (DataPagStart != null) return false;

            return true;
        }
    }
}