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
    public class PerfilController : Controller
    {
        private SCAContext db = new SCAContext();
        private PerfilRepositorio _perfilRepositorio;

        public PerfilController()
        {
            this._perfilRepositorio = new PerfilRepositorio(this.db);
        }

        // GET: Perfil
        public ActionResult Index(string nome = "")
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            var perfis = this._perfilRepositorio.getPerfis(nome);
            if (Request.IsAjaxRequest())
                return PartialView("~/Views/Perfil/_Selecionar_table.cshtml", perfis);
            return View(perfis);
        }

        // GET: Perfil/Details/5
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
            Perfil perfil = this._perfilRepositorio.getPerfilPorId((int)id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // GET: Perfil/Create
        public ActionResult Create()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            return View();
        }

        // POST: Perfil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,supervisor,status")] Perfil perfil)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    this._perfilRepositorio.inserirPerfil(perfil);
                    this._perfilRepositorio.salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.");
            }

            return View(perfil);
        }

        // GET: Perfil/Edit/5
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
            Perfil perfil = this._perfilRepositorio.getPerfilPorId((int)id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // POST: Perfil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,supervisor,status")] Perfil perfil)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    this._perfilRepositorio.atualizaPerfil(perfil);
                    this._perfilRepositorio.salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.");
            }
            return View(perfil);
        }

        // GET: Perfil/Delete/5
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
            Perfil perfil = this._perfilRepositorio.getPerfilPorId((int)id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // POST: Perfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            try
            {
                this._perfilRepositorio.deletarPerfil(id);
                this._perfilRepositorio.salvar();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                return RedirectToAction(
                    "Delete",
                    new System.Web.Routing.RouteValueDictionary {
                    { "id", id},
                    { "saveChangesError", true }
                  });
            }
        }
        // GET: Usuario/Find/5
        public JsonResult Find(int? id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            Perfil perfil = _perfilRepositorio.getPerfilPorId((int)id);
            if (perfil == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(perfil, JsonRequestBehavior.AllowGet);
        }

        // GET: Usuario/Valida/5
        public JsonResult Valida(int perfil_id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            Perfil perfil = _perfilRepositorio.getPerfilPorId(perfil_id);
            if (perfil == null)
            {
                return Json("Perfil não encontrado", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: Usuario/ValidaAtivo/5
        public JsonResult ValidaAtivo(int perfil_id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            Perfil perfil = _perfilRepositorio.getPerfilPorId(perfil_id);
            if (perfil == null)
                return Json("Perfil não encontrado", JsonRequestBehavior.AllowGet);
            else if(perfil.status == AtivoInativo.Inativo)
                return Json("Perfil intativo", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
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

        [NonAction]
        private RedirectToRouteResult validaSessaoUsuario()
        {
            var sessaoValida = Util.validaUsuarioLogado(Session);
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
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
