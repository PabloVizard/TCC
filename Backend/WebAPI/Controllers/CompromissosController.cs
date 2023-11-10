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
    [Authorize]
    public class CompromissosController : BaseController<Compromissos, CompromissosModel>
    {
        private readonly ICompromissosApp _compromissosApp;
        private readonly IUsuariosApp _usuariosApp;
        private readonly ITurmasApp _turmasApp;
        private readonly IMapper _mapper;
        public CompromissosController(ICompromissosApp compromissos, IUsuariosApp usuariosApp, ITurmasApp turmasApp, IMapper mapper) : base(compromissos)
        {
            _compromissosApp = compromissos;
            _usuariosApp = usuariosApp;
            _turmasApp = turmasApp;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("ObterCompromissosAluno")]
        public async Task<IActionResult> ObterCompromissosAluno()
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

                var compromissos = await _compromissosApp.ListAsync(x => x.idAlunoOrientado == authModel.id);

                if (compromissos == null)
                {
                    return NoContent();
                }

                List<CompromissosFullModel> compromissosFullModel = new List<CompromissosFullModel>();

                foreach (var compromisso in compromissos)
                {
                    compromissosFullModel.Add(
                        new CompromissosFullModel
                        {
                            id = compromisso.id,
                            professorOrientador = ObterUsuarioLightPorId(compromisso.idProfessorOrientador),
                            alunoOrientado = ObterUsuarioLightPorId(compromisso.idAlunoOrientado is not null ? (int)compromisso.idAlunoOrientado : 0),
                            turma = ObterTurmaPorId(compromisso.idTurma is not null ? (int)compromisso.idTurma : 0),
                            dataCompromisso = compromisso.dataCompromisso,
                            descricao = compromisso.descricao,
                            tipoCompromisso = compromisso.tipoCompromisso,
                            local = compromisso.local,
                            link = compromisso.link,
                        });
                }
                

                return Ok(compromissosFullModel);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterCompromissosProfessor")]
        public async Task<IActionResult> ObterCompromissosProfessor()
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

                var compromissos = await _compromissosApp.ListAsync(x => x.idProfessorOrientador == authModel.id);

                if (compromissos == null)
                {
                    return NoContent();
                }

                List<CompromissosFullModel> compromissosFullModel = new List<CompromissosFullModel>();

                foreach (var compromisso in compromissos)
                {
                    compromissosFullModel.Add(
                        new CompromissosFullModel
                        {
                            id = compromisso.id,
                            professorOrientador = ObterUsuarioLightPorId(compromisso.idProfessorOrientador),
                            alunoOrientado = ObterUsuarioLightPorId(compromisso.idAlunoOrientado is not null ? (int)compromisso.idAlunoOrientado : 0),
                            turma = ObterTurmaPorId(compromisso.idTurma is not null ? (int)compromisso.idTurma : 0),
                            dataCompromisso = compromisso.dataCompromisso,
                            descricao = compromisso.descricao,
                            tipoCompromisso = compromisso.tipoCompromisso
                        });
                }


                return Ok(compromissosFullModel);
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
