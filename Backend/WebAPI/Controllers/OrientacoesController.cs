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
    public class OrientacoesController : BaseController<Orientacoes, OrientacoesModel>
    {
        private readonly IOrientacoesApp _orientacoesApp;
        private readonly IUsuariosApp _usuariosApp;
        private readonly ITurmasApp _turmasApp;
        private readonly IProjetosApp _projetosApp;
        private readonly IMapper _mapper;
        public OrientacoesController(IOrientacoesApp orientacoesApp, IUsuariosApp usuariosApp, ITurmasApp turmasApp, IProjetosApp projetosApp, IMapper mapper) : base(orientacoesApp)
        {
            _orientacoesApp = orientacoesApp;
            _usuariosApp = usuariosApp;
            _turmasApp = turmasApp;
            _projetosApp = projetosApp;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("ObterOrientacaoAluno")]
        public async Task<IActionResult> ObterOrientacaoAluno()
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

                var orientacao = await _orientacoesApp.FindByAsync(x => x.idAlunoOrientado == authModel.id);

                if (orientacao == null)
                {
                    return NoContent();
                }

                OrientacoesFullModel orientacaoFull = new OrientacoesFullModel();

                orientacaoFull = new OrientacoesFullModel
                {
                    id = orientacao.id,
                    ProfessorOrientador = ObterUsuarioLightPorId(orientacao.idProfessorOrientador),
                    AlunoOrientado = ObterUsuarioLightPorId((int)orientacao.idAlunoOrientado!),
                    Projeto = ObterProjetoPorId(orientacao.idProjeto),
                    Turma = ObterTurmaPorId(orientacao.idTurma),
                    statusAprovacao = orientacao.statusAprovacao,
                };

                return Ok(orientacaoFull);
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
        private ProjetosModel ObterProjetoPorId(int idProjeto)
        {
            var projeto = _projetosApp.Find(idProjeto);

            if (projeto == null)
            {
                return null;
            }
            var projetoModel = _mapper.Map<ProjetosModel>(projeto);

            return projetoModel;
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
