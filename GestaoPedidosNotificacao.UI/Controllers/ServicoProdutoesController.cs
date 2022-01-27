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
    public class ServicoProdutoesController : Controller
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: ServicoProdutoes
        public ActionResult Index()
        {
            return View(db.ServicoProdutoes.ToList());
        }

        // GET: ServicoProdutoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicoProduto servicoProduto = db.ServicoProdutoes.Find(id);
            if (servicoProduto == null)
            {
                return HttpNotFound();
            }
            return View(servicoProduto);
        }

        // GET: ServicoProdutoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicoProdutoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Quantidade,Valor,IsProduto")] ServicoProduto servicoProduto)
        {
            if (ModelState.IsValid)
            {
                servicoProduto.DataModificacao = DateTime.Now;
                db.ServicoProdutoes.Add(servicoProduto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servicoProduto);
        }

        // GET: ServicoProdutoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicoProduto servicoProduto = db.ServicoProdutoes.Find(id);
            if (servicoProduto == null)
            {
                return HttpNotFound();
            }
            return View(servicoProduto);
        }

        // POST: ServicoProdutoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Quantidade,Valor,IsProduto")] ServicoProduto servicoProduto)
        {
            if (ModelState.IsValid)
            {
                servicoProduto.DataModificacao = DateTime.Now;
                db.Entry(servicoProduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servicoProduto);
        }

        // GET: ServicoProdutoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicoProduto servicoProduto = db.ServicoProdutoes.Find(id);
            if (servicoProduto == null)
            {
                return HttpNotFound();
            }
            return View(servicoProduto);
        }

        // POST: ServicoProdutoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicoProduto servicoProduto = db.ServicoProdutoes.Find(id);
            db.ServicoProdutoes.Remove(servicoProduto);
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
    }
}
