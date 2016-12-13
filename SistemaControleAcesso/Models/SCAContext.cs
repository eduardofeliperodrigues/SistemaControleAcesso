using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class SCAContext : DbContext
    {
        public DbSet<Estado> estado { get; set; }
        public DbSet<Cidade> cidade { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Funcionalidade> funcionalidade { get; set; }
        public DbSet<Perfil> perfil { get; set; }
        public DbSet<UsuarioPerfil> usuarioperfil { get; set; }
        public DbSet<Permissao> permissao { get; set; }

        public SCAContext() : base("name=DefaultConnection") { }
        protected override void OnModelCreating(DbModelBuilder dbmodelBuilder)
        {
            base.OnModelCreating(dbmodelBuilder);
            dbmodelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}