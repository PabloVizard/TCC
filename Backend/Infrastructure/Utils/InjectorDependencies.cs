﻿using Domain.Repositories.Interfaces;
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
            services.AddScoped<IUsuarioTurmaApp, UsuarioTurmaApp>();
            services.AddScoped<ITarefasApp, TarefasApp>();
            services.AddScoped<ITarefaAlunoApp, TarefaAlunoApp>();
            services.AddScoped<IProjetosApp, ProjetosApp>();
            services.AddScoped<IAulasApp, AulasApp>();
            services.AddScoped<IBancasApp, BancasApp>();
            services.AddScoped<IFaltasApp, FaltasApp>();

            #endregion

            #region Services

            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<IPreRegistroService, PreRegistroService>();
            services.AddScoped<ITurmasService, TurmasService>();
            services.AddScoped<IOrientacoesService, OrientacoesService>();
            services.AddScoped<IUsuarioTurmaService, UsuarioTurmaService>();
            services.AddScoped<ITarefasService, TarefasService>();
            services.AddScoped<ITarefaAlunoService, TarefaAlunoService>();
            services.AddScoped<IProjetosService, ProjetosService>();
            services.AddScoped<IAulasService, AulasService>();
            services.AddScoped<IBancasService, BancasService>();
            services.AddScoped<IFaltasService, FaltasService>();

            #endregion

            #region Repositories 

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IPreRegistroRepository, PreRegistroRepository>();
            services.AddScoped<ITurmasRepository, TurmasRepository>();
            services.AddScoped<IOrientacoesRepository, OrientacoesRepository>();
            services.AddScoped<IUsuarioTurmaRepository, UsuarioTurmaRepository>();
            services.AddScoped<ITarefasRepository, TarefasRepository>();
            services.AddScoped<ITarefaAlunoRepository, TarefaAlunoRepository>();
            services.AddScoped<IProjetosRepository, ProjetosRepository>();
            services.AddScoped<IAulasRepository, AulasRepository>();
            services.AddScoped<IBancasRepository, BancasRepository>();
            services.AddScoped<IFaltasRepository, FaltasRepository>();

            #endregion
        }
    }
}
