using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Specifications.Livros;

namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class AlterarLivroUseCase : BaseUseCase<Livro>
    {
        private readonly Livro _livro;        
        private readonly ILivroGateway _livroGateway;        
        public AlterarLivroUseCase(Livro livro, ILivroGateway livroGateway) : base(livro)
        {
            _livro = livro;
            _livroGateway = livroGateway;

            _specifications = new List<ISpecification<Livro>>
            {
                new LivroIdObrigatorioSpec(),                
                new LivroDoadorNaoPodeSerAlteradoSpec(_livroGateway)
            };
        }

        public void Alterar()
        {
            ValidaEspecificacoes();            
        }
    }
}
