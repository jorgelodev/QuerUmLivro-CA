using QuerUmLivro.Infra.Services.DTOs.Usuarios;

namespace QuerUmLivro.Infra.Services.Interfaces
{
    public interface IUsuarioService
    {
        AlteraUsuarioDto Alterar(AlteraUsuarioDto livro);
        UsuarioDto Cadastrar(CadastraUsuarioDto livro);
        UsuarioDto Desativar(int id);
        UsuarioDto ObterPorId(int id);
    }
}
