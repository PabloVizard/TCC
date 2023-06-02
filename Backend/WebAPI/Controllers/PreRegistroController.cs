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
        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Atualizar(PreRegistroModel preRegistro)
        {
            var preReg = await _preRegistroApp.FindByAsync(x => x.cpf == preRegistro.cpf);
            if (preReg == null)
            {
                return BadRequest("Pré-registro não existente");
            }

            preReg.cpf = preRegistro.cpf;
            preReg.aluno = preRegistro.aluno;
            preReg.cadastrado = preRegistro.cadastrado;
            preReg.coordenador = preRegistro.coordenador;
            preReg.orientador = preRegistro.orientador;
            preReg.professor = preRegistro.professor;
            preReg.ano = preRegistro.ano;
            preReg.semestre = preRegistro.semestre;


            _preRegistroApp.Update(preReg);
            await _preRegistroApp.SaveChangesAsync();

            return Ok(preReg);
        }
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar(PreRegistroModel preRegistro)
        {
            _preRegistroApp.Add(preRegistro);
            await _preRegistroApp.SaveChangesAsync();
            return Ok(await _preRegistroApp.FindByAsync(x => x.cpf == preRegistro.cpf));
        }

        [HttpDelete]
        [Route("Remover")]
        public async Task<IActionResult> Remover(int idPreRegistro)
        {
            var preRegistro = _preRegistroApp.Find(idPreRegistro);
            _preRegistroApp.Remove(preRegistro);
            await _preRegistroApp.SaveChangesAsync();
            return Ok(new
            {
                data = preRegistro,
                message = "Pré-Registro removido com sucesso."
            });
        }
    }
}
