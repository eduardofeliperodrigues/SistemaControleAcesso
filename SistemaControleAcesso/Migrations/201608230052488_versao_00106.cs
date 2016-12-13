namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00106 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionalidade", "ordem", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funcionalidade", "ordem");
        }
    }
}
