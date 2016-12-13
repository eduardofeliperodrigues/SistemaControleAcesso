using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class PermissaoRepositorio : IDisposable
    {
        private SCAContext _context;
        
        public PermissaoRepositorio(SCAContext context)
        {
            this._context = context;
        }

        public IEnumerable<Permissao> getPermissoes()
        {
            var permissoes = _context.permissao.Include(p => p.funcionalidade).Include(u => u.perfil).Include(u => u.usuario);
            return permissoes;

        }

        public IEnumerable<Permissao> getPermissoesPorPerfil(int perfil_id)
        {
            var permissoes = _context.permissao.Where( p => p.perfil_id == perfil_id);
            return permissoes;

        }

        public IEnumerable<Permissao> getPermissoesPorUsuario(int usuario_id)
        {
            var permissoes = _context.permissao.Where(p => p.usuario_id == usuario_id);
            return permissoes;

        }

        public Permissao getPermissaoPorId( int id) {
            return this._context.permissao.Find(id);
        }

        public void inserirPermissao(Permissao Permissao)
        {
            this._context.permissao.Add(Permissao);
        }

        public void atualizarPermissao(Permissao Permissao) {
            this._context.Entry(Permissao).State = System.Data.Entity.EntityState.Modified;
        }

        public void deletarPermissao(int id) {
            Permissao Permissao = this.getPermissaoPorId(id);
            this._context.permissao.Remove(Permissao);
        }

        public void salvar()
        {
            this._context.SaveChanges();
        }

        public IEnumerable<Permissao> getPermissoesUsuario(int usuario_id)
        {
            List<Permissao> listaPermissao = new List<Permissao>();

            Perfil perfil = new Perfil();

            bool supervisor = false;
            foreach (var p in (new UsuarioPerfilRepositorio(_context)).getPerfisUsuario(usuario_id))
            {
                if (p.supervisor == SimNao.S)
                {
                    perfil = p;
                    supervisor = true;
                    break;
                }
                foreach (var permissao in (new PermissaoRepositorio(_context)).getPermissoesPorPerfil(p.id))
                {
                    listaPermissao.Add(permissao);
                }

            }
            if (!supervisor)
            {
                foreach (var permissao in (new PermissaoRepositorio(_context)).getPermissoesPorUsuario(usuario_id))
                {
                    foreach (var p in listaPermissao)
                    {
                        if (p.funcionalidade_id == permissao.funcionalidade_id)
                        {
                            listaPermissao.Remove(p);
                            break;
                        }
                    }
                    listaPermissao.Add(permissao);
                }
                
            }
            else
            {
                var permissao = new Permissao {
                    id = 0,
                    perfil = perfil,
                    consultar = SimNao.S,
                    inserir = SimNao.S,
                    alterar = SimNao.S,
                    excluir = SimNao.S
                };
                listaPermissao.Add(permissao);
            }

            return listaPermissao;
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