
namespace GestaoPedidosNotificacao.UI.Entities
{
    using Models;
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
        [Required(ErrorMessage ="Obrigat�rio")]
        public string Nome { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Obrigat�rio")]
        public string Email { get; set; }

        [Display(Name = "Fun��o")]
        [Required(ErrorMessage = "Obrigat�rio")]
        public string Funcao { get; set; }

        [Display(Name = "Activo?")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Ultima Modifica��o")]
        public Nullable<System.DateTime> DataModificacao { get; set; }

        
        [Display(Name = "Nivel de Permiss�o")]
        [IsIDValide]
        public Nullable<int> RoleId { get; set; }
    }
}
