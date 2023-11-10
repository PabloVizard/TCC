﻿using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjetosController : BaseController<Projetos, ProjetosModel>
    {
        private readonly IProjetosApp _projetosApp;
        private readonly IUsuariosApp _usuariosApp;
        private readonly IUsuarioTurmaApp _usuarioTurmaApp;
        private readonly IOrientacoesApp _orientacoesApp;
        public ProjetosController(IProjetosApp projetosApp, IUsuariosApp usuariosApp, IUsuarioTurmaApp usuarioTurmaApp, IOrientacoesApp orientacoesApp) : base(projetosApp)
        {
            _projetosApp = projetosApp;
            _usuariosApp = usuariosApp;
            _usuarioTurmaApp = usuarioTurmaApp;
            _orientacoesApp = orientacoesApp;
        }
        [HttpGet]
        [Route("ObterProjetosDisponiveis")]
        public async Task<IActionResult> ObterProjetosDisponiveis()
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }

                var projetosDisponiveis = await _projetosApp.ListAsync(x => x.idAlunoResponsavel == null);

                if(projetosDisponiveis == null)
                {
                    return NoContent();
                }

                List<ProjetosFullModel> projetosDisponiveisFull = new List<ProjetosFullModel>();

                foreach (var projetos in projetosDisponiveis)
                {
                    projetosDisponiveisFull.Add(new ProjetosFullModel
                    {
                        id = projetos.id,
                        nome = projetos.nome,
                        descricao = projetos.descricao,
                        ProfessorResponsavel = obterUsuarioLightPorId(projetos.idProfessorResponsavel),
                        AlunoResponsavel = obterUsuarioLightPorId(projetos.idAlunoResponsavel is not null ? (int)projetos.idAlunoResponsavel : 0)
                    });
                }

                return Ok(projetosDisponiveisFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterProjetosDisponiveisProfessor")]
        public async Task<IActionResult> ObterProjetosDisponiveisProfessor(int idProfessor)
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }

                var projetosDisponiveis = await _projetosApp.ListAsync(x => x.idProfessorResponsavel == idProfessor);

                if (projetosDisponiveis == null)
                {
                    return NoContent();
                }

                List<ProjetosFullModel> projetosDisponiveisFull = new List<ProjetosFullModel>();

                foreach (var projetos in projetosDisponiveis)
                {
                    projetosDisponiveisFull.Add(new ProjetosFullModel
                    {
                        id = projetos.id,
                        nome = projetos.nome,
                        descricao = projetos.descricao,
                        ProfessorResponsavel = obterUsuarioLightPorId(projetos.idProfessorResponsavel),
                        AlunoResponsavel = obterUsuarioLightPorId(projetos.idAlunoResponsavel is not null ? (int)projetos.idAlunoResponsavel : 0)
                    });
                }

                return Ok(projetosDisponiveisFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }


        [HttpGet]
        [Route("ObterProjetoAluno")]
        public async Task<IActionResult> ObterProjetoAluno()
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }

                var projetoAtual = await _projetosApp.FindByAsync(x => x.idAlunoResponsavel == authModel.id);

                if (projetoAtual == null)
                {
                    return NoContent();
                }

                ProjetosFullModel projetoFull = new ProjetosFullModel();

                projetoFull = new ProjetosFullModel
                {
                    nome = projetoAtual.nome,
                    descricao = projetoAtual.descricao,
                    ProfessorResponsavel = obterUsuarioLightPorId(projetoAtual.idProfessorResponsavel),
                    AlunoResponsavel = obterUsuarioLightPorId((int)projetoAtual.idAlunoResponsavel!)
                };

                return Ok(projetoFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterListaProjetoProfessor")]
        public async Task<IActionResult> ObterListaProjetoProfessor()
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }

                var projetosProfessor= await _projetosApp.ListAsync(x => x.idProfessorResponsavel == authModel.id);

                if (projetosProfessor == null)
                {
                    return NoContent();
                }

                List<ProjetosFullModel> projetosProfessorFull = new List<ProjetosFullModel>();

                foreach (var projetos in projetosProfessor)
                {
                    projetosProfessorFull.Add(new ProjetosFullModel
                    {
                        nome = projetos.nome,
                        descricao = projetos.descricao,
                        ProfessorResponsavel = obterUsuarioLightPorId(projetos.idProfessorResponsavel),
                        AlunoResponsavel = obterUsuarioLightPorId(projetos.idAlunoResponsavel is not null ? (int)projetos.idAlunoResponsavel : 0)
                    });
                }

                return Ok(projetosProfessorFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public override async Task<IActionResult> Atualizar(ProjetosModel dados)
        {
            try
            {
                var dadosFind = await _projetosApp.FindAsync(dados.id);
                if (dadosFind == null)
                {
                    return BadRequest("Dados não existente");
                }

                var dadosProperties = dados.GetType().GetProperties();
                var dadosFindProperties = dadosFind.GetType().GetProperties();

                foreach (var property in dadosProperties)
                {
                    var findProperty = dadosFindProperties.FirstOrDefault(p => p.Name == property.Name);
                    if (findProperty != null && property.GetValue(dados) != null)
                    {
                        findProperty.SetValue(dadosFind, property.GetValue(dados));
                    }
                }

                _projetosApp.Update(dadosFind);
                await _projetosApp.SaveChangesAsync();

                return Ok(dadosFind);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is MySqlException mysqlException && mysqlException.Message.Contains("Duplicate entry"))
                {
                    return BadRequest("O registro com o mesmo valor já existe. Por favor, verifique seus dados.");
                }

                return BadRequest("Erro Inesperado");

            }

        }
        [HttpPost]
        [Route("Registrar")]
        public override async Task<IActionResult> Registrar(ProjetosModel dados)
        {
            try
            {
                var data = await _projetosApp.Add(dados);
                await _projetosApp.SaveChangesAsync();
                var dataEntity = (EntityEntry<Projetos>)data;

                if(dataEntity != null && dados.idAlunoResponsavel != null)
                {
                    OrientacoesModel orientacao = new OrientacoesModel
                    {
                        idAlunoOrientado = (int)dados.idAlunoResponsavel,
                        idProfessorOrientador = dados.idProfessorResponsavel,
                        idProjeto = dataEntity.Entity.id,
                        idTurma = obterTurmaIdUsuario((int) dados.idAlunoResponsavel),
                        statusAprovacao = Entities.Enumerations.StatusAprovacao.Iniciado
                        
                    };

                    var dataOrientacao = await _orientacoesApp.Add(orientacao);
                    await _orientacoesApp.SaveChangesAsync();
                }

                return Ok(dataEntity.Entity);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is MySqlException mysqlException && mysqlException.Message.Contains("Duplicate entry"))
                {
                    // Retornar uma mensagem padrão personalizada para o usuário
                    return BadRequest("O registro com o mesmo valor já existe. Por favor, verifique seus dados.");
                }

                return BadRequest("Erro Inesperado");

            }

        }

        [HttpDelete]
        [Route("Remover")]
        public override async Task<IActionResult> Remover(int id)
        {
            try
            {
                var dados = _baseApp.Find(id);
                _baseApp.Remove(dados);
                await _baseApp.SaveChangesAsync();
                return Ok(new
                {
                    data = dados,
                    message = "Removido com sucesso."
                });
            }
            catch (Exception ex)
            {

                return BadRequest("Erro Inesperado: " + ex.Message);
            }

        }

        private UsuariosLightModel obterUsuarioLightPorId(int idUsuario)
        {
            var usuario = _usuariosApp.Find(idUsuario);

            if(usuario == null)
            {
                return null;
            }

            return new UsuariosLightModel { id = usuario.id,  nomeCompleto = usuario.nomeCompleto, email = usuario.email, tipoUsuario = usuario.tipoUsuario};
        }
        private int obterTurmaIdUsuario(int idUsuario)
        {
            var turma = _usuarioTurmaApp.FindBy(x => x.idUsuario == idUsuario);

            if(turma == null)
            {
                return 0;
            }

            return turma.idTurma;
        }
    }
}
