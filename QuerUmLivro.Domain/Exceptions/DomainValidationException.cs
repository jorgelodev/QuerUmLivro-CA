using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuerUmLivro.Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public IReadOnlyList<string> ValidationErrors { get; }

        public DomainValidationException(IEnumerable<string> validationErrors)
            : base("Um ou mais erros foram identificados.")
        {
            ValidationErrors = validationErrors.ToList().AsReadOnly();
        }

        public DomainValidationException(string validationError)
            : base("Um ou mais erros foram identificados.")
        {
            IReadOnlyList<string> erro = new List<string>
            {
                validationError
            };
            
            ValidationErrors = erro;
        }
    }
}
