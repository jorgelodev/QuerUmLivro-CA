using Microsoft.EntityFrameworkCore;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Repositories;
using QuerUmLivro.Infra.Data.Context;
using System.Linq.Expressions;

namespace QuerUmLivro.Infra.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Alterar(T entidade)
        {
            var existingEntity = _dbSet.Find(entidade.Id);

            if (existingEntity != null)
            {
                _dbSet.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Update(entidade);
            _context.SaveChanges();

            return entidade;
        }

        public IEnumerable<T> Buscar(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public T Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();

            return entidade;
        }

        public virtual T Deletar(int id)
        {
            var entidade = ObterPorId(id);

            if (entidade == null)
            {
                throw new Exception("Registro não encontrado.");
            }

            try
            {
                _dbSet.Remove(entidade);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Não é possível excluir. Registro está sendo utilizado.");
            }

            return entidade;
        }

        public T ObterPorId(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }

        public IList<T> ObterTodos()
        {
            return _dbSet.ToList();
        }
    }
}
