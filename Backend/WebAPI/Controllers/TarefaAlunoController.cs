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
    public class TarefaAlunoController : BaseController<TarefaAluno, TarefaAlunoModel>
    {
        private readonly ITarefaAlunoApp _tarefaAlunoApp;
        public TarefaAlunoController(ITarefaAlunoApp tarefaAlunoApp) : base(tarefaAlunoApp)
        {
            _tarefaAlunoApp = tarefaAlunoApp;
        }
    }
}
