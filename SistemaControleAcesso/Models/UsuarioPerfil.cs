using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControleAcesso.Models
{
    public class UsuarioPerfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "Perfil é obrigatório")]
        [Remote("ValidaAtivo", "Perfil")]
        public int perfil_id { get; set; }

        [ForeignKey(name: "perfil_id")]
        public virtual Perfil perfil { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Usuário é obrigatório")]
        [Remote("ValidaAtivo", "Usuario")]
        public int usuario_id { get; set; }

        [ForeignKey(name: "usuario_id")]
        public virtual Usuario usuario { get; set; }

        [Display(Name = "Status")]
        public AtivoInativo status { get; set; }
    }
}