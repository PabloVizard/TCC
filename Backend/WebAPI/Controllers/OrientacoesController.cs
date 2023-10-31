using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrientacoesController : BaseController<Orientacoes, OrientacoesModel>
    {
        private readonly IOrientacoesApp _orientacoesApp;
        public OrientacoesController(IOrientacoesApp orientacoesApp) : base(orientacoesApp)
        {
            _orientacoesApp = orientacoesApp;
        }
    }
}
