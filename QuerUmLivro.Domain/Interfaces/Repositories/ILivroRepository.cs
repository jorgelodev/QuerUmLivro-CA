using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Domain.Interfaces.Repositories
{
    public interface ILivroRepository : IRepositoryBase<Livro>
    {        
        IList<Livro> ObterPorDoador(int idDoador);
        IList<Livro> Disponiveis();
        ICollection<Livro> ObterComInteresse(int idDoador);
    }
}
