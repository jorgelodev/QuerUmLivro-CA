using QuerUmLivro.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace QuerUmLivro.Domain.Entities.ValueObjects
{
    public class Email
    {
        public string EnderecoEmail { get; }

        public Email(string email)
        {
            var validationResult = new List<string>();

            if (string.IsNullOrWhiteSpace(email))
                 validationResult.Add("Email é obrigatório.");            

            if (!IsValidEmail(email))
                validationResult.Add("Formato de email inválido.");

            if (validationResult.Any())
                throw new DomainValidationException(validationResult);

            EnderecoEmail = email;
        }

        private static bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public override bool Equals(object obj)
        {
            if (obj is Email other)
                return EnderecoEmail == other.EnderecoEmail;
            return false;
        }

        public override int GetHashCode()
        {
            return EnderecoEmail.GetHashCode();
        }

        public override string ToString()
        {
            return EnderecoEmail;
        }
    }
}
