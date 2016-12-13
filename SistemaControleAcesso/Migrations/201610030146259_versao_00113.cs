namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00113 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Perfil", "supervisor", c => c.Int(nullable: true));
            AlterColumn("dbo.Permissao", "consultar", c => c.Int(nullable: true));
            AlterColumn("dbo.Permissao", "inserir", c => c.Int(nullable: true));
            AlterColumn("dbo.Permissao", "alterar", c => c.Int(nullable: true));
            AlterColumn("dbo.Permissao", "excluir", c => c.Int(nullable: true));
            AlterColumn("dbo.Permissao", "especial", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Permissao", "especial", c => c.String(maxLength: 1));
            AlterColumn("dbo.Permissao", "excluir", c => c.String(maxLength: 1));
            AlterColumn("dbo.Permissao", "alterar", c => c.String(maxLength: 1));
            AlterColumn("dbo.Permissao", "inserir", c => c.String(maxLength: 1));
            AlterColumn("dbo.Permissao", "consultar", c => c.String(maxLength: 1));
            AlterColumn("dbo.Perfil", "supervisor", c => c.String(maxLength: 1));
        }
    }
}
