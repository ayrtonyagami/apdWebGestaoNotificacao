using GestaoPedidosNotificacao.UI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Rotativa;

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


        public ActionResult RelatorioPedidos(int? pagina, Boolean? pdf)
        {
            var listaPedidos = db.Pedidos.OrderByDescending(c => c.DataFactura).ToList();

            if (pdf != true)
            {
                int numeroRegistros = 5;
                int numeroPagina = (pagina ?? 1);
                return View(listaPedidos.ToPagedList(numeroPagina, numeroRegistros));
            }
            else
            {
                int pagNumero = 1;
                
                var relatorioPDF = new ViewAsPdf
                {
                    ViewName = "RelatorioPedidos",
                    FileName = "RelatorioPedidos",
                    IsGrayScale = true,
                    Model = listaPedidos.ToPagedList(pagNumero, listaPedidos.Count)
                };
                return relatorioPDF;
            }
        }
    }
}