using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name ="Nome")]
        public string nome { get; set; }

        [Display(Name ="UF")]
        [Required(ErrorMessage ="Estado é obrigatório")]
        public string estado_uf { get; set; }

        [ForeignKey("estado_uf")]
        public virtual Estado estado { get; set; }

    }
}