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
    public class FuncionalidadeController : Controller
    {
        private SCAContext db = new SCAContext();
        private FuncionalidadeRepositorio _funcionalidadeRepositorio;

        public FuncionalidadeController()
        {
            this._funcionalidadeRepositorio = new FuncionalidadeRepositorio(this.db);
        }
        // GET: Funcionalidade
        public ActionResult Index(string funcionalidade_nome = "")
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            var funcionalidades = this._funcionalidadeRepositorio.getFuncionalidades(funcionalidade_nome);
            if (Request.IsAjaxRequest())
                return PartialView("~/Views/Funcionalidade/_Selecionar_table.cshtml", funcionalidades);
            return View(funcionalidades);
        }

        // GET: Funcionalidade/Details/5
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
            Funcionalidade funcionalidade = _funcionalidadeRepositorio.getFuncionalidadePorId((int)id);
            if (funcionalidade == null)
            {
                return HttpNotFound();
            }
            Funcionalidade funcionalidade_pai = _funcionalidadeRepositorio.getFuncionalidadePorId((int)funcionalidade.id_pai);
            ViewBag.nome_pai = funcionalidade_pai == null ? "" : funcionalidade_pai.nome;
            return View(funcionalidade);
        }

        // GET: Funcionalidade/Create
        public ActionResult Create()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            return View();
        }

        // POST: Funcionalidade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,tipo,link,id_pai,nivel")] Funcionalidade funcionalidade)
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
                    if (funcionalidade.id_pai == null)
                        funcionalidade.id_pai = 0;
                    string classificacaoant = _funcionalidadeRepositorio.getUltimaClassificacaoFilha(((int)funcionalidade.id_pai));

                    string classificacao = string.Empty;
                    int aux = 0;
                    if (classificacaoant != null)
                    {
                        if (funcionalidade.id_pai != 0)
                        {
                            var funcionalidade_pai = _funcionalidadeRepositorio.getFuncionalidadePorId((int)funcionalidade.id_pai);
                            if (funcionalidade_pai.classificacao == classificacaoant)
                            {
                                classificacaoant += (!string.IsNullOrEmpty(classificacaoant) ? "." : "") + "000";
                            }
                        }
                        if (classificacaoant.IndexOf('.') >= 0)
                        {
                            var niveis = classificacaoant.Split('.');
                            int i = 0;
                            while (i < niveis.Length)
                            {
                                if (i < niveis.Length - 1)
                                    classificacao += (!string.IsNullOrEmpty(classificacao) ? "." : "") + niveis[i];
                                else
                                {
                                    aux = int.Parse(niveis[i]) + 1;
                                    classificacao += (!string.IsNullOrEmpty(classificacao) ? "." : "") + aux.ToString("000");
                                }
                                i++;
                            }
                        }
                        else
                        {
                            aux = int.Parse(classificacaoant) + 1;
                            classificacao = aux.ToString("000");
                        }
                    }
                    else {
                        aux = 1;
                        classificacao = aux.ToString("000");
                    }
                    funcionalidade.classificacao = classificacao;

                    _funcionalidadeRepositorio.inserirFuncionalidade(funcionalidade);
                    _funcionalidadeRepositorio.salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.");
            }
            return View(funcionalidade);
        }

        // GET: Funcionalidade/Edit/5
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
            Funcionalidade funcionalidade = _funcionalidadeRepositorio.getFuncionalidadePorId((int)id);
            if (funcionalidade == null)
            {
                return HttpNotFound();
            }

            Funcionalidade funcionalidade_pai = _funcionalidadeRepositorio.getFuncionalidadePorId((int)funcionalidade.id_pai);
            ViewBag.nome_pai = funcionalidade_pai == null ? "" : funcionalidade_pai.nome;
            return View(funcionalidade);
        }

        // POST: Funcionalidade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,tipo,link,id_pai,nivel,classificacao")] Funcionalidade funcionalidade)
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
                    if (funcionalidade.id_pai == null)
                        funcionalidade.id_pai = 0;
                    _funcionalidadeRepositorio.atualizaFuncionalidade(funcionalidade);
                    _funcionalidadeRepositorio.salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.");
            }

            return View(funcionalidade);
        }

        // GET: Funcionalidade/Delete/5
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
            Funcionalidade funcionalidade = _funcionalidadeRepositorio.getFuncionalidadePorId((int)id);
            if (funcionalidade == null)
            {
                return HttpNotFound();
            }
            Funcionalidade funcionalidade_pai = _funcionalidadeRepositorio.getFuncionalidadePorId((int)funcionalidade.id_pai);
            ViewBag.nome_pai = funcionalidade_pai == null ? "" : funcionalidade_pai.nome;
            return View(funcionalidade);
        }

        // POST: Funcionalidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            Funcionalidade funcionalidade = _funcionalidadeRepositorio.getFuncionalidadePorId(id);
            string classificacaoPai = _funcionalidadeRepositorio.getFuncionalidadePorId((int)funcionalidade.id_pai) != null ? _funcionalidadeRepositorio.getFuncionalidadePorId((int)funcionalidade.id_pai).classificacao : string.Empty;
            try
            {
                if (ModelState.IsValid) { 
                    _funcionalidadeRepositorio.deletarFuncionalidade(id);
                    _funcionalidadeRepositorio.salvar();

                    atualizaFuncionalidadesFilhas((int)funcionalidade.id_pai, classificacaoPai);
                    _funcionalidadeRepositorio.salvar();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Erro ao deletar: " + ex.Message);
            }

            return View(viewName:"Delete", model:funcionalidade);
            
        }

        // GET: Funcionalidade/Find/5
        public JsonResult Find(int id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            Funcionalidade funcionalidade = _funcionalidadeRepositorio.getFuncionalidadePorId(id);

            if (funcionalidade == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(funcionalidade, JsonRequestBehavior.AllowGet);
        }

        // GET: Funcionalidade/Valida/5
        public JsonResult Valida(int funcionalidade_id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            if (funcionalidade_id != 0)
            {
                Funcionalidade funcionalidade = _funcionalidadeRepositorio.getFuncionalidadePorId(funcionalidade_id);

                if (funcionalidade == null)
                {
                    return Json("Funcionalidade não encontrada", JsonRequestBehavior.AllowGet);
                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        // GET: Funcionalidade/ValidaPai/5
        public JsonResult ValidaPai(int id_pai)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            if (id_pai != 0)
            {
                Funcionalidade funcionalidade = _funcionalidadeRepositorio.getFuncionalidadePorId(id_pai);

                if (funcionalidade == null)
                {
                    return Json("Funcionalidade não encontrada", JsonRequestBehavior.AllowGet);
                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        //GET: Funcionalidade/ToUp/5
        public JsonResult ToUp(int id) {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }

            if (!Util.permiteActionEspecial(Session, Url.Action(actionName: "ToUp", controllerName: "Funcionalidade")))
            {
                return Json("Usuário não tem permissão para subir funcionalidades na estrutura", JsonRequestBehavior.AllowGet);
            }

            var funcionalidadeAtual = _funcionalidadeRepositorio.getFuncionalidadePorId(id);
            var funcionalidadeAnterior = _funcionalidadeRepositorio.getFuncionlidadeAnterior(id);
            if (funcionalidadeAnterior != null)
            {
                string aux = funcionalidadeAnterior.classificacao;
                funcionalidadeAnterior.classificacao = funcionalidadeAtual.classificacao;
                funcionalidadeAtual.classificacao = aux;

                _funcionalidadeRepositorio.atualizaFuncionalidade(funcionalidadeAnterior);
                _funcionalidadeRepositorio.atualizaFuncionalidade(funcionalidadeAtual);

                atualizaFuncionalidadesFilhas(funcionalidadeAnterior.id, funcionalidadeAnterior.classificacao);
                atualizaFuncionalidadesFilhas(funcionalidadeAtual.id, funcionalidadeAtual.classificacao);

                _funcionalidadeRepositorio.salvar();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(new { erro = "Funcionalidade já é a primeira da estrutura" }, JsonRequestBehavior.AllowGet);
        }

        private void atualizaFuncionalidadesFilhas(int id, string classificao_pai) {
            int aux = 0;
            var funcionalidades = _funcionalidadeRepositorio.getFuncionalidadesFilhas(id);
            foreach (var func in funcionalidades)
            {
                aux++;
                func.classificacao = (classificao_pai != null? classificao_pai + "." : "") +  aux.ToString("000");
                _funcionalidadeRepositorio.atualizaFuncionalidade(func);

                atualizaFuncionalidadesFilhas(func.id, func.classificacao);

            }
        }

        //GET: Funcionalidade/ToDown/5
        public JsonResult ToDown(int id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }

            if (!Util.permiteActionEspecial(Session, Url.Action(actionName: "ToDown", controllerName: "Funcionalidade")))
            {
                return Json("Usuário não tem permissão para descer funcionalidades na estrutura", JsonRequestBehavior.AllowGet);
            }

            var funcionalidadeAtual    = _funcionalidadeRepositorio.getFuncionalidadePorId(id);
            var funcionalidadeSeguinte = _funcionalidadeRepositorio.getFuncionlidadeSeguinte(id);
            if (funcionalidadeSeguinte != null)
            {
                string aux = funcionalidadeSeguinte.classificacao;
                funcionalidadeSeguinte.classificacao = funcionalidadeAtual.classificacao;
                funcionalidadeAtual.classificacao = aux;

                _funcionalidadeRepositorio.atualizaFuncionalidade(funcionalidadeSeguinte);
                _funcionalidadeRepositorio.atualizaFuncionalidade(funcionalidadeAtual);

                atualizaFuncionalidadesFilhas(funcionalidadeSeguinte.id, funcionalidadeSeguinte.classificacao);
                atualizaFuncionalidadesFilhas(funcionalidadeAtual.id, funcionalidadeAtual.classificacao);

                _funcionalidadeRepositorio.salvar();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Funcionalidade já é a última da estrutura"}, JsonRequestBehavior.AllowGet);
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
            ViewBag.ToUp = Util.permiteActionEspecial(Session, Url.Action(actionName:"ToUp", controllerName: "Funcionalidade"));
            ViewBag.ToDown = Util.permiteActionEspecial(Session, Url.Action(actionName: "ToDown", controllerName: "Funcionalidade"));

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
