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
    public class UsuarioTurmaController : BaseController<UsuarioTurma, UsuarioTurmaModel>
    {
        private readonly IUsuarioTurmaApp _usuarioTurmaApp;
        private readonly IUsuariosApp _usuarioApp;
        private readonly ITurmasApp _turmasApp;
        private readonly IFaltasApp _faltasApp;
        public UsuarioTurmaController(IUsuarioTurmaApp usuarioTurmaApp, IUsuariosApp usuariosApp, ITurmasApp turmasApp, IFaltasApp faltasApp) : base(usuarioTurmaApp)
        {
            _usuarioTurmaApp = usuarioTurmaApp;
            _usuarioApp = usuariosApp;
            _turmasApp = turmasApp;
            _faltasApp = faltasApp;
        }

        [HttpGet]
        [Route("ObterUsuariosPorId")]
        public async Task<IActionResult> ObterUsuariosPorId(int id)
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

                var usuarioTurma = await _usuarioTurmaApp.FindAsync(id);

                if(usuarioTurma is null)
                {
                    return BadRequest("UsuarioTurma não encontrado.");
                }

                var usuario = await _usuarioApp.FindAsync(usuarioTurma.idUsuario);

                if(usuario is null)
                {
                    return BadRequest("Usuario não encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterTodosUsuariosPorTurma")]
        public async Task<IActionResult> ObterTodosUsuariosPorTurma(int idTurma)
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

                var usuariosTurma = await _usuarioTurmaApp.ListAsync(x => x.idTurma == idTurma);

                if (usuariosTurma is null)
                {
                    return BadRequest("UsuariosTurma não encontrados.");
                }

                List<int> userIds = usuariosTurma.Select(ut => ut.idUsuario).ToList();

                var usuarios = await _usuarioApp.FindByAsync(user => userIds.Contains(user.id));

                if (usuarios == null)
                {
                    return BadRequest("Usuários não encontrados.");
                }

                return Ok(usuarios);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterTurmaPorUsuarioId")]
        public async Task<IActionResult> ObterTurmaPorUsuarioId(int idUsuario)
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

                var usuarioTurma = await _usuarioTurmaApp.FindByAsync(x => x.idUsuario == idUsuario);

                if (usuarioTurma is null)
                {
                    return BadRequest("UsuarioTurma não encontrado.");
                }

                var turma = await _turmasApp.FindAsync(usuarioTurma.idTurma);

                if (turma == null)
                {
                    return BadRequest("Turma não encontrada.");
                }

                return Ok(turma);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterAlunosPorTurma")]
        public async Task<IActionResult> ObterAlunosPorTurma(int turmaId, int aulaId)
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

                var usuariosTurma = await _usuarioTurmaApp.ListAsync(x => x.idTurma == turmaId);

                if (usuariosTurma is null)
                {
                    return BadRequest("UsuarioTurma não encontrado.");
                }

                List<UsuarioTurmaFullModel> usuarios = new List<UsuarioTurmaFullModel>();

                foreach (var usuario in usuariosTurma)
                {
                    usuarios.Add(new UsuarioTurmaFullModel
                    {
                        id = usuario.id,
                        turma = _turmasApp.ObterTurmaPorId(usuario.idTurma),
                        usuario = _usuarioApp.ObterUsuarioLightPorId(usuario.idUsuario),
                        idFalta = _faltasApp.FindBy(x => x.idAula == aulaId && x.idAluno == usuario.idUsuario)?.id,
                        faltaAula = _faltasApp.Any(x => x.idAula == aulaId && x.idAluno == usuario.idUsuario),
                        quantidadeFaltas = _faltasApp.List(x => x.idAluno == usuario.idUsuario).Count()
                    });
                }

                return Ok(usuarios);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterTurmasProfessor")]
        public async Task<IActionResult> ObterTurmasProfessor()
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

                var turma = await _turmasApp.ListAsync(x => x.ativo);

                if (turma == null)
                {
                    return NoContent();
                }


                return Ok(turma);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterTurmaAluno")]
        public async Task<IActionResult> ObterTurmaAluno()
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

                var turmaUsuario = await _usuarioTurmaApp.FindByAsync(x => x.idUsuario == authModel.id);

                if (turmaUsuario == null)
                {
                    return NoContent();
                }

                var turma = await _turmasApp.FindByAsync(x => x.id == turmaUsuario.idTurma);

                if (turma == null)
                {
                    return NoContent();
                }


                return Ok(turma);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        private UsuariosLightModel ObterUsuarioLightPorId(int idUsuario)
        {
            var usuario = _usuarioApp.Find(idUsuario);

            if (usuario == null)
            {
                return null;
            }

            return new UsuariosLightModel { id = usuario.id, nomeCompleto = usuario.nomeCompleto, email = usuario.email, tipoUsuario = usuario.tipoUsuario };
        }
    }
}
