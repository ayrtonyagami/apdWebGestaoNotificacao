using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class IsIDValide : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string msgErro = "Valor de " + validationContext.DisplayName + " inválido.";

            if (value == null)
                return new ValidationResult(msgErro);

            int id = (int)value;
            if(id<1)
                return new ValidationResult(msgErro);
            else
            {
                return ValidationResult.Success;
            }
            
        }
    }
}