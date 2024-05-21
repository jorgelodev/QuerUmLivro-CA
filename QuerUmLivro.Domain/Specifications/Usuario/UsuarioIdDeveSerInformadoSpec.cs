using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Specifications;

namespace QuerUmLivro.Domain.Specifications.Usuarios
{
    public class UsuarioIdDeveSerInformadoSpec : ISpecification<Usuario>
    {
        public string ErrorMessage => "Id usuário não informado";

        public bool IsSatisfiedBy(Usuario usuario)
        {
            return usuario.Id > 0;
        }
    }
}
