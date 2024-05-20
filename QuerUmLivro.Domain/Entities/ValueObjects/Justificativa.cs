
namespace QuerUmLivro.Domain.Entities.ValueObjects
{
    public class Justificativa
    {
        public string Value { get; }

        public Justificativa(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Justificativa é obrigatória.");

            if (value.Length > 100)
                throw new ArgumentException("Justificativa deve ter no máximo 500 caracteres.");

            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Justificativa other)
                return Value == other.Value;
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
