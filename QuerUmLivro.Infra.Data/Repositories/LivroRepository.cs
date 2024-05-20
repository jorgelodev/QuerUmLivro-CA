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

        public IList<Livro> Disponiveis()
        {
            var livros = this._dbSet
               .Where(l => l.Disponivel)
               .ToList();

            return livros;
        }

        public ICollection<Livro> ObterComInteresse(int idDoador)
        {
            var livros = this._dbSet
                .Where(l => l.DoadorId == idDoador && l.Interesses.Any())
                .Include(l => l.Interesses)
                .ToList();

            return livros;
        }  

        public IList<Livro> ObterPorDoador(int idDoador)
        {
            var livros = this._dbSet
                .Where(l => l.DoadorId == idDoador)                
                .ToList();

            return livros;
        }
    }
}
