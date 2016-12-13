using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class PerfilRepositorio: IDisposable
    {
        private SCAContext _context;

        public PerfilRepositorio(SCAContext context)
        {
            this._context = context;
        }

        public IEnumerable<Perfil> getPerfis(string nome = "")
        {
            var perfis = this._context.perfil.AsQueryable();
            if (!string.IsNullOrEmpty(nome))
                perfis = perfis.Where(Perfil => Perfil.nome.Contains(nome));
            return perfis;
        }
        public Perfil getPerfilPorId(int id)
        {
            return this._context.perfil.Find(id);
        }
        public void inserirPerfil(Perfil Perfil)
        {
            this._context.perfil.Add(Perfil);
        }

        public void deletarPerfil(int id)
        {
            Perfil Perfil = this._context.perfil.Find(id);
            this._context.perfil.Remove(Perfil);
        }

        public void atualizaPerfil(Perfil Perfil)
        {
            this._context.Entry(Perfil).State = System.Data.Entity.EntityState.Modified;
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