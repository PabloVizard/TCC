using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MySqlConnector;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Dynamic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Model> : ControllerBase
        where Entity : BaseEntity
        where Model : BaseModel
    {
        protected readonly IBaseApp<Entity, Model> _baseApp;

        public BaseController(IBaseApp<Entity, Model> baseApp)
        {
            _baseApp = baseApp;
        }

        
        [HttpGet]
        [Route("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                return Ok(await _baseApp.ListAsync());
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Atualizar(Model dados)
        {
            try
            {
                var dadosFind = await _baseApp.AsNoTracking().FirstOrDefaultAsync(x => x.id == dados.id);
                if (dadosFind == null)
                {
                    return BadRequest("Dados não existente");
                }


                _baseApp.Update(dados);
                await _baseApp.SaveChangesAsync();

                return Ok(dados);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is MySqlException mysqlException && mysqlException.Message.Contains("Duplicate entry"))
                {
                    return BadRequest("O registro com o mesmo valor já existe. Por favor, verifique seus dados.");
                }

                return BadRequest("Erro Inesperado");

            }
            
        }
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar(Model dados)
        {
            try
            {
                var data = await _baseApp.Add(dados);
                await _baseApp.SaveChangesAsync();
                var dataEntity = (EntityEntry<Entity>)data;
                return Ok(dataEntity.Entity);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is MySqlException mysqlException && mysqlException.Message.Contains("Duplicate entry"))
                {
                    // Retornar uma mensagem padrão personalizada para o usuário
                    return BadRequest("O registro com o mesmo valor já existe. Por favor, verifique seus dados.");
                }

                return BadRequest("Erro Inesperado");

            }
            
        }

        [HttpDelete]
        [Route("Remover")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var dados = _baseApp.Find(id);
                _baseApp.Remove(dados);
                await _baseApp.SaveChangesAsync();
                return Ok(new
                {
                    data = dados,
                    message = "Removido com sucesso."
                });
            }
            catch (Exception ex)
            {

                return BadRequest("Erro Inesperado: " + ex.Message);
            }
            
        }
        protected async Task<string?> GetTokenAsync()
        {
            return await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
        }

        protected async Task<AuthModel> GetTokenAuthModelAsync()
        {
            return TokenAuthentication.GetTokenAuthModel(await GetTokenAsync());
        }

        protected string GenerateToken(AuthModel authModel)
        {
            return TokenAuthentication.GenerateToken(authModel);
        }
        protected string GetIpAddress()
        {
            return Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString()!;
        }
    }
}
