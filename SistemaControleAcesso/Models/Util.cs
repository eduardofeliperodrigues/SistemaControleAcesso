using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class Util
    {
        public static System.Web.Mvc.RedirectToRouteResult validaSessao(HttpSessionStateBase session, string url)
        {
            if (session[SistemaControleAcesso.Models.Dominios.usuario_sessao] == null)
            {
                return new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                       {
                           { "action", "Login" },
                           { "controller", "Usuario" }
                       });
            }
            else
            {
                string mensagem = string.Empty;
                string controller = url.Split('/')[1];
                string action = url.Split('/').Length >= 3 ? url.Split('/')[2] == "Index" ? "Details" : url.Split('/')[2] : "Details";
                string nome_funcionalidade = controller;
                bool achou = false;

                var usuario = ((Usuario)session[SistemaControleAcesso.Models.Dominios.usuario_sessao]);
                foreach (var permissao in (new PermissaoRepositorio(new SCAContext())).getPermissoesUsuario(usuario.id))
                {
                    if (permissao.perfil != null)
                    {
                        if (permissao.perfil.supervisor == SistemaControleAcesso.Models.SimNao.S)
                        {
                            mensagem = string.Empty;
                            achou = true;
                            break;
                        }
                    }

                    if (permissao.funcionalidade.link.Split('/')[1] == controller)
                    {
                        achou = true;
                        if (String.IsNullOrEmpty(action) && permissao.consultar == SistemaControleAcesso.Models.SimNao.S)
                        {
                            mensagem = string.Empty;
                            break;
                        }
                        else
                        {
                            string actionFuncionalidade = permissao.funcionalidade.link.Split('/').Length >= 3 ? permissao.funcionalidade.link.Split('/')[2] : "";
                            if ((((permissao.consultar == SistemaControleAcesso.Models.SimNao.S && action == "Details") ||
                                (permissao.inserir == SistemaControleAcesso.Models.SimNao.S && action == "Create") ||
                                (permissao.alterar == SistemaControleAcesso.Models.SimNao.S && action == "Edit") ||
                                (permissao.excluir == SistemaControleAcesso.Models.SimNao.S && action == "Delete")) && 
                                permissao.funcionalidade.tipo != SistemaControleAcesso.Models.Funcionalidade_tipo.especial) ||
                                (permissao.especial == SistemaControleAcesso.Models.SimNao.S && action == actionFuncionalidade && permissao.funcionalidade.tipo == SistemaControleAcesso.Models.Funcionalidade_tipo.especial))
                            {
                                mensagem = string.Empty;
                                break;
                            }
                            else
                            {
                                if (action == actionFuncionalidade) { 
                                    nome_funcionalidade = permissao.funcionalidade.nome;
                                }
                                achou = false;
                            }
                        }


                    }

                }
                if (!achou)
                {
                    mensagem = "Usuário não tem permissão para " + SistemaControleAcesso.Models.Dominios.getActionNome(action) + " " + nome_funcionalidade;
                }

                if (string.IsNullOrEmpty(mensagem))
                {
                    return null;
                }
                else
                {
                    return new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                       {
                           { "action", "Mensagem" },
                           { "controller", "Home" },
                           { "mensagem", mensagem }
                       });

                    //RedirectToAction("Mensagem", "Default", new { mensagem = mensagem });
                }
            }

        }

        public static bool permiteActionEspecial(HttpSessionStateBase session, string url)
        {
            string controller = url.Split('/')[1];
            string action = url.Split('/').Length >= 3 ? url.Split('/')[2] : "";

            return valida(session, url, action);
        }

        public static bool permiteAction(HttpSessionStateBase session, string url, string action)
        {            
            return valida(session, url, action);
        }
        private static bool valida(HttpSessionStateBase session, string url, string action)
        {
            string controller = url.Split('/')[1];
            bool achou = false;

            if (!(session[SistemaControleAcesso.Models.Dominios.usuario_sessao] == null))
            {
                var usuario = ((Usuario)session[SistemaControleAcesso.Models.Dominios.usuario_sessao]);
                foreach (var permissao in (new PermissaoRepositorio(new SCAContext())).getPermissoesUsuario(usuario.id))
                {
                    if (permissao.perfil != null)
                    {
                        if (permissao.perfil.supervisor == SistemaControleAcesso.Models.SimNao.S)
                        {
                            achou = true;
                            break;
                        }
                    }

                    string controllerFuncionalidade = permissao.funcionalidade.link.Split('/')[1];
                    string actionFuncionalidade = permissao.funcionalidade.link.Split('/').Length >= 3 ? permissao.funcionalidade.link.Split('/')[2] : "";

                    if (controllerFuncionalidade == controller)
                    {
                        if ((((permissao.consultar == SistemaControleAcesso.Models.SimNao.S && action == "Details") ||
                            (permissao.inserir == SistemaControleAcesso.Models.SimNao.S && action == "Create") ||
                            (permissao.alterar == SistemaControleAcesso.Models.SimNao.S && action == "Edit") ||
                            (permissao.excluir == SistemaControleAcesso.Models.SimNao.S && action == "Delete")) && 
                            permissao.funcionalidade.tipo != SistemaControleAcesso.Models.Funcionalidade_tipo.especial) ||
                            (permissao.especial == SistemaControleAcesso.Models.SimNao.S && action == actionFuncionalidade &&
                            permissao.funcionalidade.tipo == SistemaControleAcesso.Models.Funcionalidade_tipo.especial))
                        {
                            achou = true;
                            break;
                        }
                        else
                        {
                            achou = false;
                        }
                    }

                }
            }
            return achou;
        }

        public static List<SistemaControleAcesso.Models.Funcionalidade> getCaminhoFuncionalidade(HttpSessionStateBase session, string url)
        {
            List<SistemaControleAcesso.Models.Funcionalidade> lista = new List<SistemaControleAcesso.Models.Funcionalidade>();

            var item = ((List<SistemaControleAcesso.Models.Funcionalidade>)session[SistemaControleAcesso.Models.Dominios.funcionalidades_sessao]).Find(func => !String.IsNullOrEmpty(func.link) && func.link.Split('/')[1] == url.Split('/')[1]);
            if (item != null)
            {
                if (item.id_pai != 0)
                {
                    addPai((int)item.id_pai, session, ref lista);

                }

                bool achou = false;
                foreach (var func in lista)
                {
                    if (func.id == item.id)
                    {
                        achou = true;
                        break;
                    }
                }
                if (!achou)
                {
                    lista.Add(item);
                }
            }
            return lista;
        }
        private static void addPai(int id_pai, HttpSessionStateBase session, ref List<SistemaControleAcesso.Models.Funcionalidade> lista)
        {
            var aux = ((List<SistemaControleAcesso.Models.Funcionalidade>)session[SistemaControleAcesso.Models.Dominios.funcionalidades_sessao]).Find(func => func.id == id_pai);
            if (aux != null)
            {
                bool achou = false;
                foreach (var func in lista)
                {
                    if (func.id == aux.id)
                    {
                        achou = true;
                        break;
                    }
                }
                if (!achou)
                {
                    if (aux.id_pai != 0)
                    {
                        addPai((int)aux.id_pai, session, ref lista);
                    }
                    lista.Add(aux);
                }

            }

        }
        public static System.Web.Mvc.RedirectToRouteResult validaUsuarioLogado(HttpSessionStateBase session)
        {
            if (session[SistemaControleAcesso.Models.Dominios.usuario_sessao] == null)
            {
                return new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                       {
                           { "action", "Login" },
                           { "controller", "Usuario" }
                       });
            }
            return null;

        }
        public static string displayNameEnum(Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            System.Reflection.MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
            var outString = ((System.ComponentModel.DataAnnotations.DisplayAttribute)attrs[0]).Name;

            if (((System.ComponentModel.DataAnnotations.DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((System.ComponentModel.DataAnnotations.DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }
    }
}