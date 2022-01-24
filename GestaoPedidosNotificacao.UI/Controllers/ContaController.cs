using GestaoPedidosNotificacao.UI.Entities;
using GestaoPedidosNotificacao.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestaoPedidosNotificacao.UI.Controllers
{
    public class ContaController : Controller
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: Conta
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var logUsr = db.Utilizadors.FirstOrDefault(x => 
                        x.Email.ToLower().Equals(model.UserName.ToLower()) &&
                        x.Senha.Equals(model.Password)
                    );

                    if (logUsr == null)
                        throw new Exception("Utilizador sem acesso!");

                    FormsAuthentication.SetAuthCookie(logUsr.Id.ToString(), model.RememberMe);

                    return RedirectToAction("Index","Home");
                }

                ModelState.AddModelError("", "Utilizador sem acesso!");
                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}