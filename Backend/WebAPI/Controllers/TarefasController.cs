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
    public class TarefasController : BaseController<Tarefas, TarefasModel>
    {
        private readonly ITarefasApp _tarefasApp;
        public TarefasController(ITarefasApp tarefasApp) : base(tarefasApp)
        {
            _tarefasApp = tarefasApp;
        }
    }
}
