using GestaoPedidosNotificacao.UI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace GestaoPedidosNotificacao.UI.Controllers
{
    public class RelatoriosController : Controller
    {

        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: Relatorios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EntidadesToPrint()
        {
            var entidades = db.Entidades.Include(x => x.EntidadeTipo);
            return View(entidades.ToList());
        }

        public ActionResult PrintEntidades()
        {
            return new Rotativa.ActionAsPdf(nameof(EntidadesToPrint));
        }

        public ActionResult NotificacoesToPrint()
        {
            var pedidosFull = db.Pedidos.Include(p => p.Entidade).Include(p => p.Status).Include(p => p.Utilizador);
            return View(pedidosFull.ToList());
        }
        public ActionResult PrintNotificacoes()
        {
            return new Rotativa.ActionAsPdf(nameof(NotificacoesToPrint));
        }

        public ActionResult PrintPainel()
        {
            var result = new Rotativa.UrlAsPdf(Url.Action("Index", "Home"));
            
            return result;
        }



    }
}