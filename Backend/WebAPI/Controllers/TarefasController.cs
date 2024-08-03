using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using AutoMapper;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefasController : BaseController<Tarefas, TarefasModel>
    {
        private readonly ITarefasApp _tarefasApp;
        private readonly IUsuariosApp _usuariosApp;
        private readonly IUsuarioTurmaApp _usuariosTurmaApp;
        private readonly ITurmasApp _turmasApp;
        private readonly IMapper _mapper;
        private readonly ITarefaAlunoApp _tarefaAlunoApp;
        public TarefasController(ITarefasApp tarefasApp, IUsuariosApp usuariosApp, ITurmasApp turmasApp, IMapper mapper, IUsuarioTurmaApp usuariosTurmaApp, ITarefaAlunoApp tarefaAlunoApp) : base(tarefasApp)
        {
            _tarefasApp = tarefasApp;
            _usuariosApp = usuariosApp;
            _turmasApp = turmasApp;
            _mapper = mapper;
            _usuariosTurmaApp = usuariosTurmaApp;
            _tarefaAlunoApp = tarefaAlunoApp;
        }


        [HttpGet]
        [Route("ObterPorTurmaId")]
        public async Task<IActionResult> ObterPorTurmaId(int idTurma)
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }

                var tarefasTurma = await _tarefasApp.ListAsync(x => x.idTurma == idTurma);

                if (tarefasTurma is null)
                {
                    return BadRequest("UsuarioTurma não encontrado.");
                }


                return Ok(tarefasTurma);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterTarefasProfessor")]
        public async Task<IActionResult> ObterTarefasProfessor()
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }

                var tarefasTurma = await _tarefasApp.ListAsync(x => x.idProfessor == authModel.id);

                if (tarefasTurma is null)
                {
                    return BadRequest("UsuarioTurma não encontrado.");
                }

                List<TarefasFullModel> tarefasFull = new List<TarefasFullModel>();
                foreach (var tarefa in tarefasTurma)
                {
                    tarefasFull.Add(new TarefasFullModel
                    {
                        id = tarefa.id,
                        anexo = tarefa.anexo,
                        dataLimite = tarefa.dataLimite,
                        descricao = tarefa.descricao,
                        professor = _usuariosApp.ObterUsuarioLightPorId(tarefa.idProfessor),
                        turma = _turmasApp.ObterTurmaPorId(tarefa.idTurma)
                    });
                }


                return Ok(tarefasFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterTarefaPorId")]
        public async Task<IActionResult> ObterTarefaPorId(int tarefaId)
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }

                var tarefaTurma = await _tarefasApp.FindByAsync(x => x.id == tarefaId);

                if (tarefaTurma is null)
                {
                    return BadRequest("UsuarioTurma não encontrado.");
                }

                TarefasFullModel tarefasFull = new TarefasFullModel
                {

                    id = tarefaTurma.id,
                    anexo = tarefaTurma.anexo,
                    dataLimite = tarefaTurma.dataLimite,
                    descricao = tarefaTurma.descricao,
                    professor = _usuariosApp.ObterUsuarioLightPorId(tarefaTurma.idProfessor),
                    turma = _turmasApp.ObterTurmaPorId(tarefaTurma.idTurma)
                };


                return Ok(tarefasFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterAlunosTarefaPorTurma")]
        public async Task<IActionResult> ObterAlunosTarefaPorTurma(int idTurma, int idTarefa)
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }

                var alunosTarefa = await _tarefaAlunoApp.ListAsync(x => x.idTurma == idTurma && x.idTarefa == idTarefa);

                if (alunosTarefa is null)
                {
                    return BadRequest("Usuario tarefa não encontrado.");
                }

                List<TarefaAlunoTurmaFullModel> tarefaAlunoFull = new List<TarefaAlunoTurmaFullModel>();

                foreach (var alunoTarefa in alunosTarefa)
                {
                    tarefaAlunoFull.Add(new TarefaAlunoTurmaFullModel
                    {
                        id = alunoTarefa.id,
                        aluno = _usuariosApp.ObterUsuarioLightPorId(alunoTarefa.idAluno),
                        turma = _turmasApp.ObterTurmaPorId(alunoTarefa.idTurma),
                        tarefa = _tarefasApp.FindBy(x => x.id == alunoTarefa.idTarefa),
                        anexo = alunoTarefa?.anexo,
                        dataEntrega = alunoTarefa?.dataEntrega
                    });
                }
                return Ok(tarefaAlunoFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpPost]
        [Route("Registrar")]
        public override async Task<IActionResult> Registrar(TarefasModel tarefa)
        {
            try
            {
                var data = await _tarefasApp.Add(tarefa);
                await _tarefasApp.SaveChangesAsync();
                var dataEntity = (EntityEntry<Tarefas>)data;

                if (dataEntity != null)
                {

                    var alunosTurma = await _usuariosTurmaApp.ListAsync(x => x.idTurma ==  tarefa.idTurma);

                    List<TarefaAluno> alunosTarefaAdicionar = new List<TarefaAluno>();

                    foreach (var aluno in alunosTurma)
                    {
                        TarefaAluno tarefaAluno = new TarefaAluno
                        {
                            idAluno = aluno.idUsuario,
                            idTarefa = dataEntity.Entity.id,
                            idTurma = aluno.idTurma,
                        };
                        alunosTarefaAdicionar.Add(tarefaAluno);
                    }
                    
                    if(alunosTarefaAdicionar.Count > 0)
                    {
                        _tarefaAlunoApp.AddRange(alunosTarefaAdicionar);
                        await _tarefaAlunoApp.SaveChangesAsync();
                    }
                }

                return Ok(dataEntity.Entity);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is MySqlException mysqlException && mysqlException.Message.Contains("Duplicate entry"))
                {
                    // Retornar uma mensagem padrão personalizada para o usuário
                    return BadRequest("O registro com o mesmo valor já existe. Por favor, verifique seus dados.");
                }

                return BadRequest("Erro Inesperado");

            }

        }
    }
}
