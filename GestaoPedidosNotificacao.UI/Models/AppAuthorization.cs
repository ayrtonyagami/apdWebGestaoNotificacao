using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class AppAuthorization : ActionFilterAttribute, IAuthenticationFilter
    {
        public string Roles { get; set; }
        public void OnAuthentication(AuthenticationContext filterContext)
        {
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            try
            {
                if (CanAllowUnauthorazed(filterContext)) return;

                if (filterContext.HttpContext.Session["IsAutorizated"] == null)
                    filterContext.Result = new HttpUnauthorizedResult();

            }
            catch (Exception ex)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public bool CanAllowUnauthorazed(AuthenticationChallengeContext actionContext)
        {

            var reflectedActionDescriptor = actionContext.ActionDescriptor;
            if (reflectedActionDescriptor != null)
            {
                try
                {
                    var attr = reflectedActionDescriptor
                               .GetCustomAttributes(typeof(AppAllowUnauthorazed), false)
                               .OfType<AppAllowUnauthorazed>().FirstOrDefault();

                    return attr?.Allow ?? false;
                }
                catch { return false; }
            }
            return false;
        }

        public bool HasPermissionRole(AuthenticationChallengeContext actionContext)
        {
            string userRole = string.Empty;
            if (actionContext.HttpContext.Session["Role"] == null)
                throw new Exception("Unauthorized");
            else
                userRole = actionContext.HttpContext.Session["Role"].ToString();

            if (string.IsNullOrEmpty(this.Roles)) return true;

            var rolesArray = this.Roles.Split(',');
            bool result = rolesArray.Any(x => x == userRole);

            return result;
        }
    }

    public class AppAllowUnauthorazed : Attribute
    {
        public bool Allow
        {
            get { return true; }
        }
    }
}