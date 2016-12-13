using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaControleAcesso.Models;

namespace SistemaControleAcesso.Controllers
{
    public class EstadoController : Controller
    {
        private SCAContext db = new SCAContext();

        // GET: Estado
        public ActionResult Index()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            return View(db.estado.ToList());
        }

        // GET: Estado/Details/5
        public ActionResult Details(string id)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            return View();
        }

        // POST: Estado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "uf,nome")] Estado estado)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            if (ModelState.IsValid)
            {
                db.estado.Add(estado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estado);
        }

        // GET: Estado/Edit/5
        public ActionResult Edit(string id)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // POST: Estado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "uf,nome")] Estado estado)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            if (ModelState.IsValid)
            {
                db.Entry(estado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado);
        }

        // GET: Estado/Delete/5
        public ActionResult Delete(string id)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            Estado estado = db.estado.Find(id);
            db.estado.Remove(estado);
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
        private RedirectToRouteResult validaSessaoPermissoes()
        {
            var sessaoValida = Util.validaSessao(Session, Request.RawUrl);
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            ViewBag.caminho = Util.getCaminhoFuncionalidade(Session, Request.RawUrl);

            ViewBag.consultar = Util.permiteAction(Session, Request.RawUrl, "Details");
            ViewBag.inserir = Util.permiteAction(Session, Request.RawUrl, "Create");
            ViewBag.atualizar = Util.permiteAction(Session, Request.RawUrl, "Edit");
            ViewBag.excluir = Util.permiteAction(Session, Request.RawUrl, "Delete");

            return null;
        }
    }
}
