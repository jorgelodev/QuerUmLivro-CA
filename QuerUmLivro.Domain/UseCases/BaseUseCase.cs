
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Specifications;


namespace QuerUmLivro.Domain.UseCases
{
    public abstract class BaseUseCase<T> where T : class
    {
        protected List<ISpecification<T>> _specifications;
        public BaseUseCase()
        {
            _specifications = new List<ISpecification<T>>();
        }
        protected void ValidaEspecificacoes(T entidade)
        {
            var erros = new List<string>();
            foreach (var spec in _specifications)
            {
                if (!spec.IsSatisfiedBy(entidade))
                {
                    erros.Add(spec.ErrorMessage);
                }
            }

            if (erros.Any())
            {
                throw new DomainValidationException(erros);
            }            
        }
    }
}
