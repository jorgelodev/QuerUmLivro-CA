//using FluentValidation.Results;

namespace QuerUmLivro.Infra.Services.DTOs.Usuarios
{
    public class CadastraUsuarioDto
    {        
        public CadastraUsuarioDto()
        {
           // ValidationResult = new ValidationResult();
        }
        public string Nome { get; set; }
        public string Email { get; set; }
        //public ValidationResult ValidationResult { get; set; }
    }
}
