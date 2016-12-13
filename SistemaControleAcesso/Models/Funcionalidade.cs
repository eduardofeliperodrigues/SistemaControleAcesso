using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControleAcesso.Models
{
    public class Funcionalidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        //[MaxLength(20)]
        [Display(Name = "Tipo")]
        public Funcionalidade_tipo tipo { get; set; }

        [MaxLength(254)]
        [Display(Name = "Link")]
        public string link { get; set; }

        [Display(Name = "Funcionalidade pai")]
        [Remote("ValidaPai", "Funcionalidade")]
        public int? id_pai { get; set; }
        
        [NotMapped]
        public int qtdFilhos { get; set; }

        [Display(Name = "Classificação")]
        [MaxLength(20)]
        public string classificacao { get; set; }

        [Display(Name ="Nível")]
        public int nivel { get; set; }

        public bool isClicavel() {
            return this.tipo == Funcionalidade_tipo.clicavel; // Verificar Dominio.Funcionalidade_tipo
        }
    }
}