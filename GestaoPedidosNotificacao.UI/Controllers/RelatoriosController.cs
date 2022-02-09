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
using GestaoPedidosNotificacao.UI.Repositories;
using GestaoPedidosNotificacao.UI.Models;

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

        public ActionResult NotificacoesPageFiltro()
        {
            FillListViewBag();
            return View();
        }

        public ActionResult NotificacoesToPrint(SearchPedidosModel search = null)
        {
            PedidosRepository pedidoRepo = new PedidosRepository();

            bool all = search == null;

            var result = pedidoRepo.Buscar(search, all);
            //var pedidosFull = db.Pedidos.Include(p => p.Entidade).Include(p => p.Status).Include(p => p.Utilizador);
            return View(result);
        }
        public ActionResult PrintNotificacoes([Bind(Include = "EntidadeNome,EstadoId,NumProcesso,DataFacStart,DataFacEnd,DataPagStart,DataPagEnd")] SearchPedidosModel search = null)
        {
            return new Rotativa.ActionAsPdf(nameof(NotificacoesToPrint), search);
        }

        public ActionResult PainelToPrint()
        {
            return View();
        }
        public ActionResult PrintPainel()
        {   

            var result = new Rotativa.ActionAsImage(nameof(PainelToPrint));

            return result;
        }











        public void FillListViewBag()
        {            
            var status = db.Status.ToList();            
            status.Insert(0, (new Status { Id = -1, Descricao = "Selecionar" }));            
            ViewBag.EstadoId = new SelectList(status, "Id", "Descricao");
            return;
        }
    }
}