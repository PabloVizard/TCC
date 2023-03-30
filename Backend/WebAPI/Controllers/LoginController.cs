using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IUsuariosApp _usuariosApp;
        public LoginController(IUsuariosApp usuariosApp)
        {
            _usuariosApp = usuariosApp;
        }

        [HttpPost]
        [Route("LoginUsuario")]
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
                    idUsuarios = user.idUsuarios,
                    nomeCompleto = user.nomeCompleto,
                    email = user.email,
                    senha = user.senha,
                    ip = GetIpAddress()
                };


                var tokenJWT = TokenAuthentication.GenerateToken(authModel);

                return Ok(new
                {
                    usuario = user,
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
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuariosModel usuarios)
        {
            try
            {
                if (usuarios == null)
                {
                    return Unauthorized("Parametros Invalidos");
                }


                if (await _usuariosApp.AnyAsync(x => x.email == usuarios.email))
                {
                    return BadRequest("Usuário Já Cadastrado");
                }

                usuarios.senha = Cryptography.ConvertToSha256Hash(usuarios.senha).ToLower();

                _usuariosApp.Add(usuarios);
                await _usuariosApp.SaveChangesAsync();



                return Ok(new
                {
                    usuario = usuarios,
                    message = "Usuário cadastrado com sucesso.",
                });
            }
            catch (System.Exception)
            {
                return BadRequest("Erro Inesperado");
            }


        }
    }
}
