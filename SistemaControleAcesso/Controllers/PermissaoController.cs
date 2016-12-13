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
    public class PermissaoController : Controller
    {
        private SCAContext db = new SCAContext();

        // GET: Permissao
        public ActionResult Index()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            var permissaos = db.permissao.Include(p => p.funcionalidade).Include(p => p.perfil).Include(p => p.usuario);
            return View(permissaos.ToList());
        }

        // GET: Permissao/Details/5
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
            Permissao permissao = db.permissao.Find(id);
            if (permissao == null)
            {
                return HttpNotFound();
            }
            return View(permissao);
        }

        // GET: Permissao/Create
        public ActionResult Create()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            return View();
        }

        // POST: Permissao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,consultar,inserir,alterar,excluir,especial,perfil_id,usuario_id,funcionalidade_id")] Permissao permissao)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            if ((permissao.perfil_id == null || permissao.perfil_id == 0) && 
                (permissao.usuario_id == null || permissao.usuario_id == 0))
                ModelState.AddModelError("", "Perfil ou usuário é obrigatório");

            if (ModelState.IsValid)
            {
                db.permissao.Add(permissao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permissao);
        }

        // GET: Permissao/Edit/5
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
            Permissao permissao = db.permissao.Find(id);
            if (permissao == null)
            {
                return HttpNotFound();
            }
            return View(permissao);
        }

        // POST: Permissao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,consultar,inserir,alterar,excluir,especial,perfil_id,usuario_id,funcionalidade_id")] Permissao permissao)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            if ((permissao.perfil_id == null || permissao.perfil_id == 0) &&
                (permissao.usuario_id == null || permissao.usuario_id == 0))
                ModelState.AddModelError("", "Perfil ou usuário é obrigatório");

            if (ModelState.IsValid)
            {
                db.Entry(permissao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permissao);
        }

        // GET: Permissao/Delete/5
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
            Permissao permissao = db.permissao.Find(id);
            if (permissao == null)
            {
                return HttpNotFound();
            }
            return View(permissao);
        }

        // POST: Permissao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            Permissao permissao = db.permissao.Find(id);
            db.permissao.Remove(permissao);
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
