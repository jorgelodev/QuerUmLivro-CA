using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Specifications.Usuarios;


namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class AlterarUsuarioUseCase : BaseUseCase<Usuario>
    {
        private readonly Usuario _usuario;        
        private readonly IUsuarioGateway _usuarioGateway;
        public AlterarUsuarioUseCase(Usuario usuario, IUsuarioGateway usuarioGateway) :base(usuario)
        {
            _usuario = usuario;
            _usuarioGateway = usuarioGateway;

            _specifications = new List<ISpecification<Usuario>>
            {
                new UsuarioIdDeveSerInformadoSpec(),
                new UsuarioEmailUnicoSpec(_usuarioGateway)                
            };
        }     

        public Usuario Alterar()
        {
            ValidaEspecificacoes();

            return _usuario;
        }
    }
}
