namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00110 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissao", "funcionalidade_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Permissao", "funcionalidade_id");
            AddForeignKey("dbo.Permissao", "funcionalidade_id", "dbo.Funcionalidade", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissao", "funcionalidade_id", "dbo.Funcionalidade");
            DropIndex("dbo.Permissao", new[] { "funcionalidade_id" });
            DropColumn("dbo.Permissao", "funcionalidade_id");
        }
    }
}
