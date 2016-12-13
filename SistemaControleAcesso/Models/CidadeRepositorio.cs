using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace SistemaControleAcesso.Models
{
    public class CidadeRepositorio: IDisposable
    {
        private SCAContext _context;

        public CidadeRepositorio(SCAContext context)
        {
            this._context = context;
        }
        public Cidade getCidadePorId(int id)
        {
            return this._context.cidade.Find(id);
        }

        public IEnumerable<Cidade> getCidades(string cidade_nome = "")
        {
            var cidades = this._context.cidade.Include(m => m.estado).AsQueryable();
            if (!string.IsNullOrEmpty(cidade_nome))
                cidades = cidades.Where(cidade => cidade.nome.Contains(cidade_nome));
            return cidades;
        }
        public void inserirCidade(Cidade cidade)
        {
            this._context.cidade.Add(cidade);
        }
        public void salvar()
        {
            this._context.SaveChanges();
        }
        public void atualizaCidade(Cidade cidade)
        {
            this._context.Entry(cidade).State = System.Data.Entity.EntityState.Modified;
        }
        public void deletarCidade(int id)
        {
            Cidade cidade = this.getCidadePorId(id);
            this._context.cidade.Remove(cidade);
        }

        public SelectList getEstados()
        {
            return new SelectList(this._context.estado, "uf", "uf");
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