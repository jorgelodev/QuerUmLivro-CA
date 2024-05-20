
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Interesses
{
    public class InteresseLivroExisteSpec : ISpecification<Interesse>
    {
        private readonly ILivroGateway _livroGateway;
        public string ErrorMessage => "Livro não existe.";
        public InteresseLivroExisteSpec(ILivroGateway livroGateway)
        {
            _livroGateway = livroGateway;
        }

        public bool IsSatisfiedBy(Interesse interesse)
        {
            var livroEncontrado = _livroGateway.ObterPorId(interesse.LivroId);

            return livroEncontrado != null;
        }
    }
}
