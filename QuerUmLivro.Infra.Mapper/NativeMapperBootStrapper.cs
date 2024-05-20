using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Infra.Services.DTOs.Interesses;
using QuerUmLivro.Infra.Services.DTOs.Livros;
using QuerUmLivro.Infra.Services.DTOs.Usuarios;
using QuerUmLivro.Infra.Services.ViewModels.Interesses;
using QuerUmLivro.Infra.Services.ViewModels.Livros;
using QuerUmLivro.Infra.Services.ViewModels.Usuarios;

namespace QuerUmLivro.Infra.Mapper
{
    public class NativeMapperBootStrapper : Profile
    {
        public NativeMapperBootStrapper()
        {
            // ViewModels x DTOs
            CreateMap<LivroViewModel, LivroDto>().ReverseMap();
            CreateMap<AlteraLivroViewModel, AlteraLivroDto>().ReverseMap();
            CreateMap<CadastraLivroViewModel, CadastraLivroDto>().ReverseMap();
            CreateMap<LivroComInteressesViewModel, LivroComInteressesDto>().ReverseMap();
            CreateMap<AprovarInteresseViewModel, AprovarInteresseDto>().ReverseMap();

            CreateMap<InteresseViewModel, InteresseDto>().ReverseMap();
            CreateMap<ManifestarInteresseViewModel, InteresseDto>().ReverseMap();
            CreateMap<ManifestarInteresseViewModel, ManifestarInteresseDto>().ReverseMap();

            CreateMap<UsuarioViewModel, UsuarioDto>().ReverseMap();
            CreateMap<AlteraUsuarioViewModel, AlteraUsuarioDto>().ReverseMap();
            CreateMap<CadastraUsuarioViewModel, CadastraUsuarioDto>().ReverseMap();

            // Entities x DTOs 
            CreateMap<Livro, LivroDto>().ReverseMap();
            CreateMap<Livro, AlteraLivroDto>().ReverseMap();
            CreateMap<Livro, CadastraLivroDto>().ReverseMap();
            CreateMap<Livro, LivroComInteressesDto>().ReverseMap();
            CreateMap<StatusInteresse, StatusInteresseDto>().ReverseMap();

            CreateMap<Interesse, InteresseDto>().ReverseMap();
            CreateMap<Interesse, AprovarInteresseDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, AlteraUsuarioDto>().ReverseMap();
            CreateMap<Usuario, CadastraUsuarioDto>().ReverseMap();

        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(NativeMapperBootStrapper));
        }
    }
}