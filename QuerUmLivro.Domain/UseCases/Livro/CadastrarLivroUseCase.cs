using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Specifications.Livros;

namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class CadastrarLivroUseCase : BaseUseCase<Livro>
    {
        private readonly Livro _livro;        
        private readonly IUsuarioGateway _usuarioGateway;
        
        public CadastrarLivroUseCase(Livro livro, IUsuarioGateway usuarioGateway)
        {
            _livro = livro;            
            _usuarioGateway = usuarioGateway;

            _specifications = new List<ISpecification<Livro>>
            {
                new LivroDoadorObrigatorioSpec(),
                new LivroDoadorExisteSpec(_usuarioGateway)
            };
        }

        public Livro Cadastrar()
        {
            ValidaEspecificacoes(_livro);

            _livro.Disponivel = true;

            return _livro;
        }

    }
}
