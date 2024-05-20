
using QuerUmLivro.Infra.Services.DTOs.Livros;

namespace QuerUmLivro.Infra.Services.Interfaces
{
    public interface ILivroService
    {
        LivroDto Cadastrar(CadastraLivroDto livroDto);
        AlteraLivroDto Alterar(AlteraLivroDto alteraLivroDto);
        ICollection<LivroDto> ObterPorDoador(int idUsuario);
        LivroDto ObterPorId(int id);
        LivroDto Deletar(int id);
        ICollection<LivroDto> Disponiveis();
        ICollection<LivroComInteressesDto> ObterComInteresse(int idDoador);
    }
}
