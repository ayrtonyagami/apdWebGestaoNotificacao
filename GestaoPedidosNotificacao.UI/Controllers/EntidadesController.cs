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
    public class EntidadesController : AppBaseController
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: Entidades
        public ActionResult Index()
        {
            var entidades = db.Entidades.Include(e => e.EntidadeTipo);
            return View(entidades.ToList());
        }

        // GET: Entidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidade entidade = db.Entidades.Find(id);

            if (entidade == null)
            {
                return HttpNotFound();
            }
            return View(entidade);
        }

        // GET: Entidades/Create
        public ActionResult Create()
        {
            ViewBag.EntidadeTipoId = new SelectList(db.EntidadeTipoes, "Id", "Nome");
            return View();
        }

        // POST: Entidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Denoninacao,Email,Telefone,NumeroProcesso,EntidadeTipoId,DataEntidade")] Entidade entidade)
        {

            if (ModelState.IsValid)
            {
                db.Entidades.Add(entidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntidadeTipoId = new SelectList(db.EntidadeTipoes, "Id", "Nome", entidade.EntidadeTipoId);
            return View(entidade);
        }

        // GET: Entidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidade entidade = db.Entidades.Find(id);
            if (entidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntidadeTipoId = new SelectList(db.EntidadeTipoes, "Id", "Nome", entidade.EntidadeTipoId);
            return View(entidade);
        }

        // POST: Entidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Denoninacao,Email,Telefone,NumeroProcesso,EntidadeTipoId,DataEntidade")] Entidade entidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntidadeTipoId = new SelectList(db.EntidadeTipoes, "Id", "Nome", entidade.EntidadeTipoId);
            return View(entidade);
        }

        // GET: Entidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidade entidade = db.Entidades.Find(id);
            if (entidade == null)
            {
                return HttpNotFound();
            }
            return View(entidade);
        }

        // POST: Entidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entidade entidade = db.Entidades.Find(id);
            db.Entidades.Remove(entidade);
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
