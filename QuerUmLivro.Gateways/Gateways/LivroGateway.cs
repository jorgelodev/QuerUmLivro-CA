using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Repositories;

namespace QuerUmLivro.Gateways.Gateways
{
    public class LivroGateway : ILivroGateway
    {
        private readonly ILivroRepository _livroRepository;

        public LivroGateway(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public Livro Alterar(Livro livro)
        {
            return _livroRepository.Alterar(livro);
        }

        public Livro Cadastrar(Livro livro)
        {
            return _livroRepository.Cadastrar(livro);
        }

        public Livro Deletar(int id)
        {
            return _livroRepository.Deletar(id);
        }

        public IList<Livro> Disponiveis()
        {
            return _livroRepository.Buscar(l => l.Disponivel)
               .ToList();
        }

        public ICollection<Livro> ObterComInteresse(int idDoador)
        {
            return _livroRepository.ObterComInteresse(idDoador);
        }

        public IList<Livro> ObterPorDoador(int idDoador)
        {
            return _livroRepository.Buscar(l => l.DoadorId == idDoador).ToList();
            
        }

        public Livro ObterPorId(int id)
        {
            return _livroRepository.ObterPorId(id);
        }
    }
}
