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
    public class ProjetosController : BaseController<Projetos, ProjetosModel>
    {
        private readonly IProjetosApp _projetosApp;
        private readonly IUsuariosApp _usuariosApp;
        public ProjetosController(IProjetosApp projetosApp, IUsuariosApp usuariosApp) : base(projetosApp)
        {
            _projetosApp = projetosApp;
            _usuariosApp = usuariosApp;
        }
        [HttpGet]
        [Route("ObterProjetosDisponiveis")]
        public async Task<IActionResult> ObterProjetosDisponiveis()
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

                var projetosDisponiveis = await _projetosApp.ListAsync(x => x.idAlunoResponsavel == null);

                if(projetosDisponiveis == null)
                {
                    return NoContent();
                }

                List<ProjetosFullModel> projetosDisponiveisFull = new List<ProjetosFullModel>();

                foreach (var projetos in projetosDisponiveis)
                {
                    projetosDisponiveisFull.Add(new ProjetosFullModel
                    {
                        nome = projetos.nome,
                        descricao = projetos.descricao,
                        ProfessorResponsavel = obterUsuarioLightPorId(projetos.idProfessorResponsavel),
                        AlunoResponsavel = obterUsuarioLightPorId(projetos.idAlunoResponsavel is not null ? (int)projetos.idAlunoResponsavel : 0)
                    });
                }

                return Ok(projetosDisponiveisFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterProjetoAluno")]
        public async Task<IActionResult> ObterProjetoAluno()
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

                var projetoAtual = await _projetosApp.FindByAsync(x => x.idAlunoResponsavel == authModel.id);

                if (projetoAtual == null)
                {
                    return NoContent();
                }

                ProjetosFullModel projetoFull = new ProjetosFullModel();

                projetoFull = new ProjetosFullModel
                {
                    nome = projetoAtual.nome,
                    descricao = projetoAtual.descricao,
                    ProfessorResponsavel = obterUsuarioLightPorId(projetoAtual.idProfessorResponsavel),
                    AlunoResponsavel = obterUsuarioLightPorId((int)projetoAtual.idAlunoResponsavel!)
                };

                return Ok(projetoFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterListaProjetoProfessor")]
        public async Task<IActionResult> ObterListaProjetoProfessor()
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

                var projetosProfessor= await _projetosApp.ListAsync(x => x.idProfessorResponsavel == authModel.id);

                if (projetosProfessor == null)
                {
                    return NoContent();
                }

                List<ProjetosFullModel> projetosProfessorFull = new List<ProjetosFullModel>();

                foreach (var projetos in projetosProfessor)
                {
                    projetosProfessorFull.Add(new ProjetosFullModel
                    {
                        nome = projetos.nome,
                        descricao = projetos.descricao,
                        ProfessorResponsavel = obterUsuarioLightPorId(projetos.idProfessorResponsavel),
                        AlunoResponsavel = obterUsuarioLightPorId(projetos.idAlunoResponsavel is not null ? (int)projetos.idAlunoResponsavel : 0)
                    });
                }

                return Ok(projetosProfessorFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        private UsuariosLightModel obterUsuarioLightPorId(int idUsuario)
        {
            var usuario = _usuariosApp.Find(idUsuario);

            if(usuario == null)
            {
                return null;
            }

            return new UsuariosLightModel { id = usuario.id,  nomeCompleto = usuario.nomeCompleto, email = usuario.email, tipoUsuario = usuario.tipoUsuario};
        }
    }
}
