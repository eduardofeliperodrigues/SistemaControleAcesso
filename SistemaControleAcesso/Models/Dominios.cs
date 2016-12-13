using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControleAcesso.Models
{
    public class Dominios
    {
        public static string nome_sistema = "FS Sistemas";

        public static string N = "N";
        public static string S = "S";

        public static List<SelectListItem> SN = new List<SelectListItem> {
            new SelectListItem {Text = "SIM", Value = "S" },
            new SelectListItem {Text = "NÃO", Value = "N" }
        };
        public static List<SelectListItem> Funcionalidade_tipo = new List<SelectListItem> {
            new SelectListItem { Text = "1- NÃO CLICAVEL", Value = "1" },
            new SelectListItem { Text = "2- CLICAVEL", Value = "2" },
            new SelectListItem { Text = "3- AÇÃO ESPECIAL", Value = "3" }
        };
        public static string usuario_sessao = "usuario";
        public static string permissoes_sessao = "permissoes";
        public static string funcionalidades_sessao = "funcionalidades";
        public static string getActionNome(string action) {
            return action.Equals("Details") ? "consultar": action.Equals("Create") ? "inserir" : action.Equals("Edit") ? "editar" : action.Equals("Delete") ? "excluir": "";
        }

        //public static string DisplayName( Enum value)
        //{
        //    Type enumType = value.GetType();
        //    var enumValue = Enum.GetName(enumType, value);
        //    System.Reflection.MemberInfo member = enumType.GetMember(enumValue)[0];

        //    var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
        //    var outString = ((DisplayAttribute)attrs[0]).Name;

        //    if (((DisplayAttribute)attrs[0]).ResourceType != null)
        //    {
        //        outString = ((DisplayAttribute)attrs[0]).GetName();
        //    }

        //    return outString;
        //}


    }

    public enum Funcionalidade_tipo {
        [Display(Name = "1- NÃO CLICAVEL")]
        naoClicavel = 1,
        [Display(Name = "2- CLICAVEL")]
        clicavel = 2,
        [Display(Name = "3- AÇÃO ESPECIAL")]
        especial = 3
    }

    public enum SimNao {
        [Display(Name = "SIM")]
        S,
        [Display(Name = "NÃO")]
        N
    }

    public enum AtivoInativo {
        [Display(Name = "Ativo")]
        Ativo,
        [Display(Name = "Inativo")]
        Inativo
    }
    
}