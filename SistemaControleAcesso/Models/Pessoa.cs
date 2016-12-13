using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControleAcesso.Models
{
    public abstract class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name ="Nome")]
        [MaxLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres")]
        public string nome { get; set; }

        [Display(Name = "CPF")]
        [MaxLength(15, ErrorMessage = "Tamanho máximo de 15 caracteres")]
        public string cpf { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Cidade é obrigatória")]
        [Remote("Valida", "Cidade")]
        public int cidade_id { get; set; }

        public Endereco endereco { get; set; }

        [ForeignKey("cidade_id")]
        public virtual Cidade cidade { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido.")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string email { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(15, ErrorMessage = "Tamanho máximo de 15 caracteres")]
        public string telefone { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(15, ErrorMessage = "Tamanho máximo de 15 caracteres")]
        public string celular { get; set; }

        [Display(Name = "Status")]
        public AtivoInativo status { get; set; }

        public Pessoa()
        {
            this.endereco = new Endereco();
        }

    }
}