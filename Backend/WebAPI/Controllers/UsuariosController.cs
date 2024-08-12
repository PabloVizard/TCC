using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : BaseController<Usuarios, UsuariosModel>
    {
        private readonly IUsuariosApp _usuariosApp;
        private readonly IPreRegistroApp _preRegistroApp;
        private readonly IBancasApp _bancasApp;
        private readonly IFaltasApp _faltasApp;
        private readonly IOrientacoesApp _orientacoesApp;
        private readonly IProjetosApp _projetosApp;
        private readonly ITarefaAlunoApp _tarefaAlunoApp;
        private readonly IUsuarioTurmaApp _turmaAlunoApp;
        private readonly ITurmasApp _turmasApp;
        public UsuariosController(IUsuariosApp usuariosApp, IPreRegistroApp preRegistroApp, IBancasApp bancasApp,
            IFaltasApp faltasApp, IOrientacoesApp orientacoesApp, IProjetosApp projetosApp, ITarefaAlunoApp tarefaAlunoApp, ITurmasApp turmasApp, IUsuarioTurmaApp usuarioTurmaApp) : base(usuariosApp)
        {
            _usuariosApp = usuariosApp;
            _preRegistroApp = preRegistroApp;
            _bancasApp = bancasApp;
            _faltasApp = faltasApp;
            _orientacoesApp = orientacoesApp;
            _projetosApp = projetosApp;
            _tarefaAlunoApp = tarefaAlunoApp;
            _turmaAlunoApp = usuarioTurmaApp;
            _turmasApp = turmasApp;
        }
        [HttpGet]
        [Route("ObterPorId")]
        public override async Task<IActionResult> ObterPorId(int id)
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

                var usuario = await _usuariosApp.FindAsync(id);

                if (usuario == null)
                {
                    return NoContent();
                }

                UsuariosLightModel usuariosLightModel = new UsuariosLightModel
                {
                    id = usuario.id,
                    email = usuario.email,
                    nomeCompleto = usuario.nomeCompleto,
                    tipoUsuario = usuario.tipoUsuario
                };

                return Ok(usuariosLightModel);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterProfessores")]
        public async Task<IActionResult> ObterProfessores()
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

                var usuarios = await _usuariosApp.ListAsync(x => x.tipoUsuario == Entities.Enumerations.TipoUsuario.Orientador || x.tipoUsuario == Entities.Enumerations.TipoUsuario.ProfessorOrientador || x.tipoUsuario == Entities.Enumerations.TipoUsuario.Coordenador);

                if (usuarios == null)
                {
                    return NoContent();
                }
                List<UsuariosLightModel> retorno = new List<UsuariosLightModel>();

                foreach (var usuario in usuarios)
                {
                    UsuariosLightModel usuariosLightModel = new UsuariosLightModel
                    {
                        id = usuario.id,
                        email = usuario.email,
                        nomeCompleto = usuario.nomeCompleto,
                        tipoUsuario = usuario.tipoUsuario,
                        matricula = usuario.matricula
                    };
                    retorno.Add(usuariosLightModel);
                }

                return Ok(retorno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterTodosProfessoresOrientadores")]
        public async Task<IActionResult> ObterTodosProfessoresOrientadores()
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

                var usuarios = await _usuariosApp.ListAsync(x => x.tipoUsuario == Entities.Enumerations.TipoUsuario.Orientador || x.tipoUsuario == Entities.Enumerations.TipoUsuario.ProfessorOrientador || x.tipoUsuario == Entities.Enumerations.TipoUsuario.Professor || x.tipoUsuario == Entities.Enumerations.TipoUsuario.Coordenador);

                if (usuarios == null)
                {
                    return NoContent();
                }
                List<UsuariosLightModel> retorno = new List<UsuariosLightModel>();

                foreach (var usuario in usuarios)
                {
                    UsuariosLightModel usuariosLightModel = new UsuariosLightModel
                    {
                        id = usuario.id,
                        email = usuario.email,
                        nomeCompleto = usuario.nomeCompleto,
                        cpf = usuario.cpf,
                        tipoUsuario = usuario.tipoUsuario,
                        matricula = usuario.matricula
                    };
                    retorno.Add(usuariosLightModel);
                }

                return Ok(retorno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterTodosAlunos")]
        public async Task<IActionResult> ObterTodosAlunos()
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

                var preRegistros = await _preRegistroApp.ListAsync(x => x.tipoUsuario == Entities.Enumerations.TipoUsuario.Aluno);

                var usuariosRegistrados = await _usuariosApp.ListAsync(x => x.tipoUsuario == Entities.Enumerations.TipoUsuario.Aluno);

                if (preRegistros == null || usuariosRegistrados == null)
                {
                    return NoContent();
                }

                var cpfsUsuariosRegistrados = usuariosRegistrados.Select(x => x.cpf).ToList();

                List<UsuariosFullModel> retorno = new List<UsuariosFullModel>();

                foreach (var usuario in usuariosRegistrados)
                {
                    var preRegistroUsuario = preRegistros.FirstOrDefault(x => x.cpf == usuario.cpf);
                    var idTurma = _turmaAlunoApp.FindBy(x => x.idUsuario == usuario.id)?.idTurma;

                    UsuariosFullModel usuariosFullModel = new UsuariosFullModel
                    {
                        usuario = _usuariosApp.ObterUsuarioLightPorId(usuario.id),
                        preRegistro = preRegistroUsuario,
                        bancas = _bancasApp.FindBy(x => x.idAlunoOrientado == usuario.id),
                        projetos = ObterProjetosFullModelPorIdAluno(usuario.id),
                        orientacoes = _orientacoesApp.FindBy(x => x.idAlunoOrientado == usuario.id),
                        faltas = _faltasApp.FindBy(x => x.idAluno == usuario.id),
                        tarefaAluno = _tarefaAlunoApp.FindBy(x => x.idAluno == usuario.id),
                        turmaAluno = _turmasApp.FindBy(x => x.id == idTurma),
                    };
                    retorno.Add(usuariosFullModel);
                }

                foreach (var preRegistro in preRegistros)
                {
                    if (!cpfsUsuariosRegistrados.Contains(preRegistro.cpf))
                    {
                        UsuariosFullModel usuariosFullModel = new UsuariosFullModel
                        {
                            preRegistro = preRegistro,
                            turmaAluno = _turmasApp.FindBy(x => x.id == preRegistro.idTurma)
                        };
                        retorno.Add(usuariosFullModel);
                    }
                }

                return Ok(retorno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpPut]
        [Route("AtualizarUsuario")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] UsuariosModel usuarios, int idTurma)
        {
            try
            {
                if (usuarios == null)
                {
                    return Unauthorized("Parametros Invalidos");
                }
                var usuario = _usuariosApp.Find(usuarios.id);
                if (usuario == null)
                {
                    return BadRequest("Usuário não existente.");
                }
                usuario.nomeCompleto = usuarios.nomeCompleto ?? usuario.nomeCompleto;
                usuario.matricula = usuarios.matricula;

                _usuariosApp.UpdateEntity(usuario);
                await _usuariosApp.SaveChangesAsync();

                var usuarioTurma = _turmaAlunoApp.FindBy(x => x.idUsuario == usuario.id);

                if (usuarioTurma != null)
                {
                    usuarioTurma.idTurma = idTurma;
                    _turmaAlunoApp.Update(usuarioTurma);
                    await _turmaAlunoApp.SaveChangesAsync();
                }

                return Ok(new
                {
                    data = usuario,
                    message = "Usuário atualizado com sucesso."
                });

            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpPost]
        [Route("CadastrarProfessor")]
        public async Task<IActionResult> CadastrarProfessor([FromBody] UsuariosLightModel usuarios)
        {
            try
            {
                if (usuarios == null)
                {
                    return Unauthorized("Parametros Invalidos");
                }
                var usuarioCadastrar = new UsuariosModel
                {
                    cpf = usuarios.cpf,
                    email = usuarios.email,
                    matricula = usuarios.matricula,
                    nomeCompleto = usuarios.nomeCompleto,
                    tipoUsuario = usuarios.tipoUsuario,
                    senha = Cryptography.ConvertToSha256Hash("123456").ToLower()
                };

                await _usuariosApp.Add(usuarioCadastrar);
                await _usuariosApp.SaveChangesAsync();

                return Ok(new
                {
                    data = usuarioCadastrar,
                    message = "Usuário atualizado com sucesso."
                });

            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpPut]
        [Route("AtualizarProfessor")]
        public async Task<IActionResult> AtualizarProfessor([FromBody] UsuariosLightModel usuarios)
        {
            try
            {
                if (usuarios == null)
                {
                    return Unauthorized("Parametros Invalidos");
                }
                var usuario = _usuariosApp.Find(usuarios.id);

                if(usuario == null)
                {
                    return BadRequest("Usuário não encontrado");
                }

                usuario.email = usuarios.email;
                usuario.tipoUsuario = usuarios.tipoUsuario;
                usuario.matricula = usuarios.matricula;
                usuario.cpf = usuarios.cpf;
                usuario.nomeCompleto = usuarios.nomeCompleto;

                _usuariosApp.Update(usuario);
                await _usuariosApp.SaveChangesAsync();

                return Ok(new
                {
                    data = usuario,
                    message = "Usuário atualizado com sucesso."
                });

            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpDelete]
        [Route("RemoverUsuario")]
        public async Task<IActionResult> RemoverUsuario(string matricula)
        {
            try
            {
                var aluno = _usuariosApp.FindBy(x => x.matricula == matricula);
                var preregistro = _preRegistroApp.FindBy(x => x.matricula == matricula);

                if (aluno == null && preregistro != null)
                {
                    _preRegistroApp.Remove(preregistro);
                    await _preRegistroApp.SaveChangesAsync();
                }
                else if (aluno != null && preregistro == null)
                {
                    _usuariosApp.Remove(aluno);
                    await _usuariosApp.SaveChangesAsync();
                }
                else if (aluno != null && preregistro != null)
                {
                    _preRegistroApp.Remove(preregistro);
                    await _preRegistroApp.SaveChangesAsync();

                    _usuariosApp.Remove(aluno);
                    await _usuariosApp.SaveChangesAsync();
                }

                return Ok(new
                {
                    data = aluno,
                    message = "Removido com sucesso."
                });
            }
            catch (Exception ex)
            {

                return BadRequest("Erro Inesperado: " + ex.Message);
            }

        }

        private ProjetosFullModel ObterProjetosFullModelPorIdAluno(int idUsuario)
        {
            var projeto = _projetosApp.FindBy(x => x.idAlunoResponsavel == idUsuario);

            if (projeto == null)
            {
                return null;
            }

            return new ProjetosFullModel
            {
                id = projeto.id,
                area = projeto.area,
                descricao = projeto.descricao,
                nome = projeto.nome,
                ProfessorResponsavel = _usuariosApp.ObterUsuarioLightPorId(projeto.idProfessorResponsavel),
                AlunoResponsavel = _usuariosApp.ObterUsuarioLightPorId((int)projeto.idAlunoResponsavel)
            };
        }

    }
}
