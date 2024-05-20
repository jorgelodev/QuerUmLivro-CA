using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Repositories;
using QuerUmLivro.Infra.Data.Context;


namespace QuerUmLivro.Infra.Data.Repositories
{
    public class InteresseRepository : RepositoryBase<Interesse>, IInteresseRepository
    {
        public InteresseRepository(ApplicationDbContext context) : base(context)
        {

        }            

        public override Interesse Deletar(int id)
        {
            throw new Exception("Interesse não pode ser deletado.");
        }

    }
}
