using GestaoPedidosNotificacao.UI.AppExceptions;
using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Mvc.Filters;
using System.Web.Profile;
using System.Web.Routing;

namespace GestaoPedidosNotificacao.UI
{
    public class AppBaseController : Controller
    {
        public void ValidateAutorization()
        {
            if (Session["IsAutorizated"] == null)
                throw new AppUnautorizatedException();

            if (!Convert.ToBoolean(Session["IsAutorizated"]))
                throw new AppUnautorizatedException();
            else return;
        }

        public int UserId => Convert.ToInt32(User.Identity.Name);
        public string UserName => Session["UserName"].ToString();

         

    }
}