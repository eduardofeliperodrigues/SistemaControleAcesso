namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00108 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsuarioPerfil", "perfil_id", "dbo.Perfil");
            DropForeignKey("dbo.UsuarioPerfil", "usuario_id", "dbo.Usuario");
            DropIndex("dbo.UsuarioPerfil", new[] { "perfil_id" });
            DropIndex("dbo.UsuarioPerfil", new[] { "usuario_id" });
            AlterColumn("dbo.UsuarioPerfil", "perfil_id", c => c.Int(nullable: false));
            AlterColumn("dbo.UsuarioPerfil", "usuario_id", c => c.Int(nullable: false));
            CreateIndex("dbo.UsuarioPerfil", "perfil_id");
            CreateIndex("dbo.UsuarioPerfil", "usuario_id");
            AddForeignKey("dbo.UsuarioPerfil", "perfil_id", "dbo.Perfil", "id", cascadeDelete: false);
            AddForeignKey("dbo.UsuarioPerfil", "usuario_id", "dbo.Usuario", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioPerfil", "usuario_id", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioPerfil", "perfil_id", "dbo.Perfil");
            DropIndex("dbo.UsuarioPerfil", new[] { "usuario_id" });
            DropIndex("dbo.UsuarioPerfil", new[] { "perfil_id" });
            AlterColumn("dbo.UsuarioPerfil", "usuario_id", c => c.Int());
            AlterColumn("dbo.UsuarioPerfil", "perfil_id", c => c.Int());
            CreateIndex("dbo.UsuarioPerfil", "usuario_id");
            CreateIndex("dbo.UsuarioPerfil", "perfil_id");
            AddForeignKey("dbo.UsuarioPerfil", "usuario_id", "dbo.Usuario", "id");
            AddForeignKey("dbo.UsuarioPerfil", "perfil_id", "dbo.Perfil", "id");
        }
    }
}
