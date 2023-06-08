using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreRegistroController : BaseController<PreRegistro, PreRegistroModel>
    {
        private readonly IPreRegistroApp _preRegistroApp;
        public PreRegistroController(IPreRegistroApp preRegistroApp) : base(preRegistroApp)
        {
            _preRegistroApp = preRegistroApp;
        }
    }
}
