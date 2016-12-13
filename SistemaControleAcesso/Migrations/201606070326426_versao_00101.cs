namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00101 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Perfil", name: "perfil_nome", newName: "nome");
            RenameColumn(table: "dbo.Perfil", name: "perfil_supervisor", newName: "supervisor");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Perfil", name: "supervisor", newName: "perfil_supervisor");
            RenameColumn(table: "dbo.Perfil", name: "nome", newName: "perfil_nome");
        }
    }
}
