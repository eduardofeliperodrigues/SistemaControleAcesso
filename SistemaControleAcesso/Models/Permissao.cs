using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControleAcesso.Models
{
    public class Permissao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Consultar")]
        public SimNao consultar { get; set; }

        [Display(Name = "Inserir")]
        public SimNao inserir { get; set; }

        [Display(Name = "Alterar")]
        public SimNao alterar { get; set; }

        [Display(Name = "Excluir")]
        public SimNao excluir { get; set; }

        [Display(Name = "Especial")]
        public SimNao especial { get; set; }

        [Display(Name ="Perfil")]
        [Remote("ValidaAtivo", "Perfil")]
        public int? perfil_id { get; set; }

        [ForeignKey("perfil_id")]
        public virtual Perfil perfil { get; set; }

        [Display(Name = "Usuário")]
        [Remote("ValidaAtivo", "Usuario")]
        public int? usuario_id { get; set; }

        [ForeignKey("usuario_id")]
        public virtual Usuario usuario { get; set; }

        [Display(Name = "Funcionalidade")]
        [Required(ErrorMessage = "Funcionalidade é obrigatória")]
        [Remote("Valida", "Funcionalidade")]
        public int? funcionalidade_id { get; set; }

        [ForeignKey("funcionalidade_id")]
        public virtual Funcionalidade funcionalidade { get; set; }
    }
}