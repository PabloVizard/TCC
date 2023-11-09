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
        public UsuarioTurmaController(IUsuarioTurmaApp usuarioTurmaApp, IUsuariosApp usuariosApp, ITurmasApp turmasApp) : base(usuarioTurmaApp)
        {
            _usuarioTurmaApp = usuarioTurmaApp;
            _usuarioApp = usuariosApp;
            _turmasApp = turmasApp;
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
    }
}
