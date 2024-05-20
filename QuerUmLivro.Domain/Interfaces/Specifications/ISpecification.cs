
namespace QuerUmLivro.Domain.Interfaces.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        string ErrorMessage { get; }
    }
}
