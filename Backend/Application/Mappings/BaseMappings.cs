using Application.Models;
using AutoMapper;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class BaseMapping : Profile
    {
        public BaseMapping()
        {
            CreateMap<Usuarios, UsuariosModel>().ReverseMap();
            CreateMap<PreRegistro, PreRegistroModel>().ReverseMap();
            CreateMap<Turmas, TurmasModel>().ReverseMap();
            CreateMap<Orientacoes, OrientacoesModel>().ReverseMap();
            CreateMap<UsuarioTurma, UsuarioTurmaModel>().ReverseMap();
            CreateMap<Tarefas, TarefasModel>().ReverseMap();
            CreateMap<TarefaAluno, TarefaAlunoModel>().ReverseMap();
            CreateMap<Projetos, ProjetosModel>().ReverseMap();
            CreateMap<Aulas, AulasModel>().ReverseMap();
            CreateMap<Bancas, BancasModel>().ReverseMap();
            CreateMap<Faltas, FaltasModel>().ReverseMap();
        }
    }
}
