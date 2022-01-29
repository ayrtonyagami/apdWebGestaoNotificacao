
namespace GestaoPedidosNotificacao.UI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(UtilizadorDress))]
    public partial class Utilizador
    {

    }
    public class UtilizadorDress
    {
        
        [Display(Name = "Nome do Utilizador")]
        public string Nome { get; set; }

        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Função")]
        public string Funcao { get; set; }

        [Display(Name = "Actiovo?")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Ultima Modificação")]
        public Nullable<System.DateTime> DataModificacao { get; set; }
    }
}
