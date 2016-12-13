namespace SistemaControleAcesso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versao_00100 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        cidade_id = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        estado_uf = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.cidade_id)
                .ForeignKey("dbo.Estado", t => t.estado_uf)
                .Index(t => t.estado_uf);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        estado_uf = c.String(nullable: false, maxLength: 2),
                        nome = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.estado_uf);
            
            CreateTable(
                "dbo.Funcionalidade",
                c => new
                    {
                        funcionalidade_id = c.Int(nullable: false, identity: true),
                        nome = c.String(maxLength: 50),
                        tipo = c.String(maxLength: 20),
                        link = c.String(maxLength: 254),
                        funcionalidade_id_pai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.funcionalidade_id);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        perfil_id = c.Int(nullable: false, identity: true),
                        perfil_nome = c.String(maxLength: 30),
                        perfil_supervisor = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.perfil_id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        usuario_id = c.Int(nullable: false, identity: true),
                        senha = c.String(),
                        nome = c.String(maxLength: 50),
                        cpf = c.String(maxLength: 15),
                        endereco_logradouro = c.String(maxLength: 50),
                        endereco_numero = c.Int(nullable: false),
                        endereco_complemento = c.String(maxLength: 20),
                        endereco_bairro = c.String(maxLength: 15),
                        endereco_cep = c.String(maxLength: 15),
                        cidade_id = c.Int(),
                    })
                .PrimaryKey(t => t.usuario_id)
                .ForeignKey("dbo.Cidade", t => t.cidade_id)
                .Index(t => t.cidade_id);
            
            CreateTable(
                "dbo.UsuarioPerfil",
                c => new
                    {
                        usuarioperfil_id = c.Int(nullable: false, identity: true),
                        perfil_id = c.Int(),
                        usuario_id = c.Int(),
                    })
                .PrimaryKey(t => t.usuarioperfil_id)
                .ForeignKey("dbo.Perfil", t => t.perfil_id)
                .ForeignKey("dbo.Usuario", t => t.usuario_id)
                .Index(t => t.perfil_id)
                .Index(t => t.usuario_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioPerfil", "usuario_id", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioPerfil", "perfil_id", "dbo.Perfil");
            DropForeignKey("dbo.Usuario", "cidade_id", "dbo.Cidade");
            DropForeignKey("dbo.Cidade", "estado_uf", "dbo.Estado");
            DropIndex("dbo.UsuarioPerfil", new[] { "usuario_id" });
            DropIndex("dbo.UsuarioPerfil", new[] { "perfil_id" });
            DropIndex("dbo.Usuario", new[] { "cidade_id" });
            DropIndex("dbo.Cidade", new[] { "estado_uf" });
            DropTable("dbo.UsuarioPerfil");
            DropTable("dbo.Usuario");
            DropTable("dbo.Perfil");
            DropTable("dbo.Funcionalidade");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
        }
    }
}
