using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Domain.Entities
{
    public class Interesse : EntityBase
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int InteressadoId { get; set; }
        public Usuario Interessado { get; set; }
        public string Justificativa { get; set; }
        public StatusInteresse Status { get; private set; }
        //public bool Aprovado { get; set; }
        public DateTime Data { get; set; }

        public Interesse(int livroId, int interessadoId, string justificativa, StatusInteresse status, DateTime data)
        {
            var validationResult = new List<string>();

            if (livroId <= 0)
                validationResult.Add("Livro é obrigatório.");

            if (interessadoId <= 0)
                validationResult.Add("Interessado é obrigatório.");

            if (string.IsNullOrWhiteSpace(justificativa) || justificativa.Length > 500)
                validationResult.Add("Justificativa é obrigatória e com máximo de 500 caracteres.");

            if (validationResult.Any())
                throw new DomainValidationException(validationResult);

            LivroId = livroId;            
            InteressadoId = interessadoId;
            
            Justificativa = justificativa;
            Data = data;
            Status = status;
        }

        public void Aprovar()
        {
            if (Status.Value == StatusInteresse.StatusInteresseEnum.EM_ANALISE)
                Status = StatusInteresse.Aprovado;
            else
                throw new DomainValidationException(new List<string> { "Interesse não está em análise." });

        }
        public void Reprovar()
        {
            if (Status.Value == StatusInteresse.StatusInteresseEnum.EM_ANALISE)
                Status = StatusInteresse.Reprovado;
            else
                throw new DomainValidationException(new List<string> { "Interesse não está em análise." });

        }

    }
}
