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
        public AulasController(IAulasApp aulas, IUsuariosApp usuariosApp, ITurmasApp turmasApp, IMapper mapper) : base(aulas)
        {
            _aulasApp = aulas;
            _usuariosApp = usuariosApp;
            _turmasApp = turmasApp;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("ObterAulasAluno")]
        public async Task<IActionResult> ObterAulasAluno(int turmaId)
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

                var aulas = await _aulasApp.ListAsync(x => x.idTurma == turmaId);

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
                            professor = ObterUsuarioLightPorId(aula.idProfessor),
                            turma = ObterTurmaPorId(aula.idTurma),
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
                            professor = ObterUsuarioLightPorId(aula.idProfessor),
                            turma = ObterTurmaPorId(aula.idTurma),
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
