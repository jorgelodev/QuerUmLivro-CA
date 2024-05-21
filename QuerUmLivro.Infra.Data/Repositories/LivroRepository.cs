using Microsoft.EntityFrameworkCore;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Repositories;
using QuerUmLivro.Infra.Data.Context;

namespace QuerUmLivro.Infra.Data.Repositories
{
    public class LivroRepository : RepositoryBase<Livro>, ILivroRepository
    {
        public LivroRepository(ApplicationDbContext context) : base(context)
        {

        }

        public ICollection<Livro> ObterComInteresse(int idDoador)
        {
            var livros = this._dbSet
                .Where(l => l.DoadorId == idDoador && l.Interesses.Any())
                .Include(l => l.Interesses)
                .ToList();

            return livros;
        }
       
    }
}
