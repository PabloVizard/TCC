using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreRegistroController : BaseController
    {
        private readonly IPreRegistroApp _preRegistroApp;
        public PreRegistroController(IPreRegistroApp preRegistroApp)
        {
            _preRegistroApp = preRegistroApp;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _preRegistroApp.ListAsync());
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(PreRegistroModel preRegistro)
        {
            _preRegistroApp.Add(preRegistro);
            await _preRegistroApp.SaveChangesAsync();
            return Ok(await _preRegistroApp.FindByAsync(x => x.cpf == preRegistro.cpf));
        }
    }
}
