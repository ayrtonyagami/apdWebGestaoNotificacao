using GestaoPedidosNotificacao.UI.Entities;
using GestaoPedidosNotificacao.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoPedidosNotificacao.UI.Controllers
{
    [AppAuthorization]
    public class HomeController : AppBaseController
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return RedirectToAction(nameof(Index));
        }

        [ChildActionOnly]
        public ActionResult _StatusCard()
        {
            var liResult = db.Status.ToList();

            List<StatusCardModel> li = new List<StatusCardModel>();
            foreach (var item in liResult)
            {
                var card = new StatusCardModel();
                card.StatusName = item.Descricao;
                card.Css = item.Cor;
                card.QtdStatus = item.Pedidos.Count(x=> x.DataFactura.Value.Year == DateTime.Now.Year);
                li.Add(card);
            }

            return PartialView("_StatusCard", li);

        }

        [ChildActionOnly]
        public ActionResult _PedidosAnualChart()
        {
            int ano = DateTime.Now.Year;
            var liResult = db.Pedidos.Where(x=>x.DataFactura.Value.Year == ano).ToList();
            var li = new ListPedidosAnualChartModel();
            li.PedidosAnual = new List<PedidosAnualChartModel>();

            for (int mes = 1; mes <= 12; mes++)
            {
                PedidosAnualChartModel modal = new PedidosAnualChartModel();
                var dt = new DateTime(ano, mes, 1);                
                modal.Mes = dt.ToString("MMM");
                modal.nPedidos = liResult.Count(x => x.DataFactura.Value.Month.Equals(mes));

                li.PedidosAnual.Add(modal);
            }

            return PartialView("_PedidosAnualChart", li);

        }

        [ChildActionOnly]
        public ActionResult _PedidosMensalPagasChart()
        {
            int ano = DateTime.Now.Year;
            var liResult = db.Pedidos.Where(x => x.DataFactura.Value.Year == ano).ToList();
            var li = new ListPedidosMensalPagasChartModel();
            li.PedidosPagosAnual = new List<PedidosMensalPagasChartModel>();

            for (int mes = 1; mes <= 12; mes++)
            {
                PedidosMensalPagasChartModel modal = new PedidosMensalPagasChartModel();
                var dt = new DateTime(ano, mes, 1);
                modal.Mes = dt.ToString("MMMM");

                var pedidosPagosMes = liResult.Where(x => x.DataPagamento != null);
                if (pedidosPagosMes.Any())
                {
                    pedidosPagosMes = pedidosPagosMes.Where(x => x.DataPagamento.Value.Month.Equals(mes));
                    if (pedidosPagosMes.Any())
                    {
                        modal.ValorMensal = pedidosPagosMes.Sum(x => x.Valor) ?? 0;
                    }
                }
                else
                {
                    modal.ValorMensal = 0;
                }
                li.PedidosPagosAnual.Add(modal);
            }

            return PartialView("_PedidosMensalPagasChart", li);

        }

        [ChildActionOnly]
        public ActionResult _PedidosStatusDonut()
        {

            ListPedidosStatusDonutModel donut = new ListPedidosStatusDonutModel();

            var liResult = db.Status.ToList();

            donut.PedidosAnual = new List<PedidosStatusDonutModel>();
            foreach (var item in liResult)
            {
                var card = new PedidosStatusDonutModel();
                card.StatusName = item.Descricao;
                card.PercStatus = item.Pedidos.Count(x => x.DataFactura.Value.Year == DateTime.Now.Year);
                donut.PedidosAnual.Add(card);
            }

            donut.CalcularPercentange();


            return PartialView("_PedidosStatusDonut", donut);

        }


        [ChildActionOnly]
        public ActionResult _FinalidadesProcesso()
        {

            ListFinalidadesProgressoModel progresso = new ListFinalidadesProgressoModel();

            var liResult = db.ServicoProdutoes;

            progresso.Finalidades = new List<FinalidadesProgressoModel>();

            foreach (var item in liResult)
            {
                var card = new FinalidadesProgressoModel();
                card.Nome = item.Descricao;
                card.Prec = item.Pedidos.Count(x => x.DataFactura.Value.Year == DateTime.Now.Year);
                progresso.Finalidades.Add(card);
            }

            progresso.CalcularPercentange();
            progresso.Finalidades = progresso.Finalidades.OrderByDescending(x => x.Prec).ToList();

            return PartialView("_FinalidadesProcesso", progresso);

        }
    }
}