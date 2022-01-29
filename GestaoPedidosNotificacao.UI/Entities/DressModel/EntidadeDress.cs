
namespace GestaoPedidosNotificacao.UI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(EntidadeDress))]
    public partial class Entidade
    {

    }
    public class EntidadeDress
    {

        [Display(Name = "Designação")]
        public string Denoninacao { get; set; }

        [Display(Name = "Tipo")]
        public Nullable<int> EntidadeTipoId { get; set; }

        [Display(Name = "Data de Cadastro")]
        public Nullable<System.DateTime> DataEntidade { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    
    }
}
