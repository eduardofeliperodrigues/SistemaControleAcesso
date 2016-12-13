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
    public class UsuarioController : Controller
    {
        private SCAContext db = new SCAContext();
        private UsuarioRepositorio _usuarioRepositorio;
        public UsuarioController()
        {
            this._usuarioRepositorio = new UsuarioRepositorio(db);
        }

        // GET: Usuario
        public ActionResult Index(string nome = "", string cidade_nome = "")
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            var usuarios = _usuarioRepositorio.getUsuarios(nome, cidade_nome);
            if (Request.IsAjaxRequest())
                return PartialView("~/Views/Usuario/_Selecionar_table.cshtml", usuarios);
            return View(usuarios);
        }

        // GET: Usuario/Details/5
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
            Usuario usuario = _usuarioRepositorio.getUsuarioPorId((int)id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            var sessaoValida = validaSessaoPermissoes();
            if (sessaoValida != null)
            {
                return sessaoValida;
            }
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,senha,nome,cpf,cidade_id,endereco,telefone,celular,email,status")] Usuario usuario)
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
                    this._usuarioRepositorio.inserirUsuario(usuario);
                    this._usuarioRepositorio.salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.");
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
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
            Usuario usuario = _usuarioRepositorio.getUsuarioPorId((int)id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,senha,nome,cpf,cidade_id,endereco,telefone,celular,email,status")] Usuario usuario)
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
                    this._usuarioRepositorio.atualizarUsuario(usuario);
                    this._usuarioRepositorio.salvar();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.");
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
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
            Usuario usuario = _usuarioRepositorio.getUsuarioPorId((int)id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
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
                this._usuarioRepositorio.deletarUsuario(id);
                this._usuarioRepositorio.salvar();
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

        // GET: Usuario/EditUsuario/&email=teste@teste.com
        public ActionResult EditUsuario(string email)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return usuarioValido;
            }
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuariologado = (Usuario)Session[Dominios.usuario_sessao];
            if (email != usuariologado.email) {
                return RedirectToAction(actionName: "Mensagem", controllerName: "Home", routeValues: new { mensagem = "Este usuário não tem permissão para editar o perfil do email " + email});
            }
            Usuario usuario = _usuarioRepositorio.getUsuarioPorEmail(email);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //POST: Usuário/EditUsuario/&email=teste@teste.com
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUsuario([Bind(Include = "id,senha,nome,cpf,cidade_id,endereco,telefone,celular,email,confirmasenha,status")] Usuario usuario)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return usuarioValido;
            }
            var usuariologado = (Usuario)Session[Dominios.usuario_sessao];
            if (usuario.id != usuariologado.id)
            {
                return RedirectToAction(actionName: "Mensagem", controllerName: "Home", routeValues: new { mensagem = "Este usuário não tem permissão para editar o perfil do usuario " + usuario.nome });
            }
            if (usuario.senha != usuario.confirmasenha) {
                ModelState.AddModelError("", "Senha e confirmação de senha devem ser iguais.");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    this._usuarioRepositorio.atualizarUsuario(usuario);
                    this._usuarioRepositorio.salvar();
                    return RedirectToAction(actionName:"Index", controllerName:"Home");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Não foi possível salvar! Tente novamente.\nErro:" + ex.Message);
            }
            return View(usuario);
        }


        // GET: Usuario/Valida/5
        public JsonResult Valida(int usuario_id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            Usuario usuario = _usuarioRepositorio.getUsuarioPorId(usuario_id);
            if (usuario == null)
            {
                return Json("Usuário não encontrado", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: Usuario/ValidaAtivo/5
        public JsonResult ValidaAtivo(int usuario_id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            Usuario usuario = _usuarioRepositorio.getUsuarioPorId(usuario_id);
            if (usuario == null)
            {
                return Json("Usuário não encontrado", JsonRequestBehavior.AllowGet);
            }
            else if (usuario.status == AtivoInativo.Inativo)
            {
                return Json("Usuário inativo", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: Usuario/Find/5
        public JsonResult Find(int id)
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return Json("Usuário não autenticado", JsonRequestBehavior.AllowGet);
            }
            Usuario usuario = _usuarioRepositorio.getUsuarioPorId(id);
            if (usuario == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            usuario.senha = string.Empty;
            return Json(usuario, JsonRequestBehavior.AllowGet);

        }

        // GET: Usuario/Login
        public ActionResult Login()
        {
            Usuario usuario = new Usuario();
            return View(usuario);
        }

        // POST: Usuario/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario usuario)
        {
            if (usuario.email.Equals("") || usuario.senha.Equals(""))
                ModelState.AddModelError("", "Email e senha são obrigatórios");

            if (ModelState.IsValid)
            {
                Usuario usu = _usuarioRepositorio.login(usuario.email, usuario.senha);
                if (usu == null)
                    ModelState.AddModelError("", "Email e/ou senha incorretos");
                else if ( usu.status == AtivoInativo.Inativo) 
                    ModelState.AddModelError("", "Usuário inativo");
                else
                {
                    Session[Dominios.usuario_sessao] = usu;
                    //As permissões serão recuperadas no momento do acesso
                    //Session[Dominios.permissoes_sessao] = (new PermissaoRepositorio(db)).getPermissoesUsuario(usu.id);
                    Session[Dominios.funcionalidades_sessao] = (new FuncionalidadeRepositorio(db)).getFuncionalidadesUsuario(usu.id); 
                    return RedirectToAction("Index", "Home");
                }

            }

            return View(usuario);
        }

        // POST: Usuario/Logout
        public ActionResult Logout()
        {
            Session[Dominios.usuario_sessao] = null;
            Session[Dominios.permissoes_sessao] = null;
            Session[Dominios.funcionalidades_sessao] = null;
            return RedirectToAction("Login", "Usuario");

        }

        [NonAction]
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
