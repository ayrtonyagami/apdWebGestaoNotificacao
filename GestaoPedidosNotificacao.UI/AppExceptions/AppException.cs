using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.AppExceptions
{
    public class AppException : Exception
    {
        public AppException() : base()
        {
        }
        public AppException(string message) : base(message)
        {
        }
    }

    public class AppUnautorizatedException : AppException
    {
        public AppUnautorizatedException() : base("Utilizador Sem Acesso.")
        {
        }
    }
}