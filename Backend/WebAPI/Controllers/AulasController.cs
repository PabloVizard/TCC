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
    [Authorize]
    public class AulasController : BaseController<Aulas, AulasModel>
    {
        private readonly IAulasApp _aulasApp;
        private readonly IUsuariosApp _usuariosApp;
        private readonly ITurmasApp _turmasApp;
        private readonly IMapper _mapper;
        private readonly IUsuarioTurmaApp _usuarioTurmaApp;
        private readonly IFaltasApp _faltasApp;
        public AulasController(IAulasApp aulas, IUsuariosApp usuariosApp, ITurmasApp turmasApp, IMapper mapper, IUsuarioTurmaApp usuarioTurmaApp, IFaltasApp faltasApp) : base(aulas)
        {
            _aulasApp = aulas;
            _usuariosApp = usuariosApp;
            _turmasApp = turmasApp;
            _usuarioTurmaApp = usuarioTurmaApp;
            _mapper = mapper;
            _faltasApp = faltasApp;
        }

        [HttpGet]
        [Route("ObterAulasAluno")]
        public async Task<IActionResult> ObterAulasAluno()
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

                var turmaAluno = await _usuarioTurmaApp.FindByAsync(x => x.idUsuario == authModel.id);

                if (turmaAluno == null)
                {
                    return NoContent();
                }

                var turma = await _turmasApp.FindByAsync(x => x.id == turmaAluno.idTurma && x.ativo);

                if(turma == null)
                {
                    return NoContent();
                }

                var aulas = await _aulasApp.ListAsync(x => x.idTurma == turma.id);

                if (aulas == null)
                {
                    return NoContent();
                }

                List<AulasAlunoFullModel> aulasFullModel = new List<AulasAlunoFullModel>();

                foreach (var aula in aulas)
                {
                    aulasFullModel.Add(
                        new AulasAlunoFullModel
                        {
                            id = aula.id,
                            professor = _usuariosApp.ObterUsuarioLightPorId(aula.idProfessor),
                            turma = _turmasApp.ObterTurmaPorId(aula.idTurma),
                            dataAula = aula.dataAula,
                            descricao = aula.descricao,
                            local = aula.local,
                            link = aula.link,
                            falta = _faltasApp.Any(x => x.idAluno == authModel.id && x.idAula == aula.id)
                        });
                }
                

                return Ok(aulasFullModel);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
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

                var faltas = await _faltasApp.ListAsync(x => x.idAluno == authModel.id);

                if (faltas == null)
                {
                    return NoContent();
                }

                List<FaltasFullModel> faltasFullModel = new List<FaltasFullModel>();

                foreach (var falta in faltas)
                {
                    faltasFullModel.Add(
                        new FaltasFullModel
                        {
                            id = falta.id,
                            aluno = _usuariosApp.ObterUsuarioLightPorId(falta.idAluno),
                            aula = _aulasApp.FindBy( x => x.id == falta.idAula),
                            turma = _turmasApp.ObterTurmaPorId(falta.idTurma)
                        });
                }


                return Ok(faltasFullModel);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterAulasProfessor")]
        public async Task<IActionResult> ObterAulasProfessor()
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

                var aulas = await _aulasApp.ListAsync(x => x.idProfessor == authModel.id);

                if (aulas == null)
                {
                    return NoContent();
                }

                List<AulasFullModel> aulasFullModel = new List<AulasFullModel>();

                foreach (var aula in aulas)
                {
                    aulasFullModel.Add(
                        new AulasFullModel
                        {
                            id = aula.id,
                            professor = _usuariosApp.ObterUsuarioLightPorId(aula.idProfessor),
                            turma = _turmasApp.ObterTurmaPorId(aula.idTurma),
                            dataAula = aula.dataAula,
                            descricao = aula.descricao,
                            local = aula.local,
                            link = aula.link,
                        });
                }


                return Ok(aulasFullModel);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterAulaPorId")]
        public async Task<IActionResult> ObterAulaPorId(int aulaId)
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

                var aula = await _aulasApp.FindByAsync(x => x.id == aulaId);

                if (aula == null)
                {
                    return NoContent();
                }

                AulasFullModel aulasFullModel = new AulasFullModel
                {

                    id = aula.id,
                    professor = _usuariosApp.ObterUsuarioLightPorId(aula.idProfessor),
                    turma = _turmasApp.ObterTurmaPorId(aula.idTurma),
                    dataAula = aula.dataAula,
                    descricao = aula.descricao,
                    local = aula.local,
                    link = aula.link,
                };


                return Ok(aulasFullModel);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
    }
}
