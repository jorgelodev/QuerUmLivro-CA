using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Specifications;
using QuerUmLivro.Domain.Specifications.Usuarios;


namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class DesativarUsuarioUseCase : BaseUseCase<Usuario>
    {
        private readonly Usuario _usuario;                
        public DesativarUsuarioUseCase(Usuario usuario) : base(usuario)
        {
            _usuario = usuario;            

            _specifications = new List<ISpecification<Usuario>>
            {
                new UsuarioIdDeveSerInformadoSpec()                
            };
        }     

        public Usuario Desativar()
        {
            ValidaEspecificacoes();

            _usuario.Desativado = true;           

            return _usuario;
        }
    }
}
