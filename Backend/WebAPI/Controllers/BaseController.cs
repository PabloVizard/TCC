using Application.Models;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
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
