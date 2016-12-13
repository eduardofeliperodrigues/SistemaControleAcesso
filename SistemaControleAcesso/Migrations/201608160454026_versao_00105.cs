namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00105 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "email", c => c.String(maxLength: 100));
            AddColumn("dbo.Usuario", "telefone", c => c.String(maxLength: 15));
            AddColumn("dbo.Usuario", "celular", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "celular");
            DropColumn("dbo.Usuario", "telefone");
            DropColumn("dbo.Usuario", "email");
        }
    }
}
