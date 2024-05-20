


using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Domain.Interfaces.Gateways
{
    public interface IInteresseGateway
    {
        Interesse ObterPorLivroEInteressado(Interesse interesse);
        IList<Interesse> ObterPorLivro(int idLivro);
        Interesse Cadastrar(Interesse interesse);
        Interesse Alterar(Interesse interesse);
        Interesse ObterPorId(int id);
       
    }
}
