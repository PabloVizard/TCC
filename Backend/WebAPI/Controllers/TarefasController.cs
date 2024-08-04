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
                        aluno = _usuariosApp.ObterUsuarioLightPorId(tarefa.idAluno),
                        entregue = _tarefaAlunoApp.Any(x => x.idAluno == tarefa.idAluno && !string.IsNullOrEmpty(x.anexo) && x.idTarefa == tarefa.id),
                        anexoEntrega = _tarefaAlunoApp.FindBy(x => x.idAluno == tarefa.idAluno && x.idTarefa == tarefa.id)?.anexo
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
        [Route("ObterTarefasAluno")]
        public async Task<IActionResult> ObterTarefasAluno()
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

                var tarefasAluno = await _tarefasApp.ListAsync(x => x.idAluno == authModel.id);

                if (tarefasAluno is null)
                {
                    return BadRequest("UsuarioTurma não encontrado.");
                }

                List<TarefasAlunoFullModel> tarefasFull = new List<TarefasAlunoFullModel>();
                foreach (var tarefa in tarefasAluno)
                {
                    tarefasFull.Add(new TarefasAlunoFullModel
                    {
                        id = tarefa.id,
                        anexo = tarefa.anexo,
                        dataLimite = tarefa.dataLimite,
                        descricao = tarefa.descricao,
                        professor = _usuariosApp.ObterUsuarioLightPorId(tarefa.idProfessor),
                        aluno = _usuariosApp.ObterUsuarioLightPorId(tarefa.idAluno),
                        dataEntrega = _tarefaAlunoApp.FindBy(x => x.idAluno == tarefa.idAluno && x.idTarefa == tarefa.id)?.dataEntrega,
                        anexoEntrega = _tarefaAlunoApp.FindBy(x => x.idAluno == tarefa.idAluno && x.idTarefa == tarefa.id)?.anexo
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
                    aluno = _usuariosApp.ObterUsuarioLightPorId(tarefaTurma.idAluno)
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

                var alunosTurma = await _usuariosTurmaApp.ListAsync(x => x.idTurma == idTurma);

                if (alunosTurma == null || !alunosTurma.Any())
                {
                    return BadRequest("Usuários da turma não encontrados.");
                }

                var alunoIds = alunosTurma.Select(x => x.idUsuario).ToList();
                var alunosTarefa = await _tarefaAlunoApp.ListAsync(x => x.idTarefa == idTarefa && alunoIds.Contains(x.idAluno));

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

                    var aluno = await _usuariosApp.FindByAsync(x => x.id ==  tarefa.idAluno);

                    TarefaAluno alunoTarefaAdicionar = new TarefaAluno();

                    TarefaAlunoModel tarefaAluno = new TarefaAlunoModel
                    {
                        idAluno = aluno.id,
                        idTarefa = dataEntity.Entity.id,
                    };

                    await _tarefaAlunoApp.Add(tarefaAluno);
                    await _tarefaAlunoApp.SaveChangesAsync();
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
