
using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Domain.Interfaces.Gateways
{
    public interface ILivroGateway
    {
        Livro Cadastrar(Livro livro);
        ICollection<Livro> ObterComInteresse(int idDoador);

        ICollection<Livro> ObterDisponiveis();
    }
}
