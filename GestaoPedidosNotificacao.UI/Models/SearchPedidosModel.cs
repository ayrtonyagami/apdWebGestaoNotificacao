using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class SearchPedidosModel
    {

        [Display(Name = "Id Entidade")]
        public int? EntidadeId { get; set; }

        [Display(Name = "Entidade")]
        public string EntidadeNome { get; set; }

        [Display(Name = "Nº Processo")]
        public string NumProcesso { get; set; }

        [Display(Name = "Status")]
        public int? EstadoId { get; set; }
        public DateTime? DataFacStart { get; set; }
        public DateTime? DataFacEnd { get; set; }
        public DateTime? DataPagStart { get; set; }
        public DateTime? DataPagEnd { get; set; }

        public bool IsEmpty()
        {
            if (NumProcesso != null) return false;
            if (EstadoId.isID()) return false;
            if (EntidadeNome != null) return false;
            if (DataFacStart != null) return false;
            if (DataPagStart != null) return false;

            return true;
        }
    }
}