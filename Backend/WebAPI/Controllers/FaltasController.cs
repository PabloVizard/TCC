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
    public class FaltasController : BaseController<Faltas, FaltasModel>
    {
        private readonly IFaltasApp _faltasApp;
        private readonly IUsuariosApp _usuariosApp;
        public FaltasController(IFaltasApp faltasApp, IUsuariosApp usuariosApp) : base(faltasApp)
        {
            _faltasApp = faltasApp;
            _usuariosApp = usuariosApp;
        }

        [HttpGet]
        [Route("ObterFaltasAluno")]
        public async Task<IActionResult> ObterFaltasAluno()
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

                var faltasAluno = await _faltasApp.ListAsync(x => x.idAluno == authModel.id);

                if (faltasAluno is null)
                {
                    return BadRequest("UsuarioTurma não encontrado.");
                }
                List<FaltasFullModel> faltasFull = new List<FaltasFullModel>();
                foreach (var falta in faltasAluno)
                {
                    faltasFull.Add(new FaltasFullModel
                    {
                        id = falta.id,
                        aluno  = _usuariosApp.ObterUsuarioLightPorId(falta.idAluno)
                    });
                }
                


                return Ok(faltasAluno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
    }
}
