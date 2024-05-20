//using FluentValidation.Results;

namespace QuerUmLivro.Infra.Services.DTOs.Usuarios
{
    public class AlteraUsuarioDto
    {        
        public AlteraUsuarioDto()
        {
            //ValidationResult = new ValidationResult();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
       // public ValidationResult ValidationResult { get; set; }
    }
}
