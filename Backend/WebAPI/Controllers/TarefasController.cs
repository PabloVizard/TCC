using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using AutoMapper;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefasController : BaseController<Tarefas, TarefasModel>
    {
        private readonly ITarefasApp _tarefasApp;
        private readonly IUsuariosApp _usuariosApp;
        private readonly ITurmasApp _turmasApp;
        private readonly IMapper _mapper;
        public TarefasController(ITarefasApp tarefasApp, IUsuariosApp usuariosApp, ITurmasApp turmasApp, IMapper mapper) : base(tarefasApp)
        {
            _tarefasApp = tarefasApp;
            _usuariosApp = usuariosApp;
            _turmasApp = turmasApp;
            _mapper = mapper;
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
                        professor = ObterUsuarioLightPorId(tarefa.idProfessor),
                        turma = ObterTurmaPorId(tarefa.idTurma)
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
                    professor = ObterUsuarioLightPorId(tarefaTurma.idProfessor),
                    turma = ObterTurmaPorId(tarefaTurma.idTurma)
                };


                return Ok(tarefasFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        private UsuariosLightModel ObterUsuarioLightPorId(int idUsuario)
        {
            var usuario = _usuariosApp.Find(idUsuario);

            if (usuario == null)
            {
                return null;
            }

            return new UsuariosLightModel { id = usuario.id, nomeCompleto = usuario.nomeCompleto, email = usuario.email, tipoUsuario = usuario.tipoUsuario };
        }
        private TurmasModel ObterTurmaPorId(int idTurma)
        {
            var turma = _turmasApp.Find(idTurma);

            if (turma == null)
            {
                return null;
            }
            var turmaModel = _mapper.Map<TurmasModel>(turma);

            return turmaModel;
        }
    }
}
