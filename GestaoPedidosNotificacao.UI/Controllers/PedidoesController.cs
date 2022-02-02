using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestaoPedidosNotificacao.UI.Entities;
using GestaoPedidosNotificacao.UI.Models;

namespace GestaoPedidosNotificacao.UI.Controllers
{
    public class PedidoesController : Controller
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: Pedidoes
        //public ActionResult Index()
        //{
        //    var pedidos = db.Pedidos.Include(p => p.Entidade).Include(p => p.Status).Include(p => p.Utilizador);
        //    FillListViewBag(null);
        //    return View(pedidos.ToList());
        //}
        
        public ActionResult Index(bool toClean = false, [Bind(Include = "EntidadeId,EstadoId,NumProcesso,DataFacStart,DataFacEnd,DataPagStart,DataPagEnd")] SearchPedidosModel search = null)
        {
            if(search == null || search.IsEmpty() || toClean)
            {
                var pedidosFull = db.Pedidos.Include(p => p.Entidade).Include(p => p.Status).Include(p => p.Utilizador);
                FillListViewBag(null);
                return View(pedidosFull.ToList());
            }

            var query = db.Pedidos.Where(x=> x.EntidadeId != null);

            if (search.EntidadeId.isID()) query = query.Where(x => x.EntidadeId == search.EntidadeId);
            if (search.EstadoId.isID()) query = query.Where(x => x.EstadoId == search.EstadoId);
            if (search.NumProcesso.HasValue()) query = query.Where(x => x.NumProcesso == search.NumProcesso);

            //Data de facturação
            if (search.DataFacStart != null && search.DataFacEnd != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataFactura.Value) >= search.DataFacStart.Value.Date
                && DbFunctions.TruncateTime(x.DataFactura.Value) <= search.DataFacEnd.Value.Date);
            else if (search.DataFacStart != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataFactura.Value) == search.DataFacStart.Value.Date);

            //Data de pagamento
            if (search.DataPagStart != null && search.DataPagEnd != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataPagamento.Value) >= search.DataPagStart.Value.Date
                && DbFunctions.TruncateTime(x.DataPagamento.Value) <= search.DataPagEnd.Value.Date);
            else if (search.DataPagStart != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataFactura.Value) == search.DataPagStart.Value.Date);

            

            var pedidos = query.Include(p => p.Entidade).Include(p => p.Status).Include(p => p.Utilizador);
            FillListViewBag(null);
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
            pedido.CalcularTempo();
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public ActionResult Create(int entidadeId=0)
        {
            FillListViewBag(null);
            if (entidadeId > 0)
            {
                Entidade entidade = db.Entidades.FirstOrDefault(x=> x.Id == entidadeId);
                if (entidade == null) return View();

                ViewBag.FromEntidade = true;
                Pedido model = new Pedido();
                model.Entidade = entidade;
                model.EntidadeId = entidadeId;  

                return View(model);
            }
            else
            {
                ViewBag.FromEntidade = false;
                return View();
            }
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ServicoId,EntidadeId,EstadoId,Observacao,DataFactura,DataPagamento,UtilizadorId,NumProcesso")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                var servico = db.ServicoProdutoes.Find(pedido.ServicoId);

                pedido.Finalidade = servico.Nome;
                pedido.Valor = servico.Valor;

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
        public ActionResult Edit([Bind(Include = "Id,ServicoId,EntidadeId,EstadoId,Observacao,DataFactura,DataPagamento,UtilizadorId,NumProcesso")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                var servico = db.ServicoProdutoes.Find(pedido.ServicoId);

                pedido.Finalidade = servico.Nome;
                pedido.Valor = servico.Valor;

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
            }).ToList();

            var entidades = db.Entidades.ToList();
            var status = db.Status.ToList();
            var utilizadors = db.Utilizadors.ToList();

            entidades.Insert(0,(new Entidade { Id = -1, Denoninacao = "Selecionar" }));
            status.Insert(0, (new Status { Id = -1, Descricao = "Selecionar" }));
            utilizadors.Insert(0, (new Utilizador {Id = -1, Nome = "Selecionar" }));
            liServico.Insert(0, (new { Id = -1, Finalidade = "Selecionar" }));

            if (pedido == null)
            {
                ViewBag.EntidadeId = new SelectList(entidades, "Id", "Denoninacao");
                ViewBag.EstadoId = new SelectList(status, "Id", "Descricao");
                ViewBag.UtilizadorId = new SelectList(utilizadors, "Id", "Nome");
                ViewBag.ServicoId = new SelectList(liServico, "Id", "Finalidade");

                return;
            }

            ViewBag.EntidadeId = new SelectList(entidades, "Id", "Denoninacao", pedido.EntidadeId);
            ViewBag.EstadoId = new SelectList(status, "Id", "Descricao", pedido.EstadoId);
            ViewBag.UtilizadorId = new SelectList(utilizadors, "Id", "Nome", pedido.UtilizadorId);
            ViewBag.ServicoId = new SelectList(liServico, "Id", "Finalidade",pedido.ServicoId);

            return;
        }
    }
}
