namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00116 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionalidade", "classificacao", c => c.String(maxLength: 20));
            DropColumn("dbo.Funcionalidade", "ordem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funcionalidade", "ordem", c => c.Int(nullable: false));
            DropColumn("dbo.Funcionalidade", "classificacao");
        }
    }
}
