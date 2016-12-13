using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaControleAcesso.Models
{
    public class FuncionalidadeRepositorio : IDisposable
    {
        private SCAContext _context;

        public FuncionalidadeRepositorio(SCAContext context)
        {
            this._context = context;
        }

        public IEnumerable<Funcionalidade> getFuncionalidades(string nome = "")
        {
            var funcionalidades = this._context.funcionalidade.AsQueryable();
            if (!string.IsNullOrEmpty(nome))
                funcionalidades = funcionalidades.Where(funcionalidade => funcionalidade.nome.Contains(nome));
            funcionalidades = funcionalidades.OrderBy(funcionalidade => funcionalidade.classificacao);
            List<Funcionalidade> lista = new List<Funcionalidade>();
            foreach (var item in funcionalidades)
            {
                if (item.id_pai != 0)
                {
                    addPai((int)item.id_pai, ref lista);

                }

                bool achou = false;
                foreach (var func in lista)
                {
                    if (func.id == item.id)
                    {
                        achou = true;
                        break;
                    }
                }
                if (!achou)
                {
                    lista.Add(item);
                }


            }
            var aux = lista.AsQueryable();

            return aux.OrderBy(item => item.classificacao).ToList();
        }
        public Funcionalidade getFuncionalidadePorId(int id)
        {
            return this._context.funcionalidade.Find(id);
        }
        public void inserirFuncionalidade(Funcionalidade funcionalidade)
        {
            this._context.funcionalidade.Add(funcionalidade);
        }

        public void deletarFuncionalidade(int id)
        {
            Funcionalidade funcionalidade = this.getFuncionalidadePorId(id);
            this._context.funcionalidade.Remove(funcionalidade);
        }

        public void atualizaFuncionalidade(Funcionalidade funcionalidade)
        {
            this._context.Entry(funcionalidade).State = System.Data.Entity.EntityState.Modified;
        }

        public void salvar()
        {
            this._context.SaveChanges();
        }

        private void addPai(int id_pai, ref List<Funcionalidade> lista)
        {
            var aux = this._context.funcionalidade.Find(id_pai);
            if (aux != null)
            {
                bool achou = false;
                foreach (var func in lista)
                {
                    if (func.id == aux.id)
                    {
                        achou = true;
                        break;
                    }
                }
                if (!achou)
                {
                    if (aux.id_pai != 0)
                    {
                        addPai((int)aux.id_pai, ref lista);
                    }
                    lista.Add(aux);
                }

            }

        }

        public IEnumerable<Funcionalidade> getFuncionalidadesUsuario(int usuario_id)
        {
            List<Permissao> listaPermissao = new List<Permissao>();
            List<Funcionalidade> lista = new List<Funcionalidade>();
            bool supervisor = false;
            foreach (var perfil in (new UsuarioPerfilRepositorio(_context)).getPerfisUsuario(usuario_id))
            {
                if (perfil.supervisor == SimNao.S)
                {
                    supervisor = true;
                    break;
                }
                foreach (var permissao in (new PermissaoRepositorio(_context)).getPermissoesPorPerfil(perfil.id))
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
                        if (p.id == permissao.id)
                        {
                            listaPermissao.Remove(p);
                            break;
                        }
                    }
                    listaPermissao.Add(permissao);
                }

                foreach (var permissao in listaPermissao)
                {
                    var func = getFuncionalidadePorId((int)permissao.funcionalidade_id);
                    if (func.id_pai != 0)
                    {
                        addPai((int)func.id_pai, ref lista);

                    }

                    bool achou = false;
                    foreach (var itm in lista)
                    {
                        if (itm.id == func.id)
                        {
                            achou = true;
                            break;
                        }
                    }
                    if (!achou)
                    {
                        lista.Add(func);
                    }

                }
                var aux = lista.AsQueryable();
                lista = aux.OrderBy(item => item.classificacao).ToList();
            }
            else
            {
                lista = getFuncionalidades(null).ToList();
            }

            return lista;
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

        public string getUltimaClassificacaoFilha(int id)
        {
            var funcionalidades = _context.funcionalidade.AsQueryable();
            if (id != 0)
            {
                var funcionalidadePai = this.getFuncionalidadePorId(id);
                funcionalidades = funcionalidades.Where(func => func.classificacao.StartsWith(funcionalidadePai.classificacao));
            }
            
            funcionalidades = funcionalidades.OrderByDescending(func => func.classificacao);
            return funcionalidades.FirstOrDefault().classificacao;
        }

        public Funcionalidade getFuncionlidadeSeguinte(int id)
        {
            var funcionalidades = _context.funcionalidade.AsQueryable();

            var funcionalidadeAtual = this.getFuncionalidadePorId(id);
            funcionalidades = funcionalidades.Where(func => string.Compare(func.classificacao, funcionalidadeAtual.classificacao) > 0);
            funcionalidades = funcionalidades.Where(func => func.id_pai == funcionalidadeAtual.id_pai);

            funcionalidades = funcionalidades.OrderBy(func => func.classificacao);
            return funcionalidades.FirstOrDefault();
            
        }

        public Funcionalidade getFuncionlidadeAnterior(int id)
        {
            var funcionalidades = _context.funcionalidade.AsQueryable();

            var funcionalidadeAtual = this.getFuncionalidadePorId(id);
            funcionalidades = funcionalidades.Where(func => string.Compare(func.classificacao, funcionalidadeAtual.classificacao) < 0);
            funcionalidades = funcionalidades.Where(func => func.id_pai == funcionalidadeAtual.id_pai);

            funcionalidades = funcionalidades.OrderByDescending(func => func.classificacao);
            return funcionalidades.FirstOrDefault();
        }
        public IEnumerable<Funcionalidade> getFuncionalidadesFilhas(int id) {
            var funcionalidades = _context.funcionalidade.AsQueryable();
            funcionalidades = funcionalidades.Where(func => func.id_pai == id);
            funcionalidades = funcionalidades.OrderBy(func => func.classificacao);

            return funcionalidades;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}