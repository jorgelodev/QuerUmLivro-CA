using QuerUmLivro.Domain.Exceptions;

namespace QuerUmLivro.Domain.Entities.ValueObjects
{
    public class StatusInteresse
    {
        public StatusInteresseEnum Value { get; private set; }
        public string Text { get; private set; }
        public enum StatusInteresseEnum
        {
            EM_ANALISE = 0,
            APROVADO = 1,
            REPROVADO = 2
        }
        private StatusInteresse(StatusInteresseEnum value, string text)
        {
            Value = value;
            Text = text;
        }

        public static StatusInteresse EmAnalise => new StatusInteresse(StatusInteresseEnum.EM_ANALISE, "Em Análise");
        public static StatusInteresse Aprovado => new StatusInteresse(StatusInteresseEnum.APROVADO, "Aprovado");
        public static StatusInteresse Reprovado => new StatusInteresse(StatusInteresseEnum.REPROVADO, "Reprovado");

        public static StatusInteresse From(StatusInteresseEnum value)
        {
            switch (value)
            {
                case StatusInteresseEnum.EM_ANALISE:
                    return EmAnalise;
                case StatusInteresseEnum.APROVADO:
                    return Aprovado;
                case StatusInteresseEnum.REPROVADO:
                    return Reprovado;
                default:
                    throw new DomainValidationException(new List<string> { "Valor inválido para StatusInteresseEnum" });

            }
        }

        #region Métodos sobreescritos

        public override bool Equals(object obj)
        {
            if (obj is StatusInteresse status)
            {
                return Value == status.Value;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Text;
        }
        #endregion
    }


}
