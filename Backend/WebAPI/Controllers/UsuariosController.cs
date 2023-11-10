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
    public class UsuariosController : BaseController<Usuarios, UsuariosModel>
    {
        private readonly IUsuariosApp _usuariosApp;
        public UsuariosController(IUsuariosApp usuariosApp) : base(usuariosApp)
        {
            _usuariosApp = usuariosApp;
        }
        [HttpGet]
        [Route("ObterPorId")]
        public override async Task<IActionResult> ObterPorId(int id)
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

                var usuario = await _usuariosApp.FindAsync(id);

                if (usuario == null)
                {
                    return NoContent();
                }

                UsuariosLightModel usuariosLightModel = new UsuariosLightModel
                {
                    id = usuario.id,
                    email = usuario.email,
                    nomeCompleto = usuario.nomeCompleto,
                    tipoUsuario = usuario.tipoUsuario
                };

                return Ok(usuariosLightModel);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
    }
}
