using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Repositories;

namespace QuerUmLivro.Gateways.Gateways
{
    public class InteresseGateway : IInteresseGateway
    {
        private readonly IInteresseRepository _interesseRepository;

        public InteresseGateway(IInteresseRepository interesseRepository)
        {
            _interesseRepository = interesseRepository;
        }

        public Interesse Alterar(Interesse interesse)
        {
            return _interesseRepository.Alterar(interesse);
        }

        public Interesse Cadastrar(Interesse interesse)
        {
            return _interesseRepository.Cadastrar(interesse);
            
        }

        public Interesse ObterPorId(int id)
        {
            return _interesseRepository.ObterPorId(id);
        }

        public IList<Interesse> ObterPorLivro(int idLivro)
        {
            return _interesseRepository.Buscar(i => i.LivroId == idLivro).ToList();
        }

        public Interesse ObterPorLivroEInteressado(Interesse interesse)
        {
            return _interesseRepository
                .Buscar(i => 
                i.LivroId == interesse.LivroId && 
                i.InteressadoId == interesse.InteressadoId)
                .FirstOrDefault();
        }
    }
}
