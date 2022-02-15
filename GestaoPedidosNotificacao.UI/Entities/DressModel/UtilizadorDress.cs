
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
        [Required(ErrorMessage ="Obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Função")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Funcao { get; set; }

        [Display(Name = "Activo?")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Ultima Modificação")]
        public Nullable<System.DateTime> DataModificacao { get; set; }

        
        [Display(Name = "Nivel de Permissão")]
        [IsIDValide]
        public Nullable<int> RoleId { get; set; }
    }
}
