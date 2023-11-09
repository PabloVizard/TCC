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
    public class TarefasController : BaseController<Tarefas, TarefasModel>
    {
        private readonly ITarefasApp _tarefasApp;
        public TarefasController(ITarefasApp tarefasApp) : base(tarefasApp)
        {
            _tarefasApp = tarefasApp;
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
    }
}
