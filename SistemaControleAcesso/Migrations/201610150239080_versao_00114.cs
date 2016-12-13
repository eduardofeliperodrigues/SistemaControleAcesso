namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00114 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perfil", "status", c => c.Int(nullable: true));
            AddColumn("dbo.Usuario", "status", c => c.Int(nullable: true));
            AddColumn("dbo.UsuarioPerfil", "status", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsuarioPerfil", "status");
            DropColumn("dbo.Usuario", "status");
            DropColumn("dbo.Perfil", "status");
        }
    }
}
