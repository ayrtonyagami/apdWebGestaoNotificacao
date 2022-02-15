using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GestaoPedidosNotificacao.UI.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Senha Actual")]
        public string SenhaActual { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Nova")]
        [StringLength(20, ErrorMessage = "Deve ter entre 5 à 20 caracteres.", MinimumLength = 5)]
        public string SenhaNova{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare(nameof(SenhaNova))]
        [StringLength(20, ErrorMessage = "Deve ter entre 5 à 20 caracteres.", MinimumLength = 5)]
        public string SenhaConfirmacao{ get; set; }
    }
}