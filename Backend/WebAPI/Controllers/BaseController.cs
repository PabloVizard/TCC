using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        [Route("ObterPorId")]
        public virtual async Task<IActionResult> ObterPorId(int id)
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

                return Ok(await _baseApp.FindAsync(id));
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterTodos")]
        public virtual async Task<IActionResult> ObterTodos()
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
                var retorno = await _baseApp.ListAsync();

                return Ok(retorno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public virtual async Task<IActionResult> Atualizar(Model dados)
        {
            try
            {
                var dadosFind = await _baseApp.FindAsync(dados.id);
                if (dadosFind == null)
                {
                    return BadRequest("Dados não existente");
                }

                // Obter todas as propriedades de 'dados' e 'dadosFind'
                var dadosProperties = dados.GetType().GetProperties();
                var dadosFindProperties = dadosFind.GetType().GetProperties();

                // Mapear as propriedades e seus valores para um dicionário
                var propertyValueMap = new Dictionary<string, object>();
                foreach (var property in dadosProperties)
                {
                    propertyValueMap[property.Name] = property.GetValue(dados);
                }

                // Iterar sobre as propriedades do objeto encontrado e definir os valores
                foreach (var property in dadosFindProperties)
                {
                    if (propertyValueMap.ContainsKey(property.Name))
                    {
                        property.SetValue(dadosFind, propertyValueMap[property.Name]);
                    }
                }

                _baseApp.Update(dadosFind);
                await _baseApp.SaveChangesAsync();

                return Ok(dadosFind);
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
        public virtual async Task<IActionResult> Registrar(Model dados)
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
        public virtual async Task<IActionResult> Remover(int id)
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
