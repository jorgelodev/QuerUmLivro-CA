using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;

namespace QuerUmLivro.Domain.Specifications.Interesses
{
    public class InteresseLivroEstaDisponivelSpec : ISpecification<Interesse>
    {
        private readonly ILivroGateway _livroGateway;
        public string ErrorMessage => "Livro não está mais disponível.";
        public InteresseLivroEstaDisponivelSpec(ILivroGateway livroGateway)
        {
            _livroGateway = livroGateway;
        }

        public bool IsSatisfiedBy(Interesse entidade)
        {
            var livro = _livroGateway.ObterPorId(entidade.LivroId);

            return livro.Disponivel;
        }
    }
}
