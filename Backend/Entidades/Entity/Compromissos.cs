using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Compromissos : BaseEntity
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
}
