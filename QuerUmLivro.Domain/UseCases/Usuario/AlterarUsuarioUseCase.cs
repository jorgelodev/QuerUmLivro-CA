using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;


namespace QuerUmLivro.Domain.UseCases.Livros
{
    public class AlterarUsuarioUseCase
    {
        private readonly Usuario _usuario;        
        private readonly IUsuarioGateway _usuarioGateway;
        public AlterarUsuarioUseCase(Usuario usuario, IUsuarioGateway usuarioGateway)
        {
            _usuario = usuario;
            _usuarioGateway = usuarioGateway;
        }     

        public Usuario Alterar()
        {
            // verificar se o email já está sendo utilizado
            var emailJaUtilizado = _usuarioGateway.EmailJaUtilizado(_usuario);

            //livro.ValidationResult = new LivroAlterarValid(_usuarioRepository).Validate(livro);

            //if (!livro.ValidationResult.IsValid)
            //    return livro;



            return _usuario;
        }
    }
}
