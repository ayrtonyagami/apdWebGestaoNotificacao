using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestaoPedidosNotificacao.UI.Entities;

namespace GestaoPedidosNotificacao.UI.Controllers
{
    public class PedidoesController : Controller
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: Pedidoes
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(p => p.Entidade).Include(p => p.Status).Include(p => p.Utilizador);
            return View(pedidos.ToList());
        }

        // GET: Pedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public ActionResult Create()
        {

            FillListViewBag(null);

            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Finalidade,EntidadeId,Valor,EstadoId,Observacao,DataFactura,DataPagamento,UtilizadorId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            FillListViewBag(pedido);

            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }

            FillListViewBag(pedido);

            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Finalidade,EntidadeId,Valor,EstadoId,Observacao,DataFactura,DataPagamento,UtilizadorId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            FillListViewBag(pedido);

            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void FillListViewBag(Pedido pedido)
        {

            var liServico = db.ServicoProdutoes.Select(x => new
            {
                Id = x.Id,
                Finalidade = x.Valor + " AKz -" +  x.Nome
            });

            if(pedido == null)
            {
                ViewBag.EntidadeId = new SelectList(db.Entidades, "Id", "Denoninacao");
                ViewBag.EstadoId = new SelectList(db.Status, "Id", "Descricao");
                ViewBag.UtilizadorId = new SelectList(db.Utilizadors, "Id", "Nome");
                ViewBag.ServicoId = new SelectList(liServico, "Id", "Finalidade");

                return;
            }

            ViewBag.EntidadeId = new SelectList(db.Entidades, "Id", "Denoninacao", pedido.EntidadeId);
            ViewBag.EstadoId = new SelectList(db.Status, "Id", "Descricao", pedido.EstadoId);
            ViewBag.UtilizadorId = new SelectList(db.Utilizadors, "Id", "Nome", pedido.UtilizadorId);
            ViewBag.ServicoId = new SelectList(liServico, "Id", "Finalidade",pedido.ServicoId);

            return;
        }
    }
}
