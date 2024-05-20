using System.Linq.Expressions;

namespace QuerUmLivro.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        IList<T> ObterTodos();
        T ObterPorId(int id);
        T Cadastrar(T entidade);
        T Alterar(T entidade);
        T Deletar(int id);
        IEnumerable<T> Buscar(Expression<Func<T, bool>> predicate);
    }
}
