using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Specifications.Livros;

namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class DeletarLivroUseCase : BaseUseCase<Livro>
    {
        private readonly Livro _livro;        

        private readonly IInteresseGateway _interesseGateway;
        public DeletarLivroUseCase(Livro livro, IInteresseGateway interesseGateway) : base(livro)
        {
            _livro = livro;
            _interesseGateway = interesseGateway;

            _specifications = new List<ISpecification<Livro>>
            {
                new LivroIdObrigatorioSpec(),
                new LivroEstaSendoUtilizadoSpec(_interesseGateway)
            };
        }

        public void Deletar()
        {
            ValidaEspecificacoes();            
        }

        
    }
}
