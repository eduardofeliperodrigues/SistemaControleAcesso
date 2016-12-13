using SistemaControleAcesso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControleAcesso.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var usuarioValido = validaSessaoUsuario();
            if (usuarioValido != null)
            {
                return usuarioValido;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Mensagem(string mensagem)
        {
            ViewBag.mensagem = mensagem;
            return View();
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