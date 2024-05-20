using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuerUmLivro.Infra.Services.DTOs.Interesses;
using QuerUmLivro.Infra.Services.Interfaces;
using QuerUmLivro.Infra.Services.ViewModels.Interesses;



namespace QuerUmLivro.API.Controllers
{    
    [ApiController]
    [Route("interesse")]
    public class InteresseController : MainController
    {
        private readonly IInteresseService _intresseService;
        private readonly IMapper _mapper;

        public InteresseController(IInteresseService interesseService, IMapper mapper)
        {
            _intresseService = interesseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Manifestar interesse em um determinado livro.
        /// </summary>
        /// <param name="manifestarInteresseViewModel">ViewModel para manifestar interesse.</param>        
        /// <remarks>
        /// 
        /// Informe o id do livro, id do usuário interessado e justificativa para manifestar o interesse no livro. 
        /// 
        /// </remarks>
        /// <response code="200">Manifestação registrada com sucesso</response>
        /// <response code="400">Manifestação não realizada, é retornado mensagem com o(s) motivo(s).</response>
        [HttpPost("manifestar-interesse")]

        public IActionResult ManifestarInteresse([FromBody] ManifestarInteresseViewModel manifestarInteresseViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var interesseManifestado = _intresseService.ManifestarInteresse(_mapper.Map<ManifestarInteresseDto>(manifestarInteresseViewModel));

            return CustomResponse();
        }

        /// <summary>
        /// Doador realiza a aprovação do interesse.
        /// </summary>
        /// <param name="aprovaInteresseViewModel">ViewModel para aprovar interesse.</param>        
        /// <remarks>
        /// 
        /// Informe id do interesse e id do doador que está aprovando o interesse. 
        /// 
        /// </remarks>
        /// <response code="200">Aprovação Realizada com sucesso</response>
        /// <response code="400">Aprovação não realizada, é retornado mensagem com o(s) motivo(s).</response>
        [HttpPut("aprovar-interesse")]
        public IActionResult AprovarInteresse([FromBody] AprovarInteresseViewModel aprovaInteresseViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var interesseAprovado = _intresseService.AprovarInteresse(_mapper.Map<AprovarInteresseDto>(aprovaInteresseViewModel));

            //if (!interesseAprovado.ValidationResult.IsValid)

            //    AdicionarErroProcessamento(interesseAprovado.ValidationResult);

            return CustomResponse();

        }
    }
}
