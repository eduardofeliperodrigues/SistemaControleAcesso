namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00112 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Funcionalidade", "tipo", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Funcionalidade", "tipo", c => c.String(maxLength: 20));
        }
    }
}
