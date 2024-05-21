
using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Domain.Interfaces.Gateways
{
    public interface ILivroGateway 
    {
        Livro Alterar(Livro livro);
        Livro Cadastrar(Livro livro);
        Livro Deletar(int id);
        Livro ObterPorId(int id);
        IList<Livro> ObterPorDoador(int idDoador);
        IList<Livro> Disponiveis();
        ICollection<Livro> ObterComInteresse(int idDoador);        
    }
}
