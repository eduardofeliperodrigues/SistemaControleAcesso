using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class UsuarioRepositorio : IDisposable
    {
        private SCAContext _context;
        
        public UsuarioRepositorio(SCAContext context)
        {
            this._context = context;
        }

        public IEnumerable<Usuario> getUsuarios(string nome, string cidade_nome)
        {
            var usuarios = _context.usuario.Include(u => u.cidade);
            if (!String.IsNullOrEmpty(nome))
                usuarios = usuarios.Where(usuario => usuario.nome.Contains(nome));
            if (!String.IsNullOrEmpty(cidade_nome))
                usuarios = usuarios.Where(usuario => usuario.cidade.nome.Contains(cidade_nome));

            return usuarios;

        }
        public Usuario getUsuarioPorId( int id) {
            return this._context.usuario.Find(id);
        }

        public Usuario getUsuarioPorEmail(string email) {
            var usuarios = _context.usuario.AsQueryable();
            return usuarios.Where(usu => usu.email.Equals(email)).FirstOrDefault();
        }

        public void inserirUsuario(Usuario usuario)
        {
            this._context.usuario.Add(usuario);
        }

        public void atualizarUsuario(Usuario usuario) {
            this._context.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
        }

        public void deletarUsuario(int id) {
            Usuario usuario = this.getUsuarioPorId(id);
            this._context.usuario.Remove(usuario);
        }

        public void salvar()
        {
            this._context.SaveChanges();
        }

        public Usuario login(string email, string senha)
        {
            var usuario = _context.usuario.Where(a => a.email.Equals(email) && a.senha.Equals(senha)).FirstOrDefault();
            return usuario;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}