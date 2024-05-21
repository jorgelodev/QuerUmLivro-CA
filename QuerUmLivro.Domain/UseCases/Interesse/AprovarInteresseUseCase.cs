using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Specifications.Interesses;


namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class AprovarInteresseUseCase : BaseUseCase<Interesse>
    {
        private readonly Interesse _interesse;        
        private readonly ILivroGateway _livroGateway;
        private readonly IUsuarioGateway _usuarioGateway;
        public AprovarInteresseUseCase(Interesse interesse, ILivroGateway livroGateway, IUsuarioGateway usuarioGateway) : base(interesse)
        {
            _interesse = interesse;
            _livroGateway = livroGateway;
            _usuarioGateway = usuarioGateway;

            _specifications = new List<ISpecification<Interesse>>
            {
                new InteresseDoadorIdObrigatorioSpec(),
                new InteresseInteressadoExisteSpec(_usuarioGateway),               
                new InteresseLivroEstaDisponivelSpec(_livroGateway)
            };
        }

        public Interesse AprovarInteresse()
        {
            ValidaEspecificacoes();

            _interesse.Aprovar();

            _interesse.Livro.Disponivel = false;

            _livroGateway.Alterar(_interesse.Livro);

            return _interesse;
        }
    }
}
