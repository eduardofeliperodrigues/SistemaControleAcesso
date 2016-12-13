namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00107 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionalidade", "nivel", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funcionalidade", "nivel");
        }
    }
}
