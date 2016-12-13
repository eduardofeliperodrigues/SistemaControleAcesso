namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00102 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cidade", name: "cidade_id", newName: "id");
            RenameColumn(table: "dbo.Estado", name: "estado_uf", newName: "uf");
            RenameColumn(table: "dbo.Funcionalidade", name: "funcionalidade_id", newName: "id");
            RenameColumn(table: "dbo.Funcionalidade", name: "funcionalidade_id_pai", newName: "id_pai");
            RenameColumn(table: "dbo.Perfil", name: "perfil_id", newName: "id");
            RenameColumn(table: "dbo.Usuario", name: "usuario_id", newName: "id");
            RenameColumn(table: "dbo.UsuarioPerfil", name: "usuarioperfil_id", newName: "id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.UsuarioPerfil", name: "id", newName: "usuarioperfil_id");
            RenameColumn(table: "dbo.Usuario", name: "id", newName: "usuario_id");
            RenameColumn(table: "dbo.Perfil", name: "id", newName: "perfil_id");
            RenameColumn(table: "dbo.Funcionalidade", name: "id_pai", newName: "funcionalidade_id_pai");
            RenameColumn(table: "dbo.Funcionalidade", name: "id", newName: "funcionalidade_id");
            RenameColumn(table: "dbo.Estado", name: "uf", newName: "estado_uf");
            RenameColumn(table: "dbo.Cidade", name: "id", newName: "cidade_id");
        }
    }
}
