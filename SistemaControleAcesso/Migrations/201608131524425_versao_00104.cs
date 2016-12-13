namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00104 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuario", "cidade_id", "dbo.Cidade");
            DropIndex("dbo.Usuario", new[] { "cidade_id" });
            AlterColumn("dbo.Usuario", "cidade_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Usuario", "cidade_id");
            AddForeignKey("dbo.Usuario", "cidade_id", "dbo.Cidade", "id", cascadeDelete:false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "cidade_id", "dbo.Cidade");
            DropIndex("dbo.Usuario", new[] { "cidade_id" });
            AlterColumn("dbo.Usuario", "cidade_id", c => c.Int());
            CreateIndex("dbo.Usuario", "cidade_id");
            AddForeignKey("dbo.Usuario", "cidade_id", "dbo.Cidade", "id");
        }
    }
}
