using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestaoPedidosNotificacao.UI.Entities;
using GestaoPedidosNotificacao.UI.Repositories;
using GestaoPedidosNotificacao.UI.Models;

namespace GestaoPedidosNotificacao.UI.Controllers
{
    public class EntidadesController : AppBaseController
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        // GET: Entidades
        //public ActionResult Index()
        //{
        //    var entidades = db.Entidades.Include(e => e.EntidadeTipo);
        //    return View(entidades.ToList());
        //}

        public ActionResult Index(bool toClean = false, [Bind(Include = "search_Nome,EntidadeTipoId,Telefone,NIF,DataCadastroStart,DataCadastroEnd")] SearchEntidadeModel search = null)
        {
            EntidadesRepository entidadesRepo = new EntidadesRepository();

            var result = entidadesRepo.Buscar(search, toClean);

            FillListEntidadeViewBag();

            ViewBag.Search = search;

            return View(result);
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
            FillListEntidadeViewBag();
            return View();
        }

        // POST: Entidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Denoninacao,Email,Telefone,NIF,EntidadeTipoId,DataEntidade")] Entidade entidade)
        {

            if (ModelState.IsValid)
            {
                db.Entidades.Add(entidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            FillListEntidadeViewBag();
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
            FillListEntidadeViewBag();
            return View(entidade);
        }

        // POST: Entidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Denoninacao,Email,Telefone,NIF,EntidadeTipoId,DataEntidade")] Entidade entidade)
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

        public void FillListEntidadeViewBag()
        {
            var tipo = db.EntidadeTipoes.ToList();
            tipo.Insert(0, (new EntidadeTipo { Id = -1, Nome = "Selecionar" }));
            ViewBag.EntidadeTipoId = new SelectList(tipo, "Id", "Nome");
            return;
        }
    }
}
