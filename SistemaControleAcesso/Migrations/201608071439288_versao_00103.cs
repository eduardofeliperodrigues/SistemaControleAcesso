namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00103 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cidade", "estado_uf", "dbo.Estado");
            DropIndex("dbo.Cidade", new[] { "estado_uf" });
            AlterColumn("dbo.Cidade", "estado_uf", c => c.String(nullable: false, maxLength: 2));
            CreateIndex("dbo.Cidade", "estado_uf");
            AddForeignKey("dbo.Cidade", "estado_uf", "dbo.Estado", "uf", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cidade", "estado_uf", "dbo.Estado");
            DropIndex("dbo.Cidade", new[] { "estado_uf" });
            AlterColumn("dbo.Cidade", "estado_uf", c => c.String(maxLength: 2));
            CreateIndex("dbo.Cidade", "estado_uf");
            AddForeignKey("dbo.Cidade", "estado_uf", "dbo.Estado", "uf");
        }
    }
}
