using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class Estado
    {
        [Key]
        [MaxLength(2)]
        [Display(Name ="UF")]
        [Required(ErrorMessage ="UF é obrigatória")]
        public string uf { get; set; }
        [Display(Name = "Nome")]
        [MaxLength(30)]
        public string nome { get; set; }
    }
}