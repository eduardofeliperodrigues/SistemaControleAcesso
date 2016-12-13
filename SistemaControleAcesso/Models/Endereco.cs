using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    [ComplexType]
    public class Endereco
    {
        [Display(Name = "Logradouro")]
        [MaxLength(50)]
        public string logradouro { get; set; }

        [Display(Name = "Número")]
        public int numero { get; set; }

        [Display(Name ="Complemento")]
        [MaxLength(length:20,ErrorMessage ="Tamanho máximo 20 caracteres")]
        public string complemento { get; set; }

        [Display(Name ="Bairro")]
        [MaxLength(length: 15, ErrorMessage = "Tamanho máximo 15 caracteres")]
        public string bairro { get; set; }

        [Display(Name ="CEP")]
        [MaxLength(length: 15, ErrorMessage = "Tamanho máximo 15 caracteres")]
        public string cep { get; set; }
    }
}