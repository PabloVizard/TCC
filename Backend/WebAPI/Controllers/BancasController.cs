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
    public class BancasController : BaseController<Bancas, BancasModel>
    {
        private readonly IBancasApp _bancasApp;
        public BancasController(IBancasApp bancasApp) : base(bancasApp)
        {
            _bancasApp = bancasApp;
        }
        [HttpGet]
        [Route("ObterBancaPorAlunoId")]
        public async Task<IActionResult> ObterBancaPorAlunoId()
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

                var banca = await _bancasApp.FindByAsync(x => x.idAlunoOrientado == authModel.id);

                if (banca == null)
                {
                    return NoContent();
                }

                return Ok(banca);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
    }
}
