namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00115 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Funcionalidade", "id_pai", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Funcionalidade", "id_pai", c => c.Int(nullable: false));
        }
    }
}
