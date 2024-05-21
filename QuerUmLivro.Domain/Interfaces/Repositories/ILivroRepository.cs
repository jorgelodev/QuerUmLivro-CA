using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Domain.Interfaces.Repositories
{
    public interface ILivroRepository : IRepositoryBase<Livro>
    {        
        ICollection<Livro> ObterComInteresse(int idDoador);
    }
}
