
namespace GestaoPedidosNotificacao.UI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(EntidadeTipoDress))]
    public partial class EntidadeTipo
    {
    }
    public class EntidadeTipoDress
    {

        [Display(Name = "Tipo Entidade")]
        public string Nome { get; set; }
    
    }
}
