
using QuerUmLivro.Domain.Exceptions;
using QuerUmLivro.Domain.Interfaces.Specifications;


namespace QuerUmLivro.Domain.UseCases
{
    public abstract class BaseUseCase<T> where T : class
    {
        protected List<ISpecification<T>> _specifications;

        protected T entity;
        public BaseUseCase(T entity)
        {
            _specifications = new List<ISpecification<T>>();
            this.entity = entity;
        }
        protected void ValidaEspecificacoes()
        {
            var erros = new List<string>();

            foreach (var spec in _specifications)
            {
                if (!spec.IsSatisfiedBy(entity))
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
