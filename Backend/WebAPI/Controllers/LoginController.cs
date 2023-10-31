using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IUsuariosApp _usuariosApp;
        private readonly IPreRegistroApp _preRegistroApp;
        public LoginController(IUsuariosApp usuariosApp, IPreRegistroApp preRegistroApp)
        {
            _usuariosApp = usuariosApp;
            _preRegistroApp = preRegistroApp;
        }

        [HttpPost]
        [Route("LoginUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUsuario([FromBody] LoginModel loginModel)
        {
            try
            {
                if (loginModel == null || string.IsNullOrEmpty(loginModel.email) || string.IsNullOrEmpty(loginModel.senha))
                {
                    return Unauthorized("Parametros Invalidos");
                }

                loginModel.senha = Cryptography.ConvertToSha256Hash(loginModel.senha).ToLower();

                var user = await _usuariosApp.FindByAsync(x => x.email == loginModel.email && x.senha == loginModel.senha);

                if (user == null)
                {
                    return Unauthorized("Usuário ou Senha Invalidos");
                }

                AuthModel authModel = new AuthModel
                {
                    id = user.id,
                    nomeCompleto = user.nomeCompleto,
                    email = user.email,
                    senha = user.senha,
                    ip = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString()!,
                    tipoUsuario = user.tipoUsuario

                };


                var tokenJWT = TokenAuthentication.GenerateToken(authModel);

                return Ok(new
                {
                    data = user,
                    token = tokenJWT,
                });
            }
            catch (System.Exception)
            {
                return BadRequest("Erro Inesperado");
            }


        }

        [HttpPost]
        [Route("RegistrarUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuariosModel usuarios)
        {
            try
            {
                if (usuarios == null)
                {
                    return Unauthorized("Parametros Invalidos");
                }


                if (await _usuariosApp.AnyAsync(x => x.cpf == usuarios.cpf || x.email == usuarios.email))
                {
                    return BadRequest("Usuário Já Cadastrado");
                }

                usuarios.senha = Cryptography.ConvertToSha256Hash(usuarios.senha).ToLower();

                var preRegistro = await _preRegistroApp.FindByAsync(x => x.cpf == usuarios.cpf);

                if (preRegistro == null)
                {
                    return BadRequest("Usuário não foi pré-cadastrado");
                }

                await _usuariosApp.Add(usuarios);

                preRegistro.cadastrado = true;
                _preRegistroApp.UpdateEntity(preRegistro);

                await _usuariosApp.SaveChangesAsync();



                return Ok(new
                {
                    data = usuarios,
                    message = "Usuário cadastrado com sucesso.",
                });
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpPut]
        [Route("AtualizarUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarUsuario([FromBody] UsuariosModel usuarios)
        {
            try
            {
                if (usuarios == null)
                {
                    return Unauthorized("Parametros Invalidos");
                }
                var usuario = _usuariosApp.Find(usuarios.id);
                if (usuario == null)
                {
                    return BadRequest("Usuário não existente.");
                }
                usuarios.senha = Cryptography.ConvertToSha256Hash(usuarios.senha).ToLower();

                usuario.senha = usuarios.senha ?? usuario.senha;
                usuario.email = usuarios.email ?? usuario.email;
                usuario.nomeCompleto = usuarios.nomeCompleto ?? usuario.nomeCompleto;
                usuario.tipoUsuario = usuarios.tipoUsuario;

                _usuariosApp.UpdateEntity(usuario);
                await _usuariosApp.SaveChangesAsync();

                return Ok(new {
                    data = usuario,
                    message = "Usuário atualizado com sucesso."
                });

            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpDelete]
        [Route("DeletarUsuario")]
        public async Task<IActionResult> DeletarUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario == null)
                {
                    return Unauthorized("Parametros Invalidos");
                }
                var usuario = _usuariosApp.Find(idUsuario);
                if (usuario == null)
                {
                    return BadRequest("Usuário não existente.");
                }

                _usuariosApp.Remove(usuario);
                await _usuariosApp.SaveChangesAsync();

                return Ok(new
                {
                    data = usuario,
                    message = "Usuário removido com sucesso."
                });

            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
    }
}
