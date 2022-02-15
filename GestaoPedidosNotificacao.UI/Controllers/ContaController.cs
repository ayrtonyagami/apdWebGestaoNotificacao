using GestaoPedidosNotificacao.UI.AppExceptions;
using GestaoPedidosNotificacao.UI.Entities;
using GestaoPedidosNotificacao.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestaoPedidosNotificacao.UI.Controllers
{
    public class ContaController : AppBaseController
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: Conta
        public ActionResult Index()
        {
            return View();
        }

        [AppAllowUnauthorazed]
        [HttpGet]
        public ActionResult Login()
        {
            Session["UserName"] = null;
            Session["IsAutorizated"] = null;
            Session["Role"] = null;
            return View();
        }

        [AppAllowUnauthorazed]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var logUsr = db.Utilizadors.FirstOrDefault(x => 
                        x.Email.ToLower() == model.UserName.ToLower() &&
                        x.Senha.Equals(model.Password) &&
                        x.IsActive == true
                    );

                    if (logUsr == null)
                        throw new AppUnautorizatedException();

                    FormsAuthentication.SetAuthCookie(logUsr.Id.ToString(), model.RememberMe);

                    if (logUsr.RoleId == null)
                        throw new AppUnautorizatedException();

                    Session["UserName"] = logUsr.Nome;
                    Session["IsAutorizated"] = true;
                    Session["Role"] = logUsr.Role.Descricao;

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
        [AppAuthorization]
        [HttpGet]
        public ActionResult Perfil()
        {

            var usr = db.Utilizadors.FirstOrDefault(x => x.Id == UserId);
            FillListViewBag(usr);
            return View(usr);
        }

        [ChildActionOnly]
        public ActionResult MudarMinhaSenha()
        {
            
            ChangePasswordModel model = new ChangePasswordModel();
            model.Id = UserId;
            
            return PartialView("MudarMinhaSenha", model);
        }

        [HttpPost]
        public ActionResult MudarMinhaSenhaPost(ChangePasswordModel userUpdating)
        {
            if (ModelState.IsValid)
            {
                var utilizador = db.Utilizadors.FirstOrDefault(x => x.Id == userUpdating.Id);
                utilizador.DataModificacao = DateTime.Now;
                utilizador.Senha = userUpdating.SenhaNova;

                db.Entry(utilizador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Perfil));
            }

            return RedirectToAction(nameof(Perfil));
        }


        public void FillListViewBag(Utilizador utilizador)
        {
            var tipo = db.Roles.ToList();
            tipo.Insert(0, (new Role { Id = -1, Descricao = "Selecionar" }));

            if (utilizador == null)
            {
                ViewBag.RoleId = new SelectList(tipo, "Id", "Descricao");
            }
            else
            {
                ViewBag.RoleId = new SelectList(tipo, "Id", "Descricao", utilizador.RoleId);

            }
        }
    }
}