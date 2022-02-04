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
    public class EntidadeTipoesController : AppBaseController
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: EntidadeTipoes
        public ActionResult Index()
        {
            return View(db.EntidadeTipoes.ToList());
        }

        // GET: EntidadeTipoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadeTipo entidadeTipo = db.EntidadeTipoes.Find(id);
            if (entidadeTipo == null)
            {
                return HttpNotFound();
            }
            return View(entidadeTipo);
        }

        // GET: EntidadeTipoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntidadeTipoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome")] EntidadeTipo entidadeTipo)
        {
            if (ModelState.IsValid)
            {
                db.EntidadeTipoes.Add(entidadeTipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entidadeTipo);
        }

        // GET: EntidadeTipoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadeTipo entidadeTipo = db.EntidadeTipoes.Find(id);
            if (entidadeTipo == null)
            {
                return HttpNotFound();
            }
            return View(entidadeTipo);
        }

        // POST: EntidadeTipoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] EntidadeTipo entidadeTipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entidadeTipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entidadeTipo);
        }

        // GET: EntidadeTipoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadeTipo entidadeTipo = db.EntidadeTipoes.Find(id);
            if (entidadeTipo == null)
            {
                return HttpNotFound();
            }
            return View(entidadeTipo);
        }

        // POST: EntidadeTipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntidadeTipo entidadeTipo = db.EntidadeTipoes.Find(id);
            db.EntidadeTipoes.Remove(entidadeTipo);
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
