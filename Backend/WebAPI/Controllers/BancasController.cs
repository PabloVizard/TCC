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
        private readonly IUsuariosApp _usuariosApp;
        public BancasController(IBancasApp bancasApp, IUsuariosApp usuariosApp) : base(bancasApp)
        {
            _bancasApp = bancasApp;
            _usuariosApp = usuariosApp;
        }
        [HttpGet]
        [Route("ObterBancaPorAlunoId")]
        public async Task<IActionResult> ObterBancaPorAlunoId(int alunoId)
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

                var banca = await _bancasApp.FindByAsync(x => x.idAlunoOrientado == alunoId);

                if (banca == null)
                {
                    return NoContent();
                }

                BancasFullModel bancaFull = new BancasFullModel
                {
                    id = banca.id,
                    professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                    alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                    avaliador01 = obterUsuarioLightPorId(banca.idAvaliador01),
                    avaliador02 = banca.idAvaliador02 != null ? obterUsuarioLightPorId((int)banca.idAvaliador02) : null,
                    ano = banca.ano,
                    bancaConfirmada = banca.bancaConfirmada,
                    dataDefesa = banca.dataDefesa,
                    semestre = banca.semestre
                };

                return Ok(bancaFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        private UsuariosLightModel obterUsuarioLightPorId(int idUsuario)
        {
            var usuario = _usuariosApp.Find(idUsuario);

            if (usuario == null)
            {
                return null;
            }

            return new UsuariosLightModel { id = usuario.id, nomeCompleto = usuario.nomeCompleto, email = usuario.email, tipoUsuario = usuario.tipoUsuario };
        }
    }
}
