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
    public class BancasController : BaseController<Bancas, BancasModel>
    {
        private readonly IBancasApp _bancasApp;
        private readonly IUsuariosApp _usuariosApp;
        private readonly IProjetosApp _projetosApp;
        public BancasController(IBancasApp bancasApp, IUsuariosApp usuariosApp, IProjetosApp projetosApp) : base(bancasApp)
        {
            _bancasApp = bancasApp;
            _usuariosApp = usuariosApp;
            _projetosApp = projetosApp;
        }
        [HttpGet]
        [Route("ObterBancaPorAlunoId")]
        public async Task<IActionResult> ObterBancaPorAlunoId(int alunoId)
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

                var banca = await _bancasApp.FindByAsync(x => x.idAlunoOrientado == alunoId);

                if (banca == null)
                {
                    return NoContent();
                }

                BancasFullModel bancaFull = new BancasFullModel
                {
                    id = banca.id,
                    projeto = await _projetosApp.FindByAsync(x => x.id == banca.idProjeto),
                    professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                    alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                    avaliador01 = obterUsuarioLightPorId(banca.idAvaliador01),
                    avaliador02 = banca.idAvaliador02 != null ? obterUsuarioLightPorId((int)banca.idAvaliador02) : null,
                    ano = banca.ano,
                    bancaConfirmada = banca.bancaConfirmada,
                    dataDefesa = banca.dataDefesa,
                    semestre = banca.semestre,
                    status = banca.status
                };

                return Ok(bancaFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterBancaPorOrientadorId")]
        public async Task<IActionResult> ObterBancaPorOrientadorId(int orientadorId)
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

                var bancas = await _bancasApp.ListAsync(x => x.idProfessorOrientador == orientadorId || x.idAvaliador01 == orientadorId || x.idAvaliador02 == orientadorId);

                if (bancas == null)
                {
                    return NoContent();
                }

                List<BancasFullModel> bancasFull = new List<BancasFullModel>();

                foreach (var banca in bancas)
                {
                    bancasFull.Add(new BancasFullModel
                        {
                            id = banca.id,
                            projeto = await _projetosApp.FindByAsync( x => x.id == banca.idProjeto),
                            professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                            alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                            avaliador01 = obterUsuarioLightPorId(banca.idAvaliador01),
                            avaliador02 = banca.idAvaliador02 != null ? obterUsuarioLightPorId((int)banca.idAvaliador02) : null,
                            ano = banca.ano,
                            bancaConfirmada = banca.bancaConfirmada,
                            dataDefesa = banca.dataDefesa,
                            semestre = banca.semestre,
                            status = banca.status,
                        });
                }
                

                return Ok(bancasFull);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
        [HttpGet]
        [Route("ObterTodasBancas")]
        public async Task<IActionResult> ObterTodasBancas()
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

                var bancas = await _bancasApp.ListAsync();

                if (bancas == null)
                {
                    return NoContent();
                }

                List<BancasFullModel> bancasFull = new List<BancasFullModel>();

                foreach (var banca in bancas)
                {
                    bancasFull.Add(new BancasFullModel
                    {
                        id = banca.id,
                        projeto = await _projetosApp.FindByAsync(x => x.id == banca.idProjeto),
                        professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                        alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                        avaliador01 = obterUsuarioLightPorId(banca.idAvaliador01),
                        avaliador02 = banca.idAvaliador02 != null ? obterUsuarioLightPorId((int)banca.idAvaliador02) : null,
                        ano = banca.ano,
                        bancaConfirmada = banca.bancaConfirmada,
                        dataDefesa = banca.dataDefesa,
                        semestre = banca.semestre,
                        status = banca.status
                    });
                }


                return Ok(bancasFull.OrderByDescending(x => x.ano).ThenByDescending(x => x.semestre)) ;
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterBancasNaoConfirmadas")]
        public async Task<IActionResult> ObterBancasNaoConfirmadas()
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

                var bancas = await _bancasApp.ListAsync(x => !x.bancaConfirmada);

                if (bancas == null)
                {
                    return NoContent();
                }

                List<BancasFullModel> bancasFull = new List<BancasFullModel>();

                foreach (var banca in bancas)
                {
                    bancasFull.Add(new BancasFullModel
                    {
                        id = banca.id,
                        projeto = await _projetosApp.FindByAsync(x => x.id == banca.idProjeto),
                        professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                        alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                        avaliador01 = obterUsuarioLightPorId(banca.idAvaliador01),
                        avaliador02 = banca.idAvaliador02 != null ? obterUsuarioLightPorId((int)banca.idAvaliador02) : null,
                        ano = banca.ano,
                        bancaConfirmada = banca.bancaConfirmada,
                        dataDefesa = banca.dataDefesa,
                        semestre = banca.semestre,
                        status = banca.status
                    });
                }


                return Ok(bancasFull.OrderByDescending(x => x.ano).ThenByDescending(x => x.semestre));
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpPut]
        [Route("ConfirmarSugestao")]
        public async Task<IActionResult> ConfirmarSugestao(BancasModel ba)
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

                var bancas = await _bancasApp.ListAsync(x => !x.bancaConfirmada);

                if (bancas == null)
                {
                    return NoContent();
                }

                List<BancasFullModel> bancasFull = new List<BancasFullModel>();

                foreach (var banca in bancas)
                {
                    bancasFull.Add(new BancasFullModel
                    {
                        id = banca.id,
                        projeto = await _projetosApp.FindByAsync(x => x.id == banca.idProjeto),
                        professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                        alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                        avaliador01 = obterUsuarioLightPorId(banca.idAvaliador01),
                        avaliador02 = banca.idAvaliador02 != null ? obterUsuarioLightPorId((int)banca.idAvaliador02) : null,
                        ano = banca.ano,
                        bancaConfirmada = banca.bancaConfirmada,
                        dataDefesa = banca.dataDefesa,
                        semestre = banca.semestre,
                        status = banca.status
                    });
                }


                return Ok(bancasFull.OrderByDescending(x => x.ano).ThenByDescending(x => x.semestre));
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("BalancearBancas")]
        public async Task<IActionResult> BalancearBancas()
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

                var bancasNaoConfirmadas = await _bancasApp.ListAsync(x => !x.bancaConfirmada);

                if (bancasNaoConfirmadas == null || !bancasNaoConfirmadas.Any())
                {
                    return NoContent();
                }

                var avaliadores = await _usuariosApp.ListAsync(x =>
                    x.tipoUsuario == Entities.Enumerations.TipoUsuario.Orientador ||
                    x.tipoUsuario == Entities.Enumerations.TipoUsuario.ProfessorOrientador ||
                    x.tipoUsuario == Entities.Enumerations.TipoUsuario.Coordenador);

                if (avaliadores == null || !avaliadores.Any())
                {
                    return BadRequest("Não há avaliadores disponíveis.");
                }

                var bancasComSugestoes = new List<BancasFullModel>();
                var avaliadoresBancaCount = new Dictionary<int, int>();

                // Inicializando o dicionário com a contagem de bancas confirmadas para cada avaliador
                foreach (var avaliador in avaliadores)
                {
                    var bancasConfirmadas = await _bancasApp.ListAsync(x => x.idAvaliador01 == avaliador.id && x.bancaConfirmada);
                    avaliadoresBancaCount[avaliador.id] = bancasConfirmadas.Count();
                }

                foreach (var banca in bancasNaoConfirmadas)
                {
                    var avaliadoresDisponiveis = avaliadores
                        .Where(a => a.id != banca.idProfessorOrientador)
                        .OrderBy(a => avaliadoresBancaCount[a.id])
                        .ToList();

                    var avaliador01 = EscolherAvaliadorBalanceado(avaliadoresDisponiveis, avaliadoresBancaCount);

                    if (avaliador01 != null)
                    {
                        avaliadoresBancaCount[avaliador01.id]++;
                    }
                    else
                    {
                        return BadRequest("Erro ao selecionar avaliador01.");
                    }

                    bancasComSugestoes.Add(new BancasFullModel
                    {
                        id = banca.id,
                        projeto = await _projetosApp.FindByAsync(x => x.id == banca.idProjeto),
                        professorOrientador = obterUsuarioLightPorId(banca.idProfessorOrientador),
                        alunoOrientado = obterUsuarioLightPorId(banca.idAlunoOrientado),
                        avaliador01 = obterUsuarioLightPorId(avaliador01.id),
                        ano = banca.ano,
                        bancaConfirmada = banca.bancaConfirmada,
                        dataDefesa = banca.dataDefesa,
                        semestre = banca.semestre,
                        status = banca.status
                    });
                }

                var bancasOrdenadas = bancasComSugestoes
                    .OrderByDescending(x => x.ano)
                    .ThenByDescending(x => x.semestre)
                    .ToList();

                return Ok(bancasOrdenadas);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        // Método auxiliar para escolher um avaliador balanceado
        private Usuarios EscolherAvaliadorBalanceado(List<Usuarios> avaliadores, Dictionary<int, int> avaliadoresBancaCount)
        {
            if (!avaliadores.Any())
            {
                return null;
            }

            var menosBancas = avaliadoresBancaCount.Values.Min();
            var avaliadoresComMenosBancas = avaliadores.Where(a => avaliadoresBancaCount[a.id] == menosBancas).ToList();

            if (!avaliadoresComMenosBancas.Any())
            {
                return null;
            }

            var random = new Random();
            return avaliadoresComMenosBancas[random.Next(avaliadoresComMenosBancas.Count)];
        }






        private UsuariosLightModel obterUsuarioLightPorId(int idUsuario)
        {
            var usuario = _usuariosApp.Find(idUsuario);

            if (usuario == null)
            {
                return null;
            }

            return new UsuariosLightModel { id = usuario.id, nomeCompleto = usuario.nomeCompleto, email = usuario.email, tipoUsuario = usuario.tipoUsuario, matricula = usuario.matricula };
        }
    }
}
