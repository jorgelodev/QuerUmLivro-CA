using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Interesses
{
    public class InteresseInteressadoNaoManifestouSpec : ISpecification<Interesse>
    {
        private readonly IInteresseGateway _interesseGateway;
        public string ErrorMessage => "Interessado já manifestou interesse nesse livro.";
        public InteresseInteressadoNaoManifestouSpec(IInteresseGateway interesseGateway)
        {
            _interesseGateway = interesseGateway;
        }

        public bool IsSatisfiedBy(Interesse interesse)
        {
            var interesseEncontrado = _interesseGateway.ObterPorLivroEInteressado(interesse);

            return interesseEncontrado == null;
        }
    }
}
