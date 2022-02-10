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

        #region Entidades
        public ActionResult EntidadesPageFiltro()
        {
            FillListEntidadeViewBag();
            return View();
        }

        public ActionResult EntidadesToPrint(SearchEntidadeModel search)
        {
            EntidadesRepository entidadesRepo = new EntidadesRepository();

            bool all = search == null;

            var entidades = entidadesRepo.Buscar(search,all);

            return View(entidades);
        }

        public ActionResult PrintEntidades([Bind(Include = "Nome,EntidadeTipoId,Telefone,NIF,DataCadastroStart,DataCadastroEnd")] SearchEntidadeModel search = null)
        {
            return new Rotativa.ActionAsPdf(nameof(EntidadesToPrint), search);
        }
        #endregion Entidades

        #region Notificacoes
        public ActionResult NotificacoesPageFiltro()
        {
            FillListNotificacoesViewBag();
            return View();
        }

        public ActionResult NotificacoesToPrint(SearchPedidosModel search = null)
        {
            PedidosRepository pedidoRepo = new PedidosRepository();

            bool all = search == null;

            var result = pedidoRepo.Buscar(search, all);

            return View(result);
        }
        public ActionResult PrintNotificacoes([Bind(Include = "EntidadeNome,EstadoId,NumProcesso,DataFacStart,DataFacEnd,DataPagStart,DataPagEnd")] SearchPedidosModel search = null)
        {
            return new Rotativa.ActionAsPdf(nameof(NotificacoesToPrint), search);
        }


        #endregion Notificacoes
        public ActionResult PainelToPrint()
        {
            return View();
        }
        public ActionResult PrintPainel()
        {   

            var result = new Rotativa.ActionAsImage(nameof(PainelToPrint));

            return result;
        }










        public void FillListEntidadeViewBag()
        {
            var tipo = db.EntidadeTipoes.ToList();
            tipo.Insert(0, (new EntidadeTipo { Id = -1, Nome = "Selecionar" }));            
            ViewBag.EntidadeTipoId = new SelectList(tipo, "Id", "Nome");
            return;
        }


        public void FillListNotificacoesViewBag()
        {            
            var status = db.Status.ToList();            
            status.Insert(0, (new Status { Id = -1, Descricao = "Selecionar" }));            
            ViewBag.EstadoId = new SelectList(status, "Id", "Descricao");
            return;
        }
    }
}