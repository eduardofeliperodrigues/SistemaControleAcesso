using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class UsuarioPerfilRepositorio : IDisposable
    {
        private SCAContext _context;
        
        public UsuarioPerfilRepositorio(SCAContext context)
        {
            this._context = context;
        }

        public IEnumerable<UsuarioPerfil> getUsuarioPerfis()
        {
            var usuariosperfil = _context.usuarioperfil.Include(u => u.perfil).Include(u => u.usuario);
            return usuariosperfil;

        }
        public IEnumerable<Perfil> getPerfisUsuario(int usuario_id) {
            List<Perfil> listaPerfis = new List<Perfil>();
            foreach (var usuariosPerfil in _context.usuarioperfil.Include(u => u.perfil).Where(u => u.usuario_id == usuario_id).OrderBy(u => u.perfil.supervisor)) {
                listaPerfis.Add(usuariosPerfil.perfil);
            }
            return listaPerfis;
        }
        public UsuarioPerfil getUsuarioPerfilPorId( int id) {
            return this._context.usuarioperfil.Find(id);
        }

        public void inserirUsuarioPerfil(UsuarioPerfil usuarioPerfil)
        {
            this._context.usuarioperfil.Add(usuarioPerfil);
        }

        public void atualizarUsuarioPerfil(UsuarioPerfil usuarioPerfil) {
            this._context.Entry(usuarioPerfil).State = System.Data.Entity.EntityState.Modified;
        }

        public void deletarUsuarioPerfil(int id) {
            UsuarioPerfil usuarioPerfil = this.getUsuarioPerfilPorId(id);
            this._context.usuarioperfil.Remove(usuarioPerfil);
        }

        public void salvar()
        {
            this._context.SaveChanges();
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