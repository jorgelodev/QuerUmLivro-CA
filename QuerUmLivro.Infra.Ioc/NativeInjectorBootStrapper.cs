using Microsoft.Extensions.DependencyInjection;
using QuerUmLivro.Domain.Interfaces.Gateways;
using QuerUmLivro.Domain.Interfaces.Repositories;
using QuerUmLivro.Gateways.Gateways;
using QuerUmLivro.Infra.Data.Context;
using QuerUmLivro.Infra.Data.Repositories;
using QuerUmLivro.Infra.Services;
using QuerUmLivro.Infra.Services.Interfaces;
using QuerUmLivro.Infra.Services.Services;


namespace QuerUmLivro.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {  

            #region Infrastructure            

            // Services
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IInteresseService, InteresseService>();


            // Gateways
            services.AddScoped<ILivroGateway, LivroGateway>();
            services.AddScoped<IUsuarioGateway, UsuarioGateway>();
            services.AddScoped<IInteresseGateway, InteresseGateway>();

            #endregion

            #region Data
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IInteresseRepository, InteresseRepository>();            

            services.AddScoped<ApplicationDbContext>();
            #endregion


        }
    }
}