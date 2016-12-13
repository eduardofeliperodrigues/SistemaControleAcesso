namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00109 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        consultar = c.String(maxLength: 1),
                        inserir = c.String(maxLength: 1),
                        alterar = c.String(maxLength: 1),
                        excluir = c.String(maxLength: 1),
                        especial = c.String(maxLength: 1),
                        perfil_id = c.Int(nullable: false),
                        usuario_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Perfil", t => t.perfil_id, cascadeDelete: false)
                .ForeignKey("dbo.Usuario", t => t.usuario_id, cascadeDelete: false)
                .Index(t => t.perfil_id)
                .Index(t => t.usuario_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissao", "usuario_id", "dbo.Usuario");
            DropForeignKey("dbo.Permissao", "perfil_id", "dbo.Perfil");
            DropIndex("dbo.Permissao", new[] { "usuario_id" });
            DropIndex("dbo.Permissao", new[] { "perfil_id" });
            DropTable("dbo.Permissao");
        }
    }
}
