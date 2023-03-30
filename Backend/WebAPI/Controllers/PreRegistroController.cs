using Application.Applications.Interfaces;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreRegistroController : ControllerBase
    {
        private readonly IPreRegistroApp _preRegistroApp;
        public PreRegistroController(IPreRegistroApp preRegistroApp)
        {
            _preRegistroApp = preRegistroApp;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreRegistro>>> GetAll()
        {
            return await _preRegistroApp.ListAsync();
        }
    }
}
