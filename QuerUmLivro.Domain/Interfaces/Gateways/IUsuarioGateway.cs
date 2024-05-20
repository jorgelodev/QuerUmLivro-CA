
using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Domain.Interfaces.Gateways
{
    public interface IUsuarioGateway
    {
        Usuario Alterar(Usuario usuario);
        Usuario Cadastrar(Usuario usuario);        
        Usuario ObterPorId(int id);
        bool EmailJaUtilizado(Usuario usuario);

    }
}
