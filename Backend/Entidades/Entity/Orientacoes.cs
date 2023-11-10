using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Orientacoes : BaseEntity
    {
        public int idProfessorOrientador { get; set; }
        public int idAlunoOrientado { get; set; }

        public int idProjeto { get; set; }
        public int idTurma { get; set; }
        public StatusAprovacao statusAprovacao { get; set; }
    }
}
