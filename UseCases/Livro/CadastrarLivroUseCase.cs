using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Repositories;
//using QuerUmLivro.Domain.Validations.Livros;

namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class CadastrarLivroUseCase
    {
        private readonly Livro _livro;
        private readonly IUsuarioRepository _usuarioRepository;
        public CadastrarLivroUseCase(Livro livro, IUsuarioRepository usuarioRepository)
        {
            _livro = livro;
            _usuarioRepository = usuarioRepository;
        }

        public Livro Cadastrar(Livro livro)
        {
            //livro.ValidationResult = new LivroCadastroValid(_usuarioRepository).Validate(livro);

            //if (!livro.ValidationResult.IsValid)
            //    return livro;

            //livro.Disponivel = true;

            return _livro;
        }
    }
}
