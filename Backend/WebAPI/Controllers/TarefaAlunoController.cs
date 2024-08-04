using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefaAlunoController : BaseController<TarefaAluno, TarefaAlunoModel>
    {
        private readonly ITarefaAlunoApp _tarefaAlunoApp;
        private readonly IUsuariosApp _usuariosApp;
        public TarefaAlunoController(ITarefaAlunoApp tarefaAlunoApp, IUsuariosApp usuariosApp) : base(tarefaAlunoApp)
        {
            _tarefaAlunoApp = tarefaAlunoApp;
            _usuariosApp = usuariosApp;
        }

        [HttpGet]
        [Route("ObterTarefasAlunoPorIdAluno")]
        public async Task<IActionResult> ObterTarefasAlunoPorIdAluno(int idAluno)
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

                var tarefasAluno = await _tarefaAlunoApp.ListAsync(x => x.idAluno == idAluno);

                if (tarefasAluno is null)
                {
                    return BadRequest("Tarefa Aluno não encontrado.");
                }


                return Ok(tarefasAluno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterTarefaAluno")]
        public async Task<IActionResult> ObterTarefaAluno(int tarefaId)
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

                var tarefaAluno = await _tarefaAlunoApp.FindByAsync(x => x.idTarefa == tarefaId);

                if (tarefaAluno is null)
                {
                    return BadRequest("Usuario Tarefa não encontrado.");
                }

                return Ok(tarefaAluno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
    }
}
