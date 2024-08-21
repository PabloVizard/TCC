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
    public class TurmasController : BaseController<Turmas, TurmasModel>
    {
        private readonly ITurmasApp _turmasApp;
        public TurmasController(ITurmasApp turmasApp) : base(turmasApp)
        {
            _turmasApp = turmasApp;
        }
        [HttpGet]
        [Route("ObterTurmasAtivas")]
        public virtual async Task<IActionResult> ObterTurmasAtivas()
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
                var retorno = await _turmasApp.ListAsync(x => x.ativo);

                return Ok(retorno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterTodasTurmas")]
        public virtual async Task<IActionResult> ObterTodasTurmas()
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
                var retorno = await _turmasApp.ListAsync();

                return Ok(retorno.OrderByDescending(x => x.ano).ThenByDescending(x => x.semestre));
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
    }
}
