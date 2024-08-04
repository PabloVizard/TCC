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
        private readonly IProjetosApp _projetosApp;
        public BancasController(IBancasApp bancasApp, IUsuariosApp usuariosApp, IProjetosApp projetosApp) : base(bancasApp)
        {
            _bancasApp = bancasApp;
            _usuariosApp = usuariosApp;
            _projetosApp = projetosApp;
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
                    projeto = await _projetosApp.FindByAsync(x => x.id == banca.idProjeto),
                    professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                    alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                    avaliador01 = obterUsuarioLightPorId(banca.idAvaliador01),
                    avaliador02 = banca.idAvaliador02 != null ? obterUsuarioLightPorId((int)banca.idAvaliador02) : null,
                    ano = banca.ano,
                    bancaConfirmada = banca.bancaConfirmada,
                    dataDefesa = banca.dataDefesa,
                    semestre = banca.semestre,
                    status = banca.status
                };

                return Ok(bancaFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterBancaPorOrientadorId")]
        public async Task<IActionResult> ObterBancaPorOrientadorId(int orientadorId)
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

                var bancas = await _bancasApp.ListAsync(x => x.idProfessorOrientador == orientadorId);

                if (bancas == null)
                {
                    return NoContent();
                }

                List<BancasFullModel> bancasFull = new List<BancasFullModel>();

                foreach (var banca in bancas)
                {
                    bancasFull.Add(new BancasFullModel
                        {
                            id = banca.id,
                            projeto = await _projetosApp.FindByAsync( x => x.id == banca.idProjeto),
                            professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                            alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                            avaliador01 = obterUsuarioLightPorId(banca.idAvaliador01),
                            avaliador02 = banca.idAvaliador02 != null ? obterUsuarioLightPorId((int)banca.idAvaliador02) : null,
                            ano = banca.ano,
                            bancaConfirmada = banca.bancaConfirmada,
                            dataDefesa = banca.dataDefesa,
                            semestre = banca.semestre
                        });
                }
                

                return Ok(bancasFull);
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
