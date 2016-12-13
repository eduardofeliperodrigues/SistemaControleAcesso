using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class Perfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="ID")]
        public int id { get; set; }

        [MaxLength(30)]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Display(Name = "Supervisor")]
        public SimNao supervisor { get; set; }

        [Display(Name = "Status")]
        public AtivoInativo status { get; set; }
    }
}