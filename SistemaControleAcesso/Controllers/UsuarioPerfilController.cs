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
    public class UsuarioPerfilController : Controller
    {
        private SCAContext db = new SCAContext();

        // GET: UsuarioPerfil
        public ActionResult Index()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            var usuarioperfil = db.usuarioperfil.Include(u => u.perfil).Include(u => u.usuario);
            return View(usuarioperfil.ToList());
        }

        // GET: UsuarioPerfil/Details/5
        public ActionResult Details(int? id)
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
            UsuarioPerfil usuarioPerfil = db.usuarioperfil.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfil/Create
        public ActionResult Create()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            return View();
        }

        // POST: UsuarioPerfil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,perfil_id,usuario_id,status")] UsuarioPerfil usuarioPerfil)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            if (ModelState.IsValid)
            {
                db.usuarioperfil.Add(usuarioPerfil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.perfil_id = new SelectList(db.perfil, "id", "nome", usuarioPerfil.perfil_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "senha", usuarioPerfil.usuario_id);
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfil/Edit/5
        public ActionResult Edit(int? id)
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
            UsuarioPerfil usuarioPerfil = db.usuarioperfil.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            ViewBag.perfil_id = new SelectList(db.perfil, "id", "nome", usuarioPerfil.perfil_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "senha", usuarioPerfil.usuario_id);
            return View(usuarioPerfil);
        }

        // POST: UsuarioPerfil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,perfil_id,usuario_id,status")] UsuarioPerfil usuarioPerfil)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            if (ModelState.IsValid)
            {
                db.Entry(usuarioPerfil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.perfil_id = new SelectList(db.perfil, "id", "nome", usuarioPerfil.perfil_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "senha", usuarioPerfil.usuario_id);
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfil/Delete/5
        public ActionResult Delete(int? id)
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
            UsuarioPerfil usuarioPerfil = db.usuarioperfil.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            return View(usuarioPerfil);
        }

        // POST: UsuarioPerfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            UsuarioPerfil usuarioPerfil = db.usuarioperfil.Find(id);
            db.usuarioperfil.Remove(usuarioPerfil);
            db.SaveChanges();
            return RedirectToAction("Index");
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
