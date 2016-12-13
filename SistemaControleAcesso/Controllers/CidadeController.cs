using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaControleAcesso.Models;
using System.Web.Services;

namespace SistemaControleAcesso.Controllers
{
    public class CidadeController : Controller
    {
        private SCAContext db = new SCAContext();
        private CidadeRepositorio _cidadeRepositorio;

        public CidadeController()
        {
            this._cidadeRepositorio = new CidadeRepositorio(db);
        }

        // GET: Cidade
        public ActionResult Index(string cidade_nome = "")
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            var cidades = _cidadeRepositorio.getCidades(cidade_nome);
            if (Request.IsAjaxRequest())
                return PartialView("~/Views/Cidade/_Selecionar_table.cshtml", cidades);
            return View(cidades);
        }

        // GET: Cidade/Details/5
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
            Cidade cidade = _cidadeRepositorio.getCidadePorId((int)id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        // GET: Cidade/Create
        public ActionResult Create()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }

            ViewBag.estados = _cidadeRepositorio.getEstados();
            return View();
        }

        // POST: Cidade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,estado_uf")] Cidade cidade)
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
                    _cidadeRepositorio.inserirCidade(cidade);
                    _cidadeRepositorio.salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.");
            }
            ViewBag.estados = _cidadeRepositorio.getEstados();
            return View(cidade);
        }

        // GET: Cidade/Edit/5
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
            Cidade cidade = _cidadeRepositorio.getCidadePorId((int)id);
            if (cidade == null)
            {
                return HttpNotFound();
            }

            ViewBag.estados = _cidadeRepositorio.getEstados();
            return View(cidade);
        }

        // POST: Cidade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,estado_uf")] Cidade cidade)
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
                    _cidadeRepositorio.atualizaCidade(cidade);
                    _cidadeRepositorio.salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.");
            }

            ViewBag.estados = _cidadeRepositorio.getEstados();
            return View(cidade);
        }

        // GET: Cidade/Delete/5
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
            Cidade cidade = _cidadeRepositorio.getCidadePorId((int)id);
            if (cidade == null)
            {
                return HttpNotFound();
            }

            return View(cidade);
        }

        // POST: Cidade/Delete/5
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
                _cidadeRepositorio.deletarCidade(id);
                _cidadeRepositorio.salvar();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                  new System.Web.Routing.RouteValueDictionary {
               { "id", id },
               { "saveChangesError", true } });
            }
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

        // GET: Cidade/Find/5
        public JsonResult Find(int? id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            Cidade cidade = this._cidadeRepositorio.getCidadePorId((int)id);
            if (cidade == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(cidade, JsonRequestBehavior.AllowGet);
        }

        // GET: Cidade/Valida/5
        [WebMethod]
        public JsonResult Valida(int cidade_id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            var cidade = _cidadeRepositorio.getCidadePorId(cidade_id);
            if (cidade == null)
            {
                return Json("Cidade não encontrada", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private RedirectToRouteResult validaSessaoPermissoes() {
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
    }
}
