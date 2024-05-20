namespace QuerUmLivro.Infra.Services.DTOs.Interesses
{
    public class StatusInteresseDto
    {
        public StatusInteresseEnum Value { get; private set; }
        public string Text { get; private set; }
        public enum StatusInteresseEnum
        {
            EM_ANALISE = 0,
            APROVADO = 1,
            REPROVADO = 2
        }
        private StatusInteresseDto(StatusInteresseEnum value, string text)
        {
            Value = value;
            Text = text;
        }

        public static StatusInteresseDto EmAnalise => new StatusInteresseDto(StatusInteresseEnum.EM_ANALISE, "Em Análise");
        public static StatusInteresseDto Aprovado => new StatusInteresseDto(StatusInteresseEnum.APROVADO, "Aprovado");
        public static StatusInteresseDto Reprovado => new StatusInteresseDto(StatusInteresseEnum.REPROVADO, "Reprovado");

        public static StatusInteresseDto From(StatusInteresseEnum value)
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
                    throw new ArgumentException("Valor inválido para StatusInteresseEnum", nameof(value));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is StatusInteresseDto status)
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
    }
}
