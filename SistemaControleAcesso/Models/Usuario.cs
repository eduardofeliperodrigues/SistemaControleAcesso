using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class Usuario : Pessoa
    {

        [DataType(DataType.Password)]
        [Display(Name ="Senha")]
        public string senha { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        public string confirmasenha { get; set; }
    }
}