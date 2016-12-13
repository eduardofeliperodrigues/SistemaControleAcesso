namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00111 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Permissao", "funcionalidade_id", "dbo.Funcionalidade");
            DropForeignKey("dbo.Permissao", "perfil_id", "dbo.Perfil");
            DropForeignKey("dbo.Permissao", "usuario_id", "dbo.Usuario");
            DropIndex("dbo.Permissao", new[] { "perfil_id" });
            DropIndex("dbo.Permissao", new[] { "usuario_id" });
            DropIndex("dbo.Permissao", new[] { "funcionalidade_id" });
            AlterColumn("dbo.Permissao", "perfil_id", c => c.Int());
            AlterColumn("dbo.Permissao", "usuario_id", c => c.Int());
            AlterColumn("dbo.Permissao", "funcionalidade_id", c => c.Int());
            CreateIndex("dbo.Permissao", "perfil_id");
            CreateIndex("dbo.Permissao", "usuario_id");
            CreateIndex("dbo.Permissao", "funcionalidade_id");
            AddForeignKey("dbo.Permissao", "funcionalidade_id", "dbo.Funcionalidade", "id");
            AddForeignKey("dbo.Permissao", "perfil_id", "dbo.Perfil", "id");
            AddForeignKey("dbo.Permissao", "usuario_id", "dbo.Usuario", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissao", "usuario_id", "dbo.Usuario");
            DropForeignKey("dbo.Permissao", "perfil_id", "dbo.Perfil");
            DropForeignKey("dbo.Permissao", "funcionalidade_id", "dbo.Funcionalidade");
            DropIndex("dbo.Permissao", new[] { "funcionalidade_id" });
            DropIndex("dbo.Permissao", new[] { "usuario_id" });
            DropIndex("dbo.Permissao", new[] { "perfil_id" });
            AlterColumn("dbo.Permissao", "funcionalidade_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Permissao", "usuario_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Permissao", "perfil_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Permissao", "funcionalidade_id");
            CreateIndex("dbo.Permissao", "usuario_id");
            CreateIndex("dbo.Permissao", "perfil_id");
            AddForeignKey("dbo.Permissao", "usuario_id", "dbo.Usuario", "id", cascadeDelete: true);
            AddForeignKey("dbo.Permissao", "perfil_id", "dbo.Perfil", "id", cascadeDelete: true);
            AddForeignKey("dbo.Permissao", "funcionalidade_id", "dbo.Funcionalidade", "id", cascadeDelete: true);
        }
    }
}
