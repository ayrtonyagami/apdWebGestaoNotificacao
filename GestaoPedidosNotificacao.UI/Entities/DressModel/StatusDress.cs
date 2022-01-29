
namespace GestaoPedidosNotificacao.UI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(StatusDress))]
    public partial class Status
    {
    }

    public class StatusDress
    {
        
        [Display(Name = "Status")]
        public string Descricao { get; set; }

        [Display(Name = "Css")]
        public string Cor { get; set; }
    
    }
}
