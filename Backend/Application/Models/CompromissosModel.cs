using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CompromissosModel : BaseModel
    {
        public TipoCompromisso tipoCompromisso { get; set; }
        public string descricao { get; set; }
        public int idProfessorOrientador { get; set; }
        public int? idAlunoOrientado { get; set; }
        public int? idTurma { get; set; }
        public DateTime dataCompromisso { get; set; }
        public string? local { get; set; }
        public string? link { get; set; }
    }

    public class CompromissosFullModel : BaseModel
    {
        public TipoCompromisso tipoCompromisso { get; set; }
        public string descricao { get; set; }
        public UsuariosLightModel professorOrientador { get; set; }
        public UsuariosLightModel? alunoOrientado { get; set; }
        public TurmasModel? turma { get; set; }
        public DateTime dataCompromisso { get; set; }
        public string? local { get; set; }
        public string? link { get; set; }
    }
}
