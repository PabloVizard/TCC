using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : BaseController
    {
        private readonly ITurmasApp _turmasApp;
        public TurmasController(ITurmasApp turmasApp)
        {
            _turmasApp = turmasApp;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _turmasApp.ListAsync());
        }
        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Atualizar(TurmasModel turmas)
        {
            var turma = await _turmasApp.FindByAsync(x => x.idTurmas == turmas.idTurmas);
            if (turma == null)
            {
                return BadRequest("Turma não existente");
            }

            turma.descricao = turmas.descricao;
            turma.flagPoc = turmas.flagPoc;
            turma.ano = turmas.ano;
            turma.semestre = turmas.semestre;


            _turmasApp.Update(turma);
            await _turmasApp.SaveChangesAsync();

            return Ok(turma);
        }
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar(TurmasModel turmas)
        {
            _turmasApp.Add(turmas);
            await _turmasApp.SaveChangesAsync();
            return Ok(turmas);
        }

        [HttpDelete]
        [Route("Remover")]
        public async Task<IActionResult> Remover(int idTurmas)
        {
            var turmas = _turmasApp.Find(idTurmas);
            _turmasApp.Remove(turmas);
            await _turmasApp.SaveChangesAsync();
            return Ok(new
            {
                data = turmas,
                message = "Turma removida com sucesso."
            });
        }
    }
}
