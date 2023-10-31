using Application.Applications;
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
    public class UsuarioTurmaController : BaseController<UsuarioTurma, UsuarioTurmaModel>
    {
        private readonly IUsuarioTurmaApp _usuarioTurmaApp;
        public UsuarioTurmaController(IUsuarioTurmaApp usuarioTurmaApp) : base(usuarioTurmaApp)
        {
            _usuarioTurmaApp = usuarioTurmaApp;
        }
    }
}
