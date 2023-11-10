using Entities.Entity;
using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrientacoesModel : BaseModel
    {
        public int idProfessorOrientador { get; set; }
        public int idAlunoOrientado { get; set; }

        public int idProjeto { get; set; }
        public int idTurma { get; set; }
        public StatusAprovacao statusAprovacao { get; set; }
        public string? anexoResumoTrabalho { get; set; }
        public string? localDivulgacao { get; set; }
    }

    public class OrientacoesFullModel : BaseModel
    {
        public UsuariosLightModel ProfessorOrientador { get; set; }
        public UsuariosLightModel AlunoOrientado { get; set; }

        public ProjetosModel Projeto { get; set; }
        public TurmasModel Turma { get; set; }
        public StatusAprovacao statusAprovacao { get; set; }
        public string? anexoResumoTrabalho { get; set; }
        public string? localDivulgacao { get; set; }
    }
}
