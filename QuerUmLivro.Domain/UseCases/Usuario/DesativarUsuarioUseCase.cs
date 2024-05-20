using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;


namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class DesativarUsuarioUseCase
    {
        private readonly Usuario _usuario;        
        private readonly IUsuarioGateway _usuarioGateway;
        public DesativarUsuarioUseCase(Usuario usuario, IUsuarioGateway usuarioGateway)
        {
            _usuario = usuario;
            _usuarioGateway = usuarioGateway;
        }     

        public Usuario Desativar()
        {
            //livro.ValidationResult = new LivroAlterarValid(_usuarioRepository).Validate(livro);

            //if (!livro.ValidationResult.IsValid)
            //    return livro;

            _usuario.Desativado = true;
            

            return _usuario;
        }
    }
}
