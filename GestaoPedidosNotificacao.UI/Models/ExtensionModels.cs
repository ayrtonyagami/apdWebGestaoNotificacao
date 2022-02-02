using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI
{
    public static class ExtensionModels
    {
        public static bool isID(this int? id)
        {
            return id.HasValue && id > 0;
        }

        public static bool isID(this int id)
        {
            return id > 0;
        }

        public static bool isEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return true;
            return false;
        }

        public static bool HasValue(this string value)
        {
            return value.isEmpty() == false;
        }
    }
}