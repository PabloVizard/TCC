using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Domain.Services;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Applications.Interfaces;
using Application.Applications;

namespace Infrastructure.Utils
{
    public static class InjectorDependencies
    {
        public static void Registrer(IServiceCollection services)
        {
            #region Application

            services.AddScoped(typeof(IBaseApp<,>), typeof(BaseApp<,>));
            services.AddScoped<IUsuariosApp, UsuariosApp>();
            services.AddScoped<IPreRegistroApp, PreRegistroApp>();
            services.AddScoped<ITurmasApp, TurmasApp>();
            services.AddScoped<IOrientacoesApp, OrientacoesApp>();

            #endregion

            #region Services

            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<IPreRegistroService, PreRegistroService>();
            services.AddScoped<ITurmasService, TurmasService>();
            services.AddScoped<IOrientacoesService, OrientacoesService>();

            #endregion

            #region Repositories 

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IPreRegistroRepository, PreRegistroRepository>();
            services.AddScoped<ITurmasRepository, TurmasRepository>();
            services.AddScoped<IOrientacoesRepository, OrientacoesRepository>();

            #endregion
        }
    }
}
