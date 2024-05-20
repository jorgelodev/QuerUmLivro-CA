using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Specifications.Interesses;


namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class ManifestarInteresseUseCase : BaseUseCase<Interesse>
    {
        private readonly Interesse _interesse;        
        private readonly IInteresseGateway _interesseGateway;
        private readonly ILivroGateway _livroGateway;
        private readonly IUsuarioGateway _usuarioGateway;
        public ManifestarInteresseUseCase(
            Interesse interesse, 
            IInteresseGateway intereesseGateway, 
            ILivroGateway livroGateway, 
            IUsuarioGateway usuarioGateway)
        {
            _interesse = interesse;
            _interesseGateway = intereesseGateway;
            _livroGateway = livroGateway;
            _usuarioGateway = usuarioGateway;

            _specifications = new List<ISpecification<Interesse>>
            {
                new InteresseLivroExisteSpec(_livroGateway),
                new InteresseLivroEstaDisponivelSpec(_livroGateway),
                new InteresseInteressadoNaoManifestouSpec(_interesseGateway),
                new InteresseInteressadoExisteSpec(_usuarioGateway),
                new InteresseInteressadoNaoEhDonoSpec()
            };
        }

        public Interesse ManifestarInteresse()
        {
            _interesse.Data = DateTime.Now;

            ValidaEspecificacoes(_interesse);

            return _interesse;
        }
    }
}
